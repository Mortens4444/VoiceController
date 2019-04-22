using System.Collections.Generic;

namespace VoiceController.Commands
{
	class TaskManager : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "Task manager" }; }
		}

		public void Execute(object o = null)
		{
            ProcessUtils.Start("taskmgr");
		}
	}
}