namespace DevExpress.Xpf.Grid
{
    using DevExpress.Utils.Serializing;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using DevExpress.Xpf.Core.ConditionalFormatting.Printing;
    using DevExpress.Xpf.Core.ConditionalFormattingManager;
    using System;
    using System.Windows;

    public class ColorScaleFormatCondition : IndicatorFormatConditionBase
    {
        public static readonly DependencyProperty FormatProperty = DependencyProperty.Register("Format", typeof(ColorScaleFormat), typeof(ColorScaleFormatCondition), new PropertyMetadata(null, new PropertyChangedCallback(FormatConditionBase.OnFormatChanged), new CoerceValueCallback(FormatConditionBase.OnCoerceFreezable)));

        protected override BaseEditUnit CreateEmptyEditUnit() => 
            new ColorScaleEditUnit();

        internal override FormatConditionRuleBase CreateExportWrapper() => 
            ((this.Format == null) || (this.Format.ColorMiddle == null)) ? new FormatConditionRuleColorScale2ExportWrapper(this) : new FormatConditionRuleColorScale3ExportWrapper(this);

        protected override FormatConditionBaseInfo CreateInfo() => 
            new ColorScaleFormatConditionInfo();

        protected override void UpdateEditUnit(BaseEditUnit unit)
        {
            base.UpdateEditUnit(unit);
            ColorScaleEditUnit unit2 = unit as ColorScaleEditUnit;
            if (unit2 != null)
            {
                unit2.Format = this.Format;
            }
        }

        [XtraSerializableProperty(XtraSerializationVisibility.Content)]
        public ColorScaleFormat Format
        {
            get => 
                (ColorScaleFormat) base.GetValue(FormatProperty);
            set => 
                base.SetValue(FormatProperty, value);
        }

        public override DependencyProperty FormatPropertyForBinding =>
            FormatProperty;

        protected override bool CanAttach =>
            !string.IsNullOrEmpty(base.FieldName) || (base.Expression != null);
    }
}

