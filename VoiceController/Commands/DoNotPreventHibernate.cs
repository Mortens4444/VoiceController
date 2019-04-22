using System.Collections.Generic;
using VoiceController.Messages;

namespace VoiceController.Commands
{
	class DoNotPreventHibernate : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "Do not prevent hibernate", "You can sleep", "You can go to sleep", "You can hibernate" }; }
		}

		public void Execute(object o = null)
		{
			if (!PreventHibernate.Initialized)
                return;

			WinAPI.SetThreadExecutionState(PreventHibernate.LastExecutionState);
			Program.Reader.ReadAsync("Hibernation allowed.");
		}
	}
}