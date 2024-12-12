namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingTextDirection : OfficeDrawingIntPropertyBase
    {
        public DrawingTextDirection()
        {
            base.Value = 0;
        }

        public DrawingTextDirection(OfficeTextReadingOrder direction)
        {
            base.Value = (int) direction;
        }

        public override bool Complex =>
            false;

        public OfficeTextReadingOrder Direction
        {
            get => 
                (OfficeTextReadingOrder) base.Value;
            set => 
                base.Value = (int) value;
        }
    }
}

