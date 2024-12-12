namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingGroupShapePosV : OfficeDrawingIntPropertyBase
    {
        public DrawingGroupShapePosV()
        {
        }

        public DrawingGroupShapePosV(Msopv msoPosV)
        {
            this.MsoPosV = msoPosV;
        }

        public override void Execute(OfficeArtPropertiesBase owner)
        {
            base.Execute(owner);
            IOfficeArtTertiaryProperties properties = owner as IOfficeArtTertiaryProperties;
            if (properties != null)
            {
                properties.PosV = this.MsoPosV;
                properties.UsePosV = true;
            }
        }

        public Msopv MsoPosV
        {
            get => 
                (Msopv) base.Value;
            set => 
                base.Value = (int) value;
        }

        public enum Msopv
        {
            msopvAbs,
            msopvTop,
            msopvCenter,
            msopvBottom,
            msopvInside,
            msopvOutside
        }
    }
}

