using System;
using System.IO;

namespace VoiceController
{
    public static class FileUtils
	{
		public static string GetFileContent(string filename)
		{
			string result;
            if (File.Exists(filename))
            {
                using (var file_stream = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    using (var stream_reader = new StreamReader(file_stream))
                    {
                        result = stream_reader.ReadToEnd();
                        stream_reader.Close();
                    }
                    file_stream.Close();
                }
            }
            else
            {
                throw new FileNotFoundException(String.Concat("File not found: ", filename), filename);
            }
			return result;
		}

		public static string SearchForFirst(string directory, string filename)
		{
			try
			{
				var result = Directory.GetFiles(directory, filename, SearchOption.TopDirectoryOnly);
                if (result.Length > 0)
                {
                    return result[0];
                }
				var subdirectories = Directory.GetDirectories(directory);
				for (var i = 0; i < subdirectories.Length; i++)
				{
					//return SearchForFirst(subdirectories[i], filename);
					var found = SearchForFirst(subdirectories[i], filename);
                    if (found != null)
                    {
                        return found;
                    }
				}
				return null;
			}
			catch { return null; }
		}
	}
}
