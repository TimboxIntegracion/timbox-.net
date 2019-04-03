using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
   public class Comprobante
    {
        private string _sxml;
        private string _external_id;
        public string Xml
        {
            get
            {
                return _sxml;
            }
            set
            {
                _sxml = value;
            }
        }
        public String ExternalId
        {
            get
            {
                return _external_id;
            }
            set
            {
                _external_id = value;
            }
        }
    }
}
