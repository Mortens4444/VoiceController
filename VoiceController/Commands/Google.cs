using System.Collections.Generic;

namespace VoiceController.Commands
{
	class Google : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { /*"Google",*/ "I want to search", "Open Google", "Google homepage" }; }
		}

		public void Execute(object o = null)
		{
            ProcessUtils.Start("https://www.google.hu/");
		}
	}
}