namespace DevExpress.Utils.StructuredStorage.Internal.Writer
{
    using DevExpress.Utils;
    using DevExpress.Utils.StructuredStorage.Internal;
    using DevExpress.Utils.StructuredStorage.Writer;
    using System;
    using System.IO;

    [CLSCompliant(false)]
    public class StreamDirectoryEntry : BaseDirectoryEntry
    {
        private readonly Stream stream;

        public StreamDirectoryEntry(string name, Stream stream, StructuredStorageContext context) : base(name, context)
        {
            Guard.ArgumentNotNull(stream, "stream");
            this.stream = stream;
            base.Type = DirectoryEntryType.Stream;
        }

        protected internal override void WriteReferencedStream()
        {
            VirtualStream stream = (this.stream.Length >= base.Context.Header.MiniSectorCutoff) ? new VirtualStream(this.stream, base.Context.Fat, base.Context.Header.SectorSize, base.Context.TempOutputStream) : new VirtualStream(this.stream, base.Context.MiniFat, base.Context.Header.MiniSectorSize, base.Context.RootDirectoryEntry.MiniStream);
            stream.Write();
            base.StartSector = stream.StartSector;
            base.StreamLength = stream.Length;
        }
    }
}

