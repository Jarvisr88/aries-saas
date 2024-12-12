namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfWord
    {
        private readonly IList<PdfWordPart> parts;
        private readonly Lazy<IList<PdfWordSegment>> segments;
        private string text;
        private IList<PdfOrientedRectangle> rectangles;
        private List<PdfCharacter> characters;

        internal PdfWord(IList<PdfWordPart> parts)
        {
            this.parts = parts;
            this.segments = new Lazy<IList<PdfWordSegment>>(delegate {
                if (parts == null)
                {
                    return null;
                }
                IList<PdfWordSegment> list = new List<PdfWordSegment>();
                foreach (PdfWordPart part in parts)
                {
                    list.Add(new PdfWordSegment(part));
                }
                return list;
            });
        }

        public IList<PdfWordSegment> Segments =>
            this.segments.Value;

        public string Text
        {
            get
            {
                if (string.IsNullOrEmpty(this.text))
                {
                    this.text = string.Empty;
                    int num = this.parts.Count - 1;
                    int num2 = 0;
                    while (true)
                    {
                        if (num2 >= num)
                        {
                            this.text = this.text + this.parts[num].Text;
                            break;
                        }
                        string text = this.parts[num2].Text;
                        this.text = this.text + text.Remove(text.Length - 1);
                        num2++;
                    }
                }
                return this.text;
            }
        }

        public IList<PdfOrientedRectangle> Rectangles
        {
            get
            {
                if (this.rectangles == null)
                {
                    this.rectangles = new List<PdfOrientedRectangle>();
                    foreach (PdfWordPart part in this.parts)
                    {
                        this.rectangles.Add(part.Rectangle);
                    }
                }
                return this.rectangles;
            }
        }

        public IList<PdfCharacter> Characters
        {
            get
            {
                if (this.characters == null)
                {
                    this.characters = new List<PdfCharacter>();
                    foreach (PdfWordPart part in this.parts)
                    {
                        this.characters.AddRange(part.Characters);
                    }
                }
                return this.characters;
            }
        }

        internal IList<PdfWordPart> Parts =>
            this.parts;
    }
}

