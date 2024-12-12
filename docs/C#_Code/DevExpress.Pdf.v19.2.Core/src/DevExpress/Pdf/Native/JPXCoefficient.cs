namespace DevExpress.Pdf.Native
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct JPXCoefficient
    {
        private const byte significanceMask = 1;
        private const byte signMask = 2;
        private const byte isNotFirstRefinementMask = 0x20;
        private const byte verticalNeighborSignificanceMask = 3;
        private const byte horizontalNeighborSignificanceMask = 12;
        private byte flags;
        private byte bitsDecoded;
        private short magnitude;
        private byte neighborSignificance;
        private byte calculatedSignificancesStepNumber;
        private byte notZeroContextLabelPassNumber;
        public byte Significance
        {
            get => 
                (byte) (this.flags & 1);
            set
            {
                if (value != 0)
                {
                    this.flags = (byte) (this.flags | 1);
                }
            }
        }
        public byte Sign
        {
            get => 
                (byte) (this.flags & 2);
            set
            {
                if (value != 0)
                {
                    this.flags = (byte) (this.flags | 2);
                }
            }
        }
        public short Magnitude
        {
            get => 
                this.magnitude;
            set => 
                this.magnitude = value;
        }
        public bool IsNotFirstRefinement
        {
            get => 
                (this.flags & 0x20) > 0;
            set
            {
                if (value)
                {
                    this.flags = (byte) (this.flags | 0x20);
                }
            }
        }
        public byte BitsDecoded
        {
            get => 
                this.bitsDecoded;
            set => 
                this.bitsDecoded = value;
        }
        public byte VerticalNeighborSignificance =>
            (byte) (this.neighborSignificance & 3);
        public byte HorizontalNeighborSignificance =>
            (byte) ((this.neighborSignificance & 12) >> 2);
        public byte DiagonalNeighborSignificance =>
            (byte) (this.neighborSignificance >> 4);
        public byte RawNeighborSignificance =>
            this.neighborSignificance;
        public byte CalculatedSignificancesStepNumber
        {
            get => 
                this.calculatedSignificancesStepNumber;
            set => 
                this.calculatedSignificancesStepNumber = value;
        }
        public byte NotZeroContextLabelPassNumber
        {
            get => 
                this.notZeroContextLabelPassNumber;
            set => 
                this.notZeroContextLabelPassNumber = value;
        }
        public void IncrementDiagonalNeighborSignificance()
        {
            this.neighborSignificance = (byte) (this.neighborSignificance + 0x10);
        }

        public void IncrementHorizontalNeighborSignificance()
        {
            this.neighborSignificance = (byte) (this.neighborSignificance + 4);
        }

        public void IncrementVerticalNeighborSignificance()
        {
            this.neighborSignificance = (byte) (this.neighborSignificance + 1);
        }

        public int ToInteger() => 
            (this.Significance * this.magnitude) * (1 - this.Sign);
    }
}

