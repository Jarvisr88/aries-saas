namespace DevExpress.Office.Drawing
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    internal class TriangleArrowLineCapInfoCreator : StealthArrowLineCapInfoCreator
    {
        private static readonly float[] sizes = new float[] { 2f, 3f, 5f };

        protected override float GetInset(OutlineHeadTailSize width, OutlineHeadTailSize length) => 
            (length == OutlineHeadTailSize.Small) ? 1.5f : ((length == OutlineHeadTailSize.Medium) ? 2.5f : 4.5f);

        protected override void PreparePath(GraphicsPath path, OutlineHeadTailSize widthSize, OutlineHeadTailSize lengthSize, float inset)
        {
            float num = sizes[(int) widthSize];
            float num2 = sizes[(int) lengthSize];
            path.AddLine(new PointF(0.5f * num, -num2), new PointF(0f, 0f));
            path.AddLine(new PointF(0f, 0f), new PointF(-0.5f * num, -num2));
            path.CloseFigure();
        }

        protected override OutlineHeadTailType Type =>
            OutlineHeadTailType.TriangleArrow;
    }
}

