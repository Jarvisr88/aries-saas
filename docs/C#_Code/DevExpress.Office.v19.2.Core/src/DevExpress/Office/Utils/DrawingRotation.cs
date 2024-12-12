namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingRotation : OfficeDrawingFixedPointPropertyBase
    {
        public DrawingRotation()
        {
        }

        public DrawingRotation(int rotation)
        {
            base.Value = rotation;
        }

        public override void Execute(OfficeArtPropertiesBase owner)
        {
            IOfficeArtProperties properties = owner as IOfficeArtProperties;
            if (properties != null)
            {
                properties.Rotation = base.Value;
            }
        }

        public override bool Complex =>
            false;
    }
}

