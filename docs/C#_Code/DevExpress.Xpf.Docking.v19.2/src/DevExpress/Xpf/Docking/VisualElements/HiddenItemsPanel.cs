namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    [TemplatePart(Name="PART_ListBox", Type=typeof(ItemsControl))]
    public class HiddenItemsPanel : psvControl, IUIElement
    {
        private UIChildren uiChildren = new UIChildren();

        static HiddenItemsPanel()
        {
            new DependencyPropertyRegistrator<HiddenItemsPanel>().OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
        }

        public HiddenItemsPanel()
        {
            base.IsVisibleChanged += new DependencyPropertyChangedEventHandler(this.OnIsVisibleChanged);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.ListBox = LayoutItemsHelper.GetTemplateChild<ItemsControl>(this);
            CompositeCollection composites = new CompositeCollection();
            CollectionContainer newItem = new CollectionContainer();
            newItem.Collection = this.Manager.LayoutController.FixedItems;
            composites.Add(newItem);
            CollectionContainer container2 = new CollectionContainer();
            container2.Collection = this.Manager.LayoutController.HiddenItems;
            composites.Add(container2);
            this.ListBox.ItemsSource = composites;
        }

        private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.Manager != null)
            {
                IUIElement rootUIScope = this.GetRootUIScope();
                IView view = this.Manager.GetView(rootUIScope);
                if (view != null)
                {
                    view.Invalidate();
                }
            }
        }

        IUIElement IUIElement.Scope =>
            DockLayoutManager.GetUIScope(this);

        UIChildren IUIElement.Children
        {
            get
            {
                this.uiChildren ??= new UIChildren();
                return this.uiChildren;
            }
        }

        public DockLayoutManager Manager =>
            DockLayoutManager.GetDockLayoutManager(this);

        protected ItemsControl ListBox { get; private set; }
    }
}

