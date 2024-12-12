namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    public class LayoutTabFastRenderPanel : psvPanel
    {
        public static readonly DependencyProperty ArrangeAllCachedTabsProperty;
        private readonly Dictionary<object, FastRenderPanelContentControl> itemsHash = new Dictionary<object, FastRenderPanelContentControl>();
        private FastRenderPanelContentControl SelectedContent;
        private DevExpress.Xpf.Core.TabContentCacheMode tabContentCacheMode;

        static LayoutTabFastRenderPanel()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(LayoutTabFastRenderPanel), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            DependencyPropertyRegistrator<LayoutTabFastRenderPanel>.New().Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<LayoutTabFastRenderPanel, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(LayoutTabFastRenderPanel.get_ArrangeAllCachedTabs)), parameters), out ArrangeAllCachedTabsProperty, false, 2);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (this.SelectedContent == null)
            {
                return base.ArrangeOverride(finalSize);
            }
            if ((this.TabContentCacheMode == DevExpress.Xpf.Core.TabContentCacheMode.None) || !this.ArrangeAllCachedTabs)
            {
                this.SelectedContent.Arrange(new Rect(finalSize));
            }
            else
            {
                foreach (UIElement element in base.Children)
                {
                    element.Arrange(new Rect(finalSize));
                }
            }
            return finalSize;
        }

        private void ClearChildren()
        {
            this.ClearItemsHash();
            base.Children.Clear();
            this.SelectedContent = null;
        }

        private void ClearItemsHash()
        {
            foreach (FastRenderPanelContentControl control in this.itemsHash.Values)
            {
                control.ClearValue(ContentControl.ContentProperty);
                base.Children.Remove(control);
            }
            this.itemsHash.Clear();
        }

        private void HidePresenter(FrameworkElement presenter)
        {
            if (presenter != null)
            {
                presenter.Visibility = (this.TabContentCacheMode == DevExpress.Xpf.Core.TabContentCacheMode.CacheAllTabs) ? Visibility.Hidden : Visibility.Collapsed;
            }
        }

        public void Initialize(LayoutTabControl owner, DevExpress.Xpf.Core.TabContentCacheMode cacheMode)
        {
            this.TabContentCacheMode = cacheMode;
            if (!ReferenceEquals(this.Owner, owner))
            {
                this.UnsubscribeOwner();
                this.Owner = owner;
                this.SubscribeOwner();
            }
            this.SyncItems(null, this.Owner.Items);
            this.SyncSelection(null, this.Owner.SelectedContent);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            if (this.SelectedContent == null)
            {
                return base.MeasureOverride(availableSize);
            }
            if (this.TabContentCacheMode == DevExpress.Xpf.Core.TabContentCacheMode.None)
            {
                this.SelectedContent.Measure(availableSize);
            }
            else
            {
                foreach (UIElement element in base.Children)
                {
                    element.Measure(availableSize);
                }
            }
            return this.SelectedContent.DesiredSize;
        }

        protected override void OnDispose()
        {
            this.UnsubscribeOwner();
            this.ClearChildren();
            base.OnDispose();
        }

        private void OnTabContentCacheModeChanged()
        {
            this.ClearChildren();
        }

        private void Owner_ItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                this.ClearChildren();
                this.SyncItems(null, this.Owner.Items);
                this.SyncSelection(null, this.Owner.SelectedContent);
            }
            else
            {
                this.SyncItems(e.OldItems, e.NewItems);
                this.SyncSelection(null, this.Owner.SelectedContent);
                base.InvalidateMeasure();
            }
        }

        private void Owner_SelectionChanged(object sender, LayoutTabControlSelectionChangedEventArgs e)
        {
            this.SyncItems(null, this.Owner.Items);
            this.SyncSelection(e.OldContent, e.NewContent);
            base.InvalidateMeasure();
        }

        protected virtual void SubscribeOwner()
        {
            if (this.Owner != null)
            {
                this.Owner.ItemsChanged += new NotifyCollectionChangedEventHandler(this.Owner_ItemsChanged);
                this.Owner.SelectionChanged += new LayoutTabControlSelectionChangedEventHandler(this.Owner_SelectionChanged);
                this.Owner.Forward(this, ArrangeAllCachedTabsProperty, LayoutTabControl.ArrangeAllCachedTabsProperty, BindingMode.OneWay);
            }
        }

        private void SyncItems(IList oldItems, IList newItems)
        {
            if (oldItems != null)
            {
                foreach (object obj2 in oldItems)
                {
                    if (this.itemsHash.ContainsKey(obj2))
                    {
                        FastRenderPanelContentControl element = this.itemsHash[obj2];
                        element.ClearValue(ContentControl.ContentProperty);
                        base.Children.Remove(element);
                        this.itemsHash.Remove(obj2);
                    }
                }
            }
            if (newItems != null)
            {
                foreach (object obj3 in newItems)
                {
                    if (!this.itemsHash.ContainsKey(obj3))
                    {
                        FastRenderPanelContentControl control1 = new FastRenderPanelContentControl();
                        control1.Content = obj3;
                        control1.ContentTemplate = this.Owner.SelectedContentTemplate;
                        FastRenderPanelContentControl control2 = control1;
                        this.itemsHash.Add(obj3, control2);
                        base.Children.Add(control2);
                        this.HidePresenter(control2);
                    }
                }
            }
        }

        private void SyncSelection(object oldContent, object content)
        {
            if ((content == null) || !this.itemsHash.ContainsKey(content))
            {
                this.HidePresenter(this.SelectedContent);
                this.SelectedContent = null;
            }
            else
            {
                FastRenderPanelContentControl objB = this.itemsHash[content];
                if (this.SelectedContent != null)
                {
                    SetZIndex(this.SelectedContent, 0);
                    if (!ReferenceEquals(this.SelectedContent, objB))
                    {
                        this.HidePresenter(this.SelectedContent);
                    }
                }
                this.SelectedContent = objB;
                SetZIndex(this.SelectedContent, 1);
                this.SelectedContent.Visibility = Visibility.Visible;
            }
        }

        protected virtual void UnsubscribeOwner()
        {
            if (this.Owner != null)
            {
                this.Owner.ItemsChanged -= new NotifyCollectionChangedEventHandler(this.Owner_ItemsChanged);
                this.Owner.SelectionChanged -= new LayoutTabControlSelectionChangedEventHandler(this.Owner_SelectionChanged);
                base.ClearValue(ArrangeAllCachedTabsProperty);
            }
        }

        public bool ArrangeAllCachedTabs
        {
            get => 
                (bool) base.GetValue(ArrangeAllCachedTabsProperty);
            set => 
                base.SetValue(ArrangeAllCachedTabsProperty, value);
        }

        public LayoutTabControl Owner { get; private set; }

        public DevExpress.Xpf.Core.TabContentCacheMode TabContentCacheMode
        {
            get => 
                this.tabContentCacheMode;
            set
            {
                if (this.tabContentCacheMode != value)
                {
                    this.tabContentCacheMode = value;
                    this.OnTabContentCacheModeChanged();
                }
            }
        }

        private class FastRenderPanelContentControl : ContentControl
        {
            public FastRenderPanelContentControl()
            {
                base.Focusable = false;
                ContentControlHelper.SetContentIsNotLogical(this, true);
            }
        }
    }
}

