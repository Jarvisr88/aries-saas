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

    public class UniqueDuplicateRuleFormatCondition : ExpressionConditionBase
    {
        public static readonly DependencyProperty RuleProperty;
        public static readonly DependencyProperty SelectiveExpressionProperty;

        static UniqueDuplicateRuleFormatCondition()
        {
            RuleProperty = DependencyProperty.Register("Rule", typeof(UniqueDuplicateRule), typeof(UniqueDuplicateRuleFormatCondition), new PropertyMetadata(UniqueDuplicateRule.Duplicate, (d, e) => ((UniqueDuplicateRuleFormatCondition) d).OnInfoPropertyChanged(e)));
            SelectiveExpressionProperty = DependencyProperty.Register("SelectiveExpression", typeof(string), typeof(UniqueDuplicateRuleFormatCondition), new PropertyMetadata(null, (d, e) => ((UniqueDuplicateRuleFormatCondition) d).OnSelectiveExpressionChanged(e)));
        }

        protected override BaseEditUnit CreateEmptyEditUnit() => 
            new UniqueDuplicateEditUnit();

        internal override FormatConditionRuleBase CreateExportWrapper() => 
            new FormatConditionRuleUniqueDuplicateExportWrapper(this);

        protected override FormatConditionBaseInfo CreateInfo() => 
            new UniqueDuplicateRuleFormatConditionInfo();

        private void OnSelectiveExpressionChanged(DependencyPropertyChangedEventArgs e)
        {
            this.UniqueDuplicateInfo.SelectiveExpression = this.SelectiveExpression;
            base.OnChanged(e, FormatConditionChangeType.All);
        }

        protected override void SyncProperty(DependencyProperty property)
        {
            base.SyncProperty(property);
            base.SyncIfNeeded(property, RuleProperty, () => this.UniqueDuplicateInfo.Rule = this.Rule);
            base.SyncIfNeeded(property, SelectiveExpressionProperty, () => this.UniqueDuplicateInfo.SelectiveExpression = this.SelectiveExpression);
        }

        protected override void UpdateEditUnit(BaseEditUnit unit)
        {
            base.UpdateEditUnit(unit);
            UniqueDuplicateEditUnit unit2 = unit as UniqueDuplicateEditUnit;
            if (unit2 != null)
            {
                unit2.Rule = this.Rule;
            }
        }

        [XtraSerializableProperty]
        public UniqueDuplicateRule Rule
        {
            get => 
                (UniqueDuplicateRule) base.GetValue(RuleProperty);
            set => 
                base.SetValue(RuleProperty, value);
        }

        [XtraSerializableProperty]
        public string SelectiveExpression
        {
            get => 
                (string) base.GetValue(SelectiveExpressionProperty);
            set => 
                base.SetValue(SelectiveExpressionProperty, value);
        }

        private UniqueDuplicateRuleFormatConditionInfo UniqueDuplicateInfo =>
            base.Info as UniqueDuplicateRuleFormatConditionInfo;

        protected override bool CanAttach =>
            !string.IsNullOrEmpty(base.FieldName) || (base.Expression != null);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly UniqueDuplicateRuleFormatCondition.<>c <>9 = new UniqueDuplicateRuleFormatCondition.<>c();

            internal void <.cctor>b__19_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((UniqueDuplicateRuleFormatCondition) d).OnInfoPropertyChanged(e);
            }

            internal void <.cctor>b__19_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((UniqueDuplicateRuleFormatCondition) d).OnSelectiveExpressionChanged(e);
            }
        }
    }
}

