namespace DevExpress.Xpf.Printing
{
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class InplaceEditorInfo : InplaceEditorInfoBase
    {
        public InplaceEditorInfo(string editorName, string category, string editorDisplayName = null) : base(editorName, editorDisplayName)
        {
            Guard.ArgumentIsNotNullOrEmpty(category, "category");
            this.Category = category;
        }

        public string Category { get; private set; }

        internal override DevExpress.Xpf.Printing.EditingFieldType EditingFieldType =>
            DevExpress.Xpf.Printing.EditingFieldType.Text;
    }
}

