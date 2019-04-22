using MessageBoxes;
using System;
using System.Diagnostics;
using VoiceController.Messages;

namespace VoiceController
{
    public class WindowHandler
    {
        public void SetActiveWindow(IntPtr handle)
        {
            var active_handle = WinAPI.GetForegroundWindow();
            if (active_handle != handle)
            {
                WinAPI.SetForegroundWindow(handle);
            }
            WinAPI.ShowWindow(handle, WinAPI.IsZoomed(handle) ? nCmdShow.SW_MAXIMIZE : nCmdShow.SW_RESTORE);
        }

        public void SetForegroundProcessByProcessID(int process_id)
        {
            try
            {
                using (var ps = Process.GetProcessById(process_id))
                {
                    SetActiveWindow(ps.MainWindowHandle);
                    ps.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorBox.Show(ex);
            }
        }
    }
}
