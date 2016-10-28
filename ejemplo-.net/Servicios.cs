using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Main
{
    class Servicios
    {

        public bool timbrar_cfdi(string user_name, string password, string xml_base64)
        {
            try
            {
                TimboxWS.timbrado_portClient cliente_timbrar = new TimboxWS.timbrado_portClient();
                TimboxWS.timbrar_cfdi_result response = new TimboxWS.timbrar_cfdi_result();
                response = cliente_timbrar.timbrar_cfdi(user_name, password, xml_base64);
                XmlDocument cfdi_timbrado = new XmlDocument();
                cfdi_timbrado.LoadXml(response.xml);
                Console.WriteLine(response.xml);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool cancelar_cfdi(string user_name, string password, string rfc, string uuid, string pfx_base64, string pfx_password)
        {
            try
            {
                string[] arr_uuids = new string[1] { uuid };
                TimboxWS.uuid uuids = new TimboxWS.uuid();
                uuids.uuid1 = arr_uuids;
                TimboxWS.timbrado_port cliente_cancelar = new TimboxWS.timbrado_portClient();
                TimboxWS.cancelar_cfdi_result response = new TimboxWS.cancelar_cfdi_result();
                response = cliente_cancelar.cancelar_cfdi(user_name, password, rfc, uuids, pfx_base64, pfx_password);
                XmlDocument acuse_cancelacion = new XmlDocument();
                acuse_cancelacion.LoadXml(response.acuse_cancelacion);
                XmlDocument comprobantes_cancelados = new XmlDocument();
                comprobantes_cancelados.LoadXml(response.comprobantes_cancelados);
                Console.WriteLine(response.ToString());
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
