namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Core.FilteringUI;
    using DevExpress.Xpf.Editors.Filtering;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Runtime.CompilerServices;

    internal static class AllowedFiltersExtensions
    {
        public static DevExpress.Xpf.Core.FilteringUI.AllowedGroupFilters ToCoreFilters(this DevExpress.Xpf.Editors.Filtering.AllowedGroupFilters filters) => 
            (DevExpress.Xpf.Core.FilteringUI.AllowedGroupFilters) filters;

        public static DevExpress.Xpf.Core.FilteringUI.AllowedAnyOfFilters ToCoreFilters(this DevExpress.Xpf.Grid.AllowedAnyOfFilters filters) => 
            (DevExpress.Xpf.Core.FilteringUI.AllowedAnyOfFilters) filters;

        public static DevExpress.Xpf.Core.FilteringUI.AllowedBetweenFilters ToCoreFilters(this DevExpress.Xpf.Grid.AllowedBetweenFilters filters) => 
            (DevExpress.Xpf.Core.FilteringUI.AllowedBetweenFilters) filters;

        public static DevExpress.Xpf.Core.FilteringUI.AllowedBinaryFilters ToCoreFilters(this DevExpress.Xpf.Grid.AllowedBinaryFilters filters) => 
            (DevExpress.Xpf.Core.FilteringUI.AllowedBinaryFilters) filters;

        public static DevExpress.Xpf.Core.FilteringUI.AllowedDateTimeFilters ToCoreFilters(this DevExpress.Xpf.Grid.AllowedDateTimeFilters filters) => 
            (DevExpress.Xpf.Core.FilteringUI.AllowedDateTimeFilters) filters;

        public static DevExpress.Xpf.Core.FilteringUI.AllowedUnaryFilters ToCoreFilters(this DevExpress.Xpf.Grid.AllowedUnaryFilters filters) => 
            (DevExpress.Xpf.Core.FilteringUI.AllowedUnaryFilters) filters;
    }
}

