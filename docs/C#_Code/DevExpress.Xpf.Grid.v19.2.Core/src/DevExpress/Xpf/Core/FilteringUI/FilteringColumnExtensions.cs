namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;
    using System.Runtime.CompilerServices;

    internal static class FilteringColumnExtensions
    {
        public static bool GetUseRangeDateFilter(this FilteringColumn column) => 
            FilterTreeHelper.IsDateTimeProperty(column.Type) ? column.GetRoundDateTimeFilter() : false;
    }
}

