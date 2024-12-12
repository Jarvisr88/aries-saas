namespace DevExpress.Pdf.Native
{
    using System;
    using System.Reflection;

    public interface IFloatArray
    {
        void BlockCopyFrom(Array src, int srcOffset, int dstOffset, int count);
        void BlockCopyTo(int srcOffset, Array dst, int dstOffset, int count);

        float this[int i] { get; set; }

        int Length { get; }
    }
}

