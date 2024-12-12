namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class DockPaneContentPresenter : DockItemContentPresenter<DockPane, LayoutPanel>
    {
        public static readonly DependencyProperty BarContainerMarginProperty;
        public static readonly DependencyProperty ContentMarginProperty;
        private DataTemplateSelector defaultContentTemplateSelector;

        static DockPaneContentPresenter()
        {
            DependencyPropertyRegistrator<DockPaneContentPresenter> registrator = new DependencyPropertyRegistrator<DockPaneContentPresenter>();
            registrator.Register<Thickness>("BarContainerMargin", ref BarContainerMarginProperty, new Thickness(0.0), null, null);
            registrator.Register<Thickness>("ContentMargin", ref ContentMarginProperty, new Thickness(0.0), null, null);
        }

        protected override bool CanSelectTemplate(LayoutPanel panel) => 
            this._DefaultContentTemplateSelector != null;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if ((this.PartBarContainer != null) && !LayoutItemsHelper.IsTemplateChild<DockPaneContentPresenter>(this.PartBarContainer, this))
            {
                this.PartBarContainer.Dispose();
            }
            this.PartBarContainer = LayoutItemsHelper.GetTemplateChild<DockBarContainerControl>(this);
            if (this.PartBarContainer != null)
            {
                this.PartBarContainer.Margin = this.BarContainerMargin;
            }
            if ((this.PartControl != null) && !LayoutItemsHelper.IsTemplateChild<DockPaneContentPresenter>(this.PartControl, this))
            {
                this.PartControl.Dispose();
            }
            this.PartControl = LayoutItemsHelper.GetTemplateChild<UIElementPresenter>(this);
            if (this.PartControl != null)
            {
                this.PartControl.Margin = this.ContentMargin;
            }
            if ((this.PartLayout != null) && !LayoutItemsHelper.IsTemplateChild<DockPaneContentPresenter>(this.PartLayout, this))
            {
                this.PartLayout.Dispose();
            }
            ScrollViewer templateChild = LayoutItemsHelper.GetTemplateChild<ScrollViewer>(this);
            if (templateChild != null)
            {
                this.PartLayout = templateChild.Content as psvContentPresenter;
            }
            if ((this.PartContent != null) && !LayoutItemsHelper.IsTemplateChild<DockPaneContentPresenter>(this.PartContent, this))
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

        protected override DataTemplate SelectTemplateCore(LayoutPanel panel) => 
            this._DefaultContentTemplateSelector.SelectTemplate(panel, this);

        private DataTemplateSelector _DefaultContentTemplateSelector
        {
            get
            {
                this.defaultContentTemplateSelector ??= new DefaultContentTemplateSelector();
                return this.defaultContentTemplateSelector;
            }
        }

        public Thickness BarContainerMargin
        {
            get => 
                (Thickness) base.GetValue(BarContainerMarginProperty);
            set => 
                base.SetValue(BarContainerMarginProperty, value);
        }

        public Thickness ContentMargin
        {
            get => 
                (Thickness) base.GetValue(ContentMarginProperty);
            set => 
                base.SetValue(ContentMarginProperty, value);
        }

        public bool IsControlItemsHost
        {
            get => 
                (bool) base.GetValue(DockItemContentPresenter<DockPane, LayoutPanel>.IsControlItemsHostProperty);
            set => 
                base.SetValue(DockItemContentPresenter<DockPane, LayoutPanel>.IsControlItemsHostProperty, value);
        }

        public DockBarContainerControl PartBarContainer { get; private set; }

        public psvContentPresenter PartLayout { get; private set; }

        public UIElementPresenter PartControl { get; private set; }

        public psvContentPresenter PartContent { get; private set; }

        private class DefaultContentTemplateSelector : DataTemplateSelector
        {
            public override DataTemplate SelectTemplate(object item, DependencyObject container)
            {
                DockPaneContentPresenter presenter = container as DockPaneContentPresenter;
                LayoutPanel panel = item as LayoutPanel;
                return (((panel == null) || ((presenter == null) || (presenter.Owner == null))) ? null : (panel.IsControlItemsHost ? presenter.Owner.LayoutHostTemplate : (panel.IsDataBound ? presenter.Owner.DataHostTemplate : presenter.Owner.ControlHostTemplate)));
            }
        }
    }
}

