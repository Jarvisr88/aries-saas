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
    public class VBInputBox : Form
    {
        private IContainer components;
        private string FValue;
        private static readonly object EVENT_VALIDATEVALUE = new object();

        public event ValidateValueEventHandler ValidateValue
        {
            add
            {
                base.Events.AddHandler(EVENT_VALIDATEVALUE, value);
            }
            remove
            {
                base.Events.RemoveHandler(EVENT_VALIDATEVALUE, value);
            }
            raise
            {
                ValidateValueEventHandler handler = base.Events[EVENT_VALIDATEVALUE] as ValidateValueEventHandler;
                if (handler != null)
                {
                    handler(sender, e);
                }
            }
        }

        public VBInputBox()
        {
            this.InitializeComponent();
            this.ErrorProvider1.SetIconAlignment(this.txtInput, ErrorIconAlignment.MiddleRight);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            ValidateValueEventArgs args = new ValidateValueEventArgs(this.txtInput.Text);
            try
            {
                this.raise_ValidateValue(this, args);
            }
            catch (Exception exception1)
            {
                Exception ex = exception1;
                ProjectData.SetProjectError(ex);
                Exception exception = ex;
                ProjectData.ClearProjectError();
            }
            if (args.Valid)
            {
                this.Value = this.txtInput.Text;
                base.DialogResult = DialogResult.OK;
                base.Close();
            }
            else
            {
                string message = args.Message;
                if (string.IsNullOrEmpty(message))
                {
                    message = "Value is not acceptable";
                }
                this.ErrorProvider1.SetError(this.txtInput, message);
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
            this.components = new System.ComponentModel.Container();
            this.lblCaption = new Label();
            this.btnOK = new Button();
            this.btnCancel = new Button();
            this.txtInput = new TextBox();
            this.ErrorProvider1 = new ErrorProvider(this.components);
            ((ISupportInitialize) this.ErrorProvider1).BeginInit();
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
            this.btnCancel.Location = new Point(0x120, 40);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(60, 0x16);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.txtInput.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
            this.txtInput.Location = new Point(8, 80);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new Size(320, 20);
            this.txtInput.TabIndex = 0;
            this.ErrorProvider1.ContainerControl = this;
            base.AcceptButton = this.btnOK;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = this.btnCancel;
            base.ClientSize = new Size(0x161, 0x6d);
            base.Controls.Add(this.txtInput);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.lblCaption);
            this.MinimumSize = new Size(300, 0x88);
            base.Name = "VBInputBox";
            this.Text = "Text";
            ((ISupportInitialize) this.ErrorProvider1).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.txtInput.Text = this.FValue;
        }

        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            this.ErrorProvider1.SetError(this.txtInput, "");
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

        [field: AccessedThroughProperty("txtInput")]
        private TextBox txtInput { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ErrorProvider1")]
        internal virtual ErrorProvider ErrorProvider1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        public string Prompt
        {
            get => 
                this.lblCaption.Text;
            set => 
                this.lblCaption.Text = value;
        }

        public string Value
        {
            get => 
                this.FValue;
            set => 
                this.FValue = value;
        }
    }
}

