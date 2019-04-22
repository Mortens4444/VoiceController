using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace VoiceController.Commands
{
	class WhatIsOnClipboard : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "What is on clipboard", "Read out clipboard content" }; }
		}

		public void Execute(object o = null)
		{
			try
			{
				var thread = new Thread(() =>
    				{
    					var clipboardContent = Clipboard.GetText();
                        if (String.IsNullOrEmpty(clipboardContent))
                        {
                            return;
                        }
                        Program.Reader.ReadAsync(clipboardContent);
    				});
				thread.SetApartmentState(ApartmentState.STA);
				thread.Start();
			}
			catch { }
		}
	}
}