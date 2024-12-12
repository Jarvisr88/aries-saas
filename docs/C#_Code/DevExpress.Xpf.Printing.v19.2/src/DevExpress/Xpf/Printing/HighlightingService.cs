namespace DevExpress.Xpf.Printing
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Printing.Native;
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class HighlightingService : IHighlightingService
    {
        private Border highlightRect;

        private static Brush CreateHatchBrush()
        {
            LinearGradientBrush brush = new LinearGradientBrush {
                MappingMode = BrushMappingMode.Absolute,
                SpreadMethod = GradientSpreadMethod.Repeat,
                StartPoint = new Point(0.0, 0.0),
                EndPoint = new Point(3.0, 3.0)
            };
            GradientStop stop = new GradientStop {
                Color = Colors.Gray
            };
            brush.GradientStops.Add(stop);
            stop = new GradientStop {
                Color = Colors.Gray,
                Offset = 0.5
            };
            brush.GradientStops.Add(stop);
            stop = new GradientStop {
                Color = Colors.Transparent,
                Offset = 0.5
            };
            brush.GradientStops.Add(stop);
            stop = new GradientStop {
                Color = Colors.Transparent,
                Offset = 1.0
            };
            brush.GradientStops.Add(stop);
            return brush;
        }

        public void HideHighlighting()
        {
            this.HighlightRect.RemoveFromVisualTree();
        }

        public void ShowHighlighting(FrameworkElement parent, FrameworkElement target)
        {
            if (!(parent is Canvas))
            {
                throw new ArgumentException("parent");
            }
            new OnLoadedScheduler().Schedule(() => this.ShowHighlightingCore((Canvas) parent, target), target);
        }

        private unsafe void ShowHighlightingCore(Canvas parentCanvas, FrameworkElement target)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }
            if (parentCanvas == null)
            {
                throw new ArgumentNullException("parentCanvas");
            }
            if (target.IsInVisualTree() && parentCanvas.IsInVisualTree())
            {
                Point point = LayoutHelper.GetRelativeElementRect(target, parentCanvas).Location();
                Point* pointPtr1 = &point;
                pointPtr1.X -= 6.0;
                Point* pointPtr2 = &point;
                pointPtr2.Y -= 6.0;
                Canvas.SetLeft(this.HighlightRect, point.X);
                Canvas.SetTop(this.HighlightRect, point.Y);
                Panel.SetZIndex(this.HighlightRect, 0xff);
                this.HighlightRect.Width = target.Width + 12.0;
                this.HighlightRect.Height = target.Height + 12.0;
                Point location = new Point();
                RectangleGeometry geometry1 = new RectangleGeometry();
                RectangleGeometry geometry2 = new RectangleGeometry();
                geometry2.Rect = new Rect(location, new Size((parentCanvas.Width > point.X) ? (parentCanvas.Width - point.X) : 0.0, (parentCanvas.Height > point.Y) ? (parentCanvas.Height - point.Y) : 0.0));
                this.HighlightRect.Clip = geometry2;
                this.HideHighlighting();
                parentCanvas.Children.Add(this.HighlightRect);
            }
        }

        private Border HighlightRect
        {
            get
            {
                if (this.highlightRect == null)
                {
                    this.highlightRect = new Border();
                    this.highlightRect.BorderThickness = new Thickness(5.0);
                    this.highlightRect.BorderBrush = CreateHatchBrush();
                }
                return this.highlightRect;
            }
        }
    }
}

