namespace DevExpress.XtraPrinting.Export.Imaging
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Export;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;

    public class ImageSinglePageDocumentBuilder : ImageDocumentBuilderBase, IBrickExportVisitor
    {
        private List<BrickWithOffset> bricks;

        public ImageSinglePageDocumentBuilder(PrintingSystemBase ps, ImageExportOptions options) : base(ps, options)
        {
        }

        public override void CreateDocument(Stream stream)
        {
            float[] ranges = new float[] { 1f, 1f };
            base.Ps.ProgressReflector.SetProgressRanges(ranges);
            this.bricks = new List<BrickWithOffset>();
            base.Ps.ProgressReflector.EnsureRangeDecrement(() => base.Ps.Document.GetContinuousExportInfo().ExecuteExport(this, base.Ps));
            base.DocInfo.Update(this.bricks);
            base.CreatePicture(stream);
        }

        void IBrickExportVisitor.ExportBrick(double horizontalOffset, double verticalOffset, Brick brick)
        {
            BrickWithOffset item = new BrickWithOffset(brick, (float) horizontalOffset, (float) verticalOffset);
            this.bricks.Add(item);
        }

        protected internal override void DrawDocument(IGraphics gr)
        {
            using (Brush brush = new SolidBrush(base.GetValidBackColor(base.Ps.Graph.PageBackColor)))
            {
                gr.FillRectangle(brush, 0f, 0f, this.DocumentWidth, this.DocumentHeight);
            }
            base.Ps.ProgressReflector.InitializeRange(this.bricks.Count);
            foreach (BrickWithOffset offset in this.bricks)
            {
                BrickBaseExporter.GetExporter(gr, offset.Brick).Draw(gr, offset.Rect, RectangleF.Empty);
                ProgressReflector progressReflector = base.Ps.ProgressReflector;
                float rangeValue = progressReflector.RangeValue;
                progressReflector.RangeValue = rangeValue + 1f;
            }
        }

        protected internal override void FlushDocument()
        {
            base.Ps.ProgressReflector.MaximizeRange();
        }

        protected internal override DevExpress.XtraPrinting.Export.Imaging.ImageGraphicsFactory ImageGraphicsFactory =>
            DevExpress.XtraPrinting.Export.Imaging.ImageGraphicsFactory.OnePageImageGraphicsFactory;

        protected override float DocumentWidth =>
            base.DocInfo.RightOfBricks;

        protected override float DocumentHeight =>
            base.DocInfo.BottomOfBricks;
    }
}

