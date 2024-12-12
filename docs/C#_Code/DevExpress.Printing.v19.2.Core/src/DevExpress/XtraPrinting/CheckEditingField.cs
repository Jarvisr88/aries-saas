namespace DevExpress.XtraPrinting
{
    using DevExpress.Utils.Serializing;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class CheckEditingField : EditingField
    {
        internal CheckEditingField() : base(null)
        {
            this.GroupID = string.Empty;
        }

        public CheckEditingField(VisualBrick brick) : base(brick)
        {
            if (!(brick is ICheckBoxBrick))
            {
                throw new ArgumentException();
            }
            this.GroupID = string.Empty;
        }

        [DefaultValue(""), XtraSerializableProperty]
        public string GroupID { get; set; }

        public System.Windows.Forms.CheckState CheckState
        {
            get => 
                (System.Windows.Forms.CheckState) this.EditValue;
            set => 
                this.EditValue = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public override object EditValue
        {
            get => 
                base.EditValue;
            set => 
                base.EditValue = value;
        }
    }
}

