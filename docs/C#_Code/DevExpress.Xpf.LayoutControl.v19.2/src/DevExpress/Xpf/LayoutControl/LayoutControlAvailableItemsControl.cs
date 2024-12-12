namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Threading;

    [TemplatePart(Name="AvailableItemsListElement", Type=typeof(ListBox)), TemplatePart(Name="NewItemsListElement", Type=typeof(ListBox)), TemplateVisualState(Name="ListClosed", GroupName="LayoutStates"), TemplateVisualState(Name="ListOpen", GroupName="LayoutStates")]
    public class LayoutControlAvailableItemsControl : ControlBase, ILayoutControlAvailableItemsControl, IControl
    {
        public static readonly DependencyProperty AvailableItemsProperty;
        public static readonly DependencyProperty IsListOpenProperty;
        public static readonly DependencyProperty LayoutGroupItemTemplateProperty;
        public static readonly DependencyProperty NewItemsInfoProperty;
        public static readonly DependencyProperty NewItemsUIVisibilityProperty;
        private const string AvailableItemsListElementName = "AvailableItemsListElement";
        private const string NewItemsListElementName = "NewItemsListElement";
        private const string LayoutStates = "LayoutStates";
        private const string ListClosedLayoutState = "ListClosed";
        private const string ListOpenLayoutState = "ListOpen";

        public event Action<FrameworkElement> DeleteAvailableItem;

        public event EventHandler IsListOpenChanged;

        public event EventHandler<LayoutControlStartDragAndDropEventArgs<FrameworkElement>> StartAvailableItemDragAndDrop;

        public event EventHandler<LayoutControlStartDragAndDropEventArgs<LayoutControlNewItemInfo>> StartNewItemDragAndDrop;

        static LayoutControlAvailableItemsControl()
        {
            AvailableItemsProperty = DependencyProperty.Register("AvailableItems", typeof(FrameworkElements), typeof(LayoutControlAvailableItemsControl), new PropertyMetadata((o, e) => ((LayoutControlAvailableItemsControl) o).OnAvailableItemsChanged((FrameworkElements) e.OldValue)));
            IsListOpenProperty = DependencyProperty.Register("IsListOpen", typeof(bool), typeof(LayoutControlAvailableItemsControl), new PropertyMetadata((o, e) => ((LayoutControlAvailableItemsControl) o).OnIsListOpenChanged()));
            LayoutGroupItemTemplateProperty = DependencyProperty.Register("LayoutGroupItemTemplate", typeof(DataTemplate), typeof(LayoutControlAvailableItemsControl), null);
            NewItemsInfoProperty = DependencyProperty.Register("NewItemsInfo", typeof(LayoutControlNewItemsInfo), typeof(LayoutControlAvailableItemsControl), new PropertyMetadata((o, e) => ((LayoutControlAvailableItemsControl) o).OnNewItemsInfoChanged()));
            NewItemsUIVisibilityProperty = DependencyProperty.Register("NewItemsUIVisibility", typeof(Visibility), typeof(LayoutControlAvailableItemsControl), null);
        }

        public LayoutControlAvailableItemsControl()
        {
            base.DefaultStyleKey = typeof(LayoutControlAvailableItemsControl);
        }

        protected virtual AvailableItemInfo CreateAvailableItemInfo(FrameworkElement item) => 
            new AvailableItemInfo(item, this.GetAvailableItemLabel(item));

        protected override ControlControllerBase CreateController() => 
            new LayoutControlAvailableItemsController(this);

        protected virtual LayoutControlAvailableListBoxItem CreateListItem() => 
            new LayoutControlAvailableListBoxItem(this);

        protected LayoutControlAvailableListBoxItem CreateListItem(AvailableItemInfo itemInfo)
        {
            LayoutControlAvailableListBoxItem listItem = this.CreateListItem();
            this.InitListItem(listItem, itemInfo);
            listItem.Item = itemInfo.Item;
            return listItem;
        }

        protected virtual string GetAvailableItemDefaultLabel(FrameworkElement item) => 
            DevExpress.Xpf.LayoutControl.LayoutControl.GetCustomizationDefaultLabel(item);

        protected AvailableItemInfo GetAvailableItemInfo(ListBoxItem listItem)
        {
            int index = this.AvailableItemsListElement.Items.IndexOf(listItem);
            return ((index != -1) ? this.AvailableItemsInfo[index] : null);
        }

        protected int GetAvailableItemInfoIndex(FrameworkElement item)
        {
            for (int i = 0; i < this.AvailableItemsInfo.Count; i++)
            {
                if (ReferenceEquals(this.AvailableItemsInfo[i].Item, item))
                {
                    return i;
                }
            }
            return -1;
        }

        protected virtual string GetAvailableItemLabel(FrameworkElement item)
        {
            string customizationLabel = DevExpress.Xpf.LayoutControl.LayoutControl.GetCustomizationLabel(item) as string;
            if (string.IsNullOrEmpty(customizationLabel))
            {
                customizationLabel = this.GetAvailableItemDefaultLabel(item);
            }
            return customizationLabel;
        }

        protected virtual List<AvailableItemInfo> GetAvailableItemsInfo()
        {
            List<AvailableItemInfo> list = new List<AvailableItemInfo>();
            foreach (FrameworkElement element in this.AvailableItems)
            {
                list.Add(this.CreateAvailableItemInfo(element));
            }
            list.Sort(new Comparison<AvailableItemInfo>(AvailableItemInfo.Compare));
            return list;
        }

        protected int GetListItemIndex(FrameworkElement item)
        {
            for (int i = 0; i < this.AvailableItemsListElement.Items.Count; i++)
            {
                if (ReferenceEquals(((LayoutControlAvailableListBoxItem) this.AvailableItemsListElement.Items[i]).Item, item))
                {
                    return i;
                }
            }
            return -1;
        }

        protected LayoutControlNewItemInfo GetNewItemInfo(ListBoxItem listItem)
        {
            int index = this.NewItemsListElement.Items.IndexOf(listItem);
            return ((index != -1) ? this.NewItemsInfo[index] : null);
        }

        protected virtual void InitListItem(LayoutControlAvailableListBoxItem listItem, AvailableItemInfo itemInfo)
        {
            listItem.Content = itemInfo.Label;
            if (itemInfo.Item.IsLayoutGroup() && (this.LayoutGroupItemTemplate != null))
            {
                listItem.ContentTemplate = this.LayoutGroupItemTemplate;
            }
            if (DevExpress.Xpf.LayoutControl.LayoutControl.GetIsUserDefined(itemInfo.Item))
            {
                Binding binding = new Binding("NewItemsUIVisibility");
                binding.Source = this;
                listItem.SetBinding(LayoutControlAvailableListBoxItem.DeleteElementVisibilityProperty, binding);
            }
            listItem.DeleteElementClick += (o, e) => this.OnDeleteAvailableItem(itemInfo.Item);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.AvailableItemsListElement = base.GetTemplateChild("AvailableItemsListElement") as ListBox;
            this.NewItemsListElement = base.GetTemplateChild("NewItemsListElement") as ListBox;
            this.UpdateAvailableItemsListElement(null);
            this.UpdateNewItemsListElement();
        }

        private void OnAvailableItemsChanged(FrameworkElements oldValue)
        {
            if (oldValue != null)
            {
                oldValue.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.OnAvailableItemsChanged);
            }
            if (this.AvailableItems != null)
            {
                this.AvailableItems.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnAvailableItemsChanged);
            }
            this.OnAvailableItemsChanged((NotifyCollectionChangedEventArgs) null);
        }

        protected virtual void OnAvailableItemsChanged(NotifyCollectionChangedEventArgs args)
        {
            if (this.AvailableItems == null)
            {
                this.AvailableItemsInfo = null;
            }
            else if (((args == null) || ((args.Action != NotifyCollectionChangedAction.Add) && (args.Action != NotifyCollectionChangedAction.Remove))) || (this.AvailableItemsInfo == null))
            {
                this.AvailableItemsInfo = this.GetAvailableItemsInfo();
            }
            else if (args.Action != NotifyCollectionChangedAction.Add)
            {
                foreach (FrameworkElement element2 in args.OldItems)
                {
                    this.AvailableItemsInfo.RemoveAt(this.GetAvailableItemInfoIndex(element2));
                }
            }
            else
            {
                foreach (FrameworkElement element in args.NewItems)
                {
                    AvailableItemInfo item = this.CreateAvailableItemInfo(element);
                    this.AvailableItemsInfo.Insert(AvailableItemInfo.GetInsertionIndex(this.AvailableItemsInfo, item), item);
                }
            }
            this.UpdateAvailableItemsListElement(args);
        }

        private void OnAvailableItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            int availableItemsChangeCount = this.AvailableItemsChangeCount;
            this.AvailableItemsChangeCount = availableItemsChangeCount + 1;
            if (this.AvailableItemsChangeCount == 1)
            {
                base.Dispatcher.BeginInvoke(delegate {
                    this.OnAvailableItemsChanged((this.AvailableItemsChangeCount == 1) ? e : null);
                    this.AvailableItemsChangeCount = 0;
                }, DispatcherPriority.Render, new object[0]);
            }
        }

        protected virtual void OnDeleteAvailableItem(FrameworkElement item)
        {
            if (this.DeleteAvailableItem != null)
            {
                this.DeleteAvailableItem(item);
            }
        }

        protected virtual void OnIsListOpenChanged()
        {
            this.UpdateState(true);
            if (this.IsListOpenChanged != null)
            {
                this.IsListOpenChanged(this, EventArgs.Empty);
            }
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            e.Handled = true;
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
            e.Handled = true;
        }

        protected virtual void OnNewItemsInfoChanged()
        {
            this.UpdateNewItemsListElement();
        }

        protected virtual void OnStartAvailableItemDragAndDrop(FrameworkElement item, MouseEventArgs mouseEventArgs, FrameworkElement source)
        {
            this.OnStartItemDragAndDrop();
            if (this.StartAvailableItemDragAndDrop != null)
            {
                this.StartAvailableItemDragAndDrop(this, new LayoutControlStartDragAndDropEventArgs<FrameworkElement>(item, mouseEventArgs, source));
            }
        }

        protected virtual void OnStartItemDragAndDrop()
        {
            this.IsListOpen = false;
            base.Controller.IsMouseEntered = false;
        }

        protected internal virtual void OnStartItemDragAndDrop(ListBoxItem listItem, MouseEventArgs arguments)
        {
            AvailableItemInfo availableItemInfo = this.GetAvailableItemInfo(listItem);
            if (availableItemInfo != null)
            {
                this.OnStartAvailableItemDragAndDrop(availableItemInfo.Item, arguments, listItem);
            }
            else
            {
                LayoutControlNewItemInfo newItemInfo = this.GetNewItemInfo(listItem);
                if (newItemInfo != null)
                {
                    this.OnStartNewItemDragAndDrop(newItemInfo, arguments, listItem);
                }
            }
        }

        protected virtual void OnStartNewItemDragAndDrop(LayoutControlNewItemInfo itemInfo, MouseEventArgs mouseEventArgs, FrameworkElement source)
        {
            this.OnStartItemDragAndDrop();
            if (this.StartNewItemDragAndDrop != null)
            {
                this.StartNewItemDragAndDrop(this, new LayoutControlStartDragAndDropEventArgs<LayoutControlNewItemInfo>(itemInfo, mouseEventArgs, source));
            }
        }

        protected void UpdateAvailableItemsListElement(NotifyCollectionChangedEventArgs args)
        {
            if (this.AvailableItemsListElement != null)
            {
                if (this.AvailableItemsInfo == null)
                {
                    this.AvailableItemsListElement.Items.Clear();
                }
                else if ((args == null) || ((args.Action != NotifyCollectionChangedAction.Add) && (args.Action != NotifyCollectionChangedAction.Remove)))
                {
                    this.AvailableItemsListElement.Items.Clear();
                    foreach (AvailableItemInfo info in this.AvailableItemsInfo)
                    {
                        this.AvailableItemsListElement.Items.Add(this.CreateListItem(info));
                    }
                }
                else if (args.Action != NotifyCollectionChangedAction.Add)
                {
                    foreach (FrameworkElement element2 in args.OldItems)
                    {
                        this.AvailableItemsListElement.Items.RemoveAt(this.GetListItemIndex(element2));
                    }
                }
                else
                {
                    foreach (FrameworkElement element in args.NewItems)
                    {
                        int availableItemInfoIndex = this.GetAvailableItemInfoIndex(element);
                        this.AvailableItemsListElement.Items.Insert(availableItemInfoIndex, this.CreateListItem(this.AvailableItemsInfo[availableItemInfoIndex]));
                    }
                }
            }
        }

        protected void UpdateNewItemsListElement()
        {
            if (this.NewItemsListElement != null)
            {
                this.NewItemsListElement.Items.Clear();
                if (this.NewItemsInfo != null)
                {
                    foreach (LayoutControlNewItemInfo info in this.NewItemsInfo)
                    {
                        ListBoxItem newItem = this.CreateListItem();
                        newItem.Content = info.Label;
                        this.NewItemsListElement.Items.Add(newItem);
                    }
                }
            }
        }

        protected override void UpdateState(bool useTransitions)
        {
            base.UpdateState(useTransitions);
            this.GoToState(this.IsListOpen ? "ListOpen" : "ListClosed", useTransitions);
        }

        public FrameworkElements AvailableItems
        {
            get => 
                (FrameworkElements) base.GetValue(AvailableItemsProperty);
            set => 
                base.SetValue(AvailableItemsProperty, value);
        }

        public bool IsListOpen
        {
            get => 
                (bool) base.GetValue(IsListOpenProperty);
            set => 
                base.SetValue(IsListOpenProperty, value);
        }

        public DataTemplate LayoutGroupItemTemplate
        {
            get => 
                (DataTemplate) base.GetValue(LayoutGroupItemTemplateProperty);
            set => 
                base.SetValue(LayoutGroupItemTemplateProperty, value);
        }

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

        protected ListBox AvailableItemsListElement { get; private set; }

        protected ListBox NewItemsListElement { get; private set; }

        protected List<AvailableItemInfo> AvailableItemsInfo { get; private set; }

        private int AvailableItemsChangeCount { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LayoutControlAvailableItemsControl.<>c <>9 = new LayoutControlAvailableItemsControl.<>c();

            internal void <.cctor>b__83_0(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutControlAvailableItemsControl) o).OnAvailableItemsChanged((FrameworkElements) e.OldValue);
            }

            internal void <.cctor>b__83_1(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutControlAvailableItemsControl) o).OnIsListOpenChanged();
            }

            internal void <.cctor>b__83_2(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutControlAvailableItemsControl) o).OnNewItemsInfoChanged();
            }
        }

        protected class AvailableItemInfo
        {
            public AvailableItemInfo(FrameworkElement item, string label)
            {
                this.Item = item;
                this.Label = label;
            }

            public static int Compare(LayoutControlAvailableItemsControl.AvailableItemInfo info1, LayoutControlAvailableItemsControl.AvailableItemInfo info2) => 
                string.Compare(info1.Label, info2.Label);

            public static int GetInsertionIndex(List<LayoutControlAvailableItemsControl.AvailableItemInfo> sortedList, LayoutControlAvailableItemsControl.AvailableItemInfo info)
            {
                for (int i = 0; i < sortedList.Count; i++)
                {
                    if (Compare(sortedList[i], info) > 0)
                    {
                        return i;
                    }
                }
                return sortedList.Count;
            }

            public FrameworkElement Item { get; private set; }

            public string Label { get; private set; }
        }
    }
}

