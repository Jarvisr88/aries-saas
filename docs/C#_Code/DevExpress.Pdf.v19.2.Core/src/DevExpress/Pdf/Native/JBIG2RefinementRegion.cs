namespace DevExpress.Pdf.Native
{
    using System;

    public class JBIG2RefinementRegion : JBIG2SegmentData
    {
        private readonly JBIG2Image referenceGlyph;
        private readonly int template;
        private readonly int dx;
        private readonly int dy;
        private readonly int[] at;
        private readonly JBIG2Decoder decoder;

        public JBIG2RefinementRegion(JBIG2StreamHelper streamHelper, JBIG2SegmentHeader header, JBIG2Image image) : base(streamHelper, header, image)
        {
        }

        internal JBIG2RefinementRegion(JBIG2Image referenceGlyph, int dx, int dy, int template, int[] at, JBIG2Decoder decoder, JBIG2Image image) : base(image)
        {
            this.referenceGlyph = referenceGlyph;
            this.dx = dx;
            this.dy = dy;
            this.template = template;
            this.at = at;
            this.decoder = decoder;
        }

        public override void Process()
        {
            base.Process();
            int width = base.Image.Width;
            int height = base.Image.Height;
            int context = 0;
            Func<int, int, int> func = null;
            int template = this.template;
            if (template == 0)
            {
                func = new Func<int, int, int>(this.Template0Context);
            }
            else if (template != 1)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            else
            {
                func = new Func<int, int, int>(this.Template1Context);
            }
            int num5 = 0;
            while (num5 < height)
            {
                int num6 = 0;
                while (true)
                {
                    if (num6 >= width)
                    {
                        num5++;
                        break;
                    }
                    context = func(num6, num5);
                    base.Image.SetPixel(num6, num5, this.decoder.DecodeGR(context));
                    num6++;
                }
            }
        }

        private int Template0Context(int x, int y) => 
            ((((((((((((0 | base.Image.GetPixel(x - 1, y)) | (base.Image.GetPixel(x + 1, y - 1) << 1)) | (base.Image.GetPixel(x, y - 1) << 2)) | (base.Image.GetPixel(x + this.at[0], y + this.at[1]) << 3)) | (this.referenceGlyph.GetPixel((x - this.dx) + 1, (y - this.dy) + 1) << 4)) | (this.referenceGlyph.GetPixel(x - this.dx, (y - this.dy) + 1) << 5)) | (this.referenceGlyph.GetPixel((x - this.dx) - 1, (y - this.dy) + 1) << 6)) | (this.referenceGlyph.GetPixel((x - this.dx) + 1, y - this.dy) << 7)) | (this.referenceGlyph.GetPixel(x - this.dx, y - this.dy) << 8)) | (this.referenceGlyph.GetPixel((x - this.dx) - 1, y - this.dy) << 9)) | (this.referenceGlyph.GetPixel((x - this.dx) + 1, (y - this.dy) - 1) << 10)) | (this.referenceGlyph.GetPixel(x - this.dx, (y - this.dy) - 1) << 11)) | (this.referenceGlyph.GetPixel((x - this.dx) + this.at[2], (y - this.dy) + this.at[3]) << 12);

        private int Template1Context(int x, int y) => 
            (((((((((0 | base.Image.GetPixel(x - 1, y)) | (base.Image.GetPixel(x + 1, y - 1) << 1)) | (base.Image.GetPixel(x, y - 1) << 2)) | (base.Image.GetPixel(x - 1, y - 1) << 3)) | (this.referenceGlyph.GetPixel((x - this.dx) + 1, (y - this.dy) + 1) << 4)) | (this.referenceGlyph.GetPixel(x - this.dx, (y - this.dy) + 1) << 5)) | (this.referenceGlyph.GetPixel((x - this.dx) + 1, y - this.dy) << 6)) | (this.referenceGlyph.GetPixel(x - this.dx, y - this.dy) << 7)) | (this.referenceGlyph.GetPixel((x - this.dx) - 1, y - this.dy) << 8)) | (this.referenceGlyph.GetPixel(x - this.dx, (y - this.dy) - 1) << 9);
    }
}

