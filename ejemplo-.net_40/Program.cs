using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejemplo_C_sharp
{
    class Program
    {
        static void Main(string[] args)
        {

            // Datos Globales
            String usuario = "";
            String password = "";

            string path = "";

            // Certificados en formato PEM
            string file_cer_pem = File.ReadAllText(path + "//Archivos//XIA190128J61.cer.pem");
            string file_key_pem = File.ReadAllText(path + "//Archivos//XIA190128J61.cer.key");

            Servicios servicios_timbox = new Servicios();

            // Llamado del servicio de timbrado
            //servicios_timbox.timbrar_cfdi(usuario, password, "");

            // Numero de UUID's a cancelar
            //int no_folios = 1;
            // Llenado de los datos para la cancelación
            //String[][] datos = new String[no_folios][];
            // Los datos para la cancelación siguen el siguiente orden: UUID, rfc receptor, total, motivo y folio sustituto
            //datos[0] = new String[5] { "F0B60888-BC93-4851-A345-03C238572A8D", "XEXX010101000", "0.0", "02", "" };
            // Llamado del servicio de cancelacion
            //servicios_timbox.cancelar_cfdi(usuario, password, "XIA190128J61", datos, no_folios, file_cer_pem, file_key_pem);

            // Llamado al servicio de recuperar comprobante
            //servicios_timbox.obtener_comprobante(usuario, password, "500CF7AF-AFA0-4B6C-9827-DF39CCB4BB27");

            // Llamado al servicio de buscar cfdi
            //servicios_timbox.buscar_cfdi(usuario, password);

            // Llamado del servicio recuperar acuse
            //servicios_timbox.obtener_acuse(usuario, password, "500CF7AF-AFA0-4B6C-9827-DF39CCB4BB27");

            //Llamado de los servicios de consultar estatus
            //servicios_timbox.consultar_estatus(usuario, password, "XIA190128J61", "XIA190128J61", "502.00", "500CF7AF-AFA0-4B6C-9827-DF39CCB4BB27");

            // Llamado de los servicios de consultar peticiones pendientes
            //servicios_timbox.consultar_peticiones_pendientes(usuario, password, "XIA190128J61", , file_cer_pem, file_key_pem);

            // LLamado al servicio de timbrar retenciones
            //servicios_timbox.timbar_retencion(usuario, password, "");


            // Numero de UUID's a cancelar
            int no_folios = 1;
            // Llenado de los datos para la cancelación
            String[][] datos = new String[no_folios][];
            // Los datos para la cancelación siguen el siguiente orden: UUID, motivo y folio sustituto
            datos[0] = new String[3] { "500CF7AF-AFA0-4B6C-9827-DF39CCB4BB27", "02", "" };
            // Llamado al servicio de cancelar retencion
            servicios_timbox.cancelar_retencion_uno(usuario, password, "XIA190128J61", datos, file_cer_pem, file_key_pem);

        }
    }
}
