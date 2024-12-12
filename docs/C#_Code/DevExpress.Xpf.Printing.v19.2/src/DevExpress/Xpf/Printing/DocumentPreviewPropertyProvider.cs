namespace DevExpress.Xpf.Printing
{
    using DevExpress.Xpf.DocumentViewer;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    public class DocumentPreviewPropertyProvider : PropertyProvider
    {
        private bool showDocumentMap = true;
        private bool showParametersPanel = true;
        private readonly Action onPropertiesChangedCallback;

        public DocumentPreviewPropertyProvider(Action onPropertiesChangedCallback)
        {
            this.onPropertiesChangedCallback = onPropertiesChangedCallback;
        }

        public bool ShowDocumentMap
        {
            get => 
                this.showDocumentMap;
            set => 
                base.SetProperty<bool>(ref this.showDocumentMap, value, Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(DocumentPreviewPropertyProvider)), (MethodInfo) methodof(DocumentPreviewPropertyProvider.get_ShowDocumentMap)), new ParameterExpression[0]), this.onPropertiesChangedCallback);
        }

        public bool ShowParametersPanel
        {
            get => 
                this.showParametersPanel;
            set => 
                base.SetProperty<bool>(ref this.showParametersPanel, value, Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(DocumentPreviewPropertyProvider)), (MethodInfo) methodof(DocumentPreviewPropertyProvider.get_ShowParametersPanel)), new ParameterExpression[0]), this.onPropertiesChangedCallback);
        }
    }
}

