namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.CompilerServices;

    public static class PredefinedFiltersHelper
    {
        private const string PredefinedFilterPrefix = "$";

        public static PredefinedFiltersHelper.PredefinedFilterInfo GetPredefinedFilterInfo(FunctionOperator theOperator);
        public static PredefinedFiltersHelper.PredefinedFilterInfo GetPredefinedFilterInfo(string name, object[] values, FunctionOperatorType type);
        public static FunctionOperator MakePredefinedFilterFunction(string filterName, string propertyName);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PredefinedFiltersHelper.<>c <>9;
            public static FallbackMapper<PredefinedFiltersHelper.PredefinedFilterInfo> <>9__4_0;

            static <>c();
            internal PredefinedFiltersHelper.PredefinedFilterInfo <GetPredefinedFilterInfo>b__4_0(CriteriaOperator _);
        }

        public class PredefinedFilterInfo
        {
            public readonly string FilterName;
            public readonly string PropertyName;

            public PredefinedFilterInfo(string filterName, string propertyName);
        }
    }
}

