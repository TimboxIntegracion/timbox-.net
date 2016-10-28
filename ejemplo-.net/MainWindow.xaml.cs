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


namespace Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Params webservice = new Params();

        public MainWindow()
        {
            InitializeComponent();

            string path = Directory.GetCurrentDirectory();
            FileStream fileStream = new FileStream(@path + "\\config.xml", FileMode.Open);
            SoapFormatter formatter = new SoapFormatter();
            webservice = (Params)formatter.Deserialize(fileStream);
        }

        private void button_timbrar_Click(object sender, RoutedEventArgs e)
        {           
            var acceso_servicio = new Servicios();
            if (acceso_servicio.timbrar_cfdi(webservice.username, webservice.password, webservice.xmlbase64) == true)
                MessageBox.Show("Termino el envío a timbrar el CFDI");
            else
                MessageBox.Show("Hubo un error en la petición, revisar la consola para mas información");
        }

        private void button_cancelar_Click(object sender, RoutedEventArgs e)
        {
            //leer y pasar a base64 el archivo pfx
            string path = Directory.GetCurrentDirectory();
            byte[] fileBytes = File.ReadAllBytes(@path + "\\archivo.pfx");
            string base642 = Convert.ToBase64String(fileBytes);
            webservice.pfxbase64 = base642;

            var acceso_servicio = new Servicios();
            if (acceso_servicio.cancelar_cfdi(webservice.username, webservice.password, webservice.rfc, webservice.uuid, webservice.pfxbase64, webservice.pfxpassword) == true)
                MessageBox.Show("Termino el envío a cancelación del CFDI");
            else
                MessageBox.Show("Hubo un error en la petición, revisar la consola para mas información");
        }
    }
}
