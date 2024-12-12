namespace DevExpress.Xpf.Grid
{
    using DevExpress.Utils.Serializing;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using DevExpress.Xpf.Core.ConditionalFormatting.Printing;
    using DevExpress.Xpf.Core.ConditionalFormattingManager;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class TopBottomRuleFormatCondition : ExpressionConditionBase
    {
        internal const TopBottomRule DefaultRule = TopBottomRule.AboveAverage;
        internal const double DefaultThreshold = 10.0;
        public static readonly DependencyProperty ThresholdProperty;
        public static readonly DependencyProperty RuleProperty;

        static TopBottomRuleFormatCondition()
        {
            ThresholdProperty = DependencyProperty.Register("Threshold", typeof(double), typeof(TopBottomRuleFormatCondition), new PropertyMetadata(10.0, (d, e) => ((TopBottomRuleFormatCondition) d).OnInfoPropertyChanged(e)));
            RuleProperty = DependencyProperty.Register("Rule", typeof(TopBottomRule), typeof(TopBottomRuleFormatCondition), new PropertyMetadata(TopBottomRule.AboveAverage, (d, e) => ((TopBottomRuleFormatCondition) d).OnInfoPropertyChanged(e)));
        }

        protected override BaseEditUnit CreateEmptyEditUnit() => 
            new TopBottomEditUnit();

        internal override FormatConditionRuleBase CreateExportWrapper() => 
            ((this.Rule == TopBottomRule.AboveAverage) || (this.Rule == TopBottomRule.BelowAverage)) ? ((FormatConditionRuleBase) new FormatConditionRuleAboveBelowAverageExportWrapper(this)) : ((FormatConditionRuleBase) new FormatConditionRuleTopBottomExportWrapper(this));

        protected override FormatConditionBaseInfo CreateInfo() => 
            new TopBottomRuleFormatConditionInfo();

        protected override void SyncProperty(DependencyProperty property)
        {
            base.SyncProperty(property);
            base.SyncIfNeeded(property, ThresholdProperty, () => this.TopInfo.Threshold = this.Threshold);
            base.SyncIfNeeded(property, RuleProperty, () => this.TopInfo.Rule = this.Rule);
        }

        protected override void UpdateEditUnit(BaseEditUnit unit)
        {
            base.UpdateEditUnit(unit);
            TopBottomEditUnit unit2 = unit as TopBottomEditUnit;
            if (unit2 != null)
            {
                unit2.Threshold = this.Threshold;
                unit2.Rule = this.Rule;
            }
        }

        [XtraSerializableProperty]
        public double Threshold
        {
            get => 
                (double) base.GetValue(ThresholdProperty);
            set => 
                base.SetValue(ThresholdProperty, value);
        }

        [XtraSerializableProperty]
        public TopBottomRule Rule
        {
            get => 
                (TopBottomRule) base.GetValue(RuleProperty);
            set => 
                base.SetValue(RuleProperty, value);
        }

        internal TopBottomRuleFormatConditionInfo TopInfo =>
            base.Info as TopBottomRuleFormatConditionInfo;

        protected override bool CanAttach =>
            !string.IsNullOrEmpty(base.FieldName) || (base.Expression != null);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TopBottomRuleFormatCondition.<>c <>9 = new TopBottomRuleFormatCondition.<>c();

            internal void <.cctor>b__20_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TopBottomRuleFormatCondition) d).OnInfoPropertyChanged(e);
            }

            internal void <.cctor>b__20_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TopBottomRuleFormatCondition) d).OnInfoPropertyChanged(e);
            }
        }
    }
}

