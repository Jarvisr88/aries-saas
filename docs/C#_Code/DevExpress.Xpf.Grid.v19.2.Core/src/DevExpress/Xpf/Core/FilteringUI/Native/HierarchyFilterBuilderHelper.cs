namespace DevExpress.Xpf.Core.FilteringUI.Native
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal static class HierarchyFilterBuilderHelper
    {
        public static HierarchyFilterBuilderBase GetAppropriateBuilder(string propertyName, Func<string, CriteriaOperator> getBlanksFilter, Func<string, bool> getUseDateRangeFilter = null)
        {
            Func<string, bool> func1 = getUseDateRangeFilter;
            if (getUseDateRangeFilter == null)
            {
                Func<string, bool> local1 = getUseDateRangeFilter;
                func1 = <>c.<>9__0_0;
                if (<>c.<>9__0_0 == null)
                {
                    Func<string, bool> local2 = <>c.<>9__0_0;
                    func1 = <>c.<>9__0_0 = _ => false;
                }
            }
            getUseDateRangeFilter = func1;
            return (!getUseDateRangeFilter(propertyName) ? ((HierarchyFilterBuilderBase) new HierarchyFilterBuilderSimple(propertyName, getBlanksFilter, getUseDateRangeFilter)) : ((HierarchyFilterBuilderBase) new HierarchyFilterBuilderDateRange(propertyName, getBlanksFilter, getUseDateRangeFilter)));
        }

        public static bool IsBlankElement(object value) => 
            value == null;

        public static bool IsFirstElementBlank(IEnumerable<object> values) => 
            values.Any<object>() && IsBlankElement(values.First<object>());

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly HierarchyFilterBuilderHelper.<>c <>9 = new HierarchyFilterBuilderHelper.<>c();
            public static Func<string, bool> <>9__0_0;

            internal bool <GetAppropriateBuilder>b__0_0(string _) => 
                false;
        }
    }
}

