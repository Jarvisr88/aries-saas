namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingGroupShapePosH : OfficeDrawingIntPropertyBase
    {
        public DrawingGroupShapePosH()
        {
        }

        public DrawingGroupShapePosH(Msoph msoPosH)
        {
            this.MsoPosH = msoPosH;
        }

        public override void Execute(OfficeArtPropertiesBase owner)
        {
            base.Execute(owner);
            IOfficeArtTertiaryProperties properties = owner as IOfficeArtTertiaryProperties;
            if (properties != null)
            {
                properties.PosH = this.MsoPosH;
                properties.UsePosH = true;
            }
        }

        public Msoph MsoPosH
        {
            get => 
                (Msoph) base.Value;
            set => 
                base.Value = (int) value;
        }

        public enum Msoph
        {
            msophAbs,
            msophLeft,
            msophCenter,
            msophRight,
            msophInside,
            msophOutside
        }
    }
}

