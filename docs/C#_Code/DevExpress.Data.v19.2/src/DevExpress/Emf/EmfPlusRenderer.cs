namespace DevExpress.Emf
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public abstract class EmfPlusRenderer
    {
        private readonly IEmfMetafileBuilder builder;
        private readonly EmfPlusClippingStack clippingStack = new EmfPlusClippingStack();

        public EmfPlusRenderer(IEmfMetafileBuilder builder)
        {
            this.builder = builder;
        }

        protected void AppedObjectRecord(byte id, EmfPlusObject obj)
        {
            this.builder.AppendRecord(new EmfPlusObjectRecord(id, obj));
        }

        protected void AppendClipRecord(EmfPlusSetClipRecord record)
        {
            this.builder.AppendRecord(record);
            this.clippingStack.Push(record);
        }

        protected void DrawLines(byte penId, DXPointF[] points, bool isPolygon)
        {
            this.builder.AppendRecord(new EmfPlusDrawLinesRecord(penId, points, isPolygon));
        }

        protected abstract void RemoveLastClipPath();
        public void RestoreClip()
        {
            this.builder.AppendRecord(new EmfPlusResetClipRecord());
            if (this.clippingStack.Pop())
            {
                this.RemoveLastClipPath();
            }
            this.clippingStack.Apply(this.builder);
        }

        public void SetClip(Rectangle rect, CombineMode mode)
        {
            this.AppendClipRecord(new EmfPlusSetClipRectRecord(EmfPlusConverter.ConvertRectangle(rect), (EmfPlusCombineMode) mode));
        }

        protected IEmfMetafileBuilder Builder =>
            this.builder;
    }
}

