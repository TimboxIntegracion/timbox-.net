using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Main
{
    [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Struct)]
    public class DataListConverter : System.Attribute
    {
        private object result_pendientes;

        public DataListConverter(object result_peticiones_pendientes)
        {
            this.result_pendientes = result_peticiones_pendientes;

            Console.Write(result_peticiones_pendientes);

           
        }


    }

}
