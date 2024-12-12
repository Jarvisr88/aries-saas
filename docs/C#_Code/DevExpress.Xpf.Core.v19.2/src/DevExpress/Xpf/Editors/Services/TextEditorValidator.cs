namespace DevExpress.Xpf.Editors.Services
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Validation.Native;
    using System;

    public class TextEditorValidator : EditorSpecificValidator
    {
        public TextEditorValidator(TextEditBase editor) : base(editor)
        {
        }

        protected override StrategyValidatorBase CreateValidator() => 
            new TextEditValueValidator((TextEditBase) base.OwnerEdit);
    }
}

