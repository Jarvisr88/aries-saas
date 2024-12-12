namespace DMEWorks.Forms
{
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class ControlMissingPricing : UserControl
    {
        private IContainer components;
        private readonly int F_ID;

        public ControlMissingPricing(int id)
        {
            this.F_ID = id;
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

        public void Highlight(bool state)
        {
            this.BackColor = state ? Color.LightCoral : SystemColors.Control;
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.lblSource = new Label();
            this.cmbDestination = new ComboBox();
            base.SuspendLayout();
            this.lblSource.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.lblSource.BorderStyle = BorderStyle.FixedSingle;
            this.lblSource.Location = new Point(0, 0);
            this.lblSource.Name = "lblSource";
            this.lblSource.Size = new Size(480, 0x15);
            this.lblSource.TabIndex = 0;
            this.cmbDestination.FormattingEnabled = true;
            this.cmbDestination.Location = new Point(0x18, 0x18);
            this.cmbDestination.Name = "cmbDestination";
            this.cmbDestination.Size = new Size(320, 0x15);
            this.cmbDestination.TabIndex = 1;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.cmbDestination);
            base.Controls.Add(this.lblSource);
            base.Name = "ControlMissingPricing";
            base.Size = new Size(480, 0x30);
            base.ResumeLayout(false);
        }

        [field: AccessedThroughProperty("lblSource")]
        private Label lblSource { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbDestination")]
        private ComboBox cmbDestination { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        public ComboBox Dropdown =>
            this.cmbDestination;

        public int ID =>
            this.F_ID;

        public string Source
        {
            get => 
                this.lblSource.Text;
            set => 
                this.lblSource.Text = value;
        }
    }
}

