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
    public class ControlCredentials : UserControl
    {
        private IContainer components;

        public ControlCredentials()
        {
            this.InitializeComponent();
            this.txtPassword.UseSystemPasswordChar = true;
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
            this.txtUsername = new TextBox();
            this.lblPassword = new Label();
            this.lblUsername = new Label();
            base.SuspendLayout();
            this.txtPassword.Location = new Point(80, 0x20);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new Size(0xc0, 20);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtUsername.Location = new Point(80, 8);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new Size(0xc0, 20);
            this.txtUsername.TabIndex = 1;
            this.lblPassword.Location = new Point(8, 0x20);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new Size(0x40, 0x15);
            this.lblPassword.TabIndex = 2;
            this.lblPassword.Text = "Password:";
            this.lblPassword.TextAlign = ContentAlignment.MiddleRight;
            this.lblUsername.Location = new Point(8, 8);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new Size(0x40, 0x15);
            this.lblUsername.TabIndex = 0;
            this.lblUsername.Text = "Username:";
            this.lblUsername.TextAlign = ContentAlignment.MiddleRight;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.txtPassword);
            base.Controls.Add(this.txtUsername);
            base.Controls.Add(this.lblPassword);
            base.Controls.Add(this.lblUsername);
            base.Name = "ControlCredentials";
            base.Size = new Size(280, 0x40);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public void LoadFrom(Credentials source)
        {
            if (source != null)
            {
                this.txtUsername.Text = source.Username;
                this.txtPassword.Text = source.Password;
            }
            else
            {
                this.txtUsername.Clear();
                this.txtPassword.Clear();
            }
        }

        public Credentials Save()
        {
            Credentials credentials1 = new Credentials();
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
            tracker.Subscribe(this.txtUsername);
            tracker.Subscribe(this.txtPassword);
        }

        public void ValidateObject(ErrorProvider ValidationErrors, ErrorProvider ValidationWarnings)
        {
            if (!string.IsNullOrWhiteSpace(this.txtUsername.Text) && string.IsNullOrWhiteSpace(this.txtPassword.Text))
            {
                ValidationWarnings.SetError(this.txtPassword, "Have you forgot to enter paasword?");
            }
        }

        [field: AccessedThroughProperty("txtPassword")]
        private TextBox txtPassword { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtUsername")]
        private TextBox txtUsername { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblPassword")]
        private Label lblPassword { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblUsername")]
        private Label lblUsername { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }
    }
}

