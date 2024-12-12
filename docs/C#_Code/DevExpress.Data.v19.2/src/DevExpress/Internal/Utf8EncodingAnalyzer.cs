namespace DevExpress.Internal
{
    using System;

    public class Utf8EncodingAnalyzer : EncodingAnalyzer
    {
        private static readonly int[] classTable;
        private static readonly int[] stateTable;
        private static readonly int[] charLenTable;

        static Utf8EncodingAnalyzer()
        {
            int[] numArray1 = new int[0x20];
            numArray1[0] = Pack4Bits(1, 1, 1, 1, 1, 1, 1, 1);
            numArray1[1] = Pack4Bits(1, 1, 1, 1, 1, 1, 0, 0);
            numArray1[2] = Pack4Bits(1, 1, 1, 1, 1, 1, 1, 1);
            numArray1[3] = Pack4Bits(1, 1, 1, 0, 1, 1, 1, 1);
            numArray1[4] = Pack4Bits(1, 1, 1, 1, 1, 1, 1, 1);
            numArray1[5] = Pack4Bits(1, 1, 1, 1, 1, 1, 1, 1);
            numArray1[6] = Pack4Bits(1, 1, 1, 1, 1, 1, 1, 1);
            numArray1[7] = Pack4Bits(1, 1, 1, 1, 1, 1, 1, 1);
            numArray1[8] = Pack4Bits(1, 1, 1, 1, 1, 1, 1, 1);
            numArray1[9] = Pack4Bits(1, 1, 1, 1, 1, 1, 1, 1);
            numArray1[10] = Pack4Bits(1, 1, 1, 1, 1, 1, 1, 1);
            numArray1[11] = Pack4Bits(1, 1, 1, 1, 1, 1, 1, 1);
            numArray1[12] = Pack4Bits(1, 1, 1, 1, 1, 1, 1, 1);
            numArray1[13] = Pack4Bits(1, 1, 1, 1, 1, 1, 1, 1);
            numArray1[14] = Pack4Bits(1, 1, 1, 1, 1, 1, 1, 1);
            numArray1[15] = Pack4Bits(1, 1, 1, 1, 1, 1, 1, 1);
            numArray1[0x10] = Pack4Bits(2, 2, 2, 2, 3, 3, 3, 3);
            numArray1[0x11] = Pack4Bits(4, 4, 4, 4, 4, 4, 4, 4);
            numArray1[0x12] = Pack4Bits(4, 4, 4, 4, 4, 4, 4, 4);
            numArray1[0x13] = Pack4Bits(4, 4, 4, 4, 4, 4, 4, 4);
            numArray1[20] = Pack4Bits(5, 5, 5, 5, 5, 5, 5, 5);
            numArray1[0x15] = Pack4Bits(5, 5, 5, 5, 5, 5, 5, 5);
            numArray1[0x16] = Pack4Bits(5, 5, 5, 5, 5, 5, 5, 5);
            numArray1[0x17] = Pack4Bits(5, 5, 5, 5, 5, 5, 5, 5);
            numArray1[0x18] = Pack4Bits(0, 0, 6, 6, 6, 6, 6, 6);
            numArray1[0x19] = Pack4Bits(6, 6, 6, 6, 6, 6, 6, 6);
            numArray1[0x1a] = Pack4Bits(6, 6, 6, 6, 6, 6, 6, 6);
            numArray1[0x1b] = Pack4Bits(6, 6, 6, 6, 6, 6, 6, 6);
            numArray1[0x1c] = Pack4Bits(7, 8, 8, 8, 8, 8, 8, 8);
            numArray1[0x1d] = Pack4Bits(8, 8, 8, 8, 8, 9, 8, 8);
            numArray1[30] = Pack4Bits(10, 11, 11, 11, 11, 11, 11, 11);
            numArray1[0x1f] = Pack4Bits(12, 13, 13, 13, 14, 15, 0, 0);
            classTable = numArray1;
            int[] numArray2 = new int[0x1a];
            numArray2[0] = Pack4Bits((int) EAState.Error, (int) EAState.Start, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, 12, 10);
            numArray2[1] = Pack4Bits(9, 11, 8, 7, 6, 5, 4, 3);
            numArray2[2] = Pack4Bits((int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error);
            numArray2[3] = Pack4Bits((int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error);
            numArray2[4] = Pack4Bits((int) EAState.ItsMe, (int) EAState.ItsMe, (int) EAState.ItsMe, (int) EAState.ItsMe, (int) EAState.ItsMe, (int) EAState.ItsMe, (int) EAState.ItsMe, (int) EAState.ItsMe);
            numArray2[5] = Pack4Bits((int) EAState.ItsMe, (int) EAState.ItsMe, (int) EAState.ItsMe, (int) EAState.ItsMe, (int) EAState.ItsMe, (int) EAState.ItsMe, (int) EAState.ItsMe, (int) EAState.ItsMe);
            numArray2[6] = Pack4Bits((int) EAState.Error, (int) EAState.Error, 5, 5, 5, 5, (int) EAState.Error, (int) EAState.Error);
            numArray2[7] = Pack4Bits((int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error);
            numArray2[8] = Pack4Bits((int) EAState.Error, (int) EAState.Error, (int) EAState.Error, 5, 5, 5, (int) EAState.Error, (int) EAState.Error);
            numArray2[9] = Pack4Bits((int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error);
            numArray2[10] = Pack4Bits((int) EAState.Error, (int) EAState.Error, 7, 7, 7, 7, (int) EAState.Error, (int) EAState.Error);
            numArray2[11] = Pack4Bits((int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error);
            numArray2[12] = Pack4Bits((int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, 7, 7, (int) EAState.Error, (int) EAState.Error);
            numArray2[13] = Pack4Bits((int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error);
            numArray2[14] = Pack4Bits((int) EAState.Error, (int) EAState.Error, 9, 9, 9, 9, (int) EAState.Error, (int) EAState.Error);
            numArray2[15] = Pack4Bits((int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error);
            numArray2[0x10] = Pack4Bits((int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, 9, (int) EAState.Error, (int) EAState.Error);
            numArray2[0x11] = Pack4Bits((int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error);
            numArray2[0x12] = Pack4Bits((int) EAState.Error, (int) EAState.Error, 12, 12, 12, 12, (int) EAState.Error, (int) EAState.Error);
            numArray2[0x13] = Pack4Bits((int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error);
            numArray2[20] = Pack4Bits((int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, 12, (int) EAState.Error, (int) EAState.Error);
            numArray2[0x15] = Pack4Bits((int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error);
            numArray2[0x16] = Pack4Bits((int) EAState.Error, (int) EAState.Error, 12, 12, 12, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error);
            numArray2[0x17] = Pack4Bits((int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error);
            numArray2[0x18] = Pack4Bits((int) EAState.Error, (int) EAState.Error, (int) EAState.Start, (int) EAState.Start, (int) EAState.Start, (int) EAState.Start, (int) EAState.Error, (int) EAState.Error);
            numArray2[0x19] = Pack4Bits((int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error);
            stateTable = numArray2;
            charLenTable = new int[] { 0, 1, 0, 0, 0, 0, 2, 3, 3, 3, 4, 4, 5, 5, 6, 6 };
        }

        protected internal override int[] ClassTable =>
            classTable;

        protected internal override int[] StateTable =>
            stateTable;

        protected internal override int[] CharLenTable =>
            charLenTable;
    }
}

