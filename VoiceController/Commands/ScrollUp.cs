using System.Collections.Generic;
using System.Windows.Forms;

namespace VoiceController.Commands
{
	class ScrollUp : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "Page Up", "Scroll Up", "Press Page Up" }; }
		}

		public void Execute(object o = null)
		{
			SendKeys.SendWait("{PGUP}");
		}
	}
}