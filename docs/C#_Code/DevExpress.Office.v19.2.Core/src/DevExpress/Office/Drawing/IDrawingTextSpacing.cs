namespace DevExpress.Office.Drawing
{
    using System;

    public interface IDrawingTextSpacing
    {
        DrawingTextSpacingValueType Type { get; set; }

        int Value { get; set; }
    }
}

