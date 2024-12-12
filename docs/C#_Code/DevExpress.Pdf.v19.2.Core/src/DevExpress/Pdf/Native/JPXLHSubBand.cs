namespace DevExpress.Pdf.Native
{
    using System;

    public class JPXLHSubBand : JPXLowPassSubBand
    {
        public JPXLHSubBand(JPXTileComponent component, int resolutionLevelNumber, int codeBlockWidth, int codeBlockHeight) : base(component, resolutionLevelNumber, codeBlockWidth, codeBlockHeight)
        {
        }

        protected override int HorizontalQuantity =>
            0;

        protected override int VerticalQuantity =>
            1;

        protected override int GainLog =>
            1;
    }
}

