namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;

    public class DataUpdateConditionInfo : FormatConditionBaseInfo
    {
        public DataUpdateConditionInfo()
        {
            base.AllowConditionalAnimation = true;
        }

        public override Brush CoerceBackground(Brush value, FormatValueProvider provider) => 
            value;

        public override DataBarFormatInfo CoerceDataBarFormatInfo(DataBarFormatInfo value, FormatValueProvider provider) => 
            value;

        public override IConditionalAnimationFactory CreateAnimationFactory()
        {
            ConditionalAnimationFactory factory1 = new ConditionalAnimationFactory();
            factory1.Condition = this;
            factory1.UseTriggerIcons = true;
            return factory1;
        }

        internal override AnimationSettingsBase CreateDefaultAnimationSettings(DefaultAnimationSettings? settings)
        {
            DataUpdateAnimationSettings settings2 = new DataUpdateAnimationSettings();
            if (settings != null)
            {
                DefaultAnimationSettings settings3 = settings.Value;
                settings2.ShowDuration = settings3.TriggerAnimationShowDuration;
                settings2.HoldDuration = settings3.TriggerAnimationHoldDuration;
                settings2.HideDuration = settings3.TriggerAnimationHideDuration;
            }
            return settings2;
        }

        public override IEnumerable<ConditionalFormatSummaryType> GetSummaries() => 
            Enumerable.Empty<ConditionalFormatSummaryType>();

        internal static bool IsCustomAnimationSettings(AnimationSettingsBase settings) => 
            (settings != null) && ((settings.AnimationTimelines != null) && (settings.AnimationTimelines.Count > 0));

        private bool NeedFormatChangeAuto(FormatValueProvider oldProvider, FormatValueProvider newProvider)
        {
            if (this.Rule == DataUpdateRule.Never)
            {
                return false;
            }
            if (this.Rule == DataUpdateRule.Always)
            {
                return !Equals(oldProvider.Value, newProvider.Value);
            }
            if ((this.Rule != DataUpdateRule.Decrease) && (this.Rule != DataUpdateRule.Increase))
            {
                return false;
            }
            IComparable comparable = oldProvider.Value as IComparable;
            IComparable comparable2 = newProvider.Value as IComparable;
            if ((comparable == null) || (comparable2 == null))
            {
                return false;
            }
            int num = comparable.CompareTo(comparable2);
            return (((num >= 0) || (this.Rule != DataUpdateRule.Increase)) ? ((num > 0) && (this.Rule == DataUpdateRule.Decrease)) : true);
        }

        private bool NeedFormatChangeCustom(FormatValueProvider oldProvider, FormatValueProvider newProvider, Func<CustomDataUpdateFormatConditionEventArgsSource, bool> raiseCustomDataUpdateFormatCondition)
        {
            CustomDataUpdateFormatConditionEventArgsSource arg = new CustomDataUpdateFormatConditionEventArgsSource(oldProvider, newProvider, this);
            return ((raiseCustomDataUpdateFormatCondition == null) ? false : raiseCustomDataUpdateFormatCondition(arg));
        }

        protected override bool NeedFormatChangeOverride(AnimationTriggerContext context)
        {
            DataUpdate dataUpdate = context.DataUpdate;
            FormatValueProvider oldValue = dataUpdate.GetOldValue(base.ActualFieldName);
            FormatValueProvider newValue = dataUpdate.GetNewValue(base.ActualFieldName);
            return ((this.Rule != DataUpdateRule.Custom) ? this.NeedFormatChangeAuto(oldValue, newValue) : this.NeedFormatChangeCustom(oldValue, newValue, context.RaiseCustomDataUpdateFormatCondition));
        }

        protected DevExpress.Xpf.Core.ConditionalFormatting.Format Format =>
            base.FormatCore as DevExpress.Xpf.Core.ConditionalFormatting.Format;

        public DataUpdateRule Rule { get; set; }

        public override ConditionalFormatMask FormatMask =>
            !IsCustomAnimationSettings(base.AnimationSettings) ? ((this.Format != null) ? this.Format.FormatMask : ConditionalFormatMask.None) : (ConditionalFormatMask.TextDecorations | ConditionalFormatMask.FontWeight | ConditionalFormatMask.FontStretch | ConditionalFormatMask.FontFamily | ConditionalFormatMask.FontStyle | ConditionalFormatMask.FontSize | ConditionalFormatMask.Foreground | ConditionalFormatMask.Background | ConditionalFormatMask.DataBarOrIcon);

        public override string OwnerPredefinedFormatsPropertyName =>
            "PredefinedFormats";
    }
}

