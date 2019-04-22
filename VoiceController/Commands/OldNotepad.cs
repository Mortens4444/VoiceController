using System.Collections.Generic;

namespace VoiceController.Commands
{
	class OldNotepad : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "Old notepad", "Windows notepad" }; }
		}

		public void Execute(object o = null)
		{
            ProcessUtils.Start("notepad");
		}
	}
}