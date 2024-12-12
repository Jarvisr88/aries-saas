namespace DevExpress.Pdf.Native
{
    using System;
    using System.Reflection;

    public class FloatArray : IFloatArray
    {
        private float[] buffer;

        public FloatArray(int count)
        {
            this.buffer = new float[count];
        }

        public FloatArray(float[] buffer)
        {
            this.buffer = buffer;
        }

        public void BlockCopyFrom(Array src, int srcOffset, int dstOffset, int count)
        {
            Buffer.BlockCopy(src, srcOffset * 4, this.buffer, dstOffset * 4, count * 4);
        }

        public void BlockCopyTo(int srcOffset, Array dst, int dstOffset, int count)
        {
            Buffer.BlockCopy(this.buffer, srcOffset * 4, dst, dstOffset * 4, count * 4);
        }

        public float[] GetBuffer() => 
            this.buffer;

        public float this[int i]
        {
            get => 
                this.buffer[i];
            set => 
                this.buffer[i] = value;
        }

        public int Length =>
            this.buffer.Length;
    }
}

