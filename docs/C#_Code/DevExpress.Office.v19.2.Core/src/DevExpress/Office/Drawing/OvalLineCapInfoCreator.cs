namespace DevExpress.Office.Drawing
{
    using System;
    using System.Drawing.Drawing2D;

    internal class OvalLineCapInfoCreator : DiamondLineCapInfoCreator
    {
        private static readonly float[] sizes = new float[] { 2f, 3f, 5f };

        protected override void PreparePath(GraphicsPath path, OutlineHeadTailSize widthSize, OutlineHeadTailSize lengthSize, float inset)
        {
            base.AddEllipsePath(path, 0.5f * sizes[(int) widthSize], 0.5f * sizes[(int) lengthSize]);
        }

        protected override OutlineHeadTailType Type =>
            OutlineHeadTailType.Oval;
    }
}

