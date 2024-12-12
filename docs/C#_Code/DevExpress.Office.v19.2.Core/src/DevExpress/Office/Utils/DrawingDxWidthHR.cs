namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingDxWidthHR : DrawingHRPropertyBase
    {
        public DrawingDxWidthHR()
        {
        }

        public DrawingDxWidthHR(int value) : base(value)
        {
        }

        protected override void SetValue(IOfficeArtTertiaryProperties artProperties, int value)
        {
            artProperties.DxWidthHR = value;
        }
    }
}

