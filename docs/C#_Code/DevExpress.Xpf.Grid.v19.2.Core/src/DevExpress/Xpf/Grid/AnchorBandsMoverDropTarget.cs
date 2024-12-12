namespace DevExpress.Xpf.Grid
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Grid.Native;
    using System;
    using System.Runtime.CompilerServices;

    internal class AnchorBandsMoverDropTarget : IBandsMoverDropTarget
    {
        private ColumnNode anchor;

        public AnchorBandsMoverDropTarget(ColumnNode anchor)
        {
            this.anchor = anchor;
        }

        public void Drop(BandColumnsMoveAdapter moveAdapter, ColumnNode source)
        {
            moveAdapter.Do<BandColumnsMoveAdapter>(x => x.Insert(source, this.anchor, this.DropPlace));
        }

        public IColumnNodeOwner GetNodeOwner() => 
            this.anchor.Owner;

        public BandedViewDropPlace DropPlace { get; set; }

        public bool NeedTransferChildren =>
            false;
    }
}

