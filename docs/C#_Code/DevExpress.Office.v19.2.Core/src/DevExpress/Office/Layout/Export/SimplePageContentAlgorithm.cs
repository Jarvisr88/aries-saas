namespace DevExpress.Office.Layout.Export
{
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class SimplePageContentAlgorithm : IPageContentAlgorithm
    {
        private RectangleF bounds = RectangleF.Empty;
        private IListWrapper<Brick> bricks;

        public void OnBeforeBuildPages()
        {
            this.bricks = null;
        }

        public IList<Brick> Process(IListWrapper<Brick> bricks, RectangleF bounds)
        {
            if (ReferenceEquals(this.bricks, bricks))
            {
                return new Brick[0];
            }
            this.bricks = bricks;
            this.bounds = bounds;
            return new List<Brick>(bricks);
        }

        public RectangleF ContentBounds =>
            this.bounds;

        public bool ClipContent =>
            false;
    }
}

