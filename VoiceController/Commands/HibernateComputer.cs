using System.Collections.Generic;

namespace VoiceController.Commands
{
	class HibernateComputer : ICommand
	{
		public bool StopListening
		{
			get { return false; }
		}

		public List<string> CommandName
		{
			get { return new List<string> { "Hibernate computer" }; }
		}

		public void Execute(object o = null)
		{
            ProcessUtils.Start("shutdown", "/h");
		}
	}
}