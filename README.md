# .NET
Ejemplo con la integración al Webservice de Timbox.

Se deberá agregar la referencia del WSDL al proyecto, para hacer uso de los métodos expuestos en el Webservice:

Webservice de Timbrado 4.0:
- [Timbox Pruebas](https://staging.ws.timbox.com.mx/timbrado_cfdi40/wsdl)

- [Timbox Producción](https://sistema.timbox.com.mx/timbrado_cfdi40/wsdl)

Webservice de Cancelación:

- [Timbox Pruebas](https://staging.ws.timbox.com.mx/cancelacion/wsdl)

- [Timbox Producción](https://sistema.timbox.com.mx/cancelacion/wsdl)

Para integrar el Webservice al proyecto se requiere hacer uso librerías como `XML`, `Base64`:

```
using System.Xml;
```

## Timbrar CFDI 4.0
### Generacion de Sello
Para generar el sello se necesita: El archivo *.pfx y el XSLT del SAT (cadenaoriginal_4_0.xslt).El XSLT del SAT se utiliza para poder transformar el XML y obtener la cadena original.

La cadena original se utiliza para obtener el sello, utilizando la libreria de encriptación de .NET (System.Security.Cryptography) y se codifica en base64. 

Una vez generado el sello, se actualiza en el XML para que este sea codificado y enviado al servicio de timbrado.
Esto se logra mandando llamar el método de generar_sello en la clase de Servicios:
```
var servicios_timbox = new Servicios();
...
xmlBase64 = servicios_timbox.generar_sello(path_xml, path);
```

### Timbrado
Para hacer una petición de timbrado es necesario enviar las credenciales asignadas y enviar el xml del CFDI a timbrar convertido en una cadena en base64:
```
// Crear el objeto cliente
var timbox_cliente = new Timbrado_40_Pruebas.timbrado_cfdi40_portClient();
// Crear el objeto de la respuesta
Timbrado_40_Pruebas.timbrar_cfdi_result response = new Timbrado_40_Pruebas.timbrar_cfdi_result();
// Llamar el método de timbrado enviandole los
// parámetros con las credenciales y el xml en formato base64
response = timbox_cliente.timbrar_cfdi(user_name, password, xml_base64);
```

## Cancelar CFDI
A partir del 2022 será necesario señalar el motivo de la cancelación de los comprobantes. Al seleccionar como motivo de cancelación la clave 01 “Comprobante emitido con errores con relación deberá relacionarse el folio fiscal del comprobante que sustituye al cancelado. Se actualizan los plazos para realizar la cancelación de facturas.

**<b> Motivos de Cancelación (Código - Descripción) </b><br>**
**<b>  01    -    Comprobante emitido con errores con relación </b><br>**
**<b>  02    -    Comprobante emitido con errores sin relación </b><br>**
**<b>  03    -    No se llevó a cabo la operación </b><br>**
**<b>  04    -    Operación nominativa relacionada en la factura global </b><br>**

Para la cancelación son necesarios el certificado y llave, en formato pem que corresponde al emisor del comprobante:
```
string file_cer_pem = File.ReadAllText(@path + "//Archivos//XIA190128J61.cer.pem");
string file_key_pem = File.ReadAllText(@path + "//Archivos//XIA190128J61.key.pem");
```
Lista de folios a cancelar se construye de la siguiente forma:
```
// Numero de UUID's a cancelar
int no_folios = 1;
// Llenado de los datos para la cancelación
String[][] datos = new String[no_folios][];
// Los datos para la cancelación siguen el siguiente orden: 
// UUID, rfc receptor, total, motivo y folio sustituto
datos[0] = new String[5] { "F0B60888-BC93-4851-A345-03C238572A8D", "XEXX010101000", "0.0", "02", "" };
```
Crear un cliente y enviarle los parametros correspondientes a la función cancelar_cfdi para hacer la petición de cancelación al webservice:
```
try
{
	// Llenado de Folios
    var folios = new Cancelacion_Pruebas.folios();

    //Objeto folio
    var folio = new Cancelacion_Pruebas.folio();

    //Objeto temporal de folio
    var temp_folios = new Cancelacion_Pruebas.folio[no_folios];

    for (int i = 0; i < no_folios; i++)
    {
        folio.uuid = folios_cancelar[i][0];
        folio.rfc_receptor = folios_cancelar[i][1];
        folio.total = folios_cancelar[i][2];
        folio.motivo = folios_cancelar[i][3];
        folio.folio_sustituto = folios_cancelar[i][4];

        temp_folios.SetValue(folio, i);
    }

    //Asignación de los folios a cancelar
    folios.folio = temp_folios;

    //Llamado a los servicios de Timbox
    var timbox_cliente = new Cancelacion_Pruebas.cancelacion_portClient();
    Cancelacion_Pruebas.cancelar_cfdi_result response = new Cancelacion_Pruebas.cancelar_cfdi_result();

    response = timbox_cliente.cancelar_cfdi(user_name, password, emisor, folios, cer_file, key_file);

    XmlDocument acuse_cancelacion = new XmlDocument();
    acuse_cancelacion.LoadXml(response.acuse_cancelacion);

    Console.WriteLine(acuse_cancelacion);

    XmlDocument folios_cancelados = new XmlDocument();
    folios_cancelados.LoadXml(response.folios_cancelacion);

    return response.folios_cancelacion.ToString();
}
catch (System.ServiceModel.FaultException e)
{
	Console.WriteLine("Código de error " + e.Code.Name + ": " + e.Message);
	return "Código de error: " + e.Code.Name + "\n" + e.Message;
}
```

## Consultar Estatus CFDI
Para la consulta de estatus de CFDI solo es necesario crear un cliente y enviarle los parametros correspondientes a la función consultar_estatus para hacer la petición de consulta al webservice:
```
try
{
    //Llamado a los servicios de Timbox
    var timbox_cliente = new Cancelacion_Pruebas.cancelacion_portClient();
    Cancelacion_Pruebas.consultar_estatus_result response = new Cancelacion_Pruebas.consultar_estatus_result();

    // Llamado al servicio de Consultar Estatus
    response = timbox_cliente.consultar_estatus(user_name, password, uuid, rfc_emisor, rfc_receptor, total);

    Console.WriteLine(response.codigo_estatus);
    Console.WriteLine(response.estado);
    Console.WriteLine(response.estatus_cancelacion);
    Console.WriteLine(response.es_cancelable);

    return response.estado + "\n" + response.estado;
}
catch (System.ServiceModel.FaultException e)
{
    Console.WriteLine("Código de error " + e.Code.Name + ": " + e.Message);
    return "Código de error: " + e.Code.Name + "\n" + e.Message;
}
```
## Consultar Peticiones Pendientes
Para la consulta de peticiones pendientes son necesarios el certificado y llave, en formato pem que corresponde al receptor del comprobante:
```
string file_cer_pem = File.ReadAllText(@path + "//Archivos//XIA190128J61.cer.pem");
string file_key_pem = File.ReadAllText(@path + "//Archivos//XIA190128J61.key.pem");
```
Crear un cliente y enviarle los parametros correspondientes a la función consultar_peticiones_pendientes para hacer la petición de consultar peticiones pendientes al webservice:
```
try
{
    //Llamado a los servicios de Timbox
    var timbox_cliente = new Cancelacion_Pruebas.cancelacion_portClient();
    Cancelacion_Pruebas.consultar_peticiones_pendientes_result response = new Cancelacion_Pruebas.consultar_peticiones_pendientes_result();

    // Llamado al método
    response = timbox_cliente.consultar_peticiones_pendientes(username, password, rfc_receptor, cer_file, key_file);

    Console.WriteLine(response.codestatus);
    Console.WriteLine(response.uuids);

    return response.codestatus + "\n" + response.uuids;
}
catch (System.ServiceModel.FaultException e)
{
    Console.WriteLine("Código de error " + e.Code.Name + ": " + e.Message);
    return "Código de error: " + e.Code.Name + "\n" + e.Message;
}
```
## Procesar Respuesta
Para realizar la petición de aceptación/rechazo de la solicitud de cancelación son necesarios el certificado y llave, en formato pem que corresponde al receptor del comprobante:
```
string file_cer_pem = File.ReadAllText(@path + "//Archivos//XIA190128J61.cer.pem");
string file_key_pem = File.ReadAllText(@path + "//Archivos//XIA190128J61.key.pem");
```
Lista de folios respuestas a procesar se construye de la siguiente forma:
```
// A(Aceptar la solicitud), R(Rechazar la solicitud)
/ Numero de UUID's a cancelar
int no_folios = 2;
// Llenado de los datos para la cancelación
String[][] datos = new String[no_folios][];
// Los datos para la cancelación siguen el siguiente orden: 
// Respuesta, rfc receptor, uuid y total
datos[0] = new String[4] { "R", "XIA190128J61","F0B60888-BC93-4851-A345-03C238572A8D",  "7261.60" }
datos[0] = new String[4] { "A", "XIA190128J61", "9DDC4AB6-F1A0-4D03-B65B-39776883BA2C", "7261.60" });

```
Crear un cliente y enviarle los parametros correspondientes a la función procesar_respuesta para hacer la petición de procesar al webservice:
```
try
{
    // Objeto para contener las respuestas
    var respuestas_solicitud = new Cancelacion_Pruebas.respuestas();
    // Objeto temporal para almacenar los folios por respuesta
    var folio_respuesta = new Cancelacion_Pruebas.folios_respuestas();
    // Objeto para el total de las respuestas
    var total_respuestas = new Cancelacion_Pruebas.folios_respuestas[cantidad];

    // Llenado de las respuestas
    for(int i = 0; i < cantidad; i++)
    {
        folio_respuesta.respuesta = folios_procesar[i][0];
        folio_respuesta.rfc_emisor = folios_procesar[i][1];
        folio_respuesta.uuid = folios_procesar[i][2];
        folio_respuesta.total = folios_procesar[i][3];

        // Se añaden al objeto de arreglos
        total_respuestas.SetValue(folio_respuesta, 0);
    }

    // Se añaden el total de las respuestas a la solicitud
    respuestas_solicitud.folios_respuestas = total_respuestas;

    // Llamado a los servicios de Timbox
    var timbox_cliente = new Cancelacion_Pruebas.cancelacion_portClient();
    Cancelacion_Pruebas.procesar_respuesta_result response = new Cancelacion_Pruebas.procesar_respuesta_result();

    // Llamado del metodo
    response = timbox_cliente.procesar_respuesta(user_name, password, rfc_receptor, respuestas_solicitud, cer_file, key_file);

    Console.WriteLine(response.folios.ToString());
    return response.folios.ToString();
}
catch (System.ServiceModel.FaultException e)
{
    Console.WriteLine("Código de error " + e.Code.Name + ": " + e.Message);
    return "Código de error: " + e.Code.Name + "\n" + e.Message;
}
```
## Consultar Documentos Relacionados
Para realizar la petición de consultar documentos relacionado son necesarios el certificado y llave, en formato pem que corresponde al receptor del comprobante:
```
string file_cer_pem = File.ReadAllText(@path + "//Archivos//XIA190128J61.cer.pem");
string file_key_pem = File.ReadAllText(@path + "//Archivos//XIA190128J61.key.pem");
```
Crear un cliente y enviarle los parametros correspondientes a la función procesar_respuesta para hacer la petición de procesar al webservice:
```
try
{
    //Llamado a los servicios de Timbox
    var timbox_cliente = new Cancelacion_Pruebas.cancelacion_portClient();
    Cancelacion_Pruebas.consultar_documento_relacionado_result response = new Cancelacion_Pruebas.consultar_documento_relacionado_result();

    // Llamado del metodo
    response = timbox_cliente.consultar_documento_relacionado(username, password, uuid, rfc_emisor, rfc_receptor, cer_file, key_file);

    string result = response.resultado.ToString() + " " + response.relacionados_padres.ToString() + " " + response.relacionados_hijos.ToString();
    Console.WriteLine(result);
    return result;
}
catch(System.ServiceModel.FaultException e)
{
    Console.WriteLine("Código de error " + e.Code.Name + ": " + e.Message);
    return "Código de error: " + e.Code.Name + "\n" + e.Message;
}
```

## Timbrar Retenciones
Para hacer una petición de timbrado es necesario enviar las credenciales asignadas y enviar el xml del CFDI a timbrar convertido en una cadena en base64:
```
// Llamado a los servicios de timbox
var timbox_cliente = new Timbrado_Retenciones_Pruebas.retencion_portClient();
Timbrado_Retenciones_Pruebas.timbrar_retencion_result response = new Timbrado_Retenciones_Pruebas.timbrar_retencion_result();

// Llamado al metodo de timbrar
response = timbox_cliente.timbrar_retencion(user_name, password, xml_bas64);
```