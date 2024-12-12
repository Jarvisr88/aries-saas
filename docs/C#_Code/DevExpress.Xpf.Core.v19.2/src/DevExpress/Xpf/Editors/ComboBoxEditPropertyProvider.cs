namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Internal;
    using System;
    using System.Windows;

    public class ComboBoxEditPropertyProvider : LookUpEditBasePropertyProvider
    {
        public ComboBoxEditPropertyProvider(ComboBoxEdit editor) : base(editor)
        {
        }

        public override Style GetItemContainerStyle()
        {
            if (this.Editor.ItemContainerStyle != null)
            {
                return this.Editor.ItemContainerStyle;
            }
            ISelectorEditStyleSettings styleSettings = base.StyleSettings as ISelectorEditStyleSettings;
            return styleSettings?.GetItemContainerStyle(this.Editor);
        }

        private ComboBoxEdit Editor =>
            base.Editor as ComboBoxEdit;
    }
}

