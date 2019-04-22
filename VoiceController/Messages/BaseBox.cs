﻿using System.Windows.Forms;

namespace VoiceController.Messages
{
	public partial class BaseBox : Form
	{
		protected Form parent;
		protected int seconds_left;
		public static string OK = Constants.OK;
		public static string Cancel = Constants.CANCEL;
		public static string Yes = Constants.YES;
		public static string No = Constants.NO;
		public static string EnableAutomaticMessageClosing = Constants.ENABLE_AUTOMATIC_MESSAGE_CLOSING;
		public static string DisableAutomaticMessageClosing = Constants.DISABLE_AUTOMATIC_MESSAGE_CLOSING;
		public static string CopyToClipboard = Constants.COPY_TO_CLIPBOARD;
		public static string SendErrorReport = Constants.SEND_ERROR_REPORT;

		public BaseBox()
		{
			InitializeComponent();
		}

		protected new DialogResult Show()
		{
			return ShowDialog();
		}

		protected new DialogResult Show(IWin32Window owner)
		{
			return ShowDialog(owner);
		}
	}
}
