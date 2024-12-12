namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Data;
    using DevExpress.Xpf.Grid.Native;
    using System;
    using System.Collections.Generic;
    using System.Windows;

    public interface IRowStateClient
    {
        IAnimationConnector CreateAnimationConnector();
        void InvalidateCellsPanel();
        void UpdateAlternateBackground();
        void UpdateAppearance();
        bool UpdateButtonIsFocused();
        void UpdateButtonTabPress(bool prev);
        void UpdateCellsPanel();
        void UpdateCommitRow();
        void UpdateCompactMode(DataTemplate template);
        void UpdateContent();
        void UpdateDetailExpandButtonVisibility();
        void UpdateDetails();
        void UpdateDetailViewIndents();
        void UpdateFixedLeftBands();
        void UpdateFixedLeftCellData(IList<GridColumnData> oldValue);
        void UpdateFixedLineHeight();
        void UpdateFixedLineVisibility();
        void UpdateFixedLineWidth();
        void UpdateFixedNoneBands();
        void UpdateFixedNoneCellData();
        void UpdateFixedNoneContentWidth();
        void UpdateFixedRightBands();
        void UpdateFixedRightCellData(IList<GridColumnData> oldValue);
        void UpdateFixedRow();
        void UpdateFixRowButtonVisibility();
        void UpdateFixRowButtonWidth();
        void UpdateFocusWithinState();
        void UpdateHorizontalLineVisibility();
        void UpdateIndentScrolling();
        void UpdateIndicatorContentTemplate();
        void UpdateIndicatorState();
        void UpdateIndicatorWidth();
        void UpdateInlineEditForm();
        void UpdateIsFocused();
        void UpdateLevel();
        void UpdateMinHeight();
        void UpdateOffsetLevel();
        void UpdateRowHandle(RowHandle rowHandle);
        void UpdateRowPosition();
        void UpdateRowStyle();
        void UpdateScrollingMargin();
        void UpdateSelectionState(SelectionState selectionState);
        void UpdateShowIndicator();
        void UpdateShowRowBreak();
        void UpdateValidationError();
        void UpdateVerticalLineVisibility();
        void UpdateView();
    }
}

