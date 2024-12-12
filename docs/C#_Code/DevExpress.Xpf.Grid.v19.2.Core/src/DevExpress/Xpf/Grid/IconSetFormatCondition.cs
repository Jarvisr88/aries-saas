namespace DevExpress.Xpf.Grid
{
    using DevExpress.Utils.Serializing;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using DevExpress.Xpf.Core.ConditionalFormatting.Printing;
    using DevExpress.Xpf.Core.ConditionalFormattingManager;
    using System;
    using System.Windows;

    public class IconSetFormatCondition : IndicatorFormatConditionBase
    {
        public static readonly DependencyProperty FormatProperty = DependencyProperty.Register("Format", typeof(IconSetFormat), typeof(IconSetFormatCondition), new PropertyMetadata(null, new PropertyChangedCallback(FormatConditionBase.OnFormatChanged), new CoerceValueCallback(FormatConditionBase.OnCoerceFreezable)));

        protected override BaseEditUnit CreateEmptyEditUnit() => 
            new IconSetEditUnit();

        internal override FormatConditionRuleBase CreateExportWrapper() => 
            new FormatConditionRuleIconSetExportWrapper(this);

        protected override FormatConditionBaseInfo CreateInfo() => 
            new IconSetFormatConditionInfo();

        protected override void UpdateEditUnit(BaseEditUnit unit)
        {
            base.UpdateEditUnit(unit);
            IconSetEditUnit unit2 = unit as IconSetEditUnit;
            if (unit2 != null)
            {
                unit2.Format = this.Format;
            }
        }

        [XtraSerializableProperty(XtraSerializationVisibility.Content)]
        public IconSetFormat Format
        {
            get => 
                (IconSetFormat) base.GetValue(FormatProperty);
            set => 
                base.SetValue(FormatProperty, value);
        }

        public override DependencyProperty FormatPropertyForBinding =>
            FormatProperty;
    }
}

