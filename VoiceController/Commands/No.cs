using System.Collections.Generic;
using System.Windows.Forms;

namespace VoiceController.Commands
{
	class No : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "No", "Press tabulator and enter" }; }
		}

		public void Execute(object o = null)
		{
			SendKeys.SendWait("{TAB}{ENTER}");
		}
	}
}