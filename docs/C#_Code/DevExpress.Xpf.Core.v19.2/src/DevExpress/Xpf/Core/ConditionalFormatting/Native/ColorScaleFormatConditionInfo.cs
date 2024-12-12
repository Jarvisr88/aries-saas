namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using System;

    public class ColorScaleFormatConditionInfo : IndicatorFormatConditionInfo
    {
        protected override bool AllowTransition(object oldTransitionState, object newTransitionState) => 
            !Equals(oldTransitionState, newTransitionState);

        internal Color? CalcColor(FormatValueProvider provider)
        {
            if (this.ColorScaleFormat != null)
            {
                return this.ColorScaleFormat.CalcColor(provider, base.ActualMin, base.ActualMax);
            }
            return null;
        }

        protected override object CalcFormatTransitionState(FormatValueProvider provider) => 
            this.CalcColor(provider);

        public override IConditionalAnimationFactory CreateAnimationFactory()
        {
            ColorScaleAnimationFactory factory1 = new ColorScaleAnimationFactory();
            factory1.Condition = this;
            return factory1;
        }

        protected override bool NeedFormatChangeOverride(AnimationTriggerContext context)
        {
            DataUpdate dataUpdate = context.DataUpdate;
            if (!(base.FormatCore is DevExpress.Xpf.Core.ConditionalFormatting.ColorScaleFormat))
            {
                return false;
            }
            Color? nullable3 = this.CalcColor(dataUpdate.GetOldValue(base.ActualFieldName));
            Color? nullable4 = this.CalcColor(dataUpdate.GetNewValue(base.ActualFieldName));
            return (((nullable3 != null) == (nullable4 != null)) ? ((nullable3 != null) ? (nullable3.GetValueOrDefault() != nullable4.GetValueOrDefault()) : false) : true);
        }

        public override string OwnerPredefinedFormatsPropertyName =>
            "PredefinedColorScaleFormats";

        public override ConditionalFormatMask FormatMask =>
            ConditionalFormatMask.Background;

        private DevExpress.Xpf.Core.ConditionalFormatting.ColorScaleFormat ColorScaleFormat =>
            base.FormatCore as DevExpress.Xpf.Core.ConditionalFormatting.ColorScaleFormat;
    }
}

