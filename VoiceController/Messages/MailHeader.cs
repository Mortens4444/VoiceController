namespace VoiceController.Messages
{
    public class MailHeader
    {
        public string Name { get; private set; }

        public string Value { get; private set; }

        public MailHeader(string name, object value)
        {
            Name = name;
            Value = value.ToString();
        }
    }
}

