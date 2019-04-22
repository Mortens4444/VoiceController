using System.Collections.Generic;
using System.Windows.Forms;

namespace VoiceController.Commands
{
	class Paste : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "Insert from clipboard", "Paste from clipboard", "Press control V" }; }
		}

		public void Execute(object o = null)
		{
			SendKeys.SendWait("^V");
            Program.Reader.ReadAsync("Content pastes from clipboard.");
		}
	}
}