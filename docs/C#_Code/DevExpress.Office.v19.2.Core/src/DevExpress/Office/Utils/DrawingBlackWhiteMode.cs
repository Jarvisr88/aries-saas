namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingBlackWhiteMode : OfficeDrawingIntPropertyBase
    {
        public DrawingBlackWhiteMode()
        {
            base.Value = 1;
        }

        public DrawingBlackWhiteMode(BlackWhiteMode mode)
        {
            base.Value = (int) mode;
        }

        public override bool Complex =>
            false;

        public BlackWhiteMode Mode
        {
            get => 
                (BlackWhiteMode) base.Value;
            set => 
                base.Value = (int) value;
        }
    }
}

