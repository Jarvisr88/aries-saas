namespace DevExpress.Xpf.Editors.Services
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Validation.Native;
    using System;

    public class SpinEditValidator : EditorSpecificValidator
    {
        public SpinEditValidator(BaseEdit editor) : base(editor)
        {
        }

        protected override StrategyValidatorBase CreateValidator() => 
            new RangedValueValidator<decimal>((TextEdit) base.OwnerEdit);
    }
}

