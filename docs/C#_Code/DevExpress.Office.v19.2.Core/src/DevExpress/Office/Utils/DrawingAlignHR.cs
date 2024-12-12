namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingAlignHR : DrawingHRPropertyBase
    {
        public DrawingAlignHR()
        {
        }

        public DrawingAlignHR(int value) : base(value)
        {
        }

        protected override void SetValue(IOfficeArtTertiaryProperties artProperties, int value)
        {
            artProperties.AlignHR = value;
        }
    }
}

