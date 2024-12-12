namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;

    [TemplatePart(Name="AvailableItemsElement", Type=typeof(LayoutControlAvailableItemsControl))]
    public class LayoutControlCustomizationControl : ControlBase
    {
        public static readonly DependencyProperty AvailableItemsProperty = DependencyProperty.Register("AvailableItems", typeof(FrameworkElements), typeof(LayoutControlCustomizationControl), null);
        public static readonly DependencyProperty AvailableItemsUIVisibilityProperty = DependencyProperty.Register("AvailableItemsUIVisibility", typeof(Visibility), typeof(LayoutControlCustomizationControl), null);
        public static readonly DependencyProperty NewItemsInfoProperty = DependencyProperty.Register("NewItemsInfo", typeof(LayoutControlNewItemsInfo), typeof(LayoutControlCustomizationControl), null);
        public static readonly DependencyProperty NewItemsUIVisibilityProperty = DependencyProperty.Register("NewItemsUIVisibility", typeof(Visibility), typeof(LayoutControlCustomizationControl), null);
        private const string AvailableItemsElementName = "AvailableItemsElement";

        public event Action<FrameworkElement> DeleteAvailableItem;

        public event EventHandler IsAvailableItemsListOpenChanged;

        public event EventHandler<LayoutControlStartDragAndDropEventArgs<FrameworkElement>> StartAvailableItemDragAndDrop;

        public event EventHandler<LayoutControlStartDragAndDropEventArgs<LayoutControlNewItemInfo>> StartNewItemDragAndDrop;

        public LayoutControlCustomizationControl()
        {
            base.DefaultStyleKey = typeof(LayoutControlCustomizationControl);
            base.FocusVisualStyle = null;
        }

        public override void OnApplyTemplate()
        {
            if (this.AvailableItemsElement != null)
            {
                this.AvailableItemsElement.DeleteAvailableItem -= new Action<FrameworkElement>(this.OnDeleteAvailableItem);
                this.AvailableItemsElement.IsListOpenChanged -= new EventHandler(this.OnIsAvailableItemsListOpenChanged);
                this.AvailableItemsElement.StartAvailableItemDragAndDrop -= new EventHandler<LayoutControlStartDragAndDropEventArgs<FrameworkElement>>(this.OnAvailableItemsStartAvailableItemDragAndDrop);
                this.AvailableItemsElement.StartNewItemDragAndDrop -= new EventHandler<LayoutControlStartDragAndDropEventArgs<LayoutControlNewItemInfo>>(this.OnAvailableItemsStartNewItemDragAndDrop);
            }
            base.OnApplyTemplate();
            this.AvailableItemsElement = base.GetTemplateChild("AvailableItemsElement") as LayoutControlAvailableItemsControl;
            if (this.AvailableItemsElement != null)
            {
                this.AvailableItemsElement.DeleteAvailableItem += new Action<FrameworkElement>(this.OnDeleteAvailableItem);
                this.AvailableItemsElement.IsListOpenChanged += new EventHandler(this.OnIsAvailableItemsListOpenChanged);
                this.AvailableItemsElement.StartAvailableItemDragAndDrop += new EventHandler<LayoutControlStartDragAndDropEventArgs<FrameworkElement>>(this.OnAvailableItemsStartAvailableItemDragAndDrop);
                this.AvailableItemsElement.StartNewItemDragAndDrop += new EventHandler<LayoutControlStartDragAndDropEventArgs<LayoutControlNewItemInfo>>(this.OnAvailableItemsStartNewItemDragAndDrop);
            }
        }

        private void OnAvailableItemsStartAvailableItemDragAndDrop(object sender, LayoutControlStartDragAndDropEventArgs<FrameworkElement> e)
        {
            this.OnStartAvailableItemDragAndDrop(e);
        }

        private void OnAvailableItemsStartNewItemDragAndDrop(object sender, LayoutControlStartDragAndDropEventArgs<LayoutControlNewItemInfo> e)
        {
            this.OnStartNewItemDragAndDrop(e);
        }

        protected virtual void OnDeleteAvailableItem(FrameworkElement item)
        {
            if (this.DeleteAvailableItem != null)
            {
                this.DeleteAvailableItem(item);
            }
        }

        protected virtual void OnIsAvailableItemsListOpenChanged()
        {
            if (this.IsAvailableItemsListOpenChanged != null)
            {
                this.IsAvailableItemsListOpenChanged(this, EventArgs.Empty);
            }
        }

        private void OnIsAvailableItemsListOpenChanged(object sender, EventArgs e)
        {
            this.OnIsAvailableItemsListOpenChanged();
        }

        protected virtual void OnStartAvailableItemDragAndDrop(LayoutControlStartDragAndDropEventArgs<FrameworkElement> args)
        {
            if (this.StartAvailableItemDragAndDrop != null)
            {
                this.StartAvailableItemDragAndDrop(this, args);
            }
        }

        protected virtual void OnStartNewItemDragAndDrop(LayoutControlStartDragAndDropEventArgs<LayoutControlNewItemInfo> args)
        {
            if (this.StartNewItemDragAndDrop != null)
            {
                this.StartNewItemDragAndDrop(this, args);
            }
        }

        public FrameworkElements AvailableItems
        {
            get => 
                (FrameworkElements) base.GetValue(AvailableItemsProperty);
            set => 
                base.SetValue(AvailableItemsProperty, value);
        }

        public Visibility AvailableItemsUIVisibility
        {
            get => 
                (Visibility) base.GetValue(AvailableItemsUIVisibilityProperty);
            set => 
                base.SetValue(AvailableItemsUIVisibilityProperty, value);
        }

        public bool IsAvailableItemsListOpen =>
            (this.AvailableItemsElement != null) && this.AvailableItemsElement.IsListOpen;

        public LayoutControlNewItemsInfo NewItemsInfo
        {
            get => 
                (LayoutControlNewItemsInfo) base.GetValue(NewItemsInfoProperty);
            set => 
                base.SetValue(NewItemsInfoProperty, value);
        }

        public Visibility NewItemsUIVisibility
        {
            get => 
                (Visibility) base.GetValue(NewItemsUIVisibilityProperty);
            set => 
                base.SetValue(NewItemsUIVisibilityProperty, value);
        }

        protected LayoutControlAvailableItemsControl AvailableItemsElement { get; private set; }
    }
}

