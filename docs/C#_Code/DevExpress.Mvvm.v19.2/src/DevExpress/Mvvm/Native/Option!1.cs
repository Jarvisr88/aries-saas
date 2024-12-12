namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct Option<T>
    {
        public static readonly Option<T> Empty;
        public static readonly Option<T> Default;
        public readonly bool HasValue;
        private readonly T value;
        public Option(T value)
        {
            this = (Option) new Option<T>();
            this.value = value;
            this.HasValue = true;
        }

        public T ToValue()
        {
            if (!this.HasValue)
            {
                throw new InvalidOperationException();
            }
            return this.value;
        }

        public void Match(Action<T> action, Action noValueAction)
        {
            if (this.HasValue)
            {
                action(this.value);
            }
            else
            {
                noValueAction();
            }
        }

        public TOut Match<TOut>(Func<T, TOut> getValue, Func<TOut> getNoValue) => 
            this.HasValue ? getValue(this.value) : getNoValue();

        public TOut Match<TOut>(Func<T, TOut> getValue, TOut noValue) => 
            this.HasValue ? getValue(this.value) : noValue;

        public T GetValueOrDefault(Func<T> getDefaultValue) => 
            this.HasValue ? this.value : getDefaultValue();

        public T GetValueOrDefault(T defaultValue = null) => 
            this.HasValue ? this.value : defaultValue;

        public void DoIfHasValue(Action<T> action)
        {
            if (this.HasValue)
            {
                action(this.value);
            }
        }

        public static bool operator ==(Option<T> a, Option<T> b)
        {
            bool flag = a == 0;
            bool flag2 = b == 0;
            return (!(flag & flag2) ? (!(flag | flag2) ? ((a.HasValue || b.HasValue) ? (a.HasValue && (b.HasValue && EqualityComparer<T>.Default.Equals(a.value, b.value))) : true) : false) : true);
        }

        public override int GetHashCode() => 
            !this.HasValue ? -1 : ((this.value == null) ? 0 : this.value.GetHashCode());

        public static bool operator !=(Option<T> a, Option<T> b) => 
            !(a == b);

        public override bool Equals(object obj) => 
            (obj is Option<T>) && (this == ((Option<T>) obj));

        static Option()
        {
            Option<T>.Empty = new Option<T>();
            T local = default(T);
            Option<T>.Default = new Option<T>(local);
        }
    }
}

