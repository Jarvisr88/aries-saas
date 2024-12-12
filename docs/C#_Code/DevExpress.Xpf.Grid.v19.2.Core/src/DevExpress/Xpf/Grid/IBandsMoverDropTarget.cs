namespace DevExpress.Xpf.Grid
{
    using System;

    internal interface IBandsMoverDropTarget
    {
        void Drop(BandColumnsMoveAdapter moveAdapter, ColumnNode source);
        IColumnNodeOwner GetNodeOwner();

        bool NeedTransferChildren { get; }
    }
}

