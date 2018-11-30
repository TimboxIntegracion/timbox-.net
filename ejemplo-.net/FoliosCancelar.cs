using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
   public class FoliosCancelar
    {
        private string _uuid;
        private string _rfc_receptor;
        private string _total;

        public string Uuid
        {
            get {
                return _uuid;
            }

            set
            {
                _uuid = value;
            }
        }
        public string Rfc_receptor
        {
            get
            {
                return _rfc_receptor;
            }

            set
            {
                _rfc_receptor = value;
            }
        }
        public string Total {
            get
            {
                return _total;
            }

            set
            {
                _total = value;
            }
        }
    }
}
