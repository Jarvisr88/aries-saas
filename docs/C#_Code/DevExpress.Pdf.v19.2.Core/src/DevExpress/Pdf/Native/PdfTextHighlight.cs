namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class PdfTextHighlight : PdfHighlight
    {
        private readonly PdfPageDataCache pageDataCache;
        private readonly PdfPageTextRange wordRange;
        private IList<PdfOrientedRectangle> rectangles;

        public PdfTextHighlight(PdfPageDataCache pageDataCache, PdfPageTextRange wordRange) : base(wordRange.PageIndex)
        {
            this.pageDataCache = pageDataCache;
            this.wordRange = wordRange;
        }

        private IList<PdfOrientedRectangle> CreateRectangles()
        {
            IList<PdfOrientedRectangle> list = new List<PdfOrientedRectangle>();
            int startWordNumber = this.wordRange.StartWordNumber;
            bool flag = startWordNumber == 0;
            int endWordNumber = this.wordRange.EndWordNumber;
            bool flag2 = endWordNumber == 0;
            List<PdfTextLine> list2 = new List<PdfTextLine>(this.pageDataCache.GetPageLines(this.wordRange.PageIndex));
            if ((flag & flag2) || this.wordRange.WholePage)
            {
                foreach (PdfTextLine line in list2)
                {
                    list.Add(line.Rectangle);
                }
            }
            else
            {
                int startOffset = this.wordRange.StartOffset;
                int endOffset = this.wordRange.EndOffset;
                foreach (PdfTextLine line2 in list2)
                {
                    IList<PdfWordPart> wordParts = line2.WordParts;
                    int count = wordParts.Count;
                    if (count > 0)
                    {
                        int num7;
                        int num8;
                        int wordNumber = wordParts[0].WordNumber;
                        bool flag3 = line2.IsPositionInLine(this.wordRange.EndWordNumber, endOffset);
                        if (flag3)
                        {
                            num7 = endWordNumber - wordNumber;
                            num8 = endOffset;
                        }
                        else
                        {
                            num7 = 0;
                            num8 = 0;
                        }
                        IList<PdfOrientedRectangle> list4 = null;
                        if (line2.IsPositionInLine(this.wordRange.StartWordNumber, startOffset))
                        {
                            int sWordIndex = startWordNumber - wordNumber;
                            int sOffset = startOffset;
                            list4 = flag3 ? line2.GetHighlightRectangles(sWordIndex, sOffset, num7, num8, true) : line2.GetHighlightRectangles(sWordIndex, sOffset, true);
                        }
                        else if (flag3)
                        {
                            list4 = line2.GetHighlightRectangles(0, 0, num7, num8, true);
                        }
                        else
                        {
                            PdfWordPart part = wordParts[count - 1];
                            int num11 = part.WordNumber;
                            if ((flag || (startWordNumber <= wordNumber)) && (flag2 || ((num11 - 1) <= endWordNumber)))
                            {
                                list4 = line2.GetHighlightRectangles(0, 0, num11 - wordNumber, part.Characters.Count, true);
                            }
                        }
                        if (list4 != null)
                        {
                            foreach (PdfOrientedRectangle rectangle in list4)
                            {
                                if (rectangle.Width > 0.0)
                                {
                                    list.Add(rectangle);
                                }
                            }
                            if (flag3)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            return list;
        }

        public override IList<PdfOrientedRectangle> Rectangles
        {
            get
            {
                this.rectangles ??= this.CreateRectangles();
                return this.rectangles;
            }
        }

        public override IList<PdfOrientedRectangle> MarkupRectangles
        {
            get
            {
                List<PdfOrientedRectangle> list = new List<PdfOrientedRectangle>();
                PdfRectangle cropBox = this.pageDataCache.DocumentPages[base.PageIndex].CropBox;
                double left = cropBox.Left;
                double bottom = cropBox.Bottom;
                foreach (PdfOrientedRectangle rectangle2 in this.Rectangles)
                {
                    list.Add(new PdfOrientedRectangle(new PdfPoint(rectangle2.Left + left, rectangle2.Top + bottom), rectangle2.Width, rectangle2.Height, rectangle2.Angle));
                }
                return list;
            }
        }
    }
}

