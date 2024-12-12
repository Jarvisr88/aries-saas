namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.NumberConverters;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Text;

    internal class BulletCalculator : IDrawingBulletVisitor
    {
        public BulletCalculator(DevExpress.Office.Drawing.ShapeStyle shapeStyle)
        {
            this.ShapeStyle = shapeStyle;
            this.ParagraphsCount = new List<int>();
            this.ParagraphsStartAt = new List<int>();
        }

        public void CalcBulletParams(IDrawingTextBullets bullets, int level)
        {
            this.Level = level;
            while (this.ParagraphsCount.Count <= level)
            {
                this.ParagraphsCount.Add(0);
                this.ParagraphsStartAt.Add(0);
            }
            this.HasBullet = false;
            this.Typeface = string.Empty;
            this.Text = string.Empty;
            this.Color = null;
            this.SizeType = DrawingTextSpacingValueType.Automatic;
            this.SizeValue = 0;
            bullets.Color.Visit(this);
            bullets.Size.Visit(this);
            bullets.Typeface.Visit(this);
            bullets.Common.Visit(this);
            if (!this.HasBullet)
            {
                this.ParagraphsCount.Clear();
                this.ParagraphsStartAt.Clear();
            }
        }

        void IDrawingBulletVisitor.Visit(DrawingBlip bullet)
        {
            this.HasBullet = true;
        }

        void IDrawingBulletVisitor.Visit(DrawingBulletAutoNumbered bullet)
        {
            this.HasBullet = true;
            int realParagraphNumber = this.GetRealParagraphNumber(bullet.StartAt);
            int level = this.Level;
            List<int> paragraphsCount = this.ParagraphsCount;
            paragraphsCount[level] += 1;
            switch (bullet.SchemeType)
            {
                case DrawingTextAutoNumberSchemeType.AlphaLcParenBoth:
                    this.Text = $"({RealNumToChars(realParagraphNumber, 'a')})";
                    return;

                case DrawingTextAutoNumberSchemeType.AlphaUcParenBoth:
                    this.Text = $"({RealNumToChars(realParagraphNumber, 'A')})";
                    return;

                case DrawingTextAutoNumberSchemeType.AlphaLcParenR:
                    this.Text = $"{RealNumToChars(realParagraphNumber, 'a')})";
                    return;

                case DrawingTextAutoNumberSchemeType.AlphaUcParenR:
                    this.Text = $"{RealNumToChars(realParagraphNumber, 'A')})";
                    return;

                case DrawingTextAutoNumberSchemeType.AlphaLcPeriod:
                    this.Text = $"{RealNumToChars(realParagraphNumber, 'a')}.";
                    return;

                case DrawingTextAutoNumberSchemeType.AlphaUcPeriod:
                    this.Text = $"{RealNumToChars(realParagraphNumber, 'A')}.";
                    return;

                case DrawingTextAutoNumberSchemeType.ArabicParenBoth:
                    this.Text = $"({realParagraphNumber.ToString()})";
                    return;

                case DrawingTextAutoNumberSchemeType.ArabicParenR:
                    this.Text = $"{realParagraphNumber.ToString()})";
                    return;

                case DrawingTextAutoNumberSchemeType.ArabicPeriod:
                    this.Text = $"{realParagraphNumber.ToString()}.";
                    return;

                case DrawingTextAutoNumberSchemeType.ArabicPlain:
                    this.Text = $"{realParagraphNumber.ToString()}";
                    return;

                case DrawingTextAutoNumberSchemeType.RomanLcParenBoth:
                    this.Text = $"({new LowerRomanNumberConverterClassic().ConvertNumber((long) realParagraphNumber)})";
                    return;

                case DrawingTextAutoNumberSchemeType.RomanUcParenBoth:
                    this.Text = $"({new UpperRomanNumberConverterClassic().ConvertNumber((long) realParagraphNumber)})";
                    return;

                case DrawingTextAutoNumberSchemeType.RomanLcParenR:
                    this.Text = $"{new LowerRomanNumberConverterClassic().ConvertNumber((long) realParagraphNumber)})";
                    return;

                case DrawingTextAutoNumberSchemeType.RomanUcParenR:
                    this.Text = $"{new UpperRomanNumberConverterClassic().ConvertNumber((long) realParagraphNumber)})";
                    return;

                case DrawingTextAutoNumberSchemeType.RomanLcPeriod:
                    this.Text = $"{new LowerRomanNumberConverterClassic().ConvertNumber((long) realParagraphNumber)}.";
                    return;

                case DrawingTextAutoNumberSchemeType.RomanUcPeriod:
                    this.Text = $"{new UpperRomanNumberConverterClassic().ConvertNumber((long) realParagraphNumber)}.";
                    return;

                case DrawingTextAutoNumberSchemeType.CircleNumDbPlain:
                    this.Text = (realParagraphNumber <= 20) ? new string((char) ((0x2460 + realParagraphNumber) - 1), 1) : realParagraphNumber.ToString();
                    return;

                case DrawingTextAutoNumberSchemeType.CircleNumWdBlackPlain:
                    this.Typeface = "Wingdings";
                    this.Text = new string((char) (0x8b + (((realParagraphNumber % 10) != 0) ? (realParagraphNumber % 10) : 10)), 1);
                    return;

                case DrawingTextAutoNumberSchemeType.CircleNumWdWhitePlain:
                    this.Typeface = "Wingdings";
                    this.Text = new string((char) (0x80 + (((realParagraphNumber % 10) != 0) ? (realParagraphNumber % 10) : 10)), 1);
                    return;
            }
            this.Text = $"{realParagraphNumber.ToString()}.";
        }

        void IDrawingBulletVisitor.Visit(DrawingBulletCharacter bullet)
        {
            this.HasBullet = true;
            this.Text = bullet.Character;
        }

        void IDrawingBulletVisitor.Visit(DrawingBulletSizePercentage bullet)
        {
            this.HasBullet = true;
            this.SizeType = DrawingTextSpacingValueType.Percent;
            this.SizeValue = bullet.Value;
        }

        void IDrawingBulletVisitor.Visit(DrawingBulletSizePoints bullet)
        {
            this.HasBullet = true;
            this.SizeType = DrawingTextSpacingValueType.Points;
            this.SizeValue = bullet.Value;
        }

        void IDrawingBulletVisitor.Visit(DrawingColor bullet)
        {
            this.HasBullet = true;
            this.Color = bullet;
        }

        void IDrawingBulletVisitor.Visit(DrawingTextFont bullet)
        {
            this.HasBullet = true;
            this.Typeface = bullet.Typeface;
            if (string.IsNullOrEmpty(this.Typeface) || this.Typeface.StartsWith("+mj"))
            {
                this.Typeface = bullet.DocumentModel.OfficeTheme.FontScheme.MajorFont.Latin.Typeface;
            }
            else if (this.Typeface.StartsWith("+mn"))
            {
                this.Typeface = bullet.DocumentModel.OfficeTheme.FontScheme.MinorFont.Latin.Typeface;
            }
        }

        void IDrawingBulletVisitor.VisitBulletColorFollowText()
        {
            this.HasBullet = true;
        }

        void IDrawingBulletVisitor.VisitBulletSizeFollowText()
        {
            this.HasBullet = true;
        }

        void IDrawingBulletVisitor.VisitBulletTypefaceFollowText()
        {
            this.HasBullet = true;
        }

        void IDrawingBulletVisitor.VisitNoBullets()
        {
            this.HasBullet = false;
        }

        private int GetRealParagraphNumber(int startAt)
        {
            if (this.ParagraphsStartAt[this.Level] != startAt)
            {
                this.ParagraphsStartAt[this.Level] = startAt;
                this.ParagraphsCount[this.Level] = 0;
            }
            return (this.ParagraphsStartAt[this.Level] + this.ParagraphsCount[this.Level]);
        }

        private static string RealNumToChars(int realNum, char startChar)
        {
            StringBuilder builder = new StringBuilder();
            char ch = (char) (startChar + ((realNum - 1) % 0x1a));
            for (int i = 0; i <= ((realNum - 1) / 0x1a); i++)
            {
                builder.Append(ch);
            }
            return builder.ToString();
        }

        public string Typeface { get; set; }

        public string Text { get; set; }

        public DrawingColor Color { get; set; }

        public int SizeValue { get; set; }

        public DrawingTextSpacingValueType SizeType { get; set; }

        private DevExpress.Office.Drawing.ShapeStyle ShapeStyle { get; set; }

        private List<int> ParagraphsCount { get; set; }

        private List<int> ParagraphsStartAt { get; set; }

        private int Level { get; set; }

        internal bool HasBullet { get; set; }
    }
}

