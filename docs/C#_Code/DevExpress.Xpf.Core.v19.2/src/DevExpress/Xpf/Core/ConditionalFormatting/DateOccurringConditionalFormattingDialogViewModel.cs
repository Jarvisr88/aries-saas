namespace DevExpress.Xpf.Core.ConditionalFormatting
{
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm.POCO;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using DevExpress.Xpf.Core.ConditionalFormatting.Printing;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class DateOccurringConditionalFormattingDialogViewModel : ExpressionConditionalFormattingDialogViewModel
    {
        private static readonly FunctionOperator TodayOperator = new FunctionOperator(FunctionOperatorType.LocalDateTimeToday, new CriteriaOperator[0]);

        protected DateOccurringConditionalFormattingDialogViewModel(IFormatsOwner owner) : base(owner, ConditionalFormattingStringId.ConditionalFormatting_DateOccurringDialog_Title, ConditionalFormattingStringId.ConditionalFormatting_DateOccurringDialog_Description, ConditionalFormattingStringId.ConditionalFormatting_DateOccurringDialog_Connector)
        {
            this.SelectorItems = GetFactories().ToArray<OperatorFactory>();
        }

        protected override CriteriaOperator GetCriteria(string fieldName) => 
            GetCriteria((OperatorFactory) this.Value, fieldName);

        public static CriteriaOperator GetCriteria(OperatorFactory factory, string fieldName) => 
            factory.Factory(new OperandProperty(fieldName));

        [IteratorStateMachine(typeof(<GetFactories>d__2))]
        public static IEnumerable<OperatorFactory> GetFactories()
        {
            Func<OperandProperty, CriteriaOperator> factory = <>c.<>9__2_0;
            if (<>c.<>9__2_0 == null)
            {
                Func<OperandProperty, CriteriaOperator> local1 = <>c.<>9__2_0;
                factory = <>c.<>9__2_0 = x => new FunctionOperator(FunctionOperatorType.IsOutlookIntervalYesterday, new CriteriaOperator[] { x });
            }
            yield return new OperatorFactory(ConditionalFormattingStringId.ConditionalFormatting_DateOccurringDialog_IntervalYesterday, factory, DateOccurringConditionRule.Yesterday);
            Func<OperandProperty, CriteriaOperator> factory = <>c.<>9__2_1;
            if (<>c.<>9__2_1 == null)
            {
                Func<OperandProperty, CriteriaOperator> local2 = <>c.<>9__2_1;
                factory = <>c.<>9__2_1 = x => new FunctionOperator(FunctionOperatorType.IsOutlookIntervalToday, new CriteriaOperator[] { x });
            }
            yield return new OperatorFactory(ConditionalFormattingStringId.ConditionalFormatting_DateOccurringDialog_IntervalToday, factory, DateOccurringConditionRule.Today);
            Func<OperandProperty, CriteriaOperator> factory = <>c.<>9__2_2;
            if (<>c.<>9__2_2 == null)
            {
                Func<OperandProperty, CriteriaOperator> local3 = <>c.<>9__2_2;
                factory = <>c.<>9__2_2 = x => new FunctionOperator(FunctionOperatorType.IsOutlookIntervalTomorrow, new CriteriaOperator[] { x });
            }
            yield return new OperatorFactory(ConditionalFormattingStringId.ConditionalFormatting_DateOccurringDialog_IntervalTomorrow, factory, DateOccurringConditionRule.Tomorrow);
            Func<OperandProperty, CriteriaOperator> factory = <>c.<>9__2_3;
            if (<>c.<>9__2_3 == null)
            {
                Func<OperandProperty, CriteriaOperator> local4 = <>c.<>9__2_3;
                factory = <>c.<>9__2_3 = x => new BetweenOperator(new FunctionOperator(FunctionOperatorType.DateDiffDay, new CriteriaOperator[] { x, TodayOperator }), new ConstantValue(0), new ConstantValue(6));
            }
            yield return new OperatorFactory(ConditionalFormattingStringId.ConditionalFormatting_DateOccurringDialog_IntervalInTheLast7Days, factory, DateOccurringConditionRule.InTheLast7Days);
            Func<OperandProperty, CriteriaOperator> factory = <>c.<>9__2_4;
            if (<>c.<>9__2_4 == null)
            {
                Func<OperandProperty, CriteriaOperator> local5 = <>c.<>9__2_4;
                factory = <>c.<>9__2_4 = x => new FunctionOperator(FunctionOperatorType.IsOutlookIntervalLastWeek, new CriteriaOperator[] { x });
            }
            yield return new OperatorFactory(ConditionalFormattingStringId.ConditionalFormatting_DateOccurringDialog_IntervalLastWeek, factory, DateOccurringConditionRule.LastWeek);
            Func<OperandProperty, CriteriaOperator> factory = <>c.<>9__2_5;
            if (<>c.<>9__2_5 == null)
            {
                Func<OperandProperty, CriteriaOperator> local6 = <>c.<>9__2_5;
                factory = <>c.<>9__2_5 = x => new FunctionOperator(FunctionOperatorType.IsThisWeek, new CriteriaOperator[] { x });
            }
            yield return new OperatorFactory(ConditionalFormattingStringId.ConditionalFormatting_DateOccurringDialog_IntervalThisWeek, factory, DateOccurringConditionRule.ThisWeek);
            Func<OperandProperty, CriteriaOperator> factory = <>c.<>9__2_6;
            if (<>c.<>9__2_6 == null)
            {
                Func<OperandProperty, CriteriaOperator> local7 = <>c.<>9__2_6;
                factory = <>c.<>9__2_6 = x => new FunctionOperator(FunctionOperatorType.IsOutlookIntervalNextWeek, new CriteriaOperator[] { x });
            }
            yield return new OperatorFactory(ConditionalFormattingStringId.ConditionalFormatting_DateOccurringDialog_IntervalNextWeek, factory, DateOccurringConditionRule.NextWeek);
            Func<OperandProperty, CriteriaOperator> factory = <>c.<>9__2_7;
            if (<>c.<>9__2_7 == null)
            {
                Func<OperandProperty, CriteriaOperator> local8 = <>c.<>9__2_7;
                factory = <>c.<>9__2_7 = x => new BinaryOperator(new FunctionOperator(FunctionOperatorType.DateDiffMonth, new CriteriaOperator[] { x, TodayOperator }), new ConstantValue(1), BinaryOperatorType.Equal);
            }
            yield return new OperatorFactory(ConditionalFormattingStringId.ConditionalFormatting_DateOccurringDialog_IntervalLastMonth, factory, DateOccurringConditionRule.LastMonth);
            Func<OperandProperty, CriteriaOperator> factory = <>c.<>9__2_8;
            if (<>c.<>9__2_8 == null)
            {
                Func<OperandProperty, CriteriaOperator> local9 = <>c.<>9__2_8;
                factory = <>c.<>9__2_8 = x => new FunctionOperator(FunctionOperatorType.IsThisMonth, new CriteriaOperator[] { x });
            }
            yield return new OperatorFactory(ConditionalFormattingStringId.ConditionalFormatting_DateOccurringDialog_IntervalThisMonth, factory, DateOccurringConditionRule.ThisMonth);
            Func<OperandProperty, CriteriaOperator> factory = <>c.<>9__2_9;
            if (<>c.<>9__2_9 == null)
            {
                Func<OperandProperty, CriteriaOperator> local10 = <>c.<>9__2_9;
                factory = <>c.<>9__2_9 = x => new BinaryOperator(new FunctionOperator(FunctionOperatorType.DateDiffMonth, new CriteriaOperator[] { TodayOperator, x }), new ConstantValue(1), BinaryOperatorType.Equal);
            }
            yield return new OperatorFactory(ConditionalFormattingStringId.ConditionalFormatting_DateOccurringDialog_IntervalNextMonth, factory, DateOccurringConditionRule.NextMonth);
        }

        internal override object GetInitialValue() => 
            this.SelectorItems[0];

        public OperatorFactory[] SelectorItems { get; private set; }

        public static Func<IFormatsOwner, ConditionalFormattingDialogViewModel> Factory
        {
            get
            {
                ParameterExpression expression = Expression.Parameter(typeof(IFormatsOwner), "x");
                Expression[] expressionArray1 = new Expression[] { expression };
                ParameterExpression[] parameters = new ParameterExpression[] { expression };
                return (Func<IFormatsOwner, ConditionalFormattingDialogViewModel>) ViewModelSource.Factory<IFormatsOwner, DateOccurringConditionalFormattingDialogViewModel>(Expression.Lambda<Func<IFormatsOwner, DateOccurringConditionalFormattingDialogViewModel>>(Expression.New((ConstructorInfo) methodof(DateOccurringConditionalFormattingDialogViewModel..ctor), (IEnumerable<Expression>) expressionArray1), parameters));
            }
        }

        public override DevExpress.Xpf.Core.ConditionalFormatting.ConditionValueType ConditionValueType =>
            DevExpress.Xpf.Core.ConditionalFormatting.ConditionValueType.Selector;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DateOccurringConditionalFormattingDialogViewModel.<>c <>9 = new DateOccurringConditionalFormattingDialogViewModel.<>c();
            public static Func<OperandProperty, CriteriaOperator> <>9__2_0;
            public static Func<OperandProperty, CriteriaOperator> <>9__2_1;
            public static Func<OperandProperty, CriteriaOperator> <>9__2_2;
            public static Func<OperandProperty, CriteriaOperator> <>9__2_3;
            public static Func<OperandProperty, CriteriaOperator> <>9__2_4;
            public static Func<OperandProperty, CriteriaOperator> <>9__2_5;
            public static Func<OperandProperty, CriteriaOperator> <>9__2_6;
            public static Func<OperandProperty, CriteriaOperator> <>9__2_7;
            public static Func<OperandProperty, CriteriaOperator> <>9__2_8;
            public static Func<OperandProperty, CriteriaOperator> <>9__2_9;

            internal CriteriaOperator <GetFactories>b__2_0(OperandProperty x)
            {
                CriteriaOperator[] operands = new CriteriaOperator[] { x };
                return new FunctionOperator(FunctionOperatorType.IsOutlookIntervalYesterday, operands);
            }

            internal CriteriaOperator <GetFactories>b__2_1(OperandProperty x)
            {
                CriteriaOperator[] operands = new CriteriaOperator[] { x };
                return new FunctionOperator(FunctionOperatorType.IsOutlookIntervalToday, operands);
            }

            internal CriteriaOperator <GetFactories>b__2_2(OperandProperty x)
            {
                CriteriaOperator[] operands = new CriteriaOperator[] { x };
                return new FunctionOperator(FunctionOperatorType.IsOutlookIntervalTomorrow, operands);
            }

            internal CriteriaOperator <GetFactories>b__2_3(OperandProperty x)
            {
                CriteriaOperator[] operands = new CriteriaOperator[] { x, DateOccurringConditionalFormattingDialogViewModel.TodayOperator };
                return new BetweenOperator(new FunctionOperator(FunctionOperatorType.DateDiffDay, operands), new ConstantValue(0), new ConstantValue(6));
            }

            internal CriteriaOperator <GetFactories>b__2_4(OperandProperty x)
            {
                CriteriaOperator[] operands = new CriteriaOperator[] { x };
                return new FunctionOperator(FunctionOperatorType.IsOutlookIntervalLastWeek, operands);
            }

            internal CriteriaOperator <GetFactories>b__2_5(OperandProperty x)
            {
                CriteriaOperator[] operands = new CriteriaOperator[] { x };
                return new FunctionOperator(FunctionOperatorType.IsThisWeek, operands);
            }

            internal CriteriaOperator <GetFactories>b__2_6(OperandProperty x)
            {
                CriteriaOperator[] operands = new CriteriaOperator[] { x };
                return new FunctionOperator(FunctionOperatorType.IsOutlookIntervalNextWeek, operands);
            }

            internal CriteriaOperator <GetFactories>b__2_7(OperandProperty x)
            {
                CriteriaOperator[] operands = new CriteriaOperator[] { x, DateOccurringConditionalFormattingDialogViewModel.TodayOperator };
                return new BinaryOperator(new FunctionOperator(FunctionOperatorType.DateDiffMonth, operands), new ConstantValue(1), BinaryOperatorType.Equal);
            }

            internal CriteriaOperator <GetFactories>b__2_8(OperandProperty x)
            {
                CriteriaOperator[] operands = new CriteriaOperator[] { x };
                return new FunctionOperator(FunctionOperatorType.IsThisMonth, operands);
            }

            internal CriteriaOperator <GetFactories>b__2_9(OperandProperty x)
            {
                CriteriaOperator[] operands = new CriteriaOperator[] { DateOccurringConditionalFormattingDialogViewModel.TodayOperator, x };
                return new BinaryOperator(new FunctionOperator(FunctionOperatorType.DateDiffMonth, operands), new ConstantValue(1), BinaryOperatorType.Equal);
            }
        }


        public class OperatorFactory
        {
            private readonly ConditionalFormattingStringId stringId;

            public OperatorFactory(ConditionalFormattingStringId stringId, Func<OperandProperty, CriteriaOperator> factory, DateOccurringConditionRule rule = 0)
            {
                this.stringId = stringId;
                this.Factory = factory;
                this.Rule = rule;
            }

            public override string ToString() => 
                ConditionalFormattingLocalizer.GetString(this.stringId);

            public Func<OperandProperty, CriteriaOperator> Factory { get; private set; }

            internal DateOccurringConditionRule Rule { get; private set; }
        }
    }
}

