namespace DevExpress.Xpf.Editors.Internal
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Media;

    [MetadataType(typeof(PopupLinearGradientBrushValueMetadata))]
    public class PopupLinearGradientBrushValue : PopupGradientBrushValue
    {
        private Point startPoint;
        private Point endPoint;

        protected override void InvalidateBrush()
        {
            base.InvalidateBrush();
            LinearGradientBrush gradientBrush = this.GradientBrush;
            if (gradientBrush != null)
            {
                gradientBrush.StartPoint = this.StartPoint;
                gradientBrush.EndPoint = this.EndPoint;
            }
        }

        protected override void InvalidateParams()
        {
            base.InvalidateParams();
            this.StartPoint = this.GradientBrush.StartPoint;
            this.EndPoint = this.GradientBrush.EndPoint;
        }

        public LinearGradientBrush GradientBrush
        {
            get => 
                base.Brush as LinearGradientBrush;
            set => 
                base.Brush = value;
        }

        public Point StartPoint
        {
            get => 
                this.startPoint;
            set => 
                base.SetProperty<Point>(ref this.startPoint, value, System.Linq.Expressions.Expression.Lambda<Func<Point>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PopupLinearGradientBrushValue)), (MethodInfo) methodof(PopupLinearGradientBrushValue.get_StartPoint)), new ParameterExpression[0]), new Action(this.InvalidateBrushInternal));
        }

        public Point EndPoint
        {
            get => 
                this.endPoint;
            set => 
                base.SetProperty<Point>(ref this.endPoint, value, System.Linq.Expressions.Expression.Lambda<Func<Point>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PopupLinearGradientBrushValue)), (MethodInfo) methodof(PopupLinearGradientBrushValue.get_EndPoint)), new ParameterExpression[0]), new Action(this.InvalidateBrushInternal));
        }
    }
}

