namespace VoiceController.Messages
{
	public class SmtpServer
	{
        public string Host { get; private set; }

        public int Port { get; private set; }

        public bool Ssl { get; private set; }

        public bool RequiresAuthentication { get; private set; }

        public SmtpAuthentication SmtpAuthentication { get; private set; }

        public string Username { get; private set; }

        public string Password { get; private set; }

        public SmtpServer(string host, int port = 25, bool ssl = false, string username = null, string password = null,
            SmtpAuthentication smtpAuthentication = SmtpAuthentication.Digest)
		{
			Host = host;
			Port = port;
			Ssl = ssl;
            Username = username;
            Password = password;

            RequiresAuthentication = (username != null) || (password != null);
            SmtpAuthentication = smtpAuthentication;
		}
    }
}
