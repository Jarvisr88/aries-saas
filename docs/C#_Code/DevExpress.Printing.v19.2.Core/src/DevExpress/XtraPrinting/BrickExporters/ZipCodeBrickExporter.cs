namespace DevExpress.XtraPrinting.BrickExporters
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class ZipCodeBrickExporter : VisualBrickExporter
    {
        private static readonly Dictionary<char, int[]> digitsTable;

        static ZipCodeBrickExporter();
        private static float CalcDigitWidth(float height);
        private static float CalcSpaceWidth(float height);
        private static void DrawChar(IGraphics gr, Pen pen, RectangleF bounds, char ch);
        protected override void DrawClientContent(IGraphics gr, RectangleF clientRect);
        private static void DrawPlaceHolder(IGraphics gr, Pen pen, RectangleF bounds);
        private static void DrawSegment(IGraphics gr, Pen pen, RectangleF bounds, int index);
        protected internal override void FillDocxTableCellInternal(IDocxExportProvider exportProvider);
        protected override void FillHtmlTableCellCore(IHtmlExportProvider exportProvider);
        protected override void FillRtfTableCellCore(IRtfExportProvider exportProvider);
        private void FillTableCellWithImage(ITableExportProvider exportProvider);
        protected internal override void FillXlTableCellInternal(IXlExportProvider exportProvider);
        protected override object[] GetSpecificKeyPart();

        private DevExpress.XtraPrinting.ZipCodeBrick ZipCodeBrick { get; }
    }
}

