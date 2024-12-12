namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;

    public class MouseMoveSelectionNone : MouseMoveSelectionBase
    {
        public static readonly MouseMoveSelectionNone Instance = new MouseMoveSelectionNone();

        public override void CaptureMouse(DataViewBase tableView)
        {
        }

        public override void OnMouseDown(DataViewBase tableView, IDataViewHitInfo hitInfo)
        {
        }

        public override void OnMouseUp(DataViewBase tableView)
        {
        }

        public override void UpdateSelection(ITableView tableView)
        {
        }

        public override bool CanScrollHorizontally =>
            false;

        public override bool CanScrollVertically =>
            false;

        public override bool AllowNavigation =>
            true;
    }
}

