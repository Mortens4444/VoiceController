using System.Collections.Generic;

namespace VoiceController.Commands
{
	class RestartComputer : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "Restart computer", "Reboot computer" }; }
		}

		public void Execute(object o = null)
		{
            ProcessUtils.Start("shutdown", "/r /t 15 /c \"VoiceController will restart this computer in 15 seconds\"");
		}
	}
}