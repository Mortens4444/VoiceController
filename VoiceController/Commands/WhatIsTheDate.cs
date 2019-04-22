using System;
using System.Collections.Generic;

namespace VoiceController.Commands
{
	class WhatIsTheDate : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "What is the date?", "Can you tell me the date?" }; }
		}

		public void Execute(object o = null)
		{
            Program.Reader.ReadAsync(DateTime.Now.ToLongDateString());
		}
	}
}