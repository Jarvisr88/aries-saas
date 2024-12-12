namespace DevExpress.Xpf.Core.ConditionalFormatting
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Utils.Serializing;
    using DevExpress.Utils.Serializing.Helpers;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Markup;
    using System.Windows.Media;

    [ContentProperty("Elements")]
    public class IconSetFormat : IndicatorFormatBase, IXtraSupportDeserializeCollectionItem
    {
        public static readonly DependencyProperty ElementThresholdTypeProperty = DependencyProperty.Register("ElementThresholdType", typeof(ConditionalFormattingValueType), typeof(IconSetFormat), new PropertyMetadata(ConditionalFormattingValueType.Percent));
        public static readonly DependencyProperty ElementsProperty = DependencyProperty.Register("Elements", typeof(IconSetElementCollection), typeof(IconSetFormat), new PropertyMetadata(null, null, new CoerceValueCallback(IconSetFormat.OnCoerceFreezable)));
        public static readonly DependencyProperty IconSetTypeProperty = DependencyProperty.Register("IconSetType", typeof(XlCondFmtIconSetType?), typeof(IconSetFormat), new PropertyMetadata(null));
        public static readonly DependencyProperty IconVerticalAlignmentProperty = DependencyProperty.Register("IconVerticalAlignment", typeof(VerticalAlignment), typeof(IconSetFormat), new PropertyMetadata(VerticalAlignment.Center));
        private IconSetElement[] sortedElements;
        private Locker coerceElementsLocker = new Locker();

        public IconSetFormat()
        {
            this.coerceElementsLocker.DoLockedAction<IconSetElementCollection>(delegate {
                IconSetElementCollection elements;
                this.Elements = elements = new IconSetElementCollection();
                return elements;
            });
        }

        public override DataBarFormatInfo CoerceDataBarFormatInfo(DataBarFormatInfo value, FormatValueProvider provider, decimal? minValue, decimal? maxValue)
        {
            Func<IconSetElement, ImageSource> evaluator = <>c.<>9__25_0;
            if (<>c.<>9__25_0 == null)
            {
                Func<IconSetElement, ImageSource> local1 = <>c.<>9__25_0;
                evaluator = <>c.<>9__25_0 = x => x.Icon;
            }
            return DataBarFormatInfo.AddIcon(value, this.GetElement(provider, minValue, maxValue).With<IconSetElement, ImageSource>(evaluator), this.IconVerticalAlignment);
        }

        protected override Freezable CreateInstanceCore() => 
            new IconSetFormat();

        object IXtraSupportDeserializeCollectionItem.CreateCollectionItem(string propertyName, XtraItemEventArgs e)
        {
            if (propertyName != ElementsProperty.Name)
            {
                return null;
            }
            IconSetElement element = new IconSetElement();
            if ((e != null) && ((e.Item != null) && (e.Item.ChildProperties["Icon"] != null)))
            {
                string str = e.Item.ChildProperties["Icon"].Value.ToString();
                if (!string.IsNullOrEmpty(str))
                {
                    ImageSource source = new PatchingImageSourceConverter().FromString(str) as ImageSource;
                    if (source != null)
                    {
                        e.Item.ChildProperties["Icon"].Value = source;
                    }
                }
            }
            this.Elements.Add(element);
            return element;
        }

        void IXtraSupportDeserializeCollectionItem.SetIndexCollectionItem(string propertyName, XtraSetItemIndexEventArgs e)
        {
        }

        internal IconSetElement GetElement(FormatValueProvider provider, decimal? minValue, decimal? maxValue)
        {
            decimal? decimalValue = GetDecimalValue(provider.Value);
            if (decimalValue != null)
            {
                decimal num = decimalValue.Value;
                if (this.ElementThresholdType == ConditionalFormattingValueType.Percent)
                {
                    decimal? nullable2 = GetSummaryValue(provider, ConditionalFormatSummaryType.Min, minValue);
                    decimal? nullable3 = GetSummaryValue(provider, ConditionalFormatSummaryType.Max, maxValue);
                    if ((nullable2 == null) || (nullable3 == null))
                    {
                        return null;
                    }
                    num = ColorScaleFormat.GetRatio(nullable2.Value, nullable3.Value, GetNormalizedValue<decimal>(decimalValue.Value, nullable2.Value, nullable3.Value)) * 100M;
                }
                IconSetElement[] sortedElements = this.GetSortedElements();
                for (int i = sortedElements.Length - 1; i >= 0; i--)
                {
                    if (ValueFit(sortedElements[i], num) && ((i == 0) || !ValueFit(sortedElements[i - 1], num)))
                    {
                        return sortedElements[i];
                    }
                }
            }
            return null;
        }

        internal IconSetElement[] GetSortedElements()
        {
            IconSetElement[] sortedElements = this.sortedElements;
            if (this.sortedElements == null)
            {
                IconSetElement[] local1 = this.sortedElements;
                sortedElements = this.sortedElements = this.Elements.GetSortedElementsCore(this.ElementThresholdType);
            }
            return sortedElements;
        }

        protected override void OnChanged()
        {
            base.OnChanged();
            this.sortedElements = null;
        }

        private static object OnCoerceFreezable(DependencyObject d, object baseValue) => 
            !((IconSetFormat) d).coerceElementsLocker.IsLocked ? FormatConditionBaseInfo.OnCoerceFreezable(baseValue) : baseValue;

        private static bool ValueFit(IconSetElement element, decimal value)
        {
            decimal num = element.Threshold.AsDecimal();
            return ((element.ThresholdComparisonType == ThresholdComparisonType.GreaterOrEqual) ? (value >= num) : (value > num));
        }

        [XtraSerializableProperty]
        public ConditionalFormattingValueType ElementThresholdType
        {
            get => 
                (ConditionalFormattingValueType) base.GetValue(ElementThresholdTypeProperty);
            set => 
                base.SetValue(ElementThresholdTypeProperty, value);
        }

        [XtraSerializableProperty(XtraSerializationVisibility.Collection, true)]
        public IconSetElementCollection Elements
        {
            get => 
                (IconSetElementCollection) base.GetValue(ElementsProperty);
            set => 
                base.SetValue(ElementsProperty, value);
        }

        [XtraSerializableProperty]
        public XlCondFmtIconSetType? IconSetType
        {
            get => 
                (XlCondFmtIconSetType?) base.GetValue(IconSetTypeProperty);
            set => 
                base.SetValue(IconSetTypeProperty, value);
        }

        [XtraSerializableProperty]
        public VerticalAlignment IconVerticalAlignment
        {
            get => 
                (VerticalAlignment) base.GetValue(IconVerticalAlignmentProperty);
            set => 
                base.SetValue(IconVerticalAlignmentProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly IconSetFormat.<>c <>9 = new IconSetFormat.<>c();
            public static Func<IconSetElement, ImageSource> <>9__25_0;

            internal ImageSource <CoerceDataBarFormatInfo>b__25_0(IconSetElement x) => 
                x.Icon;
        }
    }
}

