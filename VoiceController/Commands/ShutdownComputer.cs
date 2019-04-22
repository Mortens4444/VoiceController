using System.Collections.Generic;

namespace VoiceController.Commands
{
	class ShutdownComputer : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "Shutdown computer", "Halt computer" }; }
		}

		public void Execute(object o = null)
		{
            ProcessUtils.Start("shutdown", "/s /t 15 /c \"VoiceController will shutdown this computer in 15 seconds\"");
		}
	}
}