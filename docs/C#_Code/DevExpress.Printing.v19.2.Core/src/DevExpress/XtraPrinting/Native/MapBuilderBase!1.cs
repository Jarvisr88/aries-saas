namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;

    public abstract class MapBuilderBase<T> where T: IMapNode<T>
    {
        protected MapBuilderBase();
        public T BuildMap(BrickBase brick);
        private void CollectMapNodes(BrickBase brick, T brickNode, RectangleF parentRelativeRect, RectangleF pageAbsoluteRect, BrickVisibleArea visibleArea);
        protected abstract T CreateNode(RectangleF rect, BrickBase brick, string indexes, RectangleF absoluteRect, BrickVisibleArea visibleArea);
        protected virtual void EnumerateBricks();
        private BrickVisibleArea GetBrickVisibleArea(BrickVisibleArea parentArea, RectangleF childAbsoluteRect);
    }
}

