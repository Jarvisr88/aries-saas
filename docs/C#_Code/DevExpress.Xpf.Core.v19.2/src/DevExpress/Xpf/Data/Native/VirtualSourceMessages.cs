namespace DevExpress.Xpf.Data.Native
{
    using System;

    public static class VirtualSourceMessages
    {
        public const string PropertyCannotBeAssignedAfterGetItemProperties = "{0} cannot be assigned after the ITypedList.GetItemProperties method has been called.";
        public const string PageIndexCannotBeNegative = "PageIndex cannot be negative.";
        public const string PageIndexCannotBeGreaterThanLastLoadedPageIndex = "PageIndex cannot be greater than the last loaded page's index in the Consecutive page navigation mode.";
        public const string PageIndexCannotBeGreaterThanTotalPageCount = "PageIndex cannot be greater than the total page count.";
        public const string SkipTokensAreOnlyAllowedInConsecutiveMode = "Skip Tokens are allowed in the Consecutive page navigation mode only.";
        public const string SubscribeGetUniqueValuesEvent = "Subscribe to the GetUniqueValues event before accessing the property's unique values.";
        public const string SubscribeGetTotalSummariesEvent = "Subscribe to the GetTotalSummaries event before accessing summaries.";
        public const string SubscribeFetchRowsEvent = "Subscribe to the FetchRows or FetchPage event before using the source.";
        public const string ElementTypeOrCustomPropertiesPropertyShouldBeSet = "Either the ElementType or CustomProperties property should be specified before initializing the source.";
        public const string RowIsUpdating = "Row is updating.";
    }
}

