namespace DevExpress.Xpf.Editors.Services
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Validation.Native;
    using System;
    using System.Runtime.InteropServices;

    public class TextInputValueContainerService : ValueContainerService
    {
        public TextInputValueContainerService(TextEditBase editor) : base(editor)
        {
        }

        public override object ConvertEditValueForFormatDisplayText(object convertedValue)
        {
            object obj2 = this.TextInputSettings.ConvertEditValueForFormatDisplayText(convertedValue);
            return base.ConvertEditValueForFormatDisplayText(obj2);
        }

        public override bool ProvideEditValue(object value, out object provideValue, UpdateEditorSource updateSource)
        {
            object obj2 = this.TextInputSettings.ProvideEditValue(value, updateSource);
            return base.ProvideEditValue(obj2, out provideValue, updateSource);
        }

        private TextEditPropertyProvider PropertyProvider =>
            (TextEditPropertyProvider) base.PropertyProvider;

        private TextInputSettingsBase TextInputSettings =>
            this.PropertyProvider.TextInputSettings;
    }
}

