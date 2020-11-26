# Cómo ejecutar, usando IIS Express desde Visual Studio

### En el archivo `appsettings.Development.json`, incluya el siguiente JSON:
```yaml
"ConnectionStrings": {
    "TSIDB": "CONNECTION STRING"
}
```
* Reemplace `CONNECTION STRING` con la cadena de conexión de su base de datos SQL Server local.
### Acceda a los secretos de usuario del proyecto `UdelarOnlineApi`, y agregue el siguiente JSON:
```yaml
{
    "ApiAuth": {
      "Audience": "AUDIENCE",
      "Issuer": "ISSUER",
      "SecretKey": "SECRETO"
    },
    "S3Keys": {
      "S3Access": "S3 ACCESS KEY",
      "S3Secret": "S3 SECRET KEY",
      "S3Bucket": "BUCKET NAME"
    },
    "USE_RDS": "false",
    "SES_KEY": "SES ACCESS KEY",
    "SES_SECRET": "SES SECRET KEY",
    "SES_EMAIL" :  "EMAIL"
}
 ```

### Cree un bucket en Amazon Web Services
* En su consola de administración de AWS, acceda al servicio Simple Storage Service (S3) 
* Cree un nuevo bucket en la region `us-east-1` (Norte de Virginia)

### Cree dos roles `IAM` en su cuenta de Amazon:
* Uno con la política `AmazonS3FullAccess`
* Uno con la política `AmazonSESFullAccess`

### Reemplazar en el JSON: 
* `AUDIENCE`, `ISSUER` y `SECRETO` con sus credenciales de JWT
* `S3 ACCESS KEY` y `S3 SECRET KEY` con su clave de accesso y secreto de IAM para S3
* `BUCKET NAME` con el nombre de su bucket creado previamente
* `SES ACCESS KEY` y `SES SECRET KEY` con su clave de accesso y secreto de IAM para SES
* `EMAIL` con su dirección de correo electrónico desde la cual se enviarán emails.
> El servicio Simple Email Service (SES) de AWS sólo enviará emails desde y hacia direcciones de correo electrónico verificadas, gracias a la política anti-spam de AWS que se aplica a usuarios que usan SES por primera vez. Por esta razón, todas las direcciones que usted pretenda usar para probar el sistema deberán ser verificadas en su cuenta de AWS siguiendo el [siguiente proceso](https://docs.aws.amazon.com/ses/latest/DeveloperGuide/verify-email-addresses.html).
### Construya la base de datos
Ejecute el comando `update-database` en la consola del administrador de paquetes, sobre la librería de clases `DataAccessLayer`. Para esto debe haber ingresado correctamente su cadena de conexión en `appsettings.Development.json`.

### Ejecute la solución.
