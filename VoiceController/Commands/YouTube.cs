using System.Collections.Generic;

namespace VoiceController.Commands
{
	class YouTube : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "YouTube" }; }
		}

		public void Execute(object o = null)
		{
            ProcessUtils.Start("http://www.youtube.com/?hl=hu&gl=HU");
		}
	}
}