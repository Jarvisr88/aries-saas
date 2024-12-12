namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Mvvm.DataAnnotations;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    public class PopupLinearGradientBrushValueMetadata
    {
        public static void BuildMetadata(MetadataBuilder<PopupLinearGradientBrushValue> builder)
        {
            ParameterExpression expression = Expression.Parameter(typeof(PopupLinearGradientBrushValue), "x");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            builder.Property<Point>(Expression.Lambda<Func<PopupLinearGradientBrushValue, Point>>(Expression.Property(expression, (MethodInfo) methodof(PopupLinearGradientBrushValue.get_StartPoint)), parameters)).DisplayName(EditorLocalizer.GetString(EditorStringId.BrushEditStartPoint)).Description(EditorLocalizer.GetString(EditorStringId.BrushEditStartPointDescription)).TypeConverter<PointCultureConverter>();
            expression = Expression.Parameter(typeof(PopupLinearGradientBrushValue), "x");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            builder.Property<Point>(Expression.Lambda<Func<PopupLinearGradientBrushValue, Point>>(Expression.Property(expression, (MethodInfo) methodof(PopupLinearGradientBrushValue.get_EndPoint)), expressionArray2)).DisplayName(EditorLocalizer.GetString(EditorStringId.BrushEditEndPoint)).Description(EditorLocalizer.GetString(EditorStringId.BrushEditEndPointDescription)).TypeConverter<PointCultureConverter>();
        }
    }
}

