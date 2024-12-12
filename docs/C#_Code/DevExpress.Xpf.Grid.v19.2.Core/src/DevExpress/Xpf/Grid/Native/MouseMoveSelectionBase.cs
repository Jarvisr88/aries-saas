namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Grid;
    using System;

    public abstract class MouseMoveSelectionBase
    {
        protected MouseMoveSelectionBase()
        {
        }

        public abstract void CaptureMouse(DataViewBase tableView);
        public abstract void OnMouseDown(DataViewBase tableView, IDataViewHitInfo hitInfo);
        public abstract void OnMouseUp(DataViewBase tableView);
        public virtual void ReleaseMouseCapture(DataViewBase tableView)
        {
            DataViewBase rootView = tableView.RootView;
            if (ReferenceEquals(MouseHelper.Captured, rootView))
            {
                MouseHelper.ReleaseCapture(rootView);
            }
        }

        public abstract void UpdateSelection(ITableView tableView);

        public abstract bool CanScrollHorizontally { get; }

        public abstract bool CanScrollVertically { get; }

        public abstract bool AllowNavigation { get; }
    }
}

