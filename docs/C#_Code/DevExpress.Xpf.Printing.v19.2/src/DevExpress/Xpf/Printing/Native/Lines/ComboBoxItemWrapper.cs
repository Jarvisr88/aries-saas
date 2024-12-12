namespace DevExpress.Xpf.Printing.Native.Lines
{
    using DevExpress.XtraPrinting.Native.Lines;
    using System;
    using System.Runtime.CompilerServices;

    public class ComboBoxItemWrapper
    {
        private string text;

        public ComboBoxItemWrapper(object value, IStringConverter converter)
        {
            this.Value = value;
            this.text = converter.ConvertToString(this.Value);
        }

        public override bool Equals(object obj)
        {
            ComboBoxItemWrapper wrapper = obj as ComboBoxItemWrapper;
            return ((wrapper == null) ? base.Equals(obj) : wrapper.Value.Equals(this.Value));
        }

        public override int GetHashCode() => 
            this.Value.GetHashCode();

        public object Value { get; private set; }

        public string Text =>
            this.text;
    }
}

