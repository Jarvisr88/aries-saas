namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public class LocalizedEnumWrapper<T>
    {
        public LocalizedEnumWrapper(T value, Func<T, string> localizer)
        {
            this.Text = localizer(value);
            this.Value = value;
        }

        public override bool Equals(object obj)
        {
            LocalizedEnumWrapper<T> wrapper = obj as LocalizedEnumWrapper<T>;
            return ((wrapper != null) ? wrapper.Value.Equals(this.Value) : false);
        }

        public override int GetHashCode() => 
            this.Value.GetHashCode();

        public string Text { get; private set; }

        public T Value { get; private set; }
    }
}

