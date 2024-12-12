namespace DevExpress.Pdf.Native
{
    using System;

    public class JPXHHSubBand : JPXSubBand
    {
        private static byte[] table = BuldLookupTable(new Func<byte, byte, byte, int>(JPXHHSubBand.CalculateContextLabel));

        public JPXHHSubBand(JPXTileComponent component, int resolutionLevelNumber, int codeBlockWidth, int codeBlockHeight) : base(component, resolutionLevelNumber, codeBlockWidth, codeBlockHeight)
        {
        }

        private static int CalculateContextLabel(byte v, byte h, byte d)
        {
            int num = Math.Min(2, v + h);
            switch (d)
            {
                case 0:
                    return num;

                case 1:
                    return (num + 3);

                case 2:
                    return ((num > 0) ? 7 : 6);
            }
            return 8;
        }

        public override byte[] LookupTable =>
            table;

        protected override int HorizontalQuantity =>
            1;

        protected override int VerticalQuantity =>
            1;

        protected override int GainLog =>
            2;
    }
}

