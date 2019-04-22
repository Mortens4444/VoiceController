using System.Collections.Generic;
using System.Windows.Forms;

namespace VoiceController.Commands
{
	class Down: ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { /*"Down",*/ "Down key", "Down arrow" }; }
		}

		public void Execute(object o = null)
		{
			SendKeys.SendWait("{DOWN}");
		}
	}
}