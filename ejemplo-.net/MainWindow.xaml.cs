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
  
        private string pfxBase64 = string.Empty;
        private string pfxPassword = "12345678a";
        private string fileStream = "\\Archivos\\archivoXml.xml";
        string path = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));

        private string xmlBase64 = string.Empty;
        private string respuesta_servicio = string.Empty;
        private string respuesta_cancelar = string.Empty;

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

        //CONSTRUCTOR
        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
        }

        //EVENTOS
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
            //leer y pasar a base64 el archivo pfx
            string path = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));

            //Ejecutar comandos para generar archivo pfx
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = "cmd.exe";
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.UseShellExecute = false;
            proc.Start();
            proc.StandardInput.WriteLine("openssl pkcs12 -export -out ../../Archivos/CSD01_AAA010101AAA.pfx -in ../../Archivos/CSD01_AAA010101AAA.cer.pem -inkey ../../Archivos/CSD01_AAA010101AAA.key.pem -password pass:12345678a");
            proc.StandardInput.Flush();
            proc.StandardInput.Close();
            proc.WaitForExit();
	    Console.WriteLine(proc.StandardOutput.ReadToEnd());
            proc.Close();

            byte[] fileBytes = File.ReadAllBytes(@path + "//Archivos//CSD01_AAA010101AAA.pfx");
            string base642 = Convert.ToBase64String(fileBytes);
            pfxBase64 = base642;

            var acceso_servicio = new Servicios();

            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(rfcTextBox) && !string.IsNullOrEmpty(uuidTextBox) && !string.IsNullOrEmpty(pfxBase64) && !string.IsNullOrEmpty(pfxPassword))
            {
                respuesta_cancelar = acceso_servicio.cancelar_cfdi(userName, password, rfcTextBox, uuidTextBox, pfxBase64, pfxPassword);
                MessageBox.Show(respuesta_cancelar, "Cancelar CFDI");
            }
            else
                MessageBox.Show("Por favor capture el RFC del emisor & UUID del CFDI", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
