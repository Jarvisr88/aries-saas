namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;
    using System.Windows.Media;

    public static class BorderExtensions
    {
        public static readonly DependencyProperty ClipChildProperty = DependencyProperty.RegisterAttached("ClipChild", typeof(bool), typeof(BorderExtensions), new PropertyMetadata(new PropertyChangedCallback(BorderExtensions.OnClipChildChanged)));
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static DependencyProperty BorderThicknessListener = DependencyProperty.RegisterAttached("BorderThicknessListener", typeof(Thickness), typeof(BorderExtensions), new PropertyMetadata(new PropertyChangedCallback(BorderExtensions.OnBorderPropertyChanged)));
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static DependencyProperty CornerRadiusListener = DependencyProperty.RegisterAttached("CornerRadiusListener", typeof(CornerRadius), typeof(BorderExtensions), new PropertyMetadata(new PropertyChangedCallback(BorderExtensions.OnBorderPropertyChanged)));

        private static bool CanMapRect(Visual child)
        {
            PresentationSource source = PresentationSource.FromDependencyObject(child);
            return ((source != null) && (source.RootVisual.TransformToDescendant(child) != null));
        }

        public static void ClipChild(this Border border)
        {
            if (border.Child != null)
            {
                border.Child.Clip = border.GetChildClip();
            }
        }

        public static Geometry GetChildClip(this Border border)
        {
            if ((border.Child == null) || !border.IsInVisualTree())
            {
                return null;
            }
            Rect rect = RectHelper.New(border.GetSize());
            RectHelper.Deflate(ref rect, border.BorderThickness);
            if (border.Child.GetVisible())
            {
                rect = TransformBounds(border, rect);
            }
            else
            {
                rect.X = rect.Y = 0.0;
            }
            CornerRadius cornerRadius = border.CornerRadius;
            Thickness borderThickness = border.BorderThickness;
            Size[] sizeArray = new Size[] { new Size(Math.Max((double) 0.0, (double) (cornerRadius.TopLeft - (borderThickness.Left / 2.0))), Math.Max((double) 0.0, (double) (cornerRadius.TopLeft - (borderThickness.Top / 2.0)))), new Size(Math.Max((double) 0.0, (double) (cornerRadius.TopRight - (borderThickness.Right / 2.0))), Math.Max((double) 0.0, (double) (cornerRadius.TopRight - (borderThickness.Top / 2.0)))), new Size(Math.Max((double) 0.0, (double) (cornerRadius.BottomRight - (borderThickness.Right / 2.0))), Math.Max((double) 0.0, (double) (cornerRadius.BottomRight - (borderThickness.Bottom / 2.0)))), new Size(Math.Max((double) 0.0, (double) (cornerRadius.BottomLeft - (borderThickness.Left / 2.0))), Math.Max((double) 0.0, (double) (cornerRadius.BottomLeft - (borderThickness.Bottom / 2.0)))) };
            PathFigure figure1 = new PathFigure();
            figure1.IsClosed = true;
            PathFigure figure = figure1;
            figure.StartPoint = new Point(rect.Left, rect.Top + sizeArray[0].Height);
            ArcSegment segment1 = new ArcSegment();
            segment1.Point = new Point(rect.Left + sizeArray[0].Width, rect.Top);
            segment1.Size = sizeArray[0];
            segment1.SweepDirection = SweepDirection.Clockwise;
            figure.Segments.Add(segment1);
            LineSegment segment2 = new LineSegment();
            segment2.Point = new Point(rect.Right - sizeArray[1].Width, rect.Top);
            figure.Segments.Add(segment2);
            ArcSegment segment3 = new ArcSegment();
            segment3.Point = new Point(rect.Right, rect.Top + sizeArray[1].Height);
            segment3.Size = sizeArray[1];
            segment3.SweepDirection = SweepDirection.Clockwise;
            figure.Segments.Add(segment3);
            LineSegment segment4 = new LineSegment();
            segment4.Point = new Point(rect.Right, rect.Bottom - sizeArray[2].Height);
            figure.Segments.Add(segment4);
            ArcSegment segment5 = new ArcSegment();
            segment5.Point = new Point(rect.Right - sizeArray[2].Width, rect.Bottom);
            segment5.Size = sizeArray[2];
            segment5.SweepDirection = SweepDirection.Clockwise;
            figure.Segments.Add(segment5);
            LineSegment segment6 = new LineSegment();
            segment6.Point = new Point(rect.Left + sizeArray[3].Width, rect.Bottom);
            figure.Segments.Add(segment6);
            ArcSegment segment7 = new ArcSegment();
            segment7.Point = new Point(rect.Left, rect.Bottom - sizeArray[3].Height);
            segment7.Size = sizeArray[3];
            segment7.SweepDirection = SweepDirection.Clockwise;
            figure.Segments.Add(segment7);
            return new PathGeometry { Figures = { figure } };
        }

        public static bool GetClipChild(Border border) => 
            (bool) border.GetValue(ClipChildProperty);

        private static void OnBorderPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            Border border = (Border) o;
            if (border.IsInVisualTree())
            {
                if (ReferenceEquals(e.Property, CornerRadiusListener))
                {
                    border.ClipChild();
                }
                else
                {
                    if (border.Child != null)
                    {
                        Rect rect = new Rect(0.0, 0.0, border.ActualWidth, border.ActualHeight);
                        RectHelper.Deflate(ref rect, border.BorderThickness);
                        RectHelper.Deflate(ref rect, border.Padding);
                        if (LayoutInformation.GetLayoutSlot((FrameworkElement) border.Child) == rect)
                        {
                            border.ClipChild();
                            return;
                        }
                    }
                    EventHandler onLayoutUpdated = null;
                    onLayoutUpdated = delegate (object <sender>, EventArgs <e>) {
                        border.LayoutUpdated -= onLayoutUpdated;
                        border.ClipChild();
                    };
                    border.LayoutUpdated += onLayoutUpdated;
                }
            }
        }

        private static void OnBorderSizeChanged(object sender, SizeChangedEventArgs e)
        {
            ((Border) sender).ClipChild();
        }

        private static void OnClipChildChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            Border border = o as Border;
            if (border != null)
            {
                if (!((bool) e.NewValue))
                {
                    border.ClearValue(BorderThicknessListener);
                    border.ClearValue(CornerRadiusListener);
                    border.SizeChanged -= new SizeChangedEventHandler(BorderExtensions.OnBorderSizeChanged);
                }
                else
                {
                    Binding binding = new Binding("BorderThickness");
                    binding.Source = border;
                    border.SetBinding(BorderThicknessListener, binding);
                    Binding binding2 = new Binding("CornerRadius");
                    binding2.Source = border;
                    border.SetBinding(CornerRadiusListener, binding2);
                    border.SizeChanged += new SizeChangedEventHandler(BorderExtensions.OnBorderSizeChanged);
                }
            }
        }

        public static void SetClipChild(Border border, bool value)
        {
            border.SetValue(ClipChildProperty, value);
        }

        private static Rect TransformBounds(Border border, Rect rect) => 
            CanMapRect(border.Child) ? border.MapRect(rect, ((FrameworkElement) border.Child)) : new Rect(border.TransformToVisual(border.Child).Transform(rect.TopLeft), border.TransformToVisual(border.Child).Transform(rect.BottomRight));
    }
}

