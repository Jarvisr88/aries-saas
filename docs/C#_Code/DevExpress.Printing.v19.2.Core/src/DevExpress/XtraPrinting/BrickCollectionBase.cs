namespace DevExpress.XtraPrinting
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class BrickCollectionBase : Collection<Brick>
    {
        private PanelBrick owner;

        public BrickCollectionBase(PanelBrick owner)
        {
            this.owner = owner;
        }

        protected BrickCollectionBase(PanelBrick owner, IList<Brick> list) : base(list)
        {
            this.owner = owner;
        }

        protected override void InsertItem(int index, Brick item)
        {
            if (item != null)
            {
                base.InsertItem(index, item);
                this.owner.InitializeBrick(item, true);
            }
        }
    }
}

