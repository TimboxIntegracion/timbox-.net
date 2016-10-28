using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    [Serializable()]
    public class Params
    {
        public string username { get; set; }
        public string password { get; set; }
        public string xmlbase64 { get; set; }
        public string rfc { get; set; }
        public string uuid { get; set; }
        public string pfxbase64 { get; set; }
        public string pfxpassword { get; set; }

        public Params()
        {
            username = "user_name";
            password = "password";
            xmlbase64 = "xml sellado en base64";
            rfc = "XAXX010101010";
            uuid = "123D441C-ABCD-1234-ABCD-E112FAAF5ADF";
            pfxbase64 = "archivo del pfx en base64";
            pfxpassword = "password del archivo pfx";
        }
    }
}
