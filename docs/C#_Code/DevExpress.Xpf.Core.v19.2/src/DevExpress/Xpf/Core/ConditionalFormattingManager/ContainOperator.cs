namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using System;

    public class ContainOperator : ContainOperatorBase<ConditionRule>
    {
        public ContainOperator(string name, ConditionRule rule, Func<OperandProperty, OperandValue[], CriteriaOperator> factory, Func<CriteriaOperator, object[]> extractor) : base(name, rule, factory, extractor)
        {
        }

        public virtual bool Match(ConditionRule rule) => 
            (((ConditionRule) base.Rule) == rule) && (rule != ConditionRule.Expression);
    }
}

