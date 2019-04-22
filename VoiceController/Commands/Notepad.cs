using System.Collections.Generic;
using System.IO;

namespace VoiceController.Commands
{
	class Notepad : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "Notepad", "Notepad++" }; }
		}

		public void Execute(object o = null)
		{
            if (File.Exists(@"C:\Program Files (x86)\Notepad++\notepad++.exe"))
            {
                ProcessUtils.Start(@"C:\Program Files (x86)\Notepad++\notepad++.exe");
            }
            else
            {
                if (File.Exists(@"C:\Program Files\Notepad++\notepad++.exe"))
                {
                    ProcessUtils.Start(@"C:\Program Files\Notepad++\notepad++.exe");
                }
                else
                {
                    ProcessUtils.Start("notepad");
                }
            }
		}
	}
}