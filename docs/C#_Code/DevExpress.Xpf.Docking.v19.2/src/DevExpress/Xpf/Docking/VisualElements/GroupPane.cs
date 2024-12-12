namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    [TemplatePart(Name="PART_Content", Type=typeof(GroupPaneContentPresenter))]
    public class GroupPane : psvContentControl
    {
        public static readonly DependencyProperty NoBorderTemplateProperty;
        public static readonly DependencyProperty GroupTemplateProperty;
        public static readonly DependencyProperty GroupBoxTemplateProperty;
        public static readonly DependencyProperty TabbedTemplateProperty;

        static GroupPane()
        {
            DependencyPropertyRegistrator<GroupPane> registrator = new DependencyPropertyRegistrator<GroupPane>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.Register<DataTemplate>("NoBorderTemplate", ref NoBorderTemplateProperty, null, null, null);
            registrator.Register<DataTemplate>("GroupTemplate", ref GroupTemplateProperty, null, null, null);
            registrator.Register<DataTemplate>("GroupBoxTemplate", ref GroupBoxTemplateProperty, null, null, null);
            registrator.Register<DataTemplate>("TabbedTemplate", ref TabbedTemplateProperty, null, null, null);
        }

        public GroupPane()
        {
            base.FocusVisualStyle = null;
            base.IsTabStop = false;
        }

        private void ClearLayoutItemsControlBindings(LayoutItemsControl itemsControl)
        {
            if (itemsControl != null)
            {
                itemsControl.ClearValue(LayoutItemsControl.OrientationProperty);
                psvItemsControl.Clear(itemsControl);
            }
        }

        private void CreateLayoutItemsControlBindings(LayoutItemsControl itemsControl)
        {
            if (itemsControl != null)
            {
                BindingHelper.SetBinding(itemsControl, LayoutItemsControl.OrientationProperty, base.LayoutItem, "Orientation");
                BindingHelper.SetBinding(itemsControl, ItemsControl.ItemsSourceProperty, base.LayoutItem, "ItemsInternal");
                BindingHelper.SetBinding(itemsControl, LayoutItemsControl.LastChildFillProperty, base.LayoutItem, "LastChildFill");
            }
        }

        protected override void EnsureContentElementCore(DependencyObject element)
        {
            base.EnsureContentElementCore(element);
            this.ClearLayoutItemsControlBindings(this.PartLayoutItemsControl);
            this.PartLayoutItemsControl = element as LayoutItemsControl;
            this.CreateLayoutItemsControlBindings(this.PartLayoutItemsControl);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if ((this.PartContent != null) && !LayoutItemsHelper.IsTemplateChild<GroupPane>(this.PartContent, this))
            {
                this.PartContent.Dispose();
            }
            this.PartContent = base.GetTemplateChild("PART_Content") as GroupPaneContentPresenter;
            if (this.PartContent != null)
            {
                this.PartContent.EnsureOwner(this);
            }
        }

        protected override void OnDispose()
        {
            if (this.PartContent != null)
            {
                this.PartContent.Dispose();
                this.PartContent = null;
            }
            if (this.PartLayoutItemsControl != null)
            {
                this.PartLayoutItemsControl.Dispose();
                this.PartLayoutItemsControl = null;
            }
            base.ClearValue(NoBorderTemplateProperty);
            base.ClearValue(GroupTemplateProperty);
            base.ClearValue(NoBorderTemplateProperty);
            base.ClearValue(TabbedTemplateProperty);
            base.OnDispose();
        }

        protected override void OnLayoutItemChanged(BaseLayoutItem item, BaseLayoutItem oldItem)
        {
            base.OnLayoutItemChanged(item, oldItem);
            if (oldItem != null)
            {
                this.ClearLayoutItemsControlBindings(this.PartLayoutItemsControl);
            }
            if (item != null)
            {
                this.CreateLayoutItemsControlBindings(this.PartLayoutItemsControl);
            }
        }

        public DataTemplate NoBorderTemplate
        {
            get => 
                (DataTemplate) base.GetValue(NoBorderTemplateProperty);
            set => 
                base.SetValue(NoBorderTemplateProperty, value);
        }

        public DataTemplate GroupTemplate
        {
            get => 
                (DataTemplate) base.GetValue(GroupTemplateProperty);
            set => 
                base.SetValue(GroupTemplateProperty, value);
        }

        public DataTemplate GroupBoxTemplate
        {
            get => 
                (DataTemplate) base.GetValue(GroupBoxTemplateProperty);
            set => 
                base.SetValue(GroupBoxTemplateProperty, value);
        }

        public DataTemplate TabbedTemplate
        {
            get => 
                (DataTemplate) base.GetValue(TabbedTemplateProperty);
            set => 
                base.SetValue(TabbedTemplateProperty, value);
        }

        public GroupPaneContentPresenter PartContent { get; private set; }

        public LayoutItemsControl PartLayoutItemsControl { get; private set; }
    }
}

