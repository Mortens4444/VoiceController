using System.Collections.Generic;

namespace VoiceController.Commands
{
	class CallMortens : ICommand
	{
		public bool StopListening
		{
			get { return true; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "Call Mortens", "Call developer", "Call Mortens with Skype" }; }
		}

		public void Execute(object o = null)
		{
            ProcessUtils.Start("skype:mortens.4444?call");
		}
	}
}