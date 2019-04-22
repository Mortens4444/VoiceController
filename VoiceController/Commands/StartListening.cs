using System.Collections.Generic;

namespace VoiceController.Commands
{
	class StartListening : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "Listen to me", "Hey Conor" }; }
		}

		public void Execute(object o = null)
		{
            Program.Reader.Read("What can I do for you?", "What is your wish?", "What should I do?", "I'm waiting for your commands.");
        }
    }
}