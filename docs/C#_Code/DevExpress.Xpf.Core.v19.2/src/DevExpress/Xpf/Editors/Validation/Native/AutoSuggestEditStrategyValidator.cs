namespace DevExpress.Xpf.Editors.Validation.Native
{
    using DevExpress.Xpf.Editors;
    using System;

    public class AutoSuggestEditStrategyValidator : StrategyValidatorBase
    {
        public AutoSuggestEditStrategyValidator(AutoSuggestEdit editor) : base(editor)
        {
        }

        public override object ProcessConversion(object value, UpdateEditorSource updateEditorSource) => 
            this.PropertyProvider.TextInputSettings.ProvideEditValue(value, updateEditorSource);

        private AutoSuggestEdit Editor =>
            base.Editor as AutoSuggestEdit;

        private AutoSuggestEditPropertyProvider PropertyProvider =>
            this.Editor.PropertyProvider as AutoSuggestEditPropertyProvider;
    }
}

