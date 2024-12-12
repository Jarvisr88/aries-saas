namespace DevExpress.Xpf.Core
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class CachedItemsControl : ItemsControlBase
    {
        private Dictionary<object, FrameworkElement> cache = new Dictionary<object, FrameworkElement>();
        private Guid lastCacheVersion = Guid.Empty;
        private bool shouldPurgeCache;

        protected virtual void AssignVisibleIndex(FrameworkElement presenter, object item)
        {
            if (item is ISupportVisibleIndex)
            {
                OrderPanelBase.SetVisibleIndex(presenter, ((ISupportVisibleIndex) item).VisibleIndex);
            }
        }

        protected virtual bool CanUseElementFromCache(FrameworkElement element) => 
            true;

        private void ClearCache()
        {
            this.cache.Clear();
        }

        private void ClearCacheAndInvalidateMeasure()
        {
            this.ClearCache();
            base.InvalidateMeasure();
        }

        protected override Size MeasureOverride(Size constraint)
        {
            this.ValidateCache();
            return base.MeasureOverride(constraint);
        }

        protected override void OnItemsSourceChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnItemsSourceChanged(e);
            if ((base.ItemsSource == null) || !(base.ItemsSource is ISupportCacheVersion))
            {
                this.shouldPurgeCache = true;
            }
        }

        protected override void OnItemTemplateChanged()
        {
            base.OnItemTemplateChanged();
            this.ClearCacheAndInvalidateMeasure();
        }

        private void ValidateCache()
        {
            ISupportCacheVersion itemsSource = base.ItemsSource as ISupportCacheVersion;
            if ((itemsSource != null) && (itemsSource.CacheVersion != this.lastCacheVersion))
            {
                this.ClearCache();
                this.lastCacheVersion = itemsSource.CacheVersion;
            }
        }

        protected virtual void ValidateElement(FrameworkElement element, object item)
        {
        }

        protected override void ValidateVisualTree()
        {
            if (base.ItemsSource != null)
            {
                int index = 0;
                Dictionary<object, FrameworkElement> dictionary = null;
                Dictionary<object, FrameworkElement> dictionary2 = null;
                if (this.shouldPurgeCache)
                {
                    dictionary = new Dictionary<object, FrameworkElement>();
                    dictionary2 = new Dictionary<object, FrameworkElement>(this.cache);
                }
                foreach (object obj2 in base.ItemsSource)
                {
                    FrameworkElement element;
                    if (!this.cache.TryGetValue(obj2, out element) || !this.CanUseElementFromCache(element))
                    {
                        element = this.CreateChild(obj2);
                        this.cache[obj2] = element;
                    }
                    else if (this.shouldPurgeCache)
                    {
                        dictionary.Add(obj2, null);
                    }
                    this.AssignVisibleIndex(element, obj2);
                    int num2 = base.Panel.Children.IndexOf(element);
                    if (num2 >= 0)
                    {
                        base.Panel.Children.RemoveRange(index, num2 - index);
                    }
                    else
                    {
                        Panel parent = (Panel) VisualTreeHelper.GetParent(element);
                        if (parent != null)
                        {
                            parent.Children.Remove(element);
                        }
                        base.Panel.Children.Insert(index, element);
                    }
                    index++;
                    this.ValidateElement(element, obj2);
                }
                base.Panel.Children.RemoveRange(index, this.ActualChildrenCount - index);
                if (this.shouldPurgeCache)
                {
                    foreach (object obj3 in dictionary2.Keys)
                    {
                        if (!dictionary.ContainsKey(obj3) && dictionary2.ContainsKey(obj3))
                        {
                            this.cache.Remove(obj3);
                        }
                    }
                    this.shouldPurgeCache = false;
                }
            }
        }

        protected virtual int ActualChildrenCount =>
            base.Panel.Children.Count;
    }
}

