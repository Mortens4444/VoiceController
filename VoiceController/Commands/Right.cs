using System.Collections.Generic;
using System.Windows.Forms;

namespace VoiceController.Commands
{
	class Right: ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "Right", "Right key", "Right arrow" }; }
		}

		public void Execute(object o = null)
		{
			SendKeys.SendWait("{RIGHT}");
		}
	}
}