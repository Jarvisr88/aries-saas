namespace DevExpress.Internal
{
    using System;

    public class Big5EncodingAnalyzer : EncodingAnalyzer
    {
        private static readonly int[] classTable;
        private static readonly int[] stateTable;
        private static readonly int[] charLenTable;

        static Big5EncodingAnalyzer()
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
            numArray1[8] = Pack4Bits(2, 2, 2, 2, 2, 2, 2, 2);
            numArray1[9] = Pack4Bits(2, 2, 2, 2, 2, 2, 2, 2);
            numArray1[10] = Pack4Bits(2, 2, 2, 2, 2, 2, 2, 2);
            numArray1[11] = Pack4Bits(2, 2, 2, 2, 2, 2, 2, 2);
            numArray1[12] = Pack4Bits(2, 2, 2, 2, 2, 2, 2, 2);
            numArray1[13] = Pack4Bits(2, 2, 2, 2, 2, 2, 2, 2);
            numArray1[14] = Pack4Bits(2, 2, 2, 2, 2, 2, 2, 2);
            numArray1[15] = Pack4Bits(2, 2, 2, 2, 2, 2, 2, 1);
            numArray1[0x10] = Pack4Bits(4, 4, 4, 4, 4, 4, 4, 4);
            numArray1[0x11] = Pack4Bits(4, 4, 4, 4, 4, 4, 4, 4);
            numArray1[0x12] = Pack4Bits(4, 4, 4, 4, 4, 4, 4, 4);
            numArray1[0x13] = Pack4Bits(4, 4, 4, 4, 4, 4, 4, 4);
            numArray1[20] = Pack4Bits(4, 3, 3, 3, 3, 3, 3, 3);
            numArray1[0x15] = Pack4Bits(3, 3, 3, 3, 3, 3, 3, 3);
            numArray1[0x16] = Pack4Bits(3, 3, 3, 3, 3, 3, 3, 3);
            numArray1[0x17] = Pack4Bits(3, 3, 3, 3, 3, 3, 3, 3);
            numArray1[0x18] = Pack4Bits(3, 3, 3, 3, 3, 3, 3, 3);
            numArray1[0x19] = Pack4Bits(3, 3, 3, 3, 3, 3, 3, 3);
            numArray1[0x1a] = Pack4Bits(3, 3, 3, 3, 3, 3, 3, 3);
            numArray1[0x1b] = Pack4Bits(3, 3, 3, 3, 3, 3, 3, 3);
            numArray1[0x1c] = Pack4Bits(3, 3, 3, 3, 3, 3, 3, 3);
            numArray1[0x1d] = Pack4Bits(3, 3, 3, 3, 3, 3, 3, 3);
            numArray1[30] = Pack4Bits(3, 3, 3, 3, 3, 3, 3, 3);
            numArray1[0x1f] = Pack4Bits(3, 3, 3, 3, 3, 3, 3, 0);
            classTable = numArray1;
            stateTable = new int[] { Pack4Bits((int) EAState.Error, (int) EAState.Start, (int) EAState.Start, 3, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error, (int) EAState.Error), Pack4Bits((int) EAState.Error, (int) EAState.Error, (int) EAState.ItsMe, (int) EAState.ItsMe, (int) EAState.ItsMe, (int) EAState.ItsMe, (int) EAState.ItsMe, (int) EAState.Error), Pack4Bits((int) EAState.Error, (int) EAState.Start, (int) EAState.Start, (int) EAState.Start, (int) EAState.Start, (int) EAState.Start, (int) EAState.Start, (int) EAState.Start) };
            charLenTable = new int[] { 0, 1, 1, 2, 0 };
        }

        protected internal override int[] ClassTable =>
            classTable;

        protected internal override int[] StateTable =>
            stateTable;

        protected internal override int[] CharLenTable =>
            charLenTable;
    }
}

