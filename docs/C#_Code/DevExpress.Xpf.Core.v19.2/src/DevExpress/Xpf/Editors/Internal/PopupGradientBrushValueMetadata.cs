namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Mvvm.DataAnnotations;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    public class PopupGradientBrushValueMetadata
    {
        public static void BuildMetadata(MetadataBuilder<PopupGradientBrushValue> builder)
        {
            ParameterExpression expression = Expression.Parameter(typeof(PopupGradientBrushValue), "x");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            builder.Property<BrushMappingMode>(Expression.Lambda<Func<PopupGradientBrushValue, BrushMappingMode>>(Expression.Property(expression, (MethodInfo) methodof(PopupGradientBrushValue.get_MappingMode)), parameters)).DisplayName(EditorLocalizer.GetString(EditorStringId.BrushEditMappingMode)).Description(EditorLocalizer.GetString(EditorStringId.BrushEditMappingModeDescription));
            expression = Expression.Parameter(typeof(PopupGradientBrushValue), "x");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            builder.Property<GradientSpreadMethod>(Expression.Lambda<Func<PopupGradientBrushValue, GradientSpreadMethod>>(Expression.Property(expression, (MethodInfo) methodof(PopupGradientBrushValue.get_SpreadMethod)), expressionArray2)).DisplayName(EditorLocalizer.GetString(EditorStringId.BrushEditSpreadMethod)).Description(EditorLocalizer.GetString(EditorStringId.BrushEditSpreadMethodDescription));
        }
    }
}

