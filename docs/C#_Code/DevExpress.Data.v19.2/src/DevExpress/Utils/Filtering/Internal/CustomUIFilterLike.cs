namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Utils.Filtering;
    using System;
    using System.Runtime.CompilerServices;

    internal sealed class CustomUIFilterLike : CustomUIFilter
    {
        public CustomUIFilterLike(CustomUIFilterType filterType, Func<IServiceProvider> getServiceProvider) : base(filterType, getServiceProvider)
        {
        }

        protected sealed override bool AllowCore(ICustomUIFiltersOptions userOptions)
        {
            Func<ICustomUIFiltersOptions, bool> get = <>c.<>9__2_0;
            if (<>c.<>9__2_0 == null)
            {
                Func<ICustomUIFiltersOptions, bool> local1 = <>c.<>9__2_0;
                get = <>c.<>9__2_0 = opt => opt.ShowLikeFilters;
            }
            return userOptions.Get<ICustomUIFiltersOptions, bool>(get, ExcelFilterOptions.Default.ShowLikeFilters.GetValueOrDefault(true));
        }

        protected sealed override ICustomUIFilterCriteriaParser CreateCriteriaParser(IEndUserFilteringMetric metric) => 
            new LikeCriteriaParser(base.id, metric.Path);

        protected sealed override CriteriaOperator GetCriteria(IEndUserFilteringMetric metric, ICustomUIFilterValue filterValue)
        {
            FunctionOperator operand = LikeCustomFunction.Create(new OperandProperty(metric.Path), new OperandValue(CheckNullObject(filterValue)));
            return ((((CustomUIFilterType) base.id) == CustomUIFilterType.NotLike) ? ((CriteriaOperator) new UnaryOperator(UnaryOperatorType.Not, operand)) : ((CriteriaOperator) operand));
        }

        internal static bool Match(CustomUIFilterType filterType) => 
            (filterType == CustomUIFilterType.Like) || (filterType == CustomUIFilterType.NotLike);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CustomUIFilterLike.<>c <>9 = new CustomUIFilterLike.<>c();
            public static Func<ICustomUIFiltersOptions, bool> <>9__2_0;

            internal bool <AllowCore>b__2_0(ICustomUIFiltersOptions opt) => 
                opt.ShowLikeFilters;
        }

        private sealed class LikeCriteriaParser : CustomUIFilter.CustomUIFilterCriteriaParser
        {
            private readonly object[] values;
            private bool? isInverted;

            internal LikeCriteriaParser(CustomUIFilterType filterType, string path) : base(filterType, path)
            {
                this.values = new object[1];
            }

            protected sealed override void OnFunctionOperator(FunctionOperator theOperator)
            {
                if (!LikeCustomFunction.IsBinaryCompatibleLikeFunction(theOperator))
                {
                    base.MarkInvalid();
                }
                else
                {
                    OperandProperty property = (theOperator.Operands[1] as OperandProperty) ?? (theOperator.Operands[2] as OperandProperty);
                    if (base.IsInvalid(property))
                    {
                        base.MarkInvalid();
                    }
                    else
                    {
                        OperandValue value2 = (theOperator.Operands[2] as OperandValue) ?? (theOperator.Operands[1] as OperandValue);
                        if ((value2 == null) || !CustomUIFilter.TryGetValue(value2, typeof(string), out this.values[0]))
                        {
                            base.MarkInvalid();
                        }
                        else
                        {
                            CustomUIFilterType type = base.GetFilterType(this.isInverted, CustomUIFilterType.NotLike, CustomUIFilterType.Like);
                            if (base.filterType != type)
                            {
                                base.MarkInvalid();
                            }
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
        }
    }
}

