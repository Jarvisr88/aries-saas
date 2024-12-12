namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class RenderTransformPanel : Panel
    {
        public static readonly DependencyProperty OrientationProperty;
        public static readonly DependencyProperty DockProperty;

        static RenderTransformPanel()
        {
            DependencyPropertyRegistrator<RenderTransformPanel> registrator = new DependencyPropertyRegistrator<RenderTransformPanel>();
            registrator.Register<System.Windows.Controls.Orientation>("Orientation", ref OrientationProperty, System.Windows.Controls.Orientation.Vertical, (d, e) => ((RenderTransformPanel) d).OnOrientationPropertyChanged(), null);
            registrator.Register<System.Windows.Controls.Dock>("Dock", ref DockProperty, System.Windows.Controls.Dock.Top, (d, e) => ((RenderTransformPanel) d).OnDockChanged((System.Windows.Controls.Dock) e.OldValue, (System.Windows.Controls.Dock) e.NewValue), null);
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
            (this.Orientation == System.Windows.Controls.Orientation.Horizontal) ? size : new Size(size.Height, size.Width);

        private Transform GetTransform(Size size)
        {
            TransformGroup group = new TransformGroup();
            switch (this.Dock)
            {
                case System.Windows.Controls.Dock.Left:
                {
                    RotateTransform transform1 = new RotateTransform();
                    transform1.Angle = -90.0;
                    group.Children.Add(transform1);
                    TranslateTransform transform2 = new TranslateTransform();
                    transform2.Y = size.Width;
                    group.Children.Add(transform2);
                    break;
                }
                case System.Windows.Controls.Dock.Right:
                {
                    RotateTransform transform3 = new RotateTransform();
                    transform3.Angle = 90.0;
                    group.Children.Add(transform3);
                    TranslateTransform transform4 = new TranslateTransform();
                    transform4.X = size.Height;
                    group.Children.Add(transform4);
                    break;
                }
                case System.Windows.Controls.Dock.Bottom:
                {
                    RotateTransform transform5 = new RotateTransform();
                    transform5.Angle = 180.0;
                    group.Children.Add(transform5);
                    TranslateTransform transform6 = new TranslateTransform();
                    transform6.X = size.Width;
                    group.Children.Add(transform6);
                    TranslateTransform transform7 = new TranslateTransform();
                    transform7.Y = size.Height;
                    group.Children.Add(transform7);
                    break;
                }
                default:
                    break;
            }
            return group;
        }

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

        protected virtual void OnDockChanged(System.Windows.Controls.Dock oldValue, System.Windows.Controls.Dock newValue)
        {
            base.InvalidateMeasure();
        }

        private void OnOrientationPropertyChanged()
        {
            base.InvalidateMeasure();
        }

        public System.Windows.Controls.Dock Dock
        {
            get => 
                (System.Windows.Controls.Dock) base.GetValue(DockProperty);
            set => 
                base.SetValue(DockProperty, value);
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
            public static readonly RenderTransformPanel.<>c <>9 = new RenderTransformPanel.<>c();

            internal void <.cctor>b__2_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((RenderTransformPanel) d).OnOrientationPropertyChanged();
            }

            internal void <.cctor>b__2_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((RenderTransformPanel) d).OnDockChanged((Dock) e.OldValue, (Dock) e.NewValue);
            }
        }
    }
}

