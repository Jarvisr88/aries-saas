namespace DevExpress.XtraPrinting
{
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    [BrickExporter(typeof(CompositeBrickExporter))]
    public class CompositeBrick : Brick, IEnumerable
    {
        private PointF offset;
        private IList<Brick> innerBricks;

        public CompositeBrick()
        {
            this.innerBricks = new List<Brick>();
        }

        public CompositeBrick(IList<Brick> innerBricks, PointF offset)
        {
            this.innerBricks = innerBricks;
            this.offset = offset;
        }

        protected internal override bool AfterPrintOnPage(IList<int> indices, RectangleF brickBounds, RectangleF clipRect, Page page, int pageIndex, int pageCount, Action<BrickBase, RectangleF> callback)
        {
            base.AfterPrintOnPage(indices, brickBounds, clipRect, page, pageIndex, pageCount, callback);
            int count = this.InnerBrickList.Count;
            if (this.InnerBrickList.Count != count)
            {
                this.ResetBrickMap();
            }
            return (count > 0);
        }

        private List<MapItem> CreateBrickMap()
        {
            List<MapItem> list = new List<MapItem>();
            int num = 0;
            while (num < this.InnerBricks.Count)
            {
                RectangleF rect = this.InnerBricks[num].Rect;
                int num2 = num;
                int num3 = num;
                int num4 = 0;
                while (true)
                {
                    if (((num4 + num) >= this.InnerBricks.Count) || (num4 >= 200))
                    {
                        list.Add(new MapItem(rect, num2, num3));
                        num = num3 + 1;
                        break;
                    }
                    rect = RectangleF.Union(rect, this.InnerBricks[num4 + num].Rect);
                    num3 = num4 + num;
                    num4++;
                }
            }
            return list;
        }

        protected override object CreateCollectionItemCore(string propertyName, XtraItemEventArgs e) => 
            (propertyName != "InnerBricks") ? base.CreateCollectionItemCore(propertyName, e) : this.CreateInnerBricksItem(e);

        protected virtual object CreateInnerBricksItem(XtraItemEventArgs e) => 
            BrickFactory.CreateBrick(e);

        internal void ForceBrickMap()
        {
            if ((this.BrickMap == null) || (this.BrickMap.Count == 0))
            {
                this.BrickMap = this.CreateBrickMap();
            }
        }

        public override IEnumerator GetEnumerator() => 
            new BrickEnumerator(this);

        private void ResetBrickMap()
        {
            this.BrickMap = null;
        }

        protected override void SetIndexCollectionItemCore(string propertyName, XtraSetItemIndexEventArgs e)
        {
            if (propertyName == "InnerBricks")
            {
                this.innerBricks.Add((Brick) e.Item.Value);
            }
            base.SetIndexCollectionItemCore(propertyName, e);
        }

        protected override bool ShouldSerializeCore(string propertyName) => 
            (propertyName != "Offset") ? base.ShouldSerializeCore(propertyName) : !this.Offset.IsEmpty;

        IEnumerator IEnumerable.GetEnumerator() => 
            this.InnerBricks.GetEnumerator();

        internal IList<MapItem> BrickMap { get; private set; }

        [XtraSerializableProperty, DefaultValue(false)]
        public override bool RightToLeftLayout
        {
            get => 
                base.flags[BrickBase.bitRightToLeftLayout];
            set => 
                base.flags[BrickBase.bitRightToLeftLayout] = value;
        }

        internal override IList InnerBrickList =>
            (IList) this.innerBricks;

        internal override PointF InnerBrickListOffset =>
            this.offset;

        [Description("Provides access to a collection of bricks contained within the CompositeBrick instance."), XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, false, 0, XtraSerializationFlags.Cached)]
        public virtual IList<Brick> InnerBricks =>
            this.innerBricks;

        [Description("Gets or sets the amounts to adjust the location of the CompositeBrick."), XtraSerializableProperty]
        public PointF Offset
        {
            get => 
                this.offset;
            set => 
                this.offset = value;
        }

        public override string BrickType =>
            "CompositeBrick";
    }
}

