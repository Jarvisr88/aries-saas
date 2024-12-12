namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class Chrome : ContentPresenter, IChrome, IElementOwner, IChromeSlaveMaster
    {
        public static readonly DependencyProperty ForegroundProperty;
        public static readonly DependencyProperty FontSizeProperty;
        public static readonly DependencyProperty FontFamilyProperty;
        public static readonly DependencyProperty FontStretchProperty;
        public static readonly DependencyProperty FontStyleProperty;
        public static readonly DependencyProperty FontWeightProperty;
        private static readonly DataTemplate emptyTemplate;
        private readonly ChromeSlave slave;
        private object renderContent;
        private DevExpress.Xpf.Core.Native.RenderTemplate renderTemplate;
        private DevExpress.Xpf.Core.Native.RenderTemplateSelector renderTemplateSelector;
        private bool recognizesAccessKey;
        private object content;
        private string contentStringFormat;
        private bool? useDefaultStringTemplate;

        static Chrome();
        public Chrome();
        protected override Size ArrangeOverride(Size finalSize);
        protected override DataTemplate ChooseTemplate();
        void IChrome.AddChild(FrameworkElement element);
        bool IChrome.CaptureMouse(FrameworkRenderElementContext context);
        void IChrome.GoToState(string stateName);
        void IChrome.InvalidateArrange();
        void IChrome.InvalidateMeasure();
        void IChrome.InvalidateVisual();
        void IChrome.ReleaseMouseCapture(FrameworkRenderElementContext context);
        void IChrome.RemoveChild(FrameworkElement element);
        protected virtual void ForegroundChanged(Brush newValue);
        protected override Visual GetVisualChild(int index);
        protected override HitTestResult HitTestCore(PointHitTestParameters hitTestParameters);
        protected virtual FrameworkRenderElementContext InitializeContext();
        protected void InvalidateContext();
        protected internal void InvalidateMeasureAndVisual();
        protected override Size MeasureOverride(Size availableSize);
        protected override void OnChildDesiredSizeChanged(UIElement child);
        protected virtual void OnContentChanged(object oldValue, object newValue);
        protected override void OnContentStringFormatChanged(string oldContentStringFormat, string newContentStringFormat);
        protected virtual void OnRecognizesAccessKeyChanged(bool oldValue, bool newValue);
        protected override void OnRender(DrawingContext dc);
        protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved);
        protected virtual void ReleaseContext(FrameworkRenderElementContext context);
        protected virtual void RenderContentChanged(object oldContent, object newContent);
        protected virtual void SvgStateChanged(string newValue);
        protected virtual void UpdateInnerPresenters();
        protected virtual void UpdateSubTree();

        ChromeSlave IChromeSlaveMaster.Slave { get; }

        public bool EnableContentPresenterLogic { get; set; }

        public object Content { get; set; }

        public bool RecognizesAccessKey { get; set; }

        public string ContentStringFormat { get; set; }

        public Brush Foreground { get; set; }

        public double FontSize { get; set; }

        public System.Windows.Media.FontFamily FontFamily { get; set; }

        public System.Windows.FontStretch FontStretch { get; set; }

        public System.Windows.FontStyle FontStyle { get; set; }

        public System.Windows.FontWeight FontWeight { get; set; }

        public DevExpress.Xpf.Core.Native.RenderTemplate RenderTemplate { get; set; }

        public DevExpress.Xpf.Core.Native.RenderTemplateSelector RenderTemplateSelector { get; set; }

        public object RenderContent { get; set; }

        protected override IEnumerator LogicalChildren { get; }

        protected override int VisualChildrenCount { get; }

        public Visual RealChild { get; }

        protected internal bool UseDefaultStringTemplate { get; }

        public INamescope Namescope { get; }

        public IElementHost ElementHost { get; }

        FrameworkRenderElementContext IChrome.Root { get; }

        double IChrome.DpiScale { get; }

        FrameworkElement IElementOwner.Child { get; }

        bool IChrome.IsLoaded { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly Chrome.<>c <>9;
            public static Func<string, bool> <>9__72_0;

            static <>c();
            internal void <.cctor>b__7_0(DataTemplate x);
            internal void <.cctor>b__7_1(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__7_2(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal bool <get_UseDefaultStringTemplate>b__72_0(string x);
        }
    }
}

