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

    public class FormatCondition : ExpressionConditionBase
    {
        public static readonly DependencyProperty ValueRuleProperty;
        public static readonly DependencyProperty Value1Property;
        public static readonly DependencyProperty Value2Property;

        static FormatCondition()
        {
            ValueRuleProperty = DependencyProperty.Register("ValueRule", typeof(ConditionRule), typeof(FormatCondition), new PropertyMetadata(ConditionRule.Expression, (d, e) => ((FormatCondition) d).OnInfoPropertyChanged(e)));
            Value1Property = DependencyProperty.Register("Value1", typeof(object), typeof(FormatCondition), new PropertyMetadata(null, (d, e) => ((FormatCondition) d).OnInfoPropertyChanged(e)));
            Value2Property = DependencyProperty.Register("Value2", typeof(object), typeof(FormatCondition), new PropertyMetadata(null, (d, e) => ((FormatCondition) d).OnInfoPropertyChanged(e)));
        }

        protected override BaseEditUnit CreateEmptyEditUnit() => 
            new ConditionEditUnit();

        internal override FormatConditionRuleBase CreateExportWrapper()
        {
            DateOccurringConditionRule rule = new DateOccurringConditionRuleDetector().DetectRule(base.Expression);
            return ((rule == DateOccurringConditionRule.None) ? ((FormatConditionRuleBase) new FormatConditionRuleValueExportWrapper(this)) : ((FormatConditionRuleBase) new FormatConditionRuleDateOccuringExportWrapper(base.Format, rule)));
        }

        protected override FormatConditionBaseInfo CreateInfo() => 
            new FormatConditionInfo();

        protected override void SyncProperty(DependencyProperty property)
        {
            base.SyncProperty(property);
            base.SyncIfNeeded(property, ValueRuleProperty, () => this.ValueInfo.ValueRule = this.ValueRule);
            base.SyncIfNeeded(property, Value1Property, () => this.ValueInfo.Value1 = this.Value1);
            base.SyncIfNeeded(property, Value2Property, () => this.ValueInfo.Value2 = this.Value2);
        }

        protected override void UpdateEditUnit(BaseEditUnit unit)
        {
            base.UpdateEditUnit(unit);
            ConditionEditUnit unit2 = unit as ConditionEditUnit;
            if (unit2 != null)
            {
                unit2.ValueRule = this.ValueRule;
                unit2.Value1 = this.Value1;
                unit2.Value2 = this.Value2;
            }
        }

        [XtraSerializableProperty]
        public ConditionRule ValueRule
        {
            get => 
                (ConditionRule) base.GetValue(ValueRuleProperty);
            set => 
                base.SetValue(ValueRuleProperty, value);
        }

        [XtraSerializableProperty]
        public object Value1
        {
            get => 
                base.GetValue(Value1Property);
            set => 
                base.SetValue(Value1Property, value);
        }

        [XtraSerializableProperty]
        public object Value2
        {
            get => 
                base.GetValue(Value2Property);
            set => 
                base.SetValue(Value2Property, value);
        }

        private FormatConditionInfo ValueInfo =>
            base.Info as FormatConditionInfo;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FormatCondition.<>c <>9 = new FormatCondition.<>c();

            internal void <.cctor>b__20_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FormatCondition) d).OnInfoPropertyChanged(e);
            }

            internal void <.cctor>b__20_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FormatCondition) d).OnInfoPropertyChanged(e);
            }

            internal void <.cctor>b__20_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FormatCondition) d).OnInfoPropertyChanged(e);
            }
        }
    }
}

