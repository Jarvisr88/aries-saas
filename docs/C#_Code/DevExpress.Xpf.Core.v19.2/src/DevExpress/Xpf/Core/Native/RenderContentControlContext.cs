namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class RenderContentControlContext : RenderControlContext
    {
        private RenderTemplateSelector renderContentTemplateSelector;
        private RenderTemplate renderContentTemplate;
        private object content;
        private DataTemplate contentTemplate;
        private DataTemplateSelector contentTemplateSelector;
        private HorizontalAlignment? hca;
        private VerticalAlignment? vca;
        private Thickness? padding;
        private bool? preferRenderTemplate;

        public RenderContentControlContext(RenderContentControl factory);
        protected virtual void ContentTemplateChanged();
        protected virtual void ContentTemplateSelectorChanged();
        protected virtual void HorizontalContentAlignmentChanged();
        protected override void IsTouchEnabledChanged(bool oldValue, bool newValue);
        protected virtual void OnContentChanged(object oldValue, object newValue);
        protected virtual void PaddingChanged();
        protected virtual void UpdateContentState();
        public override void UpdateStates();
        protected virtual void VerticalContentAlignmentChanged();

        public bool? PreferRenderTemplate { get; set; }

        public HorizontalAlignment? HorizontalContentAlignment { get; set; }

        public VerticalAlignment? VerticalContentAlignment { get; set; }

        public object Content { get; set; }

        public RenderTemplateSelector RenderContentTemplateSelector { get; set; }

        public RenderTemplate RenderContentTemplate { get; set; }

        public DataTemplateSelector ContentTemplateSelector { get; set; }

        public DataTemplate ContentTemplate { get; set; }

        public Thickness? Padding { get; set; }

        public RenderContentControl Factory { get; }

        protected RenderContentPresenterContext ContentPresenter { get; }
    }
}

