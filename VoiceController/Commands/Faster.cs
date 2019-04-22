using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace VoiceController.Commands
{
	class Faster : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "Faster playing", "Faster", "Play faster" }; }
		}

		public void Execute(object o = null)
		{
			var processes = Process.GetProcessesByName("vlc");
			foreach (var process in processes)
			{
				Program.WindowHandler.SetForegroundProcessByProcessID(process.Id);
				SendKeys.SendWait("+3");
				process.Close();
			}
		}
	}
}