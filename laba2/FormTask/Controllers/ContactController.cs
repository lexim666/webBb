using System;
using System.IO;
using FormTask.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace FormTask.Controllers
{
    public class ContactController : Controller
    {
        private static bool _success = false;
        private static IConfigurationRoot Configuration { get; set; }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Send(MessageModel email)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            try
            {
                ViewBag.ErrorMessage = null;
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");
                Configuration = builder.Build();

                var adminAdress = Configuration["SMTP:Admin"];
                var adress = Configuration["SMTP:Email"];
                var host = Configuration["SMTP:Host"];
                var port = Convert.ToInt32(Configuration["SMTP:Port"]);
                var password = Configuration["SMTP:password"];

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(email.Address, adress));
                message.To.Add(new MailboxAddress(adminAdress));
                message.Subject = email.Topic;
                message.Body = new TextPart("plain")
                {
                    Text = email.Text
                };

                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.Connect(host, port, true);
                    smtpClient.Authenticate(adress, password);
                    smtpClient.Send(message);
                    smtpClient.Disconnect(true);
                    _success = true;
                }
            }
            catch (Exception e)
            {
                _success = false;
                ViewBag.ErrorMessage = e.Message;
            }
            ViewBag.Success = _success;
            return View("Index");
        }
    }
}