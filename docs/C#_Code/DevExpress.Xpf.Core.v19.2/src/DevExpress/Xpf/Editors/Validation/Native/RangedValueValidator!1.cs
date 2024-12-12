namespace DevExpress.Xpf.Editors.Validation.Native
{
    using DevExpress.Xpf.Editors;
    using System;

    public class RangedValueValidator<T> : TextEditValueValidator where T: struct, IComparable
    {
        public RangedValueValidator(TextEdit editor) : base(editor)
        {
        }

        public override string GetValidationError() => 
            EditorLocalizer.GetString(EditorStringId.OutOfRange);

        protected override bool IsValidValue(object value, object convertedValue) => 
            this.Strategy.InRange(convertedValue);

        public override object ProcessConversion(object value, UpdateEditorSource updateEditorSource)
        {
            object obj2 = base.ProcessConversion(value, updateEditorSource);
            return (this.Strategy.ShouldRoundToBounds ? ((!base.Editor.AllowNullInput || !this.Strategy.IsNullValue(obj2)) ? this.Strategy.Correct(obj2) : obj2) : obj2);
        }

        private RangeEditorStrategy<T> Strategy =>
            base.Editor.EditStrategy as RangeEditorStrategy<T>;
    }
}

