namespace DevExpress.Xpf.Grid
{
    using System;

    internal class ColumnNodeCollectionFactory
    {
        public virtual ColumnNodeCollection CreateBandCollection(IColumnNodeOwner owner) => 
            this.CreateDefaultColumnNodeCollection(owner);

        public virtual ColumnNodeCollection CreateColumnCollection(IColumnNodeOwner owner) => 
            this.CreateDefaultColumnNodeCollection(owner);

        private ColumnNodeCollection CreateDefaultColumnNodeCollection(IColumnNodeOwner owner)
        {
            ColumnNodeCollection collection1 = new ColumnNodeCollection();
            collection1.Owner = owner;
            return collection1;
        }
    }
}

