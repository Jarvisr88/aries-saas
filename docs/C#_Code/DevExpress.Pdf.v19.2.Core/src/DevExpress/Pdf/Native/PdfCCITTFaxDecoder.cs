namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class PdfCCITTFaxDecoder
    {
        private static readonly Dictionary<string, int> whiteRunLengths;
        private static readonly Dictionary<string, int> blackRunLengths;
        private static readonly Dictionary<string, int> commonRunLengths;
        private static readonly PdfHuffmanTreeBranch whiteTree;
        private static readonly PdfHuffmanTreeBranch blackTree;
        private readonly byte[] data;
        private readonly int length;
        private readonly bool blackIs1;
        private readonly bool alignEncodedBytes;
        private readonly int twoDimensionalLineCount;
        private readonly int columns;
        private readonly int size;
        private readonly List<byte> result;
        private readonly int lineSize;
        private byte[] referenceLine;
        private byte[] decodingLine;
        private int currentPosition;
        private byte currentByte;
        private int currentByteOffset = 7;
        private bool isBlack;
        private int a0;
        private int a1;
        private int b1;
        private int b2;

        static PdfCCITTFaxDecoder()
        {
            Dictionary<string, int> dictionary1 = new Dictionary<string, int>();
            dictionary1.Add("00110101", 0);
            dictionary1.Add("000111", 1);
            dictionary1.Add("0111", 2);
            dictionary1.Add("1000", 3);
            dictionary1.Add("1011", 4);
            dictionary1.Add("1100", 5);
            dictionary1.Add("1110", 6);
            dictionary1.Add("1111", 7);
            dictionary1.Add("10011", 8);
            dictionary1.Add("10100", 9);
            dictionary1.Add("00111", 10);
            dictionary1.Add("01000", 11);
            dictionary1.Add("001000", 12);
            dictionary1.Add("000011", 13);
            dictionary1.Add("110100", 14);
            dictionary1.Add("110101", 15);
            dictionary1.Add("101010", 0x10);
            dictionary1.Add("101011", 0x11);
            dictionary1.Add("0100111", 0x12);
            dictionary1.Add("0001100", 0x13);
            dictionary1.Add("0001000", 20);
            dictionary1.Add("0010111", 0x15);
            dictionary1.Add("0000011", 0x16);
            dictionary1.Add("0000100", 0x17);
            dictionary1.Add("0101000", 0x18);
            dictionary1.Add("0101011", 0x19);
            dictionary1.Add("0010011", 0x1a);
            dictionary1.Add("0100100", 0x1b);
            dictionary1.Add("0011000", 0x1c);
            dictionary1.Add("00000010", 0x1d);
            dictionary1.Add("00000011", 30);
            dictionary1.Add("00011010", 0x1f);
            dictionary1.Add("00011011", 0x20);
            dictionary1.Add("00010010", 0x21);
            dictionary1.Add("00010011", 0x22);
            dictionary1.Add("00010100", 0x23);
            dictionary1.Add("00010101", 0x24);
            dictionary1.Add("00010110", 0x25);
            dictionary1.Add("00010111", 0x26);
            dictionary1.Add("00101000", 0x27);
            dictionary1.Add("00101001", 40);
            dictionary1.Add("00101010", 0x29);
            dictionary1.Add("00101011", 0x2a);
            dictionary1.Add("00101100", 0x2b);
            dictionary1.Add("00101101", 0x2c);
            dictionary1.Add("00000100", 0x2d);
            dictionary1.Add("00000101", 0x2e);
            dictionary1.Add("00001010", 0x2f);
            dictionary1.Add("00001011", 0x30);
            dictionary1.Add("01010010", 0x31);
            dictionary1.Add("01010011", 50);
            dictionary1.Add("01010100", 0x33);
            dictionary1.Add("01010101", 0x34);
            dictionary1.Add("00100100", 0x35);
            dictionary1.Add("00100101", 0x36);
            dictionary1.Add("01011000", 0x37);
            dictionary1.Add("01011001", 0x38);
            dictionary1.Add("01011010", 0x39);
            dictionary1.Add("01011011", 0x3a);
            dictionary1.Add("01001010", 0x3b);
            dictionary1.Add("01001011", 60);
            dictionary1.Add("00110010", 0x3d);
            dictionary1.Add("00110011", 0x3e);
            dictionary1.Add("00110100", 0x3f);
            dictionary1.Add("11011", 0x40);
            dictionary1.Add("10010", 0x80);
            dictionary1.Add("010111", 0xc0);
            dictionary1.Add("0110111", 0x100);
            dictionary1.Add("00110110", 320);
            dictionary1.Add("00110111", 0x180);
            dictionary1.Add("01100100", 0x1c0);
            dictionary1.Add("01100101", 0x200);
            dictionary1.Add("01101000", 0x240);
            dictionary1.Add("01100111", 640);
            dictionary1.Add("011001100", 0x2c0);
            dictionary1.Add("011001101", 0x300);
            dictionary1.Add("011010010", 0x340);
            dictionary1.Add("011010011", 0x380);
            dictionary1.Add("011010100", 960);
            dictionary1.Add("011010101", 0x400);
            dictionary1.Add("011010110", 0x440);
            dictionary1.Add("011010111", 0x480);
            dictionary1.Add("011011000", 0x4c0);
            dictionary1.Add("011011001", 0x500);
            dictionary1.Add("011011010", 0x540);
            dictionary1.Add("011011011", 0x580);
            dictionary1.Add("010011000", 0x5c0);
            dictionary1.Add("010011001", 0x600);
            dictionary1.Add("010011010", 0x640);
            dictionary1.Add("011000", 0x680);
            dictionary1.Add("010011011", 0x6c0);
            dictionary1.Add("000000000001", -1);
            whiteRunLengths = dictionary1;
            Dictionary<string, int> dictionary2 = new Dictionary<string, int>();
            dictionary2.Add("0000110111", 0);
            dictionary2.Add("010", 1);
            dictionary2.Add("11", 2);
            dictionary2.Add("10", 3);
            dictionary2.Add("011", 4);
            dictionary2.Add("0011", 5);
            dictionary2.Add("0010", 6);
            dictionary2.Add("00011", 7);
            dictionary2.Add("000101", 8);
            dictionary2.Add("000100", 9);
            dictionary2.Add("0000100", 10);
            dictionary2.Add("0000101", 11);
            dictionary2.Add("0000111", 12);
            dictionary2.Add("00000100", 13);
            dictionary2.Add("00000111", 14);
            dictionary2.Add("000011000", 15);
            dictionary2.Add("0000010111", 0x10);
            dictionary2.Add("0000011000", 0x11);
            dictionary2.Add("0000001000", 0x12);
            dictionary2.Add("00001100111", 0x13);
            dictionary2.Add("00001101000", 20);
            dictionary2.Add("00001101100", 0x15);
            dictionary2.Add("00000110111", 0x16);
            dictionary2.Add("00000101000", 0x17);
            dictionary2.Add("00000010111", 0x18);
            dictionary2.Add("00000011000", 0x19);
            dictionary2.Add("000011001010", 0x1a);
            dictionary2.Add("000011001011", 0x1b);
            dictionary2.Add("000011001100", 0x1c);
            dictionary2.Add("000011001101", 0x1d);
            dictionary2.Add("000001101000", 30);
            dictionary2.Add("000001101001", 0x1f);
            dictionary2.Add("000001101010", 0x20);
            dictionary2.Add("000001101011", 0x21);
            dictionary2.Add("000011010010", 0x22);
            dictionary2.Add("000011010011", 0x23);
            dictionary2.Add("000011010100", 0x24);
            dictionary2.Add("000011010101", 0x25);
            dictionary2.Add("000011010110", 0x26);
            dictionary2.Add("000011010111", 0x27);
            dictionary2.Add("000001101100", 40);
            dictionary2.Add("000001101101", 0x29);
            dictionary2.Add("000011011010", 0x2a);
            dictionary2.Add("000011011011", 0x2b);
            dictionary2.Add("000001010100", 0x2c);
            dictionary2.Add("000001010101", 0x2d);
            dictionary2.Add("000001010110", 0x2e);
            dictionary2.Add("000001010111", 0x2f);
            dictionary2.Add("000001100100", 0x30);
            dictionary2.Add("000001100101", 0x31);
            dictionary2.Add("000001010010", 50);
            dictionary2.Add("000001010011", 0x33);
            dictionary2.Add("000000100100", 0x34);
            dictionary2.Add("000000110111", 0x35);
            dictionary2.Add("000000111000", 0x36);
            dictionary2.Add("000000100111", 0x37);
            dictionary2.Add("000000101000", 0x38);
            dictionary2.Add("000001011000", 0x39);
            dictionary2.Add("000001011001", 0x3a);
            dictionary2.Add("000000101011", 0x3b);
            dictionary2.Add("000000101100", 60);
            dictionary2.Add("000001011010", 0x3d);
            dictionary2.Add("000001100110", 0x3e);
            dictionary2.Add("000001100111", 0x3f);
            dictionary2.Add("0000001111", 0x40);
            dictionary2.Add("000011001000", 0x80);
            dictionary2.Add("000011001001", 0xc0);
            dictionary2.Add("000001011011", 0x100);
            dictionary2.Add("000000110011", 320);
            dictionary2.Add("000000110100", 0x180);
            dictionary2.Add("000000110101", 0x1c0);
            dictionary2.Add("0000001101100", 0x200);
            dictionary2.Add("0000001101101", 0x240);
            dictionary2.Add("0000001001010", 640);
            dictionary2.Add("0000001001011", 0x2c0);
            dictionary2.Add("0000001001100", 0x300);
            dictionary2.Add("0000001001101", 0x340);
            dictionary2.Add("0000001110010", 0x380);
            dictionary2.Add("0000001110011", 960);
            dictionary2.Add("0000001110100", 0x400);
            dictionary2.Add("0000001110101", 0x440);
            dictionary2.Add("0000001110110", 0x480);
            dictionary2.Add("0000001110111", 0x4c0);
            dictionary2.Add("0000001010010", 0x500);
            dictionary2.Add("0000001010011", 0x540);
            dictionary2.Add("0000001010100", 0x580);
            dictionary2.Add("0000001010101", 0x5c0);
            dictionary2.Add("0000001011010", 0x600);
            dictionary2.Add("0000001011011", 0x640);
            dictionary2.Add("0000001100100", 0x680);
            dictionary2.Add("0000001100101", 0x6c0);
            dictionary2.Add("000000000001", -1);
            blackRunLengths = dictionary2;
            Dictionary<string, int> dictionary3 = new Dictionary<string, int>();
            dictionary3.Add("00000001000", 0x700);
            dictionary3.Add("00000001100", 0x740);
            dictionary3.Add("00000001101", 0x780);
            dictionary3.Add("000000010010", 0x7c0);
            dictionary3.Add("000000010011", 0x800);
            dictionary3.Add("000000010100", 0x840);
            dictionary3.Add("000000010101", 0x880);
            dictionary3.Add("000000010110", 0x8c0);
            dictionary3.Add("000000010111", 0x900);
            dictionary3.Add("000000011100", 0x940);
            dictionary3.Add("000000011101", 0x980);
            dictionary3.Add("000000011110", 0x9c0);
            dictionary3.Add("000000011111", 0xa00);
            commonRunLengths = dictionary3;
            whiteTree = new PdfHuffmanTreeBranch();
            blackTree = new PdfHuffmanTreeBranch();
            whiteTree.Fill(whiteRunLengths);
            whiteTree.Fill(commonRunLengths);
            blackTree.Fill(blackRunLengths);
            blackTree.Fill(commonRunLengths);
        }

        private PdfCCITTFaxDecoder(PdfCCITTFaxDecoderParameters parameters, byte[] data)
        {
            this.data = data;
            this.length = data.Length;
            this.blackIs1 = parameters.BlackIs1;
            this.alignEncodedBytes = parameters.EncodedByteAlign;
            this.twoDimensionalLineCount = parameters.TwoDimensionalLineCount;
            this.columns = parameters.Columns;
            this.size = this.columns / 8;
            if ((this.columns % 8) != 0)
            {
                this.size++;
            }
            this.size *= parameters.Rows;
            this.result = (this.size == 0) ? new List<byte>() : new List<byte>(this.size);
            this.lineSize = this.columns / 8;
            if ((this.columns % 8) > 0)
            {
                this.lineSize++;
            }
            this.referenceLine = new byte[this.lineSize];
            this.decodingLine = new byte[this.lineSize];
            if (this.length > 0)
            {
                this.currentByte = data[0];
            }
            this.b1 = this.columns;
            this.b2 = this.columns;
        }

        private void AccumulateResult()
        {
            if (!this.blackIs1)
            {
                this.result.AddRange(this.decodingLine);
            }
            else
            {
                int index = 0;
                while (true)
                {
                    if (index >= this.lineSize)
                    {
                        this.result.AddRange(this.referenceLine);
                        break;
                    }
                    this.referenceLine[index] = (byte) (this.decodingLine[index] ^ 0xff);
                    index++;
                }
            }
            if (this.alignEncodedBytes && (this.currentByteOffset < 7))
            {
                this.MoveNextByte();
            }
        }

        public static byte[] Decode(PdfCCITTFaxDecoderParameters parameters, byte[] data)
        {
            try
            {
                return PerformDecode(parameters, data);
            }
            catch
            {
                return PerformDecode(new PdfCCITTFaxDecoderParameters(parameters.BlackIs1, !parameters.EncodedByteAlign, parameters.TwoDimensionalLineCount, parameters.Columns, parameters.Rows, parameters.EncodingScheme), data);
            }
        }

        public static byte[] Decode(PdfCCITTFaxDecodeFilter filter, byte[] data) => 
            Decode(new PdfCCITTFaxDecoderParameters(filter), data);

        private void DecodeGroup3()
        {
            while (this.DecodeGroup3Line())
            {
            }
        }

        private bool DecodeGroup3Line()
        {
            while (true)
            {
                PdfHuffmanTreeBranch branch = this.isBlack ? blackTree : whiteTree;
                int num = this.FindRunningLength(branch);
                if (num < 0)
                {
                    num = this.FindRunningLength(branch);
                    if (num < 0)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (this.FindRunningLength(branch) > 0)
                            {
                                PdfDocumentStructureReader.ThrowIncorrectDataException();
                            }
                        }
                        return false;
                    }
                }
                this.a1 = this.a0 + num;
                this.FillDecodingLine(this.a0, this.a1, this.isBlack);
                this.a0 = this.a1;
                this.isBlack = !this.isBlack;
                if (this.a0 == this.columns)
                {
                    this.AccumulateResult();
                    this.a0 = 0;
                    this.isBlack = false;
                    return !this.Completed;
                }
            }
        }

        private void DecodeGroup3TwoDimensional()
        {
            int twoDimensionalLineCount;
            this.ReadEOL();
            if (!this.ReadBit())
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            if (!this.DecodeGroup3Line())
            {
                return;
            }
            else
            {
                this.ReadEOL();
                twoDimensionalLineCount = this.twoDimensionalLineCount;
                while (!this.Completed)
                {
                    if (!this.ReadBit())
                    {
                        twoDimensionalLineCount = Math.Max(0, twoDimensionalLineCount - this.DecodeGroup4());
                        continue;
                    }
                    if (twoDimensionalLineCount > 0)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    if (!this.DecodeGroup3Line())
                    {
                        break;
                    }
                    twoDimensionalLineCount = this.twoDimensionalLineCount;
                    this.ReadEOL();
                }
            }
            if (twoDimensionalLineCount > 0)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
        }

        private int DecodeGroup4()
        {
            int num = 0;
            while (true)
            {
                CodingMode mode = this.ReadMode();
                if (mode == CodingMode.Pass)
                {
                    this.FillDecodingLine(this.a0, this.b2, this.isBlack);
                    this.isBlack = !this.isBlack;
                    this.a0 = this.b2;
                }
                else if (mode == CodingMode.Horizontal)
                {
                    PdfHuffmanTreeBranch blackTree;
                    PdfHuffmanTreeBranch whiteTree;
                    if (this.isBlack)
                    {
                        blackTree = PdfCCITTFaxDecoder.blackTree;
                        whiteTree = PdfCCITTFaxDecoder.whiteTree;
                    }
                    else
                    {
                        blackTree = PdfCCITTFaxDecoder.whiteTree;
                        whiteTree = PdfCCITTFaxDecoder.blackTree;
                    }
                    int num2 = this.FindRunningLength(blackTree);
                    if (num2 < 0)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    int num3 = this.FindRunningLength(whiteTree);
                    if (num3 < 0)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    this.a1 = this.a0 + num2;
                    int num4 = this.a1 + num3;
                    if (num2 > 0)
                    {
                        this.FillDecodingLine(this.a0, this.a1, this.isBlack);
                    }
                    if (num3 > 0)
                    {
                        this.FillDecodingLine(this.a1, num4, !this.isBlack);
                    }
                    this.isBlack = !this.isBlack;
                    this.a0 = num4;
                }
                else
                {
                    if (mode == CodingMode.EndOfData)
                    {
                        if (this.a0 != 0)
                        {
                            PdfDocumentStructureReader.ThrowIncorrectDataException();
                        }
                        return num;
                    }
                    switch (mode)
                    {
                        case CodingMode.Vertical0:
                            this.a1 = this.b1;
                            break;

                        case CodingMode.VerticalRight1:
                            this.a1 = this.b1 + 1;
                            break;

                        case CodingMode.VerticalRight2:
                            this.a1 = this.b1 + 2;
                            break;

                        case CodingMode.VerticalRight3:
                            this.a1 = this.b1 + 3;
                            break;

                        case CodingMode.VerticalLeft1:
                            this.a1 = this.b1 - 1;
                            break;

                        case CodingMode.VerticalLeft2:
                            this.a1 = this.b1 - 2;
                            break;

                        case CodingMode.VerticalLeft3:
                            this.a1 = this.b1 - 3;
                            break;

                        default:
                            break;
                    }
                    this.FillDecodingLine(this.a0, this.a1, this.isBlack);
                    this.a0 = this.a1;
                }
                if (this.a0 != this.columns)
                {
                    this.isBlack = !this.isBlack;
                    this.b1 = this.FindB(this.FindB(this.a0, !this.isBlack), this.isBlack);
                    this.b2 = this.FindB(this.b1, !this.isBlack);
                }
                else
                {
                    this.AccumulateResult();
                    num++;
                    if (this.Completed && (this.currentPosition >= (this.length - 1)))
                    {
                        return num;
                    }
                    this.NextLine();
                }
            }
        }

        private static byte FillByte(byte b, int start, int end, bool black)
        {
            byte num = (byte) ((0xff >> (start & 0x1f)) & (0xff << ((8 - end) & 0x1f)));
            return (black ? ((byte) (b & (0xff ^ num))) : ((byte) (b | num)));
        }

        private void FillDecodingLine(int a0, int a1, bool black)
        {
            if ((a0 != 0) || (a1 != 0))
            {
                if ((a1 <= a0) || (a1 > this.columns))
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                int index = a0 / 8;
                int num2 = a1 / 8;
                if (index == num2)
                {
                    this.decodingLine[index] = FillByte(this.decodingLine[index], a0 % 8, a1 % 8, black);
                }
                else
                {
                    this.decodingLine[index] = FillByte(this.decodingLine[index], a0 % 8, 8, black);
                    int num3 = index + 1;
                    while (true)
                    {
                        if (num3 >= num2)
                        {
                            if (num2 < this.lineSize)
                            {
                                this.decodingLine[num2] = FillByte(this.decodingLine[num2], 0, a1 % 8, black);
                            }
                            break;
                        }
                        this.decodingLine[num3] = black ? ((byte) 0) : ((byte) 0xff);
                        num3++;
                    }
                }
            }
        }

        private int FindB(int startPosition, bool isWhite)
        {
            if (startPosition != this.columns)
            {
                int num = startPosition;
                int index = startPosition / 8;
                int num3 = startPosition % 8;
                byte num4 = (byte) (this.referenceLine[index] << (num3 & 0x1f));
                int num5 = 0;
                while (num5 < this.columns)
                {
                    if (((num4 & 0x80) == 0x80) == isWhite)
                    {
                        return num;
                    }
                    if (++num3 != 8)
                    {
                        num4 = (byte) (num4 << 1);
                    }
                    else
                    {
                        if (++index == this.lineSize)
                        {
                            return this.columns;
                        }
                        num4 = this.referenceLine[index];
                        while (true)
                        {
                            if ((!isWhite || (num4 != 0)) && (isWhite || (num4 != 0xff)))
                            {
                                num3 = 0;
                                break;
                            }
                            num5 += 8;
                            num += 8;
                            if (++index == this.lineSize)
                            {
                                return this.columns;
                            }
                            num4 = this.referenceLine[index];
                        }
                    }
                    num5++;
                    num++;
                }
            }
            return this.columns;
        }

        private int FindRunningLength(PdfHuffmanTreeBranch branch)
        {
            int num = this.FindRunningLengthPart(branch);
            if (num < 0x40)
            {
                return num;
            }
            int num2 = num;
            while (num == 0xa00)
            {
                num = this.FindRunningLengthPart(branch);
                num2 += num;
            }
            if (num >= 0x40)
            {
                num = this.FindRunningLengthPart(branch);
                if (num >= 0x40)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                num2 += num;
            }
            return num2;
        }

        private int FindRunningLengthPart(PdfHuffmanTreeBranch branch)
        {
            int runLength;
            try
            {
                int num = 0;
                while (true)
                {
                    bool flag = this.ReadBit();
                    PdfHuffmanTreeNode node = flag ? branch.One : branch.Zero;
                    if (node != null)
                    {
                        PdfHuffmanTreeLeaf leaf = node as PdfHuffmanTreeLeaf;
                        if (leaf == null)
                        {
                            branch = node as PdfHuffmanTreeBranch;
                            if (branch == null)
                            {
                                PdfDocumentStructureReader.ThrowIncorrectDataException();
                            }
                            num++;
                            continue;
                        }
                        runLength = leaf.RunLength;
                    }
                    else
                    {
                        if (flag)
                        {
                            PdfDocumentStructureReader.ThrowIncorrectDataException();
                        }
                        while (true)
                        {
                            if (num >= 10)
                            {
                                while (true)
                                {
                                    if (this.ReadBit())
                                    {
                                        runLength = -1;
                                        break;
                                    }
                                }
                                break;
                            }
                            if (this.ReadBit())
                            {
                                PdfDocumentStructureReader.ThrowIncorrectDataException();
                            }
                            num++;
                        }
                    }
                    break;
                }
            }
            catch
            {
                runLength = -1;
            }
            return runLength;
        }

        private void MoveNextByte()
        {
            int num = this.currentPosition + 1;
            this.currentPosition = num;
            if (num < this.length)
            {
                this.currentByte = this.data[this.currentPosition];
            }
            this.currentByteOffset = 7;
        }

        private void NextLine()
        {
            byte[] referenceLine = this.referenceLine;
            this.referenceLine = this.decodingLine;
            this.decodingLine = referenceLine;
            this.isBlack = false;
            this.a0 = 0;
            this.b1 = this.FindB(0, false);
            this.b2 = this.FindB(this.b1, true);
        }

        private static byte[] PerformDecode(PdfCCITTFaxDecoderParameters parameters, byte[] data)
        {
            PdfCCITTFaxDecoder decoder = new PdfCCITTFaxDecoder(parameters, data);
            if (decoder.length > 0)
            {
                PdfCCITTFaxEncodingScheme encodingScheme = parameters.EncodingScheme;
                if (encodingScheme == PdfCCITTFaxEncodingScheme.TwoDimensional)
                {
                    decoder.DecodeGroup4();
                }
                else if (encodingScheme != PdfCCITTFaxEncodingScheme.OneDimensional)
                {
                    decoder.DecodeGroup3TwoDimensional();
                }
                else
                {
                    decoder.DecodeGroup3();
                }
            }
            return decoder.result.ToArray();
        }

        private bool ReadBit()
        {
            if (this.currentPosition >= this.length)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            bool flag = ((this.currentByte >> (this.currentByteOffset & 0x1f)) & 1) == 1;
            int num = this.currentByteOffset - 1;
            this.currentByteOffset = num;
            if (num < 0)
            {
                this.MoveNextByte();
            }
            return flag;
        }

        private void ReadEOL()
        {
            int num = 1;
            while (!this.ReadBit())
            {
                num++;
            }
            if (num < 12)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.NextLine();
        }

        private CodingMode ReadMode()
        {
            int num = 1;
            try
            {
                while (!this.ReadBit())
                {
                    num++;
                }
            }
            catch (ArgumentException)
            {
                return CodingMode.EndOfData;
            }
            switch (num)
            {
                case 1:
                    return CodingMode.Vertical0;

                case 2:
                    return (this.ReadBit() ? CodingMode.VerticalRight1 : CodingMode.VerticalLeft1);

                case 3:
                    return CodingMode.Horizontal;

                case 4:
                    return CodingMode.Pass;

                case 5:
                    return (this.ReadBit() ? CodingMode.VerticalRight2 : CodingMode.VerticalLeft2);

                case 6:
                    return (this.ReadBit() ? CodingMode.VerticalRight3 : CodingMode.VerticalLeft3);
            }
            return CodingMode.EndOfData;
        }

        private bool Completed =>
            (this.size == 0) ? (this.currentPosition >= this.length) : (this.result.Count >= this.size);

        private enum CodingMode
        {
            Pass,
            Horizontal,
            Vertical0,
            VerticalRight1,
            VerticalRight2,
            VerticalRight3,
            VerticalLeft1,
            VerticalLeft2,
            VerticalLeft3,
            EndOfData
        }
    }
}

