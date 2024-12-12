namespace DevExpress.Office
{
    using System;

    public class SizeOfInfo
    {
        private readonly string displayName;
        private readonly int sizeOf;
        private readonly int count;

        public SizeOfInfo(string displayName, int sizeOf, int count)
        {
            this.displayName = displayName;
            this.sizeOf = sizeOf;
            this.count = count;
        }

        public string DisplayName =>
            this.displayName;

        public int SizeOf =>
            this.sizeOf;

        public int Count =>
            this.count;
    }
}

