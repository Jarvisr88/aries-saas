namespace DMEWorks.Forms
{
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class FormDisconnectionAlert : Form
    {
        private IContainer components;
        private DateTime? FDisconnectionTime;

        public FormDisconnectionAlert()
        {
            base.Load += new EventHandler(this.FormDisconnection_Load);
            this.FDisconnectionTime = null;
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

        private void FormDisconnection_Load(object sender, EventArgs e)
        {
            this.FDisconnectionTime = new DateTime?(DateTime.UtcNow.AddMinutes(1.0));
            this.Timer1.Enabled = true;
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FormDisconnectionAlert));
            this.lblText = new Label();
            this.Timer1 = new Timer(this.components);
            base.SuspendLayout();
            this.lblText.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblText.Location = new Point(8, 0x18);
            this.lblText.Name = "lblText";
            this.lblText.Size = new Size(280, 80);
            this.lblText.TabIndex = 0;
            this.lblText.Text = "DMEWorks! administrator initiated your disconnection. Please save all of your work and do logoff yourself and login once again. Overwise you will be disconnected in 00:00:00";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x127, 0x8b);
            base.ControlBox = false;
            base.Controls.Add(this.lblText);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "FormDisconnectionAlert";
            base.Opacity = 0.9;
            base.SizeGripStyle = SizeGripStyle.Hide;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Disconnect";
            base.TopMost = true;
            base.ResumeLayout(false);
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder(0x100);
            builder.Append("DMEWorks! administrator initiated your disconnection. ");
            builder.Append("Please save all of your work and do logoff yourself and login once again. ");
            builder.Append("Overwise you will be disconnected in ");
            if (this.FDisconnectionTime != null)
            {
                int num = Convert.ToInt32((this.FDisconnectionTime.Value - DateTime.UtcNow).TotalSeconds);
                if (num < 0)
                {
                    this.Timer1.Enabled = false;
                    if ((ClassGlobalObjects.frmMain != null) && !ClassGlobalObjects.frmMain.IsDisposed)
                    {
                        ClassGlobalObjects.frmMain.DoLogoff();
                    }
                    else
                    {
                        base.Close();
                    }
                }
                else
                {
                    int num2 = num % 60;
                    num /= 60;
                    int num3 = num % 60;
                    int num4 = num / 60;
                    builder.AppendFormat("{0:00}:{1:00}:{2:00}", num4, num3, num2);
                    this.lblText.Text = builder.ToString();
                }
            }
        }

        [field: AccessedThroughProperty("Timer1")]
        internal virtual Timer Timer1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblText")]
        private Label lblText { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }
    }
}

