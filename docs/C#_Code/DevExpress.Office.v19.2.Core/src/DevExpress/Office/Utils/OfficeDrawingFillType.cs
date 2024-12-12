namespace DevExpress.Office.Utils
{
    using System;

    public class OfficeDrawingFillType : OfficeDrawingIntPropertyBase
    {
        public OfficeDrawingFillType()
        {
            base.Value = 0;
        }

        public override bool Complex =>
            false;

        public OfficeFillType FillType
        {
            get => 
                (OfficeFillType) base.Value;
            set => 
                base.Value = (int) value;
        }
    }
}

