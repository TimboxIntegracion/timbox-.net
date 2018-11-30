using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
   public class FoliosRespuestas
    {
        private string _uuid;
        private string _rfc_emisor;
        private string _total;
        private string _respuesta;

        public string Uuid
        {
            get
            {
                return _uuid;
            }

            set
            {
                _uuid = value;
            }
        }

        public string Rfc_emisor
        {
            get
            {
                return _rfc_emisor;
            }

            set
            {
                _rfc_emisor = value;
            }
        }

        public string Total
        {
            get
            {
                return _total;
            }

            set
            {
                _total = value;
            }
        }

        public string Respuesta {
            get
            {
                return _respuesta;
            }
            set
            {
                _respuesta = value;
            }
        }
    }
}
