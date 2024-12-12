namespace DevExpress.Xpf.Editors.Services
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Validation.Native;
    using System;

    public class PopupColorEditValidator : EditorSpecificValidator
    {
        public PopupColorEditValidator(PopupColorEdit editor) : base(editor)
        {
        }

        protected override StrategyValidatorBase CreateValidator() => 
            new DevExpress.Xpf.Editors.Validation.Native.PopupColorEditValidator((PopupColorEdit) base.OwnerEdit);
    }
}

