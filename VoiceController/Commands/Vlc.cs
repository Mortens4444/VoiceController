using System.Collections.Generic;

namespace VoiceController.Commands
{
	class Vlc : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "VLC", "Media player", "Start VLC", "Open VLC", "Start media player", "Open media player" }; }
		}

		public void Execute(object o = null)
		{
            ProcessUtils.Start(@"C:\Program Files (x86)\VideoLAN\VLC\vlc.exe");
		}
	}
}