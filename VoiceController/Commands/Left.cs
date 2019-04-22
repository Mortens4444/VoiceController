using System.Collections.Generic;
using System.Windows.Forms;

namespace VoiceController.Commands
{
	class Left : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { /*"Left",*/ "Left key", "Left arrow" }; }
		}

		public void Execute(object o = null)
		{
			SendKeys.SendWait("{LEFT}");
		}
	}
}