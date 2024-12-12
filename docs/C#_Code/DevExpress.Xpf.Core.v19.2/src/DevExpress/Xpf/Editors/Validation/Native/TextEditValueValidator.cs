namespace DevExpress.Xpf.Editors.Validation.Native
{
    using DevExpress.Xpf.Editors;
    using System;

    public class TextEditValueValidator : StrategyValidatorBase
    {
        public TextEditValueValidator(TextEditBase editor) : base(editor)
        {
        }

        protected override bool DoValidateInternal(object value, object convertedValue, UpdateEditorSource updateSource) => 
            this.IsValidValue(value, convertedValue);

        public override string GetValidationError() => 
            EditorLocalizer.GetString(EditorStringId.MaskIncomplete);

        protected override bool IsValidValue(object value, object convertedValue) => 
            this.Editor.PropertyProvider.GetService<TextInputServiceBase>().IsValueValid(convertedValue);

        public override object ProcessConversion(object value, UpdateEditorSource updateEditorSource) => 
            this.Editor.PropertyProvider.GetService<TextInputServiceBase>().ProcessConversion(value, updateEditorSource);

        protected TextEditBase Editor =>
            base.Editor as TextEditBase;

        private TextEditPropertyProvider PropertyProvider =>
            base.PropertyProvider as TextEditPropertyProvider;
    }
}

