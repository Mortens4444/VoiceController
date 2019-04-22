using System;
using System.Collections.Generic;

namespace VoiceController.Commands
{
	class WhatIsTheTime : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "What is the time?", "What time is it?", "Can you tell me the time?", "Could you tell me what the time is?" }; }
		}

		public void Execute(object o = null)
		{
            Program.Reader.ReadAsync(DateTime.Now.ToLongTimeString());
		}
	}
}