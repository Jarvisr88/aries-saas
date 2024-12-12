namespace DevExpress.Office.Drawing
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    internal class DiamondLineCapInfoCreator : LineCapInfoCreatorBase
    {
        private static readonly float[] sizes = new float[] { 1f, 1.5f, 2.5f };

        private OutlineHeadTailSize GetBiggestSize(OutlineHeadTailSize widthSize, OutlineHeadTailSize lengthSize) => 
            ((lengthSize == OutlineHeadTailSize.Large) || (widthSize == OutlineHeadTailSize.Large)) ? OutlineHeadTailSize.Large : (((lengthSize == OutlineHeadTailSize.Medium) || (widthSize == OutlineHeadTailSize.Medium)) ? OutlineHeadTailSize.Medium : OutlineHeadTailSize.Small);

        protected override float GetInset(OutlineHeadTailSize width, OutlineHeadTailSize length) => 
            0f;

        protected override void PrepareBoundingPath(GraphicsPath path, OutlineHeadTailSize widthSize, OutlineHeadTailSize lengthSize)
        {
            OutlineHeadTailSize biggestSize = this.GetBiggestSize(widthSize, lengthSize);
            base.AddCirclePath(path, sizes[(int) biggestSize]);
        }

        protected override void PreparePath(GraphicsPath path, OutlineHeadTailSize widthSize, OutlineHeadTailSize lengthSize, float inset)
        {
            float x = sizes[(int) widthSize];
            float y = sizes[(int) lengthSize];
            PointF[] points = new PointF[] { new PointF(x, 0f), new PointF(0f, y), new PointF(-x, 0f), new PointF(0f, -y) };
            path.AddLines(points);
            path.CloseFigure();
        }

        protected override bool Filled =>
            true;

        protected override OutlineHeadTailType Type =>
            OutlineHeadTailType.Diamond;
    }
}

