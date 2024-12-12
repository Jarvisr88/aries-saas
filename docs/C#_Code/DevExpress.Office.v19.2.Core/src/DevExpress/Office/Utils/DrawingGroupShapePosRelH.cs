namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingGroupShapePosRelH : OfficeDrawingIntPropertyBase
    {
        public DrawingGroupShapePosRelH() : this(Msoprh.msoprhText)
        {
        }

        public DrawingGroupShapePosRelH(Msoprh msoPosRelH)
        {
            this.MsoPosRelH = msoPosRelH;
        }

        public override void Execute(OfficeArtPropertiesBase owner)
        {
            base.Execute(owner);
            IOfficeArtTertiaryProperties properties = owner as IOfficeArtTertiaryProperties;
            if (properties != null)
            {
                properties.PosRelH = this.MsoPosRelH;
                properties.UsePosH = true;
            }
        }

        public Msoprh MsoPosRelH
        {
            get => 
                (Msoprh) base.Value;
            set => 
                base.Value = (int) value;
        }

        public enum Msoprh
        {
            msoprhMargin,
            msoprhPage,
            msoprhText,
            msoprhChar
        }
    }
}

