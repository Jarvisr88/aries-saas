namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlPicture : XlDrawingObject, IXlPicture, IDisposable
    {
        private IXlExport exporter;
        private readonly XlPictureHyperlink hyperlinkClick = new XlPictureHyperlink();

        public XlPicture(IXlExport exporter)
        {
            this.exporter = exporter;
        }

        public void Dispose()
        {
            if (this.exporter != null)
            {
                this.exporter.EndPicture();
                this.exporter = null;
            }
        }

        public void FitToCell(XlCellPosition position, int cellWidth, int cellHeight, bool center)
        {
            if (this.Image == null)
            {
                throw new InvalidOperationException("Image is not specified!");
            }
            int columnOffsetInPixels = 0;
            int rowOffsetInPixels = 0;
            int num3 = cellWidth;
            int num4 = (num3 * this.Image.Height) / this.Image.Width;
            if (num4 > cellHeight)
            {
                num4 = cellHeight;
                num3 = (num4 * this.Image.Width) / this.Image.Height;
            }
            if (center)
            {
                columnOffsetInPixels = (cellWidth - num3) / 2;
                rowOffsetInPixels = (cellHeight - num4) / 2;
            }
            base.AnchorType = XlAnchorType.TwoCell;
            base.AnchorBehavior = XlAnchorType.TwoCell;
            base.TopLeft = new XlAnchorPoint(position.Column, position.Row, columnOffsetInPixels, rowOffsetInPixels, cellWidth, cellHeight);
            base.BottomRight = new XlAnchorPoint(position.Column, position.Row, columnOffsetInPixels + num3, rowOffsetInPixels + num4, cellWidth, cellHeight);
        }

        internal byte[] GetImageBytes(ImageFormat format)
        {
            if (this.Image == null)
            {
                return null;
            }
            Metafile image = this.Image as Metafile;
            if ((image != null) && (ReferenceEquals(format, ImageFormat.Emf) || ReferenceEquals(format, ImageFormat.Wmf)))
            {
                this.Format = image.RawFormat.Guid.Equals(ImageFormat.Emf.Guid) ? ImageFormat.Emf : ImageFormat.Wmf;
                return PSConvert.ImageToArray(this.Image);
            }
            using (MemoryStream stream = new MemoryStream())
            {
                if (ReferenceEquals(format, ImageFormat.Emf) || ReferenceEquals(format, ImageFormat.Wmf))
                {
                    using (Metafile metafile2 = MetafileCreator.CreateInstance(stream, this.Image.Width, this.Image.Height, MetafileFrameUnit.Pixel, EmfType.EmfPlusOnly))
                    {
                        using (Graphics graphics = Graphics.FromImage(metafile2))
                        {
                            graphics.DrawImage(this.Image, new Rectangle(0, 0, this.Image.Width, this.Image.Height));
                        }
                    }
                    this.Format = ImageFormat.Emf;
                }
                else if (image == null)
                {
                    this.Image.Save(stream, format);
                }
                else
                {
                    float num = 200f;
                    Size size = new Size((int) ((this.Image.Width * num) / this.Image.HorizontalResolution), (int) ((this.Image.Height * num) / this.Image.VerticalResolution));
                    int width = (int) (((double) (size.Width * this.DpiX)) / 96.0);
                    int height = (int) (((double) (size.Height * this.DpiY)) / 96.0);
                    using (Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb))
                    {
                        using (Graphics graphics2 = Graphics.FromImage(bitmap))
                        {
                            graphics2.DrawImage(image, 0, 0, width, height);
                            bitmap.Save(stream, format);
                        }
                    }
                }
                return stream.ToArray();
            }
        }

        public void StretchToCell(XlCellPosition position)
        {
            base.AnchorType = XlAnchorType.TwoCell;
            base.AnchorBehavior = XlAnchorType.TwoCell;
            base.TopLeft = new XlAnchorPoint(position.Column, position.Row);
            base.BottomRight = new XlAnchorPoint(position.Column + 1, position.Row + 1);
        }

        public System.Drawing.Image Image { get; set; }

        public ImageFormat Format { get; set; }

        public XlPictureHyperlink HyperlinkClick =>
            this.hyperlinkClick;

        public XlSourceRectangle SourceRectangle { get; set; }

        internal override XlDrawingObjectType DrawingObjectType =>
            XlDrawingObjectType.Picture;

        private float DpiX =>
            XlGraphicUnitConverter.GetDpi(this.exporter);

        private float DpiY =>
            XlGraphicUnitConverter.GetDpi(this.exporter);
    }
}

