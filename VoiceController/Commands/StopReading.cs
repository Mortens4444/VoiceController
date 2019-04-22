using System.Collections.Generic;

namespace VoiceController.Commands
{
	class StopReading : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "Stop reading", "Stop speaking", "Stop talking" }; }
		}

		public void Execute(object o = null)
		{
            Program.Reader.StopReading();
		}
	}
}