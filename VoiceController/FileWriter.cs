using System;
using System.IO;

namespace VoiceController
{
    public class FileWriter
    {
        public Exception Write(string filename, string data, bool overwrite, bool writeLine)
        {
            Exception result = null;
            try
            {
                var writer = overwrite ? File.CreateText(filename) : File.AppendText(filename);
                UseStreamWriter(writer, data, writeLine);
            }
            catch (Exception ex)
            {
                result = ex;
            }
            return result;
        }

        private static void UseStreamWriter(StreamWriter writer, string data, bool writeLine)
        {
            using (var streamWriter = writer)
            {
                WriteData(streamWriter, data, writeLine);
                streamWriter.Close();
            }
        }

        private static void WriteData(TextWriter streamWriter, string data, bool writeLine)
        {
            if (writeLine)
            {
                streamWriter.WriteLine(data);
            }
            else
            {
                streamWriter.Write(data);
            }
        }
    }
}
