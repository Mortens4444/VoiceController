using System.Collections.Generic;
using System.Diagnostics;

namespace VoiceController.Commands
{
	class StopVlc : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "Stop VLC", "Stop music", "Stop video", "Stop video player" }; }
		}

		public void Execute(object o = null)
		{
			var processes = Process.GetProcessesByName("vlc");
			foreach (var process in processes)
			{
				process.CloseMainWindow();
				process.Close();
			}
		}
	}
}