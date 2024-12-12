namespace DevExpress.Xpf.Printing.PreviewControl.Native.Editing
{
    using DevExpress.Printing;
    using DevExpress.Utils;
    using DevExpress.Xpf.Printing.Native;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.Native.CharacterComb;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Animation;

    public class CharacterCombCaret : Control
    {
        private readonly GdiGraphicsWrapperBase measureGraphics = new GdiGraphicsWrapperBase(Graphics.FromImage(new Bitmap(50, 50)));
        private readonly System.Windows.Media.Pen pen;
        private ICharacterComb characterComb;
        private bool update;

        public CharacterCombCaret()
        {
            base.Background = System.Windows.Media.Brushes.Transparent;
            this.InitializePen(out this.pen);
            base.Loaded += new RoutedEventHandler(this.OnLoaded);
        }

        private Rect GetCaretRect()
        {
            if (this.characterComb.CharacterCombTextElements.Length == 0)
            {
                return new Rect();
            }
            CharacterCombTextElement currentElement = this.CurrentElement;
            string text = !string.IsNullOrEmpty(currentElement.TextElement) ? currentElement.TextElement : "W";
            SizeF size = this.measureGraphics.MeasureString(text, this.characterComb.CharacterCombInfo.Style.Font, GraphicsUnit.Document);
            size.Height = this.characterComb.CharacterCombInfo.Style.Font.GetHeight((float) 300f);
            RectangleF val = RectFBase.Center(new RectangleF(PointF.Empty, size), currentElement.Rect);
            if (this.PlaceAfterLetter)
            {
                val.Offset(val.Width, 0f);
            }
            val = MathMethods.Scale(GraphicsUnitConverter.DocToDip(val), (float) this.characterComb.Zoom);
            val.Width = Math.Max(1f, val.Width);
            return DrawingConverter.ToRect(val);
        }

        private void InitializePen(out System.Windows.Media.Pen pen)
        {
            pen = new System.Windows.Media.Pen();
            DoubleAnimation animation1 = new DoubleAnimation(0.5, 0.0, new Duration(TimeSpan.FromMilliseconds(400.0)));
            animation1.AutoReverse = true;
            animation1.RepeatBehavior = RepeatBehavior.Forever;
            DoubleAnimation timeline = animation1;
            Timeline.SetDesiredFrameRate(timeline, 10);
            AnimationClock clock = timeline.CreateClock();
            pen.ApplyAnimationClock(System.Windows.Media.Pen.ThicknessProperty, clock);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            base.Loaded -= new RoutedEventHandler(this.OnLoaded);
            base.InvalidateVisual();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            System.Windows.Media.Color color = new System.Windows.Media.Color {
                ScR = 1f - this.BackColor.ScR,
                ScG = 1f - this.BackColor.ScG,
                ScB = 1f - this.BackColor.ScB,
                ScA = 1f
            };
            this.pen.Brush = new SolidColorBrush(Colors.Black);
            Rect caretRect = this.GetCaretRect();
            drawingContext.DrawLine(this.pen, new System.Windows.Point(caretRect.Left, caretRect.Top), new System.Windows.Point(caretRect.Left, caretRect.Bottom));
        }

        public void SetCharacterComb(ICharacterComb characterComb)
        {
            Guard.ArgumentNotNull(characterComb, "characterComb");
            this.characterComb = characterComb;
        }

        public void Update()
        {
            if (base.IsLoaded)
            {
                CharacterCombTextElement empty = CharacterCombTextElement.Empty;
                CharacterCombTextElement element2 = CharacterCombTextElement.Empty;
                CharacterCombTextElement[,] characterCombTextElements = this.characterComb.CharacterCombTextElements;
                int upperBound = characterCombTextElements.GetUpperBound(0);
                int num2 = characterCombTextElements.GetUpperBound(1);
                int lowerBound = characterCombTextElements.GetLowerBound(0);
                while (true)
                {
                    if (lowerBound > upperBound)
                    {
                        break;
                    }
                    int num4 = characterCombTextElements.GetLowerBound(1);
                    while (true)
                    {
                        if (num4 <= num2)
                        {
                            CharacterCombTextElement element3 = characterCombTextElements[lowerBound, num4];
                            if (element3.TextIndex >= 0)
                            {
                                if (element3.TextIndex >= (this.characterComb.SelectionStart + this.characterComb.SelectionLength))
                                {
                                    element2 = element3;
                                    break;
                                }
                                else
                                {
                                    empty = element3;
                                }
                            }
                            num4++;
                            continue;
                        }
                        else
                        {
                            lowerBound++;
                        }
                        break;
                    }
                }
                if (element2.IsEmpty)
                {
                    if (empty.IsEmpty || ((this.characterComb.SelectionStart + this.characterComb.SelectionLength) < this.characterComb.Text.Length))
                    {
                        base.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        this.CurrentElement = empty;
                        this.PlaceAfterLetter = !string.IsNullOrEmpty(empty.TextElement);
                        base.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    if (element2.TextIndex > (this.characterComb.SelectionStart + this.characterComb.SelectionLength))
                    {
                        this.CurrentElement = empty;
                        this.PlaceAfterLetter = true;
                    }
                    else
                    {
                        this.CurrentElement = element2;
                        this.PlaceAfterLetter = false;
                    }
                    base.Visibility = Visibility.Visible;
                }
                base.InvalidateVisual();
            }
        }

        public System.Windows.Media.Color BackColor { get; set; }

        private CharacterCombTextElement CurrentElement { get; set; }

        private bool PlaceAfterLetter { get; set; }

        public bool DoUpdate
        {
            get => 
                this.update;
            set
            {
                this.update = value;
                base.InvalidateVisual();
            }
        }
    }
}

