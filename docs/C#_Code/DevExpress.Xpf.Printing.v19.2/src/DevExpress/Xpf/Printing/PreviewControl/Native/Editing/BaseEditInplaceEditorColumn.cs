namespace DevExpress.Xpf.Printing.PreviewControl.Native.Editing
{
    using DevExpress.Mvvm.POCO;
    using DevExpress.Utils;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Printing.Native;
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public abstract class BaseEditInplaceEditorColumn : IInplaceEditorColumn, IDefaultEditorViewInfo
    {
        private readonly DevExpress.Xpf.DocumentViewer.BehaviorProvider behaviorProvider;
        private readonly System.Windows.Media.Brush background;
        private readonly System.Windows.Media.Brush borderBrush;
        private readonly object initialValue;
        private readonly Func<double> getScaleX;
        private readonly DataTemplateSelector editorTemplateSelector;

        event ColumnContentChangedEventHandler IInplaceEditorColumn.ContentChanged
        {
            add
            {
            }
            remove
            {
            }
        }

        protected BaseEditInplaceEditorColumn(DevExpress.Xpf.DocumentViewer.BehaviorProvider behaviorProvider, DevExpress.XtraPrinting.EditingField editingField, DataTemplateSelector templateSelector, Func<double> getScaleX)
        {
            this.behaviorProvider = behaviorProvider;
            this.EditingField = editingField;
            System.Drawing.Color backColor = editingField.Brick.BackColor;
            this.background = new SolidColorBrush(DrawingConverter.FromGdiColor((backColor.A == 0xff) ? backColor : DXColor.Blend(backColor, System.Drawing.Color.White)));
            this.borderBrush = new SolidColorBrush(DrawingConverter.FromGdiColor(editingField.Brick.BorderColor));
            Func<double> func1 = (Func<double>) this;
            if (getScaleX == null)
            {
                func1 = <>c.<>9__30_0;
                if (<>c.<>9__30_0 == null)
                {
                    Func<double> local2 = <>c.<>9__30_0;
                    func1 = <>c.<>9__30_0 = () => ScreenHelper.ScaleX;
                }
            }
            getScaleX.getScaleX = func1;
            this.editorTemplateSelector = templateSelector;
            this.initialValue = editingField.EditValue;
        }

        protected virtual Thickness GetBorderThickness() => 
            new Thickness((this.EditingField.Brick.BorderWidth == 0f) ? 1.0 : ((this.EditingField.Brick.BorderWidth * this.behaviorProvider.ZoomFactor) / this.getScaleX()));

        private Thickness GetEditorPadding()
        {
            double num = this.getScaleX();
            return new Thickness { 
                Left = (this.EditingField.Brick.Padding.Left * this.behaviorProvider.ZoomFactor) / num,
                Right = (this.EditingField.Brick.Padding.Right * this.behaviorProvider.ZoomFactor) / num,
                Top = (this.EditingField.Brick.Padding.Top * this.behaviorProvider.ZoomFactor) / num,
                Bottom = (this.EditingField.Brick.Padding.Bottom * this.behaviorProvider.ZoomFactor) / num
            };
        }

        protected double GetScaleX() => 
            this.getScaleX();

        protected internal virtual void RaiseAppearancePropertiesChanged()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(BaseEditInplaceEditorColumn), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            this.RaisePropertyChanged<BaseEditInplaceEditorColumn, Thickness>(System.Linq.Expressions.Expression.Lambda<Func<BaseEditInplaceEditorColumn, Thickness>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(BaseEditInplaceEditorColumn.get_BorderThickness)), parameters));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(BaseEditInplaceEditorColumn), "d");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            this.RaisePropertyChanged<BaseEditInplaceEditorColumn, Thickness>(System.Linq.Expressions.Expression.Lambda<Func<BaseEditInplaceEditorColumn, Thickness>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(BaseEditInplaceEditorColumn.get_EditorPadding)), expressionArray2));
        }

        public DevExpress.XtraPrinting.EditingField EditingField { get; private set; }

        public abstract BaseEditSettings EditSettings { get; }

        protected DevExpress.Xpf.DocumentViewer.BehaviorProvider BehaviorProvider =>
            this.behaviorProvider;

        public abstract HorizontalAlignment DefaultHorizontalAlignment { get; }

        public abstract VerticalAlignment DefaultVerticalAlignment { get; }

        public System.Windows.Media.Brush Background =>
            this.background;

        public System.Windows.Media.Brush BorderBrush =>
            this.borderBrush;

        public Thickness BorderThickness =>
            this.GetBorderThickness();

        public Thickness EditorPadding =>
            this.GetEditorPadding();

        public object InitialValue =>
            this.initialValue;

        public DataTemplateSelector EditorTemplateSelector =>
            this.editorTemplateSelector;

        ControlTemplate IInplaceEditorColumn.EditTemplate =>
            null;

        ControlTemplate IInplaceEditorColumn.DisplayTemplate =>
            null;

        bool IDefaultEditorViewInfo.HasTextDecorations =>
            false;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BaseEditInplaceEditorColumn.<>c <>9 = new BaseEditInplaceEditorColumn.<>c();
            public static Func<double> <>9__30_0;

            internal double <.ctor>b__30_0() => 
                ScreenHelper.ScaleX;
        }
    }
}

