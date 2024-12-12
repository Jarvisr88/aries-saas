namespace DevExpress.Office.Utils
{
    using System;

    internal static class PackedValues
    {
        public static bool GetBoolBitValue(uint packedValues, uint mask) => 
            (packedValues & mask) != 0;

        public static int GetIntBitValue(uint packedValues, uint mask) => 
            (int) (packedValues & mask);

        public static int GetIntBitValue(uint packedValues, uint mask, int offset) => 
            (int) ((packedValues & mask) >> (offset & 0x1f));

        public static void SetBoolBitValue(ref byte packedValues, byte mask, bool value)
        {
            if (value)
            {
                packedValues = (byte) (packedValues | mask);
            }
            else
            {
                packedValues = (byte) (packedValues & ~mask);
            }
        }

        public static void SetBoolBitValue(ref uint packedValues, uint mask, bool value)
        {
            if (value)
            {
                packedValues |= mask;
            }
            else
            {
                packedValues &= ~mask;
            }
        }

        public static void SetIntBitValue(ref uint packedValues, uint mask, int value)
        {
            packedValues &= ~mask;
            packedValues = (uint) (packedValues | (value & mask));
        }

        public static void SetIntBitValue(ref byte packedValues, byte mask, byte offset, byte value)
        {
            packedValues = (byte) (packedValues & ~mask);
            packedValues = (byte) (packedValues | ((byte) ((value << (offset & 0x1f)) & mask)));
        }

        public static void SetIntBitValue(ref uint packedValues, uint mask, int offset, int value)
        {
            packedValues &= ~mask;
            packedValues = (uint) (packedValues | ((value << (offset & 0x1f)) & mask));
        }
    }
}

