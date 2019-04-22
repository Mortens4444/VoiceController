using MessageBoxes;
using SourceInfo;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace VoiceController
{
    public static class UnhandledExceptionHandler
    {
        private static string unhandled_exceptions_log_file;
            
        public static void CatchUnhandledExceptions()
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            //Application.ThreadException += new ThreadExceptionEventHandler(Utils.ThreadExceptionHandler);

            var unhandled_exceptions_folder = String.Format("{0}\\{1}\\", Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), System.Windows.Forms.Application.ProductName);
            unhandled_exceptions_log_file = String.Concat(unhandled_exceptions_folder, "unhandled_exception.txt");
            if (!Directory.Exists(unhandled_exceptions_folder))
                Directory.CreateDirectory(unhandled_exceptions_folder);
            
            var current_domain = AppDomain.CurrentDomain;
            current_domain.UnhandledException += UnhandledExceptionEventHandler;
        }

        private static void UnhandledExceptionEventHandler(Object sender, UnhandledExceptionEventArgs e)
        {
            HandledException(e.ExceptionObject as Exception);
        }

        private static void HandledException(Exception ex)
        {
            var error_details = new StringBuilder();
            error_details.AppendFormat("{0} {1}{2}", DateTime.Now.ToShortDateString(), DateTime.Now.ToLongTimeString(), Environment.NewLine);
            error_details.Append(new ExceptionDetails(ex).Details);

            Program.Logger.OldLog(error_details.ToString(), unhandled_exceptions_log_file, false);
            try
            {
                ErrorBox.Show(ex, Timeout.Infinite);
            }
            catch { }
            Environment.Exit(-1);
        }
    }
}
