using System;
using System.Windows.Forms;
using MessageBoxes;

namespace VoiceController.Messages
{
	public sealed partial class SendErrorReport : Form
	{
		public static SmtpServer SmtpServer;

		public static string Str_Title = "Error report";
		public static string Str_Settings = "Settings";
		public static string Str_SMTP_Server = "SMTP Server";
		public static string Str_SMTP_Port = "SMTP port";
		public static string Str_Authentication = "Authentication";
		public static string Str_SSL = "SSL";
		public static string Str_Username = "Username";
		public static string Str_Password = "Password";
		public static string Str_ForceAuthentication = "Force authentication";
		public static string Str_SendEmail = "Send e-mail";
		public static string Str_Sender = "Sender";
		public static string Str_Recipient = "Recipient";
		public static string Str_Subject = "Subject";
		public static string Str_Send = "Send";
		public static string Str_SentSuccessful = "Sent successful";
		public static string Str_SentFailed = "Sent failed";
		public static string Str_ErrorReportHasBeenSent = "Error report has been sent";
		public static string Str_SMTPServerNotSet = "SMTP server not set";

		public SendErrorReport(string error_report)
		{
			InitializeComponent();
			cb_ForceAuthentication.SelectedIndex = 2;
			rtb_Message.Text = error_report;
			p_Settings.Visible = SendErrorReport.SmtpServer == null;

			Text = Str_Title;
			gb_Settings.Text = Str_Settings;
			lbl_SMTP_Server.Text = Str_SMTP_Server;
			lbl_Port.Text = Str_SMTP_Port;
			chk_Authentication.Text = Str_Authentication;
			chk_SSL.Text = Str_SSL;
			lbl_Username.Text = Str_Username;
			lbl_Password.Text = Str_Password;
			chk_ForceAuthentication.Text = Str_ForceAuthentication;
			gb_EmailSender.Text = Str_SendEmail;
			lbl_From.Text = Str_Sender;
			lbl_To.Text = Str_Recipient;
			lbl_Title.Text = Str_Subject;
			btn_Send.Text = Str_Send;
		}

		private void chk_Authentication_CheckedChanged(object sender, EventArgs e)
		{
			lbl_Username.Enabled = chk_Authentication.Checked;
			tb_Username.Enabled = chk_Authentication.Checked;
			lbl_Password.Enabled = chk_Authentication.Checked;
			tb_Password.Enabled = chk_Authentication.Checked;
		}

		private void chk_ForceAuthentication_CheckedChanged(object sender, EventArgs e)
		{
			cb_ForceAuthentication.Enabled = chk_ForceAuthentication.Checked;
		}

		private void btn_Send_Click(object sender, EventArgs e)
		{
			try
			{
                if (tb_SMTP_Server.Text == String.Empty)
                {
                    throw new Exception(Str_SMTPServerNotSet);
                }

				SendMail mailer;
				if (SendErrorReport.SmtpServer == null)
				{
                    if (!chk_Authentication.Checked)
                    {
                        mailer = new SendMail(tb_SMTP_Server.Text, chk_SSL.Checked, (int)nud_Port.Value);
                    }
                    else
                    {
                        mailer = new SendMail(tb_SMTP_Server.Text, chk_SSL.Checked, (int)nud_Port.Value, tb_Username.Text, tb_Password.Text);
                    }

					mailer.ForceSmtpAuthentication = chk_ForceAuthentication.Checked;
					mailer.SmtpAuthentication = (SmtpAuthentication)cb_ForceAuthentication.SelectedIndex;

                    mailer.SentChanged += MailSentChanged;

					mailer.Send(tb_From.Text, tb_To.Text, tb_Title.Text, rtb_Message.Text);
				}
				else
				{
					mailer = new SendMail(SendErrorReport.SmtpServer);
					mailer.Send(tb_From.Text, tb_To.Text, tb_Title.Text, rtb_Message.Text);
				}
			}
			catch (Exception ex)
			{
				ErrorBox.Show(Str_SentFailed, new NotDetailedException(ex.Message));
			}
		}

		private void MailSentChanged(object sender, SentChangedEventArgs e)
		{
            if (e.Sent)
            {
                InfoBox.Show(Str_SentSuccessful, Str_ErrorReportHasBeenSent);
            }
            else
            {
                ErrorBox.Show(Str_SentFailed, new NotDetailedException(e.Exception.Message));
            }
		}

		private void SendErrorReport_Load(object sender, EventArgs e)
		{
            WinAPI.SetWindowPos(Handle, new IntPtr(-1), 0, 0, 0, 0, SetWindowPosFlags.IgnoreResize | SetWindowPosFlags.IgnoreMove); // Set TOP_MOST
		}
	}
}
