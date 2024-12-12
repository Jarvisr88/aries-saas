namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.ContentGeneration;
    using DevExpress.Text.Fonts;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Media;

    public class GlyphRunDrawingBuilder : DrawingBuilder<GlyphRunDrawing>
    {
        public GlyphRunDrawingBuilder(GlyphRunDrawing drawing) : base(drawing)
        {
        }

        public override void GenerateData(PdfGraphicsCommandConstructor constructor)
        {
            constructor.SaveGraphicsState();
            PdfExportHelper.SetBrush(base.Drawing.ForegroundBrush, base.Drawing.Bounds, constructor);
            WpfExportPlatformFontProvider provider = new WpfExportPlatformFontProvider(this.GlyphRun.GlyphTypeface);
            PdfExportFontInfo fontInfo = new PdfExportFontInfo(constructor.GraphicsDocument.FontCache.GetExportFont(provider), (float) this.GlyphRun.FontRenderingEmSize.ScaleToDpi(((double) constructor.DpiX)));
            PdfWpfGlyphPlacementInfo placementInfo = new PdfWpfGlyphPlacementInfo(this.GlyphRun, fontInfo.FontSize / ((float) this.GlyphRun.FontRenderingEmSize));
            constructor.DrawPrecalculatedString(placementInfo, fontInfo, this.GetGlyphRunLocation((this.GlyphRun.BidiLevel % 2) != 0), PdfGraphicsTextOrigin.Baseline, DXKerningMode.Always);
            constructor.RestoreGraphicsState();
        }

        private PointF GetGlyphRunLocation(bool isRTL)
        {
            PointF tf = this.GlyphRun.BaselineOrigin.ToPointF();
            return (isRTL ? new PointF(tf.X - ((float) ((IEnumerable<double>) this.GlyphRun.AdvanceWidths).Sum()), tf.Y) : tf);
        }

        private System.Windows.Media.GlyphRun GlyphRun =>
            base.Drawing.GlyphRun;
    }
}

