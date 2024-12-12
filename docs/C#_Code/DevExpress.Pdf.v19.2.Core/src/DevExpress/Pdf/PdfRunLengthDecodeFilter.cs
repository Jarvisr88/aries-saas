namespace DevExpress.Pdf
{
    using System;
    using System.Collections.Generic;

    public class PdfRunLengthDecodeFilter : PdfFilter
    {
        internal const string Name = "RunLengthDecode";
        internal const string ShortName = "RL";

        internal PdfRunLengthDecodeFilter()
        {
        }

        protected internal override byte[] Decode(byte[] data)
        {
            List<byte> list = new List<byte>();
            int num = 1;
            int num2 = 0;
            foreach (byte num4 in data)
            {
                switch (num)
                {
                    case 1:
                        if (num4 == 0x80)
                        {
                            return list.ToArray();
                        }
                        if (num4 <= 0x7f)
                        {
                            num = 2;
                            num2 = num4 + 1;
                        }
                        else
                        {
                            num = 3;
                            num2 = 0x101 - num4;
                        }
                        break;

                    case 2:
                        num2--;
                        list.Add(num4);
                        if (num2 == 0)
                        {
                            num = 1;
                        }
                        break;

                    case 3:
                        num = 1;
                        for (int i = 0; i < num2; i++)
                        {
                            list.Add(num4);
                        }
                        break;

                    default:
                        break;
                }
            }
            return list.ToArray();
        }

        protected internal override string FilterName =>
            "RunLengthDecode";
    }
}

