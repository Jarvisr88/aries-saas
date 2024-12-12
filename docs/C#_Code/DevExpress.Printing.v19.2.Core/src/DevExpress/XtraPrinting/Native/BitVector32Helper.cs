namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Collections.Specialized;

    public static class BitVector32Helper
    {
        public static int CreateMask(BitVector32.Section prevSection)
        {
            BitVector32.Section section = BitVector32.CreateSection(1, prevSection);
            return (section.Mask << (section.Offset & 0x1f));
        }

        public static BitVector32.Section CreateSection(short maxValue, int previousMask) => 
            (previousMask != 0) ? BitVector32.CreateSection(maxValue, CreateTempSection(previousMask)) : BitVector32.CreateSection(maxValue);

        private static BitVector32.Section CreateTempSection(int previousMask)
        {
            if ((previousMask & 0x7fff) != 0)
            {
                return BitVector32.CreateSection((short) (previousMask & 0x7fff));
            }
            BitVector32.Section previous = BitVector32.CreateSection(0x7fff);
            previousMask = previousMask >> 15;
            if ((previousMask & 0x7fff) != 0)
            {
                return BitVector32.CreateSection((short) (previousMask & 0x7fff), previous);
            }
            previous = BitVector32.CreateSection(0x7fff, previous);
            previousMask = previousMask >> 15;
            return BitVector32.CreateSection((short) previousMask, previous);
        }
    }
}

