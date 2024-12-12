namespace DMEWorks.Maintain
{
    using DMEWorks.Ability;
    using DMEWorks.Controls;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class ControlRegionSettings : UserControl
    {
        private IContainer components;

        public ControlRegionSettings()
        {
            this.InitializeComponent();
        }

        [DebuggerNonUserCode]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && (this.components != null))
                {
                    this.components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.txtPassword = new TextBox();
            this.lblPassword = new Label();
            this.txtUsername = new TextBox();
            this.lblUsername = new Label();
            this.txtSenderId = new TextBox();
            this.lblSenderID = new Label();
            base.SuspendLayout();
            this.txtPassword.Location = new Point(80, 0x38);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new Size(0xc0, 20);
            this.txtPassword.TabIndex = 11;
            this.lblPassword.Location = new Point(8, 0x38);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new Size(0x40, 0x15);
            this.lblPassword.TabIndex = 10;
            this.lblPassword.Text = "Password:";
            this.lblPassword.TextAlign = ContentAlignment.MiddleRight;
            this.txtUsername.Location = new Point(80, 0x20);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new Size(0xc0, 20);
            this.txtUsername.TabIndex = 9;
            this.lblUsername.Location = new Point(8, 0x20);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new Size(0x40, 0x15);
            this.lblUsername.TabIndex = 8;
            this.lblUsername.Text = "Username:";
            this.lblUsername.TextAlign = ContentAlignment.MiddleRight;
            this.txtSenderId.Location = new Point(80, 8);
            this.txtSenderId.Name = "txtSenderId";
            this.txtSenderId.Size = new Size(0xc0, 20);
            this.txtSenderId.TabIndex = 7;
            this.lblSenderID.Location = new Point(8, 8);
            this.lblSenderID.Name = "lblSenderID";
            this.lblSenderID.Size = new Size(0x40, 0x15);
            this.lblSenderID.TabIndex = 6;
            this.lblSenderID.Text = "Sender ID:";
            this.lblSenderID.TextAlign = ContentAlignment.MiddleRight;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.txtPassword);
            base.Controls.Add(this.lblPassword);
            base.Controls.Add(this.txtUsername);
            base.Controls.Add(this.lblUsername);
            base.Controls.Add(this.txtSenderId);
            base.Controls.Add(this.lblSenderID);
            base.Name = "ControlRegionSettings";
            base.Size = new Size(280, 0x58);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public void LoadFrom(EnvelopeCredentials source)
        {
            if (source != null)
            {
                this.txtSenderId.Text = source.SenderId;
                this.txtUsername.Text = source.Username;
                this.txtPassword.Text = source.Password;
            }
            else
            {
                this.txtSenderId.Clear();
                this.txtUsername.Clear();
                this.txtPassword.Clear();
            }
        }

        public EnvelopeCredentials Save()
        {
            EnvelopeCredentials credentials1 = new EnvelopeCredentials();
            credentials1.SenderId = this.txtSenderId.Text;
            credentials1.Username = this.txtUsername.Text;
            credentials1.Password = this.txtPassword.Text;
            return credentials1;
        }

        public void StartTrackingChanges(ChangesTracker tracker)
        {
            if (tracker == null)
            {
                throw new ArgumentNullException("tracker");
            }
            tracker.Subscribe(this.txtSenderId);
            tracker.Subscribe(this.txtUsername);
            tracker.Subscribe(this.txtPassword);
        }

        [field: AccessedThroughProperty("txtPassword")]
        private TextBox txtPassword { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblPassword")]
        private Label lblPassword { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtUsername")]
        private TextBox txtUsername { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblUsername")]
        private Label lblUsername { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtSenderId")]
        private TextBox txtSenderId { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblSenderID")]
        private Label lblSenderID { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }
    }
}

