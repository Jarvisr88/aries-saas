namespace DMEWorks.Core
{
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;

    [DesignerGenerated]
    public class FormAutoIncrementMaintain : FormMaintain
    {
        private IContainer components;
        private bool FIsNew = true;
        private object FObjectID = null;

        public FormAutoIncrementMaintain()
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
            base.SuspendLayout();
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(0x228, 0x189);
            base.Name = "FormAutoIncrementMaintain";
            this.Text = "Form AutoIncrement Maintain";
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
                throw new Exception("Wrong state : ObjectID = null, IsNew = false");
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
            bool flag;
            int? nullable = NullableConvert.ToInt32(ID);
            if (nullable != null)
            {
                flag = this.SaveObject(nullable.Value, IsNew);
            }
            else
            {
                if (!IsNew)
                {
                    throw new Exception("Wrong state : ObjectID = null, IsNew = false");
                }
                flag = this.SaveObject(0, true);
            }
            return flag;
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
                if (!IsNew)
                {
                    throw new Exception("Wrong state : ObjectID = null, IsNew = false");
                }
                this.ValidateObject(0, true);
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

        protected override bool IsNew
        {
            get => 
                this.FIsNew;
            set => 
                this.FIsNew = value;
        }

        protected override object ObjectID
        {
            get => 
                !Versioned.IsNumeric(this.FObjectID) ? null : ((object) Conversions.ToInteger(this.FObjectID));
            set => 
                this.FObjectID = value;
        }
    }
}

