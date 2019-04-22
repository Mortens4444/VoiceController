using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;

namespace VoiceController.Messages
{
	public partial class ErrorBox : BaseBox
	{
		private ErrorBox(string title, string message, int interval_in_milliseconds)
		{
			InitializeComponent();
			this.btn_Ok.Text = BaseBox.OK;
			this.Text = String.Concat(Application.ProductName, ": ", title);
			this.rtb_Message.Text = message;
			this.t_Close.Enabled = false;
			if (interval_in_milliseconds != Timeout.Infinite) this.t_Close.Interval = interval_in_milliseconds;
			else
			{
				btn_Pin.Visible = false;
				btn_Unpin.Visible = false;
			}
			this.t_DecrementSecondsLeft.Enabled = false;

			this.tt_Hint_2.SetToolTip(btn_SendToClipboard, BaseBox.CopyToClipboard);
			this.tt_Hint_3.SetToolTip(btn_SendMessage, BaseBox.SendErrorReport);
		}

		private void PinMessage()
		{
			this.t_Close.Stop();
			this.t_DecrementSecondsLeft.Stop();
			this.btn_Pin.Visible = false;
			this.btn_Unpin.Visible = true;
			this.tt_Hint.SetToolTip(btn_Unpin, BaseBox.EnableAutomaticMessageClosing);
			this.btn_Ok.Text = BaseBox.OK;
		}

		private void btn_Pin_Click(object sender, EventArgs e)
		{
			PinMessage();
		}

		private void UnpinMessage()
		{
			this.btn_Pin.Visible = true;
			this.btn_Unpin.Visible = false;
			this.tt_Hint.SetToolTip(btn_Pin, BaseBox.DisableAutomaticMessageClosing);
			this.t_Close.Start();
			this.t_DecrementSecondsLeft.Start();
			this.seconds_left = (int)(Math.Truncate((decimal)this.t_Close.Interval / 1000));
			ShowMessageOnOKButton();
		}

		private void btn_Unpin_Click(object sender, EventArgs e)
		{
			UnpinMessage();
		}

		private void ShowMessageOnOKButton()
		{
			StringBuilder ok_seconds_left = new StringBuilder(ErrorBox.OK);
			ok_seconds_left.Append(" (");
			ok_seconds_left.Append(this.seconds_left.ToString());
			ok_seconds_left.Append(')');
			this.btn_Ok.Text = ok_seconds_left.ToString();
		}

		private void t_DecrementSecondsLeft_Tick(object sender, EventArgs e)
		{
			this.seconds_left--;
			ShowMessageOnOKButton();
		}

		private static DialogResult Show(ErrorBox eb)
		{
			System.Diagnostics.Debug.WriteLine(eb.Text);
			System.Diagnostics.Debug.WriteLine(eb.rtb_Message.Text);
			if (eb.parent != null)
			{
				eb.Left = eb.parent.Left + (eb.parent.Width / 2) - (eb.Width / 2);
				eb.Top = eb.parent.Top + (eb.parent.Height / 2) - (eb.Height / 2);
			}
			else
			{
				eb.StartPosition = FormStartPosition.CenterScreen;
			}
			Console.Beep(440, 200);
			try
			{
				return eb.ShowDialog();
			}
			catch { return DialogResult.None; }
		}

		public static DialogResult Show(string title, string message, int interval_in_milliseconds)
		{
			return Show(null, title, message, interval_in_milliseconds);
		}

		public static DialogResult Show(string title, string message)
		{
			return Show(null, title, message, Constants.MILLISECONDS_LEFT);
		}

		public static DialogResult Show(Form parent, string title, string message)
		{
			return Show(parent, title, message, Constants.MILLISECONDS_LEFT);
		}

		public static void ShowLastWin32Error()
		{
			ErrorBox.Show(new Win32Exception(Marshal.GetLastWin32Error()));
		}

		public static void ShowLastWin32ErrorIfNotSuccess()
		{
			int error_code = Marshal.GetLastWin32Error();
			if (error_code != 0) ErrorBox.Show(new Win32Exception(error_code));
		}

		public static DialogResult Show(Exception ex)
		{
			ExceptionDetails ed = new ExceptionDetails(ex);
			return Show(null, ed.ExceptionType, ed.Details, Constants.MILLISECONDS_LEFT);
		}

		public static DialogResult ShowServiceNotification(Exception ex)
		{
			return MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
		}

		public static DialogResult Show(Exception ex, int interval_in_milliseconds)
		{
			ExceptionDetails ed = new ExceptionDetails(ex);
			return Show(null, ed.ExceptionType, ed.Details, interval_in_milliseconds);
		}

		public static DialogResult Show(Form parent, Exception ex)
		{
			ExceptionDetails ed = new ExceptionDetails(ex);
			return Show(parent, ed.ExceptionType, ed.Details, Constants.MILLISECONDS_LEFT);
		}

		public static DialogResult Show(Form parent, string title, Exception ex)
		{
			return Show(parent, title, ex.GetDetails(), Constants.MILLISECONDS_LEFT);
		}

		public static DialogResult Show(string title, Exception ex)
		{
			return Show(null, title, ex.GetDetails(), Constants.MILLISECONDS_LEFT);
		}

		public static DialogResult Show(string title, Exception ex, int interval_in_milliseconds)
		{
			return Show(null, title, ex.GetDetails(), interval_in_milliseconds);
		}

		public static DialogResult Show(Form parent, string title, string message, int interval_in_milliseconds)
		{
			ErrorBox eb = new ErrorBox(title, message, interval_in_milliseconds);
			eb.parent = parent;
			if (interval_in_milliseconds != Timeout.Infinite) eb.UnpinMessage();
			return Show(eb);
		}

		private void t_Close_Tick(object sender, EventArgs e)
		{
			this.Close();
		}

		private void ErrorBox_Shown(object sender, EventArgs e)
		{
			rtb_Message.Select(0, 0);
		}

		private void ErrorBox_Load(object sender, EventArgs e)
		{
			//WinAPI.SetWindowLong(this.Handle, (int)GetWindowLongParam.GWL_EXSTYLE, (WinAPI.GetWindowLong(this.Handle, GetWindowLongParam.GWL_EXSTYLE) | (int)ExtendedWindowStyles.WS_EX_TOPMOST));
			WinAPI.SetWindowPos(this.Handle, new IntPtr(-1), 0, 0, 0, 0, SetWindowPosFlags.IgnoreResize | SetWindowPosFlags.IgnoreMove); // Set TOP_MOST
		}

		private void tsmi_Copy_Click(object sender, EventArgs e)
		{
			SendKeys.Send("^(C)");
		}

		private void btn_SendMessage_Click(object sender, EventArgs e)
		{
			PinMessage();
			SendErrorReport ser = new SendErrorReport(String.Concat(this.Text, ": ", rtb_Message.Text, Environment.NewLine, Environment.NewLine, new StackTrace().ToString()));
			ser.Show();
		}

		private void btn_SendToClipboard_Click(object sender, EventArgs e)
		{
			try
			{
				/*Thread.CurrentThread.SetApartmentState(ApartmentState.STA);
				Clipboard.SetText(rtb_Message.Text);*/
				rtb_Message.SelectAll();
				rtb_Message.Focus();
				SendKeys.Send("^(C)");
				//rtb_Message.Select(0, 0);
			}
			catch (Exception ex)
			{
				ErrorBox.Show(ex);
			}
		}
	}
}
