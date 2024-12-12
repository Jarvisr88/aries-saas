namespace DevExpress.Utils.StructuredStorage.Internal.Writer
{
    using System;

    [CLSCompliant(false)]
    public class MiniFat : AbstractFat
    {
        private uint miniFatStart;
        private uint numMiniFatSectors;

        public MiniFat(StructuredStorageContext context) : base(context)
        {
            this.miniFatStart = uint.MaxValue;
        }

        internal override void Write()
        {
            int y = base.Context.Header.SectorSize / 4;
            this.numMiniFatSectors = (uint) base.DivCeiling((long) base.Entries.Count, y);
            this.miniFatStart = base.Context.Fat.WriteChain(this.numMiniFatSectors);
            base.Context.TempOutputStream.WriteSectors(base.Context.InternalBitConverter.GetBytes(base.Entries).ToArray(), base.Context.Header.SectorSize, uint.MaxValue);
        }

        internal uint MiniFatStart =>
            this.miniFatStart;

        internal uint NumMiniFatSectors =>
            this.numMiniFatSectors;
    }
}

