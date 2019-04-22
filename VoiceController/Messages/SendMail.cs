using MessageBoxes;
using SourceInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace VoiceController.Messages
{
	public class SendMail
	{
		public List<object> Arguments { get; set; }

		public bool ForceSmtpAuthentication { get; set; }

		public SmtpAuthentication SmtpAuthentication { get; set; }

		public event SentChangedEventHandler SentChanged;

        private readonly SmtpClient smtpClient;
        private IEnumerable<MailHeader> headers;
		private MailMessage mail;

		/// <summary>
		/// SendMail
		/// </summary>
        /// <param name="smtpServer">The SMTP server</param>
		public SendMail(SmtpServer smtpServer)
		{
            if (String.IsNullOrEmpty(smtpServer.Host))
            {
                throw new ArgumentException("smtpServer.Host");
            }

			smtpClient = new SmtpClient(smtpServer.Host, smtpServer.Port);
			try
			{
                smtpClient.UseDefaultCredentials = !smtpServer.RequiresAuthentication;
				smtpClient.Credentials = smtpServer.RequiresAuthentication ?
                    new NetworkCredential(smtpServer.Username, smtpServer.Password) :
                    CredentialCache.DefaultNetworkCredentials;
			}
			catch
			{
				smtpClient.UseDefaultCredentials = true;
				smtpClient.Credentials = CredentialCache.DefaultNetworkCredentials;
			}
			smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
			smtpClient.EnableSsl = smtpServer.Ssl;
			smtpClient.SendCompleted += SendCompletedCallback;
            ForceSmtpAuthentication = true;
            SmtpAuthentication = smtpServer.SmtpAuthentication;
			Arguments = null;
		}

		public SendMail(string smtpHost, bool sslEncryption, int port)
            : this(new SmtpServer(smtpHost, port, sslEncryption, null, null))
		{ }

		public SendMail(string smtpHost, bool sslEncryption, int port, string username, string password)
            : this(new SmtpServer(smtpHost, port, sslEncryption, username, password))
		{ }

		public void Send(string sender, string recipient, string subject, string body)
		{
            Send(sender, recipient, null, null, Enumerable.Empty<MailHeader>(), subject, body);
		}

        public void Send(string sender, string recipient, IEnumerable<MailHeader> mailHeaders, string subject, string body)
		{
			Send(sender, recipient, null, null, mailHeaders, subject, body);
		}

		public void Send(string sender, string recipient, string carbonCopy, string blindCarbonCopy, IEnumerable<MailHeader> mailHeaders, string subject, string body)
		{
            if (String.IsNullOrEmpty(sender))
            {
                throw new ArgumentException("sender");
            }
            if (String.IsNullOrEmpty(recipient))
            {
                throw new ArgumentException("recipient");
            }

			mail = new MailMessage(sender, recipient);
            if (!String.IsNullOrEmpty(carbonCopy))
            {
                mail.CC.Add(carbonCopy);            
            }
            if (!String.IsNullOrEmpty(blindCarbonCopy))
            {
                mail.Bcc.Add(blindCarbonCopy);            
            }
            mail.SubjectEncoding = Encoding.UTF8;
            mail.BodyEncoding = Encoding.UTF8;
            mail.Subject = subject;
            mail.Body = body;

            headers = mailHeaders;
			if (mailHeaders != null)
			{
                foreach (var mailHeader in mailHeaders)
                {
                    try
                    {
                        if ((mailHeader.Name != String.Empty) && (mailHeader.Value != String.Empty))
                            mail.Headers.Add(Base64.Encode(mailHeader.Name), Base64.Encode(mailHeader.Value));
                    }
                    catch { }                    
                }
			}
			
			try
			{
				// NTLM (NT LAN Manager) Authentication /SMTP Extension/ throws System.FormatException - Invalid length for a Base-64 char array.
				if (ForceSmtpAuthentication)
				{
                    smtpClient.ForceSmtpAuthentication(SmtpAuthentication);
				}				

                smtpClient.SendAsync(mail, null);
			}
			catch (Exception ex)
			{
				OnSentChanged(new SentChangedEventArgs(headers, ex, Arguments));
			}
		}

		public static void EmailSentChanged(SentChangedEventArgs e)
		{
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }
            if (!e.Sent)
            {
                ErrorBox.Show("E-mail sent error", new ExceptionDetails(e.Exception).Details);
            }
            else
            {
                InfoBox.Show("E-mail succesfully sent", "E-mail sent was succesfully to target e-mail address");
            }
		}

		protected virtual void OnSentChanged(SentChangedEventArgs e)
		{
            if (SentChanged != null)
            {
                SentChanged(this, e);
            }
		}

		void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
		{
			OnSentChanged(new SentChangedEventArgs(headers, e.Error));
			mail.Dispose();
		}
	}
}