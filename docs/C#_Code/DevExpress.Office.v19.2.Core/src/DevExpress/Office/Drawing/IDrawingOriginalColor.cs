namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.Model;
    using System;
    using System.Drawing;

    public interface IDrawingOriginalColor
    {
        void SetColorFromRGB(Color rgb);

        Color Rgb { get; set; }

        SystemColorValues System { get; set; }

        SchemeColorValues Scheme { get; set; }

        string Preset { get; set; }

        ColorHSL Hsl { get; set; }

        ScRGBColor ScRgb { get; set; }

        ColorTransformCollection Transforms { get; }
    }
}

