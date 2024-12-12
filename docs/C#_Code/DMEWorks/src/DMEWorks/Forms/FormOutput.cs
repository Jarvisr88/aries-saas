namespace DMEWorks.Forms
{
    using DMEWorks.Core;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class FormOutput : Form, IParameters
    {
        private IContainer components;

        public FormOutput()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.txtOutput = new TextBox();
            base.SuspendLayout();
            this.txtOutput.Dock = DockStyle.Fill;
            this.txtOutput.MaxLength = 0;
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ScrollBars = ScrollBars.Vertical;
            this.txtOutput.Size = new Size(0x124, 0x111);
            this.txtOutput.TabIndex = 0;
            this.txtOutput.Text = "";
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(0x124, 0x111);
            Control[] controls = new Control[] { this.txtOutput };
            base.Controls.AddRange(controls);
            base.Name = "FormOutput";
            this.Text = "Output";
            base.ResumeLayout(false);
        }

        protected void SetParameters(FormParameters Params)
        {
            if ((Params != null) && Params.ContainsKey("Message"))
            {
                this.txtOutput.Text = NullableConvert.ToString(Params["Message"]);
            }
        }

        [field: AccessedThroughProperty("txtOutput")]
        private TextBox txtOutput { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }
    }
}

