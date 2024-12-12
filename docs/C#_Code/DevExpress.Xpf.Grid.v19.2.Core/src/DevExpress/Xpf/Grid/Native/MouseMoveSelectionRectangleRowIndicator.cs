namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;

    public class MouseMoveSelectionRectangleRowIndicator : MouseMoveSelectionRectangleBase
    {
        public static readonly MouseMoveSelectionRectangleRowIndicator Instance = new MouseMoveSelectionRectangleRowIndicator();

        private KeyValuePair<DataViewBase, int> GetLastRowHandle(DataViewBase view)
        {
            DataViewBase rootView = view.RootView;
            if ((rootView != null) && ((rootView.DataControl != null) && (rootView.DataProviderBase != null)))
            {
                DataViewBase base3 = null;
                int visibleIndex = 0;
                rootView.GetLastScrollRowViewAndVisibleIndex(out base3, out visibleIndex);
                if ((base3 != null) && (base3.DataControl != null))
                {
                    return new KeyValuePair<DataViewBase, int>(base3, base3.DataControl.GetRowHandleByVisibleIndexCore(visibleIndex));
                }
            }
            return new KeyValuePair<DataViewBase, int>();
        }

        public override void OnMouseDown(DataViewBase tableView, IDataViewHitInfo hitInfo)
        {
            int rowHandle = 0;
            if (!hitInfo.IsDataArea || (hitInfo.RowHandle != -2147483648))
            {
                rowHandle = hitInfo.RowHandle;
            }
            else
            {
                KeyValuePair<DataViewBase, int> lastRowHandle = this.GetLastRowHandle(tableView);
                if ((lastRowHandle.Key == null) || (lastRowHandle.Key.DataControl == null))
                {
                    return;
                }
                tableView = lastRowHandle.Key;
                rowHandle = lastRowHandle.Value;
            }
            base.OnMouseDownCore(tableView, rowHandle, hitInfo.Column, hitInfo.RowHandle == -2147483648);
        }

        public override bool CanScrollHorizontally =>
            false;
    }
}

