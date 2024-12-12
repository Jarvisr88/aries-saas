namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Windows;

    internal class StartPointSelectionRectangleInfo
    {
        private int _startRowHandle;
        private Point _offsetRow;
        private double _startHorizontalOffset;
        private Point _startXY;
        private DataViewBase _view;
        private ColumnBase _column;

        public StartPointSelectionRectangleInfo(int rowHandle, Point offsetRow, double startHorizontalOffset, Point startClickPosition, DataViewBase view, ColumnBase column)
        {
            this._startRowHandle = rowHandle;
            this._offsetRow = offsetRow;
            this._startHorizontalOffset = startHorizontalOffset;
            this._startXY = startClickPosition;
            this._view = view;
            this._column = column;
        }

        public int StartRowHandle =>
            this._startRowHandle;

        public Point OffsetRow =>
            this._offsetRow;

        public double StartHorizontalOffset =>
            this._startHorizontalOffset;

        public Point StartXY =>
            this._startXY;

        public DataViewBase View =>
            this._view;

        public ColumnBase Column =>
            this._column;
    }
}

