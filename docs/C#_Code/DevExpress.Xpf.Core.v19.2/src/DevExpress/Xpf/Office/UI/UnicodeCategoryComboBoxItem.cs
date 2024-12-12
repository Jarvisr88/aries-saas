namespace DevExpress.Xpf.Office.UI
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.CompilerServices;

    public class UnicodeCategoryComboBoxItem
    {
        private EditorStringId localizedId;

        internal UnicodeCategoryComboBoxItem(System.Globalization.UnicodeCategory? category, EditorStringId localizedId)
        {
            this.UnicodeCategory = category;
            this.localizedId = localizedId;
        }

        public override string ToString() => 
            EditorLocalizer.GetString(this.localizedId);

        public System.Globalization.UnicodeCategory? UnicodeCategory { get; set; }
    }
}

