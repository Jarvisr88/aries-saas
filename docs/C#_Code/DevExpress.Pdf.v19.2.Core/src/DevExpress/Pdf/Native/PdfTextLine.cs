namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class PdfTextLine
    {
        private readonly IList<PdfWordPart> wordParts;
        private readonly int startWordNumber;
        private readonly int endWordNumber;
        private PdfOrientedRectangle rectangle;

        public PdfTextLine(IList<PdfWordPart> parts)
        {
            this.wordParts = parts;
            if (this.wordParts.Count > 0)
            {
                this.startWordNumber = this.wordParts[0].WordNumber;
                this.endWordNumber = this.wordParts[this.wordParts.Count - 1].WordNumber;
            }
        }

        public IList<PdfOrientedRectangle> GetHighlightRectangles(int sWordIndex, int sOffset, bool splitRectangles) => 
            this.GetHighlightRectangles(sWordIndex, sOffset, this.wordParts.Count - 1, -1, splitRectangles);

        public IList<PdfOrientedRectangle> GetHighlightRectangles(int sWordIndex, int sOffset, int eWordIndex, int eOffset, bool splitRectangles)
        {
            IList<PdfCharacter> characters = this.wordParts[(sWordIndex == -1) ? 0 : sWordIndex].Characters;
            IList<PdfOrientedRectangle> list2 = new List<PdfOrientedRectangle>();
            int num = sOffset;
            if (sOffset > characters.Count)
            {
                return null;
            }
            bool flag = sOffset == characters.Count;
            if (flag)
            {
                num--;
            }
            PdfOrientedRectangle rectangle = characters[num].Rectangle;
            double angle = rectangle.Angle;
            PdfPoint point = PdfTextUtils.RotatePoint(rectangle.TopLeft, -angle);
            double x = flag ? (point.X + rectangle.Width) : point.X;
            double y = point.Y;
            double num5 = x;
            double num6 = y - rectangle.Height;
            if (sWordIndex == -1)
            {
                sWordIndex++;
            }
            for (int i = sWordIndex; i <= eWordIndex; i++)
            {
                IList<PdfCharacter> list3 = this.wordParts[i].Characters;
                int num8 = (i == sWordIndex) ? sOffset : 0;
                int num9 = ((i != eWordIndex) || (eOffset == -1)) ? list3.Count : eOffset;
                if (splitRectangles && ((num8 == 0) || (num8 < num9)))
                {
                    PdfOrientedRectangle rectangle2 = list3[num8].Rectangle;
                    PdfPoint point2 = PdfTextUtils.RotatePoint(rectangle2.TopLeft, -angle);
                    if (point2.X > (num5 + (2.0 * rectangle2.Width)))
                    {
                        list2.Add(new PdfOrientedRectangle(PdfTextUtils.RotatePoint(new PdfPoint(x, y), angle), num5 - x, y - num6, angle));
                        x = point2.X;
                        y = point2.Y;
                        num5 = x;
                        num6 = y - rectangle2.Height;
                    }
                }
                if (num9 == 0)
                {
                    num5 = PdfMathUtils.Max(num5, PdfTextUtils.RotatePoint(this.wordParts[i].Characters[0].Rectangle.TopLeft, -angle).X);
                }
                else
                {
                    for (int j = num8; j < num9; j++)
                    {
                        PdfOrientedRectangle rectangle3 = list3[j].Rectangle;
                        PdfPoint point4 = PdfTextUtils.RotatePoint(rectangle3.TopLeft, -angle);
                        x = PdfMathUtils.Min(point4.X, x);
                        y = PdfMathUtils.Max(point4.Y, y);
                        num5 = PdfMathUtils.Max(num5, point4.X + rectangle3.Width);
                        num6 = PdfMathUtils.Min(num6, point4.Y - rectangle3.Height);
                    }
                }
            }
            list2.Add(new PdfOrientedRectangle(PdfTextUtils.RotatePoint(new PdfPoint(x, y), angle), num5 - x, y - num6, angle));
            return list2;
        }

        public PdfTextPosition GetTextPosition(int pageIndex, PdfPoint point)
        {
            PdfTextPosition position;
            if (this.wordParts.Count < 1)
            {
                return null;
            }
            PdfWordPart part = null;
            double angle = this.wordParts[0].Rectangle.Angle;
            using (IEnumerator<PdfWordPart> enumerator = this.wordParts.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        PdfWordPart current = enumerator.Current;
                        PdfOrientedRectangle rectangle = current.Rectangle;
                        double x = PdfTextUtils.RotatePoint(rectangle.TopLeft, -angle).X;
                        PdfPoint point2 = PdfTextUtils.RotatePoint(point, -angle);
                        double num3 = point2.X;
                        if ((x + rectangle.Width) < num3)
                        {
                            part = current;
                            continue;
                        }
                        PdfOrientedRectangle rectangle2 = part?.Rectangle;
                        if (((part == null) || current.Rectangle.PointIsInRect(point, 0.0, 0.0)) || (Math.Abs((double) ((PdfTextUtils.RotatePoint(rectangle2.TopLeft, -angle).X + rectangle2.Width) - num3)) >= Math.Abs((double) (x - num3))))
                        {
                            part = current;
                        }
                        IList<PdfCharacter> characters = part.Characters;
                        int count = characters.Count;
                        int offset = 0;
                        while (true)
                        {
                            if (offset >= count)
                            {
                                break;
                            }
                            PdfOrientedRectangle rectangle3 = characters[offset].Rectangle;
                            point2 = PdfTextUtils.RotatePoint(point, -angle);
                            if ((PdfTextUtils.RotatePoint(rectangle3.TopLeft, -angle).X + (rectangle3.Width / 2.0)) < point2.X)
                            {
                                offset++;
                                continue;
                            }
                            return new PdfTextPosition(pageIndex, part.WordNumber, offset);
                        }
                        continue;
                    }
                    else
                    {
                        return new PdfTextPosition(pageIndex, part.WordNumber, part.Characters.Count);
                    }
                    break;
                }
            }
            return position;
        }

        public PdfPageTextRange GetTextRange(int pageIndex, PdfRectangle area)
        {
            PdfPageTextPosition startTextPosition = null;
            PdfPageTextPosition endTextPosition = null;
            foreach (PdfWordPart part in this.wordParts)
            {
                IList<PdfCharacter> characters = part.Characters;
                int count = characters.Count;
                for (int i = 0; i < count; i++)
                {
                    if (area.Intersects(characters[i].Rectangle.BoundingRectangle))
                    {
                        startTextPosition = new PdfPageTextPosition(part.WordNumber, i);
                        endTextPosition = new PdfPageTextPosition(part.WordNumber, i + 1);
                    }
                }
            }
            return (((startTextPosition == null) || (endTextPosition == null)) ? null : new PdfPageTextRange(pageIndex, startTextPosition, endTextPosition));
        }

        public bool IsPositionInLine(int wordNumber, int offset)
        {
            int count = this.wordParts.Count;
            if ((this.wordParts.Count != 0) && ((wordNumber >= this.startWordNumber) && (wordNumber <= this.endWordNumber)))
            {
                for (int i = 0; i < count; i++)
                {
                    int num3 = this.wordParts[i].Characters.Count;
                    if (wordNumber == this.wordParts[i].WordNumber)
                    {
                        return ((i != 0) ? (offset <= num3) : ((0 <= offset) && (offset <= num3)));
                    }
                }
            }
            return false;
        }

        public IList<PdfWordPart> WordParts =>
            this.wordParts;

        public PdfOrientedRectangle Rectangle
        {
            get
            {
                this.rectangle ??= this.GetHighlightRectangles(-1, 0, this.wordParts.Count - 1, -1, false)[0];
                return this.rectangle;
            }
        }

        public int StartWordNumber =>
            this.startWordNumber;

        public int EndWordNumber =>
            this.endWordNumber;
    }
}

