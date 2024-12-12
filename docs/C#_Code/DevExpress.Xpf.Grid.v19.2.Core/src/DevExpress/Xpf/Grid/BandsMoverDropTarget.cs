namespace DevExpress.Xpf.Grid
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Runtime.CompilerServices;

    internal class BandsMoverDropTarget : IBandsMoverDropTarget
    {
        private ColumnNode collectionOwner;

        public BandsMoverDropTarget(ColumnNode collectionOwner)
        {
            this.collectionOwner = collectionOwner;
        }

        public void Drop(BandColumnsMoveAdapter moveAdapter, ColumnNode source)
        {
            moveAdapter.Do<BandColumnsMoveAdapter>(x => x.Add(source));
        }

        public IColumnNodeOwner GetNodeOwner() => 
            this.collectionOwner;

        public bool NeedTransferChildren { get; set; }
    }
}

