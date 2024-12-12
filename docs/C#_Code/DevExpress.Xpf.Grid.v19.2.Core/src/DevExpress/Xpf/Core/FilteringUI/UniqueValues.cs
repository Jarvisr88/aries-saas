namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Data;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;

    [StructLayout(LayoutKind.Sequential)]
    internal struct UniqueValues
    {
        public static readonly Task<UniqueValues> Empty;
        public readonly Either<ReadOnlyCollection<object>, ReadOnlyCollection<Counted<object>>> Value;
        public static UniqueValues FromValues(IList<object> values) => 
            new UniqueValues(new ReadOnlyCollection<object>(values));

        public static UniqueValues FromValues(object[] values) => 
            new UniqueValues(new ReadOnlyCollection<object>(values));

        public static UniqueValues FromCountedValues(ReadOnlyCollection<Counted<object>> values) => 
            new UniqueValues(values);

        public static UniqueValues FromValuesAndCounts(ValueAndCount[] values)
        {
            Func<ValueAndCount, Counted<object>> selector = <>c.<>9__4_0;
            if (<>c.<>9__4_0 == null)
            {
                Func<ValueAndCount, Counted<object>> local1 = <>c.<>9__4_0;
                selector = <>c.<>9__4_0 = x => new Counted<object>(x.Value, x.Count);
            }
            return FromCountedValues(values.Select<ValueAndCount, Counted<object>>(selector).ToReadOnlyCollection<Counted<object>>());
        }

        private UniqueValues(Either<ReadOnlyCollection<object>, ReadOnlyCollection<Counted<object>>> value)
        {
            this.Value = value;
        }

        static UniqueValues()
        {
            Empty = Task.FromResult<UniqueValues>(FromCountedValues(EmptyArray<Counted<object>>.Instance.ToReadOnlyCollection<Counted<object>>()));
        }
        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly UniqueValues.<>c <>9 = new UniqueValues.<>c();
            public static Func<ValueAndCount, Counted<object>> <>9__4_0;

            internal Counted<object> <FromValuesAndCounts>b__4_0(ValueAndCount x) => 
                new Counted<object>(x.Value, x.Count);
        }
    }
}

