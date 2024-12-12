namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Base;
    using DevExpress.Xpf.Docking.Customization;
    using DevExpress.Xpf.Docking.Images;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class TreeItem : BaseCustomizationItem, IDockLayoutManagerListener
    {
        public static readonly DependencyProperty TreeItemStateProperty;

        static TreeItem()
        {
            DependencyPropertyRegistrator<TreeItem> registrator = new DependencyPropertyRegistrator<TreeItem>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.Register<DevExpress.Xpf.Docking.VisualElements.TreeItemState>("TreeItemState", ref TreeItemStateProperty, DevExpress.Xpf.Docking.VisualElements.TreeItemState.Default, null, null);
        }

        public TreeItem()
        {
            base.Focusable = false;
        }

        void IDockLayoutManagerListener.Subscribe(DockLayoutManager manager)
        {
            this.SubscribeEvents(manager);
        }

        void IDockLayoutManagerListener.Unsubscribe(DockLayoutManager manager)
        {
            this.UnsubscribeEvents(manager);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.PartBorder = base.GetTemplateChild("PART_Border") as Border;
            this.PartImage = base.GetTemplateChild("PART_Image") as Image;
            if (this.PartImage != null)
            {
                this.PartImage.Source = ImageHelper.GetImageForItem(base.LayoutItem);
            }
        }

        private void OnDragInfoChanged(object sender, DragInfoChangedEventArgs e)
        {
            if ((e.Info != null) && ReferenceEquals(base.LayoutItem, e.Info.Item))
            {
                this.TreeItemState = DevExpress.Xpf.Docking.VisualElements.TreeItemState.Dragged;
            }
            else
            {
                base.ClearValue(TreeItemStateProperty);
            }
        }

        private void OnLayoutItemSelectionChanged(object sender, LayoutItemSelectionChangedEventArgs e)
        {
            if (e.Selected && ReferenceEquals(e.Item, base.LayoutItem))
            {
                base.BringIntoView();
            }
            if (base.LayoutItem.IsSelected)
            {
                this.TreeItemState = DevExpress.Xpf.Docking.VisualElements.TreeItemState.Selected;
            }
            else
            {
                base.ClearValue(TreeItemStateProperty);
            }
        }

        protected virtual void SubscribeEvents(DockLayoutManager manager)
        {
            manager.CustomizationController.DragInfoChanged += new DragInfoChangedEventHandler(this.OnDragInfoChanged);
            manager.LayoutItemSelectionChanged += new LayoutItemSelectionChangedEventHandler(this.OnLayoutItemSelectionChanged);
        }

        protected virtual void UnsubscribeEvents(DockLayoutManager manager)
        {
            if (!manager.IsDisposing)
            {
                manager.CustomizationController.DragInfoChanged -= new DragInfoChangedEventHandler(this.OnDragInfoChanged);
                manager.LayoutItemSelectionChanged -= new LayoutItemSelectionChangedEventHandler(this.OnLayoutItemSelectionChanged);
            }
        }

        public DevExpress.Xpf.Docking.VisualElements.TreeItemState TreeItemState
        {
            get => 
                (DevExpress.Xpf.Docking.VisualElements.TreeItemState) base.GetValue(TreeItemStateProperty);
            set => 
                base.SetValue(TreeItemStateProperty, value);
        }

        public Border PartBorder { get; private set; }

        public Image PartImage { get; private set; }
    }
}

