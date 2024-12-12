namespace DevExpress.Xpf.Grid.HitTest
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Windows;

    public abstract class DataViewHitTestAcceptorBase : HitTestAcceptorBase<DataViewHitTestVisitorBase>
    {
        protected DataViewHitTestAcceptorBase()
        {
        }

        internal static int GetRowHandleByElement(FrameworkElement element)
        {
            RowData data = RowData.FindRowData(element);
            return ((data != null) ? data.RowHandle.Value : -2147483648);
        }
    }
}

