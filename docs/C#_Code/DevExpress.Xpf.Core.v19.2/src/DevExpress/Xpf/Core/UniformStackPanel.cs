namespace DevExpress.Xpf.Core
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Utils.About;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    [ToolboxTabName("DX.19.2: Navigation & Layout"), DXToolboxBrowsable, LicenseProvider(typeof(DX_WPF_LicenseProvider))]
    public class UniformStackPanel : PanelBase
    {
        public const double DefaultChildSpacing = 5.0;
        public static readonly DependencyProperty ChildSpacingProperty;
        public static readonly DependencyProperty OrientationProperty;

        static UniformStackPanel()
        {
            ChildSpacingProperty = DependencyProperty.Register("ChildSpacing", typeof(double), typeof(UniformStackPanel), new PropertyMetadata(5.0, (o, e) => ((UniformStackPanel) o).OnChildSpacingChanged()));
            OrientationProperty = DependencyProperty.Register("Orientation", typeof(System.Windows.Controls.Orientation), typeof(UniformStackPanel), new PropertyMetadata(System.Windows.Controls.Orientation.Horizontal, (o, e) => ((UniformStackPanel) o).OnOrientationChanged()));
        }

        protected override Size OnArrange(Rect bounds)
        {
            Rect finalRect = bounds;
            foreach (FrameworkElement element in base.GetLogicalChildren(true))
            {
                if (this.Orientation == System.Windows.Controls.Orientation.Horizontal)
                {
                    finalRect.Width = this.MaxChildSize.Width;
                }
                else
                {
                    finalRect.Height = this.MaxChildSize.Height;
                }
                element.Arrange(finalRect);
                if (this.Orientation == System.Windows.Controls.Orientation.Horizontal)
                {
                    finalRect.X = finalRect.Right + this.ChildSpacing;
                }
                else
                {
                    finalRect.Y = finalRect.Bottom + this.ChildSpacing;
                }
            }
            return base.OnArrange(bounds);
        }

        protected virtual void OnChildSpacingChanged()
        {
            base.Changed();
        }

        protected override Size OnMeasure(Size availableSize)
        {
            Size size = base.OnMeasure(availableSize);
            if (this.Orientation == System.Windows.Controls.Orientation.Horizontal)
            {
                availableSize.Width = double.PositiveInfinity;
            }
            else
            {
                availableSize.Height = double.PositiveInfinity;
            }
            Size zero = SizeHelper.Zero;
            foreach (FrameworkElement element in base.GetLogicalChildren(false))
            {
                element.Measure(availableSize);
                SizeHelper.UpdateMaxSize(ref zero, element.DesiredSize);
            }
            this.MaxChildSize = zero;
            int count = base.GetLogicalChildren(true).Count;
            if (this.Orientation == System.Windows.Controls.Orientation.Horizontal)
            {
                size.Width = Math.Max(size.Width, (count * this.MaxChildSize.Width) + ((count - 1) * this.ChildSpacing));
                size.Height = Math.Max(size.Height, this.MaxChildSize.Height);
            }
            else
            {
                size.Width = Math.Max(size.Width, this.MaxChildSize.Width);
                size.Height = Math.Max(size.Height, (count * this.MaxChildSize.Height) + ((count - 1) * this.ChildSpacing));
            }
            return size;
        }

        protected virtual void OnOrientationChanged()
        {
            base.Changed();
        }

        public double ChildSpacing
        {
            get => 
                (double) base.GetValue(ChildSpacingProperty);
            set => 
                base.SetValue(ChildSpacingProperty, value);
        }

        public System.Windows.Controls.Orientation Orientation
        {
            get => 
                (System.Windows.Controls.Orientation) base.GetValue(OrientationProperty);
            set => 
                base.SetValue(OrientationProperty, value);
        }

        protected Size MaxChildSize { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly UniformStackPanel.<>c <>9 = new UniformStackPanel.<>c();

            internal void <.cctor>b__18_0(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((UniformStackPanel) o).OnChildSpacingChanged();
            }

            internal void <.cctor>b__18_1(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((UniformStackPanel) o).OnOrientationChanged();
            }
        }
    }
}

