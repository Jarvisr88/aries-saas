namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.DocumentViewer;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Windows;

    public class DocumentMap : DocumentMapControl
    {
        public static readonly DependencyProperty SelectedItemProperty;

        static DocumentMap()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(DocumentMap), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DependencyPropertyRegistrator<DocumentMap>.New().Register<object>(System.Linq.Expressions.Expression.Lambda<Func<DocumentMap, object>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DocumentMap.get_SelectedItem)), parameters), out SelectedItemProperty, null, frameworkOptions);
        }

        public DocumentMap()
        {
            base.DefaultStyleKey = typeof(DocumentMap);
        }

        protected override DevExpress.Xpf.DocumentViewer.DocumentMapSettings CreateDefaultMapSettings() => 
            new DevExpress.Xpf.Printing.PreviewControl.Native.DocumentMapSettings();

        internal void SetSelectedItem(object selectedItem)
        {
            this.SelectedItem = selectedItem;
        }

        public object SelectedItem
        {
            get => 
                base.GetValue(SelectedItemProperty);
            set => 
                base.SetValue(SelectedItemProperty, value);
        }
    }
}

