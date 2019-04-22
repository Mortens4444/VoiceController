//using System.Collections.Generic;
//using System.Threading;
//using VoiceController.Commands;

//namespace VoiceController.DictateCommands
//{
//	class StopDictate : ICommand, INeedAutoResetEvent
//	{
//        readonly AutoResetEvent exitEvent;

//		public bool StopListening
//		{
//			get { return false; }
//		}

//		public List<string> CommandName
//		{
//			get { return new List<string> { "Stop Dictate" }; }
//		}

//		public StopDictate(AutoResetEvent exitEvent)
//		{
//            this.exitEvent = exitEvent;
//		}

//		public void Execute(object o = null)
//		{
//            exitEvent.Set();
//		}
//	}
//}