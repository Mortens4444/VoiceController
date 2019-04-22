using System.Collections.Generic;
using System.Windows.Forms;

namespace VoiceController.Commands
{
	class OK : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "OK", /*"Yes",*/ "Press enter" }; }
		}

		public void Execute(object o = null)
		{
			SendKeys.SendWait("{ENTER}");
		}
	}
}