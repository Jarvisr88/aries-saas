namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal sealed class CustomUIFilterTextFunction : CustomUIFilter
    {
        private static readonly IDictionary<CustomUIFilterType, FunctionOperatorType> map;

        static CustomUIFilterTextFunction()
        {
            Dictionary<CustomUIFilterType, FunctionOperatorType> dictionary1 = new Dictionary<CustomUIFilterType, FunctionOperatorType>();
            dictionary1.Add(CustomUIFilterType.BeginsWith, FunctionOperatorType.StartsWith);
            dictionary1.Add(CustomUIFilterType.DoesNotBeginsWith, FunctionOperatorType.StartsWith);
            dictionary1.Add(CustomUIFilterType.EndsWith, FunctionOperatorType.EndsWith);
            dictionary1.Add(CustomUIFilterType.DoesNotEndsWith, FunctionOperatorType.EndsWith);
            dictionary1.Add(CustomUIFilterType.Contains, FunctionOperatorType.Contains);
            dictionary1.Add(CustomUIFilterType.DoesNotContain, FunctionOperatorType.Contains);
            map = dictionary1;
        }

        public CustomUIFilterTextFunction(CustomUIFilterType filterType, Func<IServiceProvider> getServiceProvider) : base(filterType, getServiceProvider)
        {
        }

        protected sealed override ICustomUIFilterCriteriaParser CreateCriteriaParser(IEndUserFilteringMetric metric) => 
            new TextFunctionCriteriaParser(base.id, metric.Path);

        protected sealed override CriteriaOperator GetCriteria(IEndUserFilteringMetric metric, ICustomUIFilterValue filterValue)
        {
            OperandProperty property = new OperandProperty(metric.Path);
            OperandValue value2 = new OperandValue(CheckNullObject(filterValue));
            CriteriaOperator[] operands = new CriteriaOperator[] { property, value2 };
            FunctionOperator operand = new FunctionOperator(map[base.id], operands);
            return (this.NeedInversion() ? ((CriteriaOperator) new UnaryOperator(UnaryOperatorType.Not, operand)) : ((CriteriaOperator) operand));
        }

        internal static bool Match(CustomUIFilterType filterType) => 
            map.ContainsKey(filterType);

        private bool NeedInversion() => 
            (((CustomUIFilterType) base.id) == CustomUIFilterType.DoesNotContain) || ((((CustomUIFilterType) base.id) == CustomUIFilterType.DoesNotEndsWith) || (((CustomUIFilterType) base.id) == CustomUIFilterType.DoesNotBeginsWith));

        private sealed class TextFunctionCriteriaParser : CustomUIFilter.CustomUIFilterCriteriaParser
        {
            private static readonly IDictionary<CustomUIFilterType, CustomUIFilterType> inversionMap;
            private readonly object[] values;
            private bool? isInverted;

            static TextFunctionCriteriaParser()
            {
                Dictionary<CustomUIFilterType, CustomUIFilterType> dictionary1 = new Dictionary<CustomUIFilterType, CustomUIFilterType>();
                dictionary1.Add(CustomUIFilterType.Contains, CustomUIFilterType.DoesNotContain);
                dictionary1.Add(CustomUIFilterType.DoesNotContain, CustomUIFilterType.DoesNotContain);
                dictionary1.Add(CustomUIFilterType.BeginsWith, CustomUIFilterType.DoesNotBeginsWith);
                dictionary1.Add(CustomUIFilterType.DoesNotBeginsWith, CustomUIFilterType.DoesNotBeginsWith);
                dictionary1.Add(CustomUIFilterType.EndsWith, CustomUIFilterType.DoesNotEndsWith);
                dictionary1.Add(CustomUIFilterType.DoesNotEndsWith, CustomUIFilterType.DoesNotEndsWith);
                inversionMap = dictionary1;
            }

            internal TextFunctionCriteriaParser(CustomUIFilterType filterType, string path) : base(filterType, path)
            {
                this.values = new object[1];
            }

            protected sealed override void OnFunctionOperator(FunctionOperator theOperator)
            {
                if ((theOperator.OperatorType != ((FunctionOperatorType) CustomUIFilterTextFunction.map[base.filterType])) || (base.filterType != base.GetFilterType(this.isInverted, inversionMap)))
                {
                    base.MarkInvalid();
                }
                else
                {
                    Func<CriteriaOperator, bool> predicate = <>c.<>9__4_0;
                    if (<>c.<>9__4_0 == null)
                    {
                        Func<CriteriaOperator, bool> local1 = <>c.<>9__4_0;
                        predicate = <>c.<>9__4_0 = op => op is OperandProperty;
                    }
                    OperandProperty property = theOperator.Operands.FirstOrDefault<CriteriaOperator>(predicate) as OperandProperty;
                    if (base.IsInvalid(property))
                    {
                        base.MarkInvalid();
                    }
                    else
                    {
                        Func<CriteriaOperator, bool> func2 = <>c.<>9__4_1;
                        if (<>c.<>9__4_1 == null)
                        {
                            Func<CriteriaOperator, bool> local2 = <>c.<>9__4_1;
                            func2 = <>c.<>9__4_1 = op => op is OperandValue;
                        }
                        OperandValue value2 = theOperator.Operands.FirstOrDefault<CriteriaOperator>(func2) as OperandValue;
                        if (value2 == null)
                        {
                            base.MarkInvalid();
                        }
                        else if (!CustomUIFilter.TryGetValue(value2, typeof(string), out this.values[0]))
                        {
                            base.MarkInvalid();
                        }
                    }
                }
            }

            protected sealed override void OnUnaryOperator(UnaryOperator theOperator)
            {
                if ((theOperator.OperatorType != UnaryOperatorType.Not) || !(theOperator.Operand is FunctionOperator))
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

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly CustomUIFilterTextFunction.TextFunctionCriteriaParser.<>c <>9 = new CustomUIFilterTextFunction.TextFunctionCriteriaParser.<>c();
                public static Func<CriteriaOperator, bool> <>9__4_0;
                public static Func<CriteriaOperator, bool> <>9__4_1;

                internal bool <OnFunctionOperator>b__4_0(CriteriaOperator op) => 
                    op is OperandProperty;

                internal bool <OnFunctionOperator>b__4_1(CriteriaOperator op) => 
                    op is OperandValue;
            }
        }
    }
}

