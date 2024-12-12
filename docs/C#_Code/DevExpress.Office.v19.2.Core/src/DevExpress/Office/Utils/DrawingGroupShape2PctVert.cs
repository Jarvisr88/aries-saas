namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingGroupShape2PctVert : OfficeDrawingIntPropertyBase
    {
        public DrawingGroupShape2PctVert()
        {
        }

        public DrawingGroupShape2PctVert(int val)
        {
            base.Value = val;
        }

        public override void Execute(OfficeArtPropertiesBase owner)
        {
            base.Execute(owner);
            IOfficeArtTertiaryProperties properties = owner as IOfficeArtTertiaryProperties;
            if (properties != null)
            {
                if (!properties.UseRelativeHeight)
                {
                    properties.SizeRelV = DrawingGroupShape2SizeRelV.RelativeFrom.Page;
                }
                properties.UseRelativeHeight = true;
                properties.PctVert = base.Value;
            }
        }
    }
}

