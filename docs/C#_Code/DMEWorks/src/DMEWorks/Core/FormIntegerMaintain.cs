namespace DMEWorks.Core
{
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class FormIntegerMaintain : FormMaintain
    {
        private IContainer components;

        public FormIntegerMaintain()
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

        protected virtual void DeleteObject(int ID)
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
            this.lblObjectID = new Label();
            base.tpWorkArea.SuspendLayout();
            base.SuspendLayout();
            base.tpWorkArea.Controls.Add(this.lblObjectID);
            base.tpWorkArea.Controls.Add(this.txtObjectID);
            base.tpWorkArea.Name = "tpWorkArea";
            base.tpWorkArea.Size = new Size(0x220, 0x146);
            base.tpWorkArea.Visible = true;
            this.txtObjectID.AutoSize = false;
            this.txtObjectID.Location = new Point(80, 8);
            this.txtObjectID.Name = "txtObjectID";
            this.txtObjectID.Size = new Size(120, 0x15);
            this.txtObjectID.TabIndex = 1;
            this.txtObjectID.Text = "";
            this.lblObjectID.BackColor = Color.Transparent;
            this.lblObjectID.Location = new Point(8, 8);
            this.lblObjectID.Name = "lblObjectID";
            this.lblObjectID.Size = new Size(0x40, 0x15);
            this.lblObjectID.TabIndex = 0;
            this.lblObjectID.Text = "ID";
            this.lblObjectID.TextAlign = ContentAlignment.MiddleRight;
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(0x228, 0x189);
            base.Name = "FormIntegerMaintain";
            this.Text = "Form Integer Maintain";
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
            int? nullable = NullableConvert.ToInt32(ID);
            if (nullable == null)
            {
                throw new Exception("Wrong state : ObjectID is not valid number");
            }
            this.DeleteObject(nullable.Value);
        }

        protected sealed override bool IntLoadObject(object ID)
        {
            int? nullable = NullableConvert.ToInt32(ID);
            return ((nullable != null) && this.LoadObject(nullable.Value));
        }

        protected sealed override bool IntSaveObject(object ID, bool IsNew)
        {
            int? nullable = NullableConvert.ToInt32(ID);
            if (nullable == null)
            {
                throw new Exception("Wrong state : ObjectID is not valid number");
            }
            return this.SaveObject(nullable.Value, IsNew);
        }

        protected sealed override void IntValidateObject(object ID, bool IsNew)
        {
            int? nullable = NullableConvert.ToInt32(ID);
            if (nullable != null)
            {
                this.ValidateObject(nullable.Value, IsNew);
            }
            else
            {
                base.ValidationErrors.SetError(this.txtObjectID, "You should enter valid number");
            }
        }

        protected virtual bool LoadObject(int ID)
        {
            bool flag;
            if (!base.DesignMode)
            {
                throw new NotImplementedException("Method is not implemented");
            }
            return flag;
        }

        protected void ProcessParameter_ID(FormParameters Params)
        {
            if (Params != null)
            {
                object key = Params["ID"];
                if (key != null)
                {
                    base.OpenObject(key);
                }
            }
        }

        protected virtual bool SaveObject(int ID, bool IsNew)
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
            this.ProcessParameter_ID(Params);
        }

        protected virtual void ValidateObject(int ID, bool IsNew)
        {
        }

        [field: AccessedThroughProperty("txtObjectID")]
        protected virtual TextBox txtObjectID { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblObjectID")]
        protected virtual Label lblObjectID { get; [MethodImpl(MethodImplOptions.Synchronized)]
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
                !Versioned.IsNumeric(this.txtObjectID.Text) ? null : ((object) Conversions.ToInteger(this.txtObjectID.Text));
            set
            {
                if (Versioned.IsNumeric(value))
                {
                    this.txtObjectID.Text = Conversions.ToInteger(value).ToString();
                }
                else
                {
                    this.txtObjectID.Text = "";
                }
            }
        }
    }
}

