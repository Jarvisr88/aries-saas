namespace DevExpress.Xpf.Office.UI
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class EncodingComboBoxItem
    {
        internal EncodingComboBoxItem(System.Text.Encoding encoding)
        {
            this.Encoding = encoding;
        }

        public override string ToString() => 
            !ReferenceEquals(this.Encoding, System.Text.Encoding.Unicode) ? this.Encoding.WebName : EditorLocalizer.GetString(EditorStringId.Caption_EncodingUnicode);

        public System.Text.Encoding Encoding { get; set; }
    }
}

