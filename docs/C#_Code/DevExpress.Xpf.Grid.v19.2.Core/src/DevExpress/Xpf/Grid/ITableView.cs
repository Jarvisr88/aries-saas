namespace DevExpress.Xpf.Grid
{
    using DevExpress.Export;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using DevExpress.Xpf.Grid.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Media;

    public interface ITableView : IFormatsOwner
    {
        event ScrollBarAnnotationsCreatingEventHandler ScrollBarAnnotationsCreating;

        event EventHandler<ScrollBarCustomRowAnnotationEventArgs> ScrollBarCustomRowAnnotation;

        void AddFormatCondition(FormatConditionBase formatCondition);
        bool AllowMergeEditor(ColumnBase column, int rowHandle1, int rowHandle2);
        void BestFitColumn(BaseColumn column);
        void BestFitColumns();
        ITableViewHitInfo CalcHitInfo(DependencyObject d);
        bool CanRaiseCanUnselectCell();
        void CloseEditForm();
        void CopyCellsToClipboard(IEnumerable<CellBase> cells);
        DevExpress.Xpf.Grid.ScrollBarAnnotationsAppearance CreateDefaultScrollBarAnnotationsAppearance();
        CellBase CreateGridCell(int rowHandle, ColumnBase column);
        CellBase CreateGridCell(int rowHandle, ColumnBase column, DataViewBase view);
        void HideEditForm();
        bool IsCheckBoxSelectorColumn(ColumnBase column);
        void OnChangeBandSeparator();
        void OnPaste();
        bool RaiseCanSelectCell(int rowHandle, ColumnBase column);
        bool RaiseCanUnselectCell(int rowHandle, ColumnBase column);
        bool? RaiseCellMerge(ColumnBase column, int rowHandle1, int rowHandle2, bool checkValues);
        void RaiseClipboardRowCellValuePasting(ClipboardRowCellValuePastingEventArgs e);
        void RaiseClipboardRowPasting(ClipboardRowPastingEventArgs e);
        void RaiseCustomCellAppearance(CustomCellAppearanceEventArgs args);
        void RaiseCustomRowAppearance(CustomRowAppearanceEventArgs args);
        void RaiseEditFormShowing(EditFormShowingEventArgs args);
        void RaiseRowDoubleClickEvent(ITableViewHitInfo hitInfo, MouseButton changedButton);
        ScrollBarAnnotationsCreatingEventArgs RaiseScrollBarAnnotationsCreating();
        void RaiseScrollBarCustomRowAnnotation(ScrollBarCustomRowAnnotationEventArgs e);
        void ScrollBarAnnotationInfoRangeChanged();
        void SelectCells(int startRowHandle, ColumnBase startColumn, int endRowHandle, ColumnBase endColumn);
        void SetActualDetailMargin(Thickness detailMargin);
        void SetActualExpandDetailHeaderWidth(double expandDetailButtonWidth);
        void SetActualFadeSelectionOnLostFocus(bool fadeSelectionOnLostFocus);
        void SetActualIndicatorWidth(double indicatorWidth);
        void SetActualShowIndicator(bool showIndicator);
        void SetExpandColumnPosition(ColumnPosition position);
        void SetFixedLeftVisibleColumns(IList<ColumnBase> columns);
        void SetFixedNoneVisibleColumns(IList<ColumnBase> columns);
        void SetFixedRightVisibleColumns(IList<ColumnBase> columns);
        void SetHorizontalViewport(double value);
        void SetIsCompactMode(bool val);
        void SetShowTotalSummaryIndicatorIndent(bool showTotalSummaryIndicatorIndent);
        void ShowDialogEditForm();
        void ShowEditForm();
        void ShowFormatConditionDialog(ColumnBase column, FormatConditionDialogType dialogKind);
        void ShowInlineEditForm();
        void UnsubscribeFilterItemsSource(IEnumerable filterItems);
        bool UseRowDetailsTemplate(int rowHandle);

        bool AllowResizing { get; }

        bool AllowBestFit { get; }

        bool AutoWidth { get; }

        bool IsEditing { get; }

        bool ShowAutoFilterRow { get; }

        Style AutoFilterRowCellStyle { get; }

        bool AllowCascadeUpdate { get; }

        bool AllowPerPixelScrolling { get; }

        bool AllowScrollHeaders { get; }

        double ScrollAnimationDuration { get; }

        DevExpress.Xpf.Grid.ScrollAnimationMode ScrollAnimationMode { get; }

        bool AllowScrollAnimation { get; }

        bool ShowIndicator { get; set; }

        bool ActualShowIndicator { get; }

        double ActualIndicatorWidth { get; }

        bool UseIndicatorForSelection { get; }

        bool AllowHorizontalScrollingVirtualization { get; }

        bool AutoMoveRowFocus { get; }

        bool AllowFixedColumnMenu { get; set; }

        bool ShowVerticalLines { get; set; }

        bool ShowHorizontalLines { get; set; }

        bool ActualShowDetailButtons { get; }

        bool ActualShowDetailHeader { get; }

        bool IsCheckBoxSelectorColumnVisible { get; }

        double LeftGroupAreaIndent { get; }

        double RightGroupAreaIndent { get; }

        double LeftDataAreaIndent { get; }

        double RightDataAreaIndent { get; }

        double FixedNoneContentWidth { get; set; }

        double TotalSummaryFixedNoneContentWidth { get; set; }

        double VerticalScrollBarWidth { get; set; }

        double FixedLeftContentWidth { get; set; }

        double FixedRightContentWidth { get; set; }

        double TotalGroupAreaIndent { get; set; }

        double IndicatorWidth { get; set; }

        double IndicatorHeaderWidth { get; set; }

        double FixedLineWidth { get; set; }

        double HorizontalViewport { get; }

        double ActualExpandDetailButtonWidth { get; }

        Thickness ActualDetailMargin { get; }

        double ActualExpandDetailHeaderWidth { get; }

        Thickness ScrollingVirtualizationMargin { get; set; }

        Thickness ScrollingHeaderVirtualizationMargin { get; set; }

        DevExpress.Xpf.Grid.Native.TableViewBehavior TableViewBehavior { get; }

        IList<ColumnBase> ViewportVisibleColumns { get; set; }

        DataViewBase ViewBase { get; }

        ControlTemplate FocusedRowBorderTemplate { get; }

        ControlTemplate ColumnBandChooserTemplate { get; }

        DependencyPropertyKey ActualDataRowTemplateSelectorPropertyKey { get; }

        DataTemplateSelector DataRowTemplateSelector { get; }

        DataTemplate DataRowTemplate { get; set; }

        ControlTemplate RowDecorationTemplate { get; set; }

        DataTemplateSelector ActualDataRowTemplateSelector { get; }

        bool PrintAutoWidth { get; }

        bool PrintColumnHeaders { get; }

        bool PrintBandHeaders { get; }

        bool PrintTotalSummary { get; }

        bool PrintFixedTotalSummary { get; }

        Style PrintColumnHeaderStyle { get; }

        Style PrintCellStyle { get; }

        Style PrintTotalSummaryStyle { get; }

        Style PrintFixedTotalSummaryStyle { get; }

        Style PrintRowIndentStyle { get; }

        int BestFitMaxRowCount { get; }

        DevExpress.Xpf.Core.BestFitMode BestFitMode { get; }

        double RowMinHeight { get; set; }

        DevExpress.Xpf.Grid.BestFitArea BestFitArea { get; }

        bool ShowBandsPanel { get; }

        bool AllowChangeColumnParent { get; set; }

        bool AllowChangeBandParent { get; }

        bool ShowBandsInCustomizationForm { get; }

        bool AllowBandMoving { get; }

        bool AllowBandResizing { get; }

        bool AllowAdvancedVerticalNavigation { get; }

        bool AllowAdvancedHorizontalNavigation { get; }

        IComparer<BandBase> ColumnChooserBandsSortOrderComparer { get; }

        DataTemplate BandHeaderTemplate { get; }

        DataTemplateSelector BandHeaderTemplateSelector { get; }

        DataTemplate BandHeaderToolTipTemplate { get; }

        Style PrintBandHeaderStyle { get; }

        int AlternationCount { get; set; }

        Brush AlternateRowBackground { get; set; }

        Brush EvenRowBackground { get; set; }

        bool UseEvenRowBackground { get; set; }

        DataTemplate RowIndicatorContentTemplate { get; set; }

        Style RowStyle { get; set; }

        bool ActualAllowTreeIndentScrolling { get; }

        int EditFormColumnCount { get; set; }

        DevExpress.Xpf.Grid.EditFormPostMode EditFormPostMode { get; set; }

        DevExpress.Xpf.Grid.EditFormShowMode EditFormShowMode { get; set; }

        DataTemplate EditFormDialogServiceTemplate { get; set; }

        DataTemplate EditFormTemplate { get; set; }

        bool ShowEditFormUpdateCancelButtons { get; set; }

        bool ShowEditFormOnF2Key { get; set; }

        bool ShowEditFormOnEnterKey { get; set; }

        bool ShowEditFormOnDoubleClick { get; set; }

        BindingBase EditFormCaptionBinding { get; set; }

        PostConfirmationMode EditFormPostConfirmation { get; set; }

        bool NewItemRowIsDisplayed { get; }

        DevExpress.Xpf.Grid.ScrollBarAnnotationMode? ScrollBarAnnotationMode { get; set; }

        DevExpress.Xpf.Grid.ScrollBarAnnotationMode ScrollBarAnnotationModeActual { get; }

        DevExpress.Xpf.Grid.ScrollBarAnnotationsAppearance ScrollBarAnnotationsAppearance { get; set; }

        IEnumerable<ScrollBarAnnotationRowInfo> ScrollBarAnnotationInfoRange { get; }

        DevExpress.Xpf.Grid.ScrollBarAnnotationsManager ScrollBarAnnotationsManager { get; }

        bool NeedCustomScrollBarAnnotation { get; }

        bool AllowBandMultiRow { get; set; }

        bool ShowCriteriaInAutoFilterRow { get; set; }

        RowData NewItemRowData { get; }

        RowData AutoFilterRowData { get; }

        bool AllowCellMerge { get; set; }

        bool HasDetailViews { get; }

        bool AllowPrintColumnHeaderImage { get; }

        DevExpress.Export.PasteMode PasteMode { get; set; }

        DevExpress.Export.PasteMode ActualPasteMode { get; }

        bool AllowClipboardPaste { get; }

        DataTemplate DataRowCompactTemplate { get; set; }

        double SwitchToCompactModeWidth { get; set; }

        DevExpress.Xpf.Grid.CompactPanelShowMode CompactPanelShowMode { get; set; }

        ObservableCollection<ICustomItem> CompactModeFilterItems { get; }

        IEnumerable CompactModeFilterItemsSource { get; set; }

        DevExpress.Xpf.Grid.CompactFilterElementShowMode CompactFilterElementShowMode { get; set; }

        DevExpress.Xpf.Grid.CompactSortElementShowMode CompactSortElementShowMode { get; set; }

        double BandSeparatorWidth { get; set; }

        Brush BandCellSeparatorColor { get; set; }

        Brush BandHeaderSeparatorColor { get; set; }

        bool KeepViewportOnDataUpdate { get; set; }

        bool HighlightItemOnHover { get; set; }

        Style UpdateRowRectangleStyle { get; set; }

        bool ShowDataNavigator { get; set; }

        FormatConditionCollection FormatConditions { get; }

        DevExpress.Xpf.Grid.UseLightweightTemplates? UseLightweightTemplates { get; set; }

        DataTemplate RowDetailsTemplate { get; set; }

        DataTemplateSelector RowDetailsTemplateSelector { get; set; }

        DataTemplateSelector ActualRowDetailsTemplateSelector { get; }

        DependencyPropertyKey ActualRowDetailsTemplateSelectorPropertyKey { get; }

        DataTemplate UpdateRowButtonsTemplate { get; set; }

        DevExpress.Xpf.Grid.RowDetailsVisibilityMode RowDetailsVisibilityMode { get; set; }

        DataTemplate FormatConditionDialogServiceTemplate { get; set; }

        DataTemplate ConditionalFormattingManagerServiceTemplate { get; set; }

        bool AllowConditionalFormattingManager { get; set; }

        bool AllowConditionalFormattingMenu { get; set; }

        bool AnimateConditionalFormattingTransition { get; set; }

        bool AllowDataUpdateFormatConditionMenu { get; set; }

        DataTemplate FormatConditionGeneratorTemplate { get; set; }

        IEnumerable FormatConditionsSource { get; set; }

        DataTemplateSelector FormatConditionGeneratorTemplateSelector { get; set; }

        bool HasCustomCellAppearance { get; }

        bool HasCustomRowAppearance { get; }

        Duration ConditionalFormattingTransitionDuration { get; set; }

        Duration DataUpdateAnimationShowDuration { get; set; }

        Duration DataUpdateAnimationHoldDuration { get; set; }

        Duration DataUpdateAnimationHideDuration { get; set; }

        bool UseConstantDataBarAnimationSpeed { get; set; }
    }
}

