using System;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace VoiceController.Messages
{
	public partial class InfoBox : BaseBox
	{
		private InfoBox(string title, string message, int interval_in_milliseconds)
		{
			InitializeComponent();
			this.btn_Ok.Text = BaseBox.OK;
			this.Text = String.Concat(Application.ProductName, ": ", title); //title;
			this.rtb_Message.Text = message;
			this.t_Close.Enabled = false;
			if (interval_in_milliseconds != Timeout.Infinite) this.t_Close.Interval = interval_in_milliseconds;
			else
			{
				btn_Pin.Visible = false;
				btn_Unpin.Visible = false;
			}
			this.t_DecrementSecondsLeft.Enabled = false;
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
			StringBuilder ok_seconds_left = new StringBuilder(BaseBox.OK);
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

		private static DialogResult Show(InfoBox ib)
		{
			if (ib.parent != null)
			{
				ib.Left = ib.parent.Left + (ib.parent.Width / 2) - (ib.Width / 2);
				ib.Top = ib.parent.Top + (ib.parent.Height / 2) - (ib.Height / 2);
			}
			else
			{
				ib.StartPosition = FormStartPosition.CenterScreen;
			}
			return ib.ShowDialog();
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

		public static DialogResult Show(Form parent, string title, string message, int interval_in_milliseconds)
		{
			InfoBox ib = new InfoBox(title, message, interval_in_milliseconds);
			ib.parent = parent;
			if (interval_in_milliseconds != Timeout.Infinite) ib.UnpinMessage();
			return Show(ib);
		}

		private void t_Close_Tick(object sender, EventArgs e)
		{
			this.Close();
		}

		private void InfoBox_Shown(object sender, EventArgs e)
		{
			rtb_Message.Select(0, 0);
		}

		private void tsmi_Copy_Click(object sender, EventArgs e)
		{
			SendKeys.Send("^(C)");
		}
	}
}
