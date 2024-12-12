namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Runtime.CompilerServices;

    public class TokenItemData : EditableDataObject
    {
        private EditorColumn column;

        public EditorColumn Column
        {
            get
            {
                EditorColumn column = this.column;
                if (this.column == null)
                {
                    EditorColumn local1 = this.column;
                    column = this.column = new EditorColumn(this);
                }
                return column;
            }
        }

        public ButtonEditSettings Settings { get; set; }

        public string DisplayText { get; set; }
    }
}

