using System.Collections.Generic;

namespace VoiceController.Commands
{
	class VideoCallMortens : ICommand
	{
		public bool StopListening
		{
			get { return true; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "Video call Mortens", "Video call developer", "Video call Mortens with Skype" }; }
		}

		public void Execute(object o = null)
		{
            ProcessUtils.Start("skype:mortens.4444?call&video=true");
		}
	}
}