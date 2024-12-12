namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    [DXToolboxBrowsable(false)]
    public abstract class psvContentControlBase : ContentControl, IDisposable
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        internal static readonly DependencyProperty ActualSizeProperty;
        private static readonly DependencyPropertyKey LayoutItemPropertyKey;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty LayoutItemProperty;

        static psvContentControlBase()
        {
            DependencyPropertyRegistrator<psvContentControlBase> registrator = new DependencyPropertyRegistrator<psvContentControlBase>();
            registrator.Register<Size>("ActualSize", ref ActualSizeProperty, Size.Empty, (dObj, e) => ((psvContentControlBase) dObj).OnActualSizeChanged((Size) e.NewValue), null);
            registrator.RegisterReadonly<BaseLayoutItem>("LayoutItem", ref LayoutItemPropertyKey, ref LayoutItemProperty, null, (dObj, e) => ((psvContentControlBase) dObj).OnLayoutItemChanged((BaseLayoutItem) e.NewValue, (BaseLayoutItem) e.OldValue), null);
        }

        public psvContentControlBase()
        {
            base.SetCurrentValue(UIElement.FocusableProperty, false);
            base.Loaded += new RoutedEventHandler(this.psvContentControl_Loaded);
            base.Unloaded += new RoutedEventHandler(this.psvContentControl_Unloaded);
        }

        public void Dispose()
        {
            if (!this.IsDisposing)
            {
                this.IsDisposing = true;
                base.Loaded -= new RoutedEventHandler(this.psvContentControl_Loaded);
                base.Unloaded -= new RoutedEventHandler(this.psvContentControl_Unloaded);
                base.ClearValue(ActualSizeProperty);
                this.OnDispose();
                base.ClearValue(LayoutItemPropertyKey);
                base.ClearValue(ContentControl.ContentProperty);
                DockLayoutManager.Release(this);
                this.Container = null;
            }
            GC.SuppressFinalize(this);
        }

        public static void EnsureContentElement<T>(DependencyObject element, ContentPresenter presenter) where T: psvContentControlBase
        {
            if (element != null)
            {
                T templateParent = LayoutItemsHelper.GetTemplateParent<T>(presenter);
                if (templateParent != null)
                {
                    templateParent.EnsureContentElementCore(element);
                }
            }
        }

        protected virtual void EnsureContentElementCore(DependencyObject element)
        {
        }

        private void EnsureUIElements(BaseLayoutItem item, BaseLayoutItem oldItem)
        {
            IUIElement element = this as IUIElement;
            if (element != null)
            {
                if (oldItem != null)
                {
                    oldItem.UIElements.Remove(element);
                }
                if (item != null)
                {
                    item.UIElements.Add(element);
                }
            }
        }

        protected virtual void OnActualSizeChanged(Size value)
        {
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.Container = DockLayoutManager.Ensure(this, false);
        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            this.LayoutItem = LayoutItemData.ConvertToBaseLayoutItem(newContent);
            base.OnContentChanged(oldContent, newContent);
        }

        protected virtual void OnDispose()
        {
            this.Unsubscribe(this.LayoutItem);
        }

        protected virtual void OnLayoutItemChanged(BaseLayoutItem item, BaseLayoutItem oldItem)
        {
            this.Unsubscribe(oldItem);
            if (item == null)
            {
                base.ClearValue(DockLayoutManager.LayoutItemProperty);
            }
            else
            {
                base.SetValue(DockLayoutManager.LayoutItemProperty, item);
            }
            this.EnsureUIElements(item, oldItem);
            this.Subscribe(item);
        }

        protected virtual void OnLoaded()
        {
        }

        protected sealed override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.SetValue(ActualSizeProperty, sizeInfo.NewSize);
            base.OnRenderSizeChanged(sizeInfo);
        }

        protected virtual void OnUnloaded()
        {
        }

        private void psvContentControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.OnLoaded();
        }

        private void psvContentControl_Unloaded(object sender, RoutedEventArgs e)
        {
            this.OnUnloaded();
        }

        protected virtual void Subscribe(BaseLayoutItem item)
        {
        }

        protected virtual void Unsubscribe(BaseLayoutItem item)
        {
        }

        public bool IsDisposing { get; private set; }

        public BaseLayoutItem LayoutItem
        {
            get => 
                (BaseLayoutItem) base.GetValue(LayoutItemProperty);
            internal set => 
                base.SetValue(LayoutItemPropertyKey, value);
        }

        protected DockLayoutManager Container { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly psvContentControlBase.<>c <>9 = new psvContentControlBase.<>c();

            internal void <.cctor>b__3_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((psvContentControlBase) dObj).OnActualSizeChanged((Size) e.NewValue);
            }

            internal void <.cctor>b__3_1(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((psvContentControlBase) dObj).OnLayoutItemChanged((BaseLayoutItem) e.NewValue, (BaseLayoutItem) e.OldValue);
            }
        }
    }
}

