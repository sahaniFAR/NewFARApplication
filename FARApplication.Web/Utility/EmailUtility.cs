
using FARApplication.Web.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FARApplication.Web.Utility
{
    public static class EmailUtility
    {

        static string Baseurl = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetValue<string>("ApiAddress");

        

      

        public static async Task<bool> SendMail()
        {

            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress("test", "farapptest01@gmail.com"));
            message.To.Add(new MailboxAddress("bhakat", "tilakbhakat@gmail.com"));
            message.Subject = "Test mail from far app";

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = "<b>This is some html text</b>";
            bodyBuilder.TextBody = "This is some plain text";
            message.Body = bodyBuilder.ToMessageBody();

            using(var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("farapptest01@gmail.com", "dpiobyzylyxcrudo");
                client.Send(message);
                client.Disconnect(true);
            }

            return true;
        }





    }
}
