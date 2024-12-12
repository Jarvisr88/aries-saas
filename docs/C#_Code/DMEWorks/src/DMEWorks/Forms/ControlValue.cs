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
    public class ControlValue : UserControl
    {
        private IContainer components;
        private object F_Value;
        private string F_Format;

        public ControlValue()
        {
            base.Layout += new LayoutEventHandler(this.ControlValue_Layout);
            this.F_Value = null;
            this.F_Format = "";
            this.InitializeComponent();
        }

        private void Changed()
        {
            if (this.Value is IFormattable)
            {
                this.lblValue.Text = ((IFormattable) this.Value).ToString(this.Format, null);
            }
            else if (this.Value == null)
            {
                this.lblValue.Text = "";
            }
            else
            {
                this.lblValue.Text = this.Value.ToString();
            }
        }

        private void ControlValue_Layout(object sender, LayoutEventArgs e)
        {
            this.lblCaption.Height = base.Height;
            this.lblValue.Height = base.Height;
            this.lblValue.Width = Math.Max(8, base.Width - this.lblValue.Left);
        }

        [DebuggerNonUserCode]
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
            this.lblValue = new Label();
            this.lblCaption = new Label();
            base.SuspendLayout();
            this.lblValue.BorderStyle = BorderStyle.Fixed3D;
            this.lblValue.Location = new Point(0x58, 0);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new Size(0x70, 20);
            this.lblValue.TabIndex = 2;
            this.lblValue.TextAlign = ContentAlignment.MiddleRight;
            this.lblCaption.Location = new Point(0, 0);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new Size(80, 0x15);
            this.lblCaption.TabIndex = 0;
            this.lblCaption.TextAlign = ContentAlignment.MiddleRight;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.lblValue);
            base.Controls.Add(this.lblCaption);
            base.Name = "ControlValue";
            base.Size = new Size(200, 20);
            base.ResumeLayout(false);
        }

        [field: AccessedThroughProperty("lblValue")]
        private Label lblValue { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblCaption")]
        private Label lblCaption { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [Category("Appearance"), DefaultValue("")]
        public string Caption
        {
            get => 
                this.lblCaption.Text;
            set => 
                this.lblCaption.Text = value;
        }

        [DefaultValue(null)]
        public object Value
        {
            get => 
                this.F_Value;
            set
            {
                this.F_Value = value;
                this.Changed();
            }
        }

        [Category("Appearance"), DefaultValue("")]
        public string Format
        {
            get => 
                this.F_Format;
            set
            {
                this.F_Format = value;
                this.Changed();
            }
        }

        [Category("Appearance"), DefaultValue(0x40)]
        public ContentAlignment ValueAlign
        {
            get => 
                this.lblValue.TextAlign;
            set => 
                this.lblValue.TextAlign = value;
        }
    }
}

