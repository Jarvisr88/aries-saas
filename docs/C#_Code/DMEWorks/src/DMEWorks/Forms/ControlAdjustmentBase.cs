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
    public class ControlAdjustmentBase : UserControl
    {
        private IContainer components;

        public ControlAdjustmentBase()
        {
            this.InitializeComponent();
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
            this.lblOriginal = new Label();
            this.lblCaption = new Label();
            this.txtModified = new TextBox();
            base.SuspendLayout();
            this.lblOriginal.BorderStyle = BorderStyle.Fixed3D;
            this.lblOriginal.Location = new Point(0x90, 0);
            this.lblOriginal.Name = "lblOriginal";
            this.lblOriginal.Size = new Size(0x38, 20);
            this.lblOriginal.TabIndex = 2;
            this.lblOriginal.TextAlign = ContentAlignment.MiddleRight;
            this.lblCaption.Location = new Point(0, 0);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new Size(80, 0x15);
            this.lblCaption.TabIndex = 0;
            this.lblCaption.TextAlign = ContentAlignment.MiddleRight;
            this.txtModified.BorderStyle = BorderStyle.FixedSingle;
            this.txtModified.Location = new Point(0x58, 0);
            this.txtModified.Name = "txtModified";
            this.txtModified.Size = new Size(0x38, 20);
            this.txtModified.TabIndex = 1;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.txtModified);
            base.Controls.Add(this.lblOriginal);
            base.Controls.Add(this.lblCaption);
            base.Name = "ControlAdjustmentBase";
            base.Size = new Size(200, 20);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        [field: AccessedThroughProperty("lblOriginal")]
        protected virtual Label lblOriginal { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblCaption")]
        protected virtual Label lblCaption { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtModified")]
        protected virtual TextBox txtModified { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [Category("Appearance"), DefaultValue("")]
        public string Caption
        {
            get => 
                this.lblCaption.Text;
            set => 
                this.lblCaption.Text = value;
        }

        [Category("Appearance"), DefaultValue(true)]
        public bool EditorVisible
        {
            get => 
                this.txtModified.Visible;
            set => 
                this.txtModified.Visible = value;
        }
    }
}

