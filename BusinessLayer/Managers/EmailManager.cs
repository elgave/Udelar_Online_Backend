using Amazon;
using Amazon.Runtime;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Managers
{
    class EmailManager
    {
        public static async void SendMail(string cuerpo, string destinatario, IConfiguration configuration)
        {
            var emailClient = new AmazonSimpleEmailServiceClient(
            new BasicAWSCredentials(configuration["SES_KEY"], configuration["SES_SECRET"]), RegionEndpoint.USEast1);

            var sendRequest = new SendEmailRequest
            {
                Source = "UdelarOnline <noreply.udelaronline.notif@gmail.com>",
                Destination = new Destination { ToAddresses = new List<string> { destinatario } },
                Message = new Message
                {
                    Subject = new Content("Notificación de UdelarOnline"),
                    Body = new Body
                    {
                        Html = new Content
                        {
                            Charset = "UTF-8",
                            Data = "<p>"+cuerpo.Replace("\n","<br>")+"</p>"
                        },
                        Text = new Content
                        {
                            Charset = "UTF-8",
                            Data = cuerpo
                        }
                    }
                }
            };

            await emailClient.SendEmailAsync(sendRequest);
        }
    }
}
