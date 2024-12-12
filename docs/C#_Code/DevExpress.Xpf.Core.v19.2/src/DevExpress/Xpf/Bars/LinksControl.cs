namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Bars.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public abstract class LinksControl : BarItemsControl, IMutableNavigationOwner, INavigationOwner, IBarsNavigationSupport, IMutableNavigationSupport
    {
        public static readonly DependencyProperty DropIndicatorStyleProperty;
        protected static readonly DependencyPropertyKey MaxGlyphSizePropertyKey;
        public static readonly DependencyProperty MaxGlyphSizeProperty;
        public static readonly DependencyProperty GlyphPaddingProperty;
        public static readonly DependencyProperty SpacingModeProperty;
        public static readonly DependencyProperty LinksHolderProperty;
        public static readonly DependencyProperty ItemRenderTemplateSelectorProperty;
        public static readonly DependencyProperty UseLightweightTemplatesProperty;
        private DevExpress.Xpf.Bars.NavigationManager navigationManager;
        private readonly PostponedAction updateGlypsSizeAction;
        private readonly BarHistoryListItemHelper historyListItemHelper;
        private CompatibilityAdornerContainer panelAdornerContainer;
        private LinkContainerType containerTypeCore;
        private BarManager manager;
        private bool navigationIsSelected;
        private EventHandler changed;
        private Dictionary<int, Size> glyphSizes;

        event EventHandler IMutableNavigationSupport.Changed;

        static LinksControl();
        public LinksControl();
        protected internal virtual void CalculateMaxGlyphSize();
        protected override void ClearContainerForItemOverride(DependencyObject element, object item);
        protected virtual void ClearControlItemLinkCollection(IBarItemLinkInfoCollection coll);
        protected void CoerceLinksIsEnableProperty();
        protected internal virtual bool ContainsLinkControl(IBarItemLinkControl linkControl);
        protected virtual IBarItemLinkInfoCollection CreateLinkInfoCollection(BarItemLinkCollection itemLinks, CustomizedBarItemLinkCollection customLinks);
        protected virtual DevExpress.Xpf.Bars.NavigationManager CreateNavigationManager();
        void IMutableNavigationSupport.RaiseChanged();
        void INavigationOwner.OnAddedToSelection();
        void INavigationOwner.OnRemovedFromSelection(bool destroying);
        protected virtual void ExecuteActionOnLinkControls(Action<IBarItemLinkControl> action);
        protected virtual void ExecuteActionOnLinkInfos(Action<BarItemLinkInfo> action);
        protected virtual void ForceCalcMaxGlyphSize();
        protected virtual INavigationElement GetBoundElement();
        protected abstract bool GetCanEnterMenuMode();
        protected virtual bool GetExitNavigationOnFocusChangedWithin();
        protected virtual bool GetExitNavigationOnMouseUp();
        protected virtual bool GetIsSelectable();
        protected virtual IBarItemLinkInfoCollection GetItemsSource();
        protected internal IBarItemLinkControl GetLinkControl(int index);
        public virtual IBarsNavigationSupport GetLinkNavigationContainer();
        protected virtual IList<INavigationElement> GetNavigationElements();
        protected abstract int GetNavigationID();
        protected abstract NavigationKeys GetNavigationKeys();
        protected abstract KeyboardNavigationMode GetNavigationMode();
        protected abstract Orientation GetNavigationOrientation();
        protected abstract IBarsNavigationSupport GetNavigationParent();
        protected virtual void InitializeLinkInfo(BarItemLinkInfo linkInfo);
        protected internal virtual void InvalidateMeasurePanel();
        protected virtual bool IsItemsTypeValid();
        protected virtual void LoadPanelAdornerContainer();
        protected virtual void OnAccessKeyPressed(AccessKeyPressedEventArgs e);
        private static void OnAccessKeyPressed(object sender, AccessKeyPressedEventArgs e);
        protected virtual void OnActualLinksChanged(BarItemLinkCollection oldValue, BarItemLinkCollection newValue);
        private void OnActualLinksChangedCore(object sender, ValueChangedEventArgs<BarItemLinkCollection> e);
        protected virtual void OnAddedToSelectionCore();
        public override void OnApplyTemplate();
        protected internal virtual void OnClear();
        protected virtual void OnContainerChanged(BarContainerControl oldContainer);
        protected virtual void OnContainerTypeChanged(LinkContainerType oldValue);
        protected virtual void OnGlyphPaddingChanged(DependencyPropertyChangedEventArgs e);
        private void OnIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e);
        protected internal virtual void OnItemClick(IBarItemLinkControl linkControl);
        protected internal virtual void OnItemMouseEnter(IBarItemLinkControl linkControl);
        protected internal virtual void OnItemMouseLeave(IBarItemLinkControl linkControl, MouseEventArgs e);
        protected internal virtual void OnItemMouseMove(IBarItemLinkControl linkControl);
        protected virtual void OnItemRenderTemplateSelectorChanged(RenderTemplateSelector oldValue, RenderTemplateSelector newValue);
        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e);
        protected virtual void OnItemsChangedCore(NotifyCollectionChangedAction action, IList oldItems, IList newItems);
        protected virtual void OnLayoutUpdated(object sender, EventArgs e);
        protected virtual void OnLinksHolderChanged(ILinksHolder oldValue, ILinksHolder newValue);
        protected virtual void OnLoaded(object sender, RoutedEventArgs e);
        protected internal virtual void OnManagerChanged(DependencyPropertyChangedEventArgs e);
        protected virtual void OnMaxGlyphChanged(DependencyPropertyChangedEventArgs e);
        protected static void OnMaxGlyphSizePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        protected internal virtual void OnPreviewItemClick(IBarItemLinkControl linkControl);
        protected virtual void OnRemovedFromSelectionCore(bool destroying);
        protected virtual void OnSpacingModeChanged(DevExpress.Xpf.Bars.SpacingMode oldValue);
        protected virtual void OnUnloaded(object sender, RoutedEventArgs e);
        protected internal virtual void OnUseLightweightTemplatesChanged(bool oldValue, bool newValue);
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item);
        protected internal void RaiseChanged();
        protected internal virtual void RegisterGlyphSize(BarItemLinkInfo linkInfo, Size glyphSize);
        protected virtual void SetItemsSource(IBarItemLinkInfoCollection itemsSource);
        protected internal virtual void UnregisterGlyphSize(BarItemLinkInfo linkInfo);
        protected virtual void UpdateActualShowGlyph(IList newItems);
        protected internal virtual void UpdateItemsSource();
        protected internal virtual void UpdateItemsSource(BarItemLinkCollection itemLinks, CustomizedBarItemLinkCollection customLinks);
        protected virtual void UpdateLightweightLinkControlRenderTemplateSelector(IBarItemLinkControl linkControl);
        protected virtual void UpdateLinkControlProperties(IBarItemLinkControl linkControl);
        protected virtual void UpdateLinksContainerType(NotifyCollectionChangedAction action, IList oldITems, IList newItems);
        private void UpdateSpacingMode();

        public ILinksHolder LinksHolder { get; set; }

        public Style DropIndicatorStyle { get; set; }

        public RenderTemplateSelector ItemRenderTemplateSelector { get; set; }

        public bool UseLightweightTemplates { get; set; }

        protected internal System.Windows.Controls.ItemsPresenter ItemsPresenter { get; set; }

        public virtual BarItemLinkCollection ItemLinks { get; }

        public DevExpress.Xpf.Bars.NavigationManager NavigationManager { get; }

        public DevExpress.Xpf.Bars.SpacingMode SpacingMode { get; set; }

        protected internal CompatibilityAdornerContainer PanelAdornerContainer { get; }

        protected virtual bool AllowRecycling { get; }

        public LinkContainerType ContainerType { get; protected internal set; }

        public Size MaxGlyphSize { get; internal set; }

        public Thickness GlyphPadding { get; set; }

        protected virtual Size DefaultMaxGlyphSize { get; }

        protected virtual bool IsItemsValid { get; }

        protected internal virtual bool OpenPopupsAsMenu { get; }

        public BarManager Manager { get; }

        IBarsNavigationSupport IBarsNavigationSupport.Parent { get; }

        Orientation INavigationOwner.Orientation { get; }

        NavigationKeys INavigationOwner.NavigationKeys { get; }

        KeyboardNavigationMode INavigationOwner.NavigationMode { get; }

        DevExpress.Xpf.Bars.NavigationManager INavigationOwner.NavigationManager { get; }

        IList<INavigationElement> INavigationOwner.Elements { get; }

        bool INavigationOwner.CanEnterMenuMode { get; }

        INavigationElement INavigationOwner.BoundElement { get; }

        int IBarsNavigationSupport.ID { get; }

        bool IBarsNavigationSupport.IsSelectable { get; }

        bool IBarsNavigationSupport.ExitNavigationOnMouseUp { get; }

        bool IBarsNavigationSupport.ExitNavigationOnFocusChangedWithin { get; }

        bool IBarsNavigationSupport.IsSelected { get; set; }

        bool IBarsNavigationSupport.ExcludeFromNavigation { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LinksControl.<>c <>9;
            public static Action<IBarItemLinkControl> <>9__8_6;
            public static Func<bool> <>9__13_0;
            public static Func<ILinksHolder, BarItemLinkCollection> <>9__39_0;
            public static Func<ILinksHolder, BarItemLinkCollection> <>9__39_1;
            public static Func<ILinksHolder, CustomizedBarItemLinkCollection> <>9__41_0;
            public static Func<ILinksHolder, BarItemLinkCollection> <>9__42_0;
            public static Func<ILinksHolder, CustomizedBarItemLinkCollection> <>9__42_1;
            public static Func<ILinksHolder, BarItemLinkCollection> <>9__49_0;
            public static Func<IList, IEnumerator> <>9__89_0;
            public static Func<BarItemLinkInfo, IBarItemLinkControl> <>9__96_0;
            public static Func<BarItemLinkInfo, IBarItemLinkControl> <>9__156_0;

            static <>c();
            internal void <.cctor>b__8_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__8_1(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__8_2(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__8_3(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__8_4(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__8_5(DependencyObject o, DependencyPropertyChangedEventArgs args);
            internal void <.cctor>b__8_6(IBarItemLinkControl lc);
            internal bool <.ctor>b__13_0();
            internal BarItemLinkCollection <get_ItemLinks>b__49_0(ILinksHolder x);
            internal IBarItemLinkControl <GetNavigationElements>b__156_0(BarItemLinkInfo x);
            internal CustomizedBarItemLinkCollection <OnActualLinksChanged>b__41_0(ILinksHolder x);
            internal BarItemLinkCollection <OnLinksHolderChanged>b__39_0(ILinksHolder x);
            internal BarItemLinkCollection <OnLinksHolderChanged>b__39_1(ILinksHolder x);
            internal IBarItemLinkControl <UpdateActualShowGlyph>b__96_0(BarItemLinkInfo x);
            internal BarItemLinkCollection <UpdateItemsSource>b__42_0(ILinksHolder x);
            internal CustomizedBarItemLinkCollection <UpdateItemsSource>b__42_1(ILinksHolder x);
            internal IEnumerator <UpdateLinksContainerType>b__89_0(IList x);
        }
    }
}

