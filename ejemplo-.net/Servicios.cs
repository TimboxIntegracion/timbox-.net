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
using System.Collections;

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

        public string cancelar_cfdi(string user_name, string password, string rfc_emisor, List<FoliosCancelar> folios_cancelar,  string cer_file, string key_file)
        {
            try
            {
                TimboxWSCancelacion.folios folios_datos = new TimboxWSCancelacion.folios();
                var lista_folios = new List<TimboxWSCancelacion.folio>();
            
                foreach (var i in folios_cancelar)
                {
                    lista_folios.Add(new TimboxWSCancelacion.folio { uuid = i.Uuid, rfc_receptor = i.Rfc_receptor, total = i.Total });
                }

                var folio_array = lista_folios.ToArray();
                folios_datos.folio = folio_array;

                TimboxWSCancelacion.cancelacion_portClient cliente_cancelar = new TimboxWSCancelacion.cancelacion_portClient();
                TimboxWSCancelacion.cancelar_cfdi_result response = new TimboxWSCancelacion.cancelar_cfdi_result();

                response = cliente_cancelar.cancelar_cfdi(user_name, password, rfc_emisor, folios_datos, cer_file, key_file);
                           
                XmlDocument acuse_cancelacion = new XmlDocument();
                acuse_cancelacion.LoadXml(response.folios_cancelacion);

                Console.WriteLine(response.folios_cancelacion.ToString());
                return response.folios_cancelacion.ToString();

            }
            catch (System.ServiceModel.FaultException e)
            {
                Console.WriteLine("Código de error " + e.Code.Name + ": " + e.Message);
                return "Código de error: " + e.Code.Name + "\n" + e.Message;
            }
        }

        public string consultar_estatus(string user_name, string password, string uuid, string rfc_emisor, string rfc_receptor, string total)
        {
            try
            {
                TimboxWSCancelacion.cancelacion_portClient cliente_cosultar = new TimboxWSCancelacion.cancelacion_portClient();
                TimboxWSCancelacion.consultar_estatus_result response = new TimboxWSCancelacion.consultar_estatus_result();

                response = cliente_cosultar.consultar_estatus(user_name, password, uuid, rfc_emisor, rfc_receptor, total);

                Console.WriteLine(response.estatus_cancelacion.ToString());
                return response.estatus_cancelacion.ToString();
            }
            catch (System.ServiceModel.FaultException e)
            {
                Console.WriteLine("Código de error " + e.Code.Name + ": " + e.Message);
                return "Código de error: " + e.Code.Name + "\n" + e.Message;
            }
        }

        public string consultar_penticiones_pendientes(string user_name, string password, string rfc_recptor, string cer_file, string key_file)
        {
            try
            {
                TimboxWSCancelacion.cancelacion_portClient cliente_consultar = new TimboxWSCancelacion.cancelacion_portClient();
                TimboxWSCancelacion.consultar_peticiones_pendientes_result response = new TimboxWSCancelacion.consultar_peticiones_pendientes_result();

                response = cliente_consultar.consultar_peticiones_pendientes(user_name, password, rfc_recptor, cer_file, key_file);

                Console.WriteLine(response.codestatus.ToString());
                return response.codestatus.ToString();
            }
            catch (System.ServiceModel.FaultException e)
            {
                Console.WriteLine("Código de error " + e.Code.Name + ": " + e.Message);
                return "Código de error: " + e.Code.Name + "\n" + e.Message;
            }
        }

        public string procesar_respuesta(string user_name, string password, string rfc_receptor, List<FoliosRespuestas> folios_procesar, string cer_file, string key_file)
        {
            try
            {
                TimboxWSCancelacion.respuestas respuestas = new TimboxWSCancelacion.respuestas();
                var lista_folios = new List<TimboxWSCancelacion.folios_respuestas>();

                foreach (var i in folios_procesar)
                {
                   lista_folios.Add(new TimboxWSCancelacion.folios_respuestas { uuid = i.Uuid, rfc_emisor = i.Rfc_emisor, total = i.Total, respuesta = i.Respuesta});
                }

                var folio_array = lista_folios.ToArray();
                respuestas.folios_respuestas = folio_array;

                TimboxWSCancelacion.cancelacion_portClient cliente_procesar = new TimboxWSCancelacion.cancelacion_portClient();
                TimboxWSCancelacion.procesar_respuesta_result response = new TimboxWSCancelacion.procesar_respuesta_result();

                response = cliente_procesar.procesar_respuesta(user_name, password, rfc_receptor, respuestas, cer_file, key_file);

                Console.WriteLine(response.folios.ToString());
                return response.folios.ToString();
            }
            catch (System.ServiceModel.FaultException e)
            {
                Console.WriteLine("Código de error " + e.Code.Name + ": " + e.Message);
                return "Código de error: " + e.Code.Name + "\n" + e.Message;
            }
        }

        public string consultar_relacionados(string user_name, string password, string uuid, string rfc_receptor, string cer_file, string key_file)
        {
            try
            {
                TimboxWSCancelacion.cancelacion_portClient cliente_consultar_relacinados = new TimboxWSCancelacion.cancelacion_portClient();
                TimboxWSCancelacion.consultar_documento_relacionado_result response = new TimboxWSCancelacion.consultar_documento_relacionado_result();

                response = cliente_consultar_relacinados.consultar_documento_relacionado(user_name, password, uuid,rfc_receptor,cer_file,key_file);

                string result = response.resultado.ToString() + " " + response.relacionados_padres.ToString() + " " + response.relacionados_hijos.ToString();
                Console.WriteLine(result);

                return result;
            }
            catch(System.ServiceModel.FaultException e)
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
