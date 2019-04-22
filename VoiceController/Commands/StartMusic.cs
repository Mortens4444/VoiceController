using System.Collections.Generic;

namespace VoiceController.Commands
{
	class StartMusic : ICommand
	{
		public bool StopListening
		{
			get { return true; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "Start music" }; }
		}

		public void Execute(object o = null)
		{
            ProcessUtils.Start("music.xspf");
		}
	}
}