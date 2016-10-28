# .NET
Ejemplo con la integración al Webservice de Timbox

Para integrar el Webservice al proyecto se requiere hacer uso librerias como XML, Base64:

```
using System.Xml;
```

El consumo del metodo de timbrado se hace solamente creando el objeto de la clase del Webservice y llamar al metodo:

```
//Crear el objeto cliente
TimboxWS.timbrado_portClient cliente_timbrar = new TimboxWS.timbrado_portClient();

//Crear el objeto de la respuesta
TimboxWS.timbrar_cfdi_result response = new TimboxWS.timbrar_cfdi_result();

//llamar el metodo de timbrado enviandole los 
//parametros con las credenciales y el xml en formato base64
response = cliente_timbrar.timbrar_cfdi(user_name, password, xml_base64);
```

Para el metodo de cancelación se necesita construir un arreglo de UUIDs a cancelar, el RFC del Emisor y construir el archivo PFX que son la union del certificado y la llave, este debe convertirse en base64 antes de enviarlo al Webservice:

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

El llamado al metodo de cancelación se hace haciendo el llamado al metodo cancelar_cfdi:

```
TimboxWS.timbrado_port cliente_cancelar = new TimboxWS.timbrado_portClient();
TimboxWS.cancelar_cfdi_result response = new TimboxWS.cancelar_cfdi_result();
response = cliente_cancelar.cancelar_cfdi(user_name, password, rfc, uuids, pfx_base64, pfx_password);
```


