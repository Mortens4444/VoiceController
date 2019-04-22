//using System;
//using System.Collections.Generic;
//using System.Speech.Recognition;
//using System.Threading;

//namespace VoiceController.Commands
//{
//	class Search : ICommand, IStopListening
//    {
//		public static readonly SpeechRecognitionEngine SpeechRecognitionEngine;
//		static AutoResetEvent ExitEvent;

//		static Search()
//		{
//			SpeechRecognitionEngine = new SpeechRecognitionEngine(Program.SpeechRecognitionEngine.RecognizerInfo.Culture);

//			SpeechRecognitionEngine.SpeechRecognized += SpeechRecognitionEngineOnSpeechRecognized;
//			SpeechRecognitionEngine.SetInputToDefaultAudioDevice();

//			var grammar = new DictationGrammar { Name = "default dictation", Enabled = true };
//			SpeechRecognitionEngine.LoadGrammar(grammar);
//		}

//		public bool StopListening
//		{
//			get { return true; }
//		}

//		public List<string> CommandName
//		{
//			get { return new List<string> { "Search", "Look for", "Google me what is" }; }
//		}

//		public void Execute(object o = null)
//		{
//			ExitEvent = new AutoResetEvent(false);
//			SpeechRecognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
//			ExitEvent.WaitOne();
//			SpeechRecognitionEngine.RecognizeAsyncCancel();
//		}

//		private static void SpeechRecognitionEngineOnSpeechRecognized(object sender, SpeechRecognizedEventArgs speech_recognized_event_args)
//		{
//            ProcessUtils.Start(String.Concat("https://www.google.hu/#q=", speech_recognized_event_args.Result.Text));
//			ExitEvent.Set();
//		}
//	}
//}