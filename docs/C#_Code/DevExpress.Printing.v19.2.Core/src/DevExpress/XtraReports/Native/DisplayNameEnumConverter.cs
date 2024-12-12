namespace DevExpress.XtraReports.Native
{
    using DevExpress.Data.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class DisplayNameEnumConverter : EnumConverter
    {
        public DisplayNameEnumConverter(Type type) : base(type)
        {
        }

        public sealed override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) => 
            this.FindValueByDisplayName((string) value) ?? base.ConvertFrom(context, culture, value);

        public sealed override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) => 
            this.GetDisplayText((Enum) value) ?? base.ConvertTo(context, culture, value, destinationType);

        [IteratorStateMachine(typeof(<EnumerateStandardValues>d__6))]
        private IEnumerable<KeyValuePair<Enum, string>> EnumerateStandardValues()
        {
            <EnumerateStandardValues>d__6 d__1 = new <EnumerateStandardValues>d__6(-2);
            d__1.<>4__this = this;
            return d__1;
        }

        private Enum FindValueByDisplayName(string displayName)
        {
            Enum key;
            using (IEnumerator<KeyValuePair<Enum, string>> enumerator = this.EnumerateStandardValues().GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        KeyValuePair<Enum, string> current = enumerator.Current;
                        if (current.Value != displayName)
                        {
                            continue;
                        }
                        key = current.Key;
                    }
                    else
                    {
                        return 0;
                    }
                    break;
                }
            }
            return key;
        }

        protected virtual string GetDisplayText(Enum value)
        {
            FieldInfo field = base.EnumType.GetField(value.ToString());
            return ((field != null) ? new AnnotationAttributes(field.GetCustomAttributes<Attribute>()).Name : value.ToString());
        }

        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            Func<KeyValuePair<Enum, string>, Enum> selector = <>c.<>9__3_0;
            if (<>c.<>9__3_0 == null)
            {
                Func<KeyValuePair<Enum, string>, Enum> local1 = <>c.<>9__3_0;
                selector = <>c.<>9__3_0 = x => x.Key;
            }
            return new TypeConverter.StandardValuesCollection(this.EnumerateStandardValues().Select<KeyValuePair<Enum, string>, Enum>(selector).ToList<Enum>());
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DisplayNameEnumConverter.<>c <>9 = new DisplayNameEnumConverter.<>c();
            public static Func<KeyValuePair<Enum, string>, Enum> <>9__3_0;

            internal Enum <GetStandardValues>b__3_0(KeyValuePair<Enum, string> x) => 
                x.Key;
        }

        [CompilerGenerated]
        private sealed class <EnumerateStandardValues>d__6 : IEnumerable<KeyValuePair<Enum, string>>, IEnumerable, IEnumerator<KeyValuePair<Enum, string>>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private KeyValuePair<Enum, string> <>2__current;
            private int <>l__initialThreadId;
            public DisplayNameEnumConverter <>4__this;
            private IEnumerator <>7__wrap1;

            [DebuggerHidden]
            public <EnumerateStandardValues>d__6(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private void <>m__Finally1()
            {
                this.<>1__state = -1;
                IDisposable disposable = this.<>7__wrap1 as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }

            private bool MoveNext()
            {
                bool flag;
                try
                {
                    int num = this.<>1__state;
                    if (num == 0)
                    {
                        this.<>1__state = -1;
                        this.<>7__wrap1 = Enum.GetValues(this.<>4__this.EnumType).GetEnumerator();
                        this.<>1__state = -3;
                    }
                    else if (num == 1)
                    {
                        this.<>1__state = -3;
                    }
                    else
                    {
                        return false;
                    }
                    if (!this.<>7__wrap1.MoveNext())
                    {
                        this.<>m__Finally1();
                        this.<>7__wrap1 = null;
                        flag = false;
                    }
                    else
                    {
                        Enum current = (Enum) this.<>7__wrap1.Current;
                        this.<>2__current = new KeyValuePair<Enum, string>(current, this.<>4__this.ConvertToString(current));
                        this.<>1__state = 1;
                        flag = true;
                    }
                }
                fault
                {
                    this.System.IDisposable.Dispose();
                }
                return flag;
            }

            [DebuggerHidden]
            IEnumerator<KeyValuePair<Enum, string>> IEnumerable<KeyValuePair<Enum, string>>.GetEnumerator()
            {
                DisplayNameEnumConverter.<EnumerateStandardValues>d__6 d__;
                if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                else
                {
                    d__ = new DisplayNameEnumConverter.<EnumerateStandardValues>d__6(0) {
                        <>4__this = this.<>4__this
                    };
                }
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Enum,System.String>>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
                int num = this.<>1__state;
                if ((num == -3) || (num == 1))
                {
                    try
                    {
                    }
                    finally
                    {
                        this.<>m__Finally1();
                    }
                }
            }

            KeyValuePair<Enum, string> IEnumerator<KeyValuePair<Enum, string>>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

