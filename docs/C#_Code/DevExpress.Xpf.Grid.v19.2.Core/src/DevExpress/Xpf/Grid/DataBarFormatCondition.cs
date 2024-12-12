namespace DevExpress.Xpf.Grid
{
    using DevExpress.Utils.Serializing;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using DevExpress.Xpf.Core.ConditionalFormatting.Printing;
    using DevExpress.Xpf.Core.ConditionalFormattingManager;
    using System;
    using System.Windows;

    public class DataBarFormatCondition : IndicatorFormatConditionBase
    {
        public static readonly DependencyProperty FormatProperty = DependencyProperty.Register("Format", typeof(DataBarFormat), typeof(DataBarFormatCondition), new PropertyMetadata(null, new PropertyChangedCallback(FormatConditionBase.OnFormatChanged), new CoerceValueCallback(FormatConditionBase.OnCoerceFreezable)));

        protected override BaseEditUnit CreateEmptyEditUnit() => 
            new DataBarEditUnit();

        internal override FormatConditionRuleBase CreateExportWrapper() => 
            new FormatConditionRuleDataBarExportWrapper(this);

        protected override FormatConditionBaseInfo CreateInfo() => 
            new DataBarFormatConditionInfo();

        protected override void UpdateEditUnit(BaseEditUnit unit)
        {
            base.UpdateEditUnit(unit);
            DataBarEditUnit unit2 = unit as DataBarEditUnit;
            if (unit2 != null)
            {
                unit2.Format = this.Format;
            }
        }

        [XtraSerializableProperty(XtraSerializationVisibility.Content)]
        public DataBarFormat Format
        {
            get => 
                (DataBarFormat) base.GetValue(FormatProperty);
            set => 
                base.SetValue(FormatProperty, value);
        }

        public override DependencyProperty FormatPropertyForBinding =>
            FormatProperty;
    }
}

