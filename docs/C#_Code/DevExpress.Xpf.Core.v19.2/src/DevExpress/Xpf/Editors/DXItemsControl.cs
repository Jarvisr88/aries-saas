namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;

    public class DXItemsControl : ItemsControl, IDXItemContainerGenerator
    {
        public const int InvalidHandle = -2147483648;
        public static readonly DependencyProperty IsLoopedProperty;
        private const int MaxRecycledCount = 20;

        public event EventHandler<DXItemsControlOutOfRangeItemEventArgs> ProcessOutOfRangeItem;

        static DXItemsControl()
        {
            Type ownerType = typeof(DXItemsControl);
            IsLoopedProperty = DependencyPropertyManager.Register("IsLooped", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, (d, e) => ((DXItemsControl) d).IsLoopedChanged((bool) e.NewValue)));
        }

        public DXItemsControl()
        {
            base.DefaultStyleKey = typeof(DXItemsControl);
            base.Loaded += new RoutedEventHandler(this.OnLoaded);
            this.Initialize();
        }

        protected bool CanGenerateItem(int index)
        {
            object obj2;
            if (index == -2147483648)
            {
                return false;
            }
            if ((index >= 0) && (index < this.GetItemsCount()))
            {
                return true;
            }
            if (this.IsLooped)
            {
                return false;
            }
            this.OutOfRangeData.TryGetValue(index, out obj2);
            bool flag2 = this.RaiseProcessOutOfRangeItem(index, ref obj2);
            if (flag2)
            {
                this.OutOfRangeData[index] = obj2;
            }
            return flag2;
        }

        protected override void ClearContainerForItemOverride(DependencyObject element, object item)
        {
            Action<FrameworkElement> action = <>c.<>9__52_0;
            if (<>c.<>9__52_0 == null)
            {
                Action<FrameworkElement> local1 = <>c.<>9__52_0;
                action = <>c.<>9__52_0 = x => x.ClearValue(FrameworkElement.StyleProperty);
            }
            (element as FrameworkElement).Do<FrameworkElement>(action);
            Action<ContentPresenter> action2 = <>c.<>9__52_1;
            if (<>c.<>9__52_1 == null)
            {
                Action<ContentPresenter> local2 = <>c.<>9__52_1;
                action2 = <>c.<>9__52_1 = delegate (ContentPresenter x) {
                    x.ClearValue(ContentPresenter.ContentProperty);
                    x.ClearValue(ContentPresenter.ContentTemplateProperty);
                };
            }
            (element as ContentPresenter).Do<ContentPresenter>(action2);
            Action<ContentControl> action3 = <>c.<>9__52_2;
            if (<>c.<>9__52_2 == null)
            {
                Action<ContentControl> local3 = <>c.<>9__52_2;
                action3 = <>c.<>9__52_2 = delegate (ContentControl x) {
                    x.ClearValue(ContentControl.ContentProperty);
                    x.ClearValue(ContentControl.ContentTemplateProperty);
                };
            }
            (element as ContentControl).Do<ContentControl>(action3);
        }

        void IDXItemContainerGenerator.ClearItemContainer(int index, UIElement container)
        {
            if (!this.Recycled.IsManipulating)
            {
                index = this.Panel.IndexCalculator.GetIndex(index, this.GetItemsCount(), this.IsLooped);
                object item = this.GetItem(index);
                this.ClearContainerForItemOverride(container, item);
            }
        }

        UIElement IDXItemContainerGenerator.Generate(int index, out bool isNew)
        {
            FrameworkElement element;
            isNew = false;
            if (this.Panel == null)
            {
                return null;
            }
            index = this.Panel.IndexCalculator.GetIndex(index, this.GetItemsCount(), this.IsLooped);
            if (!this.CanGenerateItem(index))
            {
                return null;
            }
            if (this.PreviousInternalIndex2Item.TryGetValue(index, out element))
            {
                this.PreviousInternalIndex2Item.Remove(index);
            }
            else if ((this.Recycled.Count > 0) && this.Recycled.Contains(index))
            {
                element = this.Recycled.Pop(index);
            }
            else
            {
                isNew = true;
                object item = this.GetItem(index);
                element = this.IsItemItsOwnContainerOverride(item) ? ((FrameworkElement) item) : ((FrameworkElement) this.GetContainerForItemOverride());
            }
            this.InternalIndex2Item.Add(index, element);
            return element;
        }

        UIElement IDXItemContainerGenerator.GetContainer(int index)
        {
            FrameworkElement element;
            if (this.GetItemsCount() == 0)
            {
                return null;
            }
            index = this.Panel.IndexCalculator.GetIndex(index, this.GetItemsCount(), this.IsLooped);
            return (!this.InternalIndex2Item.TryGetValue(index, out element) ? null : element);
        }

        void IDXItemContainerGenerator.PrepareItemContainer(int index, UIElement container)
        {
            index = this.Panel.IndexCalculator.GetIndex(index, this.GetItemsCount(), this.IsLooped);
            object item = this.GetItem(index);
            this.PrepareContainerForItemOverride(container, item);
        }

        void IDXItemContainerGenerator.RemoveItems()
        {
            if (this.Recycled.Count > 20)
            {
                IDXItemContainerGenerator generator = this;
                foreach (FrameworkElement element in this.Recycled)
                {
                    generator.ClearItemContainer(0, element);
                    this.Panel.Children.Remove(element);
                }
                this.Recycled.Reset();
            }
        }

        void IDXItemContainerGenerator.StartAt(int index)
        {
            if (this.IsInItemGeneration)
            {
                throw new ArgumentException("isinitemgeneration");
            }
            this.IsInItemGeneration = true;
            this.PreviousInternalIndex2Item.AddRange<int, FrameworkElement>(this.InternalIndex2Item);
            this.InternalIndex2Item.Clear();
        }

        void IDXItemContainerGenerator.StartManipulation()
        {
            this.Recycled.IsManipulating = true;
        }

        void IDXItemContainerGenerator.Stop()
        {
            if (!this.IsInItemGeneration)
            {
                throw new ArgumentException("!isinitemgeneration");
            }
            IDXItemContainerGenerator generator = this;
            this.IsInItemGeneration = false;
            foreach (KeyValuePair<int, FrameworkElement> pair in this.PreviousInternalIndex2Item)
            {
                generator.ClearItemContainer(pair.Key, pair.Value);
                this.Recycled.Push(pair.Key, pair.Value);
            }
            this.PreviousInternalIndex2Item.Clear();
        }

        void IDXItemContainerGenerator.StopManipulation()
        {
            this.Recycled.IsManipulating = false;
        }

        protected override DependencyObject GetContainerForItemOverride() => 
            new ContentPresenter();

        protected int GetIndex(object item) => 
            this.GetIndex(item, x => Equals(x, item));

        protected int GetIndex(object item, Func<object, bool> comparer)
        {
            int num = 0;
            while (true)
            {
                if (num >= this.GetItemsCount())
                {
                    Func<KeyValuePair<int, object>, bool> <>9__0;
                    Func<KeyValuePair<int, object>, bool> predicate = <>9__0;
                    if (<>9__0 == null)
                    {
                        Func<KeyValuePair<int, object>, bool> local1 = <>9__0;
                        predicate = <>9__0 = pair => comparer(pair.Value);
                    }
                    using (IEnumerator<KeyValuePair<int, object>> enumerator = this.OutOfRangeData.Where<KeyValuePair<int, object>>(predicate).GetEnumerator())
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        return enumerator.Current.Key;
                    }
                }
                if (comparer(base.Items[num]))
                {
                    return num;
                }
                num++;
            }
            return -2147483648;
        }

        protected object GetItem(int index)
        {
            object obj2;
            if (index == -2147483648)
            {
                return null;
            }
            bool flag = (index >= 0) && (index < this.GetItemsCount());
            return (!(this.IsLooped | flag) ? (!this.OutOfRangeData.TryGetValue(index, out obj2) ? null : obj2) : base.Items[index]);
        }

        public int GetItemsCount() => 
            base.Items.Count;

        protected DataTemplate GetItemTemplate(object item) => 
            (base.ItemTemplateSelector != null) ? base.ItemTemplateSelector.SelectTemplate(item, this) : base.ItemTemplate;

        public static LoopedPanel GetPanelFromItemsControl(DXItemsControl itemsControl) => 
            itemsControl.Panel;

        private void Initialize()
        {
            this.InternalIndex2Item = new Dictionary<int, FrameworkElement>();
            this.PreviousInternalIndex2Item = new Dictionary<int, FrameworkElement>();
            this.Recycled = new RecycledCollection<FrameworkElement>();
            this.OutOfRangeData = new Dictionary<int, object>();
        }

        protected void InvalidatePanel()
        {
            Action<LoopedPanel> action = <>c.<>9__55_0;
            if (<>c.<>9__55_0 == null)
            {
                Action<LoopedPanel> local1 = <>c.<>9__55_0;
                action = <>c.<>9__55_0 = x => x.InvalidatePanel();
            }
            this.Panel.Do<LoopedPanel>(action);
        }

        protected override bool IsItemItsOwnContainerOverride(object item) => 
            item is ContentPresenter;

        protected virtual void IsLoopedChanged(bool newValue)
        {
            if (this.Panel != null)
            {
                this.Panel.IsLooped = newValue;
            }
        }

        protected virtual void ItemContainerStyleChanged(Style newValue)
        {
            this.InvalidatePanel();
        }

        protected virtual void ItemTemplateChanged(DataTemplate template)
        {
        }

        protected virtual void ItemTemplateSelectorChanged(DataTemplateSelector newValue)
        {
            this.InvalidatePanel();
        }

        public override void OnApplyTemplate()
        {
            this.ScrollViewer.Do<DXScrollViewer>(delegate (DXScrollViewer x) {
                x.ScrollChanged -= new ScrollChangedEventHandler(this.OnScrollChanged);
                x.ViewChanged -= new ViewChangedEventHandler(this.OnViewChanged);
            });
            base.OnApplyTemplate();
            Predicate<FrameworkElement> predicate = <>c.<>9__40_1;
            if (<>c.<>9__40_1 == null)
            {
                Predicate<FrameworkElement> local1 = <>c.<>9__40_1;
                predicate = <>c.<>9__40_1 = element => element is DXScrollViewer;
            }
            this.ScrollViewer = (DXScrollViewer) LayoutHelper.FindElement(this, predicate);
            this.ScrollViewer.Do<DXScrollViewer>(delegate (DXScrollViewer x) {
                x.ScrollChanged += new ScrollChangedEventHandler(this.OnScrollChanged);
                x.ViewChanged += new ViewChangedEventHandler(this.OnViewChanged);
                x.IsLooped = this.IsLooped;
            });
            if (this.ScrollViewer != null)
            {
                this.Panel = this.ScrollViewer.Content as LoopedPanel;
                if (this.Panel != null)
                {
                    this.Panel.ScrollOwner = this.ScrollViewer;
                    this.Panel.IsLooped = this.IsLooped;
                }
            }
            else
            {
                Predicate<FrameworkElement> predicate2 = <>c.<>9__40_3;
                if (<>c.<>9__40_3 == null)
                {
                    Predicate<FrameworkElement> local2 = <>c.<>9__40_3;
                    predicate2 = <>c.<>9__40_3 = element => element is LoopedPanel;
                }
                this.Panel = (LoopedPanel) LayoutHelper.FindElement(this, predicate2);
            }
            if (this.Panel == null)
            {
                throw new NotSupportedException();
            }
            this.Panel.ItemsContainerGenerator = this;
        }

        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            base.OnItemsSourceChanged(oldValue, newValue);
            this.OutOfRangeData.Clear();
            this.InvalidatePanel();
        }

        protected virtual void OnLoaded(object sender, RoutedEventArgs e)
        {
        }

        protected virtual void OnScrollChanged(object sender, ScrollChangedEventArgs e)
        {
        }

        protected virtual void OnViewChanged(object sender, ViewChangedEventArgs e)
        {
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            (element as FrameworkElement).If<FrameworkElement>(x => ((base.ItemContainerStyle != null) && (x.GetType() == base.ItemContainerStyle.TargetType))).Do<FrameworkElement>(x => x.Style = base.ItemContainerStyle);
            (element as ContentPresenter).Do<ContentPresenter>(delegate (ContentPresenter x) {
                x.Content = item;
                x.ContentTemplate = this.GetItemTemplate(item);
            });
            (element as ContentControl).Do<ContentControl>(delegate (ContentControl x) {
                x.Content = item;
                x.ContentTemplate = this.GetItemTemplate(item);
            });
        }

        private bool RaiseProcessOutOfRangeItem(int index, ref object item)
        {
            item = null;
            if (this.ProcessOutOfRangeItem == null)
            {
                return false;
            }
            DXItemsControlOutOfRangeItemEventArgs e = new DXItemsControlOutOfRangeItemEventArgs(index);
            this.ProcessOutOfRangeItem(this, e);
            if (!e.Handled)
            {
                return false;
            }
            item = e.Item;
            return true;
        }

        public bool IsLooped
        {
            get => 
                (bool) base.GetValue(IsLoopedProperty);
            set => 
                base.SetValue(IsLoopedProperty, value);
        }

        private bool IsInItemGeneration { get; set; }

        private Dictionary<int, object> OutOfRangeData { get; set; }

        private Dictionary<int, FrameworkElement> InternalIndex2Item { get; set; }

        private Dictionary<int, FrameworkElement> PreviousInternalIndex2Item { get; set; }

        private RecycledCollection<FrameworkElement> Recycled { get; set; }

        protected LoopedPanel Panel { get; private set; }

        protected DXScrollViewer ScrollViewer { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DXItemsControl.<>c <>9 = new DXItemsControl.<>c();
            public static Predicate<FrameworkElement> <>9__40_1;
            public static Predicate<FrameworkElement> <>9__40_3;
            public static Action<FrameworkElement> <>9__52_0;
            public static Action<ContentPresenter> <>9__52_1;
            public static Action<ContentControl> <>9__52_2;
            public static Action<LoopedPanel> <>9__55_0;

            internal void <.cctor>b__2_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DXItemsControl) d).IsLoopedChanged((bool) e.NewValue);
            }

            internal void <ClearContainerForItemOverride>b__52_0(FrameworkElement x)
            {
                x.ClearValue(FrameworkElement.StyleProperty);
            }

            internal void <ClearContainerForItemOverride>b__52_1(ContentPresenter x)
            {
                x.ClearValue(ContentPresenter.ContentProperty);
                x.ClearValue(ContentPresenter.ContentTemplateProperty);
            }

            internal void <ClearContainerForItemOverride>b__52_2(ContentControl x)
            {
                x.ClearValue(ContentControl.ContentProperty);
                x.ClearValue(ContentControl.ContentTemplateProperty);
            }

            internal void <InvalidatePanel>b__55_0(LoopedPanel x)
            {
                x.InvalidatePanel();
            }

            internal bool <OnApplyTemplate>b__40_1(FrameworkElement element) => 
                element is DXScrollViewer;

            internal bool <OnApplyTemplate>b__40_3(FrameworkElement element) => 
                element is LoopedPanel;
        }
    }
}

