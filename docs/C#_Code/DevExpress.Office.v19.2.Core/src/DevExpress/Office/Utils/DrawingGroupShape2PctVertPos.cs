namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingGroupShape2PctVertPos : OfficeDrawingIntPropertyBase
    {
        public DrawingGroupShape2PctVertPos()
        {
            base.Value = -10001;
        }

        public DrawingGroupShape2PctVertPos(int val)
        {
            base.Value = val;
        }

        public override void Execute(OfficeArtPropertiesBase owner)
        {
            base.Execute(owner);
            IOfficeArtTertiaryProperties properties = owner as IOfficeArtTertiaryProperties;
            if (properties != null)
            {
                properties.PctVertPos = base.Value;
            }
        }
    }
}

