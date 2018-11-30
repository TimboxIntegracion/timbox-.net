# .NET
Ejemplo con la integración al Webservice de Timbox.

Se deberá agregar la referencia del WSDL al proyecto, para hacer uso de los métodos expuestos en el Webservice:

Webservice de Timbrado:
- [Timbox Pruebas](https://staging.ws.timbox.com.mx/timbrado_cfdi33/wsdl)

- [Timbox Producción](https://sistema.timbox.com.mx/timbrado_cfdi33/wsdl)

Webservice de Cancelación:

- [Timbox Pruebas](https://staging.ws.timbox.com.mx/cancelacion/wsdl)

- [Timbox Producción](https://sistema.timbox.com.mx/cancelacion/wsdl)

Para integrar el Webservice al proyecto se requiere hacer uso librerías como `XML`, `Base64`:

```
using System.Xml;
```
## Timbrar CFDI
### Generacion de Sello
Para generar el sello se necesita: El archivo *.pfx y el XSLT del SAT (cadenaoriginal_3_3.xslt).El XSLT del SAT se utiliza para poder transformar el XML y obtener la cadena original.

La cadena original se utiliza para obtener el sello, utilizando la libreria de encriptación de .NET (System.Security.Cryptography) y se codifica en base64. 

Una vez generado el sello, se actualiza en el XML para que este sea codificado y enviado al servicio de timbrado.
Esto se logra mandando llamar el método de generar_sello en la clase de Servicios:
```
var acceso_servicio = new Servicios();
...
xmlBase64 = acceso_servicio.generar_sello(path_xml, path);
```
### Timbrado
Para hacer una petición de timbrado es necesario enviar las credenciales asignadas y enviar el xml del CFDI a timbrar convertido en una cadena en base64:

```
//Crear el objeto cliente
TimboxWS.timbrado_portClient cliente_timbrar = new TimboxWS.timbrado_portClient();

//Crear el objeto de la respuesta
TimboxWS.timbrar_cfdi_result response = new TimboxWS.timbrar_cfdi_result();

//llamar el método de timbrado enviándole los 
//parámetros con las credenciales y el xml en formato base64
response = cliente_timbrar.timbrar_cfdi(user_name, password, xml_base64);
```
## Cancelar CFDI
Para la cancelación son necesarios el certificado y llave, en formato pem que corresponde al emisor del comprobante:
```
string file_cer_pem = File.ReadAllText(@path + "//Archivos//CSD01_AAA010101AAA.cer.pem");
string file_key_pem = File.ReadAllText(@path + "//Archivos//CSD01_AAA010101AAA.key.pem");
```
Lista de folios a cancelar se construye de la siguiente forma:
```
folios = new List<FoliosCancelar>();
folios.Add(new FoliosCancelar() { Uuid = "F0B60888-BC93-4851-A345-03C238572A8D", Rfc_receptor = "IAD121214B34", Total = "7261.60" });
folios.Add(new FoliosCancelar() { Uuid = "9DDC4AB6-F1A0-4D03-B65B-39776883BA2C", Rfc_receptor = "IAD121214B34", Total = "7261.60" });
```
Crear un cliente y enviarle los parametros correspondientes a la función cancelar_cfdi para hacer la petición de cancelación al webservice:
```
try
{
	TimboxWSCancelacion.folios folios_datos = new TimboxWSCancelacion.folios();
	var lista_folios = new List<TimboxWSCancelacion.folio>();
            
	foreach (var i in folios_cancelar)
	{
		lista_folios.Add(new TimboxWSCancelacion.folio { uuid = i.Uuid, rfc_receptor = i.Rfc_receptor, total = i.Total });
	}

	var folio_array = lista_folios.ToArray();
	folios_datos.folio = folio_array;

	TimboxWSCancelacion.cancelacion_portClient cliente_cancelar = new TimboxWSCancelacion.cancelacion_portClient();
	TimboxWSCancelacion.cancelar_cfdi_result response = new TimboxWSCancelacion.cancelar_cfdi_result();

	response = cliente_cancelar.cancelar_cfdi(user_name, password, rfc_emisor, folios_datos, cer_file, key_file);
       
	XmlDocument acuse_cancelacion = new XmlDocument();
	acuse_cancelacion.LoadXml(response.folios_cancelacion);

	Console.WriteLine(response.folios_cancelacion.ToString());
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
    TimboxWSCancelacion.cancelacion_portClient cliente_cosultar = new TimboxWSCancelacion.cancelacion_portClient();
    TimboxWSCancelacion.consultar_estatus_result response = new TimboxWSCancelacion.consultar_estatus_result();
    
    response = cliente_cosultar.consultar_estatus(user_name, password, uuid, rfc_emisor, rfc_receptor, total);

    Console.WriteLine(response.estatus_cancelacion.ToString());
    return response.estatus_cancelacion.ToString();
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
string file_cer_pem = File.ReadAllText(@path + "//Archivos//CSD01_AAA010101AAA.cer.pem");
string file_key_pem = File.ReadAllText(@path + "//Archivos//CSD01_AAA010101AAA.key.pem");
```
Crear un cliente y enviarle los parametros correspondientes a la función consultar_peticiones_pendientes para hacer la petición de consultar peticiones pendientes al webservice:
```
try
{
    TimboxWSCancelacion.cancelacion_portClient cliente_consultar = new TimboxWSCancelacion.cancelacion_portClient();
    TimboxWSCancelacion.consultar_peticiones_pendientes_result response = new TimboxWSCancelacion.consultar_peticiones_pendientes_result();

    response = cliente_consultar.consultar_peticiones_pendientes(user_name, password, rfc_recptor, cer_file, key_file);
    
    Console.WriteLine(response.codestatus.ToString());
    return response.codestatus.ToString();
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
string file_cer_pem = File.ReadAllText(@path + "//Archivos//CSD01_AAA010101AAA.cer.pem");
string file_key_pem = File.ReadAllText(@path + "//Archivos//CSD01_AAA010101AAA.key.pem");
```
Lista de folios respuestas a procesar se construye de la siguiente forma:
```
// A(Aceptar la solicitud), R(Rechazar la solicitud)
 folios_respuestas = new List<FoliosRespuestas>();
 folios_respuestas.Add(new FoliosRespuestas() { Uuid = "F0B60888-BC93-4851-A345-03C238572A8D", Rfc_emisor = "AAA010101AAA", Total = "7261.60", Respuesta = "A" });
 folios_respuestas.Add(new FoliosRespuestas() { Uuid = "9DDC4AB6-F1A0-4D03-B65B-39776883BA2C", Rfc_emisor = "AAA010101AAA", Total = "7261.60", Respuesta = "R" });

```
Crear un cliente y enviarle los parametros correspondientes a la función procesar_respuesta para hacer la petición de procesar al webservice:
```
try
{
    TimboxWSCancelacion.respuestas respuestas = new TimboxWSCancelacion.respuestas();
    var lista_folios = new List<TimboxWSCancelacion.folios_respuestas>();

    foreach (var i in folios_procesar)
    {
        lista_folios.Add(new TimboxWSCancelacion.folios_respuestas { uuid = i.Uuid, rfc_emisor = i.Rfc_emisor, total = i.Total, respuesta = i.Respuesta});
    }

    var folio_array = lista_folios.ToArray();
    respuestas.folios_respuestas = folio_array;

    TimboxWSCancelacion.cancelacion_portClient cliente_procesar = new TimboxWSCancelacion.cancelacion_portClient();
    TimboxWSCancelacion.procesar_respuesta_result response = new TimboxWSCancelacion.procesar_respuesta_result();

    response = cliente_procesar.procesar_respuesta(user_name, password, rfc_receptor, respuestas, cer_file, key_file);

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
string file_cer_pem = File.ReadAllText(@path + "//Archivos//CSD01_AAA010101AAA.cer.pem");
string file_key_pem = File.ReadAllText(@path + "//Archivos//CSD01_AAA010101AAA.key.pem");
```
Crear un cliente y enviarle los parametros correspondientes a la función procesar_respuesta para hacer la petición de procesar al webservice:
```
try
{
    TimboxWSCancelacion.cancelacion_portClient cliente_consultar_relacinados = new TimboxWSCancelacion.cancelacion_portClient();
    TimboxWSCancelacion.consultar_documento_relacionado_result response = new TimboxWSCancelacion.consultar_documento_relacionado_result();

    response = cliente_consultar_relacinados.consultar_documento_relacionado(user_name, password, uuid,rfc_receptor,cer_file,key_file);

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