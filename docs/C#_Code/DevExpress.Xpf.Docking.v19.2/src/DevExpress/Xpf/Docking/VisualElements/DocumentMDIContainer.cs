namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Base;
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Media;

    [TemplatePart(Name="PART_MDIPanel", Type=typeof(MDIPanel))]
    public class DocumentMDIContainer : psvItemsControl
    {
        public static readonly DependencyProperty LayoutItemProperty;
        private static readonly DependencyPropertyKey HasMaximizedDocumentsPropertyKey;
        public static readonly DependencyProperty HasMaximizedDocumentsProperty;
        private static readonly DependencyPropertyKey IsHeaderVisiblePropertyKey;
        public static readonly DependencyProperty IsHeaderVisibleProperty;
        public static readonly DependencyProperty ThemeDependentBackgroundProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty BackgroundInternalProperty;
        public static readonly DependencyProperty ActualBackgroundProperty;

        static DocumentMDIContainer()
        {
            DependencyPropertyRegistrator<DocumentMDIContainer> registrator = new DependencyPropertyRegistrator<DocumentMDIContainer>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.Register<BaseLayoutItem>("LayoutItem", ref LayoutItemProperty, null, (dObj, e) => ((DocumentMDIContainer) dObj).OnLayoutItemChanged((BaseLayoutItem) e.OldValue, (BaseLayoutItem) e.NewValue), null);
            registrator.RegisterReadonly<bool>("HasMaximizedDocuments", ref HasMaximizedDocumentsPropertyKey, ref HasMaximizedDocumentsProperty, false, (dObj, e) => ((DocumentMDIContainer) dObj).OnHasMaximizedDocumentsChanged((bool) e.NewValue), (CoerceValueCallback) ((dObj, value) => ((DocumentMDIContainer) dObj).CoerceHasMaximizedDocumentsVisible((bool) value)));
            registrator.RegisterReadonly<bool>("IsHeaderVisible", ref IsHeaderVisiblePropertyKey, ref IsHeaderVisibleProperty, false, null, null);
            registrator.Register<Brush>("ThemeDependentBackground", ref ThemeDependentBackgroundProperty, null, (dObj, e) => ((DocumentMDIContainer) dObj).OnBackgroundChanged((Brush) e.NewValue), null);
            registrator.Register<Brush>("BackgroundInternal", ref BackgroundInternalProperty, null, (dObj, e) => ((DocumentMDIContainer) dObj).OnBackgroundChanged((Brush) e.NewValue), null);
            registrator.Register<Brush>("ActualBackground", ref ActualBackgroundProperty, null, null, (dObj, value) => ((DocumentMDIContainer) dObj).CoerceActualBackground((Brush) value));
        }

        public DocumentMDIContainer()
        {
            this.StartListen(BackgroundInternalProperty, "Background", BindingMode.OneWay);
        }

        protected override void ClearContainer(DependencyObject element)
        {
            if (element is BaseLayoutItem)
            {
                ((BaseLayoutItem) element).ClearTemplate();
            }
            base.CoerceValue(HasMaximizedDocumentsProperty);
            base.ClearContainer(element);
        }

        protected virtual object CoerceActualBackground(Brush value) => 
            base.Background ?? this.ThemeDependentBackground;

        protected virtual bool CoerceHasMaximizedDocumentsVisible(bool baseValue)
        {
            bool flag;
            using (IEnumerator enumerator = ((IEnumerable) base.Items).GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        DependencyObject current = (DependencyObject) enumerator.Current;
                        if (MDIStateHelper.GetMDIState(current) != MDIState.Maximized)
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        public UIElement GetActiveDocument() => 
            this.PartMDIPanel?.GetActiveDocument();

        protected override DependencyObject GetContainerForItemOverride() => 
            new MDIDocument();

        protected override bool IsItemItsOwnContainerOverride(object item) => 
            (item is BaseLayoutItem) || (item is MDIDocument);

        protected override void OnActualSizeChanged(Size value)
        {
            base.OnActualSizeChanged(value);
            if (!base.IsDisposing)
            {
                if (this.PartMDIPanel != null)
                {
                    this.PartMDIPanel.InvalidateMeasure();
                }
                DocumentGroup layoutItem = DockLayoutManager.GetLayoutItem(this) as DocumentGroup;
                if (layoutItem != null)
                {
                    layoutItem.MDIAreaSize = value;
                }
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.PartScroller = LayoutItemsHelper.GetTemplateChild<ScrollViewer>(this);
            if (this.PartScroller != null)
            {
                this.PartScroller.Focusable = false;
                this.PartScroller.CanContentScroll = true;
                Binding binding = new Binding();
                binding.Source = this.LayoutItem;
                binding.Path = new PropertyPath(ScrollBarExtensions.AllowMouseScrollingProperty);
                this.PartScroller.SetBinding(ScrollBarExtensions.AllowMouseScrollingProperty, binding);
            }
            this.PartMDIPanelPresenter = base.GetTemplateChild("PART_MDIPanelPresenter") as ItemsPresenter;
            if (this.PartMDIPanelPresenter != null)
            {
                this.PartMDIPanelPresenter.Loaded += new RoutedEventHandler(this.PartMDIPanelPresenter_Loaded);
            }
            base.CoerceValue(HasMaximizedDocumentsProperty);
        }

        protected virtual void OnBackgroundChanged(Brush newValue)
        {
            base.CoerceValue(ActualBackgroundProperty);
        }

        protected override void OnDispose()
        {
            if (this.PartMDIPanel != null)
            {
                this.PartMDIPanel.RequestBringIntoView -= new RequestBringIntoViewEventHandler(this.PartMDIPanel_RequestBringIntoView);
            }
            base.ClearValue(LayoutItemProperty);
            base.OnDispose();
        }

        protected virtual void OnHasMaximizedDocumentsChanged(bool hasMaximizedDocuments)
        {
            DockLayoutManager manager = DockLayoutManager.Ensure(this, false);
            this.IsHeaderVisible = false;
            if (!hasMaximizedDocuments)
            {
                MDIControllerHelper.UnMergeMDITitles(this);
                MDIControllerHelper.UnMergeMDIMenuItems(this);
            }
            else
            {
                InvokeHelper.BeginInvoke(this, () => MDIControllerHelper.MergeMDITitles(this.GetActiveDocument()));
                if (!MDIControllerHelper.MergeMDIMenuItems(this))
                {
                    this.IsHeaderVisible = true;
                }
            }
            this.UpdateVisualState();
        }

        protected virtual void OnLayoutGroupChanged(DevExpress.Xpf.Docking.LayoutGroup oldGroup, DevExpress.Xpf.Docking.LayoutGroup group)
        {
            if (oldGroup != null)
            {
                oldGroup.SelectedItemChanged -= new SelectedItemChangedEventHandler(this.OnSelectedDocumentChanged);
            }
            if (group == null)
            {
                base.ClearValue(DockLayoutManager.LayoutItemProperty);
                base.ClearValue(ItemsControl.ItemsSourceProperty);
            }
            else
            {
                base.SetValue(DockLayoutManager.LayoutItemProperty, group);
                base.SetValue(ItemsControl.ItemsSourceProperty, group.Items);
                group.SelectedItemChanged += new SelectedItemChangedEventHandler(this.OnSelectedDocumentChanged);
            }
        }

        protected virtual void OnLayoutItemChanged(BaseLayoutItem oldItem, BaseLayoutItem item)
        {
            this.OnLayoutGroupChanged(oldItem as DevExpress.Xpf.Docking.LayoutGroup, item as DevExpress.Xpf.Docking.LayoutGroup);
        }

        protected virtual void OnSelectedDocumentChanged(object sender, SelectedItemChangedEventArgs e)
        {
            this.OnHasMaximizedDocumentsChanged(this.HasMaximizedDocuments);
        }

        private void PartMDIPanel_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {
            if (!(e.OriginalSource is DocumentPanel))
            {
                e.Handled = true;
            }
        }

        private void PartMDIPanelPresenter_Loaded(object sender, RoutedEventArgs e)
        {
            this.PartMDIPanelPresenter.Loaded -= new RoutedEventHandler(this.PartMDIPanelPresenter_Loaded);
            this.PartMDIPanel = LayoutItemsHelper.GetTemplateChild<MDIPanel>(this.PartMDIPanelPresenter);
            if (this.PartMDIPanel != null)
            {
                this.PartMDIPanel.RequestBringIntoView += new RequestBringIntoViewEventHandler(this.PartMDIPanel_RequestBringIntoView);
                if (this.HasMaximizedDocuments)
                {
                    MDIControllerHelper.MergeMDITitles(this.GetActiveDocument());
                }
            }
        }

        protected override void PrepareContainer(DependencyObject element, object item)
        {
            base.PrepareContainer(element, item);
            if (item is BaseLayoutItem)
            {
                ((BaseLayoutItem) item).SelectTemplate();
            }
        }

        private void UpdateVisualState()
        {
            if (this.IsHeaderVisible)
            {
                VisualStateManager.GoToState(this, "HeaderVisible", false);
            }
            else
            {
                VisualStateManager.GoToState(this, "HeaderHidden", false);
            }
            VisualStateManager.GoToState(this, this.HasMaximizedDocuments ? "Maximized" : "EmptyMaximizedState", false);
        }

        public BaseLayoutItem LayoutItem
        {
            get => 
                (BaseLayoutItem) base.GetValue(LayoutItemProperty);
            set => 
                base.SetValue(LayoutItemProperty, value);
        }

        public Brush ThemeDependentBackground
        {
            get => 
                (Brush) base.GetValue(ThemeDependentBackgroundProperty);
            set => 
                base.SetValue(ThemeDependentBackgroundProperty, value);
        }

        public Brush ActualBackground
        {
            get => 
                (Brush) base.GetValue(ActualBackgroundProperty);
            set => 
                base.SetValue(ActualBackgroundProperty, value);
        }

        public DevExpress.Xpf.Docking.LayoutGroup LayoutGroup =>
            this.LayoutItem as DevExpress.Xpf.Docking.LayoutGroup;

        public bool HasMaximizedDocuments =>
            (bool) base.GetValue(HasMaximizedDocumentsProperty);

        public bool IsHeaderVisible
        {
            get => 
                (bool) base.GetValue(IsHeaderVisibleProperty);
            private set => 
                base.SetValue(IsHeaderVisiblePropertyKey, value);
        }

        public MDIPanel PartMDIPanel { get; private set; }

        public ItemsPresenter PartMDIPanelPresenter { get; private set; }

        public ScrollViewer PartScroller { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DocumentMDIContainer.<>c <>9 = new DocumentMDIContainer.<>c();

            internal void <.cctor>b__8_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((DocumentMDIContainer) dObj).OnLayoutItemChanged((BaseLayoutItem) e.OldValue, (BaseLayoutItem) e.NewValue);
            }

            internal void <.cctor>b__8_1(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((DocumentMDIContainer) dObj).OnHasMaximizedDocumentsChanged((bool) e.NewValue);
            }

            internal object <.cctor>b__8_2(DependencyObject dObj, object value) => 
                ((DocumentMDIContainer) dObj).CoerceHasMaximizedDocumentsVisible((bool) value);

            internal void <.cctor>b__8_3(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((DocumentMDIContainer) dObj).OnBackgroundChanged((Brush) e.NewValue);
            }

            internal void <.cctor>b__8_4(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((DocumentMDIContainer) dObj).OnBackgroundChanged((Brush) e.NewValue);
            }

            internal object <.cctor>b__8_5(DependencyObject dObj, object value) => 
                ((DocumentMDIContainer) dObj).CoerceActualBackground((Brush) value);
        }
    }
}

