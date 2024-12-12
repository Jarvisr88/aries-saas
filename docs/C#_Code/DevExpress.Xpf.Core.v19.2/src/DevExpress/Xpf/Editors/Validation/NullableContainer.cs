namespace DevExpress.Xpf.Editors.Validation
{
    using System;
    using System.Runtime.CompilerServices;

    internal class NullableContainer
    {
        public void Reset()
        {
            this.Value = null;
            this.HasValue = false;
        }

        public void SetValue(object value)
        {
            this.Value = value;
            this.HasValue = true;
        }

        public object Value { get; private set; }

        public bool HasValue { get; private set; }
    }
}

