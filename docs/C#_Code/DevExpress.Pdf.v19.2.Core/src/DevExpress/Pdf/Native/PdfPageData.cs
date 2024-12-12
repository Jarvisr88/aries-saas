namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PdfPageData
    {
        private readonly IList<PdfTextLine> textData;
        private readonly IList<PdfPageImageData> imageData;
        private List<PdfWord> words;
        public IList<PdfTextLine> TextData =>
            this.textData;
        public IList<PdfPageImageData> ImageData =>
            this.imageData;
        public IList<PdfWord> Words
        {
            get
            {
                if (this.words == null)
                {
                    this.words = new List<PdfWord>();
                    List<PdfWordPart> parts = null;
                    foreach (PdfTextLine line in this.textData)
                    {
                        int num = line.WordParts.Count - 1;
                        for (int i = 0; i < line.WordParts.Count; i++)
                        {
                            if ((i == num) && line.WordParts[num].IsWrapped)
                            {
                                parts = new List<PdfWordPart> {
                                    line.WordParts[num]
                                };
                            }
                            else if (parts == null)
                            {
                                PdfWordPart[] partArray2 = new PdfWordPart[] { line.WordParts[i] };
                                this.words.Add(new PdfWord(partArray2));
                            }
                            else if (!PdfTextUtils.IsSeparator(line.WordParts[i].Characters[0].UnicodeData))
                            {
                                parts.Add(line.WordParts[i]);
                                this.words.Add(new PdfWord(parts));
                                parts = null;
                            }
                            else
                            {
                                this.words.Add(new PdfWord(parts));
                                PdfWordPart[] partArray1 = new PdfWordPart[] { line.WordParts[i] };
                                this.words.Add(new PdfWord(partArray1));
                                parts = null;
                            }
                        }
                    }
                    if (parts != null)
                    {
                        this.words.Add(new PdfWord(parts));
                    }
                }
                return this.words;
            }
        }
        public PdfPageData(IList<PdfTextLine> textData, IList<PdfPageImageData> imageData)
        {
            this.textData = textData;
            this.imageData = imageData;
            this.words = null;
        }
    }
}

