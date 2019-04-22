//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Speech.Recognition;
//using System.Threading;

//namespace VoiceController.Commands
//{
//	class Translate : ICommand, IStopListening
//    {
//		public static readonly SpeechRecognitionEngine SpeechRecognitionEngine;
//		static AutoResetEvent ExitEvent;

//		static Translate()
//		{
//			SpeechRecognitionEngine = new SpeechRecognitionEngine(Program.SpeechRecognitionEngine.RecognizerInfo.Culture);

//			SpeechRecognitionEngine.SpeechRecognized += SpeechRecognitionEngineOnSpeechRecognized;
//			SpeechRecognitionEngine.SetInputToDefaultAudioDevice();

//			var grammar = new DictationGrammar { Name = "default dictation", Enabled = true };
//			SpeechRecognitionEngine.LoadGrammar(grammar);
//		}

//		public bool StopListening
//		{
//			get { return false; }
//		}

//		public List<string> CommandName
//		{
//			get { return new List<string> { "Translate", "Translate to hungarian" }; }
//		}

//		public void Execute(object o = null)
//		{
//			ExitEvent = new AutoResetEvent(false);
//			SpeechRecognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
//			ExitEvent.WaitOne();
//			SpeechRecognitionEngine.RecognizeAsyncCancel();
//		}

//		static void SpeechRecognitionEngineOnSpeechRecognized(object sender, SpeechRecognizedEventArgs speech_recognized_event_args)
//		{
//			using (Process.Start(String.Concat("https://translate.google.hu/?hl=hu&tab=wT#en/hu/", speech_recognized_event_args.Result.Text))) { }
//			ExitEvent.Set();
//		}
//	}
//}