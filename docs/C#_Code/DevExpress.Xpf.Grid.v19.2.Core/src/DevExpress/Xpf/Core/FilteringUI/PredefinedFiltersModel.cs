namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public sealed class PredefinedFiltersModel : CustomFiltersModelBase<PredefinedFilter>
    {
        internal PredefinedFiltersModel(FilterModelClient client) : base(client)
        {
        }

        protected override FunctionOperator CreateFilter(PredefinedFilter filter) => 
            PredefinedFiltersHelper.MakePredefinedFilterFunction(filter.Name, base.PropertyName);

        protected override Func<PredefinedFilter, bool> GetIsSelectedFilterChecker(CriteriaOperator filter)
        {
            string[] filterNames = GetPredefinedFilters(base.Filter);
            return x => filterNames.Contains<string>(x.Name);
        }

        internal static string[] GetPredefinedFilters(CriteriaOperator filter)
        {
            Func<FunctionOperator, string> mapper = <>c.<>9__4_0;
            if (<>c.<>9__4_0 == null)
            {
                Func<FunctionOperator, string> local1 = <>c.<>9__4_0;
                mapper = <>c.<>9__4_0 = delegate (FunctionOperator f) {
                    FunctionOperatorMapper<string> function = <>c.<>9__4_1;
                    if (<>c.<>9__4_1 == null)
                    {
                        FunctionOperatorMapper<string> local1 = <>c.<>9__4_1;
                        function = <>c.<>9__4_1 = delegate (string name, object[] values, FunctionOperatorType type) {
                            PredefinedFiltersHelper.PredefinedFilterInfo info1 = PredefinedFiltersHelper.GetPredefinedFilterInfo(name, values, type);
                            if (info1 != null)
                            {
                                return info1.FilterName;
                            }
                            PredefinedFiltersHelper.PredefinedFilterInfo local1 = info1;
                            return null;
                        };
                    }
                    FallbackMapper<string> fallback = <>c.<>9__4_2;
                    if (<>c.<>9__4_2 == null)
                    {
                        FallbackMapper<string> local2 = <>c.<>9__4_2;
                        fallback = <>c.<>9__4_2 = (FallbackMapper<string>) (_ => null);
                    }
                    return f.Map<string>(null, null, null, null, function, null, null, null, fallback, null);
                };
            }
            return ParseCustomFunctions<string>(filter, mapper).ToArray<string>();
        }

        protected override IEnumerable<PredefinedFilter> CoreFilters =>
            base.Column.GetSustitutedPredefinedFilters();

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PredefinedFiltersModel.<>c <>9 = new PredefinedFiltersModel.<>c();
            public static FunctionOperatorMapper<string> <>9__4_1;
            public static FallbackMapper<string> <>9__4_2;
            public static Func<FunctionOperator, string> <>9__4_0;

            internal string <GetPredefinedFilters>b__4_0(FunctionOperator f)
            {
                FunctionOperatorMapper<string> function = <>9__4_1;
                if (<>9__4_1 == null)
                {
                    FunctionOperatorMapper<string> local1 = <>9__4_1;
                    function = <>9__4_1 = delegate (string name, object[] values, FunctionOperatorType type) {
                        PredefinedFiltersHelper.PredefinedFilterInfo info1 = PredefinedFiltersHelper.GetPredefinedFilterInfo(name, values, type);
                        if (info1 != null)
                        {
                            return info1.FilterName;
                        }
                        PredefinedFiltersHelper.PredefinedFilterInfo local1 = info1;
                        return null;
                    };
                }
                FallbackMapper<string> fallback = <>9__4_2;
                if (<>9__4_2 == null)
                {
                    FallbackMapper<string> local2 = <>9__4_2;
                    fallback = <>9__4_2 = (FallbackMapper<string>) (_ => null);
                }
                return f.Map<string>(null, null, null, null, function, null, null, null, fallback, null);
            }

            internal string <GetPredefinedFilters>b__4_1(string name, object[] values, FunctionOperatorType type)
            {
                PredefinedFiltersHelper.PredefinedFilterInfo info1 = PredefinedFiltersHelper.GetPredefinedFilterInfo(name, values, type);
                if (info1 != null)
                {
                    return info1.FilterName;
                }
                PredefinedFiltersHelper.PredefinedFilterInfo local1 = info1;
                return null;
            }

            internal string <GetPredefinedFilters>b__4_2(CriteriaOperator _) => 
                null;
        }
    }
}

