using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Ejemplo_C_sharp
{
    public class Servicios
    {
        public string timbrar_cfdi(string user_name, string password, string xml_base64)
        {
            try
            {
                var timbox_cliente = new Timbrado_40_Pruebas.timbrado_cfdi40_portClient();
                Timbrado_40_Pruebas.timbrar_cfdi_result response = new Timbrado_40_Pruebas.timbrar_cfdi_result();

                response = timbox_cliente.timbrar_cfdi(user_name, password, xml_base64);

                XmlDocument cfdi_timbrado = new XmlDocument();
                cfdi_timbrado.LoadXml(response.xml);

                return response.xml;

            }
            catch (Exception e){
                Console.WriteLine(e.Message);
                return "Mensaje de Error: \n " + e.Message;
            }
        }

        public string obtener_comprobante(string user_name, string password, string uuid)
        {
            try
            {
                var timbox_cliente = new Timbrado_40_Pruebas.timbrado_cfdi40_portClient();
                Timbrado_40_Pruebas.recuperar_comprobante_result response = new Timbrado_40_Pruebas.recuperar_comprobante_result();

                var folio = new Timbrado_40_Pruebas.recuperar();
                var final = new Timbrado_40_Pruebas.recuperar();
                var comprobante = new Timbrado_40_Pruebas.Comprobante();
                var temp_folio = new Timbrado_40_Pruebas.Comprobante[1];

                comprobante.uuid = uuid;
                temp_folio.SetValue(comprobante, 0);
                folio.Comprobante = temp_folio;
                
                response = timbox_cliente.recuperar_comprobante(user_name, password, folio);

                Console.WriteLine(response.ToString());

                return response.comprobantes.ToString();
            }
            catch (Exception e){
                Console.WriteLine(e);
                return "Mensaje Error: ";  
            }
        }

        public string buscar_cfdi(string user_name, string password)
        {
            try
            {
                var timbox_cliente = new Timbrado_40_Pruebas.timbrado_cfdi40_portClient();
                Timbrado_40_Pruebas.buscar_cfdis_result response = new Timbrado_40_Pruebas.buscar_cfdis_result();

                var datos_busqueda = new Timbrado_40_Pruebas.parametros_cfdis();

                datos_busqueda.fecha_emision_inicio = "2023-01-01";
                datos_busqueda.fecha_emision_fin = "2023-03-23";

                response = timbox_cliente.buscar_cfdis(user_name, password, datos_busqueda);

                Console.WriteLine(response.cantidad);
                Console.WriteLine(response.comprobantes);

                return response.comprobantes.ToString();
            }catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "Mensaje de Error: ";
            }
        }
        
        public string obtener_acuse(string user_name, string password, string uuid)
        {
            try
            {
                var timbox_cliente = new Timbrado_40_Pruebas.timbrado_cfdi40_portClient();
                Timbrado_40_Pruebas.buscar_acuse_recepcion_result response = new Timbrado_40_Pruebas.buscar_acuse_recepcion_result();

                var acuse = new Timbrado_40_Pruebas.parametros_acuse();
                var acuse_temp = new Timbrado_40_Pruebas.parametros_acuse[1];
                var acuse_final = new Timbrado_40_Pruebas.parametros_acuse();
                var folio = new Timbrado_40_Pruebas.recuperar();
                var comprobante = new Timbrado_40_Pruebas.Comprobante();
                var temp_folio = new Timbrado_40_Pruebas.Comprobante[1];

                comprobante.uuid = uuid;
                temp_folio.SetValue(comprobante, 0);
                folio.Comprobante = temp_folio;
                
                acuse.uuids = folio;
                acuse_final.uuids = acuse.uuids;


                response = timbox_cliente.buscar_acuse_recepcion(user_name, password, acuse);

                Console.WriteLine(response.acuses);

                return "No Encontrados";
            }
            catch(Exception e){
                Console.WriteLine("Error");
                Console.WriteLine(e.Message);
                return "Error: ";
            }
        }

        public string cancelar_cfdi(string user_name, string password,string emisor, String[][] folios_cancelar,int no_folios, string cer_file, string key_file)
        {

            try
            {
                // Llenado de Folios
                var folios = new Cancelacion_Pruebas.folios();

                //Objeto folio
                var folio = new Cancelacion_Pruebas.folio();

                //Objeto temporal de folio
                var temp_folios = new Cancelacion_Pruebas.folio[no_folios];

                for (int i = 0; i < no_folios; i++)
                {
                    folio.uuid = folios_cancelar[i][0];
                    folio.rfc_receptor = folios_cancelar[i][1];
                    folio.total = folios_cancelar[i][2];
                    folio.motivo = folios_cancelar[i][3];
                    folio.folio_sustituto = folios_cancelar[i][4];

                    temp_folios.SetValue(folio, i);
                }

                //Asignación de los folios a cancelar
                folios.folio = temp_folios;

                //Llamado a los servicios de Timbox
                var timbox_cliente = new Cancelacion_Pruebas.cancelacion_portClient();
                Cancelacion_Pruebas.cancelar_cfdi_result response = new Cancelacion_Pruebas.cancelar_cfdi_result();

                response = timbox_cliente.cancelar_cfdi(user_name, password, emisor, folios, cer_file, key_file);

                XmlDocument acuse_cancelacion = new XmlDocument();
                acuse_cancelacion.LoadXml(response.acuse_cancelacion);

                Console.WriteLine(acuse_cancelacion);

                XmlDocument folios_cancelados = new XmlDocument();
                folios_cancelados.LoadXml(response.folios_cancelacion);

                return response.folios_cancelacion.ToString();

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return  "Error: " +  e.Message; 
            }
        }

        public string consultar_estatus(string user_name, string password, string rfc_emisor, string rfc_receptor, string total, string uuid)
        {
            try
            {
                //Llamado a los servicios de Timbox
                var timbox_cliente = new Cancelacion_Pruebas.cancelacion_portClient();
                Cancelacion_Pruebas.consultar_estatus_result response = new Cancelacion_Pruebas.consultar_estatus_result();

                // Llamado al servicio de Consultar Estatus
                response = timbox_cliente.consultar_estatus(user_name, password, uuid, rfc_emisor, rfc_receptor, total);

                Console.WriteLine(response.codigo_estatus);
                Console.WriteLine(response.estado);
                Console.WriteLine(response.estatus_cancelacion);
                Console.WriteLine(response.es_cancelable);

                return response.estado + "\n" + response.estado;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "Error";
            }
        }

        public string consultar_peticiones_pendientes(string username, string password, string rfc_receptor, string cer_file, string key_file)
        {
            try
            {
                //Llamado a los servicios de Timbox
                var timbox_cliente = new Cancelacion_Pruebas.cancelacion_portClient();
                Cancelacion_Pruebas.consultar_peticiones_pendientes_result response = new Cancelacion_Pruebas.consultar_peticiones_pendientes_result();

                // Llamado al método
                response = timbox_cliente.consultar_peticiones_pendientes(username, password, rfc_receptor, cer_file, key_file);

                Console.WriteLine(response.codestatus);
                Console.WriteLine(response.uuids);

                return response.codestatus + "\n" + response.uuids;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "Mensaje Error:" + e.Message;
            }
        }

        public string consultar_documentos_relacionados(string username, string password, string rfc_emisor, string rfc_receptor, string uuid, string cer_file, string key_file)
        {
            try
            {
                //Llamado a los servicios de Timbox
                var timbox_cliente = new Cancelacion_Pruebas.cancelacion_portClient();
                Cancelacion_Pruebas.consultar_documento_relacionado_result response = new Cancelacion_Pruebas.consultar_documento_relacionado_result();

                // Llamado del metodo
                response = timbox_cliente.consultar_documento_relacionado(username, password, uuid, rfc_emisor, rfc_receptor, cer_file, key_file);

                // Respuestas del servicio
                Console.WriteLine(response.resultado);
                Console.WriteLine(response.relacionados_hijos);
                Console.WriteLine(response.relacionados_padres);

                return response.resultado;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "Error: " + e.Message;
            }
        }

        public string procesar_respuesta(string user_name, string password, string rfc_receptor, string cer_file, string key_file, String[][] folios_procesar, int cantidad)
        {
            try
            {
                // Objeto para contener las respuestas
                var respuestas_solicitud = new Cancelacion_Pruebas.respuestas();
                // Objeto temporal para almacenar los folios por respuesta
                var folio_respuesta = new Cancelacion_Pruebas.folios_respuestas();
                // Objeto para el total de las respuestas
                var total_respuestas = new Cancelacion_Pruebas.folios_respuestas[cantidad];

                // Llenado de las respuestas
                for(int i = 0; i < cantidad; i++)
                {
                    folio_respuesta.respuesta = folios_procesar[i][0];
                    folio_respuesta.rfc_emisor = folios_procesar[i][1];
                    folio_respuesta.uuid = folios_procesar[i][2];
                    folio_respuesta.total = folios_procesar[i][3];

                    // Se añaden al objeto de arreglos
                    total_respuestas.SetValue(folio_respuesta, 0);
                }

                // Se añaden el total de las respuestas a la solicitud
                respuestas_solicitud.folios_respuestas = total_respuestas;

                // Llamado a los servicios de Timbox
                var timbox_cliente = new Cancelacion_Pruebas.cancelacion_portClient();
                Cancelacion_Pruebas.procesar_respuesta_result response = new Cancelacion_Pruebas.procesar_respuesta_result();

                // Llamado del metodo
                response = timbox_cliente.procesar_respuesta(user_name, password, rfc_receptor, respuestas_solicitud, cer_file, key_file);

                // Respuestas del servicio
                Console.WriteLine(response.folios);

                return response.folios.ToString();
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return "Error: " + e.Message;
            }
        }

        public string timbar_retencion(string user_name, string password, string xml_bas64)
        {
            try
            {
                // Llamado a los servicios de timbox
                var timbox_cliente = new Timbrado_Retenciones_Pruebas.retencion_portClient();
                Timbrado_Retenciones_Pruebas.timbrar_retencion_result response = new Timbrado_Retenciones_Pruebas.timbrar_retencion_result();

                // Llamado al metodo de timbrar
                response = timbox_cliente.timbrar_retencion(user_name, password, xml_bas64);

                XmlDocument cfdi_timbrado = new XmlDocument();
                cfdi_timbrado.LoadXml(response.xml);

                return response.xml;
                
            }catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "Error: " + e.Message;
            }
        }

        public string cancelar_retencion_uno(string user_name, string password, string rfc_emisor, String[][] param, string cer_file, string key_file)
        {
            try
            {
                var timbox_cliente = new Timbrado_Retenciones_Pruebas.retencion_portClient();
                Timbrado_Retenciones_Pruebas.cancelar_retencion_result response = new Timbrado_Retenciones_Pruebas.cancelar_retencion_result();

                // Llenado de los folios a cancelar
                var tem_folio = new Timbrado_Retenciones_Pruebas.Uuid[param.Length];
                var folios = new Timbrado_Retenciones_Pruebas.Uuid();
                var uuids = new Timbrado_Retenciones_Pruebas.uuid();

                for(int i = 0; i < param.Length; i++)
                {
                    folios.uuid = param[i][0];
                    folios.motivo = param[i][1];
                    folios.folio_sustituto = param[i][2];

                    tem_folio.SetValue(folios, i);
                }

                uuids.Uuid = tem_folio;

                response = timbox_cliente.cancelar_retencion(user_name, password, rfc_emisor, uuids, cer_file, key_file);

                return "";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "Error: " + e.Message;
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
                manager.AddNamespace("ns", "http://www.sat.gob.mx/cfd/4");
                XmlNode node = doc_xml.DocumentElement.SelectSingleNode("//ns:Comprobante", manager);

                string cp = node.Attributes.GetNamedItem("LugarExpedicion").Value;
                string time_zone = "";
                var postalcodes_file = File.ReadLines(@path + "\\Archivos\\cat_postal_codes.csv");

                // Microsoft Time Zone Index Values
                // Url: https://support.microsoft.com/en-us/help/973627/microsoft-time-zone-index-values
                foreach (var line in postalcodes_file)
                {
                    string cp_file = line.Split(',')[1];
                    if (cp_file == cp)
                    {
                        time_zone = line.Split(',')[6];
                        break;
                    }
                }

                DateTime fecha_hoy = DateTime.Now;
                var timezone_info = TimeZoneInfo.FindSystemTimeZoneById(time_zone);
                var convertir_fecha_hoy = TimeZoneInfo.ConvertTime(fecha_hoy, timezone_info);
                string fecha = convertir_fecha_hoy.ToString("yyyy-MM-ddTHH:mm:ss");

                //Se agrega la fecha actual al momento de generar cfdi
                node.Attributes.GetNamedItem("Fecha").Value = fecha;
                doc_xml.Save(archivo);

                XslCompiledTransform xsl = new XslCompiledTransform();

                xsl.Load(@path + "\\Archivos\\cadenaoriginal_4_0.xslt");

                XmlTextWriter xmlwritter = new XmlTextWriter(@path + "\\Archivos\\cadena_original.txt", null);
                xsl.Transform(archivo, null, xmlwritter);
                xmlwritter.Close();

                //Ejecutar comandos para obtener obtener el sello
                //System.Diagnostics.Process proc = new System.Diagnostics.Process();
                string password = "12345678a";
                X509Certificate2 cert = new X509Certificate2(@path + "/Archivos/XIA190128J61.pfx",password);
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
