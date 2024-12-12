namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;

    public class PSPage : Page
    {
        private CompositeBrick externalCompositeBrick;
        private BrickList bricks;
        private SizeF bricksSize;
        private bool isAdditional;
        private bool locked;

        public PSPage();
        public PSPage(ReadonlyPageData pageData);
        public PSPage(ReadonlyPageData pageData, bool isAdditional);
        public override void AddBrick(VisualBrick brick);
        public BrickBase AddBricks(ICollection bricks, RectangleF rect);
        public void AddContent(IEnumerable<Brick> bricks);
        private void AddInnerBrick(BrickBase brick, RectangleF rect);
        public virtual void AfterCreate(RectangleF usefulPageArea, IServiceProvider servProvider, bool rightToLeftLayout);
        public void ClearContent();
        private float GetBottomMarginOffset();
        internal float GetClippedPageHeight();
        private static RectangleF GetIntersectedBrickBounds(IList bricks, RectangleF bounds);
        internal float GetTopMarginOffset();
        internal int InsertAfter(Brick brick, Brick previous);
        public void LockContent();
        private void ModifyBricksSize(BrickBase brick);
        internal void RemoveOuterBricks();

        internal bool IsEmpty { get; }

        internal SizeF BricksSize { get; }

        public BrickList Bricks { get; }

        public SizeF ClippedPageSize { get; }

        public bool Additional { get; }

        public RectangleF TopMarginBounds { get; }

        public RectangleF BottomMarginBounds { get; }
    }
}

