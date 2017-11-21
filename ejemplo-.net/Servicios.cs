using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Xsl;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Main
{
    public class Servicios
    {
        public string timbrar_cfdi(string user_name, string password, string xml_base64)
        {
            try
            {
                TimboxWS.timbrado_cfdi33_portClient cliente_timbrar = new TimboxWS.timbrado_cfdi33_portClient();
                TimboxWS.timbrar_cfdi_result response = new TimboxWS.timbrar_cfdi_result();
                response = cliente_timbrar.timbrar_cfdi(user_name, password, xml_base64);
                XmlDocument cfdi_timbrado = new XmlDocument();
                cfdi_timbrado.LoadXml(response.xml);
                Console.WriteLine(response.xml);

                return response.xml;
            }
            catch (System.ServiceModel.FaultException e)
            {
                Console.WriteLine("Código de error " + e.Code.Name + ": " + e.Message);
                return "Código de error: " + e.Code.Name + "\n" + e.Message;
            }
        }

        public string cancelar_cfdi(string user_name, string password, string rfc, string uuid, string pfx_base64, string pfx_password)
        {
            try
            {
                string[] arr_uuids = new string[1] { uuid };
                TimboxWS.uuid uuids = new TimboxWS.uuid();
                uuids.uuid1 = arr_uuids;
                TimboxWS.timbrado_cfdi33_port cliente_cancelar = new TimboxWS.timbrado_cfdi33_portClient();
                TimboxWS.cancelar_cfdi_result response = new TimboxWS.cancelar_cfdi_result();
                response = cliente_cancelar.cancelar_cfdi(user_name, password, rfc, uuids, pfx_base64, pfx_password);
                XmlDocument acuse_cancelacion = new XmlDocument();
                acuse_cancelacion.LoadXml(response.acuse_cancelacion);
                XmlDocument comprobantes_cancelados = new XmlDocument();
                comprobantes_cancelados.LoadXml(response.comprobantes_cancelados);
                Console.WriteLine(response.comprobantes_cancelados.ToString());

                return response.comprobantes_cancelados.ToString();
            }
            catch (System.ServiceModel.FaultException e)
            {
                Console.WriteLine("Código de error " + e.Code.Name + ": " + e.Message);
                return "Código de error: " + e.Code.Name + "\n" + e.Message;
            }
        }

        public string generar_sello(string archivo, string path)
        {
            try
            {
                XmlDocument doc_xml = new XmlDocument();
                //Se carga archivo
                doc_xml.Load(archivo);

                XmlNode root = doc_xml.DocumentElement;
                XmlNamespaceManager manager = new XmlNamespaceManager(doc_xml.NameTable);
                manager.AddNamespace("ns", "http://www.sat.gob.mx/cfd/3");
                XmlNode node = doc_xml.DocumentElement.SelectSingleNode("//ns:Comprobante", manager);

                //Se agrega la fecha actual al momento de generar cfdi
                string fecha = node.Attributes.GetNamedItem("Fecha").Value;
                string fecha_hoy = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
                node.Attributes.GetNamedItem("Fecha").Value = fecha_hoy;
                doc_xml.Save(archivo);

                XslCompiledTransform xsl = new XslCompiledTransform();
                xsl.Load(@path + "\\Archivos\\cadenaoriginal_3_3.xslt");
                XmlTextWriter xmlwritter = new XmlTextWriter(@path + "\\Archivos\\cadena_original.txt", null);
                xsl.Transform(archivo, null, xmlwritter);
                xmlwritter.Close();
                string private_key = File.ReadAllText(@path + "\\Archivos\\CSD01_AAA010101AAA.key.pem");

                //Ejecutar comandos para obtener obtener el sello
                //System.Diagnostics.Process proc = new System.Diagnostics.Process();
                string password = "12345678a";
                X509Certificate2 cert = new X509Certificate2(@path + "/Archivos/CSD01_AAA010101AAA.pfx", password);
                RSACryptoServiceProvider key = (RSACryptoServiceProvider)cert.PrivateKey;

                CspParameters cspParam = new CspParameters();
                cspParam.KeyContainerName = key.CspKeyContainerInfo.KeyContainerName;
                cspParam.KeyNumber = key.CspKeyContainerInfo.KeyNumber == KeyNumber.Exchange ? 1 : 2;

                RSACryptoServiceProvider key2 = new RSACryptoServiceProvider(cspParam);
                key2.PersistKeyInCsp = false;

                SHA256Managed sha256 = new SHA256Managed();
                UnicodeEncoding encoding = new UnicodeEncoding();
                byte[] cadenaOriginal = File.ReadAllBytes(@path + "\\Archivos\\cadena_original.txt");
                sha256.ComputeHash(cadenaOriginal);
                byte[] hash = sha256.ComputeHash(cadenaOriginal);
                string sha256id = CryptoConfig.MapNameToOID("SHA256");
                byte[] sello_bytes = key2.SignHash(hash, sha256id);
                string sello_str = Convert.ToBase64String(sello_bytes);


                node.Attributes.GetNamedItem("Sello").Value = sello_str;
                doc_xml.Save(archivo);

                //conversion de xml a base64
                byte[] base64 = Encoding.UTF8.GetBytes(doc_xml.InnerXml);
                string convertXml = Convert.ToBase64String(base64);

                return convertXml;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return string.Empty;
            }
        }
    }
}
