namespace DevExpress.Office.Drawing
{
    using System;
    using System.Drawing.Drawing2D;

    internal abstract class LineCapInfoCreatorBase
    {
        protected LineCapInfoCreatorBase()
        {
        }

        protected void AddCirclePath(GraphicsPath path, float radius)
        {
            this.AddEllipsePath(path, radius, radius);
        }

        protected void AddEllipsePath(GraphicsPath path, float width, float length)
        {
            path.AddEllipse(-width, -length, 2f * width, 2f * length);
            path.CloseFigure();
        }

        public LineCapInfo Create(OutlineHeadTailSize width, OutlineHeadTailSize length)
        {
            float inset = this.GetInset(width, length);
            GraphicsPath path = new GraphicsPath(FillMode.Winding);
            this.PreparePath(path, width, length, inset);
            GraphicsPath path2 = new GraphicsPath(FillMode.Winding);
            this.PrepareBoundingPath(path2, width, length);
            return new LineCapInfo(this.Type, path, path2, this.Filled, inset);
        }

        protected abstract float GetInset(OutlineHeadTailSize width, OutlineHeadTailSize length);
        protected abstract void PrepareBoundingPath(GraphicsPath path, OutlineHeadTailSize widthSize, OutlineHeadTailSize lengthSize);
        protected abstract void PreparePath(GraphicsPath path, OutlineHeadTailSize widthSize, OutlineHeadTailSize lengthSize, float inset);

        protected abstract bool Filled { get; }

        protected abstract OutlineHeadTailType Type { get; }
    }
}

