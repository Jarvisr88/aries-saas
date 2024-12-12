namespace DevExpress.Utils.StructuredStorage.Writer
{
    using DevExpress.Utils;
    using DevExpress.Utils.StructuredStorage.Internal.Writer;
    using System;
    using System.IO;

    [CLSCompliant(false)]
    public class VirtualStream
    {
        private readonly AbstractFat fat;
        private readonly Stream stream;
        private readonly ushort sectorSize;
        private readonly OutputHandler outputHander;
        private uint startSector = uint.MaxValue;
        private uint sectorCount;

        public VirtualStream(Stream stream, AbstractFat fat, ushort sectorSize, OutputHandler outputHander)
        {
            Guard.ArgumentNotNull(stream, "stream");
            Guard.ArgumentNotNull(fat, "fat");
            this.stream = stream;
            this.fat = fat;
            this.sectorSize = sectorSize;
            this.outputHander = outputHander;
            this.sectorCount = (uint) Math.Ceiling((double) (((double) stream.Length) / ((double) sectorSize)));
        }

        public void Write()
        {
            this.startSector = this.fat.WriteChain(this.SectorCount);
            byte[] buffer = new byte[this.sectorSize];
            this.stream.Seek(0L, SeekOrigin.Begin);
            while (true)
            {
                int dataSize = this.stream.Read(buffer, 0, this.sectorSize);
                this.outputHander.WriteSectors(buffer, dataSize, this.sectorSize, 0);
                if (dataSize != this.sectorSize)
                {
                    return;
                }
            }
        }

        public uint StartSector =>
            this.startSector;

        public ulong Length =>
            (ulong) this.stream.Length;

        public uint SectorCount =>
            this.sectorCount;
    }
}

