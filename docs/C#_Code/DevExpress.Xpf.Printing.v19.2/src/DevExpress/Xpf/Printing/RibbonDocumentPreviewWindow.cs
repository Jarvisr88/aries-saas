namespace DevExpress.Xpf.Printing
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Printing.Native;
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Markup;

    [Obsolete("The RibbonDocumentPreviewWindow is now obsolete. Use the DocumentPreviewWindow class instead and set PreviewControl.CommandBarStyle to CommandBarStyle.Ribbon. More information: https://www.devexpress.com/Support/WhatsNew/DXperience/files/16.1.2.bc.xml#BC3444")]
    public class RibbonDocumentPreviewWindow : DXWindow, IComponentConnector
    {
        public static readonly DependencyProperty ModelProperty = DependencyProperty.Register("Model", typeof(IDocumentPreviewModel), typeof(RibbonDocumentPreviewWindow), new PropertyMetadata(null, new PropertyChangedCallback(RibbonDocumentPreviewWindow.ModelChangedCallback)));
        internal RibbonDocumentPreview ribbonDocumentPreview;
        private bool _contentLoaded;

        public RibbonDocumentPreviewWindow()
        {
            this.InitializeComponent();
            base.Loaded += new RoutedEventHandler(this.OnLoaded);
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/DevExpress.Xpf.Printing.v19.2;component/ribbondocumentpreviewwindow.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        private static void ModelChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((RibbonDocumentPreviewWindow) d).OnModelChanged(e.NewValue as IDocumentPreviewModel);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            base.Loaded -= new RoutedEventHandler(this.OnLoaded);
            Predicate<FrameworkElement> predicate = <>c.<>9__7_0;
            if (<>c.<>9__7_0 == null)
            {
                Predicate<FrameworkElement> local1 = <>c.<>9__7_0;
                predicate = <>c.<>9__7_0 = item => item.GetType() == typeof(ScrollablePageView);
            }
            ScrollablePageView view = (ScrollablePageView) LayoutHelper.FindElement((RibbonDocumentPreviewWindow) sender, predicate);
            if (view != null)
            {
                FocusManager.SetFocusedElement((RibbonDocumentPreviewWindow) sender, view);
            }
        }

        private void OnModelChanged(IDocumentPreviewModel model)
        {
            this.ribbonDocumentPreview.Model = model;
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never)]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            if (connectionId == 1)
            {
                this.ribbonDocumentPreview = (RibbonDocumentPreview) target;
            }
            else
            {
                this._contentLoaded = true;
            }
        }

        [Description("Specifies the model for the Document Preview.")]
        public IDocumentPreviewModel Model
        {
            get => 
                (IDocumentPreviewModel) base.GetValue(ModelProperty);
            set => 
                base.SetValue(ModelProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RibbonDocumentPreviewWindow.<>c <>9 = new RibbonDocumentPreviewWindow.<>c();
            public static Predicate<FrameworkElement> <>9__7_0;

            internal bool <OnLoaded>b__7_0(FrameworkElement item) => 
                item.GetType() == typeof(ScrollablePageView);
        }
    }
}

