namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Windows;

    public class NullGridCellEditorOwner : IGridCellEditorOwner
    {
        public static IGridCellEditorOwner Instance = new NullGridCellEditorOwner();

        private NullGridCellEditorOwner()
        {
        }

        void IGridCellEditorOwner.OnViewChanged()
        {
        }

        void IGridCellEditorOwner.SetIsFocusedCell(bool isFocusedCell)
        {
        }

        void IGridCellEditorOwner.SetSelectionState(SelectionState state)
        {
        }

        void IGridCellEditorOwner.SynProperties(GridCellData cellData)
        {
        }

        void IGridCellEditorOwner.UpdateCellBackgroundAppearance()
        {
        }

        void IGridCellEditorOwner.UpdateCellForegroundAppearance()
        {
        }

        void IGridCellEditorOwner.UpdateCellState()
        {
        }

        void IGridCellEditorOwner.UpdateIsEnabled(bool isEnabled)
        {
        }

        void IGridCellEditorOwner.UpdateIsReady()
        {
        }

        DependencyObject IGridCellEditorOwner.EditorRoot =>
            null;

        ColumnBase IGridCellEditorOwner.AssociatedColumn =>
            null;

        double IGridCellEditorOwner.ActualHeight =>
            0.0;

        bool IGridCellEditorOwner.CanRefreshContent =>
            false;
    }
}

