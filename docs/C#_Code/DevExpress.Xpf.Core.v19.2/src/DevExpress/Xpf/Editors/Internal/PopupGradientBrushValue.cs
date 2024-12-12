namespace DevExpress.Xpf.Editors.Internal
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Windows.Media;

    [MetadataType(typeof(PopupGradientBrushValueMetadata))]
    public abstract class PopupGradientBrushValue : PopupBrushValue
    {
        private BrushMappingMode mappingMode;
        private GradientSpreadMethod spreadMethod;

        protected PopupGradientBrushValue()
        {
        }

        protected override void InvalidateBrush()
        {
            GradientBrush brush = (GradientBrush) base.Brush;
            if (brush != null)
            {
                brush.MappingMode = this.MappingMode;
                brush.SpreadMethod = this.spreadMethod;
            }
        }

        protected override void InvalidateParams()
        {
            base.InvalidateParams();
            GradientBrush brush = (GradientBrush) base.Brush;
            this.MappingMode = brush.MappingMode;
            this.SpreadMethod = brush.SpreadMethod;
        }

        public BrushMappingMode MappingMode
        {
            get => 
                this.mappingMode;
            set => 
                base.SetProperty<BrushMappingMode>(ref this.mappingMode, value, Expression.Lambda<Func<BrushMappingMode>>(Expression.Property(Expression.Constant(this, typeof(PopupGradientBrushValue)), (MethodInfo) methodof(PopupGradientBrushValue.get_MappingMode)), new ParameterExpression[0]), new Action(this.InvalidateBrushInternal));
        }

        public GradientSpreadMethod SpreadMethod
        {
            get => 
                this.spreadMethod;
            set => 
                base.SetProperty<GradientSpreadMethod>(ref this.spreadMethod, value, Expression.Lambda<Func<GradientSpreadMethod>>(Expression.Property(Expression.Constant(this, typeof(PopupGradientBrushValue)), (MethodInfo) methodof(PopupGradientBrushValue.get_SpreadMethod)), new ParameterExpression[0]), new Action(this.InvalidateBrushInternal));
        }
    }
}

