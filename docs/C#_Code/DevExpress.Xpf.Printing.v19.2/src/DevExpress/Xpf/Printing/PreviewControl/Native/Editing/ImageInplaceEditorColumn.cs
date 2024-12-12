namespace DevExpress.Xpf.Printing.PreviewControl.Native.Editing
{
    using DevExpress.Mvvm.POCO;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;

    public class ImageInplaceEditorColumn : BaseEditInplaceEditorColumn
    {
        private readonly BaseEditSettings editSettings;
        private readonly VerticalAlignment verticalAlignment;
        private readonly HorizontalAlignment horizontalAlignment;

        protected ImageInplaceEditorColumn(BehaviorProvider behaviorProvider, EditingField editingField, DataTemplateSelector templateSelector, Func<double> getScaleX) : base(behaviorProvider, editingField, templateSelector, getScaleX)
        {
            this.editSettings = new ImageEditSettings();
        }

        public static ImageInplaceEditorColumn Create(BehaviorProvider behaviorProvider, EditingField editingField, DataTemplateSelector templateSelector, Func<double> getScaleX)
        {
            <>c__DisplayClass4_0 class_;
            System.Linq.Expressions.Expression[] expressionArray1 = new System.Linq.Expressions.Expression[] { System.Linq.Expressions.Expression.Field(System.Linq.Expressions.Expression.Constant(class_, typeof(<>c__DisplayClass4_0)), fieldof(<>c__DisplayClass4_0.behaviorProvider)), System.Linq.Expressions.Expression.Field(System.Linq.Expressions.Expression.Constant(class_, typeof(<>c__DisplayClass4_0)), fieldof(<>c__DisplayClass4_0.editingField)), System.Linq.Expressions.Expression.Field(System.Linq.Expressions.Expression.Constant(class_, typeof(<>c__DisplayClass4_0)), fieldof(<>c__DisplayClass4_0.templateSelector)), System.Linq.Expressions.Expression.Field(System.Linq.Expressions.Expression.Constant(class_, typeof(<>c__DisplayClass4_0)), fieldof(<>c__DisplayClass4_0.getScaleX)) };
            return ViewModelSource.Create<ImageInplaceEditorColumn>(System.Linq.Expressions.Expression.Lambda<Func<ImageInplaceEditorColumn>>(System.Linq.Expressions.Expression.New((ConstructorInfo) methodof(ImageInplaceEditorColumn..ctor), (IEnumerable<System.Linq.Expressions.Expression>) expressionArray1), new ParameterExpression[0]));
        }

        protected override Thickness GetBorderThickness()
        {
            float borderWidth = base.EditingField.Brick.BorderWidth;
            return new Thickness(base.EditingField.Brick.Sides.HasFlag(BorderSide.Left) ? ((double) borderWidth) : ((double) 0f), base.EditingField.Brick.Sides.HasFlag(BorderSide.Top) ? ((double) borderWidth) : ((double) 0f), base.EditingField.Brick.Sides.HasFlag(BorderSide.Right) ? ((double) borderWidth) : ((double) 0f), base.EditingField.Brick.Sides.HasFlag(BorderSide.Bottom) ? ((double) borderWidth) : ((double) 0f));
        }

        public override BaseEditSettings EditSettings =>
            this.editSettings;

        public override HorizontalAlignment DefaultHorizontalAlignment =>
            this.horizontalAlignment;

        public override VerticalAlignment DefaultVerticalAlignment =>
            this.verticalAlignment;
    }
}

