using MessageBoxes;
using Microsoft.Speech.Recognition;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using VoiceController.Commands;
using VoiceController.Messages;
using ICommand = VoiceController.Commands.ICommand;

namespace VoiceController
{
    class Program
	{
		public static string GetActiveWindowTitle()
		{
			const int nChars = 256;
			var Buff = new StringBuilder(nChars);
			var handle = WinAPI.GetForegroundWindow();
			return WinAPI.GetWindowText(handle, Buff, nChars) > 0 ? Buff.ToString() : null;
		}

		public const string CultureInfo = "en-US";

		static List<ICommand> commands;
        static readonly AutoResetEvent exitEvent = new AutoResetEvent(false);
		public static readonly SpeechRecognitionEngine SpeechRecognitionEngine = new SpeechRecognitionEngine(new CultureInfo(CultureInfo));
		//public static readonly SpeechRecognitionEngine SpeechRecognitionEngine = new SpeechRecognitionEngine(new CultureInfo(CultureInfo));
		//public static readonly DictationGrammar DefaultGrammar = new DictationGrammar { Name = "default dictation", Enabled = true };
		//public static readonly DictationGrammar Spelling = new DictationGrammar("grammar:dictation#spelling") { Name = "spelling dictation", Enabled = true };
		//public static readonly DictationGrammar Questions = new DictationGrammar("grammar:dictation") { Name = "question dictation", Enabled = true };
		public static Grammar Commands;
        static bool executeCommands;

        public static Logger Logger;
        public static Reader Reader;
        public static WindowHandler WindowHandler;
        public static UrlMp3Reader UrlMp3Reader;

		static void Main(string[] args)
		{
            UnhandledExceptionHandler.CatchUnhandledExceptions();
            Logger = new Logger();
            Reader = new Reader();
            WindowHandler = new WindowHandler();
            UrlMp3Reader = new UrlMp3Reader();

            Reader.ReadAsync("Listening stopped.");

			var hWnd = WinAPI.FindWindow(null, Console.Title);
            if (hWnd != IntPtr.Zero)
            {
                WinAPI.ShowWindow(hWnd, 0);
            }

			commands = new List<ICommand>();

            var commandType = typeof(ICommand);
            var basic_commands = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => !type.IsInterface && commandType.IsAssignableFrom(type));
            
			foreach (var basic_command in basic_commands)
			{
                var needAutoResetEventType = typeof(INeedAutoResetEvent);
                var command = needAutoResetEventType.IsAssignableFrom(basic_command) ?
                    (ICommand)Activator.CreateInstance(basic_command, new object[] { exitEvent }) :
                    (ICommand)Activator.CreateInstance(basic_command);
                commands.Add(command);
			}

			SpeechRecognitionEngine.SpeechRecognized += SpeechRecognitionEngineOnSpeechRecognized;
            try
            {
				SpeechRecognitionEngine.SetInputToDefaultAudioDevice();
            }
            catch (PlatformNotSupportedException)
            {
                ErrorBox.Show("Requirement not found", "Please install recognition engine: https://www.microsoft.com/en-us/download/confirmation.aspx?id=27225, https://www.microsoft.com/en-us/download/details.aspx?id=27224");
                return;
            }

			var words = new Choices();
            foreach (var command_names in commands.SelectMany(command => command.CommandName))
            {
                words.Add(command_names);
            }
			var grammar_builder = new GrammarBuilder(words) { Culture = SpeechRecognitionEngine.RecognizerInfo.Culture };

			Commands = new Grammar(grammar_builder);
			SpeechRecognitionEngine.LoadGrammar(Commands);
			SpeechRecognitionEngine.RecognizeAsync(RecognizeMode.Multiple);

            exitEvent.WaitOne();
			SpeechRecognitionEngine.RecognizeAsyncCancel();

            Reader.Read("See you later.", "Good bye!", "Bye bye!", "Bye for now!", "Bye!", "So long!");
        }

		static void SpeechRecognitionEngineOnSpeechRecognized(object sender, SpeechRecognizedEventArgs speech_recognized_event_args)
		{
			var recognition_result = speech_recognized_event_args.Result;
            if (recognition_result.Confidence < 0.6)
            {
                return;
            }

			var stopped = false;

			foreach (var command in from command in commands from command_name in command.CommandName.Where(command_name => String.Compare(command_name, speech_recognized_event_args.Result.Text, StringComparison.OrdinalIgnoreCase) == 0) select command)
			{
				try
				{
					executeCommands |= command is StartListening;
					if (executeCommands)
					{
						if (command is IStopListening)
						{
							stopped = true;
							SpeechRecognitionEngine.RecognizeAsyncStop();
                            if (!(command is IStopListening))
                            {
                                new StopListeningCmd().Execute();
                            }
						}

						command.Execute();

						if (command.StopListening)
						{
							new StopListeningCmd().Execute();
							executeCommands = false;
						}
					}
					//else Read("Use \"Start listening\" command to start recognition.");

					executeCommands &= !(command is StopListeningCmd);
				}
				catch (Exception ex)
				{
					ErrorBox.Show(ex);
				}
				finally
				{
					if ((command is IStopListening) && stopped)
					{
						SpeechRecognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
                        if (!(command is IStopListening))
                        {
                            new StartListening().Execute();
                        }
					}
				}
				break;
			}
		}
	}
}
