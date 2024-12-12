namespace DevExpress.XtraPrinting
{
    using System;

    public class EmptyBrickCollection : BrickCollectionBase
    {
        public static readonly EmptyBrickCollection Instance = new EmptyBrickCollection(null);

        private EmptyBrickCollection(PanelBrick owner) : base(owner)
        {
        }

        protected override void InsertItem(int index, Brick item)
        {
            throw new NotSupportedException();
        }

        protected override void RemoveItem(int index)
        {
            throw new NotSupportedException();
        }
    }
}

