using System.Diagnostics;

namespace VoiceController
{
    public static class ProcessUtils
    {
        public static Process Start(string path, string parameters = null)
        {
            return parameters != null ? Process.Start(path, parameters) : Process.Start(path);
        }

		/// <summary>
		/// Closes process with the given process name.
		/// </summary>
		/// <param name="process_name">The process name should not contain the extension.</param>
		/// <returns>Returns true, if operation was successful.</returns>
		public static bool CloseProcesses(string process_name)
		{
			var result = false;
			try
			{
				var processes = Process.GetProcessesByName(process_name);
				CloseProcesses(processes);
				FreeProcessArrayResources(processes);
				processes = Process.GetProcessesByName(process_name);
				result = processes.Length == 0;
			}
			catch { }
			return result;
		}

		/// <summary>
		/// Closes the given processes, using CloseMainWindow method.
		/// </summary>
		/// <param name="processes">The process or processes that should be closed.</param>
		public static void CloseProcesses(params Process[] processes)
		{
			foreach (var process in processes)
			{
				try
				{
					if (!process.HasExited) process.CloseMainWindow();
				}
				catch { }
			}
		}

		public static void FreeProcessArrayResources(params Process[] processes)
		{
            foreach (var process in processes)
            {
                process.Close();
            }
		}
	}
}
