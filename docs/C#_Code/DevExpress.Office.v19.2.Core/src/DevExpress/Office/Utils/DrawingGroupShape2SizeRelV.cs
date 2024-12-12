namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingGroupShape2SizeRelV : OfficeDrawingIntPropertyBase
    {
        public DrawingGroupShape2SizeRelV()
        {
        }

        public DrawingGroupShape2SizeRelV(RelativeFrom from)
        {
            this.From = from;
        }

        public override void Execute(OfficeArtPropertiesBase owner)
        {
            base.Execute(owner);
            IOfficeArtTertiaryProperties properties = owner as IOfficeArtTertiaryProperties;
            if (properties != null)
            {
                properties.UseRelativeHeight = true;
                properties.SizeRelV = this.From;
            }
        }

        public RelativeFrom From
        {
            get => 
                (RelativeFrom) base.Value;
            set => 
                base.Value = (int) value;
        }

        public enum RelativeFrom
        {
            Margin,
            Page,
            TopMargin,
            BottomMargin,
            InsideMargin,
            OutsideMargin
        }
    }
}

