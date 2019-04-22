using System.Collections.Generic;
using VoiceController.Messages;

namespace VoiceController.Commands
{
	class PreventHibernate : ICommand
	{
		public static bool Initialized;
		public static EXECUTION_STATE LastExecutionState;

		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "Prevent hibernate" }; }
		}

		public void Execute(object o = null)
		{
			Initialized = true;
			LastExecutionState = WinAPI.SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS | EXECUTION_STATE.ES_SYSTEM_REQUIRED | EXECUTION_STATE.ES_DISPLAY_REQUIRED);
            Program.Reader.ReadAsync("Hibernation denied.");
		}
	}
}