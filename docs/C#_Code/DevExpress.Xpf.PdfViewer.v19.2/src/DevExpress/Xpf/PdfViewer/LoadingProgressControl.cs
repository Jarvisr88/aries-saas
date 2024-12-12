namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Xpf.Utils;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class LoadingProgressControl : Control
    {
        public static readonly DependencyProperty MessageProperty;
        public static readonly DependencyProperty ProgressProperty;
        public static readonly DependencyProperty TotalProgressProperty;

        static LoadingProgressControl()
        {
            Type ownerType = typeof(LoadingProgressControl);
            MessageProperty = DependencyPropertyManager.Register("Message", typeof(string), ownerType, new FrameworkPropertyMetadata(PdfViewerLocalizer.GetString(PdfViewerStringId.LoadingDocumentCaption)));
            ProgressProperty = DependencyPropertyManager.Register("Progress", typeof(double), ownerType, new FrameworkPropertyMetadata(0.0));
            TotalProgressProperty = DependencyPropertyManager.Register("TotalProgress", typeof(double), ownerType, new FrameworkPropertyMetadata(1.0));
        }

        public LoadingProgressControl()
        {
            base.DefaultStyleKey = typeof(LoadingProgressControl);
        }

        public string Message
        {
            get => 
                (string) base.GetValue(MessageProperty);
            set => 
                base.SetValue(MessageProperty, value);
        }

        public double Progress
        {
            get => 
                (double) base.GetValue(ProgressProperty);
            set => 
                base.SetValue(ProgressProperty, value);
        }

        public double TotalProgress
        {
            get => 
                (double) base.GetValue(TotalProgressProperty);
            set => 
                base.SetValue(TotalProgressProperty, value);
        }
    }
}

