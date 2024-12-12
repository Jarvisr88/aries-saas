namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingGroupShapePosRelV : OfficeDrawingIntPropertyBase
    {
        public DrawingGroupShapePosRelV() : this(Msoprv.msoprvText)
        {
        }

        public DrawingGroupShapePosRelV(Msoprv msoPosRelV)
        {
            this.MsoPosRelV = msoPosRelV;
        }

        public override void Execute(OfficeArtPropertiesBase owner)
        {
            base.Execute(owner);
            IOfficeArtTertiaryProperties properties = owner as IOfficeArtTertiaryProperties;
            if (properties != null)
            {
                properties.PosRelV = this.MsoPosRelV;
                properties.UsePosV = true;
            }
        }

        public Msoprv MsoPosRelV
        {
            get => 
                (Msoprv) base.Value;
            set => 
                base.Value = (int) value;
        }

        public enum Msoprv
        {
            msoprvMargin,
            msoprvPage,
            msoprvText,
            msoprvLine
        }
    }
}

