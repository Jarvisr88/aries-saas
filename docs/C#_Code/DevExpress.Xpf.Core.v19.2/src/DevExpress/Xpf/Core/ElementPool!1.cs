namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class ElementPool<T> : IEnumerable<T>, IEnumerable where T: FrameworkElement, new()
    {
        private Style _ItemStyle;
        private List<T> UsedItems;

        public ElementPool(Panel container)
        {
            this.UsedItems = new List<T>();
            this.Container = container;
            this.Items = new List<T>();
        }

        public T Add()
        {
            T firstUnusedItem = this.GetFirstUnusedItem();
            if (firstUnusedItem == null)
            {
                firstUnusedItem = this.CreateItem();
                firstUnusedItem.Tag = this;
                this.Items.Add(firstUnusedItem);
                this.Container.Children.Add(firstUnusedItem);
            }
            this.MarkItemAsUsed(firstUnusedItem);
            return firstUnusedItem;
        }

        protected virtual T CreateItem()
        {
            T o = Activator.CreateInstance<T>();
            if (this.ItemStyle != null)
            {
                o.SetValueIfNotDefault(FrameworkElement.StyleProperty, this.ItemStyle);
            }
            return o;
        }

        protected virtual void DeleteItem(T item)
        {
        }

        public void DeleteUnusedItems()
        {
            for (int i = this.Items.Count - 1; i >= 0; i--)
            {
                T item = this.Items[i];
                if (!this.IsItemUsed(item))
                {
                    this.DeleteItem(item);
                    item.Tag = null;
                    this.Container.Children.Remove(item);
                    this.Items.RemoveAt(i);
                }
            }
        }

        protected T GetFirstUnusedItem()
        {
            T local2;
            using (List<T>.Enumerator enumerator = this.Items.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        T current = enumerator.Current;
                        if (this.IsItemUsed(current))
                        {
                            continue;
                        }
                        if (!this.IsItemInTree(current))
                        {
                            this.Container.Children.Add(current);
                        }
                        local2 = current;
                    }
                    else
                    {
                        return default(T);
                    }
                    break;
                }
            }
            return local2;
        }

        public int IndexOf(T item) => 
            this.Items.IndexOf(item);

        public bool IsItem(UIElement element) => 
            (element is T) && (((T) element).Tag == this);

        protected virtual bool IsItemInTree(T item) => 
            item.Parent != null;

        protected bool IsItemUsed(T item) => 
            this.UsedItems.Contains(item);

        protected void MarkItemAsUnused(T item)
        {
            this.UsedItems.Remove(item);
        }

        protected void MarkItemAsUsed(T item)
        {
            this.UsedItems.Add(item);
        }

        public void MarkItemsAsUnused()
        {
            foreach (T local in this.Items)
            {
                this.MarkItemAsUnused(local);
            }
        }

        protected virtual void OnItemStyleChanged()
        {
            foreach (T local in this.Items)
            {
                local.SetValueIfNotDefault(FrameworkElement.StyleProperty, this.ItemStyle);
            }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => 
            this.Items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => 
            this.Items.GetEnumerator();

        public T this[int index] =>
            this.Items[index];

        public Panel Container { get; private set; }

        public int Count =>
            this.Items.Count;

        public Style ItemStyle
        {
            get => 
                this._ItemStyle;
            set
            {
                if (!ReferenceEquals(this.ItemStyle, value))
                {
                    this._ItemStyle = value;
                    this.OnItemStyleChanged();
                }
            }
        }

        protected List<T> Items { get; private set; }
    }
}

