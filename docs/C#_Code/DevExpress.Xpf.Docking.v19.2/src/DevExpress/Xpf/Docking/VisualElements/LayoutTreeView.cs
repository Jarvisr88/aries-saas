namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    [TemplatePart(Name="PART_TreeView", Type=typeof(psvTreeView))]
    public class LayoutTreeView : psvControl
    {
        static LayoutTreeView()
        {
            new DependencyPropertyRegistrator<LayoutTreeView>().OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
        }

        public LayoutTreeView()
        {
            base.DefaultStyleKey = typeof(LayoutTreeView);
        }

        protected IEnumerable GetCustomizationItems()
        {
            DockLayoutManager manager = DockLayoutManager.Ensure(this, false);
            return manager?.CustomizationController.CustomizationItems;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.PartItemsControl = base.GetTemplateChild("PART_TreeView") as ItemsControl;
            if ((this.PartItemsControl != null) && (base.Container != null))
            {
                this.PartItemsControl.ItemsSource = base.Container.CustomizationController.CustomizationItems;
            }
        }

        protected override void OnDispose()
        {
            if (this.PartItemsControl != null)
            {
                this.PartItemsControl.ClearValue(ItemsControl.ItemsSourceProperty);
                this.PartItemsControl = null;
            }
            base.OnDispose();
        }

        protected ItemsControl PartItemsControl { get; private set; }
    }
}

