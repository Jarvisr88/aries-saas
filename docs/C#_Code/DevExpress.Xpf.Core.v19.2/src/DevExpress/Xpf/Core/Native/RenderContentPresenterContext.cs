namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class RenderContentPresenterContext : RenderDecoratorContext
    {
        private DevExpress.Xpf.Core.Native.RenderTemplate actualRenderTemplate;
        private DevExpress.Xpf.Core.Native.RenderTemplate renderTemplate;
        private DevExpress.Xpf.Core.Native.RenderTemplateSelector renderTemplateSelector;
        private DataTemplate contentTemplate;
        private DataTemplate defaultContentTemplate;
        private DataTemplateSelector contentTemplateSelector;
        private bool hasRealPresenter;
        private bool? allowVisualTree;
        private bool? preferRenderTemplate;
        private object content;

        public RenderContentPresenterContext(RenderContentPresenter factory);
        public override void AddChild(FrameworkRenderElementContext child);
        protected virtual void ContentChanged(object oldValue, object newValue);
        protected virtual void ContentTemplateChanged();
        protected virtual void ContentTemplateSelectorChanged();
        protected virtual void ForegroundChanged();
        protected override FrameworkRenderElementContext GetRenderChild(int index);
        protected override void IsTouchEnabledChanged(bool oldValue, bool newValue);
        public virtual void PropagateProperties();
        public override void Release();
        protected virtual void ResetContent();
        protected override void ResetTemplatesAndVisualsInternal();
        protected override void ResetValueCore(string propertyName);
        public bool ShouldUpdateTemplate(object oldValue, object newValue);
        protected virtual void UpdateContentTemplateAndSelector();

        protected override int RenderChildrenCount { get; }

        public RenderContentPresenter Factory { get; }

        public RenderRealContentPresenterContext ContentPresenter { get; }

        public bool? PreferRenderTemplate { get; set; }

        public bool? AllowVisualTree { get; set; }

        public DevExpress.Xpf.Core.Native.RenderTemplate ActualRenderTemplate { get; set; }

        public object Content { get; set; }

        public DevExpress.Xpf.Core.Native.RenderTemplateSelector RenderTemplateSelector { get; set; }

        public DevExpress.Xpf.Core.Native.RenderTemplate RenderTemplate { get; set; }

        public DataTemplateSelector ContentTemplateSelector { get; set; }

        protected internal DataTemplate DefaultContentTemplate { get; set; }

        public DataTemplate ContentTemplate { get; set; }

        public bool HasRealPresenter { get; set; }

        public FrameworkRenderElementContext Context { get; private set; }

        public Namescope InnerNamescope { get; internal set; }
    }
}

