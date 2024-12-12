namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingGroupShape2SizeRelH : OfficeDrawingIntPropertyBase
    {
        public DrawingGroupShape2SizeRelH()
        {
            this.From = RelativeFrom.Page;
        }

        public DrawingGroupShape2SizeRelH(RelativeFrom from)
        {
            this.From = from;
        }

        public override void Execute(OfficeArtPropertiesBase owner)
        {
            base.Execute(owner);
            IOfficeArtTertiaryProperties properties = owner as IOfficeArtTertiaryProperties;
            if (properties != null)
            {
                properties.UseRelativeWidth = true;
                properties.SizeRelH = this.From;
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
            LeftMargin,
            RightMargin,
            InsideMargin,
            OutsideMargin
        }
    }
}

