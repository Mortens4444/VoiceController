using System;
using System.Text;

namespace VoiceController
{
    public class Logger
    {
        private const string DELIMITER = "______________________________________________________________________________________________________________________";

        public Exception OldLog(string loginfo, string filename, bool showdate)
        {
            Exception result;
            try
            {
                var stringBuilder = new StringBuilder();
                if (showdate)
                {
                    stringBuilder.AppendLine(DELIMITER);
                    stringBuilder.Append("\t\t\t\t\tDate: ");
                    stringBuilder.Append(DateTime.Now.ToShortDateString());
                    stringBuilder.Append(' ');
                    stringBuilder.AppendLine(DateTime.Now.ToLongTimeString());
                    stringBuilder.AppendLine(DELIMITER);
                }
                stringBuilder.AppendLine(loginfo);
                var fileWriter = new FileWriter();
                result = fileWriter.Write(filename, stringBuilder.ToString(), false, true);
            }
            catch (Exception ex)
            {
                result = ex;
            }
            return result;
        }
    }
}
