namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;

    public class TransparentBrushRectangle : ContentControl
    {
        public static readonly DependencyProperty LightColorProperty;
        public static readonly DependencyProperty DarkColorProperty;
        public static readonly DependencyProperty BlockWidthProperty;
        public static readonly DependencyProperty BlockHeightProperty;

        static TransparentBrushRectangle()
        {
            Type ownerType = typeof(TransparentBrushRectangle);
            DarkColorProperty = DependencyPropertyManager.Register("DarkColor", typeof(Color), ownerType, new FrameworkPropertyMetadata(Color.FromArgb(0xff, 0xd3, 0xd3, 0xd3), FrameworkPropertyMetadataOptions.AffectsMeasure, (obj, args) => ((TransparentBrushRectangle) obj).UpdateRectangle()));
            LightColorProperty = DependencyPropertyManager.Register("LightColor", typeof(Color), ownerType, new FrameworkPropertyMetadata(Colors.White, FrameworkPropertyMetadataOptions.AffectsMeasure, (obj, args) => ((TransparentBrushRectangle) obj).UpdateRectangle()));
            BlockHeightProperty = DependencyPropertyManager.Register("BlockHeight", typeof(double), ownerType, new FrameworkPropertyMetadata(4.0, FrameworkPropertyMetadataOptions.AffectsMeasure, (obj, args) => ((TransparentBrushRectangle) obj).UpdateRectangle()));
            BlockWidthProperty = DependencyPropertyManager.Register("BlockWidth", typeof(double), ownerType, new FrameworkPropertyMetadata(4.0, FrameworkPropertyMetadataOptions.AffectsMeasure, (obj, args) => ((TransparentBrushRectangle) obj).UpdateRectangle()));
        }

        public TransparentBrushRectangle()
        {
            base.Loaded += new RoutedEventHandler(this.OnLoaded);
            base.SizeChanged += new SizeChangedEventHandler(this.OnSizeChanged);
        }

        public void GenerateRectangle(Size containerSize)
        {
            GeometryGroup group = new GeometryGroup();
            GeometryGroup group2 = new GeometryGroup();
            int num = (int) Math.Ceiling((double) (containerSize.Width / this.BlockWidth));
            int num2 = (int) Math.Ceiling((double) (containerSize.Height / this.BlockHeight));
            double x = 0.0;
            double y = 0.0;
            int num5 = 0;
            while (num5 < num2)
            {
                int num6 = 0;
                while (true)
                {
                    if (num6 >= num)
                    {
                        x = 0.0;
                        y += this.BlockHeight;
                        num5++;
                        break;
                    }
                    if ((num5 % 2) == (num6 % 2))
                    {
                        RectangleGeometry geometry1 = new RectangleGeometry();
                        geometry1.Rect = new Rect(x, y, this.BlockWidth, this.BlockHeight);
                        group2.Children.Add(geometry1);
                    }
                    else
                    {
                        RectangleGeometry geometry2 = new RectangleGeometry();
                        geometry2.Rect = new Rect(x, y, this.BlockWidth, this.BlockHeight);
                        group.Children.Add(geometry2);
                    }
                    x += this.BlockWidth;
                    num6++;
                }
            }
            Path path1 = new Path();
            path1.Fill = new SolidColorBrush(this.DarkColor);
            path1.Data = group;
            Path element = path1;
            Path path3 = new Path();
            path3.Fill = new SolidColorBrush(this.LightColor);
            path3.Data = group2;
            Path path2 = path3;
            Canvas canvas1 = new Canvas();
            canvas1.Width = containerSize.Width;
            canvas1.Height = containerSize.Height;
            RectangleGeometry geometry3 = new RectangleGeometry();
            geometry3.Rect = new Rect(0.0, 0.0, containerSize.Width, containerSize.Height);
            canvas1.Clip = geometry3;
            Canvas canvas = canvas1;
            canvas.Children.Add(element);
            canvas.Children.Add(path2);
            base.Content = canvas;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.GenerateRectangle(new Size(base.ActualWidth, base.ActualHeight));
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.GenerateRectangle(new Size(base.ActualWidth, base.ActualHeight));
        }

        private void UpdateRectangle()
        {
            this.GenerateRectangle(new Size(base.ActualWidth, base.ActualHeight));
        }

        public Color DarkColor
        {
            get => 
                (Color) base.GetValue(DarkColorProperty);
            set => 
                base.SetValue(DarkColorProperty, value);
        }

        public Color LightColor
        {
            get => 
                (Color) base.GetValue(LightColorProperty);
            set => 
                base.SetValue(LightColorProperty, value);
        }

        public double BlockWidth
        {
            get => 
                (double) base.GetValue(BlockWidthProperty);
            set => 
                base.SetValue(BlockWidthProperty, value);
        }

        public double BlockHeight
        {
            get => 
                (double) base.GetValue(BlockHeightProperty);
            set => 
                base.SetValue(BlockHeightProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TransparentBrushRectangle.<>c <>9 = new TransparentBrushRectangle.<>c();

            internal void <.cctor>b__4_0(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            {
                ((TransparentBrushRectangle) obj).UpdateRectangle();
            }

            internal void <.cctor>b__4_1(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            {
                ((TransparentBrushRectangle) obj).UpdateRectangle();
            }

            internal void <.cctor>b__4_2(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            {
                ((TransparentBrushRectangle) obj).UpdateRectangle();
            }

            internal void <.cctor>b__4_3(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            {
                ((TransparentBrushRectangle) obj).UpdateRectangle();
            }
        }
    }
}

