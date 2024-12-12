namespace DevExpress.XtraPrinting.XamlExport
{
    using DevExpress.XtraPrinting;
    using System;

    internal class PageInfoTextBrickXamlExporter : TextBrickXamlExporter
    {
        protected override string GetText(TextBrick brick, PrintingSystemBase ps, Page drawingPage, TextMeasurementSystem textMeasurementSystem)
        {
            PageInfoTextBrick brick2 = brick as PageInfoTextBrick;
            if (brick2 == null)
            {
                throw new ArgumentException("brick");
            }
            return brick2.GetTextInfo(ps, drawingPage);
        }
    }
}

