namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;
    using DevExpress.Utils.Filtering;
    using System;
    using System.Runtime.CompilerServices;

    internal sealed class CustomUIFilterBlank : CustomUIFilter
    {
        public CustomUIFilterBlank(CustomUIFilterType filterType, Func<IServiceProvider> getServiceProvider) : base(filterType, getServiceProvider)
        {
        }

        protected sealed override bool AllowCore(ICustomUIFiltersOptions userOptions)
        {
            Func<ICustomUIFiltersOptions, bool> get = <>c.<>9__2_0;
            if (<>c.<>9__2_0 == null)
            {
                Func<ICustomUIFiltersOptions, bool> local1 = <>c.<>9__2_0;
                get = <>c.<>9__2_0 = opt => opt.ShowBlanks;
            }
            return userOptions.Get<ICustomUIFiltersOptions, bool>(get, ExcelFilterOptions.Default.ShowBlanks.GetValueOrDefault(true));
        }

        protected sealed override ICustomUIFilterCriteriaParser CreateCriteriaParser(IEndUserFilteringMetric metric) => 
            new BlankCriteriaParser(base.id, metric.Path);

        protected sealed override CriteriaOperator GetCriteria(IEndUserFilteringMetric metric, ICustomUIFilterValue filterValue)
        {
            CriteriaOperator[] operands = new CriteriaOperator[] { new OperandProperty(metric.Path) };
            FunctionOperator operand = new FunctionOperator(FunctionOperatorType.IsNullOrEmpty, operands);
            return ((((CustomUIFilterType) base.id) == CustomUIFilterType.IsNotBlank) ? ((CriteriaOperator) new UnaryOperator(UnaryOperatorType.Not, operand)) : ((CriteriaOperator) operand));
        }

        internal static bool Match(CustomUIFilterType filterType) => 
            (filterType == CustomUIFilterType.IsBlank) || (filterType == CustomUIFilterType.IsNotBlank);

        protected sealed override void QueryViewModelResult(ICustomUIFilterDialogViewModel viewModel)
        {
            viewModel.SetResult(new object[0]);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CustomUIFilterBlank.<>c <>9 = new CustomUIFilterBlank.<>c();
            public static Func<ICustomUIFiltersOptions, bool> <>9__2_0;

            internal bool <AllowCore>b__2_0(ICustomUIFiltersOptions opt) => 
                opt.ShowBlanks;
        }

        private sealed class BlankCriteriaParser : CustomUIFilter.CustomUIFilterCriteriaParser
        {
            private bool? isInverted;

            internal BlankCriteriaParser(CustomUIFilterType filterType, string path) : base(filterType, path)
            {
            }

            protected sealed override void OnFunctionOperator(FunctionOperator theOperator)
            {
                if ((theOperator.OperatorType != FunctionOperatorType.IsNullOrEmpty) || (theOperator.Operands.Count != 1))
                {
                    base.MarkInvalid();
                }
                else
                {
                    OperandProperty property = theOperator.Operands[0] as OperandProperty;
                    if (base.IsInvalid(property))
                    {
                        base.MarkInvalid();
                    }
                    else
                    {
                        CustomUIFilterType type = base.GetFilterType(this.isInverted, CustomUIFilterType.IsNotBlank, CustomUIFilterType.IsBlank);
                        if (base.filterType != type)
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
                base.Prepare(ref this.isInverted);
        }
    }
}

