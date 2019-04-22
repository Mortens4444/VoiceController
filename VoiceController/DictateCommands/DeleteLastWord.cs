using System.Collections.Generic;
using System.Windows.Forms;
using VoiceController.Commands;

namespace VoiceController.DictateCommands
{
	class DeleteLastWord : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "Delete Last Word" }; }
		}

		public void Execute(object o = null)
		{
			if (!(o is int)) return;
            var lastWordLength = (int)o;
            for (var i = 0; i < lastWordLength; i++)
            {
                SendKeys.SendWait("{BACKSPACE}");
            }
		}
	}
}