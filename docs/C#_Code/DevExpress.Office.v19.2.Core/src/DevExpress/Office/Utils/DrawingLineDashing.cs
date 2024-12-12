namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingLineDashing : OfficeDrawingIntPropertyBase
    {
        public DrawingLineDashing()
        {
            base.Value = 0;
        }

        public DrawingLineDashing(OutlineDashing dashing)
        {
            base.Value = (int) dashing;
        }

        public override bool Complex =>
            false;

        public OutlineDashing Dashing
        {
            get => 
                (OutlineDashing) base.Value;
            set => 
                base.Value = (int) value;
        }
    }
}

