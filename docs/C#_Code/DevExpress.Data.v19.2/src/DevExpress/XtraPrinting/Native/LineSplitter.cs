namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Diagnostics;
    using System.Drawing;

    public class LineSplitter
    {
        private Font font;
        private string text;
        private StringFormat sf;
        private GraphicsUnit pageUnit;

        public LineSplitter(string text, Font font, StringFormat sf)
        {
            this.text = text;
            this.font = font;
            this.sf = sf;
        }

        private RectangleF GetActualTextBounds(Graphics gr, RectangleF bounds)
        {
            int firstValidSymbol = this.GetFirstValidSymbol();
            if (firstValidSymbol == -1)
            {
                return bounds;
            }
            int charactersFitted = 0;
            int linesFilled = 0;
            gr.MeasureString(this.text, this.font, bounds.Size, this.sf, out charactersFitted, out linesFilled);
            if (charactersFitted <= 0)
            {
                return bounds;
            }
            int lastValidSymbol = this.GetLastValidSymbol(charactersFitted - 1);
            if (lastValidSymbol == -1)
            {
                lastValidSymbol = firstValidSymbol;
            }
            RectangleF[] efArray = this.GetCharacterRects(gr, bounds, firstValidSymbol, lastValidSymbol);
            RectangleF ef = efArray[0];
            RectangleF ef2 = efArray[1];
            if (efArray[0].IsEmpty)
            {
                return bounds;
            }
            while (ef2.IsEmpty && (lastValidSymbol > firstValidSymbol))
            {
                lastValidSymbol--;
                ef2 = this.GetCharacterRect(gr, bounds, lastValidSymbol);
            }
            return (!ef2.IsEmpty ? RectangleF.FromLTRB(bounds.Left, ef.Top, bounds.Right, ef2.Bottom) : RectangleF.FromLTRB(bounds.Left, ef.Top, bounds.Right, ef.Bottom));
        }

        internal RectangleF GetCharacterRect(Graphics gr, RectangleF bounds, int characterIndex)
        {
            CharacterRange[] ranges = new CharacterRange[] { new CharacterRange(characterIndex, 1) };
            return this.MasureRanges(gr, bounds, ranges)[0];
        }

        private RectangleF[] GetCharacterRects(Graphics gr, RectangleF bounds, int characterIndex1, int characterIndex2)
        {
            CharacterRange[] ranges = new CharacterRange[] { new CharacterRange(characterIndex1, 1), new CharacterRange(characterIndex2, 1) };
            return this.MasureRanges(gr, bounds, ranges);
        }

        internal int GetFirstValidSymbol()
        {
            for (int i = 0; i < this.text.Length; i++)
            {
                if (!char.IsControl(this.text[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        private int GetLastValidSymbol(int initialPosition)
        {
            for (int i = initialPosition; i >= 0; i--)
            {
                if (!char.IsControl(this.text[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        protected virtual float GetLineHeight() => 
            this.font.GetHeight(GraphicsDpi.UnitToDpi(this.pageUnit));

        private RectangleF[] MasureRanges(Graphics gr, RectangleF bounds, CharacterRange[] ranges)
        {
            RectangleF[] efArray2;
            RectangleF[] efArray = new RectangleF[ranges.Length];
            this.sf.SetMeasurableCharacterRanges(ranges);
            try
            {
                Region[] regionArray = null;
                try
                {
                    regionArray = gr.MeasureCharacterRanges(this.text, this.font, bounds, this.sf);
                    for (int i = 0; i < efArray.Length; i++)
                    {
                        efArray[i] = regionArray[i].GetBounds(gr);
                    }
                }
                catch
                {
                }
                finally
                {
                    if (regionArray != null)
                    {
                        foreach (Region region in regionArray)
                        {
                            region.Dispose();
                        }
                    }
                }
                efArray2 = efArray;
            }
            finally
            {
                this.sf.SetMeasurableCharacterRanges(new CharacterRange[0]);
            }
            return efArray2;
        }

        [Conditional("DEBUGTEST")]
        private void ShowTestCase(RectangleF rect, float position, float defaultTop, RectangleF textBounds, float result)
        {
        }

        public float SplitRectangle(RectangleF rect, float position, float defaultTop, GraphicsUnit pageUnit)
        {
            if (string.IsNullOrEmpty(this.text))
            {
                return position;
            }
            using (Graphics graphics = GraphicsHelper.CreateGraphicsFromHiResImage())
            {
                graphics.PageUnit = pageUnit;
                RectangleF actualTextBounds = this.GetActualTextBounds(graphics, rect);
                this.pageUnit = pageUnit;
                return this.SplitRectangleCore(rect, position, defaultTop, actualTextBounds);
            }
        }

        protected float SplitRectangleCore(RectangleF rect, float position, float defaultTop, RectangleF textBounds)
        {
            if (textBounds.IsEmpty)
            {
                return position;
            }
            float lineHeight = this.GetLineHeight();
            if ((position >= rect.Top) && (position < textBounds.Top))
            {
                return (((textBounds.Top - rect.Top) > lineHeight) ? position : defaultTop);
            }
            if ((position >= textBounds.Top) && (position <= textBounds.Bottom))
            {
                float num3 = position - textBounds.Top;
                if ((num3 <= lineHeight) && ((textBounds.Top - rect.Top) <= (lineHeight / 2f)))
                {
                    return defaultTop;
                }
                num3 = ((float) Math.Floor((double) (num3 / lineHeight))) * lineHeight;
                return (textBounds.Top + num3);
            }
            if ((position <= textBounds.Bottom) || ((rect.Bottom - textBounds.Bottom) >= (lineHeight * 0.3f)))
            {
                return position;
            }
            float num5 = ((float) Math.Floor((double) ((position - textBounds.Top) / lineHeight))) * lineHeight;
            float num6 = textBounds.Top + num5;
            if (num6 > textBounds.Bottom)
            {
                num6 -= lineHeight;
            }
            return ((num6 >= (lineHeight * 0.95f)) ? ((((num6 - textBounds.Top) >= 0.01) || ((textBounds.Top - rect.Top) > (lineHeight / 2f))) ? num6 : defaultTop) : position);
        }
    }
}

