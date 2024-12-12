namespace DevExpress.Office.Drawing
{
    using System;

    public interface IDrawingTextSpacingsChanger
    {
        DrawingTextSpacingValueType GetType(int index);
        int GetValue(int index);
        void SetType(int index, DrawingTextSpacingValueType value);
        void SetValue(int index, int value);
    }
}

