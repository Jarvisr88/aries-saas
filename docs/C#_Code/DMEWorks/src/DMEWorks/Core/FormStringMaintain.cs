namespace DMEWorks.Core
{
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class FormStringMaintain : FormMaintain
    {
        private IContainer components;

        public FormStringMaintain()
        {
            this.InitializeComponent();
        }

        protected virtual void ClearObject()
        {
            if (!base.DesignMode)
            {
                throw new NotImplementedException("Method is not implemented");
            }
        }

        protected virtual void CloneObject()
        {
            if (!base.DesignMode)
            {
                throw new NotImplementedException("Method is not implemented");
            }
        }

        protected virtual void DeleteObject(string Code)
        {
            if (!base.DesignMode)
            {
                throw new NotImplementedException("Method is not implemented");
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
            this.txtObjectID = new TextBox();
            this.lblObjectTypeName = new Label();
            base.tpWorkArea.SuspendLayout();
            base.SuspendLayout();
            Control[] controls = new Control[] { this.lblObjectTypeName, this.txtObjectID };
            base.tpWorkArea.Controls.AddRange(controls);
            base.tpWorkArea.Visible = true;
            this.txtObjectID.AutoSize = false;
            this.txtObjectID.Location = new Point(0x68, 8);
            this.txtObjectID.Name = "txtObjectID";
            this.txtObjectID.Size = new Size(0xd0, 0x16);
            this.txtObjectID.TabIndex = 1;
            this.txtObjectID.Text = "";
            this.lblObjectTypeName.BackColor = Color.Transparent;
            this.lblObjectTypeName.Location = new Point(8, 8);
            this.lblObjectTypeName.Name = "lblObjectTypeName";
            this.lblObjectTypeName.Size = new Size(0x58, 0x18);
            this.lblObjectTypeName.TabIndex = 0;
            this.lblObjectTypeName.Text = "Code";
            this.lblObjectTypeName.TextAlign = ContentAlignment.MiddleRight;
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(0x260, 0x195);
            base.Name = "FormStringMaintain";
            this.Text = "Form String Maintain";
            base.tpWorkArea.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        protected sealed override void IntClearObject()
        {
            this.ClearObject();
        }

        protected sealed override void IntCloneObject()
        {
            this.CloneObject();
        }

        protected sealed override void IntDeleteObject(object ID)
        {
            string str = Convert.ToString(ID, CultureInfo.InvariantCulture);
            if (str != null)
            {
                str = str.TrimEnd(new char[0]);
            }
            if (string.IsNullOrEmpty(str))
            {
                throw new Exception("Wrong state : ObjectID is empty");
            }
            this.DeleteObject(str);
        }

        protected sealed override bool IntLoadObject(object ID)
        {
            string str = Convert.ToString(ID, CultureInfo.InvariantCulture);
            if (str != null)
            {
                str = str.TrimEnd(new char[0]);
            }
            return (!string.IsNullOrEmpty(str) && this.LoadObject(str));
        }

        protected sealed override bool IntSaveObject(object ID, bool IsNew)
        {
            string str = Convert.ToString(ID, CultureInfo.InvariantCulture);
            if (str != null)
            {
                str = str.TrimEnd(new char[0]);
            }
            if (string.IsNullOrEmpty(str))
            {
                throw new Exception("Wrong state : ObjectID is empty");
            }
            return this.SaveObject(str, IsNew);
        }

        protected sealed override void IntValidateObject(object ID, bool IsNew)
        {
            string str = Convert.ToString(ID, CultureInfo.InvariantCulture);
            if (str != null)
            {
                str = str.TrimEnd(new char[0]);
            }
            if (!string.IsNullOrEmpty(str))
            {
                this.ValidateObject(str, IsNew);
            }
            else
            {
                base.ValidationErrors.SetError(this.txtObjectID, "You should enter valid code");
            }
        }

        protected virtual bool LoadObject(string Code)
        {
            bool flag;
            if (!base.DesignMode)
            {
                throw new NotImplementedException("Method is not implemented");
            }
            return flag;
        }

        protected void ProcessParameter_Code(FormParameters Params)
        {
            if (Params != null)
            {
                object key = Params["Code"];
                if (key != null)
                {
                    base.OpenObject(key);
                }
            }
        }

        protected virtual bool SaveObject(string Code, bool IsNew)
        {
            bool flag;
            if (!base.DesignMode)
            {
                throw new NotImplementedException("Method is not implemented");
            }
            return flag;
        }

        protected override void SetParameters(FormParameters Params)
        {
            base.ProcessParameter_EntityCreatedListener(Params);
            base.ProcessParameter_TabPage(Params);
            this.ProcessParameter_Code(Params);
        }

        protected virtual void ValidateObject(string Code, bool IsNew)
        {
        }

        [field: AccessedThroughProperty("txtObjectID")]
        protected virtual TextBox txtObjectID { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblObjectTypeName")]
        protected virtual Label lblObjectTypeName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        protected override bool IsNew
        {
            get => 
                this.txtObjectID.Enabled;
            set => 
                this.txtObjectID.Enabled = value;
        }

        protected override object ObjectID
        {
            get => 
                this.txtObjectID.Text;
            set
            {
                if (value is string)
                {
                    this.txtObjectID.Text = Conversions.ToString(value);
                }
                else
                {
                    this.txtObjectID.Text = "";
                }
            }
        }
    }
}

