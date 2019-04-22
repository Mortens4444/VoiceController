using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace VoiceController.Commands
{
	class ReadInEnglish : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "Read in english" }; }
		}

		public void Execute(object o = null)
		{
			try
			{
				var thread = new Thread(() =>
    				{
    					var clipboard_content = Clipboard.GetText();
                        if (String.IsNullOrEmpty(clipboard_content))
                        {
                            return;
                        }
                        Program.UrlMp3Reader.ReadAsyncInEnglishWithGoogle(clipboard_content);
				    });
				thread.SetApartmentState(ApartmentState.STA);
				thread.Start();
			}
			catch { }
		}
	}
}