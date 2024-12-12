namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;

    internal class SelectionStrategyNavigationNone : SelectionStrategyBase
    {
        public SelectionStrategyNavigationNone(DataViewBase view) : base(view)
        {
        }

        public override SelectionState GetRowSelectionState(int rowHandle) => 
            SelectionState.None;
    }
}

