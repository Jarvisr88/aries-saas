namespace DevExpress.Xpf.Bars
{
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Utils.About;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Markup;
    using System.Windows.Media;

    [ToolboxTabName("DX.19.2: Navigation & Layout"), LicenseProvider(typeof(DX_WPF_ControlRequiredForReports_LicenseProvider)), DXToolboxBrowsable, ContentProperty("Child")]
    public class BarManager : Decorator, IBarManager, IRuntimeCustomizationHost
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static readonly DependencyProperty BarManagerProperty;
        [Browsable(false)]
        public static readonly DependencyProperty BarManagerInfoProperty;
        public static readonly DependencyProperty IsMenuModeProperty;
        private static readonly DependencyPropertyKey IsMenuModePropertyKey;
        public static readonly DependencyProperty MainMenuProperty;
        public static readonly DependencyProperty StatusBarProperty;
        public static readonly DependencyProperty ShowScreenTipsProperty;
        public static readonly DependencyProperty ShowShortcutInScreenTipsProperty;
        public static readonly DependencyProperty AllowHotCustomizationProperty;
        public static readonly DependencyProperty AllowQuickCustomizationProperty;
        public static readonly DependencyProperty AllowCustomizationProperty;
        public static readonly DependencyProperty ToolbarGlyphSizeProperty;
        public static readonly DependencyProperty MenuGlyphSizeProperty;
        public static readonly DependencyProperty MenuAnimationTypeProperty;
        public static readonly DependencyProperty CreateStandardLayoutProperty;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static readonly DependencyProperty DXContextMenuProperty;
        public static readonly DependencyProperty DXContextMenuPlacementProperty;
        public static readonly DependencyProperty DXContextMenuPlacementTargetProperty;
        public static readonly DependencyProperty MenuShowMouseButtonProperty;
        public static readonly DependencyProperty IsMDIChildManagerProperty;
        private static readonly DependencyPropertyKey IsMDIChildManagerPropertyKey;
        public static readonly RoutedEvent ItemClickEvent;
        public static readonly RoutedEvent ItemDoubleClickEvent;
        public static readonly RoutedEvent LayoutUpgradingEvent;
        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public static readonly DependencyProperty SkipMeasureByDockPanelLayoutHelperProperty;
        public static readonly DependencyProperty BarsSourceProperty;
        public static readonly DependencyProperty BarTemplateProperty;
        public static readonly DependencyProperty BarStyleProperty;
        public static readonly DependencyProperty BarStyleSelectorProperty;
        public static readonly DependencyProperty BarTemplateSelectorProperty;
        public static readonly DependencyProperty AllowUIAutomationSupportProperty;
        public static readonly DependencyProperty AddNewItemsProperty;
        public static readonly DependencyProperty ShowScreenTipsInPopupMenusProperty;
        protected static readonly DependencyPropertyKey IsMenuVisiblePropertyKey;
        public static readonly DependencyProperty IsMenuVisibleProperty;
        public static readonly DependencyProperty AllowGlyphThemingProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty BarsAttachedBehaviorProperty;
        public static readonly DependencyProperty MDIMergeStyleProperty;
        public static readonly DependencyProperty AllowNavigationFromEditorOnTabPressProperty;
        public static readonly DependencyProperty ShowGlyphsInPopupMenusProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty KeyGestureWorkingModeProperty;
        private static bool checkBarItemNames;
        private static bool ignoreMenuDropAlignment;
        private List<BarContainerControl> standardContainers;
        internal BarManagerThemeDependentValuesProvider ValuesProvider;
        private BarContainerControlCollection containers;
        private BarManagerCategoryCollection categories;
        private BarCollection bars;
        private BarItemCollection items;
        private Delegate onEnterMenuModeDelegate;
        private WeakReference iMDIChildHostWR;
        private EventHandler MDIChildHostMenuIsVisibleChangedHandler;
        private ThemedWindow themedWindow;
        private bool lockKeyGestureEventAfterHandling;
        private string oldVersion;
        private DevExpress.Xpf.Bars.ControllerBehavior controllerBehavior;
        private RuntimeCustomizationCollection runtimeCustomizations;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public event MouseEventHandler BeforeCheckCloseAllPopups;

        public event ItemClickEventHandler ItemClick;

        public event ItemClickEventHandler ItemDoubleClick;

        public event LayoutUpgradingEventHandler LayoutUpgrading;

        static BarManager();
        public BarManager();
        protected internal virtual void ActivateMenu();
        protected internal virtual void ActivateMenuOnGotFocus();
        public void AddBarToContainer(Bar bar, BarContainerControl container);
        protected internal void AddLogicalChild(object child);
        internal void AddStandardContainer(BarContainerControl control);
        private void AddValuesProvider();
        protected internal virtual void AddVisualChildInternal(UIElement child);
        protected override Size ArrangeOverride(Size arrangeSize);
        protected internal virtual bool CheckCloseAllPopups(MouseEventArgs e);
        private void ClearCollectionsBeforeRestoreLayout();
        private void ClearItemLinkCollectionBeforeRestoreLayout(BarItemLinkCollection linkCollection);
        [Obsolete("Use the PopupMenuManager.CloseAllPopups instead")]
        public virtual void CloseAllPopups();
        protected virtual bool? CoerceAllowMerging(bool? v);
        protected virtual BarCollection CreateBars();
        protected virtual BarManagerCategoryCollection CreateCategories();
        protected virtual BarContainerControlCollection CreateContainers();
        protected virtual DevExpress.Xpf.Bars.ControllerBehavior CreateControllerBehavior();
        [Obsolete("Implement the ICustomizationService interface to change the customization behavior")]
        protected virtual BarManagerCustomizationHelper CreateCustomizationHelper();
        protected virtual BarItemCollection CreateItems();
        protected internal virtual void DeactivateMenu();
        void IBarManager.CorrectBarManagerInDesignTime();
        void IRuntimeCustomizationHost.CustomizationApplyingSkipped(RuntimeCustomization customization);
        DependencyObject IRuntimeCustomizationHost.FindTarget(string targetName);
        protected void EnableKeyboardCues(bool enable);
        private void EnableKeyboardCues(DependencyObject element, bool enable);
        protected virtual Window GetActiveWindow();
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static BarManager GetBarManager(DependencyObject dobj);
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Obsolete("Use the BarManager property instead"), Browsable(false)]
        public static BarManagerInfo GetBarManagerInfo(DependencyObject dobj);
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static IPopupControl GetDXContextMenu(UIElement element);
        public static PlacementMode GetDXContextMenuPlacement(UIElement element);
        public static UIElement GetDXContextMenuPlacementTarget(UIElement element);
        protected internal bool GetHotQuickCustomization();
        public static DevExpress.Xpf.Bars.KeyGestureWorkingMode? GetKeyGestureWorkingMode(DependencyObject obj);
        private IEnumerator GetLogicalChildrenEnumerator();
        [IteratorStateMachine(typeof(BarManager.<GetLogicalChildrenEnumeratorImpl>d__251))]
        private IEnumerable GetLogicalChildrenEnumeratorImpl();
        public static GlyphSize GetMenuGlyphSize(DependencyObject dObj);
        protected virtual Bar GetMenuModeTarget();
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static ButtonSwitcher GetMenuShowMouseButton(UIElement element);
        private string GetSerializationAppName();
        public static bool? GetShowGlyphsInPopupMenus(DependencyObject element);
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static bool GetSkipMeasureByDockPanelLayoutHelper(DependencyObject obj);
        public static GlyphSize GetToolbarGlyphSize(DependencyObject dObj);
        protected override Visual GetVisualChild(int index);
        protected override HitTestResult HitTestCore(PointHitTestParameters hitTestParameters);
        protected virtual void Initialize();
        protected internal virtual void InitializeItemLinks(BarItemLinkCollection coll);
        protected virtual void InitializeItemsCategories();
        private bool IsAltKeyPressed(KeyEventArgs e);
        protected virtual bool IsAnyPopupFocused();
        protected virtual bool IsFocusedManager();
        protected virtual bool IsVisualOrLogicAncestor(DependencyObject parent, DependencyObject child);
        protected override Size MeasureOverride(Size constraint);
        protected virtual void OnAccessKeyPressed(AccessKeyPressedEventArgs e);
        private static void OnAccessKeyPressed(object sender, AccessKeyPressedEventArgs e);
        protected virtual void OnActivateMenuModeWhenBarItemFocusedChanged(bool oldValue);
        protected virtual void OnAllowGlyphThemingChanged(bool oldValue);
        protected static void OnAllowQuckCustomizationPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e);
        protected virtual void OnAllowQuickCustomizationChanged(DependencyPropertyChangedEventArgs e);
        protected virtual void OnAllowUIAutomationSupportChanged(bool oldValue);
        protected static void OnAllowUIAutomationSupportPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        protected internal virtual void OnBarItemAdded(BarItem item);
        protected internal virtual void OnBarItemRemoved(BarItem item);
        protected static void OnBarManagerInfoPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e);
        protected static void OnBarManagerPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e);
        protected internal virtual void OnBarsSourceChanged(DependencyPropertyChangedEventArgs e);
        protected static void OnBarsSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        protected virtual void OnBarTemplateChanged(DependencyPropertyChangedEventArgs e);
        protected static void OnBarTemplatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        protected virtual void OnBarTemplateSelectorChanged(DependencyPropertyChangedEventArgs e);
        protected static void OnBarTemplateSelectorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        protected internal virtual void OnControllerAdded(IActionContainer controller);
        protected internal virtual void OnControllerRemoved(IActionContainer controller);
        protected override AutomationPeer OnCreateAutomationPeer();
        private static void OnDXContextMenuChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        protected virtual bool OnEnterMenuMode(object sender, EventArgs e);
        protected override void OnInitialized(EventArgs e);
        protected virtual void OnIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e);
        protected virtual void OnIsMenuModeChanged(bool oldValue, bool newValue);
        private void OnIsMenuVisibleChanged();
        protected internal void OnItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e);
        protected virtual void OnLoaded(object sender, EventArgs e);
        protected virtual void OnMainMenuChanged(DependencyPropertyChangedEventArgs e);
        protected static void OnMainMenuPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e);
        protected virtual void OnMDIMergeStyleChanged(DevExpress.Xpf.Bars.MDIMergeStyle oldValue);
        protected static void OnMDIMergeStylePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        protected virtual void OnStatusBarChanged(DependencyPropertyChangedEventArgs e);
        protected static void OnStatusBarPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e);
        protected virtual void OnUnloaded(object sender, RoutedEventArgs e);
        [Obsolete("Use the RaiseItemClick(BarItem item, BarItemLink link, BarItemLinkControl linkControl) method instead")]
        protected internal virtual void RaiseItemClick(BarItem item, BarItemLink link);
        protected internal virtual void RaiseItemClick(BarItem item, BarItemLink link, IBarItemLinkControl linkControl);
        [Obsolete("Use the RaiseItemDoubleClick(BarItem item, BarItemLink link, BarItemLinkControl linkControl) method instead")]
        protected internal virtual void RaiseItemDoubleClick(BarItem item, BarItemLink link);
        protected internal virtual void RaiseItemDoubleClick(BarItem item, BarItemLink link, IBarItemLinkControl linkControl);
        protected internal void RemoveLogicalChild(object child);
        protected internal virtual void RemoveVisualChildInternal(UIElement child);
        private void RestoreLayoutCore(object path);
        public virtual void RestoreLayoutFromStream(Stream stream);
        public virtual void RestoreLayoutFromXml(string xmlFile);
        private void SaveLayoutCore(object path);
        public virtual void SaveLayoutToStream(Stream stream);
        public virtual void SaveLayoutToXml(string xmlFile);
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static void SetBarManager(DependencyObject dobj, BarManager value);
        [Obsolete("Use the BarManager property instead"), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static void SetBarManagerInfo(DependencyObject dobj, BarManagerInfo value);
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static void SetDXContextMenu(UIElement element, IPopupControl value);
        public static void SetDXContextMenuPlacement(UIElement element, PlacementMode value);
        public static void SetDXContextMenuPlacementTarget(UIElement element, UIElement value);
        public static void SetKeyGestureWorkingMode(DependencyObject obj, DevExpress.Xpf.Bars.KeyGestureWorkingMode? value);
        public static void SetMenuGlyphSize(DependencyObject dObj, GlyphSize value);
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static void SetMenuShowMouseButton(UIElement element, ButtonSwitcher value);
        public static void SetShowGlyphsInPopupMenus(DependencyObject element, bool? value);
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static void SetSkipMeasureByDockPanelLayoutHelper(DependencyObject obj, bool value);
        public static void SetToolbarGlyphSize(DependencyObject dObj, GlyphSize value);
        protected virtual void SubscribeKeyboardNavigationEvents();
        protected virtual void UnsubscribeKeyboardNavigationEvents();
        internal void UpdateGlyphColorization();
        protected virtual void UpdateMenuVisibility();

        protected internal ILogicalChildrenContainer LogicalChildrenContainer { get; private set; }

        public static bool CheckBarItemNames { get; set; }

        public static bool IgnoreMenuDropAlignment { get; set; }

        private IMDIChildHost MDIChildHost { get; set; }

        internal WeakReference RibbonControl { get; set; }

        internal WeakReference RibbonStatusBar { get; set; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public DevExpress.Xpf.Bars.KeyGestureWorkingMode KeyGestureWorkingMode { get; set; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool LockKeyGestureEventAfterHandling { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), Description("This member supports the internal infrastructure, and is not intended to be used directly from your code.")]
        public ObservableCollection<IControllerAction> Controllers { get; }

        [Description("Provides access to the object that provides methods to work with the Customization Window and customization menus.")]
        public BarManagerCustomizationHelper CustomizationHelper { get; }

        [Description("Gets or sets whether the bar manager implicitly creates four BarContainerControls at the four edges of the BarManager control, allowing you to dock Bars at these positions.This is a dependency property.")]
        public bool CreateStandardLayout { get; set; }

        [Description("Gets or sets whether a small or large image is displayed by bar item links within bars that belong to the current bar manager.This is a dependency property.")]
        public GlyphSize ToolbarGlyphSize { get; set; }

        [Description("Gets or sets whether a small or large image is displayed by bar item links within popup menus and sub-menus that belong to the current bar manager.This is a dependency property.")]
        public GlyphSize MenuGlyphSize { get; set; }

        [Description("Gets or sets if new items, added to the BarManager object after a customized layout is saved to an .xml file, will remain after restoring this layout. This is a dependency property.")]
        public bool AddNewItems { get; set; }

        [Description("Gets or sets if the BarManager's bars can be merged.")]
        public DevExpress.Xpf.Bars.MDIMergeStyle MDIMergeStyle { get; set; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool AllowNavigationFromEditorOnTabPress { get; set; }

        protected internal BarContainerControlCollection Containers { get; }

        public DataTemplate BarTemplate { get; set; }

        public DataTemplateSelector BarTemplateSelector { get; set; }

        public Style BarStyle { get; set; }

        public StyleSelector BarStyleSelector { get; set; }

        public bool AllowUIAutomationSupport { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Description("Gets the bar collection of the BarManager.")]
        public BarCollection Bars { get; }

        [Description("Provides access to the collection of bar items owned by the BarManager."), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public BarItemCollection Items { get; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Description("Gets a collection of categories used to logically organize bar items.")]
        public BarManagerCategoryCollection Categories { get; }

        public object BarsSource { get; set; }

        public bool ShowScreenTipsInPopupMenus { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Description("Gets or sets the main menu.This is a dependency property."), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public Bar MainMenu { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), Description("Gets or sets the status bar.This is a dependency property."), EditorBrowsable(EditorBrowsableState.Never)]
        public Bar StatusBar { get; set; }

        [Description("Gets whether the current BarManager belongs to a DocumentPanel which resides within another BarManager.")]
        public bool IsMDIChildManager { get; internal set; }

        [Description("Gets whether the main menu is activated by pressing the ALT key.This is a dependency property.")]
        public bool IsMenuMode { get; internal set; }

        public bool IsMenuVisible { get; protected internal set; }

        public bool AllowGlyphTheming { get; set; }

        [Description("Gets or sets whether tooltips for bar item links are enabled.This is a dependency property.")]
        public bool ShowScreenTips { get; set; }

        [Description("Gets or sets a value indicating whether shortcut keys (specified by the BarItem.KeyGesture property) must be displayed along with a hint for bar item links. This is a dependency property.")]
        public bool ShowShortcutInScreenTips { get; set; }

        [Description("Gets or sets whether a bar can be customized without invoking Customization Mode, by dragging bar item links while holding the ALT key down. This is a dependency property.")]
        public bool AllowHotCustomization { get; set; }

        [Description("Gets or sets whether bars provide Quick Customization Buttons, opening the customization menu. This is a dependency property.")]
        public bool AllowQuickCustomization { get; set; }

        [Description("Gets or sets whether bar customization is supported at runtime.This is a dependency property.")]
        public bool AllowCustomization { get; set; }

        [Description("Gets or sets the type of animation used by menus.This is a dependency property.")]
        public PopupAnimation MenuAnimationType { get; set; }

        protected bool ManagerInitialized { get; private set; }

        protected override IEnumerator LogicalChildren { get; }

        protected override int VisualChildrenCount { get; }

        protected internal List<BarContainerControl> StandardContainers { get; }

        internal static bool SkipFloatingBarHiding { get; set; }

        private bool IsManagerInvisible { get; }

        protected internal DevExpress.Xpf.Bars.ControllerBehavior ControllerBehavior { get; }

        [XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, true, false, 0, XtraSerializationFlags.None)]
        public RuntimeCustomizationCollection RuntimeCustomizations { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BarManager.<>c <>9;
            public static LinkControlAction<IBarItemLinkControl> <>9__56_1;
            public static Action<BarItem> <>9__56_0;
            public static Action<WeakReference, WeakReference> <>9__57_0;
            public static Func<KeyGestureWorkingMode?, KeyGestureWorkingMode> <>9__111_0;
            public static Func<KeyGestureWorkingMode> <>9__111_1;
            public static Func<Bar, bool> <>9__232_0;
            public static Func<Bar, BarDockInfo> <>9__232_2;
            public static Func<BarDockInfo, BarControl> <>9__232_3;
            public static Func<Bar, bool> <>9__232_1;
            public static Func<Bar, BarDockInfo> <>9__236_0;
            public static Func<BarDockInfo, bool> <>9__236_1;
            public static Func<BarDockInfo, BarControl> <>9__236_2;
            public static Func<BarControl, bool> <>9__236_3;
            public static Action<BarControl> <>9__236_4;
            public static Func<DependencyObject, BarPopupBase> <>9__279_0;
            public static Func<BarManager, IList> <>9__311_0;
            public static Func<BarManager, Bar> <>9__311_1;

            static <>c();
            internal object <.cctor>b__43_0(DependencyObject d, object v);
            internal void <.cctor>b__43_1(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__43_2(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__43_3(DependencyObject o, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__43_4(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal KeyGestureWorkingMode <get_KeyGestureWorkingMode>b__111_0(KeyGestureWorkingMode? x);
            internal KeyGestureWorkingMode <get_KeyGestureWorkingMode>b__111_1();
            internal bool <GetMenuModeTarget>b__232_0(Bar x);
            internal bool <GetMenuModeTarget>b__232_1(Bar bar);
            internal BarDockInfo <GetMenuModeTarget>b__232_2(Bar x);
            internal BarControl <GetMenuModeTarget>b__232_3(BarDockInfo x);
            internal BarPopupBase <IsAnyPopupFocused>b__279_0(DependencyObject x);
            internal BarDockInfo <OnAllowQuickCustomizationChanged>b__236_0(Bar x);
            internal bool <OnAllowQuickCustomizationChanged>b__236_1(BarDockInfo x);
            internal BarControl <OnAllowQuickCustomizationChanged>b__236_2(BarDockInfo x);
            internal bool <OnAllowQuickCustomizationChanged>b__236_3(BarControl x);
            internal void <OnAllowQuickCustomizationChanged>b__236_4(BarControl x);
            internal IList <OnBarsSourceChanged>b__311_0(BarManager barManager);
            internal Bar <OnBarsSourceChanged>b__311_1(BarManager barManager);
            internal void <OnDXContextMenuChanged>b__57_0(WeakReference cE, WeakReference cM);
            internal void <UpdateGlyphColorization>b__56_0(BarItem x);
            internal void <UpdateGlyphColorization>b__56_1(IBarItemLinkControl lc);
        }

        [CompilerGenerated]
        private sealed class <GetLogicalChildrenEnumeratorImpl>d__251 : IEnumerable<object>, IEnumerable, IEnumerator<object>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private object <>2__current;
            private int <>l__initialThreadId;
            public BarManager <>4__this;
            private IEnumerator<BarItem> <>7__wrap1;
            private IEnumerator<Bar> <>7__wrap2;
            private IEnumerator<BarManagerCategory> <>7__wrap3;
            private IEnumerator <>7__wrap4;

            [DebuggerHidden]
            public <GetLogicalChildrenEnumeratorImpl>d__251(int <>1__state);
            private void <>m__Finally1();
            private void <>m__Finally2();
            private void <>m__Finally3();
            private void <>m__Finally4();
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<object> IEnumerable<object>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            object IEnumerator<object>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }
    }
}

