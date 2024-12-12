namespace DMEWorks.Forms
{
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class VBSelectBox : Form
    {
        private IContainer components;
        private object FValue;
        private string FDisplayMamber;
        private ICollection FValues;

        public VBSelectBox()
        {
            this.InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (0 <= this.cmbInput.SelectedIndex)
            {
                this.Value = this.cmbInput.SelectedItem;
                base.DialogResult = DialogResult.OK;
                base.Close();
            }
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
            this.cmbInput = new ComboBox();
            base.SuspendLayout();
            this.lblCaption.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.lblCaption.Location = new Point(8, 8);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new Size(0x110, 0x40);
            this.lblCaption.TabIndex = 1;
            this.lblCaption.Text = "Prompt";
            this.btnOK.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnOK.Location = new Point(0x120, 8);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(60, 0x16);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnCancel.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0x120, 40);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(60, 0x16);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.cmbInput.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
            this.cmbInput.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbInput.Location = new Point(8, 80);
            this.cmbInput.Name = "cmbInput";
            this.cmbInput.Size = new Size(320, 0x15);
            this.cmbInput.TabIndex = 0;
            base.AcceptButton = this.btnOK;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = this.btnCancel;
            base.ClientSize = new Size(0x161, 0x6d);
            base.Controls.Add(this.cmbInput);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.lblCaption);
            this.MinimumSize = new Size(300, 0x88);
            base.Name = "VBInputBox";
            this.Text = "Text";
            base.ResumeLayout(false);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.cmbInput.Items.Clear();
            this.cmbInput.DisplayMember = this.DisplayMamber;
            if (this.Values != null)
            {
                IEnumerator enumerator;
                try
                {
                    enumerator = this.Values.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        object current = enumerator.Current;
                        this.cmbInput.Items.Add(current);
                    }
                }
                finally
                {
                    if (enumerator is IDisposable)
                    {
                        (enumerator as IDisposable).Dispose();
                    }
                }
            }
            try
            {
                this.cmbInput.SelectedItem = this.Value;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                ProjectData.ClearProjectError();
            }
        }

        [field: AccessedThroughProperty("lblCaption")]
        private Label lblCaption { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnOK")]
        private Button btnOK { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnCancel")]
        private Button btnCancel { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbInput")]
        private ComboBox cmbInput { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        public string Prompt
        {
            get => 
                this.lblCaption.Text;
            set => 
                this.lblCaption.Text = value;
        }

        public object Value
        {
            get => 
                this.FValue;
            set => 
                this.FValue = value;
        }

        public string DisplayMamber
        {
            get => 
                this.FDisplayMamber;
            set => 
                this.FDisplayMamber = value;
        }

        public ICollection Values
        {
            get => 
                this.FValues;
            set => 
                this.FValues = value;
        }
    }
}

