using System.Collections.Generic;

namespace VoiceController.Commands
{
	class Calculator : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "Calculator" }; }
		}

		public void Execute(object o = null)
		{
            ProcessUtils.Start("calc");
		}
	}
}