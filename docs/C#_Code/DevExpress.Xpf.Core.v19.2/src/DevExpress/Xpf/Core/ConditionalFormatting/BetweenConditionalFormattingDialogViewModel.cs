namespace DevExpress.Xpf.Core.ConditionalFormatting
{
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm.POCO;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using DevExpress.Xpf.Core.ConditionalFormattingManager;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class BetweenConditionalFormattingDialogViewModel : ExpressionConditionalFormattingDialogViewModel
    {
        protected BetweenConditionalFormattingDialogViewModel(IFormatsOwner owner) : base(owner, ConditionalFormattingStringId.ConditionalFormatting_BetweenDialog_Title, ConditionalFormattingStringId.ConditionalFormatting_BetweenDialog_Description, ConditionalFormattingStringId.ConditionalFormatting_BetweenDialog_Connector)
        {
        }

        protected override BaseEditUnit CreateEditUnit(string fieldName)
        {
            ConditionEditUnit unit = base.CreateEditUnit(fieldName) as ConditionEditUnit;
            if ((unit != null) && (this.Value2 != null))
            {
                unit.Value2 = base.ConvertRuleValue(this.Value2);
            }
            return unit;
        }

        protected override CriteriaOperator GetCriteria(string fieldName) => 
            new BetweenOperator(fieldName, this.Value, this.Value2);

        internal override ConditionRule GetValueRule() => 
            ConditionRule.Between;

        public override void Initialize(IDialogContext context)
        {
            base.Initialize(context);
            this.Value2 = this.GetInitialValue();
        }

        public static Func<IFormatsOwner, ConditionalFormattingDialogViewModel> Factory
        {
            get
            {
                ParameterExpression expression = Expression.Parameter(typeof(IFormatsOwner), "x");
                Expression[] expressionArray1 = new Expression[] { expression };
                ParameterExpression[] parameters = new ParameterExpression[] { expression };
                return (Func<IFormatsOwner, ConditionalFormattingDialogViewModel>) ViewModelSource.Factory<IFormatsOwner, BetweenConditionalFormattingDialogViewModel>(Expression.Lambda<Func<IFormatsOwner, BetweenConditionalFormattingDialogViewModel>>(Expression.New((ConstructorInfo) methodof(BetweenConditionalFormattingDialogViewModel..ctor), (IEnumerable<Expression>) expressionArray1), parameters));
            }
        }

        public object Value2 { get; set; }

        public override DevExpress.Xpf.Core.ConditionalFormatting.ConditionValueType ConditionValueType
        {
            get
            {
                Func<DevExpress.Xpf.Core.ConditionalFormatting.ConditionValueType> dateTime = <>c.<>9__8_0;
                if (<>c.<>9__8_0 == null)
                {
                    Func<DevExpress.Xpf.Core.ConditionalFormatting.ConditionValueType> local1 = <>c.<>9__8_0;
                    dateTime = <>c.<>9__8_0 = () => DevExpress.Xpf.Core.ConditionalFormatting.ConditionValueType.RangeDateTime;
                }
                return this.SelectValue<DevExpress.Xpf.Core.ConditionalFormatting.ConditionValueType>(dateTime, <>c.<>9__8_1 ??= () => DevExpress.Xpf.Core.ConditionalFormatting.ConditionValueType.RangeNumeric, <>c.<>9__8_2 ??= () => DevExpress.Xpf.Core.ConditionalFormatting.ConditionValueType.RangeText);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BetweenConditionalFormattingDialogViewModel.<>c <>9 = new BetweenConditionalFormattingDialogViewModel.<>c();
            public static Func<ConditionValueType> <>9__8_0;
            public static Func<ConditionValueType> <>9__8_1;
            public static Func<ConditionValueType> <>9__8_2;

            internal ConditionValueType <get_ConditionValueType>b__8_0() => 
                ConditionValueType.RangeDateTime;

            internal ConditionValueType <get_ConditionValueType>b__8_1() => 
                ConditionValueType.RangeNumeric;

            internal ConditionValueType <get_ConditionValueType>b__8_2() => 
                ConditionValueType.RangeText;
        }
    }
}

