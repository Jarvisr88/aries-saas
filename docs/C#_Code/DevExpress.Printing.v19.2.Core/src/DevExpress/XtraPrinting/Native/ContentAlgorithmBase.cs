namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public abstract class ContentAlgorithmBase : IPageContentAlgorithm
    {
        protected IListWrapper<Brick> bricks;
        protected RectangleF initialBounds;
        protected RectangleF bounds;
        protected List<Brick> addedPageBricks;
        protected List<Brick> newPageBricks;
        protected List<ContentAlgorithmBase.BrickItem> intersectItems;
        private ContentAlgorithmBase.BrickItem intersectItem;

        protected ContentAlgorithmBase();
        protected virtual void AddBricks(List<Brick> bricks);
        protected abstract bool ContainsFunction(RectangleF rect1, RectangleF rect2);
        protected BrickContainer CreateBrickContainer(Brick brick, RectangleF rect);
        protected abstract void FillPage(out float maxBrickBound);
        protected void FillPageAdditional();
        protected abstract float GetBrickBound(Brick brick, bool forceSplit, float maxBrickBound);
        protected virtual ContentAlgorithmBase.BrickItem GetIntersectItem();
        protected abstract float GetMaxBound(RectangleF rect);
        protected abstract bool IntersectFunction(RectangleF rect1, RectangleF rect2);
        public void OnBeforeBuildPages();
        protected virtual void OnIntersectedBrickAdded(Brick brick, float brickBound);
        public IList<Brick> Process(IListWrapper<Brick> bricks, RectangleF bounds);
        protected bool ProcessCore();
        protected virtual bool ShouldFillNextPage();
        private bool TryFillPage(out float maxBrickBound);
        private bool TryFillPageAdditional(float newBound, float maxBrickBound);

        protected abstract float MinBound { get; }

        protected abstract float MaxBound { get; set; }

        public RectangleF ContentBounds { get; }

        public bool ClipContent { get; }

        protected virtual float DefaultMaxBound { get; }

        protected ContentAlgorithmBase.BrickItem IntersectItem { get; }

        protected float IntersectBound { get; }

        protected class BrickItem
        {
            public BrickItem(DevExpress.XtraPrinting.Brick brick, DevExpress.XtraPrinting.Brick previous);

            public DevExpress.XtraPrinting.Brick Brick { get; private set; }

            public DevExpress.XtraPrinting.Brick Previous { get; private set; }

            public float Bound { get; set; }
        }
    }
}

