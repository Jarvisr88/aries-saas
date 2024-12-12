namespace DevExpress.Office.Utils
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct CombinedHashCode
    {
        public const long Initial = 0x1505L;
        private long combinedHash;
        public CombinedHashCode(long initial)
        {
            this.combinedHash = initial;
        }

        public void AddInt(int n)
        {
            this.combinedHash = ((this.combinedHash << 5) + this.combinedHash) ^ n;
        }

        public void AddObject(object obj)
        {
            if (obj != null)
            {
                this.AddInt(obj.GetHashCode());
            }
        }

        public void AddByteArray(byte[] array)
        {
            int length = array.Length;
            for (int i = 0; i < length; i++)
            {
                this.AddInt(array[i]);
            }
        }

        public long CombinedHash =>
            this.combinedHash;
        public int CombinedHash32 =>
            this.combinedHash.GetHashCode();
    }
}

