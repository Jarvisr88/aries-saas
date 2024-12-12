namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class DXDisplayCriteriaGenerator : DisplayCriteriaGenerator
    {
        protected DXDisplayCriteriaGenerator(IDisplayCriteriaGeneratorNamesSourceEx namesSource) : base(namesSource)
        {
        }

        private FunctionOperator MakeSubstitutionCriteria(string name, OperandProperty property, IEnumerable<CriteriaOperator> values) => 
            new FunctionOperator(name, this.Convert(property).Yield<CriteriaOperator>().Concat<CriteriaOperator>(from x in values select this.ProcessPossibleValue(property, x)));

        public static CriteriaOperator ProcessEx(IDisplayCriteriaGeneratorNamesSourceEx namesSource, CriteriaOperator op) => 
            new DXDisplayCriteriaGenerator(namesSource).Process(op);

        public override CriteriaOperator Visit(FunctionOperator theOperator)
        {
            FormatConditionFilterInfo info3;
            Attributed<ValueDataRange> range = BetweenDatesHelper.TryGetRangeFromSubstituted<object>(theOperator);
            if (range != null)
            {
                return ((IDisplayCriteriaGeneratorNamesSourceEx) base.NamesSource).WithDateRangeProcessing<FunctionOperator>(() => this.MakeSubstitutionCriteria(BetweenDatesHelper.BetweenDatesCustomFunctionName, range.Property, new CriteriaOperator[] { range.Value.From.ToCriteria(), range.Value.To.ToCriteria() }));
            }
            Attributed<ValueData[]> values = BetweenDatesHelper.TryGetPropertyValuesFromSubstituted<object>(theOperator);
            if (values != null)
            {
                return ((IDisplayCriteriaGeneratorNamesSourceEx) base.NamesSource).WithDateRangeProcessing<FunctionOperator>(delegate {
                    Func<ValueData, CriteriaOperator> selector = <>c.<>9__2_2;
                    if (<>c.<>9__2_2 == null)
                    {
                        Func<ValueData, CriteriaOperator> local1 = <>c.<>9__2_2;
                        selector = <>c.<>9__2_2 = x => x.ToCriteria();
                    }
                    return this.MakeSubstitutionCriteria(BetweenDatesHelper.IsOnDatesCustomFunctionName, values.Property, values.Value.Select<ValueData, CriteriaOperator>(selector));
                });
            }
            AppliedFormatConditionFilterInfo appliedFormatConditionFilterInfo = FormatConditionFiltersHelper.GetAppliedFormatConditionFilterInfo(theOperator);
            if (appliedFormatConditionFilterInfo != null)
            {
                info3 = appliedFormatConditionFilterInfo.Info;
            }
            else
            {
                AppliedFormatConditionFilterInfo local1 = appliedFormatConditionFilterInfo;
                info3 = null;
            }
            FormatConditionFilterInfo local2 = info3;
            FormatConditionFilterInfo topBottomFilterInfo = local2;
            if (local2 == null)
            {
                FormatConditionFilterInfo local3 = local2;
                topBottomFilterInfo = FormatConditionFiltersHelper.GetTopBottomFilterInfo(theOperator);
            }
            FormatConditionFilterInfo info = topBottomFilterInfo;
            if ((info != null) && info.Type.IsTopBottomOrAverageOrUniqueDuplicate())
            {
                return new TopBottomFilterFunctionOperator(theOperator.Operands.ToArray(), base.NamesSource.GetDisplayPropertyName(new OperandProperty(info.PropertyName)));
            }
            PredefinedFiltersHelper.PredefinedFilterInfo predefinedFilterInfo = PredefinedFiltersHelper.GetPredefinedFilterInfo(theOperator);
            if (predefinedFilterInfo != null)
            {
                CriteriaOperator[] operands = new CriteriaOperator[] { new OperandProperty(predefinedFilterInfo.PropertyName) };
                theOperator = new FunctionOperator(predefinedFilterInfo.FilterName, operands);
            }
            return base.Visit(theOperator);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DXDisplayCriteriaGenerator.<>c <>9 = new DXDisplayCriteriaGenerator.<>c();
            public static Func<ValueData, CriteriaOperator> <>9__2_2;

            internal CriteriaOperator <Visit>b__2_2(ValueData x) => 
                x.ToCriteria();
        }
    }
}

