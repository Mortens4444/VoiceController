using System.Collections.Generic;
using System.Threading;

namespace VoiceController.Commands
{
	class Exit : ICommand, INeedAutoResetEvent
    {
		readonly AutoResetEvent exit_event;

		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "Exit", "Exit voice application", "Exit voice control", "Exit voice controller", "Exit voice recognition" }; }
		}

		public Exit(AutoResetEvent exit_event)
		{
			this.exit_event = exit_event;
		}

		public void Execute(object o = null)
		{
			exit_event.Set();
		}
	}
}