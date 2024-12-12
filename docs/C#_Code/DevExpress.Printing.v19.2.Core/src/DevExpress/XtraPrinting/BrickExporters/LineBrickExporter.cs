namespace DevExpress.XtraPrinting.BrickExporters
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class LineBrickExporter : VisualBrickExporter
    {
        private void AddBrickViewData(List<BrickViewData> data, ExportContext htmlExportContext, RectangleF rect, BrickStyle style);
        private static Rectangle AlignHorz(Rectangle rect, Rectangle baseRect, BrickAlignment alignment);
        private static Rectangle AlignVert(Rectangle rect, Rectangle baseRect, BrickAlignment alignment);
        private static Rectangle CenterInt(Rectangle rect, Rectangle baseRect);
        protected override Image CreateTableCellImage(ITableExportProvider exportProvider, RectangleF rect);
        protected override void DrawClientContent(IGraphics gr, RectangleF clientRect);
        protected override Image DrawContentToImage(ExportContext exportContext, RectangleF rect);
        protected override void DrawObject(IGraphics gr, RectangleF rect);
        protected internal override void FillDocxTableCellInternal(IDocxExportProvider exportProvider);
        protected override void FillHtmlTableCellCore(IHtmlExportProvider exportProvider);
        protected override void FillRtfTableCellCore(IRtfExportProvider exportProvider);
        private void FillTableCellWithImage(ITableExportProvider exportProvider);
        protected internal override void FillXlTableCellInternal(IXlExportProvider exportProvider);
        protected internal override BrickViewData[] GetExportData(ExportContext exportContext, RectangleF rect, RectangleF clipRect);
        protected internal override BrickViewData[] GetHtmlData(ExportContext htmlExportContext, RectangleF bounds, RectangleF clipBoundsF);
        private BrickViewData[] GetHtmlDataCore(ExportContext htmlExportContext, RectangleF bounds, RectangleF clipBoundsF);
        protected override object[] GetSpecificKeyPart();
        internal override void ProcessLayout(PageLayoutBuilder layoutBuilder, PointF pos, RectangleF clipRect);
        private static Pair<Rectangle, Rectangle> SplitHorz(Rectangle rect, int dy);
        private static Pair<Rectangle, Rectangle> SplitVert(Rectangle rect, int dx);

        private DevExpress.XtraPrinting.LineBrick LineBrick { get; }

        private bool ShouldExportAsImage { get; }

        private DevExpress.XtraPrinting.HtmlLineDirection HtmlLineDirection { get; }

        private int PixLineWidth { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LineBrickExporter.<>c <>9;
            public static Action<Graphics> <>9__21_0;

            static <>c();
            internal void <CreateTableCellImage>b__21_0(Graphics gr);
        }
    }
}

