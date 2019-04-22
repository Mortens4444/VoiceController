using System.Collections.Generic;

namespace VoiceController.Commands
{
	class BlowUp : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "Blow up" }; }
		}

		public void Execute(object o = null)
		{
			Program.Reader.ReadAsync("My program not allows to hurt anyone.");
		}
	}
}