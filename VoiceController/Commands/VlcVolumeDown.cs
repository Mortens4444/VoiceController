using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace VoiceController.Commands
{
	class VlcVolumeDown : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "VLC volume down", "Press control down", "Reduce volume in VLC" }; }
		}

		public void Execute(object o = null)
		{
			var processes = Process.GetProcessesByName("vlc");
			foreach (var process in processes)
			{
                Program.WindowHandler.SetForegroundProcessByProcessID(process.Id);
				SendKeys.SendWait("^{DOWN}");
				process.Close();
			}
		}
	}
}