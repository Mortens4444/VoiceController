using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace VoiceController.Commands
{
	class CloseApplication : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "Close application", "Close it", "Close that" }; }
		}

		public void Execute(object o = null)
		{
			SendKeys.SendWait(String.Compare(Program.GetActiveWindowTitle(), @"C:\Windows\system32\cmd.exe", StringComparison.OrdinalIgnoreCase) == 0 ? "exit{ENTER}" : "%{F4}");
		}
	}
}