namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Mvvm.DataAnnotations;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    public class PopupRadialGradientBrushValueMetadata
    {
        public static void BuildMetadata(MetadataBuilder<PopupRadialGradientBrushValue> builder)
        {
            ParameterExpression expression = Expression.Parameter(typeof(PopupRadialGradientBrushValue), "x");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            builder.Property<Point>(Expression.Lambda<Func<PopupRadialGradientBrushValue, Point>>(Expression.Property(expression, (MethodInfo) methodof(PopupRadialGradientBrushValue.get_Center)), parameters)).DisplayName(EditorLocalizer.GetString(EditorStringId.BrushEditCenter)).Description(EditorLocalizer.GetString(EditorStringId.BrushEditCenterDescription)).TypeConverter<PointCultureConverter>();
            expression = Expression.Parameter(typeof(PopupRadialGradientBrushValue), "x");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            builder.Property<Point>(Expression.Lambda<Func<PopupRadialGradientBrushValue, Point>>(Expression.Property(expression, (MethodInfo) methodof(PopupRadialGradientBrushValue.get_GradientOrigin)), expressionArray2)).DisplayName(EditorLocalizer.GetString(EditorStringId.BrushEditGradientOrigin)).Description(EditorLocalizer.GetString(EditorStringId.BrushEditGradientOriginDescription)).TypeConverter<PointCultureConverter>();
            expression = Expression.Parameter(typeof(PopupRadialGradientBrushValue), "x");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            builder.Property<double>(Expression.Lambda<Func<PopupRadialGradientBrushValue, double>>(Expression.Property(expression, (MethodInfo) methodof(PopupRadialGradientBrushValue.get_RadiusX)), expressionArray3)).DisplayName(EditorLocalizer.GetString(EditorStringId.BrushEditRadiusX)).Description(EditorLocalizer.GetString(EditorStringId.BrushEditRadiusXDescription));
            expression = Expression.Parameter(typeof(PopupRadialGradientBrushValue), "x");
            ParameterExpression[] expressionArray4 = new ParameterExpression[] { expression };
            builder.Property<double>(Expression.Lambda<Func<PopupRadialGradientBrushValue, double>>(Expression.Property(expression, (MethodInfo) methodof(PopupRadialGradientBrushValue.get_RadiusY)), expressionArray4)).DisplayName(EditorLocalizer.GetString(EditorStringId.BrushEditRadiusY)).Description(EditorLocalizer.GetString(EditorStringId.BrushEditRadiusYDescription));
        }
    }
}

