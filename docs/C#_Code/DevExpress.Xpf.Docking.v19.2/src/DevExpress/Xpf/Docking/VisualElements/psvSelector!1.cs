namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using System;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Data;

    public class psvSelector<T> : psvItemsControl where T: class
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty ItemsSourceInternalProperty;
        public static readonly DependencyProperty SelectedItemProperty;
        public static readonly DependencyProperty SelectedIndexProperty;
        private int syncWithCurrent;
        private int deferredSelectedIndex;

        static psvSelector()
        {
            DependencyPropertyRegistrator<psvSelector<T>> registrator = new DependencyPropertyRegistrator<psvSelector<T>>();
            T defValue = default(T);
            registrator.Register<T>("SelectedItem", ref psvSelector<T>.SelectedItemProperty, defValue, (dObj, e) => ((psvSelector<T>) dObj).OnSelectedItemChanged((T) e.NewValue, (T) e.OldValue), (CoerceValueCallback) ((dObj, value) => ((psvSelector<T>) dObj).CoerceSelectedItem((T) value)));
            registrator.Register<int>("SelectedIndex", ref psvSelector<T>.SelectedIndexProperty, -1, (dObj, e) => ((psvSelector<T>) dObj).OnSelectedIndexChanged((int) e.NewValue, (int) e.OldValue), (dObj, value) => ((psvSelector<T>) dObj).CoerceSelectedIndex((int) value));
            registrator.Register<IEnumerable>("ItemsSourceInternal", ref psvSelector<T>.ItemsSourceInternalProperty, null, (dObj, e) => ((psvSelector<T>) dObj).OnItemsSourceInternalChanged(), null);
        }

        public psvSelector()
        {
            this.deferredSelectedIndex = -1;
            this.StartListen(psvSelector<T>.ItemsSourceInternalProperty, "ItemsSource", BindingMode.OneWay);
        }

        private void ChangeSelection(T item, bool selected)
        {
            psvSelectorItem item2 = base.ItemContainerGenerator.ContainerFromItem(item) as psvSelectorItem;
            if (item2 != null)
            {
                item2.IsSelected = selected;
            }
        }

        protected void CheckSelectedItemRemoved(NotifyCollectionChangedEventArgs e)
        {
            if ((e.Action == NotifyCollectionChangedAction.Remove) && e.OldItems.Contains(this.SelectedItem))
            {
                if (this.IsValidIndex(this.SelectedIndex))
                {
                    base.SetValue(psvSelector<T>.SelectedItemProperty, base.Items[this.SelectedIndex]);
                }
                else
                {
                    base.ClearValue(psvSelector<T>.SelectedItemProperty);
                }
            }
        }

        protected sealed override void ClearContainer(DependencyObject element)
        {
            psvSelectorItem selectorItem = element as psvSelectorItem;
            if (selectorItem != null)
            {
                selectorItem.IsSelected = false;
                this.ClearSelectorItem(selectorItem);
                selectorItem.Dispose();
            }
        }

        protected virtual void ClearSelectorItem(psvSelectorItem selectorItem)
        {
        }

        protected virtual object CoerceSelectedIndex(int index) => 
            !this.TrySetDeferredSelectedIndex(ref index) ? ((this.syncWithCurrent <= 0) ? ((this.IsValidIndex(index) || this.AllowsInvalidSelectedIndex) ? index : (((base.Items.Count <= 0) || (this.SelectedIndex == index)) ? base.Items.IndexOf(this.SelectedItem) : this.SelectedIndex)) : this.GetCurrentPosition()) : index;

        protected virtual T CoerceSelectedItem(T item)
        {
            if (this.syncWithCurrent > 0)
            {
                return this.GetCurrentItem();
            }
            if (base.Items.Contains(item))
            {
                return item;
            }
            if (base.Items.Contains(this.SelectedItem))
            {
                return this.SelectedItem;
            }
            if (this.IsValidIndex(this.SelectedIndex))
            {
                return (T) base.Items[this.SelectedIndex];
            }
            return default(T);
        }

        protected virtual psvSelectorItem CreateSelectorItem() => 
            new psvSelectorItem();

        protected sealed override DependencyObject GetContainerForItemOverride() => 
            this.CreateSelectorItem();

        private T GetCurrentItem() => 
            (T) base.Items.CurrentItem;

        private int GetCurrentPosition() => 
            base.Items.CurrentPosition;

        protected sealed override bool IsItemItsOwnContainerOverride(object item) => 
            item is psvSelectorItem;

        private bool IsValidIndex(int index) => 
            (index >= 0) && (index < base.Items.Count);

        private bool IsValidItem(T item, out int index)
        {
            index = base.Items.IndexOf(item);
            return (index != -1);
        }

        protected override void OnDispose()
        {
            this.deferredSelectedIndex = -1;
            base.OnDispose();
        }

        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);
            this.CheckSelectedItemRemoved(e);
        }

        private void OnItemsSourceInternalChanged()
        {
            if (this.syncWithCurrent <= 0)
            {
                bool flag = this.IsValidIndex(this.deferredSelectedIndex);
                if (!flag)
                {
                    this.syncWithCurrent++;
                }
                base.CoerceValue(psvSelector<T>.SelectedIndexProperty);
                base.CoerceValue(psvSelector<T>.SelectedItemProperty);
                this.deferredSelectedIndex = -1;
                if (!flag)
                {
                    this.syncWithCurrent--;
                }
            }
        }

        protected virtual void OnSelectedIndexChanged(int index, int oldIndex)
        {
            if (this.IsValidIndex(index))
            {
                base.SetValue(psvSelector<T>.SelectedItemProperty, base.Items[index]);
            }
            else
            {
                base.ClearValue(psvSelector<T>.SelectedItemProperty);
            }
        }

        protected virtual void OnSelectedItemChanged(T item, T oldItem)
        {
            int num;
            this.ChangeSelection(oldItem, false);
            if (!this.IsValidItem(item, out num))
            {
                base.CoerceValue(psvSelector<T>.SelectedIndexProperty);
            }
            else
            {
                this.ChangeSelection(oldItem, false);
                base.SetValue(psvSelector<T>.SelectedIndexProperty, num);
                this.ChangeSelection(item, true);
            }
        }

        protected sealed override void PrepareContainer(DependencyObject element, object item)
        {
            base.PrepareContainer(element, item);
            psvSelectorItem selectorItem = element as psvSelectorItem;
            if (selectorItem != null)
            {
                selectorItem.Content = item;
                selectorItem.IsSelected = Equals(this.SelectedItem, item);
                this.PrepareSelectorItem(selectorItem, (T) item);
            }
        }

        protected virtual void PrepareSelectorItem(psvSelectorItem selectorItem, T item)
        {
        }

        private bool TrySetDeferredSelectedIndex(ref int index)
        {
            if (!base.IsDisposing)
            {
                if (this.deferredSelectedIndex == -1)
                {
                    if (base.ItemsSource == null)
                    {
                        this.deferredSelectedIndex = index;
                        index = -1;
                        return true;
                    }
                }
                else if (base.ItemsSource != null)
                {
                    index = this.deferredSelectedIndex;
                    this.deferredSelectedIndex = -1;
                    return true;
                }
            }
            return false;
        }

        protected virtual bool AllowsInvalidSelectedIndex =>
            false;

        public T SelectedItem
        {
            get => 
                (T) base.GetValue(psvSelector<T>.SelectedItemProperty);
            set => 
                base.SetValue(psvSelector<T>.SelectedItemProperty, value);
        }

        public int SelectedIndex
        {
            get => 
                (int) base.GetValue(psvSelector<T>.SelectedIndexProperty);
            set => 
                base.SetValue(psvSelector<T>.SelectedIndexProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly psvSelector<T>.<>c <>9;

            static <>c()
            {
                psvSelector<T>.<>c.<>9 = new psvSelector<T>.<>c();
            }

            internal void <.cctor>b__3_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((psvSelector<T>) dObj).OnSelectedItemChanged((T) e.NewValue, (T) e.OldValue);
            }

            internal object <.cctor>b__3_1(DependencyObject dObj, object value) => 
                ((psvSelector<T>) dObj).CoerceSelectedItem((T) value);

            internal void <.cctor>b__3_2(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((psvSelector<T>) dObj).OnSelectedIndexChanged((int) e.NewValue, (int) e.OldValue);
            }

            internal object <.cctor>b__3_3(DependencyObject dObj, object value) => 
                ((psvSelector<T>) dObj).CoerceSelectedIndex((int) value);

            internal void <.cctor>b__3_4(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((psvSelector<T>) dObj).OnItemsSourceInternalChanged();
            }
        }
    }
}

