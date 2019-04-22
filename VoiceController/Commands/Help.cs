using System.Collections.Generic;

namespace VoiceController.Commands
{
	class Help : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "Help", "Help me", "What can I say?", "What should I say?" }; }
		}

		public void Execute(object o = null)
		{
            Program.Reader.ReadAsync("You can start programs like calculator, notepad, paint or vlc.", "You can open web pages like Google, Facebook or NCore.", "You can control volume", "You can start conversation with Skype", "You can dictate to me.", "You can turn off, restart or hibernate me.", "You can close applications.", "I can read you from clipboard.", "I can tell you date or time.");
		}
	}
}