namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Runtime.CompilerServices;

    public class PdfWidgetAppearanceCharacteristicsBuilder : IPdfWidgetAppearanceCharacteristicsBuilder
    {
        public PdfWidgetAppearanceCharacteristics CreateAppearanceCharacteristics() => 
            new PdfWidgetAppearanceCharacteristics(this);

        public PdfWidgetAppearanceCharacteristicsBuilder SetAppearance(PdfAcroFormFieldAppearance appearance)
        {
            if (appearance == null)
            {
                this.BackgroundColor = null;
                this.BorderColor = null;
            }
            else
            {
                if (appearance.BackgroundColor != null)
                {
                    this.BackgroundColor = new PdfColor(appearance.BackgroundColor);
                }
                if ((appearance.BorderAppearance != null) && appearance.BorderAppearance.IsVisible)
                {
                    this.BorderColor = new PdfColor(appearance.BorderAppearance.Color);
                }
            }
            return this;
        }

        public PdfWidgetAppearanceCharacteristicsBuilder SetButtonStyle(PdfAcroFormButtonStyle buttonStyle)
        {
            switch (buttonStyle)
            {
                case PdfAcroFormButtonStyle.Circle:
                    this.Caption = "l";
                    break;

                case PdfAcroFormButtonStyle.Check:
                    this.Caption = "4";
                    break;

                case PdfAcroFormButtonStyle.Star:
                    this.Caption = "H";
                    break;

                case PdfAcroFormButtonStyle.Cross:
                    this.Caption = "8";
                    break;

                case PdfAcroFormButtonStyle.Diamond:
                    this.Caption = "u";
                    break;

                case PdfAcroFormButtonStyle.Square:
                    this.Caption = "n";
                    break;

                default:
                    break;
            }
            return this;
        }

        public PdfWidgetAppearanceCharacteristicsBuilder SetRotation(PdfAcroFormFieldRotation rotation)
        {
            switch (rotation)
            {
                case PdfAcroFormFieldRotation.Rotate90:
                    this.RotationAngle = 90;
                    break;

                case PdfAcroFormFieldRotation.Rotate180:
                    this.RotationAngle = 180;
                    break;

                case PdfAcroFormFieldRotation.Rotate270:
                    this.RotationAngle = 270;
                    break;

                default:
                    break;
            }
            return this;
        }

        public PdfColor BackgroundColor { get; private set; }

        public PdfColor BorderColor { get; private set; }

        public int RotationAngle { get; private set; }

        public string Caption { get; private set; }
    }
}

