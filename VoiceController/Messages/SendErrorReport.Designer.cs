namespace VoiceController.Messages
{
	partial class SendErrorReport
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SendErrorReport));
			this.p_Main = new System.Windows.Forms.Panel();
			this.p_EmailSender = new System.Windows.Forms.Panel();
			this.gb_EmailSender = new System.Windows.Forms.GroupBox();
			this.tb_From = new System.Windows.Forms.TextBox();
			this.lbl_From = new System.Windows.Forms.Label();
			this.tb_To = new System.Windows.Forms.TextBox();
			this.lbl_To = new System.Windows.Forms.Label();
			this.tb_Title = new System.Windows.Forms.TextBox();
			this.lbl_Title = new System.Windows.Forms.Label();
			this.btn_Send = new System.Windows.Forms.Button();
			this.p_Settings = new System.Windows.Forms.Panel();
			this.gb_Settings = new System.Windows.Forms.GroupBox();
			this.cb_ForceAuthentication = new System.Windows.Forms.ComboBox();
			this.chk_ForceAuthentication = new System.Windows.Forms.CheckBox();
			this.tb_Password = new System.Windows.Forms.TextBox();
			this.lbl_Password = new System.Windows.Forms.Label();
			this.tb_Username = new System.Windows.Forms.TextBox();
			this.lbl_Username = new System.Windows.Forms.Label();
			this.chk_Authentication = new System.Windows.Forms.CheckBox();
			this.chk_SSL = new System.Windows.Forms.CheckBox();
			this.nud_Port = new System.Windows.Forms.NumericUpDown();
			this.lbl_Port = new System.Windows.Forms.Label();
			this.tb_SMTP_Server = new System.Windows.Forms.TextBox();
			this.lbl_SMTP_Server = new System.Windows.Forms.Label();
			this.rtb_Message = new RichTextBoxTS();
			this.p_Main.SuspendLayout();
			this.p_EmailSender.SuspendLayout();
			this.gb_EmailSender.SuspendLayout();
			this.p_Settings.SuspendLayout();
			this.gb_Settings.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nud_Port)).BeginInit();
			this.SuspendLayout();
			// 
			// p_Main
			// 
			this.p_Main.Controls.Add(this.p_EmailSender);
			this.p_Main.Controls.Add(this.p_Settings);
			this.p_Main.Dock = System.Windows.Forms.DockStyle.Fill;
			this.p_Main.Location = new System.Drawing.Point(0, 0);
			this.p_Main.Name = "p_Main";
			this.p_Main.Size = new System.Drawing.Size(447, 380);
			this.p_Main.TabIndex = 1;
			// 
			// p_EmailSender
			// 
			this.p_EmailSender.Controls.Add(this.gb_EmailSender);
			this.p_EmailSender.Dock = System.Windows.Forms.DockStyle.Fill;
			this.p_EmailSender.Location = new System.Drawing.Point(0, 123);
			this.p_EmailSender.Name = "p_EmailSender";
			this.p_EmailSender.Size = new System.Drawing.Size(447, 257);
			this.p_EmailSender.TabIndex = 1;
			// 
			// gb_EmailSender
			// 
			this.gb_EmailSender.Controls.Add(this.tb_From);
			this.gb_EmailSender.Controls.Add(this.lbl_From);
			this.gb_EmailSender.Controls.Add(this.tb_To);
			this.gb_EmailSender.Controls.Add(this.lbl_To);
			this.gb_EmailSender.Controls.Add(this.tb_Title);
			this.gb_EmailSender.Controls.Add(this.lbl_Title);
			this.gb_EmailSender.Controls.Add(this.btn_Send);
			this.gb_EmailSender.Controls.Add(this.rtb_Message);
			this.gb_EmailSender.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gb_EmailSender.Location = new System.Drawing.Point(0, 0);
			this.gb_EmailSender.Name = "gb_EmailSender";
			this.gb_EmailSender.Size = new System.Drawing.Size(447, 257);
			this.gb_EmailSender.TabIndex = 0;
			this.gb_EmailSender.TabStop = false;
			this.gb_EmailSender.Text = "Send e-mail";
			// 
			// tb_From
			// 
			this.tb_From.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tb_From.Location = new System.Drawing.Point(115, 26);
			this.tb_From.Name = "tb_From";
			this.tb_From.Size = new System.Drawing.Size(320, 20);
			this.tb_From.TabIndex = 1;
			this.tb_From.Text = "error_reporting@mortens.hu";
			// 
			// lbl_From
			// 
			this.lbl_From.AutoSize = true;
			this.lbl_From.Location = new System.Drawing.Point(12, 29);
			this.lbl_From.Name = "lbl_From";
			this.lbl_From.Size = new System.Drawing.Size(41, 13);
			this.lbl_From.TabIndex = 0;
			this.lbl_From.Text = "Sender";
			// 
			// tb_To
			// 
			this.tb_To.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tb_To.Location = new System.Drawing.Point(115, 52);
			this.tb_To.Name = "tb_To";
			this.tb_To.ReadOnly = true;
			this.tb_To.Size = new System.Drawing.Size(320, 20);
			this.tb_To.TabIndex = 3;
			this.tb_To.Text = "mortens@freemail.hu";
			// 
			// lbl_To
			// 
			this.lbl_To.AutoSize = true;
			this.lbl_To.Location = new System.Drawing.Point(12, 55);
			this.lbl_To.Name = "lbl_To";
			this.lbl_To.Size = new System.Drawing.Size(52, 13);
			this.lbl_To.TabIndex = 2;
			this.lbl_To.Text = "Recipient";
			// 
			// tb_Title
			// 
			this.tb_Title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tb_Title.Location = new System.Drawing.Point(115, 78);
			this.tb_Title.Name = "tb_Title";
			this.tb_Title.Size = new System.Drawing.Size(320, 20);
			this.tb_Title.TabIndex = 5;
			this.tb_Title.Text = "Error report";
			// 
			// lbl_Title
			// 
			this.lbl_Title.AutoSize = true;
			this.lbl_Title.Location = new System.Drawing.Point(12, 81);
			this.lbl_Title.Name = "lbl_Title";
			this.lbl_Title.Size = new System.Drawing.Size(27, 13);
			this.lbl_Title.TabIndex = 4;
			this.lbl_Title.Text = "Title";
			// 
			// btn_Send
			// 
			this.btn_Send.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_Send.Location = new System.Drawing.Point(360, 222);
			this.btn_Send.Name = "btn_Send";
			this.btn_Send.Size = new System.Drawing.Size(75, 23);
			this.btn_Send.TabIndex = 7;
			this.btn_Send.Text = "Send";
			this.btn_Send.UseVisualStyleBackColor = true;
			this.btn_Send.Click += new System.EventHandler(this.btn_Send_Click);
			// 
			// p_Settings
			// 
			this.p_Settings.Controls.Add(this.gb_Settings);
			this.p_Settings.Dock = System.Windows.Forms.DockStyle.Top;
			this.p_Settings.Location = new System.Drawing.Point(0, 0);
			this.p_Settings.Name = "p_Settings";
			this.p_Settings.Size = new System.Drawing.Size(447, 123);
			this.p_Settings.TabIndex = 0;
			// 
			// gb_Settings
			// 
			this.gb_Settings.Controls.Add(this.cb_ForceAuthentication);
			this.gb_Settings.Controls.Add(this.chk_ForceAuthentication);
			this.gb_Settings.Controls.Add(this.tb_Password);
			this.gb_Settings.Controls.Add(this.lbl_Password);
			this.gb_Settings.Controls.Add(this.tb_Username);
			this.gb_Settings.Controls.Add(this.lbl_Username);
			this.gb_Settings.Controls.Add(this.chk_Authentication);
			this.gb_Settings.Controls.Add(this.chk_SSL);
			this.gb_Settings.Controls.Add(this.nud_Port);
			this.gb_Settings.Controls.Add(this.lbl_Port);
			this.gb_Settings.Controls.Add(this.tb_SMTP_Server);
			this.gb_Settings.Controls.Add(this.lbl_SMTP_Server);
			this.gb_Settings.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gb_Settings.Location = new System.Drawing.Point(0, 0);
			this.gb_Settings.Name = "gb_Settings";
			this.gb_Settings.Size = new System.Drawing.Size(447, 123);
			this.gb_Settings.TabIndex = 0;
			this.gb_Settings.TabStop = false;
			this.gb_Settings.Text = "Settings";
			// 
			// cb_ForceAuthentication
			// 
			this.cb_ForceAuthentication.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cb_ForceAuthentication.FormattingEnabled = true;
			this.cb_ForceAuthentication.Items.AddRange(new object[] {
            "Negotiate",
            "Ntlm",
            "Digest",
            "Login"});
			this.cb_ForceAuthentication.Location = new System.Drawing.Point(278, 95);
			this.cb_ForceAuthentication.Name = "cb_ForceAuthentication";
			this.cb_ForceAuthentication.Size = new System.Drawing.Size(157, 21);
			this.cb_ForceAuthentication.TabIndex = 11;
			// 
			// chk_ForceAuthentication
			// 
			this.chk_ForceAuthentication.AutoSize = true;
			this.chk_ForceAuthentication.Checked = true;
			this.chk_ForceAuthentication.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chk_ForceAuthentication.Location = new System.Drawing.Point(278, 72);
			this.chk_ForceAuthentication.Name = "chk_ForceAuthentication";
			this.chk_ForceAuthentication.Size = new System.Drawing.Size(123, 17);
			this.chk_ForceAuthentication.TabIndex = 10;
			this.chk_ForceAuthentication.Text = "Force authentication";
			this.chk_ForceAuthentication.UseVisualStyleBackColor = true;
			this.chk_ForceAuthentication.CheckedChanged += new System.EventHandler(this.chk_ForceAuthentication_CheckedChanged);
			// 
			// tb_Password
			// 
			this.tb_Password.Enabled = false;
			this.tb_Password.Location = new System.Drawing.Point(115, 95);
			this.tb_Password.Name = "tb_Password";
			this.tb_Password.PasswordChar = '*';
			this.tb_Password.Size = new System.Drawing.Size(157, 20);
			this.tb_Password.TabIndex = 8;
			// 
			// lbl_Password
			// 
			this.lbl_Password.AutoSize = true;
			this.lbl_Password.Enabled = false;
			this.lbl_Password.Location = new System.Drawing.Point(12, 98);
			this.lbl_Password.Name = "lbl_Password";
			this.lbl_Password.Size = new System.Drawing.Size(53, 13);
			this.lbl_Password.TabIndex = 7;
			this.lbl_Password.Text = "Password";
			// 
			// tb_Username
			// 
			this.tb_Username.Enabled = false;
			this.tb_Username.Location = new System.Drawing.Point(115, 69);
			this.tb_Username.Name = "tb_Username";
			this.tb_Username.Size = new System.Drawing.Size(157, 20);
			this.tb_Username.TabIndex = 6;
			// 
			// lbl_Username
			// 
			this.lbl_Username.AutoSize = true;
			this.lbl_Username.Enabled = false;
			this.lbl_Username.Location = new System.Drawing.Point(12, 72);
			this.lbl_Username.Name = "lbl_Username";
			this.lbl_Username.Size = new System.Drawing.Size(55, 13);
			this.lbl_Username.TabIndex = 5;
			this.lbl_Username.Text = "Username";
			// 
			// chk_Authentication
			// 
			this.chk_Authentication.AutoSize = true;
			this.chk_Authentication.Location = new System.Drawing.Point(15, 42);
			this.chk_Authentication.Name = "chk_Authentication";
			this.chk_Authentication.Size = new System.Drawing.Size(94, 17);
			this.chk_Authentication.TabIndex = 4;
			this.chk_Authentication.Text = "Authentication";
			this.chk_Authentication.UseVisualStyleBackColor = true;
			this.chk_Authentication.CheckedChanged += new System.EventHandler(this.chk_Authentication_CheckedChanged);
			// 
			// chk_SSL
			// 
			this.chk_SSL.AutoSize = true;
			this.chk_SSL.Location = new System.Drawing.Point(278, 42);
			this.chk_SSL.Name = "chk_SSL";
			this.chk_SSL.Size = new System.Drawing.Size(46, 17);
			this.chk_SSL.TabIndex = 9;
			this.chk_SSL.Text = "SSL";
			this.chk_SSL.UseVisualStyleBackColor = true;
			// 
			// nud_Port
			// 
			this.nud_Port.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.nud_Port.Location = new System.Drawing.Point(380, 13);
			this.nud_Port.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
			this.nud_Port.Name = "nud_Port";
			this.nud_Port.Size = new System.Drawing.Size(55, 20);
			this.nud_Port.TabIndex = 3;
			this.nud_Port.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
			// 
			// lbl_Port
			// 
			this.lbl_Port.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lbl_Port.AutoSize = true;
			this.lbl_Port.Location = new System.Drawing.Point(278, 16);
			this.lbl_Port.Name = "lbl_Port";
			this.lbl_Port.Size = new System.Drawing.Size(58, 13);
			this.lbl_Port.TabIndex = 2;
			this.lbl_Port.Text = "SMTP port";
			// 
			// tb_SMTP_Server
			// 
			this.tb_SMTP_Server.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tb_SMTP_Server.Location = new System.Drawing.Point(115, 13);
			this.tb_SMTP_Server.Name = "tb_SMTP_Server";
			this.tb_SMTP_Server.Size = new System.Drawing.Size(157, 20);
			this.tb_SMTP_Server.TabIndex = 1;
			// 
			// lbl_SMTP_Server
			// 
			this.lbl_SMTP_Server.AutoSize = true;
			this.lbl_SMTP_Server.Location = new System.Drawing.Point(12, 16);
			this.lbl_SMTP_Server.Name = "lbl_SMTP_Server";
			this.lbl_SMTP_Server.Size = new System.Drawing.Size(69, 13);
			this.lbl_SMTP_Server.TabIndex = 0;
			this.lbl_SMTP_Server.Text = "SMTP server";
			// 
			// rtb_Message
			// 
			this.rtb_Message.AcceptsTab = true;
			this.rtb_Message.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.rtb_Message.ColoringMethod = ColoringMethods.No_Coloring;
			this.rtb_Message.DetectUrls = false;
			this.rtb_Message.Location = new System.Drawing.Point(12, 104);
			this.rtb_Message.Name = "rtb_Message";
			this.rtb_Message.ScroolToLastLine = false;
			this.rtb_Message.Size = new System.Drawing.Size(423, 112);
			this.rtb_Message.TabIndex = 6;
			this.rtb_Message.Text = "";
			// 
			// SendErrorReport
			// 
			this.AcceptButton = this.btn_Send;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(447, 380);
			this.Controls.Add(this.p_Main);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(415, 407);
			this.Name = "SendErrorReport";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Error Report";
			this.Load += new System.EventHandler(this.SendErrorReport_Load);
			this.p_Main.ResumeLayout(false);
			this.p_EmailSender.ResumeLayout(false);
			this.gb_EmailSender.ResumeLayout(false);
			this.gb_EmailSender.PerformLayout();
			this.p_Settings.ResumeLayout(false);
			this.gb_Settings.ResumeLayout(false);
			this.gb_Settings.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nud_Port)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel p_Main;
		private System.Windows.Forms.Panel p_EmailSender;
		private System.Windows.Forms.GroupBox gb_EmailSender;
		private System.Windows.Forms.TextBox tb_From;
		private System.Windows.Forms.Label lbl_From;
		private System.Windows.Forms.TextBox tb_To;
		private System.Windows.Forms.Label lbl_To;
		private System.Windows.Forms.TextBox tb_Title;
		private System.Windows.Forms.Label lbl_Title;
		private System.Windows.Forms.Button btn_Send;
		private RichTextBoxTS rtb_Message;
		private System.Windows.Forms.Panel p_Settings;
		private System.Windows.Forms.GroupBox gb_Settings;
		private System.Windows.Forms.ComboBox cb_ForceAuthentication;
		private System.Windows.Forms.CheckBox chk_ForceAuthentication;
		private System.Windows.Forms.TextBox tb_Password;
		private System.Windows.Forms.Label lbl_Password;
		private System.Windows.Forms.TextBox tb_Username;
		private System.Windows.Forms.Label lbl_Username;
		private System.Windows.Forms.CheckBox chk_Authentication;
		private System.Windows.Forms.CheckBox chk_SSL;
		private System.Windows.Forms.NumericUpDown nud_Port;
		private System.Windows.Forms.Label lbl_Port;
		private System.Windows.Forms.TextBox tb_SMTP_Server;
		private System.Windows.Forms.Label lbl_SMTP_Server;
	}
}