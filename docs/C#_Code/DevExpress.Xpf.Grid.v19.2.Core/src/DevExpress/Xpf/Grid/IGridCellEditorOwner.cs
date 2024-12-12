namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Windows;

    public interface IGridCellEditorOwner
    {
        void OnViewChanged();
        void SetIsFocusedCell(bool isFocusedCell);
        void SetSelectionState(SelectionState state);
        void SynProperties(GridCellData cellData);
        void UpdateCellBackgroundAppearance();
        void UpdateCellForegroundAppearance();
        void UpdateCellState();
        void UpdateIsEnabled(bool isEnabled);
        void UpdateIsReady();

        DependencyObject EditorRoot { get; }

        ColumnBase AssociatedColumn { get; }

        double ActualHeight { get; }

        bool CanRefreshContent { get; }
    }
}

