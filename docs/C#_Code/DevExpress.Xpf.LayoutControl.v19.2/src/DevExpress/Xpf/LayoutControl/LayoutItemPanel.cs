namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class LayoutItemPanel : Panel
    {
        public static readonly DependencyProperty ElementSpaceProperty;
        public static readonly DependencyProperty LabelPositionProperty;
        private UIElement _Content;
        private UIElement _Label;

        static LayoutItemPanel()
        {
            ElementSpaceProperty = DependencyProperty.Register("ElementSpace", typeof(double), typeof(LayoutItemPanel), new PropertyMetadata((o, e) => ((LayoutItemPanel) o).OnElementSpaceChanged()));
            LabelPositionProperty = DependencyProperty.Register("LabelPosition", typeof(LayoutItemLabelPosition), typeof(LayoutItemPanel), new PropertyMetadata((o, e) => ((LayoutItemPanel) o).OnLabelPositionChanged()));
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if ((this.Label != null) && (this.Content != null))
            {
                Rect bounds = RectHelper.New(finalSize);
                this.Label.Arrange(this.GetLabelBounds(bounds));
                this.Content.Arrange(this.GetContentBounds(bounds));
            }
            return base.ArrangeOverride(finalSize);
        }

        protected virtual Size GetContentAvailableSize(Size availableSize)
        {
            Size size = availableSize;
            this.SetLength(ref size, Math.Max((double) 0.0, (double) (this.GetLength(size) - (this.GetLength(this.Label.DesiredSize) + this.ActualElementSpace))));
            return size;
        }

        protected virtual Rect GetContentBounds(Rect bounds)
        {
            Rect rect = bounds;
            double num = this.GetLength(this.Label.DesiredSize) + this.ActualElementSpace;
            this.SetOffset(ref rect, num);
            this.SetLength(ref rect, Math.Max((double) 0.0, (double) (this.GetLength(rect) - num)));
            return rect;
        }

        protected virtual Size GetDesiredSize()
        {
            Size size = new Size();
            this.SetLength(ref size, (this.GetLength(this.Label.DesiredSize) + this.ActualElementSpace) + this.GetLength(this.Content.DesiredSize));
            this.SetWidth(ref size, Math.Max(this.GetWidth(this.Label.DesiredSize), this.GetWidth(this.Content.DesiredSize)));
            return size;
        }

        protected virtual Size GetLabelAvailableSize(Size availableSize)
        {
            Size size = availableSize;
            this.SetLength(ref size, double.PositiveInfinity);
            return size;
        }

        protected virtual Rect GetLabelBounds(Rect bounds)
        {
            Rect rect = bounds;
            this.SetLength(ref rect, this.GetLength(this.Label.DesiredSize));
            return rect;
        }

        protected double GetLength(Rect rect) => 
            (this.Orientation == System.Windows.Controls.Orientation.Horizontal) ? rect.Width : rect.Height;

        protected double GetLength(Size size) => 
            (this.Orientation == System.Windows.Controls.Orientation.Horizontal) ? size.Width : size.Height;

        protected double GetWidth(Size size) => 
            (this.Orientation == System.Windows.Controls.Orientation.Horizontal) ? size.Height : size.Width;

        protected override Size MeasureOverride(Size availableSize)
        {
            if ((this.Label == null) || (this.Content == null))
            {
                return new Size(0.0, 0.0);
            }
            this.Label.Measure(this.GetLabelAvailableSize(availableSize));
            this.Content.Measure(this.GetContentAvailableSize(availableSize));
            return this.GetDesiredSize();
        }

        protected virtual void OnElementSpaceChanged()
        {
            base.InvalidateMeasure();
        }

        protected virtual void OnLabelPositionChanged()
        {
            this.Orientation = (this.LabelPosition == LayoutItemLabelPosition.Left) ? System.Windows.Controls.Orientation.Horizontal : System.Windows.Controls.Orientation.Vertical;
            base.InvalidateMeasure();
        }

        protected void SetLength(ref Rect rect, double value)
        {
            if (this.Orientation == System.Windows.Controls.Orientation.Horizontal)
            {
                rect.Width = value;
            }
            else
            {
                rect.Height = value;
            }
        }

        protected void SetLength(ref Size size, double value)
        {
            if (this.Orientation == System.Windows.Controls.Orientation.Horizontal)
            {
                size.Width = value;
            }
            else
            {
                size.Height = value;
            }
        }

        protected void SetOffset(ref Rect rect, double value)
        {
            if (this.Orientation == System.Windows.Controls.Orientation.Horizontal)
            {
                rect.X = value;
            }
            else
            {
                rect.Y = value;
            }
        }

        protected void SetWidth(ref Size size, double value)
        {
            if (this.Orientation == System.Windows.Controls.Orientation.Horizontal)
            {
                size.Height = value;
            }
            else
            {
                size.Width = value;
            }
        }

        public UIElement Content
        {
            get => 
                this._Content;
            set
            {
                if (!ReferenceEquals(this.Content, value))
                {
                    if (this.Content != null)
                    {
                        base.Children.Remove(this.Content);
                    }
                    this._Content = value;
                    if (this.Content != null)
                    {
                        base.Children.Add(this.Content);
                    }
                }
            }
        }

        public double ElementSpace
        {
            get => 
                (double) base.GetValue(ElementSpaceProperty);
            set => 
                base.SetValue(ElementSpaceProperty, value);
        }

        public UIElement Label
        {
            get => 
                this._Label;
            set
            {
                if (!ReferenceEquals(this.Label, value))
                {
                    if (this.Label != null)
                    {
                        base.Children.Remove(this.Label);
                    }
                    this._Label = value;
                    if (this.Label != null)
                    {
                        base.Children.Add(this.Label);
                    }
                }
            }
        }

        public LayoutItemLabelPosition LabelPosition
        {
            get => 
                (LayoutItemLabelPosition) base.GetValue(LabelPositionProperty);
            set => 
                base.SetValue(LabelPositionProperty, value);
        }

        protected double ActualElementSpace =>
            ((this.Label == null) || !this.Label.GetVisible()) ? 0.0 : this.ElementSpace;

        protected System.Windows.Controls.Orientation Orientation { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LayoutItemPanel.<>c <>9 = new LayoutItemPanel.<>c();

            internal void <.cctor>b__39_0(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutItemPanel) o).OnElementSpaceChanged();
            }

            internal void <.cctor>b__39_1(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutItemPanel) o).OnLabelPositionChanged();
            }
        }
    }
}

