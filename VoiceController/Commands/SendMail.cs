using System.Collections.Generic;

namespace VoiceController.Commands
{
	class SendMail : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "Send mail", "Send e-mail", "G-Mail", "Write mail", "Write e-mail" }; }
		}

		public void Execute(object o = null)
		{
            ProcessUtils.Start("https://mail.google.com/mail/#inbox?compose=new");
		}
	}
}
