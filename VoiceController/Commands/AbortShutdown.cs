using System.Collections.Generic;

namespace VoiceController.Commands
{
	class AbortShutdown : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "Abort", "Abort shutdown", "Abort restart" }; }
		}

		public void Execute(object o = null)
		{
            ProcessUtils.Start("shutdown", "/a");
		}
	}
}