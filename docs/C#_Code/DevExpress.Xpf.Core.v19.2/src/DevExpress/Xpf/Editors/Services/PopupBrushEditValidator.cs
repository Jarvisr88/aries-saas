namespace DevExpress.Xpf.Editors.Services
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Validation.Native;
    using System;

    public class PopupBrushEditValidator : EditorSpecificValidator
    {
        public PopupBrushEditValidator(PopupBrushEditBase editor) : base(editor)
        {
        }

        protected override StrategyValidatorBase CreateValidator() => 
            new DevExpress.Xpf.Editors.Validation.Native.PopupBrushEditValidator((PopupBrushEditBase) base.OwnerEdit);
    }
}

