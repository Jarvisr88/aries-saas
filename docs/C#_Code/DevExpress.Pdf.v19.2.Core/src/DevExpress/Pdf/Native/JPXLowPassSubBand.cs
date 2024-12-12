namespace DevExpress.Pdf.Native
{
    using System;

    public abstract class JPXLowPassSubBand : JPXSubBand
    {
        private static byte[] table = BuldLookupTable(new Func<byte, byte, byte, int>(JPXLowPassSubBand.CalculateContextLabel));

        protected JPXLowPassSubBand(JPXTileComponent component, int resolutionLevelNumber, int codeBlockWidth, int codeBlockHeight) : base(component, resolutionLevelNumber, codeBlockWidth, codeBlockHeight)
        {
        }

        private static int CalculateContextLabel(byte v, byte h, byte d) => 
            (h == 0) ? ((v != 0) ? (2 + v) : Math.Min(2, d)) : ((h == 1) ? ((v <= 0) ? ((d <= 0) ? 5 : 6) : 7) : 8);

        public override byte[] LookupTable =>
            table;
    }
}

