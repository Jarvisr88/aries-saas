namespace DevExpress.Xpf.Editors.Services
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Validation.Native;
    using System;

    public class AutoSuggestEditValidator : EditorSpecificValidator
    {
        public AutoSuggestEditValidator(AutoSuggestEdit editor) : base(editor)
        {
        }

        protected override StrategyValidatorBase CreateValidator() => 
            new AutoSuggestEditStrategyValidator((AutoSuggestEdit) base.OwnerEdit);
    }
}

