namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using System;
    using System.Runtime.CompilerServices;

    public static class FormatDisplayInfoHelper
    {
        public static CriteriaOperator GetCriteria(ConditionRule rule, string expression, string propertyName, object value1, object value2)
        {
            if (rule == ConditionRule.Expression)
            {
                return CriteriaOperator.TryParse(expression, new object[0]);
            }
            OperandValue[] valueArray1 = new OperandValue[] { new OperandValue(value1), new OperandValue(value2) };
            return GetExpressionFactory(rule)(new OperandProperty(propertyName), valueArray1);
        }

        public static Func<OperandProperty, OperandValue[], CriteriaOperator> GetExpressionFactory(ConditionRule rule)
        {
            switch (rule)
            {
                case ConditionRule.None:
                    return (<>c.<>9__1_8 ??= (_, __) => new OperandValue(true));

                case ConditionRule.Equal:
                    return (<>c.<>9__1_2 ??= (op, ov) => new BinaryOperator(op, ov[0], BinaryOperatorType.Equal));

                case ConditionRule.NotEqual:
                    return (<>c.<>9__1_3 ??= (op, ov) => new BinaryOperator(op, ov[0], BinaryOperatorType.NotEqual));

                case ConditionRule.Between:
                    return (<>c.<>9__1_0 ??= (op, ov) => new BetweenOperator(op, ov[0], ov[1]));

                case ConditionRule.NotBetween:
                    return (<>c.<>9__1_1 ??= (op, ov) => new BetweenOperator(op, ov[0], ov[1]).Not());

                case ConditionRule.Less:
                    return (<>c.<>9__1_5 ??= (op, ov) => new BinaryOperator(op, ov[0], BinaryOperatorType.Less));

                case ConditionRule.Greater:
                    return (<>c.<>9__1_4 ??= (op, ov) => new BinaryOperator(op, ov[0], BinaryOperatorType.Greater));

                case ConditionRule.GreaterOrEqual:
                    return (<>c.<>9__1_6 ??= (op, ov) => new BinaryOperator(op, ov[0], BinaryOperatorType.GreaterOrEqual));

                case ConditionRule.LessOrEqual:
                    return (<>c.<>9__1_7 ??= (op, ov) => new BinaryOperator(op, ov[0], BinaryOperatorType.LessOrEqual));
            }
            throw new InvalidOperationException();
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FormatDisplayInfoHelper.<>c <>9 = new FormatDisplayInfoHelper.<>c();
            public static Func<OperandProperty, OperandValue[], CriteriaOperator> <>9__1_0;
            public static Func<OperandProperty, OperandValue[], CriteriaOperator> <>9__1_1;
            public static Func<OperandProperty, OperandValue[], CriteriaOperator> <>9__1_2;
            public static Func<OperandProperty, OperandValue[], CriteriaOperator> <>9__1_3;
            public static Func<OperandProperty, OperandValue[], CriteriaOperator> <>9__1_4;
            public static Func<OperandProperty, OperandValue[], CriteriaOperator> <>9__1_5;
            public static Func<OperandProperty, OperandValue[], CriteriaOperator> <>9__1_6;
            public static Func<OperandProperty, OperandValue[], CriteriaOperator> <>9__1_7;
            public static Func<OperandProperty, OperandValue[], CriteriaOperator> <>9__1_8;

            internal CriteriaOperator <GetExpressionFactory>b__1_0(OperandProperty op, OperandValue[] ov) => 
                new BetweenOperator(op, ov[0], ov[1]);

            internal CriteriaOperator <GetExpressionFactory>b__1_1(OperandProperty op, OperandValue[] ov) => 
                new BetweenOperator(op, ov[0], ov[1]).Not();

            internal CriteriaOperator <GetExpressionFactory>b__1_2(OperandProperty op, OperandValue[] ov) => 
                new BinaryOperator(op, ov[0], BinaryOperatorType.Equal);

            internal CriteriaOperator <GetExpressionFactory>b__1_3(OperandProperty op, OperandValue[] ov) => 
                new BinaryOperator(op, ov[0], BinaryOperatorType.NotEqual);

            internal CriteriaOperator <GetExpressionFactory>b__1_4(OperandProperty op, OperandValue[] ov) => 
                new BinaryOperator(op, ov[0], BinaryOperatorType.Greater);

            internal CriteriaOperator <GetExpressionFactory>b__1_5(OperandProperty op, OperandValue[] ov) => 
                new BinaryOperator(op, ov[0], BinaryOperatorType.Less);

            internal CriteriaOperator <GetExpressionFactory>b__1_6(OperandProperty op, OperandValue[] ov) => 
                new BinaryOperator(op, ov[0], BinaryOperatorType.GreaterOrEqual);

            internal CriteriaOperator <GetExpressionFactory>b__1_7(OperandProperty op, OperandValue[] ov) => 
                new BinaryOperator(op, ov[0], BinaryOperatorType.LessOrEqual);

            internal CriteriaOperator <GetExpressionFactory>b__1_8(OperandProperty _, OperandValue[] __) => 
                new OperandValue(true);
        }
    }
}

