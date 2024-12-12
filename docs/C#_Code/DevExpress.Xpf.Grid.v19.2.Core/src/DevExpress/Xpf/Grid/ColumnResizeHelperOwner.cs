namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Core;
    using System;

    public class ColumnResizeHelperOwner : IResizeHelperOwner
    {
        private IColumnPropertyOwner columnPropertyOwner;
        private double maxWidth = double.MaxValue;

        public ColumnResizeHelperOwner(IColumnPropertyOwner columnPropertyOwner)
        {
            this.columnPropertyOwner = columnPropertyOwner;
        }

        void IResizeHelperOwner.ChangeSize(double delta)
        {
            if (this.columnPropertyOwner.GetActualFixedStyle() == FixedStyle.Right)
            {
                delta = -delta;
            }
            this.SetWidth(this.columnPropertyOwner.ActualWidth + delta);
        }

        void IResizeHelperOwner.OnDoubleClick()
        {
            BaseColumn column = this.Column;
            if (column != null)
            {
                this.View.ViewBehavior.OnColumnResizerDoubleClick(column);
            }
        }

        void IResizeHelperOwner.SetIsResizing(bool isResizing)
        {
            ColumnBase column = this.Column as ColumnBase;
            if (!isResizing)
            {
                this.View.ViewBehavior.OnResizingComplete();
            }
            else if (column != null)
            {
                this.maxWidth = this.View.ViewBehavior.CalcColumnMaxWidth(column);
            }
        }

        private void SetWidth(double value)
        {
            this.View.ApplyResize(this.Column, value, this.maxWidth);
        }

        public BaseColumn Column =>
            this.columnPropertyOwner.Column;

        private DataViewBase View =>
            (this.Column == null) ? null : (this.Column.ResizeOwner as DataViewBase);

        double IResizeHelperOwner.ActualSize
        {
            get => 
                this.Column.HeaderWidth;
            set => 
                this.SetWidth(value);
        }

        SizeHelperBase IResizeHelperOwner.SizeHelper =>
            HorizontalSizeHelper.Instance;
    }
}

