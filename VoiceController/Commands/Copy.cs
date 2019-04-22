using System.Collections.Generic;
using System.Windows.Forms;

namespace VoiceController.Commands
{
	class Copy : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "Copy to clipboard", "Press control C" }; }
		}

		public void Execute(object o = null)
		{
			SendKeys.SendWait("^C");
			Program.Reader.ReadAsync("Content copied to clipboard.");
		}
	}
}