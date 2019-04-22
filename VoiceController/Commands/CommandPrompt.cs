using System.Collections.Generic;

namespace VoiceController.Commands
{
	class CommandPrompt : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "Command prompt", "Terminal", "Shell" }; }
		}

		public void Execute(object o = null)
		{
            ProcessUtils.Start("cmd");
		}
	}
}