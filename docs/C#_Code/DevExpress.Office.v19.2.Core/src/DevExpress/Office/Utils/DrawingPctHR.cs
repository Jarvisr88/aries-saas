namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingPctHR : DrawingHRPropertyBase
    {
        public DrawingPctHR()
        {
        }

        public DrawingPctHR(int value) : base(value)
        {
        }

        protected override void SetValue(IOfficeArtTertiaryProperties artProperties, int value)
        {
            artProperties.PctHR = value;
        }
    }
}

