namespace DMEWorks.Forms
{
    using DMEWorks.Core;
    using Infragistics.Win.UltraWinEditors;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class VBDateBox : Form
    {
        private IContainer components;
        private DateTime? FValue;

        public VBDateBox()
        {
            this.InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.FValue = NullableConvert.ToDateTime(this.dtbValue.Value);
            base.Close();
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
            this.lblCaption = new Label();
            this.btnOK = new Button();
            this.btnCancel = new Button();
            this.dtbValue = new UltraDateTimeEditor();
            base.SuspendLayout();
            this.lblCaption.Location = new Point(8, 8);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new Size(250, 70);
            this.lblCaption.TabIndex = 1;
            this.lblCaption.Text = "#";
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(290, 12);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(60, 0x16);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(290, 40);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(60, 0x16);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.dtbValue.Location = new Point(10, 90);
            this.dtbValue.Name = "dtbValue";
            this.dtbValue.Size = new Size(0x5c, 0x15);
            this.dtbValue.TabIndex = 0;
            base.AcceptButton = this.btnOK;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = this.btnCancel;
            base.ClientSize = new Size(0x161, 0x75);
            base.Controls.Add(this.dtbValue);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.lblCaption);
            base.Name = "VBDateBox";
            this.Text = "#";
            base.ResumeLayout(false);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.dtbValue.Value = NullableConvert.ToDb(this.FValue);
        }

        [field: AccessedThroughProperty("lblCaption")]
        private Label lblCaption { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnOK")]
        private Button btnOK { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbValue")]
        private UltraDateTimeEditor dtbValue { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnCancel")]
        private Button btnCancel { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        public string Prompt
        {
            get => 
                this.lblCaption.Text;
            set => 
                this.lblCaption.Text = value;
        }

        public DateTime? Value
        {
            get => 
                this.FValue;
            set => 
                this.FValue = value;
        }
    }
}

