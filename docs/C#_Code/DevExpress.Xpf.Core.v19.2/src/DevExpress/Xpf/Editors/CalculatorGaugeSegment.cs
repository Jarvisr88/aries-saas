namespace DevExpress.Xpf.Editors
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class CalculatorGaugeSegment : Control
    {
        public static readonly DependencyProperty CharProperty;
        public static readonly DependencyProperty ShowDotProperty;

        static CalculatorGaugeSegment()
        {
            CharProperty = DependencyProperty.Register("Char", typeof(char), typeof(CalculatorGaugeSegment), new PropertyMetadata((d, e) => ((CalculatorGaugeSegment) d).PropertyChangedChar((char) e.OldValue)));
            ShowDotProperty = DependencyProperty.Register("ShowDot", typeof(bool), typeof(CalculatorGaugeSegment), new PropertyMetadata((d, e) => ((CalculatorGaugeSegment) d).PropertyChangedShowDot()));
        }

        public CalculatorGaugeSegment()
        {
            this.SetDefaultStyleKey(typeof(CalculatorGaugeSegment));
        }

        protected virtual ElementType GetVisibleElements()
        {
            ElementType segmentMiddle = 0;
            char ch = this.Char;
            if (ch > 'E')
            {
                if (ch == 'o')
                {
                    segmentMiddle = ElementType.SegmentMiddle | ElementType.SegmentBottom | ElementType.SegmentRightBottom | ElementType.SegmentLeftBottom;
                }
                else if (ch == 'r')
                {
                    segmentMiddle = ElementType.SegmentMiddle | ElementType.SegmentLeftBottom;
                }
            }
            else
            {
                switch (ch)
                {
                    case '-':
                        segmentMiddle = ElementType.SegmentMiddle;
                        break;

                    case '.':
                    case '/':
                        break;

                    case '0':
                        segmentMiddle = ElementType.SegmentBottom | ElementType.SegmentRightBottom | ElementType.SegmentRightTop | ElementType.SegmentTop | ElementType.SegmentLeftTop | ElementType.SegmentLeftBottom;
                        break;

                    case '1':
                        segmentMiddle = ElementType.SegmentRightBottom | ElementType.SegmentRightTop;
                        break;

                    case '2':
                        segmentMiddle = ElementType.SegmentMiddle | ElementType.SegmentBottom | ElementType.SegmentRightTop | ElementType.SegmentTop | ElementType.SegmentLeftBottom;
                        break;

                    case '3':
                        segmentMiddle = ElementType.SegmentMiddle | ElementType.SegmentBottom | ElementType.SegmentRightBottom | ElementType.SegmentRightTop | ElementType.SegmentTop;
                        break;

                    case '4':
                        segmentMiddle = ElementType.SegmentMiddle | ElementType.SegmentRightBottom | ElementType.SegmentRightTop | ElementType.SegmentLeftTop;
                        break;

                    case '5':
                        segmentMiddle = ElementType.SegmentMiddle | ElementType.SegmentBottom | ElementType.SegmentRightBottom | ElementType.SegmentTop | ElementType.SegmentLeftTop;
                        break;

                    case '6':
                        segmentMiddle = ElementType.SegmentMiddle | ElementType.SegmentBottom | ElementType.SegmentRightBottom | ElementType.SegmentTop | ElementType.SegmentLeftTop | ElementType.SegmentLeftBottom;
                        break;

                    case '7':
                        segmentMiddle = ElementType.SegmentRightBottom | ElementType.SegmentRightTop | ElementType.SegmentTop;
                        break;

                    case '8':
                        segmentMiddle = ElementType.SegmentMiddle | ElementType.SegmentBottom | ElementType.SegmentRightBottom | ElementType.SegmentRightTop | ElementType.SegmentTop | ElementType.SegmentLeftTop | ElementType.SegmentLeftBottom;
                        break;

                    case '9':
                        segmentMiddle = ElementType.SegmentMiddle | ElementType.SegmentBottom | ElementType.SegmentRightBottom | ElementType.SegmentRightTop | ElementType.SegmentTop | ElementType.SegmentLeftTop;
                        break;

                    default:
                        if (ch == 'E')
                        {
                            segmentMiddle = ElementType.SegmentMiddle | ElementType.SegmentBottom | ElementType.SegmentTop | ElementType.SegmentLeftTop | ElementType.SegmentLeftBottom;
                        }
                        break;
                }
            }
            if (this.ShowDot)
            {
                segmentMiddle |= ElementType.Dot;
            }
            return segmentMiddle;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.UpdateElementStyles();
            this.UpdateElements();
        }

        protected virtual void PropertyChangedChar(char oldValue)
        {
            this.UpdateElements();
        }

        protected virtual void PropertyChangedShowDot()
        {
            this.UpdateElements();
        }

        protected virtual void UpdateElements()
        {
            if ((this.ElementStyleOff != null) && (this.ElementStyleOn != null))
            {
                ElementType visibleElements = this.GetVisibleElements();
                foreach (ElementType type2 in typeof(ElementType).GetValues())
                {
                    FrameworkElement templateChild = base.GetTemplateChild("Element" + type2.ToString()) as FrameworkElement;
                    if (templateChild != null)
                    {
                        templateChild.Style = ((type2 & visibleElements) == 0) ? this.ElementStyleOff : this.ElementStyleOn;
                    }
                }
            }
        }

        private void UpdateElementStyles()
        {
            FrameworkElement templateChild = base.GetTemplateChild("ElementRoot") as FrameworkElement;
            if (templateChild != null)
            {
                if (templateChild.Resources.Contains("ElementStyleOff"))
                {
                    this.ElementStyleOff = templateChild.Resources["ElementStyleOff"] as Style;
                }
                if (templateChild.Resources.Contains("ElementStyleOn"))
                {
                    this.ElementStyleOn = templateChild.Resources["ElementStyleOn"] as Style;
                }
            }
        }

        public char Char
        {
            get => 
                (char) base.GetValue(CharProperty);
            set => 
                base.SetValue(CharProperty, value);
        }

        public bool ShowDot
        {
            get => 
                (bool) base.GetValue(ShowDotProperty);
            set => 
                base.SetValue(ShowDotProperty, value);
        }

        protected Style ElementStyleOff { get; private set; }

        protected Style ElementStyleOn { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CalculatorGaugeSegment.<>c <>9 = new CalculatorGaugeSegment.<>c();

            internal void <.cctor>b__3_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CalculatorGaugeSegment) d).PropertyChangedChar((char) e.OldValue);
            }

            internal void <.cctor>b__3_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CalculatorGaugeSegment) d).PropertyChangedShowDot();
            }
        }

        [Flags]
        protected enum ElementType
        {
            SegmentLeftBottom = 1,
            SegmentLeftTop = 2,
            SegmentTop = 4,
            SegmentRightTop = 8,
            SegmentRightBottom = 0x10,
            SegmentBottom = 0x20,
            SegmentMiddle = 0x40,
            Dot = 0x80
        }
    }
}

