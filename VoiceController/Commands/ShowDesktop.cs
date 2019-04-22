using System.Collections.Generic;

namespace VoiceController.Commands
{
	class ShowDesktop : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "Show Desktop", "Minimize all", "Minimize all programs", "I want to see the desktop" }; }
		}

		public void Execute(object o = null)
		{
			//var shell = new Shell32.ShellClass();
			//shell.MinimizeAll();
		}
	}
}