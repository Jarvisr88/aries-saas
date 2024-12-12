namespace DevExpress.Internal
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Latin1EncodingDetector : EncodingDetectorBase
    {
        private const int FrequentCategoryCount = 4;
        private const byte Udf = 0;
        private const byte Oth = 1;
        private const byte Asc = 2;
        private const byte Ass = 3;
        private const byte Acv = 4;
        private const byte Aco = 5;
        private const byte Asv = 6;
        private const byte Aso = 7;
        private const int ClassCount = 8;
        private static readonly byte[] Latin1_CharToClass = new byte[] { 
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
            2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1,
            1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
            3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 1, 1, 1, 1, 1,
            1, 0, 1, 7, 1, 1, 1, 1, 1, 1, 5, 1, 5, 0, 5, 0,
            0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 7, 1, 7, 0, 7, 5,
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            4, 4, 4, 4, 4, 4, 5, 5, 4, 4, 4, 4, 4, 4, 4, 4,
            5, 5, 4, 4, 4, 4, 4, 1, 4, 4, 4, 4, 4, 5, 5, 5,
            6, 6, 6, 6, 6, 6, 7, 7, 6, 6, 6, 6, 6, 6, 6, 6,
            7, 7, 6, 6, 6, 6, 6, 1, 6, 6, 6, 6, 6, 7, 7, 7
        };
        private static readonly byte[] model = new byte[] { 
            0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 3, 3, 3, 3, 3, 3,
            0, 3, 3, 3, 3, 3, 3, 3, 0, 3, 3, 3, 1, 1, 3, 3,
            0, 3, 3, 3, 1, 2, 1, 2, 0, 3, 3, 3, 3, 3, 3, 3,
            0, 3, 1, 3, 1, 1, 1, 3, 0, 3, 1, 3, 1, 1, 3, 3
        };
        private byte lastCharacterClass = 1;
        private int[] frequencyCounters = new int[4];

        protected internal byte[] FilterBuffer(byte[] buffer, int from, int length)
        {
            List<byte> target = new List<byte>();
            int num = from + length;
            int num2 = from;
            int index = from;
            while (index < num)
            {
                byte currentByte = buffer[index];
                if (!base.IsUpperAsciiByte(currentByte) && base.IsNonEnglishLetterLowerAsciiByte(currentByte))
                {
                    if (index <= num2)
                    {
                        num2 = index + 1;
                    }
                    else
                    {
                        num2 = 1 + base.AppendBytes(buffer, num2, index, target);
                        target.Add(0x20);
                    }
                }
                index++;
            }
            base.AppendBytes(buffer, num2, index, target);
            return target.ToArray();
        }

        protected internal override unsafe DetectionResult ForceAnalyseData(byte[] buffer, int from, int length)
        {
            byte[] buffer2 = this.FilterBuffer(buffer, from, length);
            int num = buffer2.Length;
            for (int i = 0; i < num; i++)
            {
                byte num3 = Latin1_CharToClass[buffer2[i]];
                byte index = model[(this.lastCharacterClass * 8) + num3];
                if (index == 0)
                {
                    return DetectionResult.Negative;
                }
                int* numPtr1 = &(this.frequencyCounters[index]);
                numPtr1[0]++;
                this.lastCharacterClass = num3;
            }
            return base.CurrentResult;
        }

        public override float GetConfidence()
        {
            if (base.CurrentResult == DetectionResult.Negative)
            {
                return 0.01f;
            }
            int num = 0;
            for (int i = 0; i < 4; i++)
            {
                num += this.frequencyCounters[i];
            }
            float num2 = (num > 0) ? (((this.frequencyCounters[3] * 1f) / ((float) num)) - ((this.frequencyCounters[1] * 20f) / ((float) num))) : 0f;
            if (num2 < 0f)
            {
                num2 = 0f;
            }
            return (num2 * 0.5f);
        }

        public override System.Text.Encoding Encoding =>
            DXEncoding.GetEncoding(0x4e4);
    }
}

