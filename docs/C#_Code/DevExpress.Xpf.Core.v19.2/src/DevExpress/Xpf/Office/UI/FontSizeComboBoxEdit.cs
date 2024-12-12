namespace DevExpress.Xpf.Office.UI
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Settings;
    using System;

    [DXToolboxBrowsable(false)]
    public class FontSizeComboBoxEdit : ComboBoxEdit
    {
        static FontSizeComboBoxEdit()
        {
            FontSizeComboBoxEditSettings.RegisterEditor();
        }

        public FontSizeComboBoxEdit()
        {
            base.DefaultStyleKey = typeof(FontSizeComboBoxEdit);
        }

        protected internal override BaseEditSettings CreateEditorSettings() => 
            new FontSizeComboBoxEditSettings();

        private FontSizeComboBoxEditSettings InnerSettings =>
            base.Settings as FontSizeComboBoxEditSettings;

        public IOfficeFontSizeProvider OfficeFontSizeProvider
        {
            get => 
                this.InnerSettings?.OfficeFontSizeProvider;
            set
            {
                if (this.InnerSettings != null)
                {
                    this.InnerSettings.OfficeFontSizeProvider = value;
                }
            }
        }
    }
}

