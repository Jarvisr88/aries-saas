namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class ChunkedFloatArray : IFloatArray
    {
        private const int chunkSize = 0x4e20;
        private int count;
        private List<float[]> chunks;

        public ChunkedFloatArray(int count)
        {
            this.count = count;
            this.chunks = new List<float[]>();
            while (count > 0)
            {
                int num = Math.Min(count, 0x4e20);
                count -= num;
                this.chunks.Add(new float[num]);
            }
        }

        public void BlockCopyFrom(Array src, int srcOffset, int dstOffset, int count)
        {
            while (count > 0)
            {
                int num = dstOffset / 0x4e20;
                int num2 = dstOffset % 0x4e20;
                int num3 = ((0x4e20 - num2) > count) ? count : (0x4e20 - num2);
                Buffer.BlockCopy(src, srcOffset * 4, this.chunks[num], num2 * 4, num3 * 4);
                dstOffset += num3;
                count -= num3;
                srcOffset += num3;
            }
        }

        public void BlockCopyTo(int srcOffset, Array dst, int dstOffset, int count)
        {
            while (count > 0)
            {
                int num = srcOffset / 0x4e20;
                int num2 = srcOffset % 0x4e20;
                int num3 = ((0x4e20 - num2) > count) ? count : (0x4e20 - num2);
                Buffer.BlockCopy(this.chunks[num], num2 * 4, dst, dstOffset * 4, num3 * 4);
                dstOffset += num3;
                srcOffset += num3;
                count -= num3;
            }
        }

        public int Length =>
            this.count;

        public float this[int i]
        {
            get => 
                this.chunks[i / 0x4e20][i % 0x4e20];
            set => 
                this.chunks[i / 0x4e20][i % 0x4e20] = (float[]) value;
        }
    }
}

