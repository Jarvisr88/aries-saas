namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public abstract class ContainItemViewModel : ManagerViewModelBase
    {
        private string expressionField;

        protected ContainItemViewModel(IDialogContext context) : base(context)
        {
        }

        protected static object ExtractOperandValue(CriteriaOperator op)
        {
            OperandValue value2 = op as OperandValue;
            return value2?.Value;
        }

        protected static string ExtractProperty(CriteriaOperator op)
        {
            OperandProperty property = op as OperandProperty;
            return property?.PropertyName;
        }

        private CriteriaOperator GetCriteria() => 
            this.Operator.Factory(new OperandProperty(string.IsNullOrEmpty(this.expressionField) ? base.Context.ColumnInfo.FieldName : this.expressionField), this.GetCriteriaValues());

        private OperandValue[] GetCriteriaValues()
        {
            Func<object, OperandValue> selector = <>c.<>9__18_0;
            if (<>c.<>9__18_0 == null)
            {
                Func<object, OperandValue> local1 = <>c.<>9__18_0;
                selector = <>c.<>9__18_0 = x => new OperandValue(x);
            }
            return this.GetValues().Select<object, OperandValue>(selector).ToArray<OperandValue>();
        }

        protected IDataColumnInfo GetExpressionColumn()
        {
            IDialogContext context = base.Context;
            if (!string.IsNullOrEmpty(this.expressionField))
            {
                context = base.Context.Find(this.expressionField) ?? base.Context;
            }
            return context.ColumnInfo;
        }

        protected internal virtual object[] GetValues() => 
            new object[0];

        public virtual void InitFromCriteria(CriteriaOperator op)
        {
            this.Operator = this.Operators.First<ContainOperator>(x => x.Match(op));
            this.ProcessExtractedValues(this.Operator.Extractor(op));
        }

        public virtual void InitFromRule(ConditionEditUnit unit)
        {
            this.Operator = this.Operators.First<ContainOperator>(x => x.Match(unit.ValueRule));
            object[] values = new object[] { unit.FieldName, unit.Value1, unit.Value2 };
            this.ProcessExtractedValues(values);
        }

        protected virtual void ProcessExtractedValues(object[] values)
        {
            this.expressionField = values[0] as string;
        }

        public string Expression =>
            this.GetCriteria().ToString();

        public virtual ContainOperator Operator { get; set; }

        public IEnumerable<ContainOperator> Operators { get; protected set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ContainItemViewModel.<>c <>9 = new ContainItemViewModel.<>c();
            public static Func<object, OperandValue> <>9__18_0;

            internal OperandValue <GetCriteriaValues>b__18_0(object x) => 
                new OperandValue(x);
        }
    }
}

