namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;
    using DevExpress.Utils.Filtering;
    using System;
    using System.Runtime.CompilerServices;

    internal sealed class CustomUIFilterNull : CustomUIFilter
    {
        public CustomUIFilterNull(CustomUIFilterType filterType, Func<IServiceProvider> getServiceProvider) : base(filterType, getServiceProvider)
        {
        }

        protected sealed override bool AllowCore(ICustomUIFiltersOptions userOptions)
        {
            Func<ICustomUIFiltersOptions, bool> get = <>c.<>9__2_0;
            if (<>c.<>9__2_0 == null)
            {
                Func<ICustomUIFiltersOptions, bool> local1 = <>c.<>9__2_0;
                get = <>c.<>9__2_0 = opt => opt.ShowNulls;
            }
            return userOptions.Get<ICustomUIFiltersOptions, bool>(get, ExcelFilterOptions.Default.ShowNulls.GetValueOrDefault(true));
        }

        protected sealed override ICustomUIFilterCriteriaParser CreateCriteriaParser(IEndUserFilteringMetric metric) => 
            new NullCriteriaParser(base.id, metric.Path);

        protected sealed override CriteriaOperator GetCriteria(IEndUserFilteringMetric metric, ICustomUIFilterValue filterValue)
        {
            UnaryOperator operand = new UnaryOperator(UnaryOperatorType.IsNull, new OperandProperty(metric.Path));
            return ((((CustomUIFilterType) base.id) == CustomUIFilterType.IsNotNull) ? new UnaryOperator(UnaryOperatorType.Not, operand) : operand);
        }

        internal static bool Match(CustomUIFilterType filterType) => 
            (filterType == CustomUIFilterType.IsNull) || (filterType == CustomUIFilterType.IsNotNull);

        protected sealed override void QueryViewModelResult(ICustomUIFilterDialogViewModel viewModel)
        {
            viewModel.SetResult(new object[0]);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CustomUIFilterNull.<>c <>9 = new CustomUIFilterNull.<>c();
            public static Func<ICustomUIFiltersOptions, bool> <>9__2_0;

            internal bool <AllowCore>b__2_0(ICustomUIFiltersOptions opt) => 
                opt.ShowNulls;
        }

        private sealed class NullCriteriaParser : CustomUIFilter.CustomUIFilterCriteriaParser
        {
            private bool? isInverted;

            internal NullCriteriaParser(CustomUIFilterType filterType, string path) : base(filterType, path)
            {
            }

            protected sealed override void OnBinaryOperator(BinaryOperator theOperator)
            {
                if ((theOperator.OperatorType != BinaryOperatorType.Equal) && (theOperator.OperatorType != BinaryOperatorType.NotEqual))
                {
                    base.MarkInvalid();
                }
                else
                {
                    CustomUIFilterType type = base.GetFilterType(new bool?(theOperator.OperatorType == BinaryOperatorType.NotEqual), CustomUIFilterType.IsNotNull, CustomUIFilterType.IsNull);
                    if (base.filterType != type)
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
                            if ((value2 == null) || !Equals(null, CustomUIFilter.GetValue(value2.Value, typeof(object))))
                            {
                                base.MarkInvalid();
                            }
                        }
                    }
                }
            }

            protected sealed override void OnUnaryOperator(UnaryOperator theOperator)
            {
                if (theOperator.OperatorType != UnaryOperatorType.IsNull)
                {
                    if ((theOperator.OperatorType != UnaryOperatorType.Not) || !(theOperator.Operand is UnaryOperator))
                    {
                        base.MarkInvalid();
                    }
                    else
                    {
                        this.isInverted = new bool?(!this.isInverted.GetValueOrDefault());
                        theOperator.Operand.Accept(this);
                    }
                }
                else
                {
                    OperandProperty operand = theOperator.Operand as OperandProperty;
                    if (base.IsInvalid(operand))
                    {
                        base.MarkInvalid();
                    }
                    else
                    {
                        CustomUIFilterType type = base.GetFilterType(this.isInverted, CustomUIFilterType.IsNotNull, CustomUIFilterType.IsNull);
                        if (base.filterType != type)
                        {
                            base.MarkInvalid();
                        }
                    }
                }
            }

            protected sealed override object[] PrepareLocalValues() => 
                base.Prepare(ref this.isInverted);
        }
    }
}

