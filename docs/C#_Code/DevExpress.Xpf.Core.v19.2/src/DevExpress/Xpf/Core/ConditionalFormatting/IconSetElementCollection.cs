namespace DevExpress.Xpf.Core.ConditionalFormatting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class IconSetElementCollection : FreezableCollection<IconSetElement>
    {
        protected override Freezable CreateInstanceCore() => 
            new IconSetElementCollection();

        internal IconSetElement[] GetSortedElementsCore(ConditionalFormattingValueType valueType)
        {
            Func<IconSetElement, IconSetElement> keySelector = <>c.<>9__2_1;
            if (<>c.<>9__2_1 == null)
            {
                Func<IconSetElement, IconSetElement> local1 = <>c.<>9__2_1;
                keySelector = <>c.<>9__2_1 = x => x;
            }
            return (from x in this
                where (valueType == ConditionalFormattingValueType.Number) || ((x.Threshold >= 0.0) && (x.Threshold <= 100.0))
                select x).Distinct<IconSetElement>(IconSetElementComparer.Instance).OrderByDescending<IconSetElement, IconSetElement>(keySelector, IconSetElementComparer.Instance).ToArray<IconSetElement>();
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly IconSetElementCollection.<>c <>9 = new IconSetElementCollection.<>c();
            public static Func<IconSetElement, IconSetElement> <>9__2_1;

            internal IconSetElement <GetSortedElementsCore>b__2_1(IconSetElement x) => 
                x;
        }

        private class IconSetElementComparer : IEqualityComparer<IconSetElement>, IComparer<IconSetElement>
        {
            public static readonly IconSetElementCollection.IconSetElementComparer Instance = new IconSetElementCollection.IconSetElementComparer();

            private IconSetElementComparer()
            {
            }

            int IComparer<IconSetElement>.Compare(IconSetElement x, IconSetElement y)
            {
                int num = Comparer<double>.Default.Compare(x.Threshold, y.Threshold);
                return ((num != 0) ? num : Comparer<ThresholdComparisonType>.Default.Compare(x.ThresholdComparisonType, y.ThresholdComparisonType));
            }

            bool IEqualityComparer<IconSetElement>.Equals(IconSetElement x, IconSetElement y) => 
                (x.Threshold == y.Threshold) && (x.ThresholdComparisonType == y.ThresholdComparisonType);

            int IEqualityComparer<IconSetElement>.GetHashCode(IconSetElement obj) => 
                obj.Threshold.GetHashCode() ^ obj.ThresholdComparisonType.GetHashCode();
        }
    }
}

