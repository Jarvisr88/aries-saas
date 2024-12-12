namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingGroupShape2PctHorizPos : OfficeDrawingIntPropertyBase
    {
        public DrawingGroupShape2PctHorizPos()
        {
            base.Value = -10001;
        }

        public DrawingGroupShape2PctHorizPos(int val)
        {
            base.Value = val;
        }

        public override void Execute(OfficeArtPropertiesBase owner)
        {
            base.Execute(owner);
            IOfficeArtTertiaryProperties properties = owner as IOfficeArtTertiaryProperties;
            if (properties != null)
            {
                properties.PctHorizPos = base.Value;
            }
        }
    }
}

