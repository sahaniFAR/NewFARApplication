
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
        static string messagefrom = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetValue<string>("MessageFrom");






        public static async Task<bool> SendMail(string mailTo, string mailSubject, string mailBody,string mailattachmentpath)
        {
            try
            {
                var Configdata = ConfigurationProfileUtility.GetConfigurationProfileData().Result;
                MimeMessage message = new MimeMessage();
                //message.From.Add(new MailboxAddress("test", "farapptest01@gmail.com"));// app setting
                message.From.Add(new MailboxAddress(Configdata.EmailPrincipalName, messagefrom));
                //message.To.Add(new MailboxAddress("bhakat", "tilakbhakat@gmail.com"));// parameter
                // message.To.Add(new MailboxAddress("", mailTo));
                if (mailTo.IndexOf(',') > 0)
                {
                    string[] arrMailIds = mailTo.Split(',');
                    foreach (var mailId in arrMailIds)
                    {
                        message.To.Add(new MailboxAddress("", mailId));
                    }
                }
                else
                {
                    message.To.Add(new MailboxAddress("", mailTo));
                }

                //message.Subject = "Test mail from far app";
                message.Subject = mailSubject;
                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = mailBody + "</br><div>This is an auto-notification from " + Configdata.EmailPrincipalName + ". Please do not reply to this message.</div></br><div>Thank You,</div><div>" + Configdata.EmailPrincipalName + "</div>";
                bodyBuilder.Attachments.Add(mailattachmentpath);
                bodyBuilder.TextBody = "This is some plain text";
                message.Body = bodyBuilder.ToMessageBody();
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("farapptest01@gmail.com", "dpiobyzylyxcrudo");
                    client.Send(message);
                    client.Disconnect(true);
                }

                return true;
            }
            catch(Exception ex)
            {
                var strError = ex.Message;
               
                throw;
            }
        }
        public static async Task<bool> SendMail(string mailTo, string mailSubject, string mailBody)
        {
            var Configdata = ConfigurationProfileUtility.GetConfigurationProfileData().Result;
            MimeMessage message = new MimeMessage();
            //message.From.Add(new MailboxAddress("test", "farapptest01@gmail.com"));// app setting
            message.From.Add(new MailboxAddress(Configdata.EmailPrincipalName, messagefrom));
            //message.To.Add(new MailboxAddress("bhakat", "tilakbhakat@gmail.com"));// parameter
            if (mailTo.IndexOf(',') > 0)
            {
                string[] arrMailIds = mailTo.Split(',');
                foreach(var mailId in arrMailIds)
                {
                    message.To.Add(new MailboxAddress("", mailId));
                }
            }
            else
            {
                message.To.Add(new MailboxAddress("", mailTo));
            }

            //message.To.AddRange(new InternetAddress())
            //message.Subject = "Test mail from far app";
            message.Subject = mailSubject;
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = mailBody + "</br><div>This is an auto-notification from " + Configdata.EmailPrincipalName + ". Please do not reply to this message.</div></br><div>Thank You,</div><div>" + Configdata.EmailPrincipalName + "</div>";
            bodyBuilder.TextBody = "This is some plain text";
            message.Body = bodyBuilder.ToMessageBody();
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("farapptest01@gmail.com", "dpiobyzylyxcrudo");
                client.Send(message);
                client.Disconnect(true);
            }

           return  true;
        }




    }
}
