namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;

    public class SubMenuBarControl : LinksControl, IKeyTipNavigationOwner, INavigationOwner, IBarsNavigationSupport
    {
        public static readonly DependencyProperty PopupProperty;
        public static readonly DependencyProperty ContentSideVisibilityProperty;
        public static readonly DependencyProperty GlyphSideVisibilityProperty;
        public static readonly DependencyProperty GlyphSidePanelWidthProperty;
        public static readonly DependencyProperty MenuHeaderStatesHolderProperty;
        private DevExpress.Xpf.Bars.GlyphSidePanel glyphSidePanel;

        static SubMenuBarControl();
        public SubMenuBarControl();
        protected internal virtual void CaptureFocus();
        protected override void ClearContainerForItemOverride(DependencyObject element, object item);
        protected virtual Visibility CoerceGlyphSideVisibility(Visibility baseValue);
        protected override NavigationManager CreateNavigationManager();
        protected override INavigationElement GetBoundElement();
        protected override bool GetCanEnterMenuMode();
        protected override bool GetExitNavigationOnFocusChangedWithin();
        protected override bool GetExitNavigationOnMouseUp();
        protected override int GetNavigationID();
        protected override NavigationKeys GetNavigationKeys();
        protected override KeyboardNavigationMode GetNavigationMode();
        protected override Orientation GetNavigationOrientation();
        protected override IBarsNavigationSupport GetNavigationParent();
        protected override void InitializeLinkInfo(BarItemLinkInfo linkInfo);
        protected override void OnAccessKeyPressed(AccessKeyPressedEventArgs e);
        protected override void OnAddedToSelectionCore();
        public override void OnApplyTemplate();
        protected virtual void OnDownButtonClick(object sender, RoutedEventArgs e);
        protected override void OnGlyphPaddingChanged(DependencyPropertyChangedEventArgs e);
        protected virtual void OnGlyphSidePanelChanged();
        protected internal virtual void OnIsOpenChanged(DependencyPropertyChangedEventArgs e);
        protected internal virtual void OnIsOpenCoerce(object baseValue);
        protected internal override void OnItemClick(IBarItemLinkControl linkControl);
        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e);
        private void OnLeftButtonClick(object sender, RoutedEventArgs e);
        protected override void OnLoaded(object sender, RoutedEventArgs e);
        protected override void OnMaxGlyphChanged(DependencyPropertyChangedEventArgs e);
        protected virtual void OnPopupChanged(PopupMenuBase oldValue, PopupMenuBase newValue);
        protected internal override void OnPreviewItemClick(IBarItemLinkControl linkControl);
        protected override void OnRemovedFromSelectionCore(bool destroying);
        private void OnRightButtonClick(object sender, RoutedEventArgs e);
        protected virtual void OnScrollViewerLayoutUpdated(object sender, EventArgs e);
        protected override void OnUnloaded(object sender, RoutedEventArgs e);
        protected virtual void OnUpButtonClick(object sender, RoutedEventArgs e);
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item);
        protected internal virtual void ReleaseFocus();
        protected virtual void SubscribeEvents();
        protected virtual void SubscribeEvents(IBarItemLinkControl linkControl);
        protected virtual void UnsubscribeEvents();
        protected virtual void UnsubscribeEvents(IBarItemLinkControl linkControl);
        protected override void UpdateActualShowGlyph(IList newItems);
        protected virtual void UpdateButtonsVisibility();
        protected internal virtual void UpdateFromPopup();
        private void UpdateGlyphSidePanelWidth();

        public BarItemLinkMenuHeaderControlStatesResourceHolder MenuHeaderStatesHolder { get; set; }

        protected internal DevExpress.Xpf.Bars.GlyphSidePanel GlyphSidePanel { get; set; }

        public double GlyphSidePanelWidth { get; set; }

        public Visibility ContentSideVisibility { get; set; }

        public Visibility GlyphSideVisibility { get; set; }

        public PopupMenuBase Popup { get; set; }

        protected RepeatButton UpButton { get; private set; }

        protected RepeatButton DownButton { get; private set; }

        protected RepeatButton LeftButton { get; private set; }

        protected RepeatButton RightButton { get; private set; }

        protected internal SubMenuScrollViewer ScrollViewer { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SubMenuBarControl.<>c <>9;
            public static Func<PopupMenuBase, BarPopupExpandMode> <>9__59_0;
            public static Func<BarPopupExpandMode> <>9__59_1;
            public static Func<PopupMenuBase, IBarItemLinkControl> <>9__75_0;
            public static Func<PopupMenuBase, IBarItemLinkControl> <>9__76_0;
            public static Func<PopupMenuBase, IBarItemLinkControl> <>9__79_0;
            public static Func<IBarItemLinkControl, BarItemLinkInfo> <>9__79_1;
            public static Func<IBarsNavigationSupport, bool> <>9__79_3;
            public static Func<PopupMenuBase, IBarsNavigationSupport> <>9__79_2;
            public static Func<PopupMenuBase, IBarItemLinkControl> <>9__85_0;
            public static Func<IBarItemLinkControl, BarItemLinkInfo> <>9__85_1;

            static <>c();
            internal void <.cctor>b__5_0(DependencyObject o, DependencyPropertyChangedEventArgs e);
            internal object <.cctor>b__5_1(DependencyObject o, object value);
            internal IBarItemLinkControl <CaptureFocus>b__75_0(PopupMenuBase x);
            internal IBarItemLinkControl <GetBoundElement>b__85_0(PopupMenuBase x);
            internal BarItemLinkInfo <GetBoundElement>b__85_1(IBarItemLinkControl x);
            internal IBarItemLinkControl <GetNavigationParent>b__79_0(PopupMenuBase x);
            internal BarItemLinkInfo <GetNavigationParent>b__79_1(IBarItemLinkControl x);
            internal IBarsNavigationSupport <GetNavigationParent>b__79_2(PopupMenuBase p);
            internal bool <GetNavigationParent>b__79_3(IBarsNavigationSupport x);
            internal BarPopupExpandMode <InitializeLinkInfo>b__59_0(PopupMenuBase popup);
            internal BarPopupExpandMode <InitializeLinkInfo>b__59_1();
            internal IBarItemLinkControl <ReleaseFocus>b__76_0(PopupMenuBase x);
        }
    }
}

