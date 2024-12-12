namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingGroupShape2PctHoriz : OfficeDrawingIntPropertyBase
    {
        public DrawingGroupShape2PctHoriz()
        {
        }

        public DrawingGroupShape2PctHoriz(int val)
        {
            base.Value = val;
        }

        public override void Execute(OfficeArtPropertiesBase owner)
        {
            base.Execute(owner);
            IOfficeArtTertiaryProperties properties = owner as IOfficeArtTertiaryProperties;
            if (properties != null)
            {
                if (!properties.UseRelativeWidth)
                {
                    properties.SizeRelH = DrawingGroupShape2SizeRelH.RelativeFrom.Page;
                }
                properties.UseRelativeWidth = true;
                properties.PctHoriz = base.Value;
            }
        }
    }
}

