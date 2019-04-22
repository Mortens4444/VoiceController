//using System;
//using System.Collections.Generic;
//using System.Speech.Recognition;
//using System.Threading;
//using System.Windows.Forms;

//namespace VoiceController.Commands
//{
//	class Press : ICommand, IStopListening
//    {
//		public static readonly SpeechRecognitionEngine SpeechRecognitionEngine;
//		static AutoResetEvent ExitEvent;

//		static Press()
//		{
//			SpeechRecognitionEngine = new SpeechRecognitionEngine(Program.SpeechRecognitionEngine.RecognizerInfo.Culture);

//			SpeechRecognitionEngine.SpeechRecognized += SpeechRecognitionEngineOnSpeechRecognized;
//			SpeechRecognitionEngine.SetInputToDefaultAudioDevice();

//			var spelling = new DictationGrammar("grammar:dictation#spelling") { Name = "spelling dictation", Enabled = true };
//			SpeechRecognitionEngine.LoadGrammar(spelling);
//		}

//		public bool StopListening
//		{
//			get { return true; }
//		}

//		public List<string> CommandName
//		{
//			get { return new List<string> { "Press", "Send key", "Keypress" }; }
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
//			try
//			{
//				SendKeys.SendWait(speech_recognized_event_args.Result.Text);
//				ExitEvent.Set();
//			}
//			catch { }
//		}
//	}
//}