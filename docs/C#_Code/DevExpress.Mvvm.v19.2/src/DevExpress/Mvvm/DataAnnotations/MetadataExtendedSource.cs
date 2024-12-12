namespace DevExpress.Mvvm.DataAnnotations
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class MetadataExtendedSource : IEnumerable
    {
        public MetadataExtendedSource(object source = null, DevExpress.Mvvm.DataAnnotations.MetadataLocator metadataLocator = null)
        {
            this.Source = source;
            this.MetadataLocator = metadataLocator;
        }

        public IEnumerator GetEnumerator()
        {
            Func<IEnumerable, IEnumerator> evaluator = <>c.<>9__9_0;
            if (<>c.<>9__9_0 == null)
            {
                Func<IEnumerable, IEnumerator> local1 = <>c.<>9__9_0;
                evaluator = <>c.<>9__9_0 = x => x.GetEnumerator();
            }
            return (this.Source as IEnumerable).Return<IEnumerable, IEnumerator>(evaluator, (<>c.<>9__9_1 ??= () => Enumerable.Empty<object>().GetEnumerator()));
        }

        public object Source { get; private set; }

        public DevExpress.Mvvm.DataAnnotations.MetadataLocator MetadataLocator { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly MetadataExtendedSource.<>c <>9 = new MetadataExtendedSource.<>c();
            public static Func<IEnumerable, IEnumerator> <>9__9_0;
            public static Func<IEnumerator> <>9__9_1;

            internal IEnumerator <GetEnumerator>b__9_0(IEnumerable x) => 
                x.GetEnumerator();

            internal IEnumerator <GetEnumerator>b__9_1() => 
                Enumerable.Empty<object>().GetEnumerator();
        }
    }
}

