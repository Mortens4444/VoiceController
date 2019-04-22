using System.Collections.Generic;
using System.Diagnostics;

namespace VoiceController.Commands
{
	class Left4Dead2 : ICommand, IStopListening
    {
		public bool StopListening
		{
			get { return true; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "Left For Dead Two", "I want to kill zombies" }; }
		}

		public void Execute(object o = null)
		{
			using (var process = Process.Start(@"C:\Program Files (x86)\Steam\steamapps\common\left 4 dead 2\left4dead2.exe", "-secure -novid"))
			{
                if (process == null)
                {
                    return;
                }
				process.WaitForExit();
				process.Close();
			}
		}
	}
}