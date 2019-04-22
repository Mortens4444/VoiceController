using System.Collections.Generic;

namespace VoiceController.Commands
{
	class WriteToMortens : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "Chat with Mortens", "Chat with developer", "Write to Mortens", "Write to developer", "Write to Mortens with Skype" }; }
		}

		public void Execute(object o = null)
		{
            ProcessUtils.Start("skype:mortens.4444?chat");
		}
	}
}