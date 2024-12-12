namespace DevExpress.Office.Drawing
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    internal class ArrowLineCapInfoCreator : LineCapInfoCreatorBase
    {
        private static readonly float[] widths = new float[] { 2.5f, 3.5f, 5f };
        private static readonly float[] lengths = new float[] { 2f, 3f, 4.5f };
        private static readonly float[] insets = new float[] { 1f, 0.9f, 0.7f, 1.2f, 1f, 0.8f, 1.8f, 1.4f, 1f };
        private static readonly float[] boundingInsets = new float[] { 1.8f, 1.4f, 1.3f, 2.2f, 1.5f, 1.1f, 3.1f, 1.7f, 0.9f };

        protected override float GetInset(OutlineHeadTailSize widthSize, OutlineHeadTailSize lengthSize)
        {
            int index = (int) (((OutlineHeadTailSize.Large | OutlineHeadTailSize.Medium) * lengthSize) + widthSize);
            return insets[index];
        }

        protected override void PrepareBoundingPath(GraphicsPath path, OutlineHeadTailSize widthSize, OutlineHeadTailSize lengthSize)
        {
            int index = (int) (((OutlineHeadTailSize.Large | OutlineHeadTailSize.Medium) * lengthSize) + widthSize);
            float radius = lengths[(int) lengthSize] + boundingInsets[index];
            base.AddCirclePath(path, radius);
        }

        protected override void PreparePath(GraphicsPath path, OutlineHeadTailSize widthSize, OutlineHeadTailSize lengthSize, float inset)
        {
            float num = widths[(int) widthSize];
            float num2 = lengths[(int) lengthSize];
            path.AddLine(new PointF(num / 2f, -num2 - inset), new PointF(0f, -inset));
            path.AddLine(new PointF(0f, -inset), new PointF(-num / 2f, -num2 - inset));
        }

        protected override bool Filled =>
            false;

        protected override OutlineHeadTailType Type =>
            OutlineHeadTailType.Arrow;
    }
}

