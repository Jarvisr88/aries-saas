namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingFillShadePreset : OfficeDrawingIntPropertyBase
    {
        public const int DefaultValue = 0;

        public DrawingFillShadePreset()
        {
            base.Value = 0;
        }

        public DrawingFillShadePreset(int value)
        {
            base.Value = value;
        }

        public override bool Complex =>
            false;
    }
}

