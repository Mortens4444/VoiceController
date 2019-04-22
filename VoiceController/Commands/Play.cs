using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VoiceController.Messages;

namespace VoiceController.Commands
{
	class Play : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "Play", "Play movie", "Playing", "Continoue", "Continoue playing", "Resume playing", "You can play now" }; }
		}

		public void Execute(object o = null)
		{
			var processes = Process.GetProcessesByName("vlc");
			foreach (var process in processes)
			{
                Program.WindowHandler.SetForegroundProcessByProcessID(process.Id);
				Rectangle rect;
				WinAPI.GetWindowRect(WinAPI.GetForegroundWindow(), out rect);

				var full_screen = Screen.AllScreens.Any(screen => (screen.Bounds.X == rect.X) && (screen.Bounds.Y == rect.Y) && (screen.Bounds.Width == rect.Width) && (screen.Bounds.Height == rect.Height));
				//Program.WaitForProcessToComeForeground(process.MainWindowHandle);
				SendKeys.SendWait(!full_screen ? "f " : " ");
				process.Close();
			}
		}
	}
}