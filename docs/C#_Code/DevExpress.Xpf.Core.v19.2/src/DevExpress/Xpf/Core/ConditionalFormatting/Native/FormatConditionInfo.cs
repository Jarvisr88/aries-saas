namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using DevExpress.Data;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using System;
    using System.Runtime.CompilerServices;

    public class FormatConditionInfo : ExpressionConditionBaseInfo
    {
        public override bool CalcCondition(FormatValueProvider provider) => 
            (this.ValueRule == ConditionRule.Expression) ? IsFit(provider.Value) : this.CalcRule(provider);

        private bool CalcRule(FormatValueProvider provider)
        {
            object obj2 = provider.Value;
            if (!InvalidFormatCache.Instance.Equals(obj2) && !AsyncServerModeDataController.IsNoValue(obj2))
            {
                if (DBNull.Value.Equals(obj2))
                {
                    obj2 = null;
                }
                object y = Convert(obj2, this.Value1);
                if ((y != null) || (this.Value1 == null))
                {
                    ValueComparer valueComparer = provider.ValueComparer;
                    if (valueComparer == null)
                    {
                        return false;
                    }
                    int num = valueComparer.Compare(obj2, y);
                    switch (this.ValueRule)
                    {
                        case ConditionRule.None:
                            return true;

                        case ConditionRule.Equal:
                            return (num == 0);

                        case ConditionRule.NotEqual:
                            return (num != 0);

                        case ConditionRule.Between:
                        case ConditionRule.NotBetween:
                        {
                            object obj4 = Convert(obj2, this.Value2);
                            if ((obj4 == null) && (this.Value2 != null))
                            {
                                return false;
                            }
                            int num2 = valueComparer.Compare(obj2, obj4);
                            return ((this.ValueRule == ConditionRule.Between) == ((num > 0) && (num2 < 0)));
                        }
                        case ConditionRule.Less:
                            return (num < 0);

                        case ConditionRule.Greater:
                            return (num > 0);

                        case ConditionRule.GreaterOrEqual:
                            return (num >= 0);

                        case ConditionRule.LessOrEqual:
                            return (num <= 0);
                    }
                }
            }
            return false;
        }

        private static object Convert(object target, object source)
        {
            if ((target != null) && (source != null))
            {
                Type valType = target.GetType();
                if (!valType.Equals(source.GetType()))
                {
                    source = DoConversion(() => System.Convert.ChangeType(source, valType));
                }
            }
            return source;
        }

        private static object DoConversion(Func<object> conversion)
        {
            object obj2 = null;
            try
            {
                obj2 = conversion();
            }
            catch
            {
                return null;
            }
            return obj2;
        }

        public ConditionRule ValueRule { get; set; }

        public object Value1 { get; set; }

        public object Value2 { get; set; }

        protected override string ActualExpression =>
            (this.ValueRule == ConditionRule.Expression) ? base.ActualExpression : null;
    }
}

