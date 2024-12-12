namespace DevExpress.Xpf.Editors.Services
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Validation.Native;
    using System;

    public class DateEditValidator : EditorSpecificValidator
    {
        public DateEditValidator(BaseEdit editor) : base(editor)
        {
        }

        protected override StrategyValidatorBase CreateValidator() => 
            new RangedValueValidator<DateTime>((TextEdit) base.OwnerEdit);
    }
}

