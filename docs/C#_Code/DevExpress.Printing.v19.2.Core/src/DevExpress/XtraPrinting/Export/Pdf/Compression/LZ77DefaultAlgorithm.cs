namespace DevExpress.XtraPrinting.Export.Pdf.Compression
{
    using System;

    public class LZ77DefaultAlgorithm : ILZ77
    {
        private int windowSize;
        private int dictionarySize;
        private int position;
        private byte[] input;
        private const int minLength = 3;
        private const int maxLength = 0x102;
        private const int maxOffset = 0x8000;
        private int[] literalOffsets;

        public LZ77DefaultAlgorithm(byte[] input) : this(input, 15)
        {
        }

        public LZ77DefaultAlgorithm(byte[] input, int windowSizeExponent)
        {
            if ((windowSizeExponent < 8) || (windowSizeExponent > 15))
            {
                windowSizeExponent = 15;
            }
            this.windowSize = 1 << (windowSizeExponent & 0x1f);
            this.dictionarySize = this.windowSize - 0x102;
            this.Reset(input);
        }

        private int CreateLiteralOffsets(byte literal)
        {
            int newLength = (this.dictionarySize < this.position) ? this.dictionarySize : this.position;
            this.UpdateLiteralOfssetsArray(newLength, true);
            int index = 0;
            for (int i = 1; i <= newLength; i++)
            {
                if (this.input[this.position - i] == literal)
                {
                    this.literalOffsets[index] = i;
                    index++;
                }
            }
            return index;
        }

        private int GetMatchLength(int offset)
        {
            int num = this.input.Length - this.position;
            int num2 = 1;
            int num3 = (0x102 < num) ? 0x102 : num;
            int num4 = this.position - offset;
            for (int i = 1; (i < num3) && (this.input[num4 + i] == this.input[this.position + i]); i++)
            {
                num2++;
            }
            return num2;
        }

        public bool Next(LZ77ResultValue resultValue)
        {
            int num3;
            int num4;
            if (resultValue == null)
            {
                return false;
            }
            if (this.position >= this.input.Length)
            {
                return false;
            }
            byte literal = this.input[this.position];
            int num2 = this.CreateLiteralOffsets(literal);
            if (num2 <= 0)
            {
                resultValue.Literal = literal;
                resultValue.IsLiteral = true;
                this.UpdateWindow(1);
                goto TR_0002;
            }
            else
            {
                num3 = 0;
                num4 = 0;
                for (int i = 0; i < num2; i++)
                {
                    int matchLength = this.GetMatchLength(this.literalOffsets[i]);
                    if (matchLength > num4)
                    {
                        num3 = this.literalOffsets[i];
                        num4 = matchLength;
                        if (num4 > 0x102)
                        {
                            num4 = 0x102;
                            break;
                        }
                    }
                }
            }
            if (num4 < 3)
            {
                num4 = 1;
            }
            if (num4 <= 1)
            {
                resultValue.Literal = literal;
                resultValue.IsLiteral = true;
            }
            else
            {
                if (num3 > 0x8000)
                {
                    throw new CompressException("The phrase offset is out of bounds");
                }
                resultValue.Offset = num3;
                resultValue.Length = num4;
                resultValue.IsLiteral = false;
            }
            this.UpdateWindow(num4);
        TR_0002:
            return true;
        }

        public void Reset()
        {
            this.position = 0;
        }

        public void Reset(byte[] input)
        {
            this.input = input;
            this.UpdateLiteralOfssetsArray(input.Length, false);
            this.Reset();
        }

        private void UpdateLiteralOfssetsArray(int newLength, bool failOnRecreate)
        {
            if ((this.literalOffsets == null) || (this.literalOffsets.Length < newLength))
            {
                this.literalOffsets = new int[newLength];
                bool flag1 = failOnRecreate;
            }
        }

        private void UpdateWindow(int length)
        {
            this.position += length;
        }
    }
}

