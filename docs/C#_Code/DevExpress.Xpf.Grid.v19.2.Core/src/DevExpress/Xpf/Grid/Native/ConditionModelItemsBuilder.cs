namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Mvvm.UI.Native.ViewGenerator.Model;
    using DevExpress.Xpf.Core.ConditionalFormattingManager;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Windows;

    public class ConditionModelItemsBuilder : IConditionModelItemsBuilder
    {
        private IEditingContext context;

        public ConditionModelItemsBuilder(IEditingContext context)
        {
            this.context = context;
        }

        public IModelItem BuildColorScaleCondition(ColorScaleEditUnit unit, IModelItem source)
        {
            IModelItem editableCondition = this.GetEditableCondition(source, typeof(ColorScaleFormatCondition));
            this.SetIndicatorProperties(editableCondition, unit);
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(ColorScaleEditUnit), "x");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            unit.SetModelItemProperty<ColorScaleEditUnit, ColorScaleFormat>(editableCondition, ColorScaleFormatCondition.FormatProperty, System.Linq.Expressions.Expression.Lambda<Func<ColorScaleEditUnit, ColorScaleFormat>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(ColorScaleEditUnit.get_Format)), parameters));
            return editableCondition;
        }

        public IModelItem BuildCondition(ConditionEditUnit unit, IModelItem source)
        {
            IModelItem editableCondition = this.GetEditableCondition(source, typeof(FormatCondition));
            this.SetExpressionConditionProperties(editableCondition, unit);
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(ConditionEditUnit), "x");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            unit.SetModelItemProperty<ConditionEditUnit, object>(editableCondition, FormatCondition.Value1Property, System.Linq.Expressions.Expression.Lambda<Func<ConditionEditUnit, object>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(ConditionEditUnit.get_Value1)), parameters));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(ConditionEditUnit), "x");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            unit.SetModelItemProperty<ConditionEditUnit, object>(editableCondition, FormatCondition.Value2Property, System.Linq.Expressions.Expression.Lambda<Func<ConditionEditUnit, object>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(ConditionEditUnit.get_Value2)), expressionArray2));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(ConditionEditUnit), "x");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            unit.SetModelItemProperty<ConditionEditUnit, ConditionRule>(editableCondition, FormatCondition.ValueRuleProperty, System.Linq.Expressions.Expression.Lambda<Func<ConditionEditUnit, ConditionRule>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(ConditionEditUnit.get_ValueRule)), expressionArray3));
            return editableCondition;
        }

        public IModelItem BuildDataBarCondition(DataBarEditUnit unit, IModelItem source)
        {
            IModelItem editableCondition = this.GetEditableCondition(source, typeof(DataBarFormatCondition));
            this.SetIndicatorProperties(editableCondition, unit);
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(DataBarEditUnit), "x");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            unit.SetModelItemProperty<DataBarEditUnit, DataBarFormat>(editableCondition, DataBarFormatCondition.FormatProperty, System.Linq.Expressions.Expression.Lambda<Func<DataBarEditUnit, DataBarFormat>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DataBarEditUnit.get_Format)), parameters));
            return editableCondition;
        }

        public IModelItem BuildDataUpdateFormatCondition(AnimationEditUnit unit, IModelItem source)
        {
            IModelItem editableCondition = this.GetEditableCondition(source, typeof(DataUpdateFormatCondition));
            this.SetExpressionConditionProperties(editableCondition, unit);
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(AnimationEditUnit), "x");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            unit.SetModelItemProperty<AnimationEditUnit, DataUpdateRule>(editableCondition, DataUpdateFormatCondition.RuleProperty, System.Linq.Expressions.Expression.Lambda<Func<AnimationEditUnit, DataUpdateRule>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(AnimationEditUnit.get_Rule)), parameters));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(AnimationEditUnit), "x");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            unit.SetModelItemProperty<AnimationEditUnit, DataUpdateAnimationSettings>(editableCondition, DataUpdateFormatCondition.AnimationSettingsProperty, System.Linq.Expressions.Expression.Lambda<Func<AnimationEditUnit, DataUpdateAnimationSettings>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(AnimationEditUnit.get_AnimationSettings)), expressionArray2));
            return editableCondition;
        }

        public IModelItem BuildIconSetCondition(IconSetEditUnit unit, IModelItem source)
        {
            IModelItem editableCondition = this.GetEditableCondition(source, typeof(IconSetFormatCondition));
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(IconSetEditUnit), "x");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            unit.SetModelItemProperty<IconSetEditUnit, IconSetFormat>(editableCondition, IconSetFormatCondition.FormatProperty, System.Linq.Expressions.Expression.Lambda<Func<IconSetEditUnit, IconSetFormat>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(IconSetEditUnit.get_Format)), parameters));
            this.SetIndicatorProperties(editableCondition, unit);
            return editableCondition;
        }

        public IModelItem BuildTopBottomCondition(TopBottomEditUnit unit, IModelItem source)
        {
            IModelItem editableCondition = this.GetEditableCondition(source, typeof(TopBottomRuleFormatCondition));
            this.SetExpressionConditionProperties(editableCondition, unit);
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(TopBottomEditUnit), "x");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            unit.SetModelItemProperty<TopBottomEditUnit, TopBottomRule>(editableCondition, TopBottomRuleFormatCondition.RuleProperty, System.Linq.Expressions.Expression.Lambda<Func<TopBottomEditUnit, TopBottomRule>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(TopBottomEditUnit.get_Rule)), parameters));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(TopBottomEditUnit), "x");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            unit.SetModelItemProperty<TopBottomEditUnit, double>(editableCondition, TopBottomRuleFormatCondition.ThresholdProperty, System.Linq.Expressions.Expression.Lambda<Func<TopBottomEditUnit, double>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(TopBottomEditUnit.get_Threshold)), expressionArray2));
            return editableCondition;
        }

        public IModelItem BuildUniqueDuplicateCondition(UniqueDuplicateEditUnit unit, IModelItem source)
        {
            IModelItem editableCondition = this.GetEditableCondition(source, typeof(UniqueDuplicateRuleFormatCondition));
            this.SetExpressionConditionProperties(editableCondition, unit);
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(UniqueDuplicateEditUnit), "x");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            unit.SetModelItemProperty<UniqueDuplicateEditUnit, UniqueDuplicateRule>(editableCondition, UniqueDuplicateRuleFormatCondition.RuleProperty, System.Linq.Expressions.Expression.Lambda<Func<UniqueDuplicateEditUnit, UniqueDuplicateRule>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(UniqueDuplicateEditUnit.get_Rule)), parameters));
            return editableCondition;
        }

        private IModelItem GetEditableCondition(IModelItem source, Type type) => 
            source ?? this.context.CreateItem(type);

        private void SetBaseProperties(IModelItem item, BaseEditUnit unit)
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(BaseEditUnit), "x");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            unit.SetModelItemProperty<BaseEditUnit, string>(item, FormatConditionBase.FieldNameProperty, System.Linq.Expressions.Expression.Lambda<Func<BaseEditUnit, string>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(BaseEditUnit.get_FieldName)), parameters));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(BaseEditUnit), "x");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            unit.SetModelItemProperty<BaseEditUnit, string>(item, FormatConditionBase.ExpressionProperty, System.Linq.Expressions.Expression.Lambda<Func<BaseEditUnit, string>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(BaseEditUnit.get_Expression)), expressionArray2));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(BaseEditUnit), "x");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            unit.SetModelItemProperty<BaseEditUnit, string>(item, FormatConditionBase.PredefinedFormatNameProperty, System.Linq.Expressions.Expression.Lambda<Func<BaseEditUnit, string>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(BaseEditUnit.get_PredefinedFormatName)), expressionArray3));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(BaseEditUnit), "x");
            ParameterExpression[] expressionArray4 = new ParameterExpression[] { expression };
            unit.SetModelItemProperty<BaseEditUnit, bool>(item, FormatConditionBase.IsEnabledProperty, System.Linq.Expressions.Expression.Lambda<Func<BaseEditUnit, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(BaseEditUnit.get_IsEnabled)), expressionArray4));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(BaseEditUnit), "x");
            ParameterExpression[] expressionArray5 = new ParameterExpression[] { expression };
            unit.SetModelItemProperty<BaseEditUnit, bool>(item, FormatConditionBase.ApplyToRowProperty, System.Linq.Expressions.Expression.Lambda<Func<BaseEditUnit, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(BaseEditUnit.get_ApplyToRow)), expressionArray5));
            Freezable format = unit.GetFormat();
            if ((format != null) && !Equals(format, item.Properties["Format"].ComputedValue))
            {
                item.Properties[FormatConditionBase.PredefinedFormatNameProperty.Name].ClearValue();
            }
        }

        private void SetExpressionConditionProperties(IModelItem item, ExpressionEditUnit unit)
        {
            this.SetBaseProperties(item, unit);
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(ExpressionEditUnit), "x");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            unit.SetModelItemProperty<ExpressionEditUnit, Format>(item, ExpressionConditionBase.FormatProperty, System.Linq.Expressions.Expression.Lambda<Func<ExpressionEditUnit, Format>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(ExpressionEditUnit.get_Format)), parameters));
        }

        private void SetIndicatorProperties(IModelItem item, IndicatorEditUnit unit)
        {
            this.SetBaseProperties(item, unit);
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(IndicatorEditUnit), "x");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            unit.SetModelItemProperty<IndicatorEditUnit, object>(item, IndicatorFormatConditionBase.MinValueProperty, System.Linq.Expressions.Expression.Lambda<Func<IndicatorEditUnit, object>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(IndicatorEditUnit.get_MinValue)), parameters));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(IndicatorEditUnit), "x");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            unit.SetModelItemProperty<IndicatorEditUnit, object>(item, IndicatorFormatConditionBase.MaxValueProperty, System.Linq.Expressions.Expression.Lambda<Func<IndicatorEditUnit, object>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(IndicatorEditUnit.get_MaxValue)), expressionArray2));
        }
    }
}

