namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class PdfTextParser
    {
        private readonly PdfRectangle cropBox;
        private readonly List<PdfTextBlock> pageBlocks = new List<PdfTextBlock>();

        public PdfTextParser(PdfRectangle pageCropBox)
        {
            this.cropBox = new PdfRectangle(0.0, 0.0, pageCropBox.Width, pageCropBox.Height);
        }

        public void AddBlock(PdfStringData data, PdfGraphicsState graphicsState)
        {
            byte[][] charCodes = data.CharCodes;
            double[] advances = data.Advances;
            PdfFont font = graphicsState.TextState.Font;
            if ((charCodes != null) && ((charCodes.Length != 0) && ((advances != null) && (advances.Length != 0))))
            {
                this.pageBlocks.Add(new PdfTextBlock(data, graphicsState));
            }
        }

        public IList<PdfTextLine> Parse()
        {
            if ((this.pageBlocks == null) || (this.pageBlocks.Count < 1))
            {
                return new List<PdfTextLine>();
            }
            PdfPageTextLineBuilder builder = new PdfPageTextLineBuilder();
            List<PdfTextLine> list = new List<PdfTextLine>();
            foreach (IReadOnlyList<PdfTextBlock> list2 in new PdfPageTextLineIterator(this.pageBlocks))
            {
                PdfTextLine item = builder.CreateLine(list2, this.cropBox);
                if (item != null)
                {
                    list.Add(item);
                }
            }
            return list;
        }
    }
}

