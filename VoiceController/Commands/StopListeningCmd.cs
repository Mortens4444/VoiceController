using System.Collections.Generic;
namespace VoiceController.Commands
{
	class StopListeningCmd : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "Do not listen", "Don't listen", "Stop listening", "Stop executing commands", "Please stop", "Oh my God!", "Jesus Christ!" }; }
		}

		public void Execute(object o = null)
		{
            Program.Reader.ReadAsync("Listening stopped.");
		}
	}
}