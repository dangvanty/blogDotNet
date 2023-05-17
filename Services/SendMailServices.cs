using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;

namespace blogDotNet.Services

{
	public class MailServiceSettings
	{
		public string Mail {set;get;}
		public string DisplayName{set;get;}
		public string Password {set;get;}
		public string Host{set;get;}
		public int Port{set;get;}
	}
	public class SendMailServices : IEmailSender
	{

		private readonly MailServiceSettings mailSettings;
		private ILogger<SendMailServices> logger;
		public SendMailServices(IOptions<MailServiceSettings> _mailSettings, ILogger<SendMailServices> _logger)
		{
			mailSettings = _mailSettings.Value;
			logger = _logger;
			logger.LogInformation("SendMailService is created!!!");
		}
		public async Task SendEmailAsync(string email, string subject, string htmlMessage)
		{
			var message = new MimeMessage();
			// email.Sender= new MailboxAddress("ten hien thi", " dia chỉ email");
			message.Sender = new MailboxAddress(mailSettings.DisplayName, mailSettings.Mail);
			message.From.Add(new MailboxAddress(mailSettings.DisplayName, mailSettings.Mail));
			message.ReplyTo.Add(new MailboxAddress(mailSettings.DisplayName, mailSettings.Mail));

			message.To.Add(MailboxAddress.Parse(email));
			message.Subject = subject;

			var builder = new BodyBuilder();
			builder.HtmlBody = htmlMessage;

			// có thể gửi đi với file đính kèm, text body
			// builder.Attachments
			// builder.TextBody
			message.Body = builder.ToMessageBody();

			using var smtp = new MailKit.Net.Smtp.SmtpClient();

			try
			{
				await smtp.ConnectAsync(mailSettings.Host, mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
				await smtp.AuthenticateAsync(mailSettings.Mail,mailSettings.Password);
				await smtp.SendAsync(message);
				
			}
			catch (System.Exception e)
			{
				System.IO.Directory.CreateDirectory("mailLogs");
				var emailsavefile = string.Format(@"mailLogs/{0}.eml", Guid.NewGuid());
				await message.WriteToAsync(emailsavefile);

				logger.LogInformation("Lỗi gửi mail, lưu tại - "+ emailsavefile);
				logger.LogError(e.Message);
				Console.WriteLine(e.Message);
				
			}
			smtp.Disconnect(true);	
			logger.LogInformation("send mail to " + email + "--- thoi gian" + DateTime.Now.ToShortTimeString());
			
		}
	}
}