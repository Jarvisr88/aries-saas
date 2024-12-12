namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    internal class TTFUtils
    {
        private TTFUtils()
        {
        }

        public static uint CalculateCheckSum(TTFStream ttfStream, int start, int length)
        {
            uint num5;
            int num = length % 4;
            if (num > 0)
            {
                length += 4 - num;
            }
            length /= 4;
            int position = ttfStream.Position;
            try
            {
                ttfStream.Seek(start);
                uint num3 = 0;
                int num4 = 0;
                while (true)
                {
                    if (num4 >= length)
                    {
                        num5 = num3;
                        break;
                    }
                    num3 += ttfStream.ReadULong();
                    num4++;
                }
            }
            finally
            {
                ttfStream.Seek(position);
            }
            return num5;
        }
    }
}

