using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.Diagnostics;
using System.Reflection;
using System.Data;
using System.Xml;
using System.Xml.Linq;

namespace Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //VARIABLES
        private string userName = "AAA010101000";
        private string password = "h6584D56fVdBbSmmnB";
        private string fileStream = "\\Archivos\\ejemplo_cfdi_33_cancelada_con_aceptacion.xml";
        string path = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));

        private string xmlBase64 = string.Empty;
        private string respuesta_servicio = string.Empty;
        private string respuesta_cancelar = string.Empty;
        private string respuesta_consultar = string.Empty;
        private string respuesta_pendientes = string.Empty;
        private string respuesta_relacionados = string.Empty;
        private string respuesta_procesar = string.Empty;
        private List<FoliosCancelar> folios;
        private List<FoliosRespuestas> folios_respuestas;


        //PROPIEDADES
        private string _rfcTextBox;
        public string rfcTextBox
        {
            get
            {
                return _rfcTextBox;
            }
            set
            {
                if (value != null)
                    _rfcTextBox = value;
            }
        }

        private string _uuidTextBox;
        public string uuidTextBox
        {
            get
            {
                return _uuidTextBox;
            }
            set
            {
                if (value != null)
                    _uuidTextBox = value;
            }
        }

        private string _ResultadoTextBox;
        public string ResultadoTextBox
        {
            get
            {
                return _ResultadoTextBox;
            }
            set
            {
                if (value != null)
                    _ResultadoTextBox = value;
            }
        }

        private bool _TabTimbrarSelected;
        public bool TabTimbrarSelected
        {
            get
            {
                return _TabTimbrarSelected;
            }
            set
            {
                _TabTimbrarSelected = value;
            }
        }

        private bool _TabCancelarSelected;
        public bool TabCancelarSelected
        {
            get
            {
                return _TabCancelarSelected;
            }
            set
            {
                _TabCancelarSelected = value;
            }
        }

        // CONSTRUCTOR
        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();

            // Folios a cancelar
            folios = new List<FoliosCancelar>();
            folios.Add(new FoliosCancelar() { Uuid = "FAE45F76-BF09-42CF-A4E5-1D868A0B242C", Rfc_receptor = "IAD121214B34", Total = "7261.60" });
            folios.Add(new FoliosCancelar() { Uuid = "4B49ECF8-61D7-438B-A2AE-8889BB6468DB", Rfc_receptor = "IAD121214B34", Total = "7261.60" });
            
            // Folios a procesar respuesta
            // A(Aceptar la solicitud), R(Rechazar la solicitud)
            folios_respuestas = new List<FoliosRespuestas>();
            folios_respuestas.Add(new FoliosRespuestas() { Uuid = "5462C83D-25EA-4F36-8C8C-B0AAB8F9C806", Rfc_emisor = "AAA010101AAA", Total = "7261.60", Respuesta = "A" });
            folios_respuestas.Add(new FoliosRespuestas() { Uuid = "2FFF3267-E4DC-438F-87D7-38A68D0C04C4", Rfc_emisor = "AAA010101AAA", Total = "7261.60", Respuesta = "R" });

        }

        // EVENTOS
        private void button_timbrar_Click(object sender, RoutedEventArgs e)
        {
            var acceso_servicio = new Servicios();
            
            string path_xml = path + fileStream;
            xmlBase64 = acceso_servicio.generar_sello(path_xml, path);

            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(xmlBase64))
            {
                respuesta_servicio = acceso_servicio.timbrar_cfdi(userName, password, xmlBase64);
                TextBox_resultado.Text = respuesta_servicio.ToString();
            }
            else
                MessageBox.Show("Intente de nuevo.");

            //Limpiar respuesta
            respuesta_servicio = string.Empty;
        }

        private void button_cancelar_Click(object sender, RoutedEventArgs e)
        {
            string path = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            string rfc_emisor = "AAA010101AAA";

            string file_cer_pem = File.ReadAllText(@path + "//Archivos//CSD01_AAA010101AAA.cer.pem");
            string file_key_pem = File.ReadAllText(@path + "//Archivos//CSD01_AAA010101AAA.key.pem");
            getDataFolios();

            var acceso_servicio = new Servicios();
            respuesta_cancelar = acceso_servicio.cancelar_cfdi(userName, password, rfc_emisor, folios, file_cer_pem, file_key_pem);
            MessageBox.Show(respuesta_cancelar, "Cancelar CFDI");
        }


        private void button_get_uuids_Click(object sender, RoutedEventArgs e)
        {
            getDataFolios();
        }

        private void getDataFoliosRespuestas()
        {

            dataProcesarGrid1.Items.Clear();
            foreach (var i in folios_respuestas)
            {
                var data = new { uuid_procesar_grid = i.Uuid, rfcemisor_procesar_grid = i.Rfc_emisor, total_procesar_grid = i.Total, respuesta_procesar_grid = i.Respuesta };
                dataProcesarGrid1.Items.Add(data);
            }
        }
        private void getDataFolios()
        {
            dataGrid1.Items.Clear();
            foreach (var i in folios)
            {
                var data = new { uuid_grid = i.Uuid, rfcreceptor_grid = i.Rfc_receptor, total_grid = i.Total };
                dataGrid1.Items.Add(data);
            }
        }

        private void button_consultar_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBox_uuid.Text) && !string.IsNullOrEmpty(TextBox_rfc_emisor.Text) && !string.IsNullOrEmpty(TextBox_rfc_receptor.Text) && !string.IsNullOrEmpty(TextBox_total.Text))
            {
                var acceso_servicio = new Servicios();
                respuesta_consultar = acceso_servicio.consultar_estatus(userName, password, TextBox_uuid.Text, TextBox_rfc_emisor.Text, TextBox_rfc_receptor.Text, TextBox_total.Text);
                MessageBox.Show(respuesta_consultar, "Consultar Estatus de CFDI");
            }
            else
            {
                MessageBox.Show("Por favor capture el UUID, RFC (Emisor o Receptor) y Total correctamente", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            
        }

        private void button_consultar_peticiones_Click(object sender, RoutedEventArgs e)
        {

            string path = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));

            string rfc_receptor = "AAA010101AAA";
            string file_cer_pem = File.ReadAllText(@path + "//Archivos//CSD01_AAA010101AAA.cer.pem");
            string file_key_pem = File.ReadAllText(@path + "//Archivos//CSD01_AAA010101AAA.key.pem");

            var acceso_servicio = new Servicios();
            respuesta_pendientes = acceso_servicio.consultar_penticiones_pendientes(userName, password, rfc_receptor, file_cer_pem, file_key_pem);

            MessageBox.Show(respuesta_pendientes, "Peticiones Pendientes CFDI");
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(respuesta_pendientes);
            XmlNodeList nodeList = xmldoc.GetElementsByTagName("UUID");
            dataGrid2.Items.Clear();
            foreach (XmlNode node in nodeList)
            {
                Console.WriteLine(node.InnerText);
                var data = new { uuid_pendientes_grid = node.InnerText };
                dataGrid2.Items.Add(data);
            }        
        }

        private void button_procesar_Click(object sender, RoutedEventArgs e)
        {
            string path = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            string rfc_receptor = "AAA010101AAA";

            string file_cer_pem = File.ReadAllText(@path + "//Archivos//CSD01_AAA010101AAA.cer.pem");
            string file_key_pem = File.ReadAllText(@path + "//Archivos//CSD01_AAA010101AAA.key.pem");
            getDataFoliosRespuestas();

            var acceso_servicio = new Servicios();
            respuesta_procesar = acceso_servicio.procesar_respuesta(userName, password, rfc_receptor, folios_respuestas, file_cer_pem, file_key_pem);
            MessageBox.Show(respuesta_procesar, "Procesar CFDI");
        }

        private void button_get_uuids_procesar_Click(object sender, RoutedEventArgs e)
        {
            getDataFoliosRespuestas();
        }


        private void button_consultar_relacionados_Click(object sender, RoutedEventArgs e)
        {
            string path = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));

            string rfc_receptor = "AAA010101AAA";
            string file_cer_pem = File.ReadAllText(@path + "//Archivos//CSD01_AAA010101AAA.cer.pem");
            string file_key_pem = File.ReadAllText(@path + "//Archivos//CSD01_AAA010101AAA.key.pem");

            var acceso_servicio = new Servicios();

            if (!string.IsNullOrEmpty(TextBox_uuid_Relacionados.Text))
            {
                respuesta_relacionados = acceso_servicio.consultar_relacionados(userName, password, TextBox_uuid_Relacionados.Text, rfc_receptor, file_cer_pem, file_key_pem);
                MessageBox.Show(respuesta_relacionados, "Consultar Documentos Relacionados");
            }
            else
            {
                MessageBox.Show("Por favor capture el UUID", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            
        }

        private void button_timbrar_limpiar_Click(object sender, RoutedEventArgs e)
        {
            TextBox_resultado.Text = String.Empty;
        }

        private void TextBox_resultado_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}
