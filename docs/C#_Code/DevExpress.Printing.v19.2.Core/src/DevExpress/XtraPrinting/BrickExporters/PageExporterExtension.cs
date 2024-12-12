namespace DevExpress.XtraPrinting.BrickExporters
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public static class PageExporterExtension
    {
        public static void DrawPage(this PageExporter exporter, IGraphics gr, PointF location);
    }
}

