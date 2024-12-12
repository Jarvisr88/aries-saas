namespace DevExpress.Xpf.Core
{
    using DevExpress.Data.Helpers;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Bars.Helpers;
    using DevExpress.Xpf.Bars.Native;
    using DevExpress.Xpf.Core.Internal;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Threading;

    public class ItemsAttachedBehaviorCore<TContainer, TItem> : IWeakEventListener where TContainer: DependencyObject where TItem: DependencyObject
    {
        private const string CanBeInheritanceContextPropertyName = "CanBeInheritanceContext";
        private static readonly ReflectionHelper reflectionHelper;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty ResetProperty;
        private readonly Func<TContainer, IList> getTargetFunction;
        private readonly Func<TContainer, TItem> createItemDelegate;
        private readonly Action<TItem> setDefaultBindingAction;
        private readonly bool useDefaultTemplateSelector;
        private readonly bool useDefaultTemplateValidation;
        private readonly Func<TItem, bool> customClear;
        private readonly bool forceBindingsProcessing;
        private readonly DefaultTemplateChangeNotifier defaultTemplateNotifier;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private readonly DependencyProperty itemGeneratorTemplateProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private readonly DependencyProperty itemGeneratorTemplateSelectorProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private readonly DependencyProperty itemGeneratorStyleProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private readonly DependencyProperty itemGeneratorStyleSelectorProperty;
        private readonly Dispatcher dispatcher;
        private Action<int, object> insertItemAction;
        private Action<TItem, object> linkItemWithSourceAction;
        private ISupportInitialize supportInitialize;
        private readonly Dictionary<Freezable, FrameworkElement> freezableWrappers;
        protected bool lockSynchronization;
        private IList source;

        static ItemsAttachedBehaviorCore()
        {
            ItemsAttachedBehaviorCore<TContainer, TItem>.reflectionHelper = new ReflectionHelper();
            ItemsAttachedBehaviorCore<TContainer, TItem>.ResetProperty = DependencyProperty.RegisterAttached("Reset", typeof(ResetProperties<TContainer, TItem>), typeof(ItemsAttachedBehaviorCore<TContainer, TItem>), new PropertyMetadata(ResetProperties<TContainer, TItem>.None));
        }

        protected ItemsAttachedBehaviorCore(Func<TContainer, IList> getTargetFunction, Func<TContainer, TItem> createItemDelegate, DependencyProperty itemGeneratorTemplateProperty, DependencyProperty itemGeneratorTemplateSelectorProperty, DependencyProperty itemGeneratorStyleProperty, DependencyProperty itemGeneratorStyleSelectorProperty, IEnumerable source, Action<TItem> setDefaultBindingAction, bool useDefaultTemplateSelector, bool useDefaultTemplateValidation, Func<TItem, bool> customClear, bool forceBindingsProcessing)
        {
            this.freezableWrappers = new Dictionary<Freezable, FrameworkElement>();
            this.getTargetFunction = getTargetFunction;
            this.setDefaultBindingAction = setDefaultBindingAction;
            this.createItemDelegate = createItemDelegate;
            this.useDefaultTemplateSelector = useDefaultTemplateSelector;
            this.useDefaultTemplateValidation = useDefaultTemplateValidation;
            this.customClear = customClear;
            this.forceBindingsProcessing = forceBindingsProcessing;
            this.itemGeneratorTemplateProperty = itemGeneratorTemplateProperty;
            this.itemGeneratorTemplateSelectorProperty = itemGeneratorTemplateSelectorProperty;
            this.itemGeneratorStyleProperty = itemGeneratorStyleProperty;
            this.itemGeneratorStyleSelectorProperty = itemGeneratorStyleSelectorProperty;
            IList list1 = ((source is IList) || (source == null)) ? (source as IList) : new EnumerableObservableWrapper<object>(source);
            this.Source = list1;
            this.dispatcher = Dispatcher.CurrentDispatcher;
            this.defaultTemplateNotifier = new DefaultTemplateChangeNotifier();
            this.defaultTemplateNotifier.NeedsRecalc += new EventHandler(this.RecalcDefaultTemplate);
        }

        protected static void BehaviorDestroy(DependencyProperty itemsAttachedBehaviorStoreProperty, DependencyObject d)
        {
            ItemsAttachedBehaviorCore<TContainer, TItem> behavior = ItemsAttachedBehaviorCore<TContainer, TItem>.GetBehavior(d, itemsAttachedBehaviorStoreProperty);
            if (behavior != null)
            {
                behavior.Disconnect();
            }
        }

        protected static void BehaviorInit(ItemsAttachedBehaviorCore<TContainer, TItem> behaviour, DependencyProperty itemsAttachedBehaviorStoreProperty, Action<int, object> insertItemAction, ISupportInitialize supportInitialize, Action<TItem, object> linkItemWithSourceAction, DependencyObject d)
        {
            d.SetValue(itemsAttachedBehaviorStoreProperty, behaviour);
            behaviour.insertItemAction = insertItemAction;
            behaviour.supportInitialize = supportInitialize;
            behaviour.linkItemWithSourceAction = linkItemWithSourceAction;
            behaviour.ConnectToContainer((TContainer) d);
        }

        protected virtual void ClearItem(object item)
        {
            (item as Freezable).Do<Freezable>(delegate (Freezable x) {
                if (base.freezableWrappers.ContainsKey(x))
                {
                    LogicalTreeWrapper.RemoveLogicalChild(base.Control, base.freezableWrappers[x], null, false);
                    base.freezableWrappers.Remove(x);
                }
            });
            if (((item is FrameworkElement) || (item is FrameworkContentElement)) && ((this.customClear == null) || !this.customClear(item as TItem)))
            {
                DependencyObject obj2 = (DependencyObject) item;
                ResetProperties<TContainer, TItem> properties = (ResetProperties<TContainer, TItem>) obj2.GetValue(ItemsAttachedBehaviorCore<TContainer, TItem>.ResetProperty);
                if (properties.HasFlag(ResetProperties<TContainer, TItem>.DataContext))
                {
                    obj2.ClearValue(FrameworkElement.DataContextProperty);
                }
                if (properties.HasFlag(ResetProperties<TContainer, TItem>.Style))
                {
                    obj2.ClearValue(FrameworkElement.StyleProperty);
                }
            }
        }

        private void ConnectToContainer(TContainer d)
        {
            this.Control = d;
            LogicalTreeWrapper.AddLogicalChild(this.Control, this.defaultTemplateNotifier, null, false);
            this.Populate();
        }

        protected virtual TItem CreateItem(object item)
        {
            Style style;
            if (((this.itemGeneratorStyleProperty != null) ? (this.Control.GetValue(this.itemGeneratorStyleProperty) as Style) : null) == null)
            {
                StyleSelector selector2 = (this.itemGeneratorStyleSelectorProperty != null) ? (this.Control.GetValue(this.itemGeneratorStyleSelectorProperty) as StyleSelector) : null;
                if (selector2 != null)
                {
                    style = selector2.SelectStyle(item, this.Control);
                }
            }
            DataTemplateSelector selector = (this.itemGeneratorTemplateSelectorProperty != null) ? (this.Control.GetValue(this.itemGeneratorTemplateSelectorProperty) as DataTemplateSelector) : null;
            DataTemplate template = (selector == null) ? ((this.itemGeneratorTemplateProperty != null) ? (this.Control.GetValue(this.itemGeneratorTemplateProperty) as DataTemplate) : null) : selector.SelectTemplate(item, this.Control);
            if ((template == null) && this.useDefaultTemplateSelector)
            {
                template = DefaultTemplateSelector.Instance.SelectTemplate(item, this.Control).If<DataTemplate>(new Func<DataTemplate, bool>(this.IsTemplateValid));
            }
            if ((template == null) && (this.useDefaultTemplateSelector && (item != null)))
            {
                this.defaultTemplateNotifier.RegisterType(item.GetType());
            }
            if ((template == null) && ((style == null) && (!(item is TItem) && (this.setDefaultBindingAction == null))))
            {
                return default(TItem);
            }
            TItem local = TemplateHelper.LoadFromTemplate<TItem>(template, new Func<object, object>(this.ProcessItemFromTemplate));
            if (local == null)
            {
                if (item is TItem)
                {
                    local = item as TItem;
                }
                else
                {
                    local = this.createItemDelegate(this.Control);
                    if (this.setDefaultBindingAction != null)
                    {
                        this.setDefaultBindingAction(local);
                    }
                }
            }
            if ((local is FrameworkElement) || (local is FrameworkContentElement))
            {
                ResetProperties<TContainer, TItem> none = ResetProperties<TContainer, TItem>.None;
                if (!Equals(local, item))
                {
                    none |= !Equals(item, local.GetValue(FrameworkElement.DataContextProperty)) ? ResetProperties<TContainer, TItem>.DataContext : ResetProperties<TContainer, TItem>.None;
                    local.SetValue(FrameworkElement.DataContextProperty, item);
                }
                if ((style != null) && (local.ReadLocalValue(FrameworkElement.StyleProperty) == DependencyProperty.UnsetValue))
                {
                    local.SetValue(FrameworkElement.StyleProperty, style);
                    none |= ResetProperties<TContainer, TItem>.Style;
                }
                local.SetValue(ItemsAttachedBehaviorCore<TContainer, TItem>.ResetProperty, none);
            }
            if (this.linkItemWithSourceAction != null)
            {
                this.linkItemWithSourceAction(local, item);
            }
            Freezable key = local as Freezable;
            if ((local != null) && ((key == null) || !Equals(local, item)))
            {
                DependencyObjectExtensions.SetDataContext(local, item);
                ItemsAttachedBehaviorProperties.SetSource(local, item);
            }
            if ((key != null) && !Equals(local, item))
            {
                ContentControl child = new ContentControl();
                LogicalTreeWrapper.AddLogicalChild(this.Control, child, null, false);
                this.freezableWrappers.Add(key, child);
                child.DataContext = item;
                child.Content = key;
            }
            return local;
        }

        public static TItem CreateItem(DependencyObject o, DependencyProperty itemsAttachedBehaviorStoreProperty, object dataContext)
        {
            ItemsAttachedBehaviorCore<TContainer, TItem> behavior = ItemsAttachedBehaviorCore<TContainer, TItem>.GetBehavior(o, itemsAttachedBehaviorStoreProperty);
            if (behavior != null)
            {
                return behavior.CreateItem(dataContext);
            }
            return default(TItem);
        }

        private void DestroyFreezableBindings(Freezable freezable)
        {
            BindingOperations.ClearAllBindings(freezable);
            LocalValueEnumerator localValueEnumerator = freezable.GetLocalValueEnumerator();
            while (localValueEnumerator.MoveNext())
            {
                LocalValueEntry current = localValueEnumerator.Current;
                (current.Value as Freezable).Do<Freezable>(x => base.DestroyFreezableBindings(x));
            }
        }

        private void Disconnect()
        {
            this.Source = null;
            LogicalTreeWrapper.RemoveLogicalChild(this.Control, this.defaultTemplateNotifier, null, false);
        }

        private static ItemsAttachedBehaviorCore<TContainer, TItem> GetBehavior(DependencyObject o, DependencyProperty itemsAttachedBehaviorStoreProperty) => 
            o.GetValue(itemsAttachedBehaviorStoreProperty) as ItemsAttachedBehaviorCore<TContainer, TItem>;

        private bool GetCanBeInheritanceContext(Freezable freezable) => 
            ItemsAttachedBehaviorCore<TContainer, TItem>.reflectionHelper.GetPropertyValue<bool>(freezable, "CanBeInheritanceContext", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        private bool IsTemplateValid(DataTemplate template)
        {
            if (!this.useDefaultTemplateValidation)
            {
                return true;
            }
            Dictionary<int, Type> dictionary = new Dictionary<int, Type>();
            if (template.VisualTree == null)
            {
                dictionary = ItemsAttachedBehaviorCore<TContainer, TItem>.reflectionHelper.GetPropertyValue<Dictionary<int, Type>>(template, "ChildTypeFromChildIndex", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            }
            else
            {
                FrameworkElementFactory visualTree = template.VisualTree;
                int num2 = 1;
                bool flag = false;
                while (visualTree != null)
                {
                    if (!flag)
                    {
                        dictionary.Add(num2++, visualTree.Type);
                        if (visualTree.FirstChild != null)
                        {
                            visualTree = visualTree.FirstChild;
                            continue;
                        }
                    }
                    flag = false;
                    if (visualTree.NextSibling != null)
                    {
                        visualTree = visualTree.NextSibling;
                    }
                    else
                    {
                        visualTree = visualTree.Parent;
                        flag = true;
                    }
                }
            }
            int count = dictionary.Count;
            return ((count >= 1) ? (!typeof(TItem).IsAssignableFrom(dictionary[1]) ? ((count >= 2) ? ((typeof(ContentControl).IsAssignableFrom(dictionary[1]) || typeof(ContentPresenter).IsAssignableFrom(dictionary[1])) ? typeof(TItem).IsAssignableFrom(dictionary[2]) : false) : false) : true) : false);
        }

        private void MoveSourceItem(int oldPosition, int newPosition)
        {
            if (this.Source != null)
            {
                object obj2 = this.Source[oldPosition];
                this.lockSynchronization = true;
                try
                {
                    this.Source.RemoveAt(oldPosition);
                    this.Source.Insert(newPosition, obj2);
                }
                finally
                {
                    this.lockSynchronization = false;
                }
            }
        }

        public static void OnItemsGeneratorTemplatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e, DependencyProperty itemsAttachedBehaviorStoreProperty)
        {
            ItemsAttachedBehaviorCore<TContainer, TItem>.OnItemsGeneratorTemplatePropertyChanged(d, e.OldValue, e.NewValue, itemsAttachedBehaviorStoreProperty);
        }

        public static void OnItemsGeneratorTemplatePropertyChanged(DependencyObject d, object oldValue, object newValue, DependencyProperty itemsAttachedBehaviorStoreProperty)
        {
            ItemsAttachedBehaviorCore<TContainer, TItem> behavior = ItemsAttachedBehaviorCore<TContainer, TItem>.GetBehavior(d, itemsAttachedBehaviorStoreProperty);
            if (behavior != null)
            {
                behavior.Populate();
            }
        }

        public static void OnItemsSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e, DependencyProperty itemsAttachedBehaviorStoreProperty, DependencyProperty itemGeneratorTemplateProperty, DependencyProperty itemGeneratorTemplateSelectorProperty, DependencyProperty itemGeneratorStyleProperty, Func<TContainer, IList> getTargetFunction, Func<TContainer, TItem> createItemDelegate, Action<int, object> insertItemAction = null, Action<TItem> setDefaultBindingAction = null, ISupportInitialize supportInitialize = null, Action<TItem, object> linkItemWithSourceAction = null, bool useDefaultTemplateSelector = true, bool useDefaultTemplateValidation = true, Func<TItem, bool> customClear = null, bool forceBindingsProcessing = false)
        {
            ItemsAttachedBehaviorCore<TContainer, TItem>.OnItemsSourcePropertyChanged(d, e.OldValue as IEnumerable, e.NewValue as IEnumerable, itemsAttachedBehaviorStoreProperty, itemGeneratorTemplateProperty, itemGeneratorTemplateSelectorProperty, itemGeneratorStyleProperty, getTargetFunction, createItemDelegate, insertItemAction, setDefaultBindingAction, supportInitialize, linkItemWithSourceAction, useDefaultTemplateSelector, useDefaultTemplateValidation, customClear, forceBindingsProcessing);
        }

        public static void OnItemsSourcePropertyChanged(DependencyObject d, IEnumerable oldValue, IEnumerable newValue, DependencyProperty itemsAttachedBehaviorStoreProperty, DependencyProperty itemGeneratorTemplateProperty, DependencyProperty itemGeneratorTemplateSelectorProperty, DependencyProperty itemGeneratorStyleProperty, Func<TContainer, IList> getTargetFunction, Func<TContainer, TItem> createItemDelegate, Action<int, object> insertItemAction = null, Action<TItem> setDefaultBindingAction = null, ISupportInitialize supportInitialize = null, Action<TItem, object> linkItemWithSourceAction = null, bool useDefaultTemplateSelector = true, bool useDefaultTemplateValidation = true, Func<TItem, bool> customClear = null, bool forceBindingsProcessing = false)
        {
            ItemsAttachedBehaviorCore<TContainer, TItem>.BehaviorDestroy(itemsAttachedBehaviorStoreProperty, d);
            ItemsAttachedBehaviorCore<TContainer, TItem>.BehaviorInit(new ItemsAttachedBehaviorCore<TContainer, TItem>(getTargetFunction, createItemDelegate, itemGeneratorTemplateProperty, itemGeneratorTemplateSelectorProperty, itemGeneratorStyleProperty, null, newValue, setDefaultBindingAction, useDefaultTemplateSelector, useDefaultTemplateValidation, customClear, forceBindingsProcessing), itemsAttachedBehaviorStoreProperty, insertItemAction, supportInitialize, linkItemWithSourceAction, d);
        }

        public static void OnItemsSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e, DependencyProperty itemsAttachedBehaviorStoreProperty, DependencyProperty itemGeneratorTemplateProperty, DependencyProperty itemGeneratorTemplateSelectorProperty, DependencyProperty itemGeneratorStyleProperty, DependencyProperty itemGeneratorStyleSelectorProperty, Func<TContainer, IList> getTargetFunction, Func<TContainer, TItem> createItemDelegate, Action<int, object> insertItemAction = null, Action<TItem> setDefaultBindingAction = null, ISupportInitialize supportInitialize = null, Action<TItem, object> linkItemWithSourceAction = null, bool useDefaultTemplateSelector = true, bool useDefaultTemplateValidation = true, Func<TItem, bool> customClear = null, bool forceBindingsProcessing = false)
        {
            ItemsAttachedBehaviorCore<TContainer, TItem>.OnItemsSourcePropertyChanged(d, e.OldValue as IEnumerable, e.NewValue as IEnumerable, itemsAttachedBehaviorStoreProperty, itemGeneratorTemplateProperty, itemGeneratorTemplateSelectorProperty, itemGeneratorStyleProperty, itemGeneratorStyleSelectorProperty, getTargetFunction, createItemDelegate, insertItemAction, setDefaultBindingAction, supportInitialize, linkItemWithSourceAction, useDefaultTemplateSelector, useDefaultTemplateValidation, customClear, forceBindingsProcessing);
        }

        public static void OnItemsSourcePropertyChanged(DependencyObject d, IEnumerable oldValue, IEnumerable newValue, DependencyProperty itemsAttachedBehaviorStoreProperty, DependencyProperty itemGeneratorTemplateProperty, DependencyProperty itemGeneratorTemplateSelectorProperty, DependencyProperty itemGeneratorStyleProperty, DependencyProperty itemGeneratorStyleSelectorProperty, Func<TContainer, IList> getTargetFunction, Func<TContainer, TItem> createItemDelegate, Action<int, object> insertItemAction = null, Action<TItem> setDefaultBindingAction = null, ISupportInitialize supportInitialize = null, Action<TItem, object> linkItemWithSourceAction = null, bool useDefaultTemplateSelector = true, bool useDefaultTemplateValidation = true, Func<TItem, bool> customClear = null, bool forceBindingsProcessing = false)
        {
            ItemsAttachedBehaviorCore<TContainer, TItem>.BehaviorDestroy(itemsAttachedBehaviorStoreProperty, d);
            ItemsAttachedBehaviorCore<TContainer, TItem>.BehaviorInit(new ItemsAttachedBehaviorCore<TContainer, TItem>(getTargetFunction, createItemDelegate, itemGeneratorTemplateProperty, itemGeneratorTemplateSelectorProperty, itemGeneratorStyleProperty, itemGeneratorStyleSelectorProperty, newValue, setDefaultBindingAction, useDefaultTemplateSelector, useDefaultTemplateValidation, customClear, forceBindingsProcessing), itemsAttachedBehaviorStoreProperty, insertItemAction, supportInitialize, linkItemWithSourceAction, d);
        }

        private void OnSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (this.dispatcher.CheckAccess())
            {
                this.OnSourceCollectionChangedCore(sender, e);
            }
            else
            {
                this.dispatcher.Invoke(() => ((ItemsAttachedBehaviorCore<TContainer, TItem>) this).OnSourceCollectionChangedCore(sender, e));
            }
        }

        private void OnSourceCollectionChangedCore(object sender, NotifyCollectionChangedEventArgs e)
        {
            if ((this.Control != null) && ((this.Source != null) && !this.lockSynchronization))
            {
                this.lockSynchronization = true;
                this.UpdateCollection(() => SyncCollectionHelper.SyncCollection(e, ((ItemsAttachedBehaviorCore<TContainer, TItem>) this).Target, ((ItemsAttachedBehaviorCore<TContainer, TItem>) this).Source, new Func<object, object>(((ItemsAttachedBehaviorCore<TContainer, TItem>) this).CreateItem), ((ItemsAttachedBehaviorCore<TContainer, TItem>) this).insertItemAction, ((ItemsAttachedBehaviorCore<TContainer, TItem>) this).supportInitialize, new Action<object>(((ItemsAttachedBehaviorCore<TContainer, TItem>) this).ClearItem)));
                if ((this.Target != null) && (this.Target.Count == this.Source.Count))
                {
                    this.defaultTemplateNotifier.Reset();
                }
                this.lockSynchronization = false;
            }
        }

        public static void OnTargetItemPositionChanged(DependencyObject o, DependencyProperty itemsAttachedBehaviorStoreProperty, int oldPosition, int newPosition)
        {
            ItemsAttachedBehaviorCore<TContainer, TItem> behavior = ItemsAttachedBehaviorCore<TContainer, TItem>.GetBehavior(o, itemsAttachedBehaviorStoreProperty);
            if (behavior != null)
            {
                behavior.MoveSourceItem(oldPosition, newPosition);
            }
        }

        private void Populate()
        {
            this.defaultTemplateNotifier.Reset();
            this.UpdateCollection(() => SyncCollectionHelper.PopulateCore(base.Target, base.Source, new Func<object, object>(this.CreateItem), base.insertItemAction, base.supportInitialize, new Action<object>(this.ClearItem)));
        }

        private object ProcessItemFromTemplate(object item) => 
            (item as Freezable).Return<Freezable, object>(delegate (Freezable x) {
                Freezable freezable = x.Clone();
                base.DestroyFreezableBindings(x);
                return freezable;
            }, () => item);

        private void RecalcDefaultTemplate(object sender, EventArgs e)
        {
            this.Populate();
        }

        private void SetCanBeInheritanceContext(Freezable freezable, bool value)
        {
            ItemsAttachedBehaviorCore<TContainer, TItem>.reflectionHelper.SetPropertyValue(freezable, "CanBeInheritanceContext", value, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        }

        private void SubscribeSource()
        {
            INotifyCollectionChanged source = this.Source as INotifyCollectionChanged;
            if (source != null)
            {
                CollectionChangedEventManager.AddListener(source, this);
            }
        }

        bool IWeakEventListener.ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            if (!(managerType == typeof(CollectionChangedEventManager)))
            {
                return false;
            }
            this.OnSourceCollectionChanged(sender, (NotifyCollectionChangedEventArgs) e);
            return true;
        }

        private void UnsubscribeSource()
        {
            INotifyCollectionChanged source = this.Source as INotifyCollectionChanged;
            if (source != null)
            {
                CollectionChangedEventManager.RemoveListener(source, this);
            }
        }

        private void UpdateCollection(Action updateAction)
        {
            using (this.forceBindingsProcessing ? DataBindEngineHelper.CalcAndForcePostponedBindings() : null)
            {
                Freezable target = this.Target as Freezable;
                if (target == null)
                {
                    updateAction();
                }
                else
                {
                    this.SetCanBeInheritanceContext(target, false);
                    updateAction();
                    this.SetCanBeInheritanceContext(target, this.GetCanBeInheritanceContext(target));
                }
            }
        }

        private TContainer Control { get; set; }

        protected IList Source
        {
            get => 
                this.source;
            set
            {
                this.UnsubscribeSource();
                this.source = value;
                this.SubscribeSource();
            }
        }

        private IList Target =>
            (this.Control != null) ? this.getTargetFunction(this.Control) : null;

        [Flags]
        private enum ResetProperties
        {
            public const ItemsAttachedBehaviorCore<TContainer, TItem>.ResetProperties None = ItemsAttachedBehaviorCore<TContainer, TItem>.ResetProperties.None;,
            public const ItemsAttachedBehaviorCore<TContainer, TItem>.ResetProperties DataContext = ItemsAttachedBehaviorCore<TContainer, TItem>.ResetProperties.DataContext;,
            public const ItemsAttachedBehaviorCore<TContainer, TItem>.ResetProperties Style = ItemsAttachedBehaviorCore<TContainer, TItem>.ResetProperties.Style;
        }
    }
}

