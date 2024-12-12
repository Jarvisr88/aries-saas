namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Data.Utils;
    using DevExpress.Mvvm.UI;
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Bars.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Ribbon;
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Input;

    [TemplatePart(Name="PART_Content", Type=typeof(UIElement)), TemplatePart(Name="PART_ContentPresenter", Type=typeof(DocumentContentPresenter)), TemplatePart(Name="PART_InactiveLayer", Type=typeof(UIElement))]
    public class BaseDocument : psvContentControl, IMDIChildHost, IMDIMergeStyleListener, IDockLayoutManagerListener, IMergingClient
    {
        public static readonly DependencyProperty IsSelectedProperty;
        public static readonly DependencyProperty ControlHostTemplateProperty;
        public static readonly DependencyProperty LayoutHostTemplateProperty;
        public static readonly DependencyProperty DataHostTemplateProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty LayoutItemContentProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty IsActiveProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty CanMergeProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty IsMergingTargetProperty;
        private BarManager parentBarManager;
        private bool fMergingRequested;
        private bool _IsChildMenuVisibleCore = true;
        protected WeakList<EventHandler> isChildMenuVisibleChangedHandlers = new WeakList<EventHandler>();
        private MDIMergeStylePropertyChangedWeakEventHandler<BaseDocument> mergeStylePropertyChangedHandler;

        event EventHandler IMDIChildHost.IsChildMenuVisibleChanged
        {
            add
            {
                this.isChildMenuVisibleChangedHandlers.Add(value);
            }
            remove
            {
                this.isChildMenuVisibleChangedHandlers.Remove(value);
            }
        }

        static BaseDocument()
        {
            DependencyPropertyRegistrator<BaseDocument> registrator = new DependencyPropertyRegistrator<BaseDocument>();
            registrator.Register<DataTemplate>("ControlHostTemplate", ref ControlHostTemplateProperty, null, null, null);
            registrator.Register<DataTemplate>("LayoutHostTemplate", ref LayoutHostTemplateProperty, null, null, null);
            registrator.Register<DataTemplate>("DataHostTemplate", ref DataHostTemplateProperty, null, null, null);
            registrator.Register<bool>("IsSelected", ref IsSelectedProperty, false, (dObj, e) => ((BaseDocument) dObj).OnIsSelectedChanged((bool) e.NewValue), null);
            registrator.Register<object>("LayoutItemContent", ref LayoutItemContentProperty, null, (dObj, e) => ((BaseDocument) dObj).OnLayoutItemContentChanged(e.OldValue, e.NewValue), null);
            registrator.Register<bool>("IsActive", ref IsActiveProperty, false, (dObj, e) => ((BaseDocument) dObj).OnIsActiveChanged((bool) e.OldValue, (bool) e.NewValue), null);
            registrator.Register<bool>("CanMerge", ref CanMergeProperty, false, (dObj, e) => ((BaseDocument) dObj).OnCanMergeChanged((bool) e.OldValue, (bool) e.NewValue), null);
            registrator.Register<bool>("IsMergingTarget", ref IsMergingTargetProperty, false, (dObj, e) => ((BaseDocument) dObj).OnIsMergingTargetChanged((bool) e.OldValue, (bool) e.NewValue), null);
        }

        public BaseDocument()
        {
            base.LayoutUpdated += new EventHandler(this.OnLayoutUpdated);
            BarManagerHelper.SetMDIChildHost(this, this);
        }

        protected void BeginMerge()
        {
            if (!this.fMergingRequested)
            {
                this.fMergingRequested = true;
                MergingHelper.AddMergingClient(this);
                base.InvalidateArrange();
            }
        }

        protected virtual void CheckIsChildMenuVisible()
        {
            this.IsChildMenuVisibleCore = this.GetIsChildMenuVisible();
        }

        void IDockLayoutManagerListener.Subscribe(DockLayoutManager manager)
        {
            manager.MDIMergeStyleChanged += this.MDIMergeStylePropertyChangedHandler.Handler;
        }

        void IDockLayoutManagerListener.Unsubscribe(DockLayoutManager manager)
        {
            manager.MDIMergeStyleChanged -= this.MDIMergeStylePropertyChangedHandler.Handler;
        }

        void IMDIMergeStyleListener.OnMDIMergeStyleChanged(MDIMergeStyle oldValue, MDIMergeStyle newValue)
        {
            if (!oldValue.IsDefault() || !newValue.IsDefault())
            {
                this.CheckIsChildMenuVisible();
                this.ProcessMergeActions();
            }
        }

        void IMergingClient.Merge()
        {
            this.Merge();
        }

        void IMergingClient.QueueMerge()
        {
            this.ProcessMergeActions();
        }

        void IMergingClient.QueueUnmerge()
        {
            this.UnMerge();
        }

        protected void EnsureTemplateChildren()
        {
            Func<FrameworkElement, bool> predicate = <>c.<>9__51_0;
            if (<>c.<>9__51_0 == null)
            {
                Func<FrameworkElement, bool> local1 = <>c.<>9__51_0;
                predicate = <>c.<>9__51_0 = x => x.Name == "PART_Content";
            }
            this.PartContent = this.GetTemplateChild<FrameworkElement>(predicate);
            Func<FrameworkElement, bool> func2 = <>c.<>9__51_1;
            if (<>c.<>9__51_1 == null)
            {
                Func<FrameworkElement, bool> local2 = <>c.<>9__51_1;
                func2 = <>c.<>9__51_1 = x => x.Name == "PART_InactiveLayer";
            }
            this.InactiveLayer = this.GetTemplateChild<FrameworkElement>(func2);
            if (this.InactiveLayer != null)
            {
                this.SubscribeInactiveLayer();
            }
        }

        protected virtual bool GetIsChildMenuVisible() => 
            true;

        protected virtual bool GetIsChildMenuVisibleForFloatingElement(bool isFloating)
        {
            Func<IMergingSupport, bool> predicate = <>c.<>9__54_0 ??= x => ((!(x is IBar) || (x is ClosedItemsBar)) ? (x is IRibbonControl) : true);
            if ((this.DocumentPanel == null) || (DesignerProperties.GetIsInDesignMode(this) || !BarNameScope.GetService<IElementRegistratorService>(this).GetElements<IMergingSupport>(ScopeSearchSettings.Ancestors).Any<IMergingSupport>(predicate)))
            {
                return true;
            }
            MDIMergeStyle actualMDIMergeStyle = MDIControllerHelper.GetActualMDIMergeStyle(this.DocumentPanel.GetDockLayoutManager(), this.DocumentPanel);
            return (isFloating || (actualMDIMergeStyle == MDIMergeStyle.Never));
        }

        private T GetTemplateChild<T>(Func<T, bool> predicate) => 
            LayoutTreeHelper.GetVisualChildren(this).OfType<T>().FirstOrDefault<T>(predicate);

        private void InactiveLayer_DragEnter(object sender, DragEventArgs e)
        {
            if (base.Container != null)
            {
                base.Container.Activate(base.LayoutItem);
            }
        }

        private void InactiveLayer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.PartContent != null)
            {
                IInputElement element = this.PartContent.InputHitTest(e.GetPosition(this.InactiveLayer));
                if (element != null)
                {
                    element.RaiseEvent(e);
                }
            }
        }

        protected void Merge()
        {
            if (this.fMergingRequested && !DesignerProperties.GetIsInDesignMode(this))
            {
                this.MergeCore();
                this.fMergingRequested = false;
            }
        }

        protected virtual void MergeCore()
        {
            MDIControllerHelper.DoMerge(this);
        }

        protected virtual void NotifyListeners()
        {
            foreach (EventHandler handler in this.isChildMenuVisibleChangedHandlers)
            {
                if (handler != null)
                {
                    handler(this, EventArgs.Empty);
                }
            }
        }

        protected override void OnActualSizeChanged(Size value)
        {
            base.OnActualSizeChanged(value);
            if (!base.IsDisposing && (base.LayoutItem != null))
            {
                base.LayoutItem.LayoutSize = value;
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if ((this.PartContentPresenter != null) && !LayoutItemsHelper.IsTemplateChild<BaseDocument>(this.PartContentPresenter, this))
            {
                this.PartContentPresenter.Dispose();
            }
            this.PartContentPresenter = base.GetTemplateChild("PART_ContentPresenter") as DocumentContentPresenter;
            if (this.PartContentPresenter != null)
            {
                this.PartContentPresenter.EnsureOwner(this);
                BindingHelper.SetBinding(this.PartContentPresenter, DockItemContentPresenter<BaseDocument, DevExpress.Xpf.Docking.DocumentPanel>.IsControlItemsHostProperty, base.LayoutItem, "IsControlItemsHost");
                BindingHelper.SetBinding(this.PartContentPresenter, DockItemContentPresenter<BaseDocument, DevExpress.Xpf.Docking.DocumentPanel>.IsDataBoundProperty, base.LayoutItem, "IsDataBound");
            }
            this.EnsureTemplateChildren();
        }

        protected virtual void OnCanMergeChanged(bool oldValue, bool newValue)
        {
            this.ProcessMergeActions();
        }

        protected override void OnDispose()
        {
            this.UnSubscribeInactiveLayer();
            this.UnMerge();
            base.ClearValue(ControlHostTemplateProperty);
            base.ClearValue(LayoutHostTemplateProperty);
            base.ClearValue(DataHostTemplateProperty);
            base.ClearValue(IsSelectedProperty);
            base.ClearValue(CanMergeProperty);
            base.ClearValue(IsMergingTargetProperty);
            base.ClearValue(LayoutItemContentProperty);
            if (this.PartContentPresenter != null)
            {
                this.PartContentPresenter.Dispose();
                this.PartContentPresenter = null;
            }
            this.isChildMenuVisibleChangedHandlers.Clear();
            base.OnDispose();
        }

        protected virtual void OnIsActiveChanged(bool oldValue, bool newValue)
        {
            this.ProcessMergeActions();
        }

        protected virtual void OnIsMergingTargetChanged(bool oldValue, bool newValue)
        {
            this.ProcessMergeActions();
        }

        protected virtual void OnIsSelectedChanged(bool selected)
        {
        }

        protected override void OnLayoutItemChanged(BaseLayoutItem item, BaseLayoutItem oldItem)
        {
            base.OnLayoutItemChanged(item, oldItem);
            if (item == null)
            {
                BindingHelper.ClearBinding(this, IsSelectedProperty);
                BindingHelper.ClearBinding(this, CanMergeProperty);
                BindingHelper.ClearBinding(this, IsMergingTargetProperty);
                BindingHelper.ClearBinding(this, LayoutItemContentProperty);
                BindingHelper.ClearBinding(this, IsActiveProperty);
                DevExpress.Xpf.Docking.DocumentPanel panel2 = oldItem as DevExpress.Xpf.Docking.DocumentPanel;
                if (panel2 != null)
                {
                    panel2.MergingClient = null;
                }
                if (oldItem != null)
                {
                    BarManagerHelper.SetMDIChildHost(oldItem, null);
                }
            }
            else
            {
                Binding binding = new Binding("IsSelectedItem");
                binding.Source = item;
                base.SetBinding(IsSelectedProperty, binding);
                Binding binding2 = new Binding("CanMerge");
                binding2.Source = item;
                base.SetBinding(CanMergeProperty, binding2);
                Binding binding3 = new Binding("Content");
                binding3.Source = item;
                base.SetBinding(LayoutItemContentProperty, binding3);
                Binding binding4 = new Binding("IsActive");
                binding4.Source = item;
                base.SetBinding(IsActiveProperty, binding4);
                Binding binding5 = new Binding("IsMergingTarget");
                binding5.Source = item;
                base.SetBinding(IsMergingTargetProperty, binding5);
                if (this.PartContentPresenter != null)
                {
                    BindingHelper.SetBinding(this.PartContentPresenter, DockItemContentPresenter<BaseDocument, DevExpress.Xpf.Docking.DocumentPanel>.IsControlItemsHostProperty, base.LayoutItem, "IsControlItemsHost");
                    BindingHelper.SetBinding(this.PartContentPresenter, DockItemContentPresenter<BaseDocument, DevExpress.Xpf.Docking.DocumentPanel>.IsDataBoundProperty, base.LayoutItem, "IsDataBound");
                }
                DevExpress.Xpf.Docking.DocumentPanel panel = item as DevExpress.Xpf.Docking.DocumentPanel;
                if (panel != null)
                {
                    panel.MergingClient = this;
                }
                BarManagerHelper.SetMDIChildHost(item, this);
            }
            this.CheckIsChildMenuVisible();
        }

        protected virtual void OnLayoutItemContentChanged(object oldContent, object newContent)
        {
            if (oldContent != null)
            {
                this.UnMerge();
            }
            this.ProcessMergeActions();
        }

        private void OnLayoutUpdated(object sender, EventArgs e)
        {
            if (this.fMergingRequested)
            {
                MergingHelper.DoMerging();
            }
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            this.CheckIsChildMenuVisible();
        }

        private void OnMDIMergeStyleChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!base.Container.MDIMergeStyle.IsDefault() || !DevExpress.Xpf.Docking.DocumentPanel.GetMDIMergeStyle(this).IsDefault())
            {
                this.CheckIsChildMenuVisible();
                this.ProcessMergeActions();
            }
        }

        protected virtual void ProcessMergeActions()
        {
        }

        [Obsolete("No longer in use. Use the ProcessMergeActions() method instead")]
        protected virtual void ProcessMergeActions(BaseLayoutItem item)
        {
        }

        private void SubscribeInactiveLayer()
        {
            if (this.InactiveLayer != null)
            {
                this.InactiveLayer.AllowDrop = true;
                this.InactiveLayer.MouseDown += new MouseButtonEventHandler(this.InactiveLayer_MouseDown);
                this.InactiveLayer.DragEnter += new DragEventHandler(this.InactiveLayer_DragEnter);
                if (this.DocumentPanel != null)
                {
                    BindingHelper.SetBinding(this.InactiveLayer, UIElement.IsHitTestVisibleProperty, this.DocumentPanel, DevExpress.Xpf.Docking.DocumentPanel.EnableMouseHoverWhenInactiveProperty, new DevExpress.Xpf.Core.BoolInverseConverter());
                }
            }
        }

        protected void UnMerge()
        {
            if (this.fMergingRequested)
            {
                this.fMergingRequested = false;
            }
            this.UnmergeCore();
        }

        protected virtual void UnmergeCore()
        {
            MDIControllerHelper.DoUnMerge(this);
        }

        private void UnSubscribeInactiveLayer()
        {
            if (this.InactiveLayer != null)
            {
                this.InactiveLayer.MouseDown -= new MouseButtonEventHandler(this.InactiveLayer_MouseDown);
                this.InactiveLayer.DragEnter -= new DragEventHandler(this.InactiveLayer_DragEnter);
                BindingHelper.ClearBinding(this.InactiveLayer, UIElement.IsHitTestVisibleProperty);
            }
        }

        public DataTemplate ControlHostTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ControlHostTemplateProperty);
            set => 
                base.SetValue(ControlHostTemplateProperty, value);
        }

        public DataTemplate LayoutHostTemplate
        {
            get => 
                (DataTemplate) base.GetValue(LayoutHostTemplateProperty);
            set => 
                base.SetValue(LayoutHostTemplateProperty, value);
        }

        public DataTemplate DataHostTemplate
        {
            get => 
                (DataTemplate) base.GetValue(DataHostTemplateProperty);
            set => 
                base.SetValue(DataHostTemplateProperty, value);
        }

        public bool IsSelected
        {
            get => 
                (bool) base.GetValue(IsSelectedProperty);
            set => 
                base.SetValue(IsSelectedProperty, value);
        }

        internal bool CanMerge
        {
            get => 
                (bool) base.GetValue(CanMergeProperty);
            set => 
                base.SetValue(CanMergeProperty, value);
        }

        internal bool IsMergingTarget
        {
            get => 
                (bool) base.GetValue(IsMergingTargetProperty);
            set => 
                base.SetValue(IsMergingTargetProperty, value);
        }

        public bool IsActive
        {
            get => 
                (bool) base.GetValue(IsActiveProperty);
            set => 
                base.SetValue(IsActiveProperty, value);
        }

        public DevExpress.Xpf.Docking.DocumentPanel DocumentPanel =>
            base.LayoutItem as DevExpress.Xpf.Docking.DocumentPanel;

        protected BarManager ParentBarManager
        {
            get
            {
                this.parentBarManager ??= LayoutHelper.FindParentObject<BarManager>(base.Container);
                return this.parentBarManager;
            }
        }

        protected UIElement InactiveLayer { get; private set; }

        protected UIElement PartContent { get; private set; }

        public DocumentContentPresenter PartContentPresenter { get; private set; }

        protected bool IsChildMenuVisibleCore
        {
            get => 
                this._IsChildMenuVisibleCore;
            set
            {
                if (this._IsChildMenuVisibleCore != value)
                {
                    this._IsChildMenuVisibleCore = value;
                    this.NotifyListeners();
                }
            }
        }

        bool IMDIChildHost.IsChildMenuVisible =>
            this._IsChildMenuVisibleCore;

        bool IMDIChildHost.CanResize =>
            LayoutItemsHelper.IsFloatingRootItem(base.LayoutItem) && base.LayoutItem.AllowSizing;

        private FloatGroup FloatingRoot =>
            base.LayoutItem.GetRoot() as FloatGroup;

        Size IMDIChildHost.Size
        {
            get => 
                this.FloatingRoot.FloatSize;
            set => 
                this.FloatingRoot.FloatSize = value;
        }

        Size IMDIChildHost.MinSize =>
            LayoutItemsHelper.GetResizingMinSize(this.FloatingRoot);

        Size IMDIChildHost.MaxSize =>
            LayoutItemsHelper.GetResizingMaxSize(this.FloatingRoot);

        private MDIMergeStylePropertyChangedWeakEventHandler<BaseDocument> MDIMergeStylePropertyChangedHandler
        {
            get
            {
                if (this.mergeStylePropertyChangedHandler == null)
                {
                    Action<BaseDocument, object, PropertyChangedEventArgs> onEventAction = <>c.<>9__102_0;
                    if (<>c.<>9__102_0 == null)
                    {
                        Action<BaseDocument, object, PropertyChangedEventArgs> local1 = <>c.<>9__102_0;
                        onEventAction = <>c.<>9__102_0 = delegate (BaseDocument owner, object o, PropertyChangedEventArgs e) {
                            owner.OnMDIMergeStyleChanged(o, e);
                        };
                    }
                    this.mergeStylePropertyChangedHandler = new MDIMergeStylePropertyChangedWeakEventHandler<BaseDocument>(this, onEventAction);
                }
                return this.mergeStylePropertyChangedHandler;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BaseDocument.<>c <>9 = new BaseDocument.<>c();
            public static Func<FrameworkElement, bool> <>9__51_0;
            public static Func<FrameworkElement, bool> <>9__51_1;
            public static Func<IMergingSupport, bool> <>9__54_0;
            public static Action<BaseDocument, object, PropertyChangedEventArgs> <>9__102_0;

            internal void <.cctor>b__8_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseDocument) dObj).OnIsSelectedChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__8_1(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseDocument) dObj).OnLayoutItemContentChanged(e.OldValue, e.NewValue);
            }

            internal void <.cctor>b__8_2(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseDocument) dObj).OnIsActiveChanged((bool) e.OldValue, (bool) e.NewValue);
            }

            internal void <.cctor>b__8_3(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseDocument) dObj).OnCanMergeChanged((bool) e.OldValue, (bool) e.NewValue);
            }

            internal void <.cctor>b__8_4(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseDocument) dObj).OnIsMergingTargetChanged((bool) e.OldValue, (bool) e.NewValue);
            }

            internal bool <EnsureTemplateChildren>b__51_0(FrameworkElement x) => 
                x.Name == "PART_Content";

            internal bool <EnsureTemplateChildren>b__51_1(FrameworkElement x) => 
                x.Name == "PART_InactiveLayer";

            internal void <get_MDIMergeStylePropertyChangedHandler>b__102_0(BaseDocument owner, object o, PropertyChangedEventArgs e)
            {
                owner.OnMDIMergeStyleChanged(o, e);
            }

            internal bool <GetIsChildMenuVisibleForFloatingElement>b__54_0(IMergingSupport x) => 
                (!(x is IBar) || (x is ClosedItemsBar)) ? (x is IRibbonControl) : true;
        }

        private class MDIMergeStylePropertyChangedWeakEventHandler<TOwner> : WeakEventHandler<TOwner, PropertyChangedEventArgs, PropertyChangedEventHandler> where TOwner: class
        {
            private static Action<WeakEventHandler<TOwner, PropertyChangedEventArgs, PropertyChangedEventHandler>, object> action;
            private static Func<WeakEventHandler<TOwner, PropertyChangedEventArgs, PropertyChangedEventHandler>, PropertyChangedEventHandler> create;

            static MDIMergeStylePropertyChangedWeakEventHandler()
            {
                BaseDocument.MDIMergeStylePropertyChangedWeakEventHandler<TOwner>.action = delegate (WeakEventHandler<TOwner, PropertyChangedEventArgs, PropertyChangedEventHandler> h, object o) {
                    ((DockLayoutManager) o).MDIMergeStyleChanged -= h.Handler;
                };
                BaseDocument.MDIMergeStylePropertyChangedWeakEventHandler<TOwner>.create = h => new PropertyChangedEventHandler(h.OnEvent);
            }

            public MDIMergeStylePropertyChangedWeakEventHandler(TOwner owner, Action<TOwner, object, PropertyChangedEventArgs> onEventAction) : base(owner, onEventAction, BaseDocument.MDIMergeStylePropertyChangedWeakEventHandler<TOwner>.action, BaseDocument.MDIMergeStylePropertyChangedWeakEventHandler<TOwner>.create)
            {
            }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly BaseDocument.MDIMergeStylePropertyChangedWeakEventHandler<TOwner>.<>c <>9;

                static <>c()
                {
                    BaseDocument.MDIMergeStylePropertyChangedWeakEventHandler<TOwner>.<>c.<>9 = new BaseDocument.MDIMergeStylePropertyChangedWeakEventHandler<TOwner>.<>c();
                }

                internal void <.cctor>b__3_0(WeakEventHandler<TOwner, PropertyChangedEventArgs, PropertyChangedEventHandler> h, object o)
                {
                    ((DockLayoutManager) o).MDIMergeStyleChanged -= h.Handler;
                }

                internal PropertyChangedEventHandler <.cctor>b__3_1(WeakEventHandler<TOwner, PropertyChangedEventArgs, PropertyChangedEventHandler> h) => 
                    new PropertyChangedEventHandler(h.OnEvent);
            }
        }
    }
}

