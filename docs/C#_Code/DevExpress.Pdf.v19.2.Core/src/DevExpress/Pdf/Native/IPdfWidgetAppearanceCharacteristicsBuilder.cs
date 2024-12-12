namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public interface IPdfWidgetAppearanceCharacteristicsBuilder
    {
        PdfColor BackgroundColor { get; }

        PdfColor BorderColor { get; }

        int RotationAngle { get; }

        string Caption { get; }
    }
}

