namespace DevExpress.Utils.Zip
{
    using DevExpress.Utils.Zip.Internal;
    using System;
    using System.IO;

    public class StreamProxy
    {
        private readonly Stream baseStream;
        private readonly long startPositionInBaseStream;
        private readonly long length;
        private bool isPackedStream;

        public StreamProxy(Stream baseStream, long startPositionInBaseStream, long length, bool isPackedStream)
        {
            this.isPackedStream = isPackedStream;
            this.baseStream = baseStream;
            this.startPositionInBaseStream = startPositionInBaseStream;
            this.length = length;
        }

        public static StreamProxy Create(Stream stream) => 
            new StreamProxy(stream, stream.Position, stream.Length, false);

        public static StreamProxy Create(Stream stream, long position) => 
            new StreamProxy(stream, position, stream.Length, false);

        public Stream CreateRawStream() => 
            new FixedOffsetSequentialReadOnlyStream(this.BaseStream, this.StartPositionInBaseStream, this.Length, this.IsPackedStream);

        public Stream BaseStream =>
            this.baseStream;

        public long StartPositionInBaseStream =>
            this.startPositionInBaseStream;

        public long Length =>
            this.length;

        public bool IsPackedStream =>
            this.isPackedStream;
    }
}

