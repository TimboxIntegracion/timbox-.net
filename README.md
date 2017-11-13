# .NET
Ejemplo con la integración al Webservice de Timbox.

Se deberá agregar la referencia del WSDL al proyecto, para hacer uso de los métodos expuestos en el Webservice:

- [Timbox Pruebas](https://staging.ws.timbox.com.mx/timbrado_cfdi33/wsdl)

- [Timbox Producción](https://sistema.timbox.com.mx/timbrado_cfdi33/wsdl)


Para integrar el Webservice al proyecto se requiere hacer uso librerías como `XML`, `Base64`:

```
using System.Xml;
```
## Timbrar CFDI
### Generacion de Sello
Para generar el sello se necesita: la llave privada (.key) en formato PEM. También es necesario incluir el XSLT del SAT para poder transformar el XML y obtener la cadena original.

La cadena original se utiliza para obtener el digest, usando comandos de OpenSSL, luego se utiliza el digest y la llave privada para obtener el sello.

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
Para el método de cancelación se necesita construir un arreglo de UUIDs a cancelar, el RFC del Emisor y construir el archivo PFX que son la unión del certificado y la llave, este debe convertirse en base64 antes de enviarlo al Webservice:

```
string path = Directory.GetCurrentDirectory();
byte[] fileBytes = File.ReadAllBytes(@path + "\\archivo.pfx");
string base642 = Convert.ToBase64String(fileBytes);
```
El arreglo de UUIDs se construye de la siguiente forma:

```
string[] arr_uuids = new string[1] { uuid };
TimboxWS.uuid uuids = new TimboxWS.uuid();
uuids.uuid1 = arr_uuids;
```

El llamado al método de cancelación se hace haciendo el llamado al método cancelar_cfdi:

```
TimboxWS.timbrado_port cliente_cancelar = new TimboxWS.timbrado_portClient();
TimboxWS.cancelar_cfdi_result response = new TimboxWS.cancelar_cfdi_result();
response = cliente_cancelar.cancelar_cfdi(user_name, password, rfc, uuids, pfx_base64, pfx_password);
```


