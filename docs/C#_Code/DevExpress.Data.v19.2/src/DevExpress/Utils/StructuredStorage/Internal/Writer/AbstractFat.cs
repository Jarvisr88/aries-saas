namespace DevExpress.Utils.StructuredStorage.Internal.Writer
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;

    [CLSCompliant(false)]
    public abstract class AbstractFat
    {
        private readonly List<uint> entries = new List<uint>();
        private readonly StructuredStorageContext context;
        private uint currentEntry;

        protected AbstractFat(StructuredStorageContext context)
        {
            Guard.ArgumentNotNull(context, "context");
            this.context = context;
        }

        protected long DivCeiling(long x, int y)
        {
            long num = x / ((long) y);
            if ((x % ((long) y)) != 0)
            {
                num += 1L;
            }
            return num;
        }

        internal abstract void Write();
        internal uint WriteChain(uint entryCount)
        {
            if (entryCount == 0)
            {
                return 0xfffffffe;
            }
            uint currentEntry = this.currentEntry;
            for (int i = 0; i < (entryCount - 1); i++)
            {
                this.currentEntry++;
                this.entries.Add(this.currentEntry);
            }
            this.currentEntry++;
            this.entries.Add(-2);
            return currentEntry;
        }

        public List<uint> Entries =>
            this.entries;

        public uint CurrentEntry
        {
            get => 
                this.currentEntry;
            set => 
                this.currentEntry = value;
        }

        public StructuredStorageContext Context =>
            this.context;
    }
}

