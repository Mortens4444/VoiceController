using System.Collections.Generic;

namespace VoiceController.Commands
{
	interface ICommand
	{
		bool StopListening { get; }

		List<string> CommandName { get; }

		void Execute(object o = null);
	}
}
