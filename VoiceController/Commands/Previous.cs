using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace VoiceController.Commands
{
	class Previous : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "Previous" }; }
		}

		public void Execute(object o = null)
		{
			var processes = Process.GetProcessesByName("vlc");
			foreach (var process in processes)
			{
				Program.WindowHandler.SetForegroundProcessByProcessID(process.Id);
				SendKeys.SendWait("p");
				process.Close();
			}
		}
	}
}