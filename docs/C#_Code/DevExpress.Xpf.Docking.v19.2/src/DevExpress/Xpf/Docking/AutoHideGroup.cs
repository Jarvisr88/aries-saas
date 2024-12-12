namespace DevExpress.Xpf.Docking
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.ModuleInjection;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Utils.Serializing;
    using DevExpress.Xpf.Docking.Base;
    using DevExpress.Xpf.Docking.UIAutomation;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;

    public class AutoHideGroup : LayoutGroup
    {
        public static readonly DependencyProperty DockTypeProperty;
        public static readonly DependencyProperty AutoHideSizeProperty;
        public static readonly DependencyProperty AutoHideSpeedProperty;
        public static readonly DependencyProperty AutoHideTypeProperty;
        public static readonly DependencyProperty SizeToContentProperty;
        public static readonly DependencyProperty IsAutoHideCenterProperty;
        private static readonly RoutedEvent DockTypeChangedEvent;
        internal bool HasPersistentGroups;

        public event DockTypeChangedEventHandler DockTypeChanged;

        static AutoHideGroup()
        {
            DockingStrategyRegistrator.RegisterAutoHideGroupStrategy();
            DevExpress.Xpf.Docking.DependencyPropertyRegistrator<AutoHideGroup> registrator = new DevExpress.Xpf.Docking.DependencyPropertyRegistrator<AutoHideGroup>();
            registrator.OverrideMetadata<bool>(BaseLayoutItem.AllowSelectionProperty, false, null, null);
            registrator.OverrideMetadata<bool>(LayoutGroup.AllowExpandProperty, false, null, null);
            registrator.OverrideMetadata<bool>(LayoutGroup.IsExpandedPropertyKey, false, null, null);
            registrator.Register<Dock>("DockType", ref DockTypeProperty, Dock.Left, (dObj, e) => ((AutoHideGroup) dObj).OnDockTypeChanged((Dock) e.OldValue, (Dock) e.NewValue), null);
            registrator.RegisterAttached<Size>("AutoHideSize", ref AutoHideSizeProperty, new Size(150.0, 150.0), null, new CoerceValueCallback(AutoHideGroup.CoerceAutoHideSize));
            registrator.Register<int>("AutoHideSpeed", ref AutoHideSpeedProperty, 150, null, null);
            registrator.RegisterAttached<AutoHideType>("AutoHideType", ref AutoHideTypeProperty, AutoHideType.Default, null, null);
            registrator.RegisterAttached<SizeToContent>("SizeToContent", ref SizeToContentProperty, SizeToContent.Manual, null, null);
            DockTypeChangedEvent = EventManager.RegisterRoutedEvent("DockTypeChanged", RoutingStrategy.Direct, typeof(DockTypeChangedEventHandler), typeof(AutoHideGroup));
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(DependencyObject), "d");
            System.Linq.Expressions.Expression[] arguments = new System.Linq.Expressions.Expression[] { expression };
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<AutoHideGroup>.New().RegisterAttached<DependencyObject, bool>(System.Linq.Expressions.Expression.Lambda<Func<DependencyObject, bool>>(System.Linq.Expressions.Expression.Call(null, (MethodInfo) methodof(AutoHideGroup.GetIsAutoHideCenter), arguments), parameters), out IsAutoHideCenterProperty, false, frameworkOptions);
        }

        public AutoHideGroup() : this(new BaseLayoutItem[0])
        {
        }

        internal AutoHideGroup(params BaseLayoutItem[] items) : base(items)
        {
            base.IsEnabledChanged += new DependencyPropertyChangedEventHandler(this.OnIsEnabledChanged);
        }

        protected override Size CalcMaxSizeValue(Size value) => 
            value;

        protected override Size CalcMinSizeValue(Size value) => 
            value;

        protected override bool CanCreateItemsInternal() => 
            false;

        private static object CoerceAutoHideSize(DependencyObject dObj, object value)
        {
            if (!(dObj is BaseLayoutItem))
            {
                return value;
            }
            BaseLayoutItem item = (BaseLayoutItem) dObj;
            return MathHelper.MeasureSize(item.ActualMinSize, item.ActualMaxSize, (Size) value);
        }

        protected override BaseLayoutItem CoerceSelectedItem(BaseLayoutItem item) => 
            base.IsValid(base.SelectedTabIndex) ? base.Items[base.SelectedTabIndex] : item;

        protected override void CoerceSizes()
        {
            base.CoerceSizes();
            base.CoerceValue(AutoHideSizeProperty);
        }

        protected override BaseLayoutItemCollection CreateItems() => 
            new AutoHideItemsCollection(this);

        internal Size GetActualAutoHideSize(BaseLayoutItem item)
        {
            if ((DependencyPropertyHelper.GetValueSource(item, AutoHideSizeProperty).BaseValueSource != BaseValueSource.Default) && this.IsAutoHideSizeValid(item))
            {
                return GetAutoHideSize(item);
            }
            LayoutPanel panel = item as LayoutPanel;
            Size size = (panel != null) ? panel.LayoutSizeBeforeHide : Size.Empty;
            return (MathHelper.IsEmpty(size) ? item.GetSize(this.AutoHideSize) : size);
        }

        [XtraSerializableProperty]
        public static Size GetAutoHideSize(DependencyObject target) => 
            (Size) target.GetValue(AutoHideSizeProperty);

        [XtraSerializableProperty]
        public static AutoHideType GetAutoHideType(DependencyObject obj) => 
            (AutoHideType) obj.GetValue(AutoHideTypeProperty);

        public static bool GetIsAutoHideCenter(DependencyObject d) => 
            (bool) d.GetValue(IsAutoHideCenterProperty);

        protected override LayoutItemType GetLayoutItemTypeCore() => 
            LayoutItemType.AutoHideGroup;

        [XtraSerializableProperty]
        public static SizeToContent GetSizeToContent(DependencyObject target) => 
            (SizeToContent) target.GetValue(SizeToContentProperty);

        private bool IsAutoHideSizeValid(BaseLayoutItem item)
        {
            Size autoHideSize = GetAutoHideSize(item);
            return (((this.DockType == Dock.Top) || (this.DockType == Dock.Bottom)) ? MathHelper.IsConstraintValid(autoHideSize.Width) : MathHelper.IsConstraintValid(autoHideSize.Height));
        }

        protected override void OnAllowDockChanged(bool value)
        {
            base.OnAllowDockChanged(value);
            foreach (BaseLayoutItem item in base.Items)
            {
                item.CoerceValue(BaseLayoutItem.IsPinButtonVisibleProperty);
            }
        }

        protected override AutomationPeer OnCreateAutomationPeer() => 
            new AutoHideGroupAutomationPeer(this);

        protected override void OnDockLayoutManagerChanged(DockLayoutManager oldValue, DockLayoutManager newValue)
        {
            base.OnDockLayoutManagerChanged(oldValue, newValue);
            if (newValue != null)
            {
                DockLayoutManager.AddLogicalChild(newValue, this);
            }
        }

        protected virtual void OnDockTypeChanged(Dock prev, Dock value)
        {
            if (this.DockTypeChanged != null)
            {
                DockTypeChangedEventArgs e = new DockTypeChangedEventArgs(value, prev);
                e.RoutedEvent = DockTypeChangedEvent;
                e.Source = this;
                this.DockTypeChanged(this, e);
            }
        }

        private void OnIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Action<BaseLayoutItem> action = <>c.<>9__50_0;
            if (<>c.<>9__50_0 == null)
            {
                Action<BaseLayoutItem> local1 = <>c.<>9__50_0;
                action = <>c.<>9__50_0 = x => x.CoerceValue(UIElement.IsEnabledProperty);
            }
            base.Items.ForEach<BaseLayoutItem>(action);
        }

        protected override void OnIsExpandedChanged(bool expanded)
        {
            base.OnIsExpandedChanged(expanded);
            if (base.Manager != null)
            {
                ItemEventArgs e = expanded ? ((ItemEventArgs) new DockItemExpandedEventArgs(this)) : ((ItemEventArgs) new DockItemCollapsedEventArgs(this));
                base.Manager.RaiseEvent(e);
            }
        }

        protected override void OnItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsCollectionChanged(sender, e);
            if (base.IsExpanded && ((base.SelectedItem == null) || !base.Items.Contains(base.SelectedItem)))
            {
                DockControllerHelper.CheckHideView(base.Manager, this);
            }
        }

        internal void OnOwnerCollectionChanged()
        {
            if ((base.Manager != null) && !base.Manager.AutoHideGroups.Contains(this))
            {
                DockLayoutManager.RemoveLogicalChild(base.Manager, this);
            }
        }

        protected override void OnVisualParentChanged(DependencyObject oldParent)
        {
            base.OnVisualParentChanged(oldParent);
            if (base.VisualParent != null)
            {
                Action<LayoutPanel> action = <>c.<>9__48_0;
                if (<>c.<>9__48_0 == null)
                {
                    Action<LayoutPanel> local1 = <>c.<>9__48_0;
                    action = <>c.<>9__48_0 = x => x.ApplyExpandState();
                }
                base.Items.OfType<LayoutPanel>().ForEach<LayoutPanel>(action);
            }
        }

        public static void SetAutoHideSize(DependencyObject target, Size value)
        {
            target.SetValue(AutoHideSizeProperty, value);
        }

        public static void SetAutoHideType(DependencyObject obj, AutoHideType value)
        {
            obj.SetValue(AutoHideTypeProperty, value);
        }

        public static void SetIsAutoHideCenter(DependencyObject d, bool value)
        {
            d.SetValue(IsAutoHideCenterProperty, value);
        }

        public static void SetSizeToContent(DependencyObject target, SizeToContent value)
        {
            target.SetValue(SizeToContentProperty, value);
        }

        [Description("Gets or sets the size of panels belonging to the current AutoHideGroup, in pixels.This is a dependency property."), XtraSerializableProperty, Category("Layout")]
        public Size AutoHideSize
        {
            get => 
                (Size) base.GetValue(AutoHideSizeProperty);
            set => 
                base.SetValue(AutoHideSizeProperty, value);
        }

        [Description("Gets or sets the time, in milliseconds, required to open/close an auto-hidden panel belonging to the current group.This is a dependency property."), XtraSerializableProperty]
        public int AutoHideSpeed
        {
            get => 
                (int) base.GetValue(AutoHideSpeedProperty);
            set => 
                base.SetValue(AutoHideSpeedProperty, value);
        }

        [Description("Gets or sets the side of the DockLayoutManager at which the current AutoHideGroup object is docked.This is a dependency property."), XtraSerializableProperty, Category("Layout")]
        public Dock DockType
        {
            get => 
                (Dock) base.GetValue(DockTypeProperty);
            set => 
                base.SetValue(DockTypeProperty, value);
        }

        protected internal override bool IgnoreOrientation =>
            true;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly AutoHideGroup.<>c <>9 = new AutoHideGroup.<>c();
            public static Action<LayoutPanel> <>9__48_0;
            public static Action<BaseLayoutItem> <>9__50_0;

            internal void <.cctor>b__7_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((AutoHideGroup) dObj).OnDockTypeChanged((Dock) e.OldValue, (Dock) e.NewValue);
            }

            internal void <OnIsEnabledChanged>b__50_0(BaseLayoutItem x)
            {
                x.CoerceValue(UIElement.IsEnabledProperty);
            }

            internal void <OnVisualParentChanged>b__48_0(LayoutPanel x)
            {
                x.ApplyExpandState();
            }
        }
    }
}

