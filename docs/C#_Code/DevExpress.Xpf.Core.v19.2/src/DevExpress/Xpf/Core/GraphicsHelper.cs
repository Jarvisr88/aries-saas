namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Windows;
    using System.Windows.Media;

    public static class GraphicsHelper
    {
        public static PathFigure CreateRectFigure(Rect rect)
        {
            PathFigure figure1 = new PathFigure();
            figure1.IsClosed = true;
            figure1.StartPoint = rect.TopLeft();
            PathFigure figure = figure1;
            PolyLineSegment segment = new PolyLineSegment {
                Points = { 
                    rect.TopRight(),
                    rect.BottomRight(),
                    rect.BottomLeft()
                }
            };
            figure.Segments.Add(segment);
            return figure;
        }
    }
}

