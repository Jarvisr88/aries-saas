namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;

    public class BarControl : LinksControl, IBarLayoutTableInfo, INavigationElement, IBarsNavigationSupport
    {
        private static readonly DependencyPropertyKey ActualShowQuickCustomizationButtonPropertyKey;
        private static readonly DependencyPropertyKey ActualShowDragWidgetPropertyKey;
        private static readonly DependencyPropertyKey ContainerOrientationPropertyKey;
        private static readonly DependencyPropertyKey ActualShowSizeGripPropertyKey;
        protected static readonly DependencyPropertyKey ActualShowContentPropertyKey;
        public static readonly DependencyProperty BarItemDisplayModeProperty;
        public static readonly DependencyProperty ShowBackgroundProperty;
        private static readonly DependencyPropertyKey ActualQuickCustomizationButtonShowModePropertyKey;
        public static readonly DependencyProperty ActualShowContentProperty;
        public static readonly DependencyProperty ActualShowQuickCustomizationButtonProperty;
        public static readonly DependencyProperty ActualShowDragWidgetProperty;
        public static readonly DependencyProperty ContainerOrientationProperty;
        public static readonly DependencyProperty ActualShowSizeGripProperty;
        public static readonly DependencyProperty ActualQuickCustomizationButtonShowModeProperty;
        private DevExpress.Xpf.Bars.DragWidget dragWidget;
        private Thumb thumb;
        private EventHandler layoutPropertyChanged;
        private static List<BarContainerType> barContainerTypes;

        event DependencyPropertyChangedEventHandler IBarLayoutTableInfo.IsVisibleChanged;

        event EventHandler IBarLayoutTableInfo.LayoutPropertyChanged;

        static BarControl();
        public BarControl();
        private bool BarHasMergingCandidates();
        protected internal virtual void CalcBarCustomizationButonsVisibility();
        protected virtual bool CanReduce();
        protected void CheckContinueDragBar();
        void IBarLayoutTableInfo.Arrange(Rect finalRect);
        bool IBarLayoutTableInfo.CanDock(Dock dock);
        void IBarLayoutTableInfo.InvalidateMeasure();
        bool IBarLayoutTableInfo.MakeFloating();
        void IBarLayoutTableInfo.Measure(Size constraint);
        bool INavigationElement.ProcessKeyDown(KeyEventArgs e);
        protected internal bool GetAllowQuickCustomizationButton();
        protected override bool GetCanEnterMenuMode();
        private int GetCollectionIndex();
        private int GetNavigationElementIngex(BarItemLinkInfo info);
        protected override IList<INavigationElement> GetNavigationElements();
        protected override int GetNavigationID();
        protected override NavigationKeys GetNavigationKeys();
        protected override KeyboardNavigationMode GetNavigationMode();
        protected override Orientation GetNavigationOrientation();
        protected override IBarsNavigationSupport GetNavigationParent();
        private Window GetParentWindow();
        protected internal override void InvalidateMeasurePanel();
        protected virtual bool IsAllLinksVisible();
        protected override Size MeasureOverride(Size constraint);
        private double MinMax(double value, double minValue, double maxValue);
        protected virtual void OnActualShowDragWigetChanged(DependencyPropertyChangedEventArgs e);
        protected virtual void OnActualShowQuickCustomizationButtonChanged(DependencyPropertyChangedEventArgs e);
        protected virtual void OnActualShowSizeGripChanged();
        public override void OnApplyTemplate();
        protected internal virtual void OnBarIsRemovedChanged();
        private void OnBarItemDisplayModeChanged(DevExpress.Xpf.Bars.BarItemDisplayMode oldValue);
        protected internal virtual void OnBarVisibleChanged();
        protected virtual void OnContainerOrientationChanged();
        protected override AutomationPeer OnCreateAutomationPeer();
        protected virtual void OnDragWidgetDoubleClick(object sender, MouseButtonEventArgs e);
        protected internal virtual void OnGlyphSizeChanged(DependencyPropertyChangedEventArgs e);
        protected virtual void OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e);
        protected virtual void OnHasVisibleItemsChanged(object sender, EventArgs e);
        protected virtual void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e);
        protected override void OnItemsChangedCore(NotifyCollectionChangedAction action, IList oldItems, IList newItems);
        protected override void OnKeyDown(KeyEventArgs e);
        protected internal void OnLayoutPropertyChanged();
        protected override void OnLinksHolderChanged(ILinksHolder oldValue, ILinksHolder newValue);
        protected override void OnLoaded(object sender, RoutedEventArgs e);
        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e);
        protected override void OnUnloaded(object sender, RoutedEventArgs e);
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item);
        protected virtual void SubscribeEvents();
        private void thumb_DragDelta(object sender, DragDeltaEventArgs e);
        private void thumb_MouseEnter(object sender, MouseEventArgs e);
        protected virtual void UnSubscribeEvents();
        protected internal virtual void UpdateActualCustomizationButtonShowMode();
        protected internal virtual void UpdateBarControlProperties();
        protected internal virtual void UpdateBarCustomizationButonsVisibility();
        protected virtual void UpdateDragCursor();
        protected virtual void UpdateDragWidgetVisibility();
        protected internal virtual void UpdateItemsDisplayMode();
        protected void UpdateLinkControlOrientation(IBarItemLinkControl linkControl);
        protected override void UpdateLinkControlProperties(IBarItemLinkControl linkControl);
        protected virtual void UpdateThumb();
        protected internal virtual void UpdateVisibility();
        protected internal virtual void UpdateVisualState();

        public bool ActualShowContent { get; protected internal set; }

        public bool ActualShowQuickCustomizationButton { get; private set; }

        public bool ActualShowDragWidget { get; private set; }

        public Orientation ContainerOrientation { get; protected internal set; }

        public bool ActualShowSizeGrip { get; protected internal set; }

        public DevExpress.Xpf.Bars.BarItemDisplayMode BarItemDisplayMode { get; set; }

        public bool ShowBackground { get; set; }

        public bool? ActualQuickCustomizationButtonShowMode { get; protected internal set; }

        public DevExpress.Xpf.Bars.Bar Bar { get; }

        public BarDockInfo DockInfo { get; }

        protected internal double RowCount { get; set; }

        protected internal DevExpress.Xpf.Bars.DragWidget DragWidget { get; }

        protected internal ContentControl ContentBackground { get; private set; }

        protected internal ContentControl Root { get; private set; }

        internal bool ActualShowQuickCustomizationButtonCore { get; set; }

        DevExpress.Xpf.Bars.Bar IBarLayoutTableInfo.Bar { get; }

        bool IBarLayoutTableInfo.CanReduce { get; }

        bool IBarLayoutTableInfo.UseWholeRow { get; }

        int IBarLayoutTableInfo.Row { get; set; }

        int IBarLayoutTableInfo.Column { get; set; }

        int IBarLayoutTableInfo.CollectionIndex { get; }

        double IBarLayoutTableInfo.Offset { get; set; }

        Size IBarLayoutTableInfo.DesiredSize { get; }

        Size IBarLayoutTableInfo.RenderSize { get; }

        INavigationOwner INavigationElement.BoundOwner { get; }

        bool IBarsNavigationSupport.IsSelected { get; set; }

        bool IBarsNavigationSupport.ExcludeFromNavigation { get; }

        bool IBarsNavigationSupport.ExitNavigationOnMouseUp { get; }

        double IBarLayoutTableInfo.Opacity { get; set; }

        bool IBarLayoutTableInfo.IsVisible { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BarControl.<>c <>9;
            public static Func<BarContainerControl, bool> <>9__93_0;
            public static Func<bool> <>9__93_1;
            public static Func<BarManager, bool> <>9__93_2;
            public static Func<bool> <>9__93_3;
            public static Func<BarItemLinkInfo, bool> <>9__95_0;
            public static Func<BarItemLinkInfo, bool> <>9__96_0;
            public static Action<IBarItemLinkControl> <>9__108_0;
            public static Func<Bar, bool> <>9__121_0;
            public static Func<Bar, int> <>9__123_0;
            public static Func<int> <>9__123_1;
            public static Func<Bar, int> <>9__126_0;
            public static Func<int> <>9__126_1;
            public static Func<Bar, double> <>9__131_0;
            public static Func<double> <>9__131_1;
            public static Func<BarNameScope, UIElement> <>9__137_0;
            public static Func<Bar, bool> <>9__144_0;
            public static Func<Bar, BarControl> <>9__144_2;
            public static Func<BarControl, bool> <>9__144_3;
            public static Func<BarControl, <>f__AnonymousType1<BarControl, int>> <>9__144_4;
            public static Func<<>f__AnonymousType1<BarControl, int>, int> <>9__144_5;
            public static Func<BarItemLinkInfo, int, <>f__AnonymousType2<int, BarItemLinkInfo>> <>9__160_0;
            public static Func<<>f__AnonymousType2<int, BarItemLinkInfo>, int> <>9__160_2;
            public static Func<<>f__AnonymousType2<int, BarItemLinkInfo>, BarItemLinkInfo> <>9__160_3;
            public static Func<ItemsPresenter, BarClientPanel> <>9__160_4;
            public static Func<BarClientPanel, BarQuickCustomizationButton> <>9__160_5;
            public static Func<BarItemLink, BarItemAlignment> <>9__161_0;
            public static Func<BarItemAlignment> <>9__161_1;
            public static Func<BarItem, BarItemAlignment> <>9__161_2;
            public static Func<BarItemAlignment> <>9__161_3;

            static <>c();
            internal void <.cctor>b__48_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__48_1(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__48_2(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__48_3(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__48_4(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal bool <CanReduce>b__96_0(BarItemLinkInfo x);
            internal int <DevExpress.Xpf.Bars.IBarLayoutTableInfo.get_Column>b__126_0(Bar x);
            internal int <DevExpress.Xpf.Bars.IBarLayoutTableInfo.get_Column>b__126_1();
            internal double <DevExpress.Xpf.Bars.IBarLayoutTableInfo.get_Offset>b__131_0(Bar x);
            internal double <DevExpress.Xpf.Bars.IBarLayoutTableInfo.get_Offset>b__131_1();
            internal int <DevExpress.Xpf.Bars.IBarLayoutTableInfo.get_Row>b__123_0(Bar x);
            internal int <DevExpress.Xpf.Bars.IBarLayoutTableInfo.get_Row>b__123_1();
            internal bool <DevExpress.Xpf.Bars.IBarLayoutTableInfo.get_UseWholeRow>b__121_0(Bar x);
            internal UIElement <DevExpress.Xpf.Bars.IBarLayoutTableInfo.MakeFloating>b__137_0(BarNameScope x);
            internal bool <GetCanEnterMenuMode>b__144_0(Bar x);
            internal BarControl <GetCanEnterMenuMode>b__144_2(Bar x);
            internal bool <GetCanEnterMenuMode>b__144_3(BarControl x);
            internal <>f__AnonymousType1<BarControl, int> <GetCanEnterMenuMode>b__144_4(BarControl x);
            internal int <GetCanEnterMenuMode>b__144_5(<>f__AnonymousType1<BarControl, int> x);
            internal BarItemAlignment <GetNavigationElementIngex>b__161_0(BarItemLink x);
            internal BarItemAlignment <GetNavigationElementIngex>b__161_1();
            internal BarItemAlignment <GetNavigationElementIngex>b__161_2(BarItem x);
            internal BarItemAlignment <GetNavigationElementIngex>b__161_3();
            internal <>f__AnonymousType2<int, BarItemLinkInfo> <GetNavigationElements>b__160_0(BarItemLinkInfo x, int i);
            internal int <GetNavigationElements>b__160_2(<>f__AnonymousType2<int, BarItemLinkInfo> x);
            internal BarItemLinkInfo <GetNavigationElements>b__160_3(<>f__AnonymousType2<int, BarItemLinkInfo> x);
            internal BarClientPanel <GetNavigationElements>b__160_4(ItemsPresenter x);
            internal BarQuickCustomizationButton <GetNavigationElements>b__160_5(BarClientPanel x);
            internal bool <IsAllLinksVisible>b__95_0(BarItemLinkInfo x);
            internal bool <UpdateActualCustomizationButtonShowMode>b__93_0(BarContainerControl x);
            internal bool <UpdateActualCustomizationButtonShowMode>b__93_1();
            internal bool <UpdateActualCustomizationButtonShowMode>b__93_2(BarManager x);
            internal bool <UpdateActualCustomizationButtonShowMode>b__93_3();
            internal void <UpdateItemsDisplayMode>b__108_0(IBarItemLinkControl x);
        }

        private delegate void DragDelegate();
    }
}

