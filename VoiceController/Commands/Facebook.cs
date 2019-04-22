using System.Collections.Generic;

namespace VoiceController.Commands
{
	class Facebook : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "Facebook", "Face book" }; }
		}

		public void Execute(object o = null)
		{
            ProcessUtils.Start("https://www.facebook.com/");
		}
	}
}