using System;
using System.Drawing;
using System.Text;
using System.Runtime.InteropServices;

namespace VoiceController.Messages
{
	public static class WinAPI
	{
		[DllImport("User32.dll")]
		public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		[DllImport("User32.dll")]
		public static extern IntPtr GetForegroundWindow();

		[DllImport("User32.dll", SetLastError = true)]
		public static extern bool GetWindowRect(IntPtr hWnd, out Rectangle lpRect);

		[DllImport("User32.dll")]
		public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

		[DllImport("User32.dll")]
		public static extern bool IsZoomed(IntPtr hWnd);

		[DllImport("User32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.I4)]
		public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

		[DllImport("User32.dll", SetLastError = true)]
		public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 msg, Int32 wParam, int[] lParam);

		[DllImport("User32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool SetForegroundWindow(IntPtr hWnd);

		[DllImport("User32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y  , int cx, int cy, SetWindowPosFlags uFlags);

		[DllImport("User32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, nCmdShow nCmdShow);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto,SetLastError = true)]
		public static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);
	}
}
