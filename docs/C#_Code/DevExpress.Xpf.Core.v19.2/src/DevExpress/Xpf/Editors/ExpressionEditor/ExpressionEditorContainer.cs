namespace DevExpress.Xpf.Editors.ExpressionEditor
{
    using DevExpress.Data;
    using DevExpress.Xpf.Editors.ExpressionEditor.Native;
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;

    public class ExpressionEditorContainer : ContentControl
    {
        public static readonly DependencyProperty CurrentColumnInfoProperty = DependencyProperty.Register("CurrentColumnInfo", typeof(IDataColumnInfo), typeof(ExpressionEditorContainer), new PropertyMetadata(null));
        public static readonly DependencyProperty BindableExpressionProperty = DependencyProperty.Register("BindableExpression", typeof(string), typeof(ExpressionEditorContainer), new PropertyMetadata(string.Empty));

        public ExpressionEditorContainer()
        {
            base.Loaded += new RoutedEventHandler(this.OnLoaded);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            IDataColumnInfo currentColumnInfo = this.CurrentColumnInfo;
            if (currentColumnInfo != null)
            {
                base.Content = ExpressionEditorHelper.GetAutoCompleteExpressionEditorControl(currentColumnInfo, false);
                base.Content ??= new ExpressionEditorControl(currentColumnInfo);
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Closing += new CancelEventHandler(this.OnWindowClosing);
        }

        private void OnWindowClosing(object sender, CancelEventArgs e)
        {
            Window.GetWindow(this).Closing -= new CancelEventHandler(this.OnWindowClosing);
            if (base.Content != null)
            {
                this.BindableExpression = ((ISupportExpressionString) base.Content).GetExpressionString(this.CurrentColumnInfo);
            }
        }

        public IDataColumnInfo CurrentColumnInfo
        {
            get => 
                (IDataColumnInfo) base.GetValue(CurrentColumnInfoProperty);
            set => 
                base.SetValue(CurrentColumnInfoProperty, value);
        }

        public string BindableExpression
        {
            get => 
                (string) base.GetValue(BindableExpressionProperty);
            set => 
                base.SetValue(BindableExpressionProperty, value);
        }
    }
}

