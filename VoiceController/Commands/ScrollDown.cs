using System.Collections.Generic;
using System.Windows.Forms;

namespace VoiceController.Commands
{
	class ScrollDown : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "Page Down", "Scroll Down", "Press Page Down" }; }
		}

		public void Execute(object o = null)
		{
			SendKeys.SendWait("{PGDN}");
		}
	}
}