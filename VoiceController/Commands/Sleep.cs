using System.Collections.Generic;

namespace VoiceController.Commands
{
	class Sleep : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "Sleep" }; }
		}

		public void Execute(object o = null)
		{
            ProcessUtils.Start(@".\Commands\Sleep\sleep.bat");
		}
	}
}