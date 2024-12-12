namespace DevExpress.XtraPrinting
{
    using DevExpress.Utils.Serializing;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class TextEditingField : EditingField
    {
        internal TextEditingField() : this(null)
        {
        }

        public TextEditingField(VisualBrick brick) : base(brick)
        {
            this.EditorName = string.Empty;
        }

        [DefaultValue(""), XtraSerializableProperty]
        public string EditorName { get; set; }
    }
}

