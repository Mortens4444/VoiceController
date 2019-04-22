using System;

namespace VoiceController.Messages
{
	public delegate void SentChangedEventHandler(object sender, SentChangedEventArgs e);
	public delegate void TimerEventHandler(int id, int msg, IntPtr user, int dw1, int dw2);
//	public delegate IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
	public delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);
	public delegate string StringResultVoidParams();

	public delegate void VoidResultBoolParams(bool boolean);
	public delegate void VoidResultStringParams(string str);

	public delegate void VoidResultVoidParams();
}