namespace DevExpress.Xpf.Printing.PreviewControl.Native.Editing
{
    using DevExpress.Mvvm.POCO;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.Editors.Native;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Printing.Native;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class TextInplaceEditorColumn : BaseEditInplaceEditorColumn
    {
        private readonly Brush foreground;
        private readonly System.Windows.Media.FontFamily fontFamily;
        private readonly BaseEditSettings editSettings;
        private readonly VerticalAlignment verticalAlignment;
        private readonly HorizontalAlignment horizontalAlignment;

        protected TextInplaceEditorColumn(BehaviorProvider behaviorProvider, TextEditingField editingField, DataTemplateSelector templateSelector, Func<double> getScaleX) : base(behaviorProvider, editingField, templateSelector, getScaleX)
        {
            SpinEditSettings settings2;
            this.foreground = new SolidColorBrush(DrawingConverter.FromGdiColor(editingField.Brick.Style.ForeColor));
            this.fontFamily = new System.Windows.Media.FontFamily(editingField.Brick.Style.Font.FontFamily.Name);
            TextBrick brick = editingField.Brick as TextBrick;
            if (brick != null)
            {
                DevExpress.XtraPrinting.TextAlignment textAlignment = TextAlignmentConverter.ToTextAlignment(brick.HorzAlignment, brick.VertAlignment);
                this.horizontalAlignment = DrawingConverter.GetHorizontalAlignment(textAlignment);
                this.verticalAlignment = DrawingConverter.GetVerticalAlignment(textAlignment);
            }
            if (PSNativeMethods.IsNumericalType(this.EditingField.EditValue.GetType()))
            {
                settings2 = new SpinEditSettings();
            }
            else
            {
                TextEditSettings settings1 = new TextEditSettings();
                settings1.AcceptsReturn = true;
                settings1.TextWrapping = TextWrapping.Wrap;
                settings2 = (SpinEditSettings) settings1;
            }
            this.editSettings = settings2;
            this.editSettings.VerticalContentAlignment = DrawingConverter.GetVerticalAlignment(editingField.Brick.Style.TextAlignment);
            this.editSettings.HorizontalContentAlignment = EditSettingsHorizontalAlignmentHelper.GetEditSettingsHorizontalAlignment(DrawingConverter.GetHorizontalAlignment(editingField.Brick.Style.TextAlignment));
        }

        public static TextInplaceEditorColumn Create(BehaviorProvider behaviorProvider, TextEditingField editingField, DataTemplateSelector templateSelector, Func<double> getScaleX)
        {
            <>c__DisplayClass21_0 class_;
            System.Linq.Expressions.Expression[] expressionArray1 = new System.Linq.Expressions.Expression[] { System.Linq.Expressions.Expression.Field(System.Linq.Expressions.Expression.Constant(class_, typeof(<>c__DisplayClass21_0)), fieldof(<>c__DisplayClass21_0.behaviorProvider)), System.Linq.Expressions.Expression.Field(System.Linq.Expressions.Expression.Constant(class_, typeof(<>c__DisplayClass21_0)), fieldof(<>c__DisplayClass21_0.editingField)), System.Linq.Expressions.Expression.Field(System.Linq.Expressions.Expression.Constant(class_, typeof(<>c__DisplayClass21_0)), fieldof(<>c__DisplayClass21_0.templateSelector)), System.Linq.Expressions.Expression.Field(System.Linq.Expressions.Expression.Constant(class_, typeof(<>c__DisplayClass21_0)), fieldof(<>c__DisplayClass21_0.getScaleX)) };
            return ViewModelSource.Create<TextInplaceEditorColumn>(System.Linq.Expressions.Expression.Lambda<Func<TextInplaceEditorColumn>>(System.Linq.Expressions.Expression.New((ConstructorInfo) methodof(TextInplaceEditorColumn..ctor), (IEnumerable<System.Linq.Expressions.Expression>) expressionArray1), new ParameterExpression[0]));
        }

        protected internal override void RaiseAppearancePropertiesChanged()
        {
            base.RaiseAppearancePropertiesChanged();
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(TextInplaceEditorColumn), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            this.RaisePropertyChanged<TextInplaceEditorColumn, double>(System.Linq.Expressions.Expression.Lambda<Func<TextInplaceEditorColumn, double>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(TextInplaceEditorColumn.get_FontSize)), parameters));
        }

        public TextEditingField EditingField =>
            (TextEditingField) base.EditingField;

        public bool IsMultiValue =>
            this.EditingField.EditValue.GetType().IsArray && !(this.EditingField.EditValue is string);

        public Brush Foreground =>
            this.foreground;

        public double FontSize =>
            ((base.BehaviorProvider.ZoomFactor * this.EditingField.Brick.Style.Font.SizeInPoints) * 96.0) / 72.0;

        public System.Windows.Media.FontFamily FontFamily =>
            this.fontFamily;

        public override BaseEditSettings EditSettings =>
            this.editSettings;

        public override HorizontalAlignment DefaultHorizontalAlignment =>
            this.horizontalAlignment;

        public override VerticalAlignment DefaultVerticalAlignment =>
            this.verticalAlignment;
    }
}

