using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace VoiceController.Commands
{
	class Pause : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "Pause", "Break", "Stop for a second", "Hold for a moment", "Wait a sec", "Take a break", "Take a short break" }; }
		}

		public void Execute(object o = null)
		{
			var processes = Process.GetProcessesByName("vlc");
			foreach (var process in processes)
			{
				Program.WindowHandler.SetForegroundProcessByProcessID(process.Id);
				//Program.WaitForProcessToComeForeground(process.MainWindowHandle);
				SendKeys.SendWait(" ");
				process.Close();
			}
		}
	}
}