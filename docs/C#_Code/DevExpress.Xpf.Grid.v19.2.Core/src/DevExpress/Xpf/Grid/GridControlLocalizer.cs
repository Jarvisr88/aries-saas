namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data;
    using DevExpress.Utils.Localization;
    using DevExpress.Utils.Localization.Internal;
    using DevExpress.Xpf.Core;
    using System;

    public class GridControlLocalizer : DXLocalizer<GridControlStringId>
    {
        internal const string ConditionalFormattingObsoleteMessage = "Use the DevExpress.Xpf.Core.ConditionalFormatting.ConditionalFormattingLocalizer instead";

        static GridControlLocalizer()
        {
            SetActiveLocalizerProvider(new DefaultActiveLocalizerProvider<GridControlStringId>(CreateDefaultLocalizer()));
        }

        public static XtraLocalizer<GridControlStringId> CreateDefaultLocalizer() => 
            new GridControlResXLocalizer();

        public override XtraLocalizer<GridControlStringId> CreateResXLocalizer() => 
            new GridControlResXLocalizer();

        internal static GridControlStringId? GetMenuSortByGroupSummaryStringId(SummaryItemType summaryType)
        {
            switch (summaryType)
            {
                case SummaryItemType.Sum:
                    return 0x49;

                case SummaryItemType.Min:
                    return 70;

                case SummaryItemType.Max:
                    return 0x45;

                case SummaryItemType.Count:
                    return 0x47;

                case SummaryItemType.Average:
                    return 0x48;
            }
            return null;
        }

        public static string GetString(GridControlStringId id) => 
            XtraLocalizer<GridControlStringId>.Active.GetLocalizedString(id);

        internal static string GetString(string stringId) => 
            GetString((GridControlStringId) Enum.Parse(typeof(GridControlStringId), stringId, false));

        protected override void PopulateStringTable()
        {
            this.AddString(GridControlStringId.CellPeerName, "{2}, Item: {0}, Column Display Index: {1}");
            this.AddString(GridControlStringId.GridGroupPanelText, "Drag a column header here to group by that column");
            this.AddString(GridControlStringId.GridGroupRowDisplayTextFormat, "{0}: {1}");
            this.AddString(GridControlStringId.ErrorWindowTitle, "Error");
            this.AddString(GridControlStringId.InvalidRowExceptionMessage, "Do you want to correct the value?");
            this.AddString(GridControlStringId.GridOutlookIntervals, "Older;Last Month;Earlier this Month;Three Weeks Ago;Two Weeks Ago;Last Week;;;;;;;;Yesterday;Today;Tomorrow;;;;;;;;Next Week;Two Weeks Away;Three Weeks Away;Later this Month;Next Month;Beyond Next Month;");
            this.AddString(GridControlStringId.DefaultGroupSummaryFormatString_Count, "Count={0}");
            this.AddString(GridControlStringId.DefaultGroupSummaryFormatString_Min, "Min of {1} is {0}");
            this.AddString(GridControlStringId.DefaultGroupSummaryFormatString_Max, "Max of {1} is {0}");
            this.AddString(GridControlStringId.DefaultGroupSummaryFormatString_Avg, "Avg of {1} is {0:0.##}");
            this.AddString(GridControlStringId.DefaultGroupSummaryFormatString_Sum, "Sum of {1} is {0:0.##}");
            this.AddString(GridControlStringId.DefaultTotalSummaryFormatStringInSameColumn_Count, "Count={0}");
            this.AddString(GridControlStringId.DefaultTotalSummaryFormatStringInSameColumn_Min, "Min={0}");
            this.AddString(GridControlStringId.DefaultTotalSummaryFormatStringInSameColumn_Max, "Max={0}");
            this.AddString(GridControlStringId.DefaultTotalSummaryFormatStringInSameColumn_Avg, "Avg={0:0.##}");
            this.AddString(GridControlStringId.DefaultTotalSummaryFormatStringInSameColumn_Sum, "Sum={0:0.##}");
            this.AddString(GridControlStringId.DefaultTotalSummaryFormatString_Count, "Count={0}");
            this.AddString(GridControlStringId.DefaultTotalSummaryFormatString_Min, "Min of {1} is {0}");
            this.AddString(GridControlStringId.DefaultTotalSummaryFormatString_Max, "Max of {1} is {0}");
            this.AddString(GridControlStringId.DefaultTotalSummaryFormatString_Avg, "Avg of {1} is {0:0.##}");
            this.AddString(GridControlStringId.DefaultTotalSummaryFormatString_Sum, "Sum of {1} is {0:0.##}");
            this.AddString(GridControlStringId.DefaultGroupColumnSummaryFormatStringInSameColumn_Count, "Count={0}");
            this.AddString(GridControlStringId.DefaultGroupColumnSummaryFormatStringInSameColumn_Min, "Min={0}");
            this.AddString(GridControlStringId.DefaultGroupColumnSummaryFormatStringInSameColumn_Max, "Max={0}");
            this.AddString(GridControlStringId.DefaultGroupColumnSummaryFormatStringInSameColumn_Avg, "Avg={0:0.##}");
            this.AddString(GridControlStringId.DefaultGroupColumnSummaryFormatStringInSameColumn_Sum, "Sum={0:0.##}");
            this.AddString(GridControlStringId.DefaultGroupColumnSummaryFormatString_Count, "Count={0}");
            this.AddString(GridControlStringId.DefaultGroupColumnSummaryFormatString_Min, "Min of {1} is {0}");
            this.AddString(GridControlStringId.DefaultGroupColumnSummaryFormatString_Max, "Max of {1} is {0}");
            this.AddString(GridControlStringId.DefaultGroupColumnSummaryFormatString_Avg, "Avg of {1} is {0:0.##}");
            this.AddString(GridControlStringId.DefaultGroupColumnSummaryFormatString_Sum, "Sum of {1} is {0:0.##}");
            this.AddString(GridControlStringId.PopupFilterAll, "(All)");
            this.AddString(GridControlStringId.PopupFilterBlanks, "(Blanks)");
            this.AddString(GridControlStringId.PopupFilterNonBlanks, "(Non blanks)");
            this.AddString(GridControlStringId.ColumnChooserCaption, "Column Chooser");
            this.AddString(GridControlStringId.ColumnBandChooserCaption, "Customization");
            this.AddString(GridControlStringId.ColumnChooserCaptionForMasterDetail, "{0}: Column Chooser");
            this.AddString(GridControlStringId.ColumnChooserDragText, "Drag a column here to customize layout");
            this.AddString(GridControlStringId.BandChooserDragText, "Drag a band here to customize layout");
            this.AddString(GridControlStringId.ColumnBandChooserColumnsTabCaption, "Columns");
            this.AddString(GridControlStringId.ColumnBandChooserBandsTabCaption, "Bands");
            this.AddString(GridControlStringId.GridNewRowText, "Click here to add a new row");
            this.AddString(GridControlStringId.ExcelColumnFilterPopupSearchScopeDay, "Date");
            this.AddString(GridControlStringId.ExcelColumnFilterPopupSearchScopeMonth, "Month");
            this.AddString(GridControlStringId.ExcelColumnFilterPopupSearchScopeYear, "Year");
            this.AddString(GridControlStringId.ExcelColumnFilterPopupSearchScopeAll, "All");
            this.AddString(GridControlStringId.ExcelColumnFilterPopupValuesTabCaption, "FILTER VALUES");
            this.AddString(GridControlStringId.ExcelColumnFilterPopupFilterRulesTabCaption, "FILTER RULES");
            this.AddString(GridControlStringId.ExcelColumnFilterPopupClearFilter, "Clear Filter");
            this.AddString(GridControlStringId.ExcelColumnFilterPopupFilterBetweenFrom, "From: ");
            this.AddString(GridControlStringId.ExcelColumnFilterPopupFilterBetweenTo, "To: ");
            this.AddString(GridControlStringId.AddCurrentSelectionToFilter, "Add current selection to filter");
            this.AddString(GridControlStringId.ExcelColumnFilterPopupSearchNullText, "Search");
            this.AddString(GridControlStringId.ExcelColumnFilterPopupSearchNullTextAll, "Search (All)");
            this.AddString(GridControlStringId.ExcelColumnFilterPopupSearchNullTextYear, "Search Year");
            this.AddString(GridControlStringId.ExcelColumnFilterPopupSearchNullTextMonth, "Search Month");
            this.AddString(GridControlStringId.ExcelColumnFilterPopupSearchNullTextDate, "Search Date");
            this.AddString(GridControlStringId.ExcelColumnFilterPopupEnterValue, "Enter a value...");
            this.AddString(GridControlStringId.ExcelColumnFilterPopupSelectValue, "Select a value...");
            this.AddString(GridControlStringId.ExcelColumnFilterPopupSelectDate, "Select a date...");
            this.AddString(GridControlStringId.ExtendedColumnChooserSearchColumns, "Search Columns...");
            this.AddString(GridControlStringId.ExtendedColumnChooserSearchColumnsAndBands, "Search Columns and Bands...");
            this.AddString(GridControlStringId.MenuGroupPanelFullCollapse, "Full Collapse");
            this.AddString(GridControlStringId.MenuGroupPanelFullExpand, "Full Expand");
            this.AddString(GridControlStringId.MenuGroupPanelClearGrouping, "Clear Grouping");
            this.AddString(GridControlStringId.MenuColumnSortAscending, "Sort Ascending");
            this.AddString(GridControlStringId.MenuColumnSortDescending, "Sort Descending");
            this.AddString(GridControlStringId.MenuColumnSortBySummaryAverage, "Average");
            this.AddString(GridControlStringId.MenuColumnSortBySummaryCount, "Count");
            this.AddString(GridControlStringId.MenuColumnSortBySummarySum, "Sum");
            this.AddString(GridControlStringId.MenuColumnSortBySummaryMax, "Max");
            this.AddString(GridControlStringId.MenuColumnSortBySummaryMin, "Min");
            this.AddString(GridControlStringId.MenuColumnSortBySummaryAscending, "Ascending");
            this.AddString(GridControlStringId.MenuColumnSortBySummaryDescending, "Descending");
            this.AddString(GridControlStringId.MenuColumnClearSorting, "Clear Sorting");
            this.AddString(GridControlStringId.MenuColumnUnGroup, "Ungroup");
            this.AddString(GridControlStringId.MenuColumnGroup, "Group By This Column");
            this.AddString(GridControlStringId.MenuColumnShowGroupPanel, "Show Group Panel");
            this.AddString(GridControlStringId.MenuColumnHideGroupPanel, "Hide Group Panel");
            this.AddString(GridControlStringId.MenuColumnGroupInterval, "Group Interval");
            this.AddString(GridControlStringId.MenuColumnGroupIntervalNone, "None");
            this.AddString(GridControlStringId.MenuColumnGroupIntervalDay, "Day");
            this.AddString(GridControlStringId.MenuColumnGroupIntervalMonth, "Month");
            this.AddString(GridControlStringId.MenuColumnGroupIntervalYear, "Year");
            this.AddString(GridControlStringId.MenuColumnGroupIntervalSmart, "Smart");
            this.AddString(GridControlStringId.MenuColumnResetGroupSummarySort, "Clear Summary Sorting");
            this.AddString(GridControlStringId.MenuColumnSortGroupBySummaryMenu, "Sort By Summary");
            this.AddString(GridControlStringId.MenuColumnGroupSummarySortFormat, "{1} by '{0}' - {2}");
            this.AddString(GridControlStringId.MenuColumnGroupSummaryEditor, "Group Summary Editor...");
            this.AddString(GridControlStringId.MenuColumnShowColumnChooser, "Show Column Chooser");
            this.AddString(GridControlStringId.MenuColumnHideColumnChooser, "Hide Column Chooser");
            this.AddString(GridControlStringId.MenuColumnShowColumnBandChooser, "Show Column/Band Chooser");
            this.AddString(GridControlStringId.MenuColumnHideColumnBandChooser, "Hide Column/Band Chooser");
            this.AddString(GridControlStringId.MenuColumnBestFit, "Best Fit");
            this.AddString(GridControlStringId.MenuColumnBestFitColumns, "Best Fit (all columns)");
            this.AddString(GridControlStringId.MenuColumnUnboundExpressionEditor, "Expression Editor...");
            this.AddString(GridControlStringId.MenuColumnClearFilter, "Clear Filter");
            this.AddString(GridControlStringId.MenuColumnFilterEditor, "Filter Editor...");
            this.AddString(GridControlStringId.MenuColumnFixedStyle, "Fixed Style");
            this.AddString(GridControlStringId.MenuColumnFixedNone, "None");
            this.AddString(GridControlStringId.MenuColumnFixedLeft, "Left");
            this.AddString(GridControlStringId.MenuColumnFixedRight, "Right");
            this.AddString(GridControlStringId.MenuColumnShowSearchPanel, "Show Search Panel");
            this.AddString(GridControlStringId.MenuColumnHideSearchPanel, "Hide Search Panel");
            this.AddString(GridControlStringId.MenuFooterSum, "Sum");
            this.AddString(GridControlStringId.MenuFooterMax, "Max");
            this.AddString(GridControlStringId.MenuFooterMin, "Min");
            this.AddString(GridControlStringId.MenuFooterCount, "Count");
            this.AddString(GridControlStringId.MenuFooterAverage, "Average");
            this.AddString(GridControlStringId.MenuFooterCustom, "Custom");
            this.AddString(GridControlStringId.MenuFooterCustomize, "Customize...");
            this.AddString(GridControlStringId.MenuFooterRowCount, "Show row count");
            this.AddString(GridControlStringId.GroupSummaryEditorFormCaption, "Group Summaries");
            this.AddString(GridControlStringId.TotalSummaryEditorFormCaption, "Totals for '{0}'");
            this.AddString(GridControlStringId.TotalSummaryPanelEditorFormCaption, "View Totals");
            this.AddString(GridControlStringId.SummaryEditorFormItemsTabCaption, "Items");
            this.AddString(GridControlStringId.SummaryEditorFormOrderTabCaption, "Order");
            this.AddString(GridControlStringId.SummaryEditorFormOrderLeftSide, "Left side:");
            this.AddString(GridControlStringId.SummaryEditorFormOrderRightSide, "Right side:");
            this.AddString(GridControlStringId.SummaryEditorFormOrderAndAlignmentTabCaption, "Order and Alignment");
            this.AddString(GridControlStringId.SummaryEditorFormMoveItemUpCaption, "Up");
            this.AddString(GridControlStringId.SummaryEditorFormMoveItemDownCaption, "Down");
            this.AddString(GridControlStringId.FilterEditorTitle, "Filter Editor");
            this.AddString(GridControlStringId.FilterPanelCaptionFormatStringForMasterDetail, "{0} filter:");
            this.AddString(GridControlStringId.GroupPanelDisplayFormatStringForMasterDetail, "{0}:");
            this.AddString(GridControlStringId.ProgressWindowTitle, "Loading data");
            this.AddString(GridControlStringId.ProgressWindowCancel, "Cancel");
            this.AddString(GridControlStringId.ProgressWindowTitleCancelling, "Cancelling");
            this.AddString(GridControlStringId.ErrorPanelTextFormatString, "Error occurred during processing server request ({0})");
            this.AddString(GridControlStringId.SummaryItemsSeparator, ", ");
            this.AddString(GridControlStringId.InvalidValueErrorMessage, "Invalid Value");
            this.AddString(GridControlStringId.CheckboxSelectorColumnCaption, "Selection");
            this.AddString(GridControlStringId.GridCardExpandButtonTooltip, "Expand a card");
            this.AddString(GridControlStringId.GridCardCollapseButtonTooltip, "Collapse a card");
            this.AddString(GridControlStringId.NavigationMoveFirstRow, "First");
            this.AddString(GridControlStringId.NavigationMovePrevPage, "Previous Page");
            this.AddString(GridControlStringId.NavigationMovePrevRow, "Previous");
            this.AddString(GridControlStringId.NavigationMoveNextRow, "Next");
            this.AddString(GridControlStringId.NavigationMoveNextPage, "Next Page");
            this.AddString(GridControlStringId.NavigationMoveLastRow, "Last");
            this.AddString(GridControlStringId.NavigationAddNewRow, "Append");
            this.AddString(GridControlStringId.NavigationDeleteFocusedRow, "Delete");
            this.AddString(GridControlStringId.NavigationEditFocusedRow, "Edit");
            this.AddString(GridControlStringId.NavigationRecord, "Record {0} of {1}");
            this.AddString(GridControlStringId.DateFiltering_ShowAllFilterName, "Show all");
            this.AddString(GridControlStringId.DateFiltering_FilterBySpecificDateFilterName, "Filter by a specific date");
            this.AddString(GridControlStringId.DateFiltering_PriorToThisYearFilterName, "Prior to this year");
            this.AddString(GridControlStringId.DateFiltering_EarlierThisYearFilterName, "Earlier this year");
            this.AddString(GridControlStringId.DateFiltering_EarlierThisMonthFilterName, "Earlier this month");
            this.AddString(GridControlStringId.DateFiltering_LastWeekFilterName, "Last week");
            this.AddString(GridControlStringId.DateFiltering_EarlierThisWeekFilterName, "Earlier this week");
            this.AddString(GridControlStringId.DateFiltering_YesterdayFilterName, "Yesterday");
            this.AddString(GridControlStringId.DateFiltering_TodayFilterName, "Today");
            this.AddString(GridControlStringId.DateFiltering_TomorrowFilterName, "Tomorrow");
            this.AddString(GridControlStringId.DateFiltering_LaterThisWeekFilterName, "Later this week");
            this.AddString(GridControlStringId.DateFiltering_NextWeekFilterName, "Next week");
            this.AddString(GridControlStringId.DateFiltering_LaterThisMonthFilterName, "Later this month");
            this.AddString(GridControlStringId.DateFiltering_LaterThisYearFilterName, "Later this year");
            this.AddString(GridControlStringId.DateFiltering_BeyondThisYearFilterName, "Beyond this year");
            this.AddString(GridControlStringId.DateFiltering_BeyondFilterName, "Beyond");
            this.AddString(GridControlStringId.DateFiltering_ThisWeekFilterName, "This week");
            this.AddString(GridControlStringId.DateFiltering_ThisMonthFilterName, "This month");
            this.AddString(GridControlStringId.DateFiltering_EarlierFilterName, "Earlier");
            this.AddString(GridControlStringId.DateFiltering_EmptyFilterName, "Empty");
            this.AddString(GridControlStringId.DDExtensionsAddRows, "Add rows");
            this.AddString(GridControlStringId.DDExtensionsCannotDropHere, "Cannot drop here");
            this.AddString(GridControlStringId.DDExtensionsDraggingMultipleRows, "Dragging {0} rows");
            this.AddString(GridControlStringId.DDExtensionsDraggingOneRow, "Dragging 1 row:");
            this.AddString(GridControlStringId.DDExtensionsInsertAfter, "Insert after row:");
            this.AddString(GridControlStringId.DDExtensionsInsertBefore, "Insert before row:");
            this.AddString(GridControlStringId.DDExtensionsMoveToChildrenCollection, "Move to children collection:");
            this.AddString(GridControlStringId.DDExtensionsMoveToGroup, "Move to group:");
            this.AddString(GridControlStringId.DDExtensionsRow, "Row");
            this.AddString(GridControlStringId.EditForm_UpdateButton, "Update");
            this.AddString(GridControlStringId.EditForm_CancelButton, "Cancel");
            this.AddString(GridControlStringId.EditForm_Modified, "Your data is modified. Do you want to save the changes?");
            this.AddString(GridControlStringId.EditForm_Cancel, "Do you want to cancel editing?");
            this.AddString(GridControlStringId.EditForm_Warning, "Warning");
            this.AddString(GridControlStringId.MenuRowFixedTop, "Top");
            this.AddString(GridControlStringId.MenuRowFixedNone, "None");
            this.AddString(GridControlStringId.MenuRowFixedBottom, "Bottom");
            this.AddString(GridControlStringId.MenuCompactModeShowInGroups, "Show in Groups");
            this.AddString(GridControlStringId.CompactModeNoneColumn, "None");
            this.AddString(GridControlStringId.CompactModeAscendingOrder, "Ascending");
            this.AddString(GridControlStringId.CompactModeDescendingOrder, "Descending");
            this.AddString(GridControlStringId.CompactModeBy, "By");
            this.AddString(GridControlStringId.CompactModeFilter, "Filter");
            this.AddString(GridControlStringId.MenuCompactModeArrangeBy, "Arrange By");
            this.AddString(GridControlStringId.MenuCompactModeReverseSort, "Reverse Sort");
            this.AddString(GridControlStringId.LoadingRowRetry, "Retry");
            this.AddString(GridControlStringId.LoadingRowLoadMore, "Load More");
            this.AddString(GridControlStringId.NoRecords, "No Records");
            this.AddString(GridControlStringId.NoRecordsFound, "No Records Found");
            this.AddString(GridControlStringId.UpdateRowButtonsUpdate, "Update");
            this.AddString(GridControlStringId.UpdateRowButtonsCancel, "Cancel");
        }
    }
}

