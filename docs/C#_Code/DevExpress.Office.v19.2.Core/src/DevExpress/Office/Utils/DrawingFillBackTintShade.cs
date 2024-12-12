namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingFillBackTintShade : OfficeDrawingIntPropertyBase
    {
        public DrawingFillBackTintShade()
        {
            base.Value = 2;
        }

        public override bool Complex =>
            false;

        public OfficeTintShade TineShade
        {
            get => 
                (OfficeTintShade) base.Value;
            set => 
                base.Value = (int) value;
        }
    }
}

