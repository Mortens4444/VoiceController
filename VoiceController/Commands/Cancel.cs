using System.Collections.Generic;
using System.Windows.Forms;

namespace VoiceController.Commands
{
	class Cancel : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "Cancel", "Press escape", "Press escape key", "Send key escape", "Press ESC" }; }
		}

		public void Execute(object o = null)
		{
			SendKeys.SendWait("{ESC}");
		}
	}
}