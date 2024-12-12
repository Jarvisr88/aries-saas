namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Base;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;

    public abstract class LayoutGroupWrapperBase<TTarget, TChild> : ISelectorWrapper<TTarget>, IItemsControlWrapper<TTarget>, ITargetWrapper<TTarget> where TTarget: LayoutGroup where TChild: ContentItem
    {
        private TTarget target;
        private DockLayoutManager manager;
        private ObservableCollection<object> itemsSource;
        private object selectedItem;
        private bool lockSelection;
        private readonly Dictionary<object, TChild> children;
        [CompilerGenerated]
        private EventHandler SelectionChanged;
        [CompilerGenerated]
        private EventHandler<ChildClosingEventArgs<TTarget, TChild>> Closing;
        private bool focusOnSelectedItemChanged;

        public event EventHandler<ChildClosingEventArgs<TTarget, TChild>> Closing
        {
            [CompilerGenerated] add
            {
                EventHandler<ChildClosingEventArgs<TTarget, TChild>> closing = this.Closing;
                while (true)
                {
                    EventHandler<ChildClosingEventArgs<TTarget, TChild>> comparand = closing;
                    EventHandler<ChildClosingEventArgs<TTarget, TChild>> handler3 = comparand + value;
                    closing = Interlocked.CompareExchange<EventHandler<ChildClosingEventArgs<TTarget, TChild>>>(ref this.Closing, handler3, comparand);
                    if (ReferenceEquals(closing, comparand))
                    {
                        return;
                    }
                }
            }
            [CompilerGenerated] remove
            {
                EventHandler<ChildClosingEventArgs<TTarget, TChild>> closing = this.Closing;
                while (true)
                {
                    EventHandler<ChildClosingEventArgs<TTarget, TChild>> comparand = closing;
                    EventHandler<ChildClosingEventArgs<TTarget, TChild>> handler3 = comparand - value;
                    closing = Interlocked.CompareExchange<EventHandler<ChildClosingEventArgs<TTarget, TChild>>>(ref this.Closing, handler3, comparand);
                    if (ReferenceEquals(closing, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public event EventHandler SelectionChanged
        {
            [CompilerGenerated] add
            {
                EventHandler selectionChanged = this.SelectionChanged;
                while (true)
                {
                    EventHandler comparand = selectionChanged;
                    EventHandler handler3 = comparand + value;
                    selectionChanged = Interlocked.CompareExchange<EventHandler>(ref this.SelectionChanged, handler3, comparand);
                    if (ReferenceEquals(selectionChanged, comparand))
                    {
                        return;
                    }
                }
            }
            [CompilerGenerated] remove
            {
                EventHandler selectionChanged = this.SelectionChanged;
                while (true)
                {
                    EventHandler comparand = selectionChanged;
                    EventHandler handler3 = comparand - value;
                    selectionChanged = Interlocked.CompareExchange<EventHandler>(ref this.SelectionChanged, handler3, comparand);
                    if (ReferenceEquals(selectionChanged, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public LayoutGroupWrapperBase()
        {
            this.focusOnSelectedItemChanged = true;
            this.children = new Dictionary<object, TChild>();
        }

        protected virtual void Add(object viewModel)
        {
            TChild local = this.CreateChild(viewModel);
            this.children.Add(viewModel, local);
        }

        protected virtual void Clear()
        {
            this.ClearChildren(this.children.Values);
            this.children.Clear();
        }

        protected void ClearChild(TChild child)
        {
            child.SetCurrentValue(FrameworkElement.DataContextProperty, null);
            child.SetCurrentValue(ContentItem.ContentProperty, null);
            child.SetCurrentValue(BaseLayoutItem.CaptionProperty, null);
        }

        protected virtual void ClearChildren(IEnumerable<TChild> children)
        {
            foreach (TChild local in children)
            {
                this.RemoveChild(local);
            }
        }

        protected void ConfigureChild(TChild child, object viewModel)
        {
            string str = this.Owner.RegionName ?? string.Empty;
            string key = this.Owner.GetKey(viewModel);
            string str3 = !string.IsNullOrEmpty(key) ? $"{this.Target.GetType().Name}_{str}_{key}" : null;
            if (str3 != null)
            {
                child.SetValue(FrameworkElement.NameProperty, str3);
            }
            child.SetCurrentValue(FrameworkElement.DataContextProperty, viewModel);
            child.SetCurrentValue(ContentItem.ContentProperty, viewModel);
            child.SetCurrentValue(BaseLayoutItem.CaptionProperty, viewModel);
            this.Owner.ConfigureChild(child);
        }

        protected abstract TChild CreateChild(object viewModel);
        protected TChild GetChild(object viewModel)
        {
            if (this.children.ContainsKey(viewModel))
            {
                return this.children[viewModel];
            }
            return default(TChild);
        }

        private void OnItemsSourceChanged(ObservableCollection<object> oldValue, ObservableCollection<object> newValue)
        {
            if (oldValue != null)
            {
                oldValue.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.OnItemsSourceCollectionChanged);
            }
            if (newValue != null)
            {
                newValue.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnItemsSourceCollectionChanged);
            }
        }

        private void OnItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if ((e.Action == NotifyCollectionChangedAction.Move) || (e.Action == NotifyCollectionChangedAction.Replace))
            {
                throw new NotImplementedException();
            }
            if ((e.Action == NotifyCollectionChangedAction.Reset) && (this.itemsSource.Count == 0))
            {
                this.Clear();
            }
            else
            {
                if (e.NewItems != null)
                {
                    foreach (object obj2 in e.NewItems)
                    {
                        this.Add(obj2);
                    }
                }
                if (e.OldItems != null)
                {
                    foreach (object obj3 in e.OldItems)
                    {
                        this.Remove(obj3);
                    }
                }
            }
        }

        protected virtual void OnManagerChanged(DockLayoutManager oldValue, DockLayoutManager newValue)
        {
            if (oldValue != null)
            {
                oldValue.DockItemActivated -= new DockItemActivatedEventHandler(this.OnManagerDockItemActivated);
                oldValue.DockItemClosing -= new DockItemCancelEventHandler(this.OnManagerDockItemClosing);
            }
            if (newValue != null)
            {
                newValue.DockItemActivated += new DockItemActivatedEventHandler(this.OnManagerDockItemActivated);
                newValue.DockItemClosing += new DockItemCancelEventHandler(this.OnManagerDockItemClosing);
            }
        }

        private void OnManagerDockItemActivated(object sender, DockItemActivatedEventArgs e)
        {
            bool flag = false;
            if ((e.OldItem != null) && ((e.OldItem is TChild) && (this.children.ContainsValue((TChild) e.OldItem) && (e.Item == null))))
            {
                flag = this.SelectedItem == this.children.GetKeyByValue<object, TChild>(((TChild) e.OldItem));
            }
            if (flag)
            {
                this.lockSelection = true;
                this.SelectedItem = null;
                this.lockSelection = false;
            }
            else if ((e.Item is TChild) && this.children.ContainsValue((TChild) e.Item))
            {
                this.lockSelection = true;
                this.SelectedItem = this.children.GetKeyByValue<object, TChild>((TChild) e.Item);
                this.lockSelection = false;
            }
        }

        private void OnManagerDockItemClosing(object sender, ItemCancelEventArgs e)
        {
            if ((e.Item != null) && !e.Handled)
            {
                LayoutGroup item = e.Item as LayoutGroup;
                if (item != null)
                {
                    (from x in item.GetNestedItems().OfType<TChild>()
                        where base.children.ContainsValue(x)
                        select x).ForEach<TChild>(x => base.Manager.DockController.Close(x));
                }
                if (((e.Item is TChild) && this.children.ContainsValue((TChild) e.Item)) && (DockControllerHelper.GetActualClosingBehavior(this.Manager, e.Item) == ClosingBehavior.ImmediatelyRemove))
                {
                    ChildClosingEventArgs<TTarget, TChild> args = new ChildClosingEventArgs<TTarget, TChild>(this.children.GetKeyByValue<object, TChild>((TChild) e.Item), (TChild) e.Item);
                    if (this.Closing != null)
                    {
                        this.Closing(this, args);
                    }
                    e.Cancel = args.Cancel;
                    e.Handled = true;
                }
            }
        }

        protected virtual void OnSelectedItemChanged(object oldValue, object newValue)
        {
            if (newValue == null)
            {
                if ((this.SelectionChanged != null) && (oldValue != newValue))
                {
                    this.SelectionChanged(this, EventArgs.Empty);
                }
            }
            else if (this.children.ContainsKey(newValue))
            {
                TChild child = this.children[newValue];
                if (!this.lockSelection)
                {
                    this.SelectChild(child);
                }
                if ((this.SelectionChanged != null) && (oldValue != newValue))
                {
                    this.SelectionChanged(this, EventArgs.Empty);
                }
            }
        }

        protected virtual void OnTargetChanged(TTarget oldValue, TTarget newValue)
        {
            this.UpdateManager();
        }

        protected virtual void Remove(object viewModel)
        {
            if (this.children.ContainsKey(viewModel))
            {
                this.RemoveChild(this.children[viewModel]);
                this.children.Remove(viewModel);
            }
        }

        protected abstract void RemoveChild(TChild child);
        protected virtual void SelectChild(TChild child)
        {
            if (this.Manager.DockController != null)
            {
                this.Manager.DockController.Restore(child);
                if (this.focusOnSelectedItemChanged)
                {
                    this.Manager.DockController.Activate(child);
                    if (this.Target.IsTabHost && !child.IsSelectedItem)
                    {
                        child.SetCurrentValue(BaseLayoutItem.IsSelectedItemProperty, true);
                    }
                }
            }
        }

        public void SelectItem(object item, bool focus)
        {
            this.focusOnSelectedItemChanged = focus;
            this.SelectedItem = item;
            this.focusOnSelectedItemChanged = true;
        }

        private void UpdateManager()
        {
            if (this.Target == null)
            {
                this.Manager = null;
            }
            else if (this.manager == null)
            {
                this.Manager = this.Target.FindDockLayoutManager();
            }
        }

        public TTarget Target
        {
            get => 
                this.target;
            set
            {
                if (this.target != value)
                {
                    TTarget target = this.target;
                    this.target = value;
                    this.OnTargetChanged(target, this.target);
                }
            }
        }

        public IStrategyOwner Owner { get; set; }

        public DockLayoutManager Manager
        {
            get
            {
                if (this.manager == null)
                {
                    this.UpdateManager();
                }
                return this.manager;
            }
            set
            {
                if (!ReferenceEquals(this.manager, value))
                {
                    DockLayoutManager oldValue = this.manager;
                    this.manager = value;
                    this.OnManagerChanged(oldValue, this.manager);
                }
            }
        }

        public object ItemsSource
        {
            get => 
                this.itemsSource;
            set
            {
                if (!ReferenceEquals(this.itemsSource, value))
                {
                    ObservableCollection<object> itemsSource = this.itemsSource;
                    this.itemsSource = (ObservableCollection<object>) value;
                    this.OnItemsSourceChanged(itemsSource, this.itemsSource);
                }
            }
        }

        public object SelectedItem
        {
            get => 
                this.selectedItem;
            set
            {
                object selectedItem = this.selectedItem;
                this.selectedItem = value;
                this.OnSelectedItemChanged(selectedItem, this.selectedItem);
            }
        }

        public DataTemplate ItemTemplate
        {
            get => 
                this.Target.ItemContentTemplate;
            set => 
                this.Target.ItemContentTemplate = value;
        }

        public DataTemplateSelector ItemTemplateSelector
        {
            get => 
                this.Target.ItemContentTemplateSelector;
            set => 
                this.Target.ItemContentTemplateSelector = value;
        }

        public class ChildClosingEventArgs : CancelEventArgs
        {
            public ChildClosingEventArgs(object viewModel, TChild child)
            {
                this.ViewModel = viewModel;
                this.Child = child;
            }

            public object ViewModel { get; private set; }

            public TChild Child { get; private set; }
        }
    }
}

