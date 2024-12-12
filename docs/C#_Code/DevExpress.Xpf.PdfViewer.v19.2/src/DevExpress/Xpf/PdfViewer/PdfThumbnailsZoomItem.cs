namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Xpf.DocumentViewer;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    public class PdfThumbnailsZoomItem : CommandBase
    {
        private double minValue;
        private double maxValue;
        private double smallStep;
        private double largeStep;

        public double MinValue
        {
            get => 
                this.minValue;
            set => 
                base.SetProperty<double>(ref this.minValue, value, Expression.Lambda<Func<double>>(Expression.Property(Expression.Constant(this, typeof(PdfThumbnailsZoomItem)), (MethodInfo) methodof(PdfThumbnailsZoomItem.get_MinValue)), new ParameterExpression[0]));
        }

        public double MaxValue
        {
            get => 
                this.maxValue;
            set => 
                base.SetProperty<double>(ref this.maxValue, value, Expression.Lambda<Func<double>>(Expression.Property(Expression.Constant(this, typeof(PdfThumbnailsZoomItem)), (MethodInfo) methodof(PdfThumbnailsZoomItem.get_MaxValue)), new ParameterExpression[0]));
        }

        public double SmallStep
        {
            get => 
                this.smallStep;
            set => 
                base.SetProperty<double>(ref this.smallStep, value, Expression.Lambda<Func<double>>(Expression.Property(Expression.Constant(this, typeof(PdfThumbnailsZoomItem)), (MethodInfo) methodof(PdfThumbnailsZoomItem.get_SmallStep)), new ParameterExpression[0]));
        }

        public double LargeStep
        {
            get => 
                this.largeStep;
            set => 
                base.SetProperty<double>(ref this.largeStep, value, Expression.Lambda<Func<double>>(Expression.Property(Expression.Constant(this, typeof(PdfThumbnailsZoomItem)), (MethodInfo) methodof(PdfThumbnailsZoomItem.get_LargeStep)), new ParameterExpression[0]));
        }
    }
}

