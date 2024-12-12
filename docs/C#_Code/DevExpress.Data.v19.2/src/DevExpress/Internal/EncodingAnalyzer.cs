namespace DevExpress.Internal
{
    using System;

    public abstract class EncodingAnalyzer
    {
        private EAState currentState;
        private int currentCharLength;

        protected EncodingAnalyzer()
        {
        }

        public EAState NextState(byte c)
        {
            int index = Unpack4Bits(c, this.ClassTable);
            if (this.currentState == EAState.Start)
            {
                this.currentCharLength = this.CharLenTable[index];
            }
            this.currentState = (EAState) Unpack4Bits(((int) (this.currentState * this.CharLenTable.Length)) + index, this.StateTable);
            return this.currentState;
        }

        private static int Pack16Bits(int a, int b) => 
            (b << 0x10) | a;

        protected internal static int Pack4Bits(int a, int b, int c, int d, int e, int f, int g, int h) => 
            Pack8Bits((b << 4) | a, (d << 4) | c, (f << 4) | e, (h << 4) | g);

        private static int Pack8Bits(int a, int b, int c, int d) => 
            Pack16Bits((b << 8) | a, (d << 8) | c);

        protected internal static int Unpack4Bits(int i, int[] buffer)
        {
            int num = i;
            return ((buffer[num >> 3] >> (((num & 7) << 2) & 0x1f)) & 15);
        }

        public int CurrentCharLength =>
            this.currentCharLength;

        protected internal abstract int[] CharLenTable { get; }

        protected internal abstract int[] StateTable { get; }

        protected internal abstract int[] ClassTable { get; }
    }
}

