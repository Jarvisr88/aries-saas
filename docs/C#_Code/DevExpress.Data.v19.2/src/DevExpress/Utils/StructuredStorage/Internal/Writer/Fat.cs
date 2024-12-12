namespace DevExpress.Utils.StructuredStorage.Internal.Writer
{
    using System;
    using System.Collections.Generic;

    [CLSCompliant(false)]
    public class Fat : AbstractFat
    {
        private readonly List<uint> diFatEntries;
        private uint numFatSectors;
        private uint numDiFatSectors;
        private uint diFatStartSector;

        public Fat(StructuredStorageContext context) : base(context)
        {
            this.diFatEntries = new List<uint>();
        }

        internal void ThrowInconsistencyException()
        {
            throw new Exception("Inconsistency found while writing DiFat.");
        }

        internal override void Write()
        {
            this.numDiFatSectors = 0;
            int y = base.Context.Header.SectorSize / 4;
            while (true)
            {
                uint numDiFatSectors = this.numDiFatSectors;
                this.numFatSectors = ((uint) base.DivCeiling((long) base.Entries.Count, y)) + this.numDiFatSectors;
                this.numDiFatSectors = (this.numFatSectors <= 0x6d) ? 0 : ((uint) base.DivCeiling((long) ((ulong) (this.numFatSectors - 0x6d)), y - 1));
                if (numDiFatSectors == this.numDiFatSectors)
                {
                    this.diFatStartSector = this.WriteDiFatEntriesToFat(this.numDiFatSectors);
                    this.writeDiFatSectorsToStream(base.CurrentEntry);
                    for (int i = 0; i < this.numFatSectors; i++)
                    {
                        base.Entries.Add(-3);
                    }
                    base.Context.TempOutputStream.WriteSectors(base.Context.InternalBitConverter.GetBytes(base.Entries).ToArray(), base.Context.Header.SectorSize, uint.MaxValue);
                    return;
                }
            }
        }

        private uint WriteDiFatEntriesToFat(uint sectorCount)
        {
            if (sectorCount == 0)
            {
                return 0xfffffffe;
            }
            uint currentEntry = base.CurrentEntry;
            for (int i = 0; i < sectorCount; i++)
            {
                uint num3 = base.CurrentEntry;
                base.CurrentEntry = num3 + 1;
                base.Entries.Add(-4);
            }
            return currentEntry;
        }

        private void writeDiFatSectorsToStream(uint fatStartSector)
        {
            for (uint i = 0; i < this.numFatSectors; i++)
            {
                this.diFatEntries.Add(fatStartSector + i);
            }
            for (int j = 0; j < 0x6d; j++)
            {
                if (j < this.diFatEntries.Count)
                {
                    base.Context.Header.WriteNextDiFatSector(this.diFatEntries[j]);
                }
                else
                {
                    base.Context.Header.WriteNextDiFatSector(uint.MaxValue);
                }
            }
            if (this.diFatEntries.Count > 0x6d)
            {
                List<uint> input = new List<uint>();
                for (int k = 0; k < (this.diFatEntries.Count - 0x6d); k++)
                {
                    input.Add(this.diFatEntries[k + 0x6d]);
                }
                uint item = this.diFatStartSector + 1;
                int num2 = base.Context.Header.SectorSize / 4;
                int num3 = num2;
                int num4 = input.Count / (num2 - 1);
                for (int m = 0; m < num4; m++)
                {
                    input.Insert(num3 - 1, item);
                    item++;
                    num3 += num2;
                }
                for (int n = input.Count; (n % num2) != 0; n++)
                {
                    input.Add(uint.MaxValue);
                }
                input.RemoveAt(input.Count - 1);
                input.Add(-2);
                List<byte> bytes = base.Context.InternalBitConverter.GetBytes(input);
                if ((bytes.Count % base.Context.Header.SectorSize) != 0)
                {
                    this.ThrowInconsistencyException();
                }
                base.Context.TempOutputStream.WriteSectors(bytes.ToArray(), base.Context.Header.SectorSize, uint.MaxValue);
            }
        }

        internal uint NumFatSectors =>
            this.numFatSectors;

        internal uint NumDiFatSectors =>
            this.numDiFatSectors;

        internal uint DiFatStartSector =>
            this.diFatStartSector;
    }
}

