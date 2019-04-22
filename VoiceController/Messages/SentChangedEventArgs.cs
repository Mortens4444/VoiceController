using System;
using System.Collections.Generic;

namespace VoiceController.Messages
{
	public class SentChangedEventArgs : EventArgs
	{
        public bool Sent { get; private set; }

        public List<object> Arguments { get; private set; }

        public IEnumerable<MailHeader> Headers { get; private set; }

        public Exception Exception { get; private set; }

        public SentChangedEventArgs(IEnumerable<MailHeader> headers, Exception exception, List<object> arguments = null)
		{
            Sent = exception != null;
			Headers = headers;
			Exception = exception;
			Arguments = arguments;
		}
	}
}
