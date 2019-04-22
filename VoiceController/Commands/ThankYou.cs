using System.Collections.Generic;

namespace VoiceController.Commands
{
	class ThankYou : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "Thank you", "Thank you very much", "Thanks" }; }
		}

		public void Execute(object o = null)
		{
            Program.Reader.ReadAsync("With pleasure.", "With the greatest pleasure.", "No trouble at all.", "My pleasure!", "The pleasure is mine!", "Not at all!", "You bet!", "You betcha!");
		}
	}
}