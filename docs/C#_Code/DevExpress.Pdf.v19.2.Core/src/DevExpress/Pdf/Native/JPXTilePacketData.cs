namespace DevExpress.Pdf.Native
{
    using System;
    using System.IO;

    public class JPXTilePacketData : IJPXTileDataAction
    {
        private readonly Stream stream;

        public JPXTilePacketData(Stream stream)
        {
            this.stream = stream;
        }

        public void Process(JPXTile tile)
        {
            while (this.stream.Position < this.stream.Length)
            {
                tile.AppendPacket(this.stream);
            }
            this.stream.Dispose();
        }
    }
}

