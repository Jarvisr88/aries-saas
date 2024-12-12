namespace DevExpress.Xpf.Editors.Internal
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Media;

    [MetadataType(typeof(PopupRadialGradientBrushValueMetadata))]
    public class PopupRadialGradientBrushValue : PopupGradientBrushValue
    {
        private Point gradientOrigin;
        private Point center;
        private double radiusX;
        private double radiusY;

        protected override void InvalidateBrush()
        {
            base.InvalidateBrush();
            RadialGradientBrush gradientBrush = this.GradientBrush;
            if (gradientBrush != null)
            {
                gradientBrush.GradientOrigin = this.GradientOrigin;
                gradientBrush.Center = this.Center;
                gradientBrush.RadiusX = this.RadiusX;
                gradientBrush.RadiusY = this.RadiusY;
            }
        }

        protected override void InvalidateParams()
        {
            base.InvalidateParams();
            this.GradientOrigin = this.GradientBrush.GradientOrigin;
            this.Center = this.GradientBrush.Center;
            this.RadiusX = this.GradientBrush.RadiusX;
            this.RadiusY = this.GradientBrush.RadiusY;
        }

        public RadialGradientBrush GradientBrush
        {
            get => 
                base.Brush as RadialGradientBrush;
            set => 
                base.Brush = value;
        }

        public Point GradientOrigin
        {
            get => 
                this.gradientOrigin;
            set => 
                base.SetProperty<Point>(ref this.gradientOrigin, value, System.Linq.Expressions.Expression.Lambda<Func<Point>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PopupRadialGradientBrushValue)), (MethodInfo) methodof(PopupRadialGradientBrushValue.get_GradientOrigin)), new ParameterExpression[0]), new Action(this.InvalidateBrushInternal));
        }

        public Point Center
        {
            get => 
                this.center;
            set => 
                base.SetProperty<Point>(ref this.center, value, System.Linq.Expressions.Expression.Lambda<Func<Point>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PopupRadialGradientBrushValue)), (MethodInfo) methodof(PopupRadialGradientBrushValue.get_Center)), new ParameterExpression[0]), new Action(this.InvalidateBrushInternal));
        }

        public double RadiusX
        {
            get => 
                this.radiusX;
            set => 
                base.SetProperty<double>(ref this.radiusX, value, System.Linq.Expressions.Expression.Lambda<Func<double>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PopupRadialGradientBrushValue)), (MethodInfo) methodof(PopupRadialGradientBrushValue.get_RadiusX)), new ParameterExpression[0]), new Action(this.InvalidateBrushInternal));
        }

        public double RadiusY
        {
            get => 
                this.radiusY;
            set => 
                base.SetProperty<double>(ref this.radiusY, value, System.Linq.Expressions.Expression.Lambda<Func<double>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PopupRadialGradientBrushValue)), (MethodInfo) methodof(PopupRadialGradientBrushValue.get_RadiusY)), new ParameterExpression[0]), new Action(this.InvalidateBrushInternal));
        }
    }
}

