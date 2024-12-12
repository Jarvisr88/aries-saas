namespace DevExpress.Pdf.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public class PdfJPXImageScanlineSource : IPdfImageScanlineSource, IDisposable
    {
        private readonly JPXImage image;
        private int offset;
        private readonly JPXTile[] tiles;
        private int startTileIndex = -1;

        public PdfJPXImageScanlineSource(JPXImage image, int componentsCount, bool hasAlpha)
        {
            this.image = image;
            this.<ComponentsCount>k__BackingField = componentsCount;
            this.<HasAlpha>k__BackingField = hasAlpha;
            this.tiles = new JPXTile[image.NumXTiles];
        }

        public void Dispose()
        {
            this.DisposeTiles();
        }

        private void DisposeTiles()
        {
            foreach (JPXTile tile in this.tiles)
            {
                if (tile != null)
                {
                    tile.Dispose();
                }
            }
        }

        public void FillNextScanline(byte[] scanline)
        {
            int length = scanline.Length;
            JPXSize size = this.image.Size;
            int num2 = this.offset / length;
            int num3 = this.image.NumXTiles * (num2 / size.TileHeight);
            if (num3 != this.startTileIndex)
            {
                this.startTileIndex = num3;
                this.FillNextTileRow();
            }
            int num5 = this.image.CodingStyleComponents.Length;
            int num6 = num5 * size.GridWidth;
            for (int i = 0; i < this.image.NumXTiles; i++)
            {
                JPXTile tile = this.tiles[i];
                tile.ColorTransformation.TransformLine(scanline, num5 * tile.X0, num2 % size.TileHeight);
            }
            this.offset += length;
        }

        private void FillNextTileRow()
        {
            this.DisposeTiles();
            PdfTask.Execute(i => this.tiles[i] = this.image.GetTile(i + this.startTileIndex), this.image.NumXTiles);
        }

        public bool HasAlpha { get; }

        public int ComponentsCount { get; }
    }
}

