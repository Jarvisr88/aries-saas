namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Windows.Media;

    public abstract class BaseSparklinePainter
    {
        private readonly Stack antialiasingStack = new Stack();
        private SparklineMappingBase mapping;
        private IList<SparklinePoint> data;
        private SparklineControl view;
        private SparklineDrawingCache localCache;
        private ExtremePointIndexes extremeIndexes;

        protected BaseSparklinePainter()
        {
        }

        public void Draw(DrawingContext drawingContext)
        {
            if (this.mapping != null)
            {
                this.localCache ??= new SparklineDrawingCache();
                this.DrawInternal(drawingContext);
            }
        }

        protected abstract void DrawInternal(DrawingContext drawingContext);
        protected Pen GetPen(Color color, int width)
        {
            Pen pen = null;
            if (this.localCache != null)
            {
                pen = this.localCache.GetPen(this.localCache.GetSolidBrush(color), width);
            }
            if (pen == null)
            {
                throw new Exception("Incorrect cache");
            }
            if (pen.Thickness != width)
            {
                pen.Thickness = width;
            }
            return pen;
        }

        protected Pen GetPen(SolidColorBrush brush, int width)
        {
            Pen pen = null;
            if (this.localCache != null)
            {
                pen = this.localCache.GetPen(brush, width);
            }
            if (pen == null)
            {
                throw new Exception("Incorrect cache");
            }
            if (pen.Thickness != width)
            {
                pen.Thickness = width;
            }
            return pen;
        }

        protected virtual SolidColorBrush GetPointBrush(PointPresentationType pointType)
        {
            switch (pointType)
            {
                case PointPresentationType.HighPoint:
                    return this.View.ActualMaxPointBrush;

                case PointPresentationType.LowPoint:
                    return this.View.ActualMinPointBrush;

                case PointPresentationType.StartPoint:
                    return this.View.ActualStartPointBrush;

                case PointPresentationType.EndPoint:
                    return this.View.ActualEndPointBrush;

                case PointPresentationType.NegativePoint:
                    return this.View.ActualNegativePointBrush;
            }
            return this.View.ActualBrush;
        }

        protected PointPresentationType GetPointPresentationType(int index)
        {
            if ((index >= 0) && (index < this.Data.Count))
            {
                if (this.View.ActualHighlightMaxPoint && this.extremeIndexes.IsMaxPoint(index))
                {
                    return PointPresentationType.HighPoint;
                }
                if (this.View.ActualHighlightMinPoint && this.extremeIndexes.IsMinPoint(index))
                {
                    return PointPresentationType.LowPoint;
                }
                if (this.View.ActualHighlightStartPoint && this.extremeIndexes.IsStartPoint(index))
                {
                    return PointPresentationType.StartPoint;
                }
                if (this.View.ActualHighlightEndPoint && this.extremeIndexes.IsEndPoint(index))
                {
                    return PointPresentationType.EndPoint;
                }
                if ((this.Data[index].Value < 0.0) && this.View.ActualShowNegativePoint)
                {
                    return PointPresentationType.NegativePoint;
                }
            }
            return PointPresentationType.SimplePoint;
        }

        protected SolidColorBrush GetSolidBrush(Color color)
        {
            if (this.localCache == null)
            {
                throw new Exception("Incorrect cache");
            }
            return this.localCache.GetSolidBrush(color);
        }

        public void Initialize(IList<SparklinePoint> data, SparklineControl view, SparklineMappingBase mapping, ExtremePointIndexes extremeIndexes)
        {
            if ((view != null) && (data != null))
            {
                this.data = data;
                this.view = view;
                this.extremeIndexes = extremeIndexes;
                if (view.Type == this.SparklineType)
                {
                    this.mapping = mapping;
                }
            }
        }

        public abstract SparklineViewType SparklineType { get; }

        protected abstract bool EnableAntialiasing { get; }

        protected IList<SparklinePoint> Data =>
            this.data;

        protected SparklineControl View =>
            this.view;

        protected internal SparklineMappingBase Mapping =>
            this.mapping;

        public enum PointPresentationType
        {
            HighPoint,
            LowPoint,
            StartPoint,
            EndPoint,
            NegativePoint,
            SimplePoint
        }
    }
}

