namespace DevExpress.Utils.StructuredStorage.Reader
{
    using DevExpress.Utils;
    using DevExpress.Utils.StructuredStorage.Internal;
    using DevExpress.Utils.StructuredStorage.Internal.Reader;
    using System;
    using System.Collections.Generic;
    using System.IO;

    [CLSCompliant(false)]
    public class StructuredStorageReader
    {
        private readonly InputHandler fileHandler;
        private readonly Header header;
        private readonly Fat fat;
        private readonly MiniFat miniFat;
        private readonly DirectoryTree directory;

        public StructuredStorageReader(Stream stream) : this(stream, false)
        {
        }

        public StructuredStorageReader(Stream stream, bool keepOpen)
        {
            Guard.ArgumentNotNull(stream, "stream");
            this.fileHandler = keepOpen ? new KeepOpenInputHandler(stream) : new InputHandler(stream);
            this.header = new Header(this.fileHandler);
            this.fat = new Fat(this.header, this.fileHandler);
            this.directory = new DirectoryTree(this.fat, this.header, this.fileHandler);
            this.miniFat = new MiniFat(this.fat, this.header, this.fileHandler, this.directory.GetMiniStreamStart(), this.directory.GetSizeOfMiniStream());
        }

        public void Close()
        {
            this.fileHandler.CloseStream();
        }

        public void Dispose()
        {
            this.Close();
        }

        public DirectoryEntry GetEntry(string path)
        {
            DirectoryEntry directoryEntry = this.directory.GetDirectoryEntry(path);
            if (directoryEntry == null)
            {
                throw new Exception("DirectoryEntry with name '" + path + "' not found.");
            }
            return directoryEntry;
        }

        public VirtualStream GetStream(string path)
        {
            DirectoryEntry directoryEntry = this.directory.GetDirectoryEntry(path);
            if (directoryEntry == null)
            {
                this.directory.ThrowStreamNotFoundException(path);
            }
            if (directoryEntry.Type != DirectoryEntryType.Stream)
            {
                throw new Exception("The directory entry is not of type STGTY_STREAM.");
            }
            if (directoryEntry.StreamLength > 0x7fffffffffffffffL)
            {
                this.header.ThrowUnsupportedSizeException(directoryEntry.StreamLength.ToString());
            }
            return ((directoryEntry.StreamLength >= this.header.MiniSectorCutoff) ? new VirtualStream(this.fat, directoryEntry.StartSector, (long) directoryEntry.StreamLength, path) : new VirtualStream(this.miniFat, directoryEntry.StartSector, (long) directoryEntry.StreamLength, path));
        }

        public ICollection<string> FullNameOfAllEntries =>
            this.directory.GetPathsOfAllEntries();

        public ICollection<string> FullNameOfAllStreamEntries =>
            this.directory.GetPathsOfAllStreamEntries();

        public ICollection<DirectoryEntry> AllEntries =>
            this.directory.GetAllEntries();

        public ICollection<DirectoryEntry> AllStreamEntries =>
            this.directory.GetAllStreamEntries();

        public DirectoryEntry RootDirectoryEntry =>
            this.directory.GetDirectoryEntry((uint) 0);
    }
}

