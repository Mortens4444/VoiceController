using System.Collections.Generic;

namespace VoiceController.Commands
{
	class SaveDesktopImage : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "Save desktop image", "Save background", "Save background image" }; }
		}

		public void Execute(object o = null)
		{
			Program.Reader.ReadAsync("Saving background image!");
            ProcessUtils.Start(@"C:\Users\Mortens\Documents\Visual Studio 2008\Projects\DesktopUpdater\DesktopUpdater\bin\Release\DesktopUpdater.exe", @"-s F:\Képek\Bing");
		}
	}
}