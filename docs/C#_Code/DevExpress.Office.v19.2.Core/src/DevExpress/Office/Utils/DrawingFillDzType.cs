namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingFillDzType : OfficeDrawingIntPropertyBase
    {
        public DrawingFillDzType()
        {
            base.Value = 0;
        }

        public DrawingFillDzType(OfficeDzType dzType)
        {
            base.Value = (int) dzType;
        }

        public override bool Complex =>
            false;

        public OfficeDzType DzType
        {
            get => 
                (OfficeDzType) base.Value;
            set => 
                base.Value = (int) value;
        }
    }
}

