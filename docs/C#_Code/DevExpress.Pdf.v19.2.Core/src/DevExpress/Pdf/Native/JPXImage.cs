namespace DevExpress.Pdf.Native
{
    using System;
    using System.IO;

    public class JPXImage
    {
        private JPXSize size;
        private JPXCodingStyleComponent[] codingStyleComponents;
        private JPXQuantizationComponentParameters[] quantizationParameters;
        private int numXTiles;
        private int numYTiles;
        private JPXCodingStyleDefault codingStyleDefault;
        private JPXTileData[] tiles;

        public static JPXImageDecodeResult Decode(byte[] data)
        {
            JPXImage image = DecodeImage(data);
            int gridWidth = image.Size.GridWidth;
            int length = image.CodingStyleComponents.Length;
            byte[] result = new byte[(length * gridWidth) * image.Size.GridHeight];
            for (int i = 0; i < image.TileCount; i++)
            {
                using (JPXTile tile = image.GetTile(i))
                {
                    JPXColorTransformation.Create(tile).Transform(result, length * (tile.X0 + (tile.Y0 * gridWidth)), gridWidth);
                }
            }
            return new JPXImageDecodeResult(result, image.Size.Components.Length);
        }

        public static JPXImage DecodeImage(byte[] data)
        {
            using (MemoryStream stream = new MemoryStream(data))
            {
                JPXImage image = new JPXImage();
                PdfBigEndianStreamReader reader = new PdfBigEndianStreamReader(stream);
                bool flag = reader.ReadByte() == 0xff;
                stream.Position = 0L;
                if (flag)
                {
                    JPXContiguousCodeStreamBox box1 = new JPXContiguousCodeStreamBox(reader, (int) reader.Length, image);
                }
                else
                {
                    while (!reader.Finish)
                    {
                        JPXBox.Parse(reader, image);
                    }
                }
                return image;
            }
        }

        public JPXTile GetTile(int index)
        {
            JPXTile tile = new JPXTile(this, index);
            this.Tiles[index].Process(tile);
            return tile;
        }

        public JPXSize Size
        {
            get => 
                this.size;
            set
            {
                this.size = value;
                int length = this.size.Components.Length;
                this.codingStyleComponents = new JPXCodingStyleComponent[length];
                this.quantizationParameters = new JPXQuantizationComponentParameters[length];
                this.numXTiles = (int) Math.Ceiling((double) (((float) (this.size.GridWidth - Math.Max(this.size.TileHorizontalOffset, this.size.GridHorizontalOffset))) / ((float) this.size.TileWidth)));
                this.numYTiles = (int) Math.Ceiling((double) (((float) (this.size.GridHeight - Math.Max(this.size.TileVerticalOffset, this.size.GridVerticalOffset))) / ((float) this.size.TileHeight)));
            }
        }

        public JPXCodingStyleComponent[] CodingStyleComponents =>
            this.codingStyleComponents;

        public JPXQuantizationComponentParameters[] QuantizationParameters =>
            this.quantizationParameters;

        public int NumXTiles =>
            this.numXTiles;

        public int NumYTiles =>
            this.numYTiles;

        public JPXCodingStyleDefault CodingStyleDefault
        {
            get => 
                this.codingStyleDefault;
            set => 
                this.codingStyleDefault = value;
        }

        public int TileCount =>
            this.numXTiles * this.numYTiles;

        public JPXTileData[] Tiles
        {
            get
            {
                if (this.tiles == null)
                {
                    int num = this.numXTiles * this.numYTiles;
                    this.tiles = new JPXTileData[num];
                    for (int i = 0; i < num; i++)
                    {
                        this.tiles[i] = new JPXTileData();
                    }
                }
                return this.tiles;
            }
        }
    }
}

