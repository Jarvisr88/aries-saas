namespace DevExpress.Utils.Serializing.Helpers
{
    using System;
    using System.IO;
    using System.IO.Compression;

    public class DeflateStreamsArchiveWriter : DeflateStreamsArchiveManagerBase
    {
        private int offsetTablePosition;
        private int streamIndex;

        public DeflateStreamsArchiveWriter(int streamCount, Stream baseStream) : base(baseStream)
        {
            base.fStreamCount = streamCount;
            this.WriteBytes(DeflateStreamsArchiveManagerBase.PrefixBytes);
            this.WriteBytes(DeflateStreamsArchiveManagerBase.VersionBytes);
            this.WriteInt32(streamCount);
            this.offsetTablePosition = this.CurrentOffset;
            base.offsets = new int[streamCount];
            this.SkipInt32(streamCount);
        }

        public void Close()
        {
            if (this.streamIndex < base.StreamCount)
            {
                ThrowInvalidOperationException();
            }
            base.baseStream.Seek((long) this.offsetTablePosition, SeekOrigin.Begin);
            foreach (int num2 in base.offsets)
            {
                this.WriteInt32(num2);
            }
        }

        public Stream GetNextRawStream()
        {
            base.CheckStreamIndex(this.streamIndex);
            base.offsets[this.streamIndex] = this.CurrentOffset;
            this.streamIndex++;
            return base.CreateRawStream();
        }

        public Stream GetNextStream()
        {
            base.CheckStreamIndex(this.streamIndex);
            base.offsets[this.streamIndex] = this.CurrentOffset;
            this.streamIndex++;
            return base.CreateDeflateStream(CompressionMode.Compress);
        }

        private void SkipInt32(int count)
        {
            base.baseStream.Seek((long) (count * 4), SeekOrigin.Current);
        }

        private void WriteBytes(byte[] bytes)
        {
            base.baseStream.Write(bytes, 0, bytes.Length);
        }

        private void WriteInt32(int number)
        {
            byte[] bytes = BitConverter.GetBytes(number);
            this.WriteBytes(bytes);
        }

        private int CurrentOffset =>
            (int) base.baseStream.Position;
    }
}

