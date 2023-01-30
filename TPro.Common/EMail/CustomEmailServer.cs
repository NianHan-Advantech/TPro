using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;

namespace TPro.Common.EMail
{
    public partial class NEmailServer
    {
        private readonly SmtpClient _smtpClient = new();

        private NEmailServer()
        {
            _smtpClient.Credentials = new NetworkCredential("3062381934@qq.com", "vfdgrqsxjosudhag");
        }

        public NEmailServer(string host, int port) : this()
        {
            _smtpClient.Host = "smtp.qq.com";
            _smtpClient.Port = 25;
        }

        public NEmailServer(IOptions<NEmailConfiguration> options) : this()
        {
            _smtpClient.Host = options.Value.Host;
            _smtpClient.Port = options.Value.Port;
        }

        public void SendEmail(MailMessage message)
        {
            message.From = new MailAddress("3062381934@qq.com");
            message.To.Add("2996874707@qq.com");
            message.Subject = "c#邮件测试";
            message.Body = "测试内容";
            try
            {
                _smtpClient.Send(message);
                _smtpClient.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public NEmailResponse Send(NEmail email)
        {
            if (!email.IsValid(out string msg))
                return NEmailResponse.Fail(msg);
            var emailmessage = new MailMessage
            {
                From = new MailAddress(email.From),
                Subject = email.Subject,
                Body = email.Body
            };
            foreach (var item in email.To)
            {
                emailmessage.To.Add(item);
            }
            foreach (var item in email.CC)
            {
                emailmessage.CC.Add(item);
            }
            foreach (var item in email.BCC)
            {
                emailmessage.Bcc.Add(item);
            }
            SendEmail(emailmessage);
            return NEmailResponse.Success();
        }

        

        
    }
}