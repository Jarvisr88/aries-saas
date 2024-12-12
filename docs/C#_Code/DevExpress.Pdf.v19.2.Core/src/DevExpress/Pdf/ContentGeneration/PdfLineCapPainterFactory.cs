namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using System;

    public static class PdfLineCapPainterFactory
    {
        public static IPdfLineCapPainter Create(DXLineCap lineCap, DXCustomLineCap customLineCap, double penWidth)
        {
            switch (lineCap)
            {
                case DXLineCap.Square:
                    return new PdfSquareLineCapPainter(penWidth);

                case DXLineCap.Round:
                    return new PdfRoundLineCapPainter(penWidth);

                case DXLineCap.Triangle:
                    return new PdfTriangleLineCapPainter(penWidth);
            }
            switch (lineCap)
            {
                case DXLineCap.SquareAnchor:
                    return new PdfSquareAnchorLineCapPainter(penWidth);

                case DXLineCap.RoundAnchor:
                    return new PdfRoundAnchorLineCapPainter(penWidth);

                case DXLineCap.DiamondAnchor:
                    return new PdfDiamondAnchorLineCapPainter(penWidth);

                case DXLineCap.ArrowAnchor:
                    return new PdfArrowAnchorLineCapPainter(penWidth);
            }
            return ((lineCap == DXLineCap.Custom) ? new PdfCustomLineCapPainter(customLineCap, penWidth) : null);
        }
    }
}

