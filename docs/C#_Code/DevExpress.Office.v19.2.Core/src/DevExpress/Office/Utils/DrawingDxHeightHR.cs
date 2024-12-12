namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingDxHeightHR : DrawingHRPropertyBase
    {
        public DrawingDxHeightHR()
        {
        }

        public DrawingDxHeightHR(int value) : base(value)
        {
        }

        protected override void SetValue(IOfficeArtTertiaryProperties artProperties, int value)
        {
            artProperties.DxHeightHR = value;
        }
    }
}

