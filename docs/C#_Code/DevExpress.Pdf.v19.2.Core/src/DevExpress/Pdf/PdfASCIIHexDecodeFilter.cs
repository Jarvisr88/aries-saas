namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfASCIIHexDecodeFilter : PdfFilter
    {
        internal const string Name = "ASCIIHexDecode";
        internal const string ShortName = "AHx";
        private const byte nullSymbol = 0;
        private const byte horizontalTab = 9;
        private const byte lineFeed = 10;
        private const byte formFeed = 12;
        private const byte carriageReturn = 13;
        private const byte space = 0x20;
        private const byte zero = 0x30;
        private const byte one = 0x31;
        private const byte two = 50;
        private const byte three = 0x33;
        private const byte four = 0x34;
        private const byte five = 0x35;
        private const byte six = 0x36;
        private const byte seven = 0x37;
        private const byte eight = 0x38;
        private const byte nine = 0x39;
        private const byte a = 0x61;
        private const byte b = 0x62;
        private const byte c = 0x63;
        private const byte d = 100;
        private const byte e = 0x65;
        private const byte f = 0x66;
        private const byte capitalA = 0x41;
        private const byte capitalB = 0x42;
        private const byte capitalC = 0x43;
        private const byte capitalD = 0x44;
        private const byte capitalE = 0x45;
        private const byte capitalF = 70;
        private const byte eod = 0x3e;
        private static readonly byte[] endToken = new byte[] { 0x3e };

        internal PdfASCIIHexDecodeFilter()
        {
        }

        protected internal override byte[] Decode(byte[] data)
        {
            byte num4;
            List<byte> list = new List<byte>(data.Length / 2);
            bool flag = true;
            byte item = 0;
            byte[] buffer = data;
            int index = 0;
            goto TR_0015;
        TR_0001:
            index++;
            goto TR_0015;
        TR_0005:
            if (flag)
            {
                item = (byte) (num4 << 4);
            }
            else
            {
                list.Add((byte) (item + num4));
            }
            flag = !flag;
            goto TR_0001;
        TR_0006:
            PdfDocumentStructureReader.ThrowIncorrectDataException();
            num4 = 0;
            goto TR_0005;
        TR_0015:
            while (true)
            {
                if (index >= buffer.Length)
                {
                    return list.ToArray();
                }
                byte num3 = buffer[index];
                if (num3 > 13)
                {
                    if (num3 != 0x20)
                    {
                        switch (num3)
                        {
                            case 0x30:
                            case 0x31:
                            case 50:
                            case 0x33:
                            case 0x34:
                            case 0x35:
                            case 0x36:
                            case 0x37:
                            case 0x38:
                            case 0x39:
                                num4 = (byte) (num3 - 0x30);
                                goto TR_0005;

                            case 0x3a:
                            case 0x3b:
                            case 60:
                            case 0x3d:
                            case 0x3f:
                            case 0x40:
                                break;

                            case 0x3e:
                                if (!flag)
                                {
                                    list.Add(item);
                                }
                                return list.ToArray();

                            case 0x41:
                            case 0x42:
                            case 0x43:
                            case 0x44:
                            case 0x45:
                            case 70:
                                num4 = (byte) ((num3 - 0x41) + 10);
                                goto TR_0005;

                            default:
                                switch (num3)
                                {
                                    case 0x61:
                                    case 0x62:
                                    case 0x63:
                                    case 100:
                                    case 0x65:
                                    case 0x66:
                                        num4 = (byte) ((num3 - 0x61) + 10);
                                        goto TR_0005;

                                    default:
                                        break;
                                }
                                break;
                        }
                        break;
                    }
                    goto TR_0001;
                }
                else
                {
                    if (num3 != 0)
                    {
                        switch (num3)
                        {
                            case 9:
                            case 10:
                            case 12:
                            case 13:
                                break;

                            default:
                                goto TR_0006;
                        }
                    }
                    goto TR_0001;
                }
                break;
            }
            goto TR_0006;
        }

        protected internal override string FilterName =>
            "ASCIIHexDecode";

        internal override byte[] EodToken =>
            endToken;
    }
}

