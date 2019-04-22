using System.Collections.Generic;
using System.Windows.Forms;

namespace VoiceController.Commands
{
	class Up : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { /*"Up",*/ "Up key", "Up arrow" }; }
		}

		public void Execute(object o = null)
		{
			SendKeys.SendWait("{UP}");
		}
	}
}