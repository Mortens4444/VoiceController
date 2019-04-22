using System.Collections.Generic;
using System.Windows.Forms;
using VoiceController.Commands;

namespace VoiceController.DictateCommands
{
	class Period : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "Period" }; }
		}

		public void Execute(object o = null)
		{
			SendKeys.SendWait("{BACKSPACE}.");
		}
	}
}