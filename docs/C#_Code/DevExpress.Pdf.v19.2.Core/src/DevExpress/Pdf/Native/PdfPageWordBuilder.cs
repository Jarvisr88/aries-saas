namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class PdfPageWordBuilder
    {
        private List<PdfCharacter> characters = new List<PdfCharacter>();
        private double wordMinX;
        private double wordMaxX;
        private double wordMinY;
        private double wordMaxY;
        private double wordAngle;

        public void AppendChar(PdfRectangle cropBox, PdfPoint location, PdfCharacter character)
        {
            if (!HasFormatCharacters(character) && cropBox.Intersects(character.Rectangle.BoundingRectangle))
            {
                double angle = character.Rectangle.Angle;
                PdfPoint point = PdfTextUtils.RotatePoint(location, -angle);
                double y = PdfTextUtils.RotatePoint(character.Rectangle.BottomLeft, -angle).Y;
                double num4 = y + character.Rectangle.Height;
                if (this.characters.Count != 0)
                {
                    this.wordMinY = Math.Min(this.wordMinY, y);
                    this.wordMaxY = Math.Max(this.wordMaxY, num4);
                }
                else
                {
                    this.wordMinX = point.X;
                    this.wordMaxX = this.wordMinX;
                    this.wordMinY = y;
                    this.wordMaxY = num4;
                    this.wordAngle = angle;
                }
                double width = character.Rectangle.Width;
                this.wordMaxX = Math.Max(this.wordMaxX, point.X + width);
                this.wordMinX = Math.Min(point.X, this.wordMinX);
                this.characters.Add(character);
            }
        }

        public PdfWordPart CreatePart(bool wordEnded)
        {
            Func<PdfCharacter, double> keySelector = <>c.<>9__15_0;
            if (<>c.<>9__15_0 == null)
            {
                Func<PdfCharacter, double> local1 = <>c.<>9__15_0;
                keySelector = <>c.<>9__15_0 = c => PdfTextUtils.RotatePoint(c.Rectangle.TopLeft, -c.Rectangle.Angle).X;
            }
            return new PdfWordPart(new PdfOrientedRectangle(PdfTextUtils.RotatePoint(new PdfPoint(this.wordMinX, this.wordMaxY), this.wordAngle), this.wordMaxX - this.wordMinX, this.wordMaxY - this.wordMinY, this.wordAngle), this.characters.OrderBy<PdfCharacter, double>(keySelector).ToList<PdfCharacter>(), wordEnded);
        }

        private static bool HasFormatCharacters(PdfCharacter character)
        {
            string unicodeData = character.UnicodeData;
            if (string.IsNullOrWhiteSpace(unicodeData) || (unicodeData == "\x00a0"))
            {
                return true;
            }
            foreach (char ch in unicodeData)
            {
                if (char.GetUnicodeCategory(ch) == UnicodeCategory.Format)
                {
                    return true;
                }
            }
            return false;
        }

        public PdfPoint LeftBoundary =>
            new PdfPoint(this.wordMinX, this.wordMaxY);

        public PdfPoint RightBoundary =>
            new PdfPoint(this.wordMaxX, this.wordMaxY);

        public IEnumerable<PdfCharacter> Characters =>
            this.characters;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfPageWordBuilder.<>c <>9 = new PdfPageWordBuilder.<>c();
            public static Func<PdfCharacter, double> <>9__15_0;

            internal double <CreatePart>b__15_0(PdfCharacter c) => 
                PdfTextUtils.RotatePoint(c.Rectangle.TopLeft, -c.Rectangle.Angle).X;
        }
    }
}

