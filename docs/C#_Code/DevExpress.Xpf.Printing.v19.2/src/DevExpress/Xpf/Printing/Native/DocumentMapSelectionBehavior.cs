namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Xpf.Printing;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Windows;

    public class DocumentMapSelectionBehavior : DevExpress.Mvvm.UI.Interactivity.EventTrigger
    {
        public static readonly DependencyProperty ModelProperty = DependencyPropertyManager.Register("Model", typeof(IDocumentPreviewModel), typeof(DocumentMapSelectionBehavior), new PropertyMetadata(null));

        public DocumentMapSelectionBehavior() : base("SelectedItemChanged")
        {
        }

        private void Invoke(object sender, object parameter)
        {
            if (this.Model != null)
            {
                object obj2 = new DocumentMapSelectionChangedEventArgsConverter().Convert(sender, parameter);
                this.Model.DocumentMapSelectedNode = obj2 as DocumentMapTreeViewNode;
            }
        }

        protected override void OnEvent(object sender, object eventArgs)
        {
            base.OnEvent(sender, eventArgs);
            this.Invoke(sender, eventArgs);
        }

        public IDocumentPreviewModel Model
        {
            get => 
                (IDocumentPreviewModel) base.GetValue(ModelProperty);
            set => 
                base.SetValue(ModelProperty, value);
        }
    }
}

