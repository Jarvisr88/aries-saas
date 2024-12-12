namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class ControlItemControlPresenter : ControlItemElementPresenter
    {
        private DataTemplateSelector defaultContentTemplateSelector;

        protected override bool CanSelectTemplate(LayoutControlItem item) => 
            this._DefaultContentTemplateSelector != null;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if ((this.PartControl != null) && !LayoutItemsHelper.IsTemplateChild<ControlItemControlPresenter>(this.PartControl, this))
            {
                this.PartControl.Dispose();
            }
            this.PartControl = LayoutItemsHelper.GetTemplateChild<UIElementPresenter>(this);
        }

        protected override void OnDispose()
        {
            if (this.PartControl != null)
            {
                this.PartControl.Dispose();
                this.PartControl = null;
            }
            base.OnDispose();
        }

        protected override DataTemplate SelectTemplateCore(LayoutControlItem item) => 
            this._DefaultContentTemplateSelector.SelectTemplate(item, this);

        private DataTemplateSelector _DefaultContentTemplateSelector
        {
            get
            {
                this.defaultContentTemplateSelector ??= new DefaultContentTemplateSelector();
                return this.defaultContentTemplateSelector;
            }
        }

        public UIElementPresenter PartControl { get; private set; }

        private class DefaultContentTemplateSelector : DataTemplateSelector
        {
            public override DataTemplate SelectTemplate(object item, DependencyObject container)
            {
                ControlItemControlPresenter presenter = container as ControlItemControlPresenter;
                return (((presenter == null) || (presenter.Owner == null)) ? null : presenter.Owner.ContentTemplate);
            }
        }
    }
}

