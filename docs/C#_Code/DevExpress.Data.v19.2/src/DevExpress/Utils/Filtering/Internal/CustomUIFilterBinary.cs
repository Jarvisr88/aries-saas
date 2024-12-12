namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;
    using DevExpress.Utils.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal sealed class CustomUIFilterBinary : CustomUIFilter
    {
        private static readonly IDictionary<CustomUIFilterType, BinaryOperatorType> map;

        static CustomUIFilterBinary()
        {
            Dictionary<CustomUIFilterType, BinaryOperatorType> dictionary1 = new Dictionary<CustomUIFilterType, BinaryOperatorType>();
            dictionary1.Add(CustomUIFilterType.Equals, BinaryOperatorType.Equal);
            dictionary1.Add(CustomUIFilterType.DoesNotEqual, BinaryOperatorType.NotEqual);
            dictionary1.Add(CustomUIFilterType.GreaterThan, BinaryOperatorType.Greater);
            dictionary1.Add(CustomUIFilterType.GreaterThanOrEqualTo, BinaryOperatorType.GreaterOrEqual);
            dictionary1.Add(CustomUIFilterType.LessThan, BinaryOperatorType.Less);
            dictionary1.Add(CustomUIFilterType.LessThanOrEqualTo, BinaryOperatorType.LessOrEqual);
            dictionary1.Add(CustomUIFilterType.After, BinaryOperatorType.Greater);
            dictionary1.Add(CustomUIFilterType.Before, BinaryOperatorType.Less);
            map = dictionary1;
        }

        public CustomUIFilterBinary(CustomUIFilterType filterType, Func<IServiceProvider> getServiceProvider) : base(filterType, getServiceProvider)
        {
        }

        protected sealed override bool AllowCore(ICustomUIFiltersOptions userOptions)
        {
            if ((((CustomUIFilterType) base.id) == CustomUIFilterType.After) || ((((CustomUIFilterType) base.id) == CustomUIFilterType.Before) || ((((CustomUIFilterType) base.id) == CustomUIFilterType.Equals) || (((CustomUIFilterType) base.id) == CustomUIFilterType.DoesNotEqual))))
            {
                return true;
            }
            Func<ICustomUIFiltersOptions, bool> get = <>c.<>9__3_0;
            if (<>c.<>9__3_0 == null)
            {
                Func<ICustomUIFiltersOptions, bool> local1 = <>c.<>9__3_0;
                get = <>c.<>9__3_0 = opt => opt.ShowComparisons;
            }
            return userOptions.Get<ICustomUIFiltersOptions, bool>(get, ExcelFilterOptions.Default.ShowComparisons.GetValueOrDefault(true));
        }

        protected sealed override ICustomUIFilterCriteriaParser CreateCriteriaParser(IEndUserFilteringMetric metric) => 
            new BinaryCriteriaParser(base.id, metric.Path, metric.Type, metric.Attributes);

        protected sealed override CriteriaOperator GetCriteria(IEndUserFilteringMetric metric, ICustomUIFilterValue filterValue)
        {
            object value = CheckNullObject(filterValue);
            if (((value == null) || ReferenceEquals(DBNull.Value, value)) && ((((CustomUIFilterType) base.id) == CustomUIFilterType.Equals) || (((CustomUIFilterType) base.id) == CustomUIFilterType.DoesNotEqual)))
            {
                FunctionOperator operator1;
                if (!(metric.Attributes as MetricAttributes).Get<MetricAttributes, bool>(x => x.ShouldUseBlanksFilterForNullValue(value), false))
                {
                    operator1 = (FunctionOperator) new UnaryOperator(UnaryOperatorType.IsNull, metric.Path);
                }
                else
                {
                    CriteriaOperator[] operands = new CriteriaOperator[] { new OperandProperty(metric.Path) };
                    operator1 = new FunctionOperator(FunctionOperatorType.IsNullOrEmpty, operands);
                }
                CriteriaOperator operand = operator1;
                if (((CustomUIFilterType) base.id) == CustomUIFilterType.Equals)
                {
                    return operand;
                }
                if (((CustomUIFilterType) base.id) == CustomUIFilterType.DoesNotEqual)
                {
                    return new UnaryOperator(UnaryOperatorType.Not, operand);
                }
            }
            return new BinaryOperator(metric.Path, value, map[base.id]);
        }

        internal static bool Match(CustomUIFilterType filterType) => 
            map.ContainsKey(filterType);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CustomUIFilterBinary.<>c <>9 = new CustomUIFilterBinary.<>c();
            public static Func<ICustomUIFiltersOptions, bool> <>9__3_0;

            internal bool <AllowCore>b__3_0(ICustomUIFiltersOptions opt) => 
                opt.ShowComparisons;
        }

        private sealed class BinaryCriteriaParser : CustomUIFilter.CustomUIFilterCriteriaParser
        {
            private readonly Type type;
            private readonly IMetricAttributes attributes;
            private static readonly IDictionary<CustomUIFilterType, CustomUIFilterType> inversionMap;
            private readonly object[] values;
            private bool? isInverted;

            static BinaryCriteriaParser()
            {
                Dictionary<CustomUIFilterType, CustomUIFilterType> dictionary1 = new Dictionary<CustomUIFilterType, CustomUIFilterType>();
                dictionary1.Add(CustomUIFilterType.Equals, CustomUIFilterType.DoesNotEqual);
                dictionary1.Add(CustomUIFilterType.DoesNotEqual, CustomUIFilterType.Equals);
                dictionary1.Add(CustomUIFilterType.GreaterThan, CustomUIFilterType.LessThanOrEqualTo);
                dictionary1.Add(CustomUIFilterType.GreaterThanOrEqualTo, CustomUIFilterType.LessThan);
                dictionary1.Add(CustomUIFilterType.LessThan, CustomUIFilterType.GreaterThanOrEqualTo);
                dictionary1.Add(CustomUIFilterType.LessThanOrEqualTo, CustomUIFilterType.GreaterThan);
                dictionary1.Add(CustomUIFilterType.After, CustomUIFilterType.Before);
                dictionary1.Add(CustomUIFilterType.Before, CustomUIFilterType.After);
                inversionMap = dictionary1;
            }

            internal BinaryCriteriaParser(CustomUIFilterType filterType, string path, Type type, IMetricAttributes attributes) : base(filterType, path)
            {
                this.values = new object[1];
                this.type = type;
                this.attributes = attributes;
            }

            private static bool GetForceFilterByText(Type type, IMetricAttributes metricAttributes) => 
                MetricAttributes.ForceFilterByText(type, (IDisplayMetricAttributes) metricAttributes);

            protected sealed override void OnBinaryOperator(BinaryOperator theOperator)
            {
                if (theOperator.OperatorType != ((BinaryOperatorType) CustomUIFilterBinary.map[base.GetFilterType(this.isInverted, inversionMap)]))
                {
                    base.MarkInvalid();
                }
                else
                {
                    OperandProperty leftOperand = theOperator.LeftOperand as OperandProperty;
                    OperandProperty property = leftOperand;
                    if (leftOperand == null)
                    {
                        OperandProperty local1 = leftOperand;
                        property = theOperator.RightOperand as OperandProperty;
                    }
                    if (this.IsInvalid(property))
                    {
                        base.MarkInvalid();
                    }
                    else
                    {
                        OperandValue value2 = (theOperator.LeftOperand as OperandValue) ?? (theOperator.RightOperand as OperandValue);
                        if (value2 == null)
                        {
                            base.MarkInvalid();
                        }
                        else if (!CustomUIFilter.TryGetValue(value2, GetForceFilterByText(this.type, this.attributes) ? typeof(string) : this.type, out this.values[0]) || (this.values[0] == null))
                        {
                            base.MarkInvalid();
                        }
                    }
                }
            }

            protected sealed override void OnUnaryOperator(UnaryOperator theOperator)
            {
                if ((theOperator.OperatorType != UnaryOperatorType.Not) || !(theOperator.Operand is BinaryOperator))
                {
                    base.MarkInvalid();
                }
                else
                {
                    this.isInverted = new bool?(!this.isInverted.GetValueOrDefault());
                    theOperator.Operand.Accept(this);
                }
            }

            protected sealed override object[] PrepareLocalValues() => 
                base.Prepare(this.values, ref this.isInverted);
        }
    }
}

