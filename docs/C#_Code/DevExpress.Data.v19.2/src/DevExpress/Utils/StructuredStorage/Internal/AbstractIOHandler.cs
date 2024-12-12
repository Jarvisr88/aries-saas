namespace DevExpress.Utils.StructuredStorage.Internal
{
    using DevExpress.Utils;
    using System;
    using System.IO;

    [CLSCompliant(false)]
    public abstract class AbstractIOHandler
    {
        private readonly System.IO.Stream stream;
        private AbstractHeader header;
        private InternalBitConverter bitConverter;

        protected AbstractIOHandler(System.IO.Stream stream)
        {
            Guard.ArgumentNotNull(stream, "stream");
            this.stream = stream;
        }

        public virtual void CloseStream()
        {
            if (this.stream != null)
            {
                this.stream.Dispose();
            }
        }

        public void InitBitConverter(bool isLittleEndian)
        {
            this.bitConverter = InternalBitConverter.Create(isLittleEndian);
        }

        public void SetHeaderReference(AbstractHeader header)
        {
            this.header = header;
        }

        public abstract ulong IOStreamSize { get; }

        public AbstractHeader Header =>
            this.header;

        public InternalBitConverter BitConverter =>
            this.bitConverter;

        public System.IO.Stream Stream =>
            this.stream;
    }
}

