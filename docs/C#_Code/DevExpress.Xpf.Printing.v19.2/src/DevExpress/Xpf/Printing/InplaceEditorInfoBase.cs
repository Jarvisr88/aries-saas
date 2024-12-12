namespace DevExpress.Xpf.Printing
{
    using DevExpress.Utils;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public abstract class InplaceEditorInfoBase
    {
        protected InplaceEditorInfoBase(string editorName, string editorDisplayName)
        {
            Guard.ArgumentIsNotNullOrEmpty(editorName, "editorName");
            this.EditorName = editorName;
            this.DisplayName = string.IsNullOrEmpty(editorDisplayName) ? editorName : editorDisplayName;
        }

        public string EditorName { get; private set; }

        public string DisplayName { get; private set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        internal abstract DevExpress.Xpf.Printing.EditingFieldType EditingFieldType { get; }
    }
}

