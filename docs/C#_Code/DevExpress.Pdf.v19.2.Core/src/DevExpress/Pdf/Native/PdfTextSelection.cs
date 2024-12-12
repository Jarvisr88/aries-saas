namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PdfTextSelection : PdfSelection
    {
        private readonly PdfPageDataCache pageDataCache;
        private readonly IList<PdfPageTextRange> textRange;
        private string text;
        private IList<PdfHighlight> highlights;

        public PdfTextSelection(PdfPageDataCache pageDataCache, IList<PdfPageTextRange> textRange)
        {
            this.pageDataCache = pageDataCache;
            this.textRange = textRange;
        }

        private static void AppendWordPart(PdfBidiStringBuilder builder, PdfWordPart part)
        {
            AppendWordPart(builder, part, 0, part.Characters.Count);
        }

        private static void AppendWordPart(PdfBidiStringBuilder builder, PdfWordPart part, int startOffset)
        {
            AppendWordPart(builder, part, startOffset, part.Characters.Count);
        }

        private static void AppendWordPart(PdfBidiStringBuilder builder, PdfWordPart part, int startOffset, int endOffset)
        {
            for (int i = startOffset; i < endOffset; i++)
            {
                builder.Append(part.Characters[i].UnicodeData);
            }
        }

        public static bool AreEqual(PdfTextSelection selection1, PdfTextSelection selection2)
        {
            IList<PdfPageTextRange> textRange = selection1.textRange;
            IList<PdfPageTextRange> list2 = selection2.textRange;
            int count = textRange.Count;
            if (count != list2.Count)
            {
                return false;
            }
            for (int i = 0; i < count; i++)
            {
                if (!PdfPageTextRange.AreEqual(textRange[i], list2[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public Dictionary<int, PdfOrientedRectangleList> CreateSelectionMapping()
        {
            if (this.Highlights.Count == 0)
            {
                return null;
            }
            Dictionary<int, PdfOrientedRectangleList> dictionary = new Dictionary<int, PdfOrientedRectangleList>();
            foreach (PdfHighlight highlight in this.Highlights)
            {
                if (highlight.MarkupRectangles.Count > 0)
                {
                    PdfOrientedRectangleList list;
                    int pageIndex = highlight.PageIndex;
                    if (!dictionary.TryGetValue(pageIndex, out list))
                    {
                        list = new PdfOrientedRectangleList();
                        dictionary.Add(pageIndex, list);
                    }
                    list.AddRange(highlight.MarkupRectangles);
                }
            }
            return ((dictionary.Count == 0) ? null : dictionary);
        }

        public string GetPageText(int pageIndex) => 
            this.GetText(index => index == pageIndex);

        private string GetText(Func<int, bool> pageSelector)
        {
            PdfBidiStringBuilder builder = new PdfBidiStringBuilder();
            int count = this.textRange.Count;
            for (int i = 0; i < count; i++)
            {
                PdfPageTextRange range = this.textRange[i];
                if (pageSelector(range.PageIndex))
                {
                    int startWordNumber = range.StartWordNumber;
                    int startOffset = range.StartOffset;
                    int endOffset = range.EndOffset;
                    IList<PdfTextLine> pageLines = this.pageDataCache.GetPageLines(range.PageIndex);
                    int endWordNumber = range.EndWordNumber;
                    if (pageLines.Count > 0)
                    {
                        PdfTextLine line = pageLines[pageLines.Count - 1];
                        if (range.WholePage)
                        {
                            startWordNumber = pageLines[0].StartWordNumber;
                            startOffset = 0;
                            endWordNumber = line.EndWordNumber;
                            endOffset = line.WordParts[line.WordParts.Count - 1].Length;
                        }
                        endWordNumber = (endWordNumber == 0) ? line.EndWordNumber : endWordNumber;
                        startWordNumber = (startWordNumber == 0) ? 1 : startWordNumber;
                    }
                    foreach (PdfTextLine line2 in pageLines)
                    {
                        if ((line2.EndWordNumber >= startWordNumber) && (line2.StartWordNumber <= endWordNumber))
                        {
                            bool flag = (line2.StartWordNumber <= endWordNumber) && (line2.EndWordNumber >= endWordNumber);
                            bool flag2 = (line2.StartWordNumber <= startWordNumber) && (line2.EndWordNumber >= startWordNumber);
                            foreach (PdfWordPart part2 in line2.WordParts)
                            {
                                int wordNumber = part2.WordNumber;
                                if ((wordNumber >= startWordNumber) && (wordNumber <= endWordNumber))
                                {
                                    int num8 = (startWordNumber == wordNumber) ? startOffset : 0;
                                    AppendWordPart(builder, part2, num8, ((endWordNumber != wordNumber) || (endOffset == -1)) ? part2.Length : endOffset);
                                    if (part2.WordEnded && ((endWordNumber != wordNumber) && ((wordNumber != line2.EndWordNumber) && !builder.Empty)))
                                    {
                                        builder.Append(" ");
                                    }
                                }
                            }
                            PdfWordPart part = line2.WordParts[line2.WordParts.Count - 1];
                            if (!builder.Empty && (!builder.EndsWithNewLine && ((part.WordNumber >= startWordNumber) && ((part.WordNumber < endWordNumber) || ((part.WordNumber == endWordNumber) && ((endOffset == -1) || (endOffset == part.Length)))))))
                            {
                                builder.AppendLine();
                            }
                        }
                        if (line2.StartWordNumber > endWordNumber)
                        {
                            break;
                        }
                    }
                    if ((i != (count - 1)) && ((range.PageIndex == this.textRange[i + 1].PageIndex) && (!builder.Empty && (!builder.EndsWithNewLine && (range.WholePage || (count > 1))))))
                    {
                        builder.AppendLine();
                    }
                }
            }
            return builder.EndCurrentLineAndGetString();
        }

        public IList<PdfPageTextRange> TextRange =>
            this.textRange;

        public string Text
        {
            get
            {
                if (this.text == null)
                {
                    Func<int, bool> pageSelector = <>c.<>9__11_0;
                    if (<>c.<>9__11_0 == null)
                    {
                        Func<int, bool> local1 = <>c.<>9__11_0;
                        pageSelector = <>c.<>9__11_0 = index => true;
                    }
                    this.text = this.GetText(pageSelector);
                }
                return this.text;
            }
        }

        public override PdfDocumentContentType ContentType =>
            PdfDocumentContentType.Text;

        public override IList<PdfHighlight> Highlights
        {
            get
            {
                if (this.highlights == null)
                {
                    this.highlights = new List<PdfHighlight>();
                    foreach (PdfPageTextRange range in this.textRange)
                    {
                        this.highlights.Add(new PdfTextHighlight(this.pageDataCache, range));
                    }
                }
                return this.highlights;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfTextSelection.<>c <>9 = new PdfTextSelection.<>c();
            public static Func<int, bool> <>9__11_0;

            internal bool <get_Text>b__11_0(int index) => 
                true;
        }
    }
}

