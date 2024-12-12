namespace DevExpress.Utils.StructuredStorage.Internal.Writer
{
    using DevExpress.Office.Utils;
    using DevExpress.Utils.StructuredStorage.Internal;
    using DevExpress.Utils.StructuredStorage.Writer;
    using System;

    [CLSCompliant(false)]
    public class RootDirectoryEntry : StorageDirectoryEntry
    {
        private readonly OutputHandler miniStream;

        internal RootDirectoryEntry(StructuredStorageContext context) : base("Root Entry", context)
        {
            this.miniStream = new OutputHandler(new ChunkedMemoryStream());
            base.Type = DirectoryEntryType.Root;
            base.Sid = 0;
        }

        protected internal override void WriteReferencedStream()
        {
            VirtualStream stream = new VirtualStream(this.miniStream.BaseStream, base.Context.Fat, base.Context.Header.SectorSize, base.Context.TempOutputStream);
            stream.Write();
            base.StartSector = stream.StartSector;
            base.StreamLength = stream.Length;
        }

        internal OutputHandler MiniStream =>
            this.miniStream;
    }
}

