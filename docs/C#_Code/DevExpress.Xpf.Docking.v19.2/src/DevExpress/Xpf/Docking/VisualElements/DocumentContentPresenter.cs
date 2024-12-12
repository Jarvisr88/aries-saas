namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class DocumentContentPresenter : DockItemContentPresenter<BaseDocument, DocumentPanel>
    {
        private DataTemplateSelector defaultContentTemplateSelector;

        protected override bool CanSelectTemplate(DocumentPanel document) => 
            this._DefaultContentTemplateSelector != null;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if ((this.PartBarContainer != null) && !LayoutItemsHelper.IsTemplateChild<DocumentContentPresenter>(this.PartBarContainer, this))
            {
                this.PartBarContainer.Dispose();
            }
            this.PartBarContainer = LayoutItemsHelper.GetTemplateChild<DockBarContainerControl>(this);
            if ((this.PartControl != null) && !LayoutItemsHelper.IsTemplateChild<DocumentContentPresenter>(this.PartControl, this))
            {
                this.PartControl.Dispose();
            }
            this.PartControl = LayoutItemsHelper.GetTemplateChild<UIElementPresenter>(this);
            if ((this.PartLayout != null) && !LayoutItemsHelper.IsTemplateChild<DocumentContentPresenter>(this.PartLayout, this))
            {
                this.PartLayout.Dispose();
            }
            ScrollViewer templateChild = LayoutItemsHelper.GetTemplateChild<ScrollViewer>(this);
            if (templateChild != null)
            {
                this.PartLayout = templateChild.Content as psvContentPresenter;
            }
            if ((this.PartContent != null) && !LayoutItemsHelper.IsTemplateChild<DocumentContentPresenter>(this.PartContent, this))
            {
                this.PartContent.Dispose();
            }
            this.PartContent = LayoutItemsHelper.GetTemplateChild<psvContentPresenter>(this, false);
        }

        protected override void OnDispose()
        {
            if (this.PartBarContainer != null)
            {
                this.PartBarContainer.Dispose();
                this.PartBarContainer = null;
            }
            if (this.PartControl != null)
            {
                this.PartControl.Dispose();
                this.PartControl = null;
            }
            if (this.PartLayout != null)
            {
                this.PartLayout.Dispose();
                this.PartLayout = null;
            }
            if (this.PartContent != null)
            {
                this.PartContent.Dispose();
                this.PartContent = null;
            }
            base.OnDispose();
        }

        protected override DataTemplate SelectTemplateCore(DocumentPanel document) => 
            this._DefaultContentTemplateSelector.SelectTemplate(document, this);

        private DataTemplateSelector _DefaultContentTemplateSelector
        {
            get
            {
                this.defaultContentTemplateSelector ??= new DefaultContentTemplateSelector();
                return this.defaultContentTemplateSelector;
            }
        }

        public DockBarContainerControl PartBarContainer { get; private set; }

        public UIElementPresenter PartControl { get; private set; }

        public psvContentPresenter PartLayout { get; private set; }

        public psvContentPresenter PartContent { get; private set; }

        private class DefaultContentTemplateSelector : DataTemplateSelector
        {
            public override DataTemplate SelectTemplate(object item, DependencyObject container)
            {
                DocumentContentPresenter presenter = container as DocumentContentPresenter;
                DocumentPanel panel = item as DocumentPanel;
                return (((panel == null) || ((presenter == null) || (presenter.Owner == null))) ? null : (panel.IsControlItemsHost ? presenter.Owner.LayoutHostTemplate : (panel.IsDataBound ? presenter.Owner.DataHostTemplate : presenter.Owner.ControlHostTemplate)));
            }
        }
    }
}

