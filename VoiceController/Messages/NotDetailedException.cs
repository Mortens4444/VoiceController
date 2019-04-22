using System;

namespace VoiceController.Messages
{
	public class NotDetailedException : Exception
	{
		public string Title { get; private set; }

		public NotDetailedException(Exception ex) : this(ex.GetType().ToString(), ex.Message)
		{
		}

		public NotDetailedException(string message) : this(String.Empty, message)
		{
		}

		public NotDetailedException(string title, string message) : base(message)
		{
			Title = title;
		}
	}
}
