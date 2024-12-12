namespace DevExpress.Utils.StructuredStorage.Internal.Writer
{
    using DevExpress.Utils.StructuredStorage.Internal;
    using System;

    [CLSCompliant(false)]
    public class StructuredStorageContext
    {
        private readonly DevExpress.Utils.StructuredStorage.Internal.Writer.Header header;
        private readonly DevExpress.Utils.StructuredStorage.Internal.Writer.Fat fat;
        private readonly DevExpress.Utils.StructuredStorage.Internal.Writer.MiniFat miniFat;
        private OutputHandler tempOutputStream = new OutputHandler(new ChunkedMemoryStream());
        private readonly OutputHandler directoryStream = new OutputHandler(new ChunkedMemoryStream());
        private readonly DevExpress.Utils.StructuredStorage.Internal.InternalBitConverter internalBitConverter;
        private readonly DevExpress.Utils.StructuredStorage.Internal.Writer.RootDirectoryEntry rootDirectoryEntry;
        private uint sidCounter;

        public StructuredStorageContext()
        {
            this.header = new DevExpress.Utils.StructuredStorage.Internal.Writer.Header(this);
            this.internalBitConverter = DevExpress.Utils.StructuredStorage.Internal.InternalBitConverter.Create(true);
            this.fat = new DevExpress.Utils.StructuredStorage.Internal.Writer.Fat(this);
            this.miniFat = new DevExpress.Utils.StructuredStorage.Internal.Writer.MiniFat(this);
            this.rootDirectoryEntry = new DevExpress.Utils.StructuredStorage.Internal.Writer.RootDirectoryEntry(this);
        }

        internal uint GetNewSid()
        {
            uint num = this.sidCounter + 1;
            this.sidCounter = num;
            return num;
        }

        internal DevExpress.Utils.StructuredStorage.Internal.Writer.Header Header =>
            this.header;

        internal DevExpress.Utils.StructuredStorage.Internal.Writer.Fat Fat =>
            this.fat;

        internal DevExpress.Utils.StructuredStorage.Internal.Writer.MiniFat MiniFat =>
            this.miniFat;

        internal OutputHandler TempOutputStream
        {
            get => 
                this.tempOutputStream;
            set => 
                this.tempOutputStream = value;
        }

        internal OutputHandler DirectoryStream =>
            this.directoryStream;

        internal DevExpress.Utils.StructuredStorage.Internal.InternalBitConverter InternalBitConverter =>
            this.internalBitConverter;

        public DevExpress.Utils.StructuredStorage.Internal.Writer.RootDirectoryEntry RootDirectoryEntry =>
            this.rootDirectoryEntry;
    }
}

