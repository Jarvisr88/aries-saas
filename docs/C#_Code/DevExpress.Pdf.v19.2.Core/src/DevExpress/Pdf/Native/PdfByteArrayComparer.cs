namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfByteArrayComparer : IComparer<byte[]>
    {
        public int Compare(byte[] x, byte[] y)
        {
            int length = x.Length;
            int num2 = y.Length;
            int num3 = Math.Min(length, num2);
            for (int i = 0; i < num3; i++)
            {
                int num5 = x[i].CompareTo(y[i]);
                if (num5 != 0)
                {
                    return num5;
                }
            }
            return length.CompareTo(num2);
        }
    }
}

