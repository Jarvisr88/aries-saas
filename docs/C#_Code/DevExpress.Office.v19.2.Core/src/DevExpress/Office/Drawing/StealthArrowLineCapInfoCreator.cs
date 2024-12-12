namespace DevExpress.Office.Drawing
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    internal class StealthArrowLineCapInfoCreator : LineCapInfoCreatorBase
    {
        private static readonly float[] widths = new float[] { 1f, 1.5f, 2.5f };
        private static readonly float[] lengths = new float[] { 2f, 3f, 5f };
        private static readonly float[] boundingInsets = new float[] { 0f, 0.25f, 0.85f, -0.15f, 0f, 0.5f, -0.4f, -0.3f, 0.03f };

        protected override float GetInset(OutlineHeadTailSize width, OutlineHeadTailSize length) => 
            (length == OutlineHeadTailSize.Small) ? 1f : ((length == OutlineHeadTailSize.Medium) ? 2f : 3f);

        protected override void PrepareBoundingPath(GraphicsPath path, OutlineHeadTailSize widthSize, OutlineHeadTailSize lengthSize)
        {
            int index = (int) (((OutlineHeadTailSize.Large | OutlineHeadTailSize.Medium) * lengthSize) + widthSize);
            float radius = lengths[(int) lengthSize] + boundingInsets[index];
            base.AddCirclePath(path, radius);
        }

        protected override void PreparePath(GraphicsPath path, OutlineHeadTailSize widthSize, OutlineHeadTailSize lengthSize, float inset)
        {
            float x = widths[(int) widthSize];
            float num2 = lengths[(int) lengthSize];
            PointF[] points = new PointF[] { new PointF(0f, -inset), new PointF(x, -num2), new PointF(0f, 0f), new PointF(-x, -num2) };
            path.AddLines(points);
            path.CloseFigure();
        }

        protected override bool Filled =>
            true;

        protected override OutlineHeadTailType Type =>
            OutlineHeadTailType.StealthArrow;
    }
}

