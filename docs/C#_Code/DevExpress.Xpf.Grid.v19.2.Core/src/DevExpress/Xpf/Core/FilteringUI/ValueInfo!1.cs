namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;
    using System.Runtime.CompilerServices;

    public class ValueInfo<T> : BindableFast
    {
        private T value;
        private int? count;

        public ValueInfo(T value, int? count, Func<T, string> getDisplayText)
        {
            this.value = value;
            this.count = count;
            this.<GetDisplayText>k__BackingField = () => (getDisplayText != null) ? getDisplayText(((ValueInfo<T>) this).Value) : Convert.ToString(((ValueInfo<T>) this).Value);
        }

        public T Value
        {
            get => 
                this.value;
            internal set => 
                base.SetValue<T>(ref this.value, value, "Value");
        }

        public int? Count
        {
            get => 
                this.count;
            internal set => 
                base.SetValue<int?>(ref this.count, value, "Count");
        }

        public Func<string> GetDisplayText { get; }
    }
}

