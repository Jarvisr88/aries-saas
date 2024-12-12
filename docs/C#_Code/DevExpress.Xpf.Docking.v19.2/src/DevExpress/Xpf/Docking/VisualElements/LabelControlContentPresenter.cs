namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class LabelControlContentPresenter : BasePanePresenter<LabelControl, LabelItem>
    {
        private DataTemplateSelector defaultContentTemplateSelector;

        protected override bool CanSelectTemplate(LabelItem item) => 
            this._DefaultContentTemplateSelector != null;

        protected override LabelItem ConvertToLogicalItem(object content) => 
            (LayoutItemData.ConvertToBaseLayoutItem(content) as LabelItem) ?? base.ConvertToLogicalItem(content);

        protected override DataTemplate SelectTemplateCore(LabelItem item) => 
            this._DefaultContentTemplateSelector.SelectTemplate(item, this);

        private DataTemplateSelector _DefaultContentTemplateSelector
        {
            get
            {
                this.defaultContentTemplateSelector ??= new DefaultContentTemplateSelector();
                return this.defaultContentTemplateSelector;
            }
        }

        private class DefaultContentTemplateSelector : DataTemplateSelector
        {
            public override DataTemplate SelectTemplate(object item, DependencyObject container)
            {
                LabelControlContentPresenter presenter = container as LabelControlContentPresenter;
                return (((presenter == null) || (presenter.Owner == null)) ? null : presenter.Owner.ContentTemplate);
            }
        }
    }
}

