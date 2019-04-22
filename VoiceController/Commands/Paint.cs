using System.Collections.Generic;

namespace VoiceController.Commands
{
	class Paint : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "Paint", "Microsoft paint" }; }
		}

		public void Execute(object o = null)
		{
            ProcessUtils.Start("mspaint");
		}
	}
}