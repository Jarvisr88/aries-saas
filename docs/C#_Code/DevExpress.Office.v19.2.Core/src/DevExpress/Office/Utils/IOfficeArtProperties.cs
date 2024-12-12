namespace DevExpress.Office.Utils
{
    using System;
    using System.Drawing;

    public interface IOfficeArtProperties : IOfficeArtPropertiesBase
    {
        int BlipIndex { get; set; }

        int TextIndex { get; set; }

        bool UseTextTop { get; set; }

        bool UseTextBottom { get; set; }

        bool UseTextRight { get; set; }

        bool UseTextLeft { get; set; }

        bool UseFitShapeToText { get; set; }

        bool FitShapeToText { get; set; }

        int TextTop { get; set; }

        int TextBottom { get; set; }

        int TextRight { get; set; }

        int TextLeft { get; set; }

        int WrapLeftDistance { get; set; }

        bool UseWrapLeftDistance { get; set; }

        int WrapRightDistance { get; set; }

        bool UseWrapRightDistance { get; set; }

        int WrapTopDistance { get; set; }

        bool UseWrapTopDistance { get; set; }

        int WrapBottomDistance { get; set; }

        bool UseWrapBottomDistance { get; set; }

        double CropFromTop { get; set; }

        double CropFromBottom { get; set; }

        double CropFromLeft { get; set; }

        double CropFromRight { get; set; }

        double Rotation { get; set; }

        bool Line { get; set; }

        bool UseLine { get; set; }

        double LineWidth { get; set; }

        Color LineColor { get; set; }

        Color FillColor { get; set; }

        int PictureContrast { get; set; }

        int PictureBrightness { get; set; }
    }
}

