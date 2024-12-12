namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class RenderRealContentPresenterContext : RenderControlBaseContext
    {
        public RenderRealContentPresenterContext(RenderRealContentPresenter factory);
        protected internal override void AttachToVisualTree(FrameworkElement root);
        protected internal override void DetachFromVisualTree(FrameworkElement root);
        protected override bool IsContextProperty(string propertyName);

        public object Content { get; set; }

        public DataTemplate ContentTemplate { get; set; }

        public DataTemplateSelector ContentTemplateSelector { get; set; }

        public string ContentStringFormat { get; set; }

        public bool RecognizesAccessKey { get; set; }

        protected override bool UpdateControlPropertyWithCurrentValue { get; }

        private System.Windows.Controls.ContentPresenter ContentPresenter { get; }
    }
}

