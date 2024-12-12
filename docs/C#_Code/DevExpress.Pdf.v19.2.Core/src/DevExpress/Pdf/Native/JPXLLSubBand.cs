namespace DevExpress.Pdf.Native
{
    using System;

    public class JPXLLSubBand : JPXLowPassSubBand
    {
        public JPXLLSubBand(JPXTileComponent component, int resolutionLevelNumber, int codeBlockWidth, int codeBlockHeight) : base(component, resolutionLevelNumber, codeBlockWidth, codeBlockHeight)
        {
        }

        protected override int HorizontalQuantity =>
            0;

        protected override int VerticalQuantity =>
            0;

        protected override int GainLog =>
            0;
    }
}

