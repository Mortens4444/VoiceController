using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace VoiceController.Commands
{
    class VlcVolumeUp : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "VLC volume up", "Press control up", "Increase volume in VLC" }; }
		}

		public void Execute(object o = null)
		{
			var processes = Process.GetProcessesByName("vlc");
			foreach (var process in processes)
			{
                Program.WindowHandler.SetForegroundProcessByProcessID(process.Id);
				SendKeys.SendWait("^{UP}");
				process.Close();
			}
		}
	}
}