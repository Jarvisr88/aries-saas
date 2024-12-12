namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class IconSetFormatConditionInfo : IndicatorFormatConditionInfo
    {
        protected override bool AllowTransition(object value1, object value2)
        {
            DataBarFormatInfo info2 = (DataBarFormatInfo) value2;
            return !ReferenceEquals(((DataBarFormatInfo) value1).Icon, info2.Icon);
        }

        public override IConditionalAnimationFactory CreateAnimationFactory()
        {
            IconSetAnimationFactory factory1 = new IconSetAnimationFactory();
            factory1.Condition = this;
            return factory1;
        }

        public override IEnumerable<ConditionalFormatSummaryType> GetSummaries() => 
            (this.IconFormat.ElementThresholdType != ConditionalFormattingValueType.Percent) ? Enumerable.Empty<ConditionalFormatSummaryType>() : base.GetSummaries();

        public override string OwnerPredefinedFormatsPropertyName =>
            "PredefinedIconSetFormats";

        private IconSetFormat IconFormat =>
            base.FormatCore as IconSetFormat;

        public override ConditionalFormatMask FormatMask =>
            ConditionalFormatMask.DataBarOrIcon;
    }
}

