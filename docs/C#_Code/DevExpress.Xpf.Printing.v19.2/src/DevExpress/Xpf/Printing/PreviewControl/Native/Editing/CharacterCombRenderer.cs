namespace DevExpress.Xpf.Printing.PreviewControl.Native.Editing
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Printing;
    using DevExpress.Utils;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Printing.Native;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native.CharacterComb;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;

    internal class CharacterCombRenderer : INativeImageRenderer
    {
        private readonly CharacterCombEdit characterComb;
        private Color? background;
        private Locker locker = new Locker();
        private INativeImageRendererCallback callback;

        public CharacterCombRenderer(CharacterCombEdit characterComb)
        {
            this.characterComb = characterComb;
        }

        void INativeImageRenderer.RegisterCallback(INativeImageRendererCallback callback)
        {
            this.callback = callback;
        }

        void INativeImageRenderer.ReleaseCallback()
        {
            this.callback = null;
        }

        private Color FindBackground(VisualBrick brick, int pageIndex)
        {
            Page item = brick.PrintingSystem.Pages[pageIndex];
            Func<IEnumerable<BrickBase>, BrickBase> func = delegate (IEnumerable<BrickBase> bricks) {
                Func<BrickBase, bool> <>9__1;
                Func<BrickBase, bool> predicate = <>9__1;
                if (<>9__1 == null)
                {
                    Func<BrickBase, bool> local1 = <>9__1;
                    predicate = <>9__1 = delegate (BrickBase x) {
                        Func<BrickBase, IEnumerable<BrickBase>> getItems = <>c.<>9__6_2;
                        if (<>c.<>9__6_2 == null)
                        {
                            Func<BrickBase, IEnumerable<BrickBase>> local1 = <>c.<>9__6_2;
                            getItems = <>c.<>9__6_2 = b => b.InnerBrickList.OfType<BrickBase>();
                        }
                        return x.Yield<BrickBase>().Flatten<BrickBase>(getItems).Contains<BrickBase>(brick);
                    };
                }
                return bricks.Where<BrickBase>(predicate).Single<BrickBase>();
            };
            List<BrickBase> source = new List<BrickBase>();
            BrickBase base2 = item;
            source.Add(item);
            while (true)
            {
                base2 = func(base2.InnerBrickList.OfType<BrickBase>());
                source.Add(base2);
                if (ReferenceEquals(base2, brick))
                {
                    Color backColor;
                    source.Remove(brick);
                    source.Reverse();
                    using (IEnumerator<VisualBrick> enumerator = source.OfType<VisualBrick>().GetEnumerator())
                    {
                        while (true)
                        {
                            if (enumerator.MoveNext())
                            {
                                VisualBrick current = enumerator.Current;
                                if (DXColor.IsTransparentOrEmpty(current.Style.BackColor))
                                {
                                    continue;
                                }
                                backColor = current.Style.BackColor;
                            }
                            else
                            {
                                break;
                            }
                            break;
                        }
                    }
                    return backColor;
                }
            }
        }

        public IEnumerable<CharacterCombTextElement> GetSelectedItems()
        {
            List<CharacterCombTextElement> list = new List<CharacterCombTextElement>();
            CharacterCombTextElement[,] characterCombTextElements = this.characterComb.CharacterCombTextElements;
            int upperBound = characterCombTextElements.GetUpperBound(0);
            int num2 = characterCombTextElements.GetUpperBound(1);
            int lowerBound = characterCombTextElements.GetLowerBound(0);
            while (lowerBound <= upperBound)
            {
                int num4 = characterCombTextElements.GetLowerBound(1);
                while (true)
                {
                    if (num4 > num2)
                    {
                        lowerBound++;
                        break;
                    }
                    CharacterCombTextElement item = characterCombTextElements[lowerBound, num4];
                    if ((item.TextIndex >= this.characterComb.SelectionStart) && (item.TextIndex < (this.characterComb.SelectionStart + this.characterComb.SelectionLength)))
                    {
                        list.Add(item);
                    }
                    num4++;
                }
            }
            return list;
        }

        public void Render(Graphics graphics, Rect invalidateRect, System.Windows.Size renderSize)
        {
            if ((this.characterComb.IsLoaded && (this.characterComb.CharacterCombTextElements.Length != 0)) && !this.locker.IsLocked)
            {
                this.locker.Lock();
                double scaleX = this.characterComb.GetScaleX();
                SizeF size = this.characterComb.Brick.Size;
                IEnumerable<CharacterCombTextElement> selectedItems = this.GetSelectedItems();
                if (this.background == null)
                {
                    this.background = new Color?(this.FindBackground(this.characterComb.Brick, this.characterComb.PageIndex));
                }
                graphics.FillRectangle(new SolidBrush(this.background.Value), new RectangleF((float) invalidateRect.X, (float) invalidateRect.Y, (float) invalidateRect.Width, (float) invalidateRect.Height));
                graphics.ResetTransform();
                graphics.SmoothingMode = (this.characterComb.Zoom < 1.0) ? SmoothingMode.HighQuality : SmoothingMode.Default;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.ScaleTransform((float) this.characterComb.Zoom, (float) this.characterComb.Zoom);
                GdiGraphicsWrapperBase base1 = new GdiGraphicsWrapperBase(graphics);
                base1.PageUnit = GraphicsUnit.Document;
                GdiGraphicsWrapperBase gr = base1;
                string displayText = this.characterComb.DisplayText;
                new CustomCharacterCombPainter(this.characterComb.CharacterCombInfo, selectedItems).DrawContent(gr, graphics.DpiX, new RectangleF(new PointF(0f, 0f), size), displayText);
                this.locker.Unlock();
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CharacterCombRenderer.<>c <>9 = new CharacterCombRenderer.<>c();
            public static Func<BrickBase, IEnumerable<BrickBase>> <>9__6_2;

            internal IEnumerable<BrickBase> <FindBackground>b__6_2(BrickBase b) => 
                b.InnerBrickList.OfType<BrickBase>();
        }

        private class CustomCharacterCombPainter : CharacterCombPainter
        {
            private IEnumerable<CharacterCombTextElement> selectedElements;

            public CustomCharacterCombPainter(CharacterCombInfo ccInfo, IEnumerable<CharacterCombTextElement> selectedElements) : base(ccInfo)
            {
                this.selectedElements = selectedElements;
            }

            protected override void DrawText(IGraphicsBase gr, RectangleF bounds, StringFormat sf, string text)
            {
                Color color = this.selectedElements.Contains<CharacterCombTextElement>(base.CurrentElement) ? System.Drawing.SystemColors.HighlightText : base.Style.ForeColor;
                gr.DrawString(text, base.Style.Font, gr.GetBrush(color), bounds, sf);
            }

            protected override void FillRect(IGraphicsBase gr, RectangleF fillRect)
            {
                Color color = (!this.selectedElements.Contains<CharacterCombTextElement>(base.CurrentElement) || string.IsNullOrEmpty(base.CurrentElement.TextElement)) ? base.Style.BackColor : System.Drawing.SystemColors.Highlight;
                gr.FillRectangle(gr.GetBrush(color), fillRect);
            }
        }
    }
}

