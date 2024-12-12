namespace DevExpress.Pdf.Native
{
    using System;

    public class JPXHLSubBand : JPXLowPassSubBand
    {
        private static byte[] table = BuldLookupTable(new Func<byte, byte, byte, int>(JPXHLSubBand.CalculateContextLabel));

        public JPXHLSubBand(JPXTileComponent component, int resolutionLevelNumber, int codeBlockWidth, int codeBlockHeight) : base(component, resolutionLevelNumber, codeBlockWidth, codeBlockHeight)
        {
        }

        private static int CalculateContextLabel(byte h, byte v, byte d) => 
            (h == 0) ? ((v != 0) ? (2 + v) : Math.Min(2, d)) : ((h == 1) ? ((v <= 0) ? ((d <= 0) ? 5 : 6) : 7) : 8);

        public override byte[] LookupTable =>
            table;

        protected override int HorizontalQuantity =>
            1;

        protected override int VerticalQuantity =>
            0;

        protected override int GainLog =>
            1;
    }
}

