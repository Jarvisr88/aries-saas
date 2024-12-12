namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using System;

    public class DataBarFormatConditionInfo : IndicatorFormatConditionInfo
    {
        protected override bool AllowTransition(object value1, object value2)
        {
            DataBarFormatInfo info = (DataBarFormatInfo) value1;
            DataBarFormatInfo info2 = (DataBarFormatInfo) value2;
            return ((info.ValuePosition != info2.ValuePosition) && (info.ZeroPosition == info2.ZeroPosition));
        }

        public override IConditionalAnimationFactory CreateAnimationFactory()
        {
            DataBarAnimationFactory factory1 = new DataBarAnimationFactory();
            factory1.Condition = this;
            return factory1;
        }

        public override string OwnerPredefinedFormatsPropertyName =>
            "PredefinedDataBarFormats";

        public override ConditionalFormatMask FormatMask =>
            ConditionalFormatMask.DataBarOrIcon;
    }
}

