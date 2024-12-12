namespace DevExpress.Xpf.Grid
{
    using System;

    public interface IDataViewHitInfo
    {
        void SetBand(BandBase band);
        void SetColumn(ColumnBase column);
        void SetHitTest(TableViewHitTest hitTest);
        void SetRowHandle(int rowHandle);

        bool InColumnHeader { get; }

        bool InGroupPanel { get; }

        bool InRow { get; }

        bool IsRowCell { get; }

        int RowHandle { get; }

        ColumnBase Column { get; }

        BandBase Band { get; }

        bool IsDataArea { get; }
    }
}

