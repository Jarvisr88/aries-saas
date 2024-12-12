namespace DevExpress.Internal
{
    using System;
    using System.IO;

    public abstract class SingleByteEncodingDetector : EncodingDetectorBase
    {
        private const int SampleSize = 0x40;
        private const int EnoughSequensesThreashold = 0x400;
        private const float PositiveShortcutThreshold = 0.95f;
        private const float NegativeShortcutThreshold = 0.05f;
        private const int SymbolCategoryOrder = 250;
        private const int SequenceCategoryCount = 4;
        private const int PosititveCategoryIndex = 3;
        private byte lastCharacterOrder = 0xff;
        private int totalSeqeuenceCount;
        private int[] sequenceCounters = new int[4];
        private int totalCharCount;
        private int frequentCharCount;

        protected SingleByteEncodingDetector()
        {
        }

        protected internal virtual int CalculatePrecedenceMatrixIndex(byte order) => 
            (this.lastCharacterOrder * 0x40) + order;

        protected internal override unsafe DetectionResult ForceAnalyseData(byte[] buffer, int from, int length)
        {
            int num = from + length;
            for (int i = from; i < num; i++)
            {
                byte order = this.CharacterToOrderMap[buffer[i]];
                if (order < 250)
                {
                    this.totalCharCount++;
                }
                if (order < 0x40)
                {
                    this.frequentCharCount++;
                    if (this.lastCharacterOrder < 0x40)
                    {
                        this.totalSeqeuenceCount++;
                        int* numPtr1 = &(this.sequenceCounters[this.GetPrecedenceValue(this.CalculatePrecedenceMatrixIndex(order))]);
                        numPtr1[0]++;
                    }
                }
                this.lastCharacterOrder = order;
            }
            if ((base.CurrentResult == DetectionResult.Detecting) && (this.totalSeqeuenceCount > 0x400))
            {
                float confidence = this.GetConfidence();
                if (confidence > 0.95f)
                {
                    return DetectionResult.Positive;
                }
                if (confidence < 0.05f)
                {
                    return DetectionResult.Negative;
                }
            }
            return base.CurrentResult;
        }

        public override float GetConfidence()
        {
            if (this.totalSeqeuenceCount <= 0)
            {
                return 0.01f;
            }
            float num = ((((1f * this.sequenceCounters[3]) / ((float) this.totalSeqeuenceCount)) / this.PositiveRatio) * this.frequentCharCount) / ((float) this.totalCharCount);
            if (num >= 1f)
            {
                num = 0.99f;
            }
            return num;
        }

        protected internal virtual int GetPrecedenceValue(int index) => 
            Unpack2Bits(index, this.PrecedenceMatrix);

        protected internal static int[] LoadPrecedenceTable(Stream stream)
        {
            int[] numArray2;
            using (BinaryReader reader = new BinaryReader(stream))
            {
                int num = ((int) stream.Length) / 4;
                int[] numArray = new int[num];
                int index = 0;
                while (true)
                {
                    if (index >= num)
                    {
                        numArray2 = numArray;
                        break;
                    }
                    numArray[index] = reader.ReadInt32();
                    index++;
                }
            }
            return numArray2;
        }

        protected internal static int[] LoadPrecedenceTable(string resourceFileName) => 
            LoadPrecedenceTable(typeof(SingleByteEncodingDetector).Assembly.GetManifestResourceStream(resourceFileName));

        protected internal static int Unpack2Bits(int i, int[] buffer)
        {
            int num = i;
            return ((buffer[num >> 4] >> (((num & 15) << 1) & 0x1f)) & 3);
        }

        protected internal abstract float PositiveRatio { get; }

        protected internal abstract byte[] CharacterToOrderMap { get; }

        protected internal abstract int[] PrecedenceMatrix { get; }
    }
}

