namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class ControlItemCaptionPresenter : ControlItemElementPresenter
    {
        private DataTemplateSelector defaultCaptionTemplateSelector;

        protected override bool CanSelectTemplate(LayoutControlItem item) => 
            this._DefaultCaptionTemplateSelector != null;

        protected override DataTemplate SelectTemplateCore(LayoutControlItem item) => 
            this._DefaultCaptionTemplateSelector.SelectTemplate(item, this);

        private DataTemplateSelector _DefaultCaptionTemplateSelector
        {
            get
            {
                this.defaultCaptionTemplateSelector ??= new DefaultCaptionTemplateSelector();
                return this.defaultCaptionTemplateSelector;
            }
        }

        private class DefaultCaptionTemplateSelector : DataTemplateSelector
        {
            public override DataTemplate SelectTemplate(object item, DependencyObject container)
            {
                ControlItemCaptionPresenter presenter = container as ControlItemCaptionPresenter;
                return (((presenter == null) || (presenter.Owner == null)) ? null : presenter.Owner.HeaderTemplate);
            }
        }
    }
}

