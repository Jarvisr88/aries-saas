namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class LayoutTransformPanel : Panel
    {
        public static readonly DependencyProperty ClockwiseProperty;
        public static readonly DependencyProperty OrientationProperty;

        static LayoutTransformPanel()
        {
            ClockwiseProperty = DependencyPropertyManager.Register("Clockwise", typeof(bool), typeof(LayoutTransformPanel), new PropertyMetadata(false, (d, e) => ((LayoutTransformPanel) d).OnClockwisePropertyChanged()));
            OrientationProperty = DependencyPropertyManager.Register("Orientation", typeof(System.Windows.Controls.Orientation), typeof(LayoutTransformPanel), new PropertyMetadata(System.Windows.Controls.Orientation.Vertical, (d, e) => ((LayoutTransformPanel) d).OnOrientationPropertyChanged()));
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (base.Children.Count == 0)
            {
                return Size.Empty;
            }
            Size correctSize = this.GetCorrectSize(finalSize);
            base.Children[0].Arrange(new Rect(0.0, 0.0, correctSize.Width, correctSize.Height));
            base.RenderTransform = this.GetTransform(correctSize);
            return this.GetCorrectSize(correctSize);
        }

        private Size GetCorrectSize(Size size) => 
            (this.Orientation == System.Windows.Controls.Orientation.Vertical) ? size : new Size(size.Height, size.Width);

        private Transform GetTransform(Size size) => 
            (this.Orientation == System.Windows.Controls.Orientation.Horizontal) ? this.GetTransformHorizontal(size) : this.GetTransformVertical(size);

        private Transform GetTransformHorizontal(Size size)
        {
            TransformGroup group = new TransformGroup();
            if (this.Clockwise)
            {
                RotateTransform transform1 = new RotateTransform();
                transform1.Angle = 90.0;
                group.Children.Add(transform1);
                TranslateTransform transform2 = new TranslateTransform();
                transform2.X = size.Height;
                group.Children.Add(transform2);
            }
            else
            {
                RotateTransform transform3 = new RotateTransform();
                transform3.Angle = -90.0;
                group.Children.Add(transform3);
                TranslateTransform transform4 = new TranslateTransform();
                transform4.Y = size.Width;
                group.Children.Add(transform4);
            }
            return group;
        }

        private Transform GetTransformVertical(Size size) => 
            new RotateTransform();

        protected override Size MeasureOverride(Size availableSize)
        {
            if (base.Children.Count == 0)
            {
                return Size.Empty;
            }
            UIElement element = base.Children[0];
            element.Measure(this.GetCorrectSize(availableSize));
            return this.GetCorrectSize(element.DesiredSize);
        }

        private void OnClockwisePropertyChanged()
        {
            base.InvalidateMeasure();
        }

        private void OnOrientationPropertyChanged()
        {
            base.InvalidateMeasure();
        }

        public bool Clockwise
        {
            get => 
                (bool) base.GetValue(ClockwiseProperty);
            set => 
                base.SetValue(ClockwiseProperty, value);
        }

        public System.Windows.Controls.Orientation Orientation
        {
            get => 
                (System.Windows.Controls.Orientation) base.GetValue(OrientationProperty);
            set => 
                base.SetValue(OrientationProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LayoutTransformPanel.<>c <>9 = new LayoutTransformPanel.<>c();

            internal void <.cctor>b__17_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutTransformPanel) d).OnClockwisePropertyChanged();
            }

            internal void <.cctor>b__17_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutTransformPanel) d).OnOrientationPropertyChanged();
            }
        }
    }
}

