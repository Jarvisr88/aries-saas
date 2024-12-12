namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingFillFocus : OfficeDrawingIntPropertyBase
    {
        public const int DefaultValue = 0;

        public DrawingFillFocus()
        {
            base.Value = 0;
        }

        public DrawingFillFocus(int focus)
        {
            if ((focus < -100) || (focus > 100))
            {
                throw new ArgumentOutOfRangeException("Focus out of range -100..100!");
            }
            base.Value = focus;
        }

        public override bool Complex =>
            false;
    }
}

