namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Windows;
    using System.Windows.Media;

    public class BarSparklinePainter : BaseSparklinePainter
    {
        private void DrawBar(DrawingContext drawingContext, double x, double y, double barWidth, int i)
        {
            double height = Math.Abs((double) (y - base.Mapping.ScreenYZeroValue));
            if (height == 0.0)
            {
                height = 1.0;
            }
            BaseSparklinePainter.PointPresentationType pointPresentationType = base.GetPointPresentationType(i);
            SolidColorBrush pointBrush = this.GetPointBrush(pointPresentationType);
            drawingContext.DrawRectangle(pointBrush, base.GetPen(pointBrush, 1), new Rect(x, Math.Min(y, base.Mapping.ScreenYZeroValue), barWidth, height));
        }

        protected override void DrawInternal(DrawingContext drawingContext)
        {
            double barWidth = Math.Max((double) 1.0, (double) ((base.Mapping.MinPointsDistancePx - this.BarView.ActualBarDistance) - 1.0));
            int num2 = Convert.ToInt32((double) (barWidth / 2.0));
            Point point = new Point();
            int i = -1;
            bool flag = false;
            for (int j = 0; j < base.Data.Count; j++)
            {
                double barValue = base.Data[j].Value;
                if (SparklineMathUtils.IsValidDouble(barValue))
                {
                    barValue = this.GetBarValue(barValue);
                    if (this.ShouldRenderBar(barValue))
                    {
                        double x = base.Mapping.MapXValueToScreen(base.Data[j].Argument) - num2;
                        double y = base.Mapping.MapYValueToScreen(barValue);
                        if (!base.Mapping.IsArgumentVisible(base.Data[j].Argument) && !flag)
                        {
                            point = new Point(x, y);
                            i = j;
                        }
                        else
                        {
                            if (i >= 0)
                            {
                                this.DrawBar(drawingContext, point.X, point.Y, barWidth, i);
                                i = -1;
                            }
                            flag = true;
                            this.DrawBar(drawingContext, x, y, barWidth, j);
                            if (!base.Mapping.IsArgumentVisible(base.Data[j].Argument))
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }

        protected virtual double GetBarValue(double value) => 
            value;

        internal bool ShouldRenderBar(double value) => 
            ((value <= 0.0) || (value < base.Mapping.MaxValue)) ? (((value < 0.0) && (value <= base.Mapping.MinValue)) || (((base.Mapping.MaxValue > 0.0) && (base.Mapping.MinValue < 0.0)) || base.Mapping.IsValueVisible(value))) : true;

        private BarSparklineControlBase BarView =>
            (BarSparklineControlBase) base.View;

        protected override bool EnableAntialiasing =>
            false;

        public override SparklineViewType SparklineType =>
            SparklineViewType.Bar;
    }
}

