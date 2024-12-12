namespace DevExpress.Xpf.Docking
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Utils.Serializing;
    using DevExpress.Xpf;
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Bars.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Core.Serialization;
    using DevExpress.Xpf.Docking.Base;
    using DevExpress.Xpf.Docking.Customization;
    using DevExpress.Xpf.Docking.Images;
    using DevExpress.Xpf.Docking.Platform;
    using DevExpress.Xpf.Docking.UIAutomation;
    using DevExpress.Xpf.Docking.VisualElements;
    using DevExpress.Xpf.Layout.Core;
    using DevExpress.Xpf.Utils;
    using DevExpress.Xpf.Utils.About;
    using Microsoft.Win32;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Markup;
    using System.Windows.Media;
    using System.Windows.Threading;

    [SupportDXTheme(TypeInAssembly=typeof(DockLayoutManager)), DXToolboxBrowsable, ContentProperty("LayoutRoot"), TemplatePart(Name="PART_LeftAutoHideTray", Type=typeof(AutoHideTray)), TemplatePart(Name="PART_RightAutoHideTray", Type=typeof(AutoHideTray)), TemplatePart(Name="PART_TopAutoHideTray", Type=typeof(AutoHideTray)), TemplatePart(Name="PART_BottomAutoHideTray", Type=typeof(AutoHideTray)), TemplatePart(Name="PART_LeftAutoHideTrayPanel", Type=typeof(AutoHidePane)), TemplatePart(Name="PART_RightAutoHideTrayPanel", Type=typeof(AutoHidePane)), TemplatePart(Name="PART_TopAutoHideTrayPanel", Type=typeof(AutoHidePane)), TemplatePart(Name="PART_BottomAutoHideTrayPanel", Type=typeof(AutoHidePane)), LicenseProvider(typeof(DX_WPF_ControlRequiredForReports_LicenseProvider)), TemplatePart(Name="PART_ClosedItemsPanel", Type=typeof(DevExpress.Xpf.Docking.ClosedItemsPanel))]
    public class DockLayoutManager : psvControl, IWeakEventListener, IUIElement, ISupportBatchUpdate, ILogicalOwner, IInputElement
    {
        public static readonly DependencyProperty DockLayoutManagerProperty;
        public static readonly DependencyProperty LayoutItemProperty;
        public static readonly DependencyProperty UIScopeProperty;
        public static readonly DependencyProperty LayoutRootProperty;
        public static readonly DependencyProperty FloatingModeProperty;
        public static readonly DependencyProperty ClosedPanelsBarVisibilityProperty;
        public static readonly DependencyProperty ActiveDockItemProperty;
        public static readonly DependencyProperty ActiveLayoutItemProperty;
        public static readonly DependencyProperty ActiveMDIItemProperty;
        public static readonly DependencyProperty IsCustomizationProperty;
        private static readonly DependencyPropertyKey IsCustomizationPropertyKey;
        public static readonly DependencyProperty IsRenamingProperty;
        private static readonly DependencyPropertyKey IsRenamingPropertyKey;
        public static readonly DependencyProperty AllowDockItemRenameProperty;
        public static readonly DependencyProperty AllowLayoutItemRenameProperty;
        public static readonly DependencyProperty AllowCustomizationProperty;
        public static readonly DependencyProperty AllowDocumentSelectorProperty;
        public static readonly DependencyProperty AutoHideExpandModeProperty;
        public static readonly DependencyProperty AutoHideModeProperty;
        public static readonly DependencyProperty AllowMergingAutoHidePanelsProperty;
        public static readonly DependencyProperty ShowInvisibleItemsProperty;
        public static readonly DependencyProperty ShowInvisibleItemsInCustomizationFormProperty;
        public static readonly DependencyProperty DestroyLastDocumentGroupProperty;
        public static readonly DependencyProperty ClosedPanelsBarPositionProperty;
        public static readonly DependencyProperty DefaultTabPageCaptionImageProperty;
        public static readonly DependencyProperty DefaultAutoHidePanelCaptionImageProperty;
        public static readonly RoutedEvent RequestUniqueNameEvent;
        public static readonly RoutedEvent ShowingMenuEvent;
        public static readonly RoutedEvent DockItemActivatedEvent;
        public static readonly RoutedEvent LayoutItemActivatedEvent;
        public static readonly RoutedEvent MDIItemActivatedEvent;
        public static readonly RoutedEvent DockItemActivatingEvent;
        public static readonly RoutedEvent DockItemStartDockingEvent;
        public static readonly RoutedEvent DockItemDockingEvent;
        public static readonly RoutedEvent DockItemEndDockingEvent;
        public static readonly RoutedEvent DockItemClosingEvent;
        public static readonly RoutedEvent DockItemClosedEvent;
        public static readonly RoutedEvent DockItemHidingEvent;
        public static readonly RoutedEvent DockItemHiddenEvent;
        public static readonly RoutedEvent DockItemRestoringEvent;
        public static readonly RoutedEvent DockItemRestoredEvent;
        public static readonly RoutedEvent DockItemDraggingEvent;
        public static readonly RoutedEvent DockItemExpandedEvent;
        public static readonly RoutedEvent DockItemCollapsedEvent;
        public static readonly RoutedEvent LayoutItemActivatingEvent;
        public static readonly RoutedEvent LayoutItemSelectionChangingEvent;
        public static readonly RoutedEvent LayoutItemSelectionChangedEvent;
        public static readonly RoutedEvent LayoutItemSizeChangedEvent;
        public static readonly RoutedEvent LayoutItemHiddenEvent;
        public static readonly RoutedEvent LayoutItemRestoredEvent;
        public static readonly RoutedEvent LayoutItemMovedEvent;
        public static readonly RoutedEvent IsCustomizationChangedEvent;
        public static readonly RoutedEvent CustomizationFormVisibleChangedEvent;
        public static readonly RoutedEvent LayoutItemStartRenamingEvent;
        public static readonly RoutedEvent LayoutItemEndRenamingEvent;
        public static readonly RoutedEvent MDIItemActivatingEvent;
        public static readonly RoutedEvent ShowInvisibleItemsChangedEvent;
        public static readonly RoutedEvent ItemIsVisibleChangedEvent;
        public static readonly RoutedEvent ShowingDockHintsEvent;
        public static readonly RoutedEvent MergeEvent;
        public static readonly RoutedEvent UnMergeEvent;
        public static readonly RoutedEvent BeforeItemAddedEvent;
        public static readonly RoutedEvent DockOperationStartingEvent;
        public static readonly RoutedEvent DockOperationCompletedEvent;
        [IgnoreDependencyPropertiesConsistencyChecker]
        internal static readonly DependencyProperty OwnerWindowTitleProperty;
        public static readonly DependencyProperty ShowMaximizedDocumentCaptionInWindowTitleProperty;
        public static readonly DependencyProperty WindowTitleFormatProperty;
        public static readonly DependencyProperty DisposeOnWindowClosingProperty;
        public static readonly DependencyProperty AllowFloatGroupTransparencyProperty;
        public static readonly DependencyProperty EnableWin32CompatibilityProperty;
        public static readonly DependencyProperty ShowFloatWindowsInTaskbarProperty;
        public static readonly DependencyProperty OwnsFloatWindowsProperty;
        public static readonly DependencyProperty ItemsSourceProperty;
        public static readonly DependencyProperty ItemTemplateProperty;
        public static readonly DependencyProperty ItemTemplateSelectorProperty;
        public static readonly DependencyProperty IsSynchronizedWithCurrentItemProperty;
        public static readonly DependencyProperty MDIMergeStyleProperty;
        public static readonly DependencyProperty RedrawContentWhenResizingProperty;
        public static readonly DependencyProperty DockingStyleProperty;
        public static readonly DependencyProperty FloatingDocumentContainerProperty;
        public static readonly DependencyProperty AllowAeroSnapProperty;
        public static readonly DependencyProperty CloseFloatWindowsOnManagerUnloadingProperty;
        public static readonly DependencyProperty AutoHideGroupsCheckIntervalProperty;
        public static readonly DependencyProperty LogicalTreeStructureProperty;
        public static readonly DependencyProperty ViewStyleProperty;
        public static readonly DependencyProperty ShowContentWhenDraggingProperty;
        public static readonly DependencyProperty IsDarkThemeProperty;
        private static readonly DependencyPropertyKey IsDarkThemePropertyKey;
        public static readonly DependencyProperty HandleHwndHostMouseEventsProperty;
        internal int isDockItemActivation;
        internal int isLayoutItemActivation;
        internal int isMDIItemActivation;
        protected DockLayoutManagerItemsCollection itemsInternal;
        private readonly LayoutGroupGenerator layoutGroupGenerator;
        private readonly Dictionary<string, WeakList<object>> pendingItems = new Dictionary<string, WeakList<object>>();
        protected AutoHidePane PartBottomAutoHidePane;
        protected AutoHideTray PartBottomAutoHideTray;
        protected AutoHidePane PartLeftAutoHidePane;
        protected AutoHideTray PartLeftAutoHideTray;
        protected AutoHidePane PartRightAutoHidePane;
        protected AutoHideTray PartRightAutoHideTray;
        protected AutoHidePane PartTopAutoHidePane;
        protected AutoHideTray PartTopAutoHideTray;
        private readonly FocusLocker _focusLocker = new FocusLocker();
        private readonly PendingActionsHelper _layoutLocker = new PendingActionsHelper();
        private readonly WeakList<DockLayoutManager> _Linked = new WeakList<DockLayoutManager>();
        private readonly List<AutoHideTray> autoHideTrayCollection = new List<AutoHideTray>();
        private readonly Locker closedPanelsLocker = new Locker();
        private readonly Locker itemToLinkBinderServiceLocker = new Locker();
        private readonly List<LogicalTreeLocker> logicalTreeLocks = new List<LogicalTreeLocker>();
        private readonly Locker themeChangedLocker = new Locker();
        private readonly Locker windowTitleLocker = new Locker();
        private bool _ManualClosedPanelsBarVisibility;
        private string _previousTheme;
        private int activationLockCount;
        private DevExpress.Xpf.Docking.Platform.Win32AdornerWindowProvider adornerWindowProvider;
        private AutoHideGroupCollection autoHideGroupsCore;
        private ClosedPanelCollection closedPanelsCore;
        private InputBinding closeMDIItemCommandBinding;
        private ContainerGenerator containerGenerator;
        private ICustomizationController customizationControllerCore;
        private LayoutGroupCollection decomposedItems;
        private DevExpress.Xpf.Docking.DelayedActivationHelper delayedActivationHelper;
        private DisableFloatingPanelTransparencyBehavior disableTransparencyBehavior;
        private IDisposable displaySettingsListener;
        private DevExpress.Xpf.Docking.DockControllerImpl dockControllerImpl;
        private IDockController dockControllerCore;
        private DispatcherOperation dockItemActivatedOperation;
        private Point? effectiveRestoreOffset;
        private DispatcherOperation ensureAdornerVisibilityOperation;
        private FloatGroupCollection floatGroupsCore;
        private int floatingCounter;
        private DelayedActionsHelper initilizedActionsHelper;
        private ILayoutController layoutControllerCore;
        private LayoutItemStateHelper layoutItemStateHelper;
        private DelayedActionsHelper loadedActionsHelper;
        private int lockOwnerWindowTitleChanged;
        private int lockUpdateCounter;
        private IMDIController mdiControllerCore;
        private DockLayoutManagerMergingHelper mergingHelper;
        private DocumentPanel mergingTarget;
        private string OwnerWindowTitle;
        private BaseLayoutItem previousActiveItem;
        private DevExpress.Xpf.Docking.RenameHelper renameHelperCore;
        private DispatcherOperation restoreCustomizationOperation;
        private ISerializationController serializationControllerCore;
        private bool shouldUpdateLayout;
        private Window topLevelWindow;
        private ThemeTreeWalkerHelper treeWalkerHelper;
        private DispatcherOperation updateFloatingPaneResourcesOperation;
        private int updatesCount;
        private DockLayoutManagerThemeDependentValuesProvider ValuesProvider;
        private IViewAdapter viewAdapterCore;
        private VisualCollection visualChildrenCore;
        private WeakReference walker;
        private DevExpress.Xpf.Docking.Platform.Win32DragService win32DragService;
        private UIChildren uiChildren;
        private readonly LogicalChildrenCollection logicalChildrenCore;
        private readonly LogicalChildrenCollection internalElements;

        protected internal event PropertyChangedEventHandler AutoHideDisplayModeChanged;

        [Description("Fires before an item is added to the current DockLayoutManager object.")]
        public event BeforeItemAddedEventHandler BeforeItemAdded
        {
            add
            {
                base.AddHandler(BeforeItemAddedEvent, value);
            }
            remove
            {
                base.RemoveHandler(BeforeItemAddedEvent, value);
            }
        }

        [Description("Fires after the Customization Window has been displayed or hidden.")]
        public event CustomizationFormVisibleChangedEventHandler CustomizationFormVisibleChanged
        {
            add
            {
                base.AddHandler(CustomizationFormVisibleChangedEvent, value);
            }
            remove
            {
                base.RemoveHandler(CustomizationFormVisibleChangedEvent, value);
            }
        }

        event RoutedEventHandler ILogicalOwner.Loaded
        {
            add
            {
                base.Loaded += value;
            }
            remove
            {
                base.Loaded -= value;
            }
        }

        [Description("Fires after a dock item has been activated.")]
        public event DockItemActivatedEventHandler DockItemActivated
        {
            add
            {
                base.AddHandler(DockItemActivatedEvent, value);
            }
            remove
            {
                base.RemoveHandler(DockItemActivatedEvent, value);
            }
        }

        [Description("Fires before a dock item is activated, and allows you to prevent this action.")]
        public event DockItemCancelEventHandler DockItemActivating
        {
            add
            {
                base.AddHandler(DockItemActivatingEvent, value);
            }
            remove
            {
                base.RemoveHandler(DockItemActivatingEvent, value);
            }
        }

        [Description("Fires after a dock item has been closed (hidden).")]
        public event DockItemClosedEventHandler DockItemClosed
        {
            add
            {
                base.AddHandler(DockItemClosedEvent, value);
            }
            remove
            {
                base.RemoveHandler(DockItemClosedEvent, value);
            }
        }

        [Description("Fires before a dock item is closed (hidden), and allows you to prevent this action.")]
        public event DockItemCancelEventHandler DockItemClosing
        {
            add
            {
                base.AddHandler(DockItemClosingEvent, value);
            }
            remove
            {
                base.RemoveHandler(DockItemClosingEvent, value);
            }
        }

        [Description("Fires after a visible auto-hidden dock panel has slid away.")]
        public event DockItemCollapsedEventHandler DockItemCollapsed
        {
            add
            {
                base.AddHandler(DockItemCollapsedEvent, value);
            }
            remove
            {
                base.RemoveHandler(DockItemCollapsedEvent, value);
            }
        }

        [Description("Fires before a dock item is dragged over dock hints, and allows you to prevent dock zones from being displayed.")]
        public event DockItemDockingEventHandler DockItemDocking
        {
            add
            {
                base.AddHandler(DockItemDockingEvent, value);
            }
            remove
            {
                base.RemoveHandler(DockItemDockingEvent, value);
            }
        }

        [Description("Fires repeatedly while a dock panel is being dragged.")]
        public event DockItemDraggingEventHandler DockItemDragging
        {
            add
            {
                base.AddHandler(DockItemDraggingEvent, value);
            }
            remove
            {
                base.RemoveHandler(DockItemDraggingEvent, value);
            }
        }

        [Description("Fires after a dock item has been dropped, and allows you to prevent this action.")]
        public event DockItemDockingEventHandler DockItemEndDocking
        {
            add
            {
                base.AddHandler(DockItemEndDockingEvent, value);
            }
            remove
            {
                base.RemoveHandler(DockItemEndDockingEvent, value);
            }
        }

        [Description("Fires after a hidden auto-hidden dock panel has slid out.")]
        public event DockItemExpandedEventHandler DockItemExpanded
        {
            add
            {
                base.AddHandler(DockItemExpandedEvent, value);
            }
            remove
            {
                base.RemoveHandler(DockItemExpandedEvent, value);
            }
        }

        [Description("Fires after a dock item has been made auto-hidden.")]
        public event DockItemEventHandler DockItemHidden
        {
            add
            {
                base.AddHandler(DockItemHiddenEvent, value);
            }
            remove
            {
                base.RemoveHandler(DockItemHiddenEvent, value);
            }
        }

        [Description("Fires before a dock item is auto-hidden, and allows you to prevent this action.")]
        public event DockItemCancelEventHandler DockItemHiding
        {
            add
            {
                base.AddHandler(DockItemHidingEvent, value);
            }
            remove
            {
                base.RemoveHandler(DockItemHidingEvent, value);
            }
        }

        [Description("Fires after a dock item has been restored from the closed (hidden) state.")]
        public event DockItemEventHandler DockItemRestored
        {
            add
            {
                base.AddHandler(DockItemRestoredEvent, value);
            }
            remove
            {
                base.RemoveHandler(DockItemRestoredEvent, value);
            }
        }

        [Description("Fires before a dock item is restored from the closed (hidden) state, and allows you to prevent this action.")]
        public event DockItemCancelEventHandler DockItemRestoring
        {
            add
            {
                base.AddHandler(DockItemRestoringEvent, value);
            }
            remove
            {
                base.RemoveHandler(DockItemRestoringEvent, value);
            }
        }

        [Description("Fires when a docking operation starts, and allows you to prevent this operation.")]
        public event DockItemCancelEventHandler DockItemStartDocking
        {
            add
            {
                base.AddHandler(DockItemStartDockingEvent, value);
            }
            remove
            {
                base.RemoveHandler(DockItemStartDockingEvent, value);
            }
        }

        public event DockOperationCompletedEventHandler DockOperationCompleted
        {
            add
            {
                base.AddHandler(DockOperationCompletedEvent, value);
            }
            remove
            {
                base.RemoveHandler(DockOperationCompletedEvent, value);
            }
        }

        public event DockOperationStartingEventHandler DockOperationStarting
        {
            add
            {
                base.AddHandler(DockOperationStartingEvent, value);
            }
            remove
            {
                base.RemoveHandler(DockOperationStartingEvent, value);
            }
        }

        [Description("Fires when Customization Mode is enabled or disabled.")]
        public event IsCustomizationChangedEventHandler IsCustomizationChanged
        {
            add
            {
                base.AddHandler(IsCustomizationChangedEvent, value);
            }
            remove
            {
                base.RemoveHandler(IsCustomizationChangedEvent, value);
            }
        }

        [Description("Fires when the item's IsVisible property is changed.")]
        public event ItemIsVisibleChangedEventHandler ItemIsVisibleChanged
        {
            add
            {
                base.AddHandler(ItemIsVisibleChangedEvent, value);
            }
            remove
            {
                base.RemoveHandler(ItemIsVisibleChangedEvent, value);
            }
        }

        [Description("Fires after a layout item has been activated.")]
        public event LayoutItemActivatedEventHandler LayoutItemActivated
        {
            add
            {
                base.AddHandler(LayoutItemActivatedEvent, value);
            }
            remove
            {
                base.RemoveHandler(LayoutItemActivatedEvent, value);
            }
        }

        [Description("Fires when a layout item is about to be activated.")]
        public event LayoutItemCancelEventHandler LayoutItemActivating
        {
            add
            {
                base.AddHandler(LayoutItemActivatingEvent, value);
            }
            remove
            {
                base.RemoveHandler(LayoutItemActivatingEvent, value);
            }
        }

        [Description("Fires when layout item renaming is completed.")]
        public event LayoutItemEndRenamingEventHandler LayoutItemEndRenaming
        {
            add
            {
                base.AddHandler(LayoutItemEndRenamingEvent, value);
            }
            remove
            {
                base.RemoveHandler(LayoutItemEndRenamingEvent, value);
            }
        }

        [Description("Fires when a layout item is hidden.")]
        public event LayoutItemHiddenEventHandler LayoutItemHidden
        {
            add
            {
                base.AddHandler(LayoutItemHiddenEvent, value);
            }
            remove
            {
                base.RemoveHandler(LayoutItemHiddenEvent, value);
            }
        }

        [Description("Fires after a layout item has been moved to a new position.")]
        public event LayoutItemMovedEventHandler LayoutItemMoved
        {
            add
            {
                base.AddHandler(LayoutItemMovedEvent, value);
            }
            remove
            {
                base.RemoveHandler(LayoutItemMovedEvent, value);
            }
        }

        [Description("Fires when a hidden layout item is restored (added to the layout).")]
        public event LayoutItemRestoredEventHandler LayoutItemRestored
        {
            add
            {
                base.AddHandler(LayoutItemRestoredEvent, value);
            }
            remove
            {
                base.RemoveHandler(LayoutItemRestoredEvent, value);
            }
        }

        [Description("Fires after the layout item's selection state has changed.")]
        public event LayoutItemSelectionChangedEventHandler LayoutItemSelectionChanged
        {
            add
            {
                base.AddHandler(LayoutItemSelectionChangedEvent, value);
            }
            remove
            {
                base.RemoveHandler(LayoutItemSelectionChangedEvent, value);
            }
        }

        [Description("Fires when the layout item's selection state is about to be changed.")]
        public event LayoutItemSelectionChangingEventHandler LayoutItemSelectionChanging
        {
            add
            {
                base.AddHandler(LayoutItemSelectionChangingEvent, value);
            }
            remove
            {
                base.RemoveHandler(LayoutItemSelectionChangingEvent, value);
            }
        }

        [Description("Fires after a layout item's width/height has changed.")]
        public event LayoutItemSizeChangedEventHandler LayoutItemSizeChanged
        {
            add
            {
                base.AddHandler(LayoutItemSizeChangedEvent, value);
            }
            remove
            {
                base.RemoveHandler(LayoutItemSizeChangedEvent, value);
            }
        }

        [Description("Fires when layout item renaming is initiated.")]
        public event LayoutItemCancelEventHandler LayoutItemStartRenaming
        {
            add
            {
                base.AddHandler(LayoutItemStartRenamingEvent, value);
            }
            remove
            {
                base.RemoveHandler(LayoutItemStartRenamingEvent, value);
            }
        }

        [Description("Fires when an MDI child document has been activated.")]
        public event MDIItemActivatedEventHandler MDIItemActivated
        {
            add
            {
                base.AddHandler(MDIItemActivatedEvent, value);
            }
            remove
            {
                base.RemoveHandler(MDIItemActivatedEvent, value);
            }
        }

        [Description("Fires before an MDI child panel is activated.")]
        public event MDIItemCancelEventHandler MDIItemActivating
        {
            add
            {
                base.AddHandler(MDIItemActivatingEvent, value);
            }
            remove
            {
                base.RemoveHandler(MDIItemActivatingEvent, value);
            }
        }

        protected internal event PropertyChangedEventHandler MDIMergeStyleChanged;

        [Description("Allows you to customize menus and bars when the merging mechanism is invoked.")]
        public event BarMergeEventHandler Merge
        {
            add
            {
                base.AddHandler(MergeEvent, value);
            }
            remove
            {
                base.RemoveHandler(MergeEvent, value);
            }
        }

        internal event EventHandler NativeDraggingCompleted;

        internal event EventHandler NativeDraggingStarted;

        [Description("Allows you to provide unique names for layout items, whose names conflict with existing names.")]
        public event RequestUniqueNameEventHandler RequestUniqueName
        {
            add
            {
                base.AddHandler(RequestUniqueNameEvent, value);
            }
            remove
            {
                base.RemoveHandler(RequestUniqueNameEvent, value);
            }
        }

        [Description("Allows you to hide and disable individual dock hints.")]
        public event ShowingDockHintsEventHandler ShowingDockHints
        {
            add
            {
                base.AddHandler(ShowingDockHintsEvent, value);
            }
            remove
            {
                base.RemoveHandler(ShowingDockHintsEvent, value);
            }
        }

        [Description("Fires before showing a context menu, and allows it to be customized.")]
        public event ShowingMenuEventHandler ShowingMenu
        {
            add
            {
                base.AddHandler(ShowingMenuEvent, value);
            }
            remove
            {
                base.RemoveHandler(ShowingMenuEvent, value);
            }
        }

        [Description("Fires when the value of the DockLayoutManager.ShowInvisibleItems property is changed.")]
        public event ShowInvisibleItemsChangedEventHandler ShowInvisibleItemsChanged
        {
            add
            {
                base.AddHandler(ShowInvisibleItemsChangedEvent, value);
            }
            remove
            {
                base.RemoveHandler(ShowInvisibleItemsChangedEvent, value);
            }
        }

        [Description("Allows you to undo bars customizations performed via the DockLayoutManager.Merge event.")]
        public event BarMergeEventHandler UnMerge
        {
            add
            {
                base.AddHandler(UnMergeEvent, value);
            }
            remove
            {
                base.RemoveHandler(UnMergeEvent, value);
            }
        }

        static DockLayoutManager()
        {
            DevExpress.Xpf.Docking.DependencyPropertyRegistrator<DockLayoutManager> registrator = new DevExpress.Xpf.Docking.DependencyPropertyRegistrator<DockLayoutManager>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.OverrideUIMetadata<SerializationProvider>(DXSerializer.SerializationProviderProperty, new DXDockingSerializationProvider(), null, null);
            registrator.OverrideUIMetadata<string>(DXSerializer.SerializationIDDefaultProperty, "DockLayoutManager", null, null);
            registrator.Register<string>("OwnerWindowTitle", ref OwnerWindowTitleProperty, null, (dObj, e) => ((DockLayoutManager) dObj).OnOwnerWindowTitleChanged(), null);
            registrator.Register<bool>("ShowMaximizedDocumentCaptionInWindowTitle", ref ShowMaximizedDocumentCaptionInWindowTitleProperty, true, null, null);
            registrator.Register<string>("WindowTitleFormat", ref WindowTitleFormatProperty, null, null, (CoerceValueCallback) ((dObj, value) => ((DockLayoutManager) dObj).CoerceWindowTitleFormat((string) value)));
            registrator.Register<bool>("DisposeOnWindowClosing", ref DisposeOnWindowClosingProperty, true, null, null);
            registrator.Register<bool>("AllowFloatGroupTransparency", ref AllowFloatGroupTransparencyProperty, true, (d, e) => ((DockLayoutManager) d).InvokeUpdateFloatingPaneResources(), null);
            registrator.Register<bool>("EnableWin32Compatibility", ref EnableWin32CompatibilityProperty, false, (d, e) => ((DockLayoutManager) d).OnEnableWin32CompatibilityChanged((bool) e.OldValue, (bool) e.NewValue), null);
            registrator.Register<bool>("ShowFloatWindowsInTaskbar", ref ShowFloatWindowsInTaskbarProperty, true, null, null);
            registrator.Register<bool>("OwnsFloatWindows", ref OwnsFloatWindowsProperty, true, (d, e) => ((DockLayoutManager) d).OnOwnsFloatWindowsChanged((bool) e.OldValue, (bool) e.NewValue), null);
            registrator.Register<bool>("AllowAeroSnap", ref AllowAeroSnapProperty, true, null, null);
            registrator.Register<bool>("CloseFloatWindowsOnManagerUnloading", ref CloseFloatWindowsOnManagerUnloadingProperty, false, null, null);
            registrator.RegisterDirectEvent<RequestUniqueNameEventHandler>("RequestUniqueName", ref RequestUniqueNameEvent);
            registrator.RegisterDirectEvent<ShowingMenuEventHandler>("ShowingMenu", ref ShowingMenuEvent);
            registrator.RegisterDirectEvent<DockItemCancelEventHandler>("DockItemActivating", ref DockItemActivatingEvent);
            registrator.RegisterDirectEvent<LayoutItemCancelEventHandler>("LayoutItemActivating", ref LayoutItemActivatingEvent);
            registrator.RegisterDirectEvent<MDIItemCancelEventHandler>("MDIItemActivating", ref MDIItemActivatingEvent);
            registrator.RegisterDirectEvent<DockItemActivatedEventHandler>("DockItemActivated", ref DockItemActivatedEvent);
            registrator.RegisterDirectEvent<LayoutItemActivatedEventHandler>("LayoutItemActivated", ref LayoutItemActivatedEvent);
            registrator.RegisterDirectEvent<MDIItemActivatedEventHandler>("MDIItemActivated", ref MDIItemActivatedEvent);
            registrator.RegisterDirectEvent<DockItemCancelEventHandler>("DockItemClosing", ref DockItemClosingEvent);
            registrator.RegisterDirectEvent<DockItemCancelEventHandler>("DockItemHiding", ref DockItemHidingEvent);
            registrator.RegisterDirectEvent<DockItemCancelEventHandler>("DockItemRestoring", ref DockItemRestoringEvent);
            registrator.RegisterDirectEvent<DockItemClosedEventHandler>("DockItemClosed", ref DockItemClosedEvent);
            registrator.RegisterDirectEvent<DockItemEventHandler>("DockItemHidden", ref DockItemHiddenEvent);
            registrator.RegisterDirectEvent<DockItemEventHandler>("DockItemRestored", ref DockItemRestoredEvent);
            registrator.RegisterDirectEvent<DockItemCancelEventHandler>("DockItemStartDocking", ref DockItemStartDockingEvent);
            registrator.RegisterDirectEvent<DockItemDockingEventHandler>("DockItemDocking", ref DockItemDockingEvent);
            registrator.RegisterDirectEvent<DockItemDockingEventHandler>("DockItemEndDocking", ref DockItemEndDockingEvent);
            registrator.RegisterDirectEvent<LayoutItemSelectionChangingEventHandler>("LayoutItemSelectionChanging", ref LayoutItemSelectionChangingEvent);
            registrator.RegisterDirectEvent<LayoutItemSelectionChangedEventHandler>("LayoutItemSelectionChanged", ref LayoutItemSelectionChangedEvent);
            registrator.RegisterDirectEvent<LayoutItemSizeChangedEventHandler>("LayoutItemSizeChanged", ref LayoutItemSizeChangedEvent);
            registrator.RegisterDirectEvent<LayoutItemHiddenEventHandler>("LayoutItemHidden", ref LayoutItemHiddenEvent);
            registrator.RegisterDirectEvent<LayoutItemRestoredEventHandler>("LayoutItemRestored", ref LayoutItemRestoredEvent);
            registrator.RegisterDirectEvent<LayoutItemMovedEventHandler>("LayoutItemMoved", ref LayoutItemMovedEvent);
            registrator.RegisterDirectEvent<LayoutItemCancelEventHandler>("LayoutItemStartRenaming", ref LayoutItemStartRenamingEvent);
            registrator.RegisterDirectEvent<LayoutItemEndRenamingEventHandler>("LayoutItemEndRenaming", ref LayoutItemEndRenamingEvent);
            registrator.RegisterDirectEvent<IsCustomizationChangedEventHandler>("IsCustomizationChanged", ref IsCustomizationChangedEvent);
            registrator.RegisterDirectEvent<CustomizationFormVisibleChangedEventHandler>("CustomizationFormVisibleChanged", ref CustomizationFormVisibleChangedEvent);
            registrator.RegisterDirectEvent<ShowInvisibleItemsChangedEventHandler>("ShowInvisibleItemsChanged", ref ShowInvisibleItemsChangedEvent);
            registrator.RegisterDirectEvent<ItemIsVisibleChangedEventHandler>("ItemIsVisibleChanged", ref ItemIsVisibleChangedEvent);
            registrator.RegisterDirectEvent<DockItemDraggingEventHandler>("DockItemDragging", ref DockItemDraggingEvent);
            registrator.RegisterDirectEvent<DockItemExpandedEventHandler>("DockItemExpanded", ref DockItemExpandedEvent);
            registrator.RegisterDirectEvent<DockItemCollapsedEventHandler>("DockItemCollapsed", ref DockItemCollapsedEvent);
            registrator.RegisterDirectEvent<ShowingDockHintsEventHandler>("ShowingDockHints", ref ShowingDockHintsEvent);
            registrator.RegisterDirectEvent<BarMergeEventHandler>("Merge", ref MergeEvent);
            registrator.RegisterDirectEvent<BarMergeEventHandler>("UnMerge", ref UnMergeEvent);
            registrator.RegisterDirectEvent<BeforeItemAddedEventHandler>("BeforeItemAdded", ref BeforeItemAddedEvent);
            registrator.RegisterDirectEvent<DockOperationStartingEventHandler>("DockOperationStarting", ref DockOperationStartingEvent);
            registrator.RegisterDirectEvent<DockOperationCompletedEventHandler>("DockOperationCompleted", ref DockOperationCompletedEvent);
            registrator.RegisterAttachedInherited<DockLayoutManager>("DockLayoutManager", ref DockLayoutManagerProperty, null, new PropertyChangedCallback(DockLayoutManager.OnDockLayoutManagerChanged), null);
            registrator.RegisterAttachedInherited<BaseLayoutItem>("LayoutItem", ref LayoutItemProperty, null, new PropertyChangedCallback(DockLayoutManager.OnLayoutItemChanged), new CoerceValueCallback(DockLayoutManager.CoerceLayoutItem));
            registrator.RegisterAttached<IUIElement>("UIScope", ref UIScopeProperty, null, new PropertyChangedCallback(DockLayoutManager.OnUIScopeChanged), null);
            registrator.Register<LayoutGroup>("LayoutRoot", ref LayoutRootProperty, null, new PropertyChangedCallback(DockLayoutManager.OnLayoutRootChanged), null);
            registrator.Register<DevExpress.Xpf.Docking.FloatingMode>("FloatingMode", ref FloatingModeProperty, DevExpress.Xpf.Docking.FloatingMode.Window, null, null);
            registrator.Register<DevExpress.Xpf.Docking.Base.ClosedPanelsBarVisibility>("ClosedPanelsBarVisibility", ref ClosedPanelsBarVisibilityProperty, DevExpress.Xpf.Docking.Base.ClosedPanelsBarVisibility.Default, (dObj, e) => ((DockLayoutManager) dObj).OnClosedItemsBarVisibilityChanged((DevExpress.Xpf.Docking.Base.ClosedPanelsBarVisibility) e.NewValue), null);
            registrator.Register<DevExpress.Xpf.Docking.Base.AutoHideExpandMode>("AutoHideExpandMode", ref AutoHideExpandModeProperty, DevExpress.Xpf.Docking.Base.AutoHideExpandMode.Default, null, null);
            registrator.RegisterAttachedInherited<bool>("AllowMergingAutoHidePanels", ref AllowMergingAutoHidePanelsProperty, false, new PropertyChangedCallback(DockLayoutManager.OnAllowMergingAutoHidePanelsChanged), null);
            registrator.Register<DevExpress.Xpf.Docking.Base.AutoHideMode>("AutoHideMode", ref AutoHideModeProperty, DevExpress.Xpf.Docking.Base.AutoHideMode.Default, (d, e) => ((DockLayoutManager) d).OnAutoHideModeChanged((DevExpress.Xpf.Docking.Base.AutoHideMode) e.OldValue, (DevExpress.Xpf.Docking.Base.AutoHideMode) e.NewValue), null);
            registrator.Register<BaseLayoutItem>("ActiveDockItem", ref ActiveDockItemProperty, null, (dObj, e) => ((DockLayoutManager) dObj).OnActiveDockItemChanged((BaseLayoutItem) e.OldValue, (BaseLayoutItem) e.NewValue), null);
            registrator.Register<BaseLayoutItem>("ActiveLayoutItem", ref ActiveLayoutItemProperty, null, (dObj, e) => ((DockLayoutManager) dObj).OnActiveLayoutItemChanged((BaseLayoutItem) e.OldValue, (BaseLayoutItem) e.NewValue), null);
            registrator.Register<BaseLayoutItem>("ActiveMDIItem", ref ActiveMDIItemProperty, null, (dObj, e) => ((DockLayoutManager) dObj).OnActiveMDIItemChanged((BaseLayoutItem) e.OldValue, (BaseLayoutItem) e.NewValue), null);
            registrator.Register<bool>("AllowCustomization", ref AllowCustomizationProperty, true, (dObj, e) => ((DockLayoutManager) dObj).OnAllowCustomizationChanged((bool) e.NewValue), null);
            registrator.Register<bool>("AllowDocumentSelector", ref AllowDocumentSelectorProperty, true, (dObj, e) => ((DockLayoutManager) dObj).OnAllowDocumentSelectorChanged((bool) e.NewValue), null);
            registrator.RegisterReadonly<bool>("IsCustomization", ref IsCustomizationPropertyKey, ref IsCustomizationProperty, false, (dObj, e) => ((DockLayoutManager) dObj).OnIsCustomizationChanged((bool) e.NewValue), (dObj, value) => ((DockLayoutManager) dObj).CoerceIsCustomization((bool) value));
            registrator.RegisterReadonly<bool>("IsRenaming", ref IsRenamingPropertyKey, ref IsRenamingProperty, false, null, (dObj, value) => ((DockLayoutManager) dObj).CoerceIsRenaming((bool) value));
            bool? defValue = null;
            registrator.Register<bool?>("AllowDockItemRename", ref AllowDockItemRenameProperty, defValue, null, null);
            defValue = null;
            registrator.Register<bool?>("AllowLayoutItemRename", ref AllowLayoutItemRenameProperty, defValue, null, null);
            defValue = null;
            registrator.Register<bool?>("ShowInvisibleItems", ref ShowInvisibleItemsProperty, defValue, (dObj, e) => ((DockLayoutManager) dObj).OnShowInvisibleItemsChanged((bool?) e.NewValue), null);
            registrator.Register<bool>("ShowInvisibleItemsInCustomizationForm", ref ShowInvisibleItemsInCustomizationFormProperty, true, (dObj, e) => ((DockLayoutManager) dObj).OnShowInvisibleItemsInCustomizationFormChanged((bool) e.NewValue), null);
            registrator.Register<bool>("DestroyLastDocumentGroup", ref DestroyLastDocumentGroupProperty, false, null, null);
            registrator.Register<Dock>("ClosedPanelsBarPosition", ref ClosedPanelsBarPositionProperty, Dock.Top, null, null);
            registrator.Register<DevExpress.Xpf.Bars.MDIMergeStyle>("MDIMergeStyle", ref MDIMergeStyleProperty, DevExpress.Xpf.Bars.MDIMergeStyle.Default, (dObj, e) => ((DockLayoutManager) dObj).OnMDIMergeStyleChanged((DevExpress.Xpf.Bars.MDIMergeStyle) e.OldValue, (DevExpress.Xpf.Bars.MDIMergeStyle) e.NewValue), null);
            registrator.Register<DevExpress.Xpf.Docking.DockingStyle>("DockingStyle", ref DockingStyleProperty, DevExpress.Xpf.Docking.DockingStyle.Default, null, null);
            registrator.Register<bool>("RedrawContentWhenResizing", ref RedrawContentWhenResizingProperty, true, null, null);
            registrator.Register<DevExpress.Xpf.Docking.Base.FloatingDocumentContainer>("FloatingDocumentContainer", ref FloatingDocumentContainerProperty, DevExpress.Xpf.Docking.Base.FloatingDocumentContainer.Default, null, null);
            registrator.Register<IEnumerable>("ItemsSource", ref ItemsSourceProperty, null, (dObj, e) => ((DockLayoutManager) dObj).OnItemsSourceChanged((IEnumerable) e.OldValue, (IEnumerable) e.NewValue, e), null);
            registrator.Register<DataTemplate>("ItemTemplate", ref ItemTemplateProperty, null, null, null);
            registrator.Register<DataTemplateSelector>("ItemTemplateSelector", ref ItemTemplateSelectorProperty, null, null, null);
            registrator.Register<bool>("IsSynchronizedWithCurrentItem", ref IsSynchronizedWithCurrentItemProperty, false, (dObj, e) => ((DockLayoutManager) dObj).OnIsSynchronizedWithCurrentItemChanged((bool) e.OldValue, (bool) e.NewValue), null);
            registrator.Register<ImageSource>("DefaultTabPageCaptionImage", ref DefaultTabPageCaptionImageProperty, DevExpress.Xpf.Docking.Images.ImageHelper.GetImage("DefaultTabPageCaption"), null, null);
            registrator.Register<ImageSource>("DefaultAutoHidePanelCaptionImage", ref DefaultAutoHidePanelCaptionImageProperty, DevExpress.Xpf.Docking.Images.ImageHelper.GetImage("DefaultAutoHidePanelCaption"), null, null);
            registrator.Register<TimeSpan>("AutoHideGroupsCheckInterval", ref AutoHideGroupsCheckIntervalProperty, new TimeSpan(0L), null, null);
            registrator.RegisterClassCommandBinding(DockControllerCommand.Activate, new ExecutedRoutedEventHandler(DockControllerCommand.Executed), new CanExecuteRoutedEventHandler(DockControllerCommand.CanExecute));
            registrator.RegisterClassCommandBinding(DockControllerCommand.Close, new ExecutedRoutedEventHandler(DockControllerCommand.Executed), new CanExecuteRoutedEventHandler(DockControllerCommand.CanExecute));
            registrator.RegisterClassCommandBinding(DockControllerCommand.CloseActive, new ExecutedRoutedEventHandler(DockControllerCommand.Executed), new CanExecuteRoutedEventHandler(DockControllerCommand.CanExecute));
            registrator.RegisterClassCommandBinding(DockControllerCommand.Dock, new ExecutedRoutedEventHandler(DockControllerCommand.Executed), new CanExecuteRoutedEventHandler(DockControllerCommand.CanExecute));
            registrator.RegisterClassCommandBinding(DockControllerCommand.Float, new ExecutedRoutedEventHandler(DockControllerCommand.Executed), new CanExecuteRoutedEventHandler(DockControllerCommand.CanExecute));
            registrator.RegisterClassCommandBinding(DockControllerCommand.Hide, new ExecutedRoutedEventHandler(DockControllerCommand.Executed), new CanExecuteRoutedEventHandler(DockControllerCommand.CanExecute));
            registrator.RegisterClassCommandBinding(DockControllerCommand.Restore, new ExecutedRoutedEventHandler(DockControllerCommand.Executed), new CanExecuteRoutedEventHandler(DockControllerCommand.CanExecute));
            registrator.RegisterClassCommandBinding(SerializationControllerCommand.SaveLayout, new ExecutedRoutedEventHandler(SerializationControllerCommand.Executed), new CanExecuteRoutedEventHandler(SerializationControllerCommand.CanExecute));
            registrator.RegisterClassCommandBinding(SerializationControllerCommand.RestoreLayout, new ExecutedRoutedEventHandler(SerializationControllerCommand.Executed), new CanExecuteRoutedEventHandler(SerializationControllerCommand.CanExecute));
            registrator.RegisterClassCommandBinding(CustomizationControllerCommand.ShowClosedItems, new ExecutedRoutedEventHandler(CustomizationControllerCommand.Executed), new CanExecuteRoutedEventHandler(CustomizationControllerCommand.CanExecute));
            registrator.RegisterClassCommandBinding(CustomizationControllerCommand.HideClosedItems, new ExecutedRoutedEventHandler(CustomizationControllerCommand.Executed), new CanExecuteRoutedEventHandler(CustomizationControllerCommand.CanExecute));
            registrator.RegisterClassCommandBinding(LayoutControllerCommand.ShowCaption, new ExecutedRoutedEventHandler(LayoutControllerCommand.Executed), new CanExecuteRoutedEventHandler(LayoutControllerCommand.CanExecute));
            registrator.RegisterClassCommandBinding(LayoutControllerCommand.ShowControl, new ExecutedRoutedEventHandler(LayoutControllerCommand.Executed), new CanExecuteRoutedEventHandler(LayoutControllerCommand.CanExecute));
            registrator.RegisterClassCommandBinding(LayoutControllerCommand.ShowCaptionImageBeforeText, new ExecutedRoutedEventHandler(LayoutControllerCommand.Executed), new CanExecuteRoutedEventHandler(LayoutControllerCommand.CanExecute));
            registrator.RegisterClassCommandBinding(LayoutControllerCommand.ShowCaptionImageAfterText, new ExecutedRoutedEventHandler(LayoutControllerCommand.Executed), new CanExecuteRoutedEventHandler(LayoutControllerCommand.CanExecute));
            registrator.RegisterClassCommandBinding(LayoutControllerCommand.ShowCaptionOnLeft, new ExecutedRoutedEventHandler(LayoutControllerCommand.Executed), new CanExecuteRoutedEventHandler(LayoutControllerCommand.CanExecute));
            registrator.RegisterClassCommandBinding(LayoutControllerCommand.ShowCaptionOnRight, new ExecutedRoutedEventHandler(LayoutControllerCommand.Executed), new CanExecuteRoutedEventHandler(LayoutControllerCommand.CanExecute));
            registrator.RegisterClassCommandBinding(LayoutControllerCommand.ShowCaptionAtTop, new ExecutedRoutedEventHandler(LayoutControllerCommand.Executed), new CanExecuteRoutedEventHandler(LayoutControllerCommand.CanExecute));
            registrator.RegisterClassCommandBinding(LayoutControllerCommand.ShowCaptionAtBottom, new ExecutedRoutedEventHandler(LayoutControllerCommand.Executed), new CanExecuteRoutedEventHandler(LayoutControllerCommand.CanExecute));
            registrator.RegisterClassCommandBinding(MDIControllerCommand.Minimize, new ExecutedRoutedEventHandler(MDIControllerCommand.Executed), new CanExecuteRoutedEventHandler(MDIControllerCommand.CanExecute));
            registrator.RegisterClassCommandBinding(MDIControllerCommand.Maximize, new ExecutedRoutedEventHandler(MDIControllerCommand.Executed), new CanExecuteRoutedEventHandler(MDIControllerCommand.CanExecute));
            registrator.RegisterClassCommandBinding(MDIControllerCommand.Restore, new ExecutedRoutedEventHandler(MDIControllerCommand.Executed), new CanExecuteRoutedEventHandler(MDIControllerCommand.CanExecute));
            registrator.RegisterClassCommandBinding(MDIControllerCommand.Cascade, new ExecutedRoutedEventHandler(MDIControllerCommand.Executed), new CanExecuteRoutedEventHandler(MDIControllerCommand.CanExecute));
            registrator.RegisterClassCommandBinding(MDIControllerCommand.TileHorizontal, new ExecutedRoutedEventHandler(MDIControllerCommand.Executed), new CanExecuteRoutedEventHandler(MDIControllerCommand.CanExecute));
            registrator.RegisterClassCommandBinding(MDIControllerCommand.TileVertical, new ExecutedRoutedEventHandler(MDIControllerCommand.Executed), new CanExecuteRoutedEventHandler(MDIControllerCommand.CanExecute));
            registrator.RegisterClassCommandBinding(MDIControllerCommand.ArrangeIcons, new ExecutedRoutedEventHandler(MDIControllerCommand.Executed), new CanExecuteRoutedEventHandler(MDIControllerCommand.CanExecute));
            registrator.RegisterClassCommandBinding(MDIControllerCommand.ChangeMDIStyle, new ExecutedRoutedEventHandler(MDIControllerCommand.Executed), new CanExecuteRoutedEventHandler(MDIControllerCommand.CanExecute));
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(DockLayoutManager), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<DockLayoutManager> registrator1 = DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<DockLayoutManager>.New().Register<DevExpress.Xpf.Docking.LogicalTreeStructure>(System.Linq.Expressions.Expression.Lambda<Func<DockLayoutManager, DevExpress.Xpf.Docking.LogicalTreeStructure>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DockLayoutManager.get_LogicalTreeStructure)), parameters), out LogicalTreeStructureProperty, DevExpress.Xpf.Docking.LogicalTreeStructure.Default, (d, oldValue, newValue) => d.OnLogicalTreeStructureChanged(oldValue, newValue), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DockLayoutManager), "d");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<DockLayoutManager> registrator2 = registrator1.Register<DockingViewStyle>(System.Linq.Expressions.Expression.Lambda<Func<DockLayoutManager, DockingViewStyle>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DockLayoutManager.get_ViewStyle)), expressionArray2), out ViewStyleProperty, DockingViewStyle.Default, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DockLayoutManager), "d");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<DockLayoutManager> registrator3 = registrator2.Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<DockLayoutManager, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DockLayoutManager.get_ShowContentWhenDragging)), expressionArray3), out ShowContentWhenDraggingProperty, true, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DockLayoutManager), "d");
            ParameterExpression[] expressionArray4 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<DockLayoutManager> registrator4 = registrator3.Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<DockLayoutManager, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DockLayoutManager.get_HandleHwndHostMouseEvents)), expressionArray4), out HandleHwndHostMouseEventsProperty, false, (d, oldValue, newValue) => d.OnHandleHwndHostMouseEventsChanged(oldValue), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DockLayoutManager), "d");
            ParameterExpression[] expressionArray5 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            registrator4.RegisterReadOnly<bool>(System.Linq.Expressions.Expression.Lambda<Func<DockLayoutManager, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DockLayoutManager.get_IsDarkTheme)), expressionArray5), out IsDarkThemePropertyKey, out IsDarkThemeProperty, false, frameworkOptions);
        }

        public DockLayoutManager()
        {
            PropertyChangedEventHandler handler1 = <>c.<>9__182_0;
            if (<>c.<>9__182_0 == null)
            {
                PropertyChangedEventHandler local1 = <>c.<>9__182_0;
                handler1 = <>c.<>9__182_0 = delegate (object <sender>, PropertyChangedEventArgs <e>) {
                };
            }
            this.AutoHideDisplayModeChanged = handler1;
            PropertyChangedEventHandler handler2 = <>c.<>9__182_1;
            if (<>c.<>9__182_1 == null)
            {
                PropertyChangedEventHandler local2 = <>c.<>9__182_1;
                handler2 = <>c.<>9__182_1 = delegate (object <sender>, PropertyChangedEventArgs <e>) {
                };
            }
            this.MDIMergeStyleChanged = handler2;
            this.<CanShowFloatGroup>k__BackingField = true;
            this.uiChildren = new UIChildren();
            this.logicalChildrenCore = new LogicalChildrenCollection();
            this.internalElements = new LogicalChildrenCollection();
            this.CheckLicense();
            if (DockLayoutManagerParameters.LogicalTreeStructure != null)
            {
                base.SetCurrentValue(LogicalTreeStructureProperty, DockLayoutManagerParameters.LogicalTreeStructure.Value);
            }
            this.layoutGroupGenerator = new LayoutGroupGenerator(this);
            base.CoerceValue(WindowTitleFormatProperty);
            this.ShouldRestoreOnActivate = false;
            this.DockHintsContainer = new LogicalContainer<UIElement>();
            this.DragAdorner = this.CreateDragAdorner();
            this.floatGroupsCore = this.CreateFloatGroupsCollection();
            this.floatGroupsCore.Owner = this;
            this.floatGroupsCore.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnFloatGroupsCollectionChanged);
            this.autoHideGroupsCore = this.CreateAutoHideGroupsCollection();
            this.autoHideGroupsCore.Owner = this;
            this.autoHideGroupsCore.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnAutoHideGroupsCollectionChanged);
            this.closedPanelsCore = this.CreateClosedPanelsCollection();
            this.dockControllerCore = this.CreateDockController();
            this.layoutControllerCore = this.CreateLayoutController();
            this.mdiControllerCore = this.CreateMDIController();
            this.customizationControllerCore = this.CreateCustomizationController();
            this.serializationControllerCore = this.CreateSerializationController();
            this.renameHelperCore = this.CreateRenameHelper();
            this.viewAdapterCore = this.CreateViewAdapter();
            this.loadedActionsHelper = new DelayedActionsHelper();
            this.initilizedActionsHelper = new DelayedActionsHelper();
            this.layoutItemStateHelper = new LayoutItemStateHelper(this);
            this.containerGenerator = new ContainerGenerator();
            base.IsVisibleChanged += new DependencyPropertyChangedEventHandler(this.OnIsVisibleChanged);
            base.IsEnabledChanged += new DependencyPropertyChangedEventHandler(this.OnIsEnabledChanged);
            this.RegisterInputBindings();
            this.itemsInternal = new DockLayoutManagerItemsCollection(this);
            this.decomposedItems = new DecomposedItemsCollection(this);
            this.visualChildrenCore = new VisualCollection(this);
            Ensure(this, false);
            ThemeChangedEventManager.AddListener(this, this);
            BarNameScope.SetIsScopeOwner(this, true);
            MergingProperties.SetElementMergingBehavior(this, ElementMergingBehavior.InternalWithExternal);
            this.MinimizedItems = new ObservableCollection<object>();
            this.ValuesProvider = new DockLayoutManagerThemeDependentValuesProvider(this);
        }

        public void Activate(BaseLayoutItem item)
        {
            this.Activate(item, true, false);
        }

        internal void Activate(BaseLayoutItem item, bool focus, bool delayed)
        {
            this.DelayedActivationHelper.Activate(item, focus, delayed);
        }

        protected internal LayoutGroup ActivateCore(BaseLayoutItem itemToActivate) => 
            this.ActivateCore(itemToActivate, true);

        protected internal LayoutGroup ActivateCore(BaseLayoutItem itemToActivate, bool focus)
        {
            if (itemToActivate == null)
            {
                return null;
            }
            this.LockItemActivationOnFocus();
            LayoutGroup root = itemToActivate.GetRoot();
            if (!(LayoutItemsHelper.IsLayoutItem(itemToActivate) || (itemToActivate.ItemType == LayoutItemType.Group)) || ((root == null) || !root.IsLayoutRoot))
            {
                if (!(itemToActivate is AutoHideGroup))
                {
                    DocumentPanel document = GetDocument(itemToActivate);
                    if ((document == null) || this.TryActivate(this.MDIController, document, false))
                    {
                        this.DockController.Activate(itemToActivate, focus);
                    }
                }
            }
            else
            {
                if (root.ParentPanel != null)
                {
                    DocumentPanel document = GetDocument(root.ParentPanel);
                    if (((document == null) || this.TryActivate(this.MDIController, document, false)) && !this.TryActivate(this.DockController, root.ParentPanel, false))
                    {
                        return root;
                    }
                }
                this.LayoutController.Activate(itemToActivate, focus);
            }
            return root;
        }

        protected virtual void AddDelayedAction(Action action)
        {
            this.AddDelayedAction(action, DelayedActionPriority.Default);
        }

        private void AddDelayedAction(Action action, DelayedActionPriority priority)
        {
            if (base.IsLoaded && ScreenHelper.IsAttachedToPresentationSource(this))
            {
                action();
            }
            else
            {
                this.loadedActionsHelper.AddDelayedAction(action, priority);
            }
        }

        protected void AddLogicalChild(object child)
        {
            if (LogicalTreeHelper.GetParent(child as DependencyObject) == null)
            {
                BaseLayoutItem item = child as BaseLayoutItem;
                if (item != null)
                {
                    this.logicalChildrenCore.AddOnce(item);
                }
                else
                {
                    this.internalElements.AddOnce(child);
                }
                base.AddLogicalChild(child);
            }
        }

        internal static void AddLogicalChild(DockLayoutManager container, DependencyObject child)
        {
            if (((child != null) && (container != null)) && (LogicalTreeHelper.GetParent(child) == null))
            {
                container.AddLogicalChild(child);
            }
        }

        private void AddToPending(object item, string targetName)
        {
            if (!string.IsNullOrEmpty(targetName))
            {
                WeakList<object> list;
                if (!this.pendingItems.TryGetValue(targetName, out list))
                {
                    this.pendingItems[targetName] = list = new WeakList<object>();
                }
                if (!list.Contains(item))
                {
                    list.Add(item);
                }
            }
        }

        internal static void AddToVisualTree(DockLayoutManager container, FrameworkElement child)
        {
            if (container != null)
            {
                container.AddVisualChild(child);
            }
        }

        private void AddValuesProvider()
        {
            if (!ReferenceEquals(this.ValuesProvider.Parent, this))
            {
                this.AddLogicalChild(this.ValuesProvider);
                this.AddVisualChild(this.ValuesProvider);
            }
        }

        protected virtual void AddVisualChild(FrameworkElement child)
        {
            if ((child.Parent == null) && !this.visualChildrenCore.Contains(child))
            {
                this.visualChildrenCore.Add(child);
            }
        }

        public void BeginCustomization()
        {
            this.CustomizationController.BeginCustomization();
        }

        protected internal void BeginFloating()
        {
            this.floatingCounter++;
        }

        protected virtual void BeginUpdate()
        {
        }

        public bool BringToFront(BaseLayoutItem item)
        {
            FloatGroup root = item.GetRoot() as FloatGroup;
            if ((root == null) || !root.IsOpen)
            {
                return false;
            }
            if (root.UIElements.Count == 0)
            {
                base.UpdateLayout();
            }
            IView view = this.GetView(root.UIElements.GetElement<FloatPanePresenter>());
            if (view != null)
            {
                this.ViewAdapter.UIInteractionService.Activate(view);
            }
            return (view != null);
        }

        private Point CalcRestoreOffset() => 
            this.CalcRestoreOffset(RestoreLayoutOptions.GetFloatPanelsRestoreOffset(this));

        private Point CalcRestoreOffset(Point savedOffset)
        {
            Point point = new Point(0.0, 0.0);
            if (this.IsDesktopFloatingMode && (!double.IsNaN(savedOffset.X) && !double.IsNaN(savedOffset.Y)))
            {
                Point restoreOffsetCore = this.GetRestoreOffsetCore();
                point = new Point(savedOffset.X - restoreOffsetCore.X, savedOffset.Y - restoreOffsetCore.Y);
            }
            return point;
        }

        private bool CanGetRestoreOffset() => 
            !base.IsDisposing && (ScreenHelper.IsAttachedToPresentationSource(this) && (((this.OwnerWindow == null) || (this.OwnerWindow.WindowState != WindowState.Minimized)) && base.IsMeasureValid));

        private bool CanProcessKey(DependencyObject eventProcessor) => 
            (this.ViewAdapter.DragService.OperationType == DevExpress.Xpf.Layout.Core.OperationType.Regular) && (base.IsVisible && (ReferenceEquals(eventProcessor, this) || ((eventProcessor == null) && this.IsInVisualTree())));

        private bool CanShowDocumentSelector() => 
            this.AllowDocumentSelector && !this.CustomizationController.IsDocumentSelectorVisible;

        internal void CheckAutoHiddenState(BaseLayoutItem item)
        {
            this.CheckLayoutItemState(item, LayoutItemState.AutoHide);
        }

        internal bool CheckAutoHideExpandState(LayoutPanel panel)
        {
            if ((panel == null) || (!panel.IsAutoHidden || base.IsDisposing))
            {
                return false;
            }
            AutoHideTray tray = panel.Parent.GetRootUIScope() as AutoHideTray;
            if ((tray == null) || !tray.HasItems)
            {
                return false;
            }
            switch (panel.AutoHideExpandState)
            {
                case AutoHideExpandState.Hidden:
                    if (ReferenceEquals(tray.HotItem, panel))
                    {
                        tray.DoCollapseIfPossible(true);
                    }
                    break;

                case AutoHideExpandState.Visible:
                    tray.DoRestore(panel);
                    break;

                case AutoHideExpandState.Expanded:
                    base.Dispatcher.BeginInvoke(delegate {
                        tray.DoMaximize(panel);
                    }, DispatcherPriority.Render, new object[0]);
                    break;

                default:
                    break;
            }
            return true;
        }

        internal void CheckClosedState(BaseLayoutItem item)
        {
            this.CheckLayoutItemState(item, LayoutItemState.Close);
        }

        private void CheckCustomizationRoot(BaseLayoutItem item)
        {
            LayoutGroup customizationRoot = this.CustomizationController.CustomizationRoot;
            if (!ReferenceEquals(item.GetRoot(), customizationRoot))
            {
                this.CustomizationController.CustomizationRoot = ((customizationRoot == null) || customizationRoot.IsInTree()) ? customizationRoot : this.LayoutRoot;
            }
        }

        private void CheckFloatGroupRestoreBounds()
        {
            Point offset = this.CalcRestoreOffset();
            foreach (FloatGroup group in this.FloatGroups)
            {
                Func<FloatGroup, FloatingWindowLock> evaluator = <>c.<>9__655_0;
                if (<>c.<>9__655_0 == null)
                {
                    Func<FloatGroup, FloatingWindowLock> local1 = <>c.<>9__655_0;
                    evaluator = <>c.<>9__655_0 = x => x.FloatingWindowLock;
                }
                Action<FloatingWindowLock> action = <>c.<>9__655_1;
                if (<>c.<>9__655_1 == null)
                {
                    Action<FloatingWindowLock> local2 = <>c.<>9__655_1;
                    action = <>c.<>9__655_1 = x => x.Lock(FloatingWindowLock.LockerKey.CheckFloatBounds);
                }
                group.With<FloatGroup, FloatingWindowLock>(evaluator).Do<FloatingWindowLock>(action);
                try
                {
                    Point point2 = this.CheckFloatGroupRestoreBounds(group.FloatBounds, offset);
                    if (group.FloatLocation == point2)
                    {
                        group.FloatLocation = new Point(group.FloatLocation.X + 1.0, group.FloatLocation.Y);
                    }
                    group.FloatLocation = point2;
                }
                finally
                {
                    Func<FloatGroup, FloatingWindowLock> func1 = <>c.<>9__655_2;
                    if (<>c.<>9__655_2 == null)
                    {
                        Func<FloatGroup, FloatingWindowLock> local3 = <>c.<>9__655_2;
                        func1 = <>c.<>9__655_2 = x => x.FloatingWindowLock;
                    }
                    Action<FloatingWindowLock> action1 = <>c.<>9__655_3;
                    if (<>c.<>9__655_3 == null)
                    {
                        Action<FloatingWindowLock> local4 = <>c.<>9__655_3;
                        action1 = <>c.<>9__655_3 = x => x.Unlock(FloatingWindowLock.LockerKey.CheckFloatBounds);
                    }
                    group.With<FloatGroup, FloatingWindowLock>(func1).Do<FloatingWindowLock>(action1);
                }
                if (group.FloatState != FloatState.Normal)
                {
                    Rect restoreBounds = DocumentPanel.GetRestoreBounds(group);
                    DocumentPanel.SetRestoreBounds(group, new Rect(this.CheckFloatGroupRestoreBounds(restoreBounds, offset), restoreBounds.Size()));
                }
                group.FloatStateLockHelper.Unlock();
            }
        }

        internal void CheckFloatGroupRestoreBounds(FloatGroup floatGroup, Point savedOffset)
        {
            Point offset = this.CalcRestoreOffset(savedOffset);
            floatGroup.EnsureFloatLocation(this.CheckFloatGroupRestoreBounds(floatGroup.FloatBounds, offset));
        }

        private Point CheckFloatGroupRestoreBounds(Rect restorebounds, Point offset)
        {
            Point point = restorebounds.Location();
            if (!WindowHelper.IsXBAP)
            {
                Rect screenBounds = restorebounds;
                int num = (base.FlowDirection == FlowDirection.RightToLeft) ? -1 : 1;
                screenBounds = new Rect(screenBounds.X + (num * offset.X), screenBounds.Y + offset.Y, screenBounds.Width, screenBounds.Height);
                screenBounds = WindowHelper.CheckScreenBounds(this, screenBounds);
                point = new Point(screenBounds.Left, screenBounds.Top);
            }
            return point;
        }

        private void CheckFloatGroupsFloatState()
        {
            Action<FloatGroup> action = <>c.<>9__657_0;
            if (<>c.<>9__657_0 == null)
            {
                Action<FloatGroup> local1 = <>c.<>9__657_0;
                action = <>c.<>9__657_0 = x => x.CheckFloatState();
            }
            this.FloatGroups.ForEach<FloatGroup>(action);
        }

        private void CheckLayoutItemState(BaseLayoutItem item, LayoutItemState state)
        {
            if (!base.IsDisposing && (item != null))
            {
                this.layoutItemStateHelper.QueueCheckLayoutItemState(item, state);
            }
        }

        protected virtual void CheckLicense()
        {
            About.CheckLicenseShowNagScreen(typeof(DockLayoutManager));
        }

        internal void CheckMergingTarget(DocumentPanel documentPanel)
        {
            bool canMerge = documentPanel.CanMerge;
            if (Equals(this.MergingTarget, documentPanel))
            {
                if (!canMerge)
                {
                    this.MergingTarget = null;
                }
            }
            else
            {
                if (canMerge && ((this.MergingTarget == null) || documentPanel.IsActive))
                {
                    this.MergingTarget = documentPanel;
                }
                if (this.MergingTarget == null)
                {
                    DocumentPanel panel = (this.ActiveDockItem ?? this.ActiveMDIItem) as DocumentPanel;
                    if ((panel != null) && panel.CanMerge)
                    {
                        this.MergingTarget = panel;
                    }
                }
            }
        }

        protected virtual void ClearContainerForItem(object item)
        {
            this.containerGenerator.ClearContainerForItem(item);
        }

        private void ClearItemCore(object obj)
        {
            this.ClearContainerForItem(obj);
            (obj as INotifyPropertyChanged).Do<INotifyPropertyChanged>(delegate (INotifyPropertyChanged x) {
                x.PropertyChanged -= new PropertyChangedEventHandler(this.OnItemTargetNameChanged);
            });
        }

        private static void ClearItemsControlPanel(UIElement uiElement)
        {
            BaseHeadersPanel panel = uiElement as BaseHeadersPanel;
            if (panel != null)
            {
                UIElement[] array = new UIElement[panel.Children.Count];
                panel.Children.CopyTo(array, 0);
                panel.IsItemsHost = false;
                panel.Children.Clear();
                for (int i = 0; i < array.Length; i++)
                {
                    array[i].ClearValue(DockLayoutManagerProperty);
                }
            }
            DockBarContainerControl dObj = uiElement as DockBarContainerControl;
            if (dObj != null)
            {
                BarManagerPropertyHelper.ClearBarManager(dObj);
                BarContainerControlPropertyAccessor.GetBars(dObj).Clear();
            }
        }

        public void CloseMenu()
        {
            this.CustomizationController.CloseMenu();
        }

        protected virtual object CoerceIsCustomization(bool baseValue) => 
            this.LayoutController.IsCustomization;

        protected virtual object CoerceIsRenaming(bool newValue)
        {
            object obj2;
            using (IEnumerator<IView> enumerator = this.ViewAdapter.Views.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        LayoutView current = (LayoutView) enumerator.Current;
                        RenameController renameController = current.AdornerHelper.GetRenameController();
                        if ((renameController == null) || !renameController.IsRenamingStarted)
                        {
                            continue;
                        }
                        obj2 = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return obj2;
        }

        private static object CoerceLayoutItem(DependencyObject dObj, object value) => 
            !(dObj is DockLayoutManager) ? value : null;

        protected virtual string CoerceWindowTitleFormat(string format) => 
            string.IsNullOrEmpty(format) ? DockLayoutManagerParameters.WindowTitleFormat : format;

        public bool Collapse(bool immediately)
        {
            bool flag = false;
            foreach (IView view in this.ViewAdapter.Views)
            {
                if (view.Type == HostType.AutoHide)
                {
                    this.ViewAdapter.ActionService.Hide(view, immediately);
                    flag = true;
                }
            }
            return flag;
        }

        internal void CollapseOtherViews(BaseLayoutItem item)
        {
            LayoutPanel panel = item as LayoutPanel;
            if ((panel != null) && (panel.IsAutoHidden && !base.IsDisposing))
            {
                AutoHideTray rootUIScope = panel.Parent.GetRootUIScope() as AutoHideTray;
                if (rootUIScope != null)
                {
                    foreach (AutoHideTray tray2 in this.autoHideTrayCollection)
                    {
                        if (!ReferenceEquals(rootUIScope, tray2))
                        {
                            tray2.DoCollapse(null);
                        }
                    }
                }
            }
        }

        internal void CompleteNativeDragging()
        {
            if (this.NativeDraggingCompleted == null)
            {
                EventHandler nativeDraggingCompleted = this.NativeDraggingCompleted;
            }
            else
            {
                this.NativeDraggingCompleted(this, EventArgs.Empty);
            }
        }

        protected internal virtual AutoHideGroup CreateAutoHideGroup() => 
            new AutoHideGroup();

        protected virtual AutoHideGroupCollection CreateAutoHideGroupsCollection() => 
            new AutoHideGroupCollection();

        protected virtual IView CreateAutoHideView(IUIElement element) => 
            new AutoHideView(element);

        protected virtual ClosedPanelCollection CreateClosedPanelsCollection() => 
            new ClosedPanelCollection(this);

        protected virtual ICustomizationController CreateCustomizationController() => 
            new DevExpress.Xpf.Docking.Customization.CustomizationController(this);

        protected virtual IView CreateCustomizationView(IUIElement element) => 
            new CustomizationView(element);

        protected virtual IDockController CreateDockController() => 
            new DevExpress.Xpf.Docking.DockController(this);

        protected internal virtual DocumentGroup CreateDocumentGroup() => 
            new DocumentGroup();

        protected internal virtual DocumentPanel CreateDocumentPanel() => 
            new DocumentPanel();

        protected virtual DevExpress.Xpf.Docking.Platform.DragAdorner CreateDragAdorner() => 
            new DevExpress.Xpf.Docking.Platform.DragAdorner(this);

        protected internal virtual EmptySpaceItem CreateEmptySpaceItem() => 
            new EmptySpaceItem();

        protected internal virtual FloatGroup CreateFloatGroup() => 
            new FloatGroup();

        protected virtual FloatGroupCollection CreateFloatGroupsCollection() => 
            new FloatGroupCollection();

        protected virtual IView CreateFloatingView(IUIElement element) => 
            new FloatingView(element);

        protected internal virtual LabelItem CreateLabelItem() => 
            new LabelItem();

        protected internal virtual LayoutControlItem CreateLayoutControlItem() => 
            new LayoutControlItem();

        protected virtual ILayoutController CreateLayoutController() => 
            new DevExpress.Xpf.Docking.LayoutController(this);

        protected internal virtual LayoutGroup CreateLayoutGroup() => 
            new LayoutGroup();

        protected internal virtual LayoutGroup CreateLayoutGroup(Orientation orientation)
        {
            LayoutGroup group = this.CreateLayoutGroup();
            group.BeginInit();
            group.Orientation = orientation;
            group.EndInit();
            return group;
        }

        protected internal virtual LayoutPanel CreateLayoutPanel() => 
            new LayoutPanel();

        protected internal virtual LayoutSplitter CreateLayoutSplitter() => 
            new LayoutSplitter();

        protected virtual IView CreateLayoutView(IUIElement element) => 
            new LayoutView(element);

        protected virtual IMDIController CreateMDIController() => 
            new DevExpress.Xpf.Docking.MDIController(this);

        protected virtual DevExpress.Xpf.Docking.RenameHelper CreateRenameHelper() => 
            new DevExpress.Xpf.Docking.RenameHelper(this);

        protected internal virtual SeparatorItem CreateSeparatorItem() => 
            new SeparatorItem();

        protected virtual ISerializationController CreateSerializationController() => 
            new DevExpress.Xpf.Docking.SerializationController(this);

        protected internal virtual TabbedGroup CreateTabbedGroup() => 
            new TabbedGroup();

        private IView CreateView(IUIElement element) => 
            !(element is AutoHideTray) ? (!(element is FloatPanePresenter) ? ((!(element is LayoutGroup) || (((LayoutGroup) element).ItemType != LayoutItemType.Group)) ? (!(element is CustomizationControl) ? null : this.CreateCustomizationView(element)) : this.CreateLayoutView(element)) : this.CreateFloatingView(element)) : this.CreateAutoHideView(element);

        protected virtual IViewAdapter CreateViewAdapter() => 
            new DockLayoutManagerViewAdapter(this);

        void ILogicalOwner.AddChild(object child)
        {
            this.AddLogicalChild(child);
        }

        void ILogicalOwner.RemoveChild(object child)
        {
            this.RemoveLogicalChild(child);
        }

        void ISupportBatchUpdate.BeginUpdate()
        {
            this.LockUpdate();
        }

        void ISupportBatchUpdate.EndUpdate()
        {
            this.UnlockUpdate();
        }

        protected void DisposeAutoHideTrays()
        {
            string[] strArray = new string[] { "Left", "Top", "Right", "Bottom" };
            for (int i = 0; i < strArray.Length; i++)
            {
                AutoHideTray templateChild = base.GetTemplateChild("PART_" + strArray[i] + "AutoHideTray") as AutoHideTray;
                AutoHidePane refToDispose = base.GetTemplateChild("PART_" + strArray[i] + "AutoHideTrayPanel") as AutoHidePane;
                Ref.Dispose<AutoHidePane>(ref refToDispose);
                Ref.Dispose<AutoHideTray>(ref templateChild);
            }
            this.autoHideTrayCollection.Clear();
        }

        protected void DisposeClosedPanel()
        {
            if (this.ClosedItemsPanel != null)
            {
                this.ClosedItemsPanel.Dispose();
                this.ClosedItemsPanel = null;
            }
        }

        protected internal void DisposeFloatContainers()
        {
            foreach (FloatGroup group in this.FloatGroups.ToArray())
            {
                FloatPanePresenter element = group.UIElements.GetElement<FloatPanePresenter>();
                if (element != null)
                {
                    element.Dispose();
                }
            }
        }

        protected void DisposeFloatingLayer()
        {
            if (this.FloatingLayer != null)
            {
                this.FloatingLayer.Dispose();
                this.FloatingLayer = null;
            }
        }

        protected void DisposeLayoutLayer()
        {
            if (this.LayoutLayer != null)
            {
                this.LayoutLayer.ClearValue(DXContentPresenter.ContentProperty);
                this.LayoutLayer = null;
            }
            if (this.LayoutRoot != null)
            {
                this.LayoutRoot.ClearTemplate();
            }
        }

        protected void DisposeVisualTreeHost()
        {
            this.visualChildrenCore.Clear();
        }

        private void DoDelayedActivation(DelayedActivation operation)
        {
            if (!base.IsDisposing && (operation.Item != null))
            {
                this.ActivateCore(operation.Item, operation.Focus);
            }
        }

        public void EndCustomization()
        {
            this.CustomizationController.EndCustomization();
        }

        protected internal void EndFloating()
        {
            this.floatingCounter--;
        }

        protected virtual void EndUpdate()
        {
        }

        internal static DockLayoutManager Ensure(DependencyObject dObj, bool forceFind = false)
        {
            DockLayoutManager objA = (dObj as DockLayoutManager) ?? GetDockLayoutManager(dObj);
            if (ReferenceEquals(objA, null) | forceFind)
            {
                objA = FindManager(dObj);
            }
            return EnsureManager(dObj, objA);
        }

        private void EnsureCustomization()
        {
            string themeName = null;
            if (this.walker != null)
            {
                ThemeTreeWalker target = this.walker.Target as ThemeTreeWalker;
                if (target != null)
                {
                    themeName = target.ThemeName;
                }
            }
            ThemeTreeWalker treeWalker = ThemeManager.GetTreeWalker(this);
            if ((treeWalker == null) || (treeWalker.ThemeName == themeName))
            {
                this.walker = null;
            }
            else
            {
                if (this.walker != null)
                {
                    this.EndCustomization();
                }
                this.walker = new WeakReference(treeWalker);
            }
        }

        private void EnsureIsCustomizationInDesignTime()
        {
            if (this.IsInDesignTime)
            {
                DevExpress.Xpf.Docking.InvokeHelper.BeginInvoke(this, new Action(this.EnsureIsCustomizationInDesignTimeCore), DispatcherPriority.Render, new object[0]);
            }
        }

        private void EnsureIsCustomizationInDesignTimeCore()
        {
            ((DevExpress.Xpf.Docking.Customization.CustomizationController) this.CustomizationController).CoerceValue(DevExpress.Xpf.Docking.Customization.CustomizationController.IsCustomizationProperty);
        }

        protected internal void EnsureLayoutRoot()
        {
            this.LayoutRoot ??= this.CreateLayoutGroup();
        }

        protected internal void EnsureLogicalTree()
        {
            this.AddLogicalChild(this.DockHintsContainer);
            Array.ForEach<BaseLayoutItem>(this.GetItems(), item => item.Manager = this);
        }

        private static DockLayoutManager EnsureManager(DependencyObject dObj, DockLayoutManager manager)
        {
            dObj.SetValue(DockLayoutManagerProperty, manager);
            return manager;
        }

        private void EnsureOwnerWindowSubscriptions()
        {
            if (this.OwnerWindow != null)
            {
                this.UnSubscribeOwnerWindowEvents(false);
            }
            this.OwnerWindow = DevExpress.Xpf.Docking.WindowServiceHelper.GetWindow(this);
            this.TopLevelWindow = LayoutHelper.FindRoot(this, false) as Window;
            if (this.OwnerWindow != null)
            {
                this.OwnerWindowTitle = this.OwnerWindow.Title;
                this.SubscribeOwnerWindowEvents();
            }
            if (this.VisualRoot != null)
            {
                this.UnsubscribeVisualRootEvents();
            }
            Window ownerWindow = this.OwnerWindow;
            Window topLevelVisual = ownerWindow;
            if (ownerWindow == null)
            {
                Window local1 = ownerWindow;
                topLevelVisual = (Window) LayoutHelper.GetTopLevelVisual(this);
            }
            this.VisualRoot = topLevelVisual;
            if (this.VisualRoot != null)
            {
                this.SubscribeVisualRootEvents();
            }
        }

        private void EnsureSystemEventsSubscriptions()
        {
            this.UnsubscribeSystemEvents();
            this.SubscribeSystemEvents();
        }

        internal void ExecuteActionOnLayoutUnlocked(PendingActionCallback pendingAction, object arg)
        {
            this._layoutLocker.AddPendingAction(pendingAction, arg);
        }

        public bool ExtendSelectionToParent()
        {
            if (base.IsDisposing || (!this.IsCustomization || (this.ActiveLayoutItem == null)))
            {
                return false;
            }
            IView view = this.GetView(this.ActiveLayoutItem.GetRoot());
            return this.ViewAdapter.SelectionService.ExtendSelectionToParent(view);
        }

        internal bool FindItemNameInLinked(string name) => 
            this.LinkedItemNames.Contains<string>(name);

        private static BaseLayoutItem FindLayoutItem(DependencyObject dObj)
        {
            BaseLayoutItem layoutItem = GetLayoutItem(dObj);
            if (layoutItem == null)
            {
                psvContentControl control = dObj as psvContentControl;
                if (control != null)
                {
                    layoutItem = control.Content as BaseLayoutItem;
                }
            }
            return layoutItem;
        }

        internal static DockLayoutManager FindManager(DependencyObject dObj)
        {
            DockLayoutManager manager = dObj as DockLayoutManager;
            if (manager == null)
            {
                IUIElement element = (dObj as IUIElement) ?? dObj.GetParentIUIElement();
                if (element != null)
                {
                    manager = element.GetManager() as DockLayoutManager;
                }
            }
            return manager;
        }

        protected virtual void FloatingWindowsReposition()
        {
            if (!this.IsDesktopFloatingMode)
            {
                foreach (FloatGroup group in this.FloatGroups)
                {
                    if (group.IsOpen)
                    {
                        FloatPanePresenter element = group.UIElements.GetElement<FloatPanePresenter>();
                        if (element != null)
                        {
                            element.CheckBoundsInContainer();
                        }
                    }
                    group.UpdateMaximizedBounds();
                }
            }
        }

        internal void FocusItem(BaseLayoutItem item, bool ignoreFocusState = false)
        {
            if (item is LayoutPanel)
            {
                this.FocusPanelItem((LayoutPanel) item, ignoreFocusState);
            }
            else
            {
                this.FocusLayoutItem(item);
            }
        }

        private void FocusLayoutItem(BaseLayoutItem item)
        {
            LayoutControlItem item2 = item as LayoutControlItem;
            if ((item2 != null) && (item2.FocusContentOnActivating && !this.IsCustomization))
            {
                KeyboardFocusHelper.FocusElement(item2.Control, false);
            }
        }

        private void FocusPanelItem(LayoutPanel panelItem, bool ignoreFocusState)
        {
            if (panelItem != null)
            {
                if (panelItem.IsControlItemsHost)
                {
                    this.FocusLayoutItem(LayoutItemsHelper.GetFirstItemInGroup(panelItem.Layout));
                }
                else if (panelItem.FocusContentOnActivating)
                {
                    KeyboardFocusHelper.FocusElement(panelItem.Control ?? panelItem.ContentPresenter, ignoreFocusState || this.IsFloating);
                }
            }
        }

        internal void ForceCollapseByMouseCheck(Point? pos)
        {
            this.autoHideTrayCollection.ForEach(x => x.CheckCollapseByMouse(pos));
        }

        internal T GenerateGroup<T>() where T: LayoutGroup
        {
            T group = this.layoutGroupGenerator.GetGroup<T>();
            group.IsAutoGenerated = true;
            return group;
        }

        internal bool GetClosedPanelsVisibility() => 
            this._ManualClosedPanelsBarVisibility;

        protected virtual object GetContainerForItem(object item, BaseLayoutItem target) => 
            this.containerGenerator.GetContainerForItem(target as IGeneratorHost, item, this.ItemTemplate, this.ItemTemplateSelector);

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static DockLayoutManager GetDockLayoutManager(DependencyObject obj) => 
            (DockLayoutManager) obj.GetValue(DockLayoutManagerProperty);

        private static DocumentPanel GetDocument(BaseLayoutItem item)
        {
            DocumentPanel selectedItem = item as DocumentPanel;
            DocumentGroup group = item as DocumentGroup;
            if (group != null)
            {
                selectedItem = group.SelectedItem as DocumentPanel;
            }
            FloatGroup group2 = item as FloatGroup;
            if ((group2 != null) && group2.HasItems)
            {
                selectedItem = group2[0] as DocumentPanel;
            }
            return (((selectedItem == null) || !selectedItem.IsMDIChild) ? null : selectedItem);
        }

        protected FloatGroup[] GetFloatGroups(bool getSorted)
        {
            FloatGroup[] array = new FloatGroup[this.FloatGroups.Count];
            if (array.Length != 0)
            {
                this.FloatGroups.CopyTo(array, 0);
                if (getSorted)
                {
                    Array.Sort<FloatGroup>(array, this.GetFloatGroupComparison());
                }
            }
            return array;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static BaseLayoutItem GetLayoutItem(DependencyObject obj) => 
            (BaseLayoutItem) obj.GetValue(LayoutItemProperty);

        protected internal DevExpress.Xpf.Core.FloatingMode GetRealFloatingMode() => 
            (WindowHelper.IsXBAP || this.IsInDesignTime) ? DevExpress.Xpf.Core.FloatingMode.Adorner : ((DevExpress.Xpf.Core.FloatingMode) this.FloatingMode);

        internal Point GetRestoreOffset() => 
            !this.CanGetRestoreOffset() ? this.SavedRestoreOffset : this.GetRestoreOffsetCore();

        private Point GetRestoreOffsetCore() => 
            WindowHelper.GetScreenLocation(this);

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static IUIElement GetUIScope(DependencyObject obj) => 
            (IUIElement) obj.GetValue(UIScopeProperty);

        protected override Visual GetVisualChild(int index) => 
            (index >= base.VisualChildrenCount) ? this.visualChildrenCore[index - base.VisualChildrenCount] : base.GetVisualChild(index);

        protected internal virtual void HideCustomization()
        {
            if (this.IsCustomization)
            {
                this.ViewAdapter.ProcessAction(ViewAction.HideSelection);
                this.ShouldRestoreCustomizationForm = (this.CustomizationController.CustomizationForm != null) && this.CustomizationController.CustomizationForm.IsOpen;
                this.CustomizationController.HideCustomizationForm();
                this.RenameHelper.CancelRenaming();
            }
        }

        public void HideCustomizationForm()
        {
            this.CustomizationController.HideCustomizationForm();
        }

        protected internal virtual void HideFloatingWindows()
        {
            this.CanShowFloatGroup = false;
            this.CustomizationController.CloseMenu();
            foreach (FloatGroup group in this.FloatGroups)
            {
                if (group.IsOpen)
                {
                    group.ShouldRestoreOnActivate = true;
                    group.IsOpen = false;
                }
            }
        }

        internal void InvokeUpdateFloatingPaneResources()
        {
            Action<DispatcherOperation> action = <>c.<>9__517_0;
            if (<>c.<>9__517_0 == null)
            {
                Action<DispatcherOperation> local1 = <>c.<>9__517_0;
                action = <>c.<>9__517_0 = x => x.Abort();
            }
            this.updateFloatingPaneResourcesOperation.Do<DispatcherOperation>(action);
            this.updateFloatingPaneResourcesOperation = DevExpress.Xpf.Docking.InvokeHelper.BeginInvoke(this, () => this.GetIsStyleUpdateInProgress(), new Action(this.UpdateFloatingPaneResources), DispatcherPriority.Normal);
        }

        internal bool IsContainerHost(LayoutGroup group) => 
            this.containerGenerator.IsContainerHost(group) || this.Linked.Any<DockLayoutManager>(x => x.containerGenerator.IsContainerHost(group));

        private bool IsLogicalTreeChangingLocked(DependencyObject element) => 
            this.logicalTreeLocks.FirstOrDefault<LogicalTreeLocker>(x => x.IsLocked(element)) != null;

        internal void LockActivation()
        {
            this.activationLockCount++;
            if (this.DockController is ILockOwner)
            {
                ((ILockOwner) this.DockController).Lock();
            }
            if (this.MDIController is ILockOwner)
            {
                ((ILockOwner) this.MDIController).Lock();
            }
        }

        internal void LockClosedPanelsVisibility()
        {
            this.closedPanelsLocker.Lock();
        }

        internal void LockItemActivationOnFocus()
        {
            this._focusLocker.Lock();
            base.Dispatcher.BeginInvoke(() => this._focusLocker.Unlock(), DispatcherPriority.Input, new object[0]);
        }

        private void LockItemToLinkBinderService()
        {
            if (!this.itemToLinkBinderServiceLocker)
            {
                BarNameScope.GetService<IItemToLinkBinderService>(this).Lock();
            }
            this.itemToLinkBinderServiceLocker.Lock();
        }

        internal void LockLogicalTreeChanging(LogicalTreeLocker logicalLocker)
        {
            this.LockItemToLinkBinderService();
            if (!this.logicalTreeLocks.Contains(logicalLocker))
            {
                this.logicalTreeLocks.Add(logicalLocker);
            }
        }

        protected void LockUpdate()
        {
            if (!this.IsUpdateLocked)
            {
                this.updatesCount = 0;
            }
            this.lockUpdateCounter++;
        }

        private bool Navigate(bool forward)
        {
            DependencyObject focusedElement = KeyboardFocusHelper.FocusedElement;
            BaseLayoutItem activeItem = null;
            if (focusedElement != null)
            {
                activeItem = GetLayoutItem(focusedElement);
            }
            return this.ProcessPanelNavigation(activeItem, forward);
        }

        private static void NotifyListener(IDockLayoutManagerListener listener, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != null)
            {
                listener.Unsubscribe((DockLayoutManager) e.OldValue);
            }
            if (e.NewValue != null)
            {
                listener.Subscribe((DockLayoutManager) e.NewValue);
            }
        }

        protected virtual void OnActiveDockItemChanged(BaseLayoutItem oldItem, BaseLayoutItem activeItem)
        {
            if (!base.IsDisposing)
            {
                this.DockController.ActiveItem = activeItem;
                this.CheckCustomizationRoot(activeItem);
                this.SynchronizeWithCurrentItem(activeItem);
            }
        }

        protected virtual void OnActiveLayoutItemChanged(BaseLayoutItem oldItem, BaseLayoutItem activeItem)
        {
            if (!base.IsDisposing)
            {
                this.LayoutController.ActiveItem = activeItem;
                this.CheckCustomizationRoot(activeItem);
                this.SynchronizeWithCurrentItem(activeItem);
            }
        }

        protected virtual void OnActiveMDIItemChanged(BaseLayoutItem oldItem, BaseLayoutItem activeItem)
        {
            if (!base.IsDisposing)
            {
                this.MDIController.ActiveItem = activeItem;
                this.CheckCustomizationRoot(activeItem);
                this.SynchronizeWithCurrentItem(activeItem);
            }
        }

        protected override void OnActualSizeChanged(Size value)
        {
            base.OnActualSizeChanged(value);
            this.SaveRestoreOffset();
        }

        protected internal virtual void OnAddToItemsSource(IEnumerable newItems, int startingIndex = 0)
        {
            using (new UpdateBatch(this))
            {
                if (newItems != null)
                {
                    foreach (object obj2 in newItems)
                    {
                        this.PrepareItemCore(obj2);
                    }
                    this.Update();
                }
            }
        }

        protected virtual void OnAllowCustomizationChanged(bool allow)
        {
            if (!allow)
            {
                this.CustomizationController.EndCustomization();
            }
        }

        protected virtual void OnAllowDocumentSelectorChanged(bool allow)
        {
            if (!allow)
            {
                this.CustomizationController.HideDocumentSelectorForm();
            }
        }

        private void OnAllowMergingAutoHidePanelsChanged(bool oldValue, bool newValue)
        {
            Action<DockLayoutManagerMergingHelper> action = <>c.<>9__673_0;
            if (<>c.<>9__673_0 == null)
            {
                Action<DockLayoutManagerMergingHelper> local1 = <>c.<>9__673_0;
                action = <>c.<>9__673_0 = x => x.Changed();
            }
            this.MergingHelper.Do<DockLayoutManagerMergingHelper>(action);
        }

        private static void OnAllowMergingAutoHidePanelsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as DockLayoutManager).Do<DockLayoutManager>(x => x.OnAllowMergingAutoHidePanelsChanged((bool) e.OldValue, (bool) e.NewValue));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            HitTestHelper.ResetCache();
            this.autoHideTrayCollection.Clear();
            this.SetAutoHideTrayAffinity(Dock.Left, ref this.PartLeftAutoHideTray, ref this.PartLeftAutoHidePane);
            this.SetAutoHideTrayAffinity(Dock.Right, ref this.PartRightAutoHideTray, ref this.PartRightAutoHidePane);
            this.SetAutoHideTrayAffinity(Dock.Top, ref this.PartTopAutoHideTray, ref this.PartTopAutoHidePane);
            this.SetAutoHideTrayAffinity(Dock.Bottom, ref this.PartBottomAutoHideTray, ref this.PartBottomAutoHidePane);
            this.SetClosedItemsPanelAffinity();
            this.SetLayoutLayerAffinity();
            this.SetFloatingLayerAffinity();
            this.EnsureCustomization();
            if (this.IsInDesignTime)
            {
                this.EnsureLogicalTree();
            }
        }

        private void OnAutoHideGroupsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (this.AllowMergingAutoHidePanels)
            {
                Func<DockLayoutManager, bool> evaluator = <>c.<>9__674_0;
                if (<>c.<>9__674_0 == null)
                {
                    Func<DockLayoutManager, bool> local1 = <>c.<>9__674_0;
                    evaluator = <>c.<>9__674_0 = x => x.AllowMergingAutoHidePanels;
                }
                if (this.MergedParent.Return<DockLayoutManager, bool>(evaluator, (<>c.<>9__674_1 ??= () => false)) && base.IsVisible)
                {
                    Func<DockLayoutManager, AutoHideGroupCollection> func2 = <>c.<>9__674_2;
                    if (<>c.<>9__674_2 == null)
                    {
                        Func<DockLayoutManager, AutoHideGroupCollection> local3 = <>c.<>9__674_2;
                        func2 = <>c.<>9__674_2 = x => x.AutoHideGroups;
                    }
                    this.MergedParent.With<DockLayoutManager, AutoHideGroupCollection>(func2).Do<AutoHideGroupCollection>(x => x.Merge(this, this.AutoHideGroups));
                }
            }
        }

        private void OnAutoHideModeChanged(DevExpress.Xpf.Docking.Base.AutoHideMode oldValue, DevExpress.Xpf.Docking.Base.AutoHideMode newValue)
        {
            this.RaiseAutoHideDisplayModeChanged();
        }

        protected virtual void OnClosedItemsBarVisibilityChanged(DevExpress.Xpf.Docking.Base.ClosedPanelsBarVisibility visibility)
        {
            this.CustomizationController.ClosedPanelsBarVisibility = visibility;
        }

        protected override AutomationPeer OnCreateAutomationPeer() => 
            new DockLayoutManagerAutomationPeer(this);

        protected internal virtual void OnCurrentChanged(object sender)
        {
            ICollectionView view = sender as ICollectionView;
            if (view != null)
            {
                this.OnCurrentCollectionItemChanged(view.CurrentItem);
            }
        }

        protected virtual void OnCurrentCollectionItemChanged(object value)
        {
            if (this.IsSynchronizedWithCurrentItem && (value != null))
            {
                BaseLayoutItem containerForItem = this.containerGenerator.GetContainerForItem(value) as BaseLayoutItem;
                this.Activate(containerForItem);
            }
        }

        protected override void OnDispose()
        {
            HwndHostEventAccumulator.Unregister(this);
            HitTestHelper.ResetCache();
            ThemeChangedEventManager.RemoveListener(this, this);
            if (this.ensureAdornerVisibilityOperation == null)
            {
                DispatcherOperation ensureAdornerVisibilityOperation = this.ensureAdornerVisibilityOperation;
            }
            else
            {
                this.ensureAdornerVisibilityOperation.Abort();
            }
            if (this.restoreCustomizationOperation == null)
            {
                DispatcherOperation restoreCustomizationOperation = this.restoreCustomizationOperation;
            }
            else
            {
                this.restoreCustomizationOperation.Abort();
            }
            base.IsVisibleChanged -= new DependencyPropertyChangedEventHandler(this.OnIsVisibleChanged);
            this.UnsubscribeSystemEvents();
            if (this.OwnerWindow != null)
            {
                this.ResetMDIChildrenTitle();
                this.UnSubscribeOwnerWindowEvents(false);
            }
            if (this.VisualRoot != null)
            {
                this.UnsubscribeVisualRootEvents();
            }
            this.VisualRoot = null;
            Ref.Dispose<DockLayoutManagerItemsCollection>(ref this.itemsInternal);
            this.DisposeFloatContainers();
            this.DisposeAutoHideTrays();
            this.DisposeClosedPanel();
            this.DisposeLayoutLayer();
            this.DisposeFloatingLayer();
            Ref.Dispose<ISerializationController>(ref this.serializationControllerCore);
            Ref.Dispose<ICustomizationController>(ref this.customizationControllerCore);
            Ref.Dispose<IDockController>(ref this.dockControllerCore);
            Ref.Dispose<ILayoutController>(ref this.layoutControllerCore);
            Ref.Dispose<IMDIController>(ref this.mdiControllerCore);
            Ref.Dispose<ClosedPanelCollection>(ref this.closedPanelsCore);
            Ref.Dispose<DockLayoutManagerMergingHelper>(ref this.mergingHelper);
            Ref.Dispose<FloatGroupCollection>(ref this.floatGroupsCore);
            this.autoHideGroupsCore.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.OnAutoHideGroupsCollectionChanged);
            Ref.Dispose<AutoHideGroupCollection>(ref this.autoHideGroupsCore);
            Ref.Dispose<IViewAdapter>(ref this.viewAdapterCore);
            Ref.Dispose<DevExpress.Xpf.Docking.RenameHelper>(ref this.renameHelperCore);
            Ref.Dispose<DelayedActionsHelper>(ref this.loadedActionsHelper);
            Ref.Dispose<DelayedActionsHelper>(ref this.initilizedActionsHelper);
            Ref.Dispose<DevExpress.Xpf.Docking.Platform.Win32AdornerWindowProvider>(ref this.adornerWindowProvider);
            this.DisposeVisualTreeHost();
            base.OnDispose();
        }

        private static void OnDockLayoutManagerChanged(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
        {
            if (dObj is IDockLayoutManagerListener)
            {
                NotifyListener((IDockLayoutManagerListener) dObj, e);
            }
            if (e.NewValue == null)
            {
                ClearItemsControlPanel(dObj as UIElement);
            }
            IUIElement element = dObj as IUIElement;
            if (element != null)
            {
                DockLayoutManager oldValue;
                if (e.OldValue != null)
                {
                    oldValue = (DockLayoutManager) e.OldValue;
                    oldValue.ResetView(element);
                    oldValue.UnRegisterView(element);
                    if (!(dObj is BaseLayoutItem))
                    {
                        dObj.ClearValue(UIScopeProperty);
                        BaseLayoutItem item = FindLayoutItem(dObj);
                        if (item != null)
                        {
                            item.UIElements.Remove(element);
                        }
                    }
                }
                if (e.NewValue != null)
                {
                    oldValue = (DockLayoutManager) e.NewValue;
                    IUIElement element2 = (dObj is BaseLayoutItem) ? null : dObj.FindUIScope();
                    if (element2 is DockLayoutManager)
                    {
                        oldValue.RegisterView(element);
                    }
                    BaseLayoutItem item2 = dObj as BaseLayoutItem;
                    if (item2 == null)
                    {
                        dObj.SetValue(UIScopeProperty, element2);
                        oldValue.ResetView(element);
                    }
                    else if (oldValue.IsInitialized && (!Equals(item2.Manager, oldValue) && oldValue.GetItems().Contains<BaseLayoutItem>(item2)))
                    {
                        item2.Manager = oldValue;
                    }
                }
            }
        }

        private void OnEnableWin32CompatibilityChanged(bool oldValue, bool newValue)
        {
            this.InvokeUpdateFloatingPaneResources();
            Action<FloatGroup> action = <>c.<>9__676_0;
            if (<>c.<>9__676_0 == null)
            {
                Action<FloatGroup> local1 = <>c.<>9__676_0;
                action = <>c.<>9__676_0 = x => x.CoerceValue(FloatGroup.IsOpenProperty);
            }
            this.FloatGroups.ForEach<FloatGroup>(action);
        }

        private void OnFloatGroupsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            bool flag = e.Action == NotifyCollectionChangedAction.Reset;
            IList list1 = (flag || (e.OldItems == null)) ? ((IList) new FloatGroup[0]) : e.OldItems;
            IEnumerable enumerable = list1;
            IList list2 = (flag || (e.NewItems == null)) ? this.FloatGroups : e.NewItems;
            IEnumerable enumerable2 = list2;
            foreach (object obj2 in enumerable)
            {
                FloatGroup item = (FloatGroup) obj2;
                item.DockLayoutManagerCore = null;
                if (this.MinimizedItems.Contains(item))
                {
                    this.MinimizedItems.Remove(item);
                }
            }
            if (flag)
            {
                this.MinimizedItems.Clear();
            }
            foreach (object obj3 in enumerable2)
            {
                FloatGroup item = (FloatGroup) obj3;
                item.DockLayoutManagerCore = this;
                if ((item.FloatState == FloatState.Minimized) && !this.MinimizedItems.Contains(item))
                {
                    this.MinimizedItems.Add(item);
                }
            }
        }

        internal void OnGroupNameChanged(LayoutGroup layoutGroup)
        {
            WeakList<object> list;
            if (!string.IsNullOrEmpty(layoutGroup.Name) && this.pendingItems.TryGetValue(layoutGroup.Name, out list))
            {
                foreach (object obj2 in list.ToList<object>())
                {
                    this.PrepareItemCore(obj2);
                }
            }
        }

        private void OnHandleHwndHostMouseEventsChanged(bool oldValue)
        {
            if (!this.HandleHwndHostMouseEvents)
            {
                HwndHostEventAccumulator.Unregister(this);
            }
            else if (base.IsLoaded)
            {
                HwndHostEventAccumulator.Register(this);
            }
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            this.mergingHelper = new DockLayoutManagerMergingHelper(this);
            this.AddLogicalChild(this.mergingHelper);
            BarNameScope.EnsureRegistrator(this);
            this.EnsureLogicalTree();
            this.initilizedActionsHelper.DoDelayedActions();
            this.AddValuesProvider();
        }

        protected virtual void OnIsCustomizationChanged(bool newValue)
        {
            this.RenameHelper.CancelRenamingAndResetClickedState();
            this.ViewAdapter.ProcessAction(newValue ? ViewAction.ShowSelection : ViewAction.HideSelection);
            foreach (BaseLayoutItem item in this.GetItems())
            {
                item.OnIsCustomizationChanged(newValue);
            }
            this.Update();
        }

        protected virtual void OnIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Action<FloatGroup> action = <>c.<>9__616_0;
            if (<>c.<>9__616_0 == null)
            {
                Action<FloatGroup> local1 = <>c.<>9__616_0;
                action = <>c.<>9__616_0 = x => x.CoerceValue(UIElement.IsEnabledProperty);
            }
            this.FloatGroups.ForEach<FloatGroup>(action);
        }

        protected virtual void OnIsSynchronizedWithCurrentItemChanged(bool oldValue, bool newValue)
        {
            this.OnCurrentCollectionItemChanged(this.itemsInternal.CurrentItem);
        }

        protected virtual void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            bool newValue = (bool) e.NewValue;
            this.ShouldRestoreOnIsVisibleChanged = newValue;
            if (this.restoreCustomizationOperation == null)
            {
                DispatcherOperation restoreCustomizationOperation = this.restoreCustomizationOperation;
            }
            else
            {
                this.restoreCustomizationOperation.Abort();
            }
            if (this.ensureAdornerVisibilityOperation == null)
            {
                DispatcherOperation ensureAdornerVisibilityOperation = this.ensureAdornerVisibilityOperation;
            }
            else
            {
                this.ensureAdornerVisibilityOperation.Abort();
            }
            if (!newValue)
            {
                this.CustomizationController.HideDocumentSelectorForm();
                this.HideFloatingWindows();
                this.HideCustomization();
                this.ResetAdorners();
            }
            else
            {
                this.loadedActionsHelper.AddDelayedAction(new Action(this.RestoreFloatingWindows));
                this.ensureAdornerVisibilityOperation = base.Dispatcher.BeginInvoke(() => VisualizerAdornerHelper.EnsureAdornerActivated(this.DragAdorner), new object[0]);
                this.restoreCustomizationOperation = DevExpress.Xpf.Docking.InvokeHelper.BeginInvoke(this, new Action(this.RestoreCustomization), DispatcherPriority.Render, new object[0]);
                if (ScreenHelper.IsAttachedToPresentationSource(this) && base.IsLoaded)
                {
                    this.loadedActionsHelper.DoDelayedActions();
                }
            }
            Action<DockLayoutManagerMergingHelper> action = <>c.<>9__617_1;
            if (<>c.<>9__617_1 == null)
            {
                Action<DockLayoutManagerMergingHelper> local3 = <>c.<>9__617_1;
                action = <>c.<>9__617_1 = x => x.Changed();
            }
            this.MergingHelper.Do<DockLayoutManagerMergingHelper>(action);
        }

        protected internal virtual void OnItemReplacedInItemsSource(IList oldItems, IList newItems, int newStartingIndex)
        {
            this.containerGenerator.LinkContainerToItem(oldItems[0], newItems[0], newStartingIndex, this.ItemTemplate, this.ItemTemplateSelector);
        }

        protected virtual void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue, DependencyPropertyChangedEventArgs e)
        {
            if (!base.IsDisposing)
            {
                using (new UpdateBatch(this))
                {
                    this.ResetItemsSource();
                    if (!base.IsInitialized && (newValue != null))
                    {
                        this.initilizedActionsHelper.AddDelayedAction(() => this.itemsInternal.SetItemsSource(this.ItemsSource));
                    }
                    else
                    {
                        this.itemsInternal.SetItemsSource(newValue);
                    }
                }
            }
        }

        private void OnItemTargetNameChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "TargetName")
            {
                this.ClearItemCore(sender);
                this.PrepareItemCore(sender);
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if ((e.Key == Key.Tab) && (((Keyboard.Modifiers & ModifierKeys.Control) != ModifierKeys.None) && !this.AllowDocumentSelector))
            {
                e.Handled = this.Navigate(!KeyHelper.IsShiftPressed);
            }
            base.OnKeyDown(e);
        }

        private static void OnLayoutItemChanged(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
        {
            BaseLayoutItem oldValue = e.OldValue as BaseLayoutItem;
            OnLayoutItemChanged(dObj, oldValue, e.NewValue as BaseLayoutItem);
        }

        private static void OnLayoutItemChanged(DependencyObject dObj, BaseLayoutItem oldValue, BaseLayoutItem newValue)
        {
            IUIElement element = dObj as IUIElement;
            if ((element != null) && !(dObj is Splitter))
            {
                BaseLayoutItem item;
                DockLayoutManager objB = FindManager(dObj);
                if (oldValue != null)
                {
                    item = oldValue;
                    item.UIElements.Remove(element);
                    if (!ReferenceEquals(item.Manager, objB))
                    {
                        Func<bool> fallback = <>c.<>9__105_1;
                        if (<>c.<>9__105_1 == null)
                        {
                            Func<bool> local1 = <>c.<>9__105_1;
                            fallback = <>c.<>9__105_1 = () => false;
                        }
                        if (!item.Manager.Return<DockLayoutManager, bool>(x => x.IsLogicalTreeChangingLocked(item), fallback) && (((IUIElement) item).Scope == null))
                        {
                            item.Manager = null;
                        }
                    }
                }
                if (newValue != null)
                {
                    item = newValue;
                    if (Array.IndexOf<BaseLayoutItem>(objB.GetItems(), item) != -1)
                    {
                        item.UIElements.Add(element);
                        item.Manager = objB;
                    }
                }
                if (objB != null)
                {
                    objB.ResetView(element);
                }
            }
        }

        internal void OnLayoutRestored()
        {
            this.EnsureLogicalTree();
            this.UpdateClosedPanelsBar();
            this.Update();
            foreach (IView view in this.ViewAdapter.Views)
            {
                view.Invalidate();
            }
            this.CustomizationController.ClearSelection();
            Action method = new Action(this.RequestCheckFloatGroupRestoreBounds);
            Window window = DevExpress.Xpf.Docking.WindowServiceHelper.GetWindow(this);
            if (ScreenHelper.IsAttachedToPresentationSource(this) && base.IsLoaded)
            {
                this.RestoreFloatingWindows();
            }
            else
            {
                this.loadedActionsHelper.AddDelayedAction(new Action(this.RestoreFloatingWindows));
            }
            if (window is DXWindow)
            {
                base.Dispatcher.BeginInvoke(method, new object[0]);
            }
            else
            {
                method();
            }
            DevExpress.Xpf.Docking.InvokeHelper.BeginInvoke(this, new Action(this.RestoreFloatGroupOrder), DispatcherPriority.Loaded, new object[0]);
        }

        private static void OnLayoutRootChanged(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
        {
            DockLayoutManager manager = dObj as DockLayoutManager;
            if (manager != null)
            {
                manager.InvalidateMeasure();
                manager.InvalidateArrange();
                LayoutGroup newValue = e.NewValue as LayoutGroup;
                if ((newValue != null) && (newValue.ItemType != LayoutItemType.Group))
                {
                    throw new NotSupportedException(DockLayoutManagerHelper.GetRule(DockLayoutManagerRule.WrongLayoutRoot));
                }
                if (newValue != null)
                {
                    newValue.DockLayoutManagerCore = manager;
                    newValue.IsRootGroup = true;
                    if (!manager.OptimizedLogicalTree)
                    {
                        manager.AddLogicalChild(newValue);
                    }
                    newValue.Manager = manager;
                    newValue.SelectTemplate();
                }
                newValue = e.OldValue as LayoutGroup;
                if (newValue != null)
                {
                    newValue.DockLayoutManagerCore = null;
                    newValue.IsRootGroup = false;
                    if (!manager.OptimizedLogicalTree)
                    {
                        manager.RemoveLogicalChild(newValue);
                    }
                    newValue.ClearTemplate();
                }
            }
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            this.EnsureLayoutRoot();
            if (!WindowHelper.IsXBAP)
            {
                this.EnsureSystemEventsSubscriptions();
                this.EnsureOwnerWindowSubscriptions();
                this.EnsureIsCustomizationInDesignTime();
                this.SaveRestoreOffset();
            }
            this.CheckFloatGroupsFloatState();
            if (ScreenHelper.IsAttachedToPresentationSource(this))
            {
                this.loadedActionsHelper.DoDelayedActions();
            }
            if (this.HandleHwndHostMouseEvents)
            {
                HwndHostEventAccumulator.Register(this);
            }
        }

        protected virtual void OnLogicalTreeStructureChanged(DevExpress.Xpf.Docking.LogicalTreeStructure oldValue, DevExpress.Xpf.Docking.LogicalTreeStructure newValue)
        {
            this.LayoutRoot.Do<LayoutGroup>(delegate (LayoutGroup x) {
                if (newValue == DevExpress.Xpf.Docking.LogicalTreeStructure.Default)
                {
                    this.AddLogicalChild(x);
                }
                else
                {
                    this.RemoveLogicalChild(x);
                }
            });
            this.GetItems().OfType<LayoutGroup>().ToList<LayoutGroup>().ForEach(x => x.SyncLogicalTreeWithManager(null, this));
        }

        protected virtual void OnMDIMergeStyleChanged(DevExpress.Xpf.Bars.MDIMergeStyle oldValue, DevExpress.Xpf.Bars.MDIMergeStyle newValue)
        {
            this.RaiseMDIStyleChanged();
        }

        internal void OnMerge(DockLayoutManager child)
        {
            if (!base.IsDisposing)
            {
                DockLayoutManagerLinker.Link(this, child);
                child.MergedParent = this;
                if (this.AllowMergingAutoHidePanels && (child.AllowMergingAutoHidePanels && child.IsVisible))
                {
                    this.AutoHideGroups.Merge(child, child.AutoHideGroups);
                }
            }
        }

        protected virtual void OnOwnerWindowTitleChanged()
        {
            if (this.lockOwnerWindowTitleChanged <= 0)
            {
                this.lockOwnerWindowTitleChanged++;
                this.OwnerWindowTitle = this.OwnerWindow?.Title;
                this.lockOwnerWindowTitleChanged--;
            }
        }

        protected virtual void OnOwnsFloatWindowsChanged(bool oldValue, bool newValue)
        {
            foreach (FloatGroup group in this.FloatGroups)
            {
                FloatPanePresenter element = group.UIElements.GetElement<FloatPanePresenter>();
                if (element != null)
                {
                    element.EnsureOwnerWindow();
                }
            }
        }

        protected internal virtual void OnRemoveFromItemsSource(IEnumerable oldItems)
        {
            using (new UpdateBatch(this))
            {
                if (oldItems != null)
                {
                    foreach (object obj2 in oldItems)
                    {
                        this.ClearItemCore(obj2);
                    }
                }
            }
        }

        protected internal virtual void OnResetItemsSource(IEnumerable newItems)
        {
            this.ResetItemsSource(newItems);
        }

        protected virtual void OnShowInvisibleItemsChanged(bool? newValue)
        {
            this.UpdateItemsVisibility();
            ShowInvisibleItemsChangedEventArgs e = new ShowInvisibleItemsChangedEventArgs(newValue);
            e.Source = this;
            base.RaiseEvent(e);
        }

        protected virtual void OnShowInvisibleItemsInCustomizationFormChanged(bool value)
        {
            this.UpdateItemsVisibility();
        }

        private void OnSystemEventsDisplaySettingsChanged(object sender, EventArgs e)
        {
            if (!base.IsDisposing)
            {
                Action<FloatGroup> action = <>c.<>9__678_0;
                if (<>c.<>9__678_0 == null)
                {
                    Action<FloatGroup> local1 = <>c.<>9__678_0;
                    action = <>c.<>9__678_0 = x => x.CoerceValue(BaseLayoutItem.FloatSizeProperty);
                }
                this.FloatGroups.ForEach<FloatGroup>(action);
            }
        }

        protected internal virtual void OnThemeChanged()
        {
            this.PrepareLayoutForModification();
            if (this.IsDesktopFloatingMode && (this.CanGetRestoreOffset() || (this.effectiveRestoreOffset != null)))
            {
                this.themeChangedLocker.Lock();
                Point restoreOffset = this.GetRestoreOffset();
                object[] args = new object[] { this.FloatGroups.ToArray(), restoreOffset };
                base.Dispatcher.BeginInvoke(new Action<IEnumerable<FloatGroup>, Point>(this.OnThemeChangedAsync), DispatcherPriority.Normal, args);
            }
        }

        internal void OnThemeChanged(ThemeChangedRoutedEventArgs e)
        {
            this.GetItems().ForEach<BaseLayoutItem>(x => x.OnThemeChanged(e));
            if (this._previousTheme != e.ThemeName)
            {
                this._previousTheme = e.ThemeName;
                this.OnThemeChanged();
            }
        }

        private void OnThemeChangedAsync(IEnumerable<FloatGroup> affectedItems, Point prevOffset)
        {
            this.themeChangedLocker.Unlock();
            if (this.CanGetRestoreOffset())
            {
                Point restoreOffset = this.GetRestoreOffset();
                foreach (FloatGroup group in affectedItems)
                {
                    group.EnsureFloatLocation(prevOffset, restoreOffset);
                }
                this.SaveRestoreOffset();
            }
        }

        internal void OnThemeChanging(ThemeChangingRoutedEventArgs e)
        {
            this.GetItems().ForEach<BaseLayoutItem>(x => x.OnThemeChanging(e));
        }

        private void OnTopLevelWindowChanged()
        {
            VisualizerAdornerHelper.EnsureAdornerActivated(this.DragAdorner);
        }

        private static void OnUIScopeChanged(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
        {
            IUIElement element = dObj as IUIElement;
            if (element != null)
            {
                if (e.OldValue != null)
                {
                    ((IUIElement) e.OldValue).Children.Remove(element);
                }
                if (e.NewValue != null)
                {
                    ((IUIElement) e.NewValue).Children.Add(element);
                }
            }
        }

        protected override void OnUnloaded()
        {
            HwndHostEventAccumulator.Unregister(this);
            if (this.OwnerWindow != null)
            {
                this.UnSubscribeOwnerWindowEvents(true);
            }
            if (this.VisualRoot != null)
            {
                this.UnsubscribeVisualRootEvents();
            }
            this.VisualRoot = null;
            this.UnRegisterInputBindings();
            this.UnsubscribeSystemEvents();
            base.OnUnloaded();
        }

        internal void OnUnmerge(DockLayoutManager child)
        {
            if (!base.IsDisposing)
            {
                DockLayoutManagerLinker.Unlink(this, child);
                this.AutoHideGroups.Unmerge(child, child.AutoHideGroups);
            }
        }

        protected void OwnerWindowBoundsChanged()
        {
            this.FloatingWindowsReposition();
            this.SaveRestoreOffset();
        }

        protected virtual void OwnerWindowClosed(object sender)
        {
            if (this.DisposeOnWindowClosing && ReferenceEquals(DevExpress.Xpf.Docking.WindowServiceHelper.GetWindow(this), sender))
            {
                base.Dispose();
            }
            Action<LayoutItemsControl> action = <>c.<>9__628_0;
            if (<>c.<>9__628_0 == null)
            {
                Action<LayoutItemsControl> local1 = <>c.<>9__628_0;
                action = <>c.<>9__628_0 = x => x.PrepareForWindowClosing();
            }
            this.FloatingLayer.Do<LayoutItemsControl>(action);
        }

        protected virtual void OwnerWindowPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Handled)
            {
                if ((e.Key == Key.Tab) && ((Keyboard.Modifiers & ModifierKeys.Control) != ModifierKeys.None))
                {
                    Predicate<DependencyObject> predicate1 = <>c.<>9__629_0;
                    if (<>c.<>9__629_0 == null)
                    {
                        Predicate<DependencyObject> local1 = <>c.<>9__629_0;
                        predicate1 = <>c.<>9__629_0 = fe => (fe is DockLayoutManager) && ((DockLayoutManager) fe).AllowDocumentSelector;
                    }
                    DependencyObject obj2 = LayoutHelper.FindLayoutOrVisualParentObject(e.OriginalSource as DependencyObject, predicate1, false, null);
                    if (this.CanProcessKey(obj2) && this.CanShowDocumentSelector())
                    {
                        this.CustomizationController.ShowDocumentSelectorForm();
                        e.Handled = this.CustomizationController.IsDocumentSelectorVisible;
                    }
                }
                Predicate<DependencyObject> predicate = <>c.<>9__629_1;
                if (<>c.<>9__629_1 == null)
                {
                    Predicate<DependencyObject> local2 = <>c.<>9__629_1;
                    predicate = <>c.<>9__629_1 = fe => fe is DockLayoutManager;
                }
                DockLayoutManager eventProcessor = LayoutHelper.FindLayoutOrVisualParentObject(e.OriginalSource as DependencyObject, predicate, true, null) as DockLayoutManager;
                if ((eventProcessor != null) && this.CanProcessKey(eventProcessor))
                {
                    Key key = e.Key;
                    if (key == Key.Return)
                    {
                        this.RenameHelper.EndRenaming();
                    }
                    else if (key == Key.Escape)
                    {
                        if (!this.RenameHelper.CancelRenamingAndResetClickedState())
                        {
                            this.ExtendSelectionToParent();
                        }
                    }
                    else if ((key == Key.Delete) && !this.IsRenaming)
                    {
                        this.LayoutController.HideSelectedItems();
                    }
                }
            }
        }

        protected virtual void OwnerWindowPreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (KeyboardHelper.IsControlPressed && this.CustomizationController.IsDocumentSelectorVisible)
            {
                this.CustomizationController.HideDocumentSelectorForm();
            }
        }

        private void PrepareItemCore(object obj)
        {
            BaseLayoutItem item;
            string str = this.ResolveLayoutAdapter().Resolve(this, obj);
            (obj as INotifyPropertyChanged).Do<INotifyPropertyChanged>(delegate (INotifyPropertyChanged x) {
                x.PropertyChanged += new PropertyChangedEventHandler(this.OnItemTargetNameChanged);
            });
            if (!this.RaiseBeforeItemAddedEvent(obj, (string.IsNullOrEmpty(str) ? null : this.GetItem(str)), out item))
            {
                if (item == null)
                {
                    this.AddToPending(obj, str);
                }
                else
                {
                    this.RemoveFromPending(obj, str);
                    this.GetContainerForItem(obj, item);
                }
            }
        }

        private void PrepareItems()
        {
            if (this.SerializationController.IsDeserializing || this.TreeWalkerHelper.IsThemeChanging())
            {
                Array.ForEach<BaseLayoutItem>(this.GetItems(), item => item.PrepareForModification(this.SerializationController.IsDeserializing));
            }
        }

        protected internal void PrepareLayoutForModification()
        {
            if (this.SerializationController.IsDeserializing)
            {
                this.EnsureLayoutRoot();
            }
            this.PrepareViews();
            this.PrepareItems();
        }

        private void PrepareViews()
        {
            foreach (IView view in this.ViewAdapter.Views)
            {
                if (view.Type == HostType.AutoHide)
                {
                    this.ViewAdapter.ProcessAction(view, ViewAction.Hide);
                }
                view.Invalidate();
            }
        }

        internal void ProcessKey(KeyEventArgs e)
        {
            LayoutView dragSource = this.ViewAdapter.DragService.DragSource as LayoutView;
            if (dragSource != null)
            {
                dragSource.RootUIElementKeyDown(this, e);
            }
        }

        internal void PurgeLogicalChildren()
        {
            Func<object, bool> predicate = <>c.<>9__529_0;
            if (<>c.<>9__529_0 == null)
            {
                Func<object, bool> local1 = <>c.<>9__529_0;
                predicate = <>c.<>9__529_0 = x => x is BaseLayoutItem;
            }
            foreach (object obj2 in this.logicalChildrenCore.Except<object>(this.GetItems()).Where<object>(predicate).ToArray<object>())
            {
                if (!this.IsLogicalTreeChangingLocked(obj2 as DependencyObject))
                {
                    this.RemoveLogicalChild(obj2);
                }
            }
        }

        internal void QueueFocus(BaseLayoutItem item)
        {
            this._focusLocker.QueueFocus(item);
        }

        protected void RaiseAutoHideDisplayModeChanged()
        {
            this.AutoHideDisplayModeChanged(this, new PropertyChangedEventArgs("AutoHideDisplayModeChanged"));
        }

        internal void RaiseDockItemActivatedEvent(BaseLayoutItem item, BaseLayoutItem oldItem)
        {
            if (DockLayoutManagerParameters.SquashDockItemActivatedEvents)
            {
                if (this.dockItemActivatedOperation == null)
                {
                    this.previousActiveItem = oldItem;
                }
                else
                {
                    oldItem = this.previousActiveItem ?? oldItem;
                }
                Action<DispatcherOperation> action = <>c.<>9__531_0;
                if (<>c.<>9__531_0 == null)
                {
                    Action<DispatcherOperation> local2 = <>c.<>9__531_0;
                    action = <>c.<>9__531_0 = x => x.Abort();
                }
                this.dockItemActivatedOperation.Do<DispatcherOperation>(action);
            }
            DockItemActivatedEventArgs args = new DockItemActivatedEventArgs(item, oldItem);
            object[] objArray1 = new object[] { args };
            this.dockItemActivatedOperation = DevExpress.Xpf.Docking.InvokeHelper.BeginInvoke(this, delegate (DockItemActivatedEventArgs x) {
                if (!ReferenceEquals(x.Item, x.OldItem))
                {
                    base.RaiseEvent(x);
                }
                this.dockItemActivatedOperation = null;
                this.previousActiveItem = null;
            }, DispatcherPriority.Normal, objArray1);
        }

        protected void RaiseMDIStyleChanged()
        {
            if (this.MDIMergeStyleChanged != null)
            {
                this.MDIMergeStyleChanged(this, new PropertyChangedEventArgs("MDIMergeStyle"));
            }
        }

        protected virtual void RegisterInputBindings()
        {
            if (!base.InputBindings.Contains(this.CloseMDIItemCommandBinding))
            {
                base.InputBindings.Add(this.CloseMDIItemCommandBinding);
            }
        }

        private void RegisterView(IUIElement element)
        {
            if (!base.IsDisposing)
            {
                IView view = this.ViewAdapter.GetView(element);
                if (view != null)
                {
                    this.ViewAdapter.Views.Remove(view);
                    Ref.Dispose<IView>(ref view);
                }
                IView view2 = this.CreateView(element);
                if (view2 != null)
                {
                    this.ViewAdapter.Views.Add(view2);
                }
            }
        }

        internal void RegisterViewIfNeeded(IUIElement element)
        {
            if (!base.IsDisposing && (this.ViewAdapter.GetView(element) == null))
            {
                this.RegisterView(element);
            }
        }

        internal static void Release(DependencyObject dObj)
        {
            if (dObj != null)
            {
                dObj.ClearValue(DockLayoutManagerProperty);
                dObj.ClearValue(LayoutItemProperty);
            }
        }

        private void RemoveFromPending(object item, string targetName)
        {
            WeakList<object> list;
            if (!string.IsNullOrEmpty(targetName) && this.pendingItems.TryGetValue(targetName, out list))
            {
                list.Remove(item);
            }
        }

        internal static void RemoveFromVisualTree(DockLayoutManager container, FrameworkElement child)
        {
            if (container != null)
            {
                container.RemoveVisualChild(child);
            }
        }

        protected void RemoveLogicalChild(object child)
        {
            DependencyObject current = child as DependencyObject;
            this.logicalChildrenCore.Remove(child);
            this.internalElements.Remove(child);
            if (ReferenceEquals(LogicalTreeHelper.GetParent(current), this))
            {
                Action<object> method = x => base.RemoveLogicalChild(x);
                if (this.GetIsLogicalChildrenIterationInProgress())
                {
                    object[] args = new object[] { child };
                    base.Dispatcher.BeginInvoke(method, args);
                }
                else
                {
                    method(child);
                }
            }
        }

        internal static void RemoveLogicalChild(DockLayoutManager container, DependencyObject child)
        {
            if ((child != null) && (container != null))
            {
                container.RemoveLogicalChild(child);
            }
        }

        protected virtual void RemoveVisualChild(FrameworkElement child)
        {
            if (this.visualChildrenCore.Contains(child))
            {
                this.visualChildrenCore.Remove(child);
            }
        }

        public bool Rename(BaseLayoutItem item) => 
            this.RenameHelper.Rename(item);

        protected internal virtual void RequestCheckFloatGroupRestoreBounds()
        {
            if (!base.IsDisposing)
            {
                VisitDelegate<FloatGroup> visit = <>c.<>9__565_0;
                if (<>c.<>9__565_0 == null)
                {
                    VisitDelegate<FloatGroup> local1 = <>c.<>9__565_0;
                    visit = <>c.<>9__565_0 = x => x.FloatStateLockHelper.Lock();
                }
                this.FloatGroups.Accept<FloatGroup>(visit);
                Window window = DevExpress.Xpf.Docking.WindowServiceHelper.GetWindow(this);
                this.AddDelayedAction(new Action(this.CheckFloatGroupRestoreBounds), DelayedActionPriority.Default);
            }
        }

        protected void ResetAdorners()
        {
            DevExpress.Xpf.Docking.InvokeHelper.BeginInvoke(this, delegate {
                if (!base.IsVisible && !base.IsDisposing)
                {
                    foreach (LayoutView view in this.ViewAdapter.Views)
                    {
                        if ((view != null) && view.IsAdornerHelperInitialized)
                        {
                            view.AdornerHelper.Reset();
                        }
                    }
                    if (this.DragAdorner.IsActivated)
                    {
                        this.DragAdorner.Deactivate();
                    }
                }
            });
        }

        protected virtual void ResetItemsSource()
        {
            this.ResetItemsSource(null);
        }

        protected virtual void ResetItemsSource(IEnumerable newItems)
        {
            using (new UpdateBatch(this))
            {
                using (this._layoutLocker.Lock())
                {
                    IEnumerable items = this.containerGenerator.GetItems();
                    this.OnRemoveFromItemsSource(items);
                    this.containerGenerator.Reset();
                    this.OnAddToItemsSource(newItems, 0);
                }
            }
        }

        protected internal void ResetMDIChildrenTitle()
        {
            this.lockOwnerWindowTitleChanged++;
            if ((this.OwnerWindow != null) && (this.windowTitleLocker && (this.OwnerWindow.GetBindingExpression(Window.TitleProperty) == null)))
            {
                this.OwnerWindow.Title = this.OwnerWindowTitle;
            }
            this.windowTitleLocker.Unlock();
            this.lockOwnerWindowTitleChanged--;
        }

        internal void ResetView(IUIElement element)
        {
            if (!base.IsDisposing)
            {
                IUIElement rootUIScope = element.GetRootUIScope();
                IView view = this.ViewAdapter?.GetView(rootUIScope);
                if (view != null)
                {
                    view.Invalidate();
                }
            }
        }

        protected virtual ILayoutAdapter ResolveLayoutAdapter() => 
            MVVMHelper.GetLayoutAdapter(this) ?? LayoutAdapter.Instance;

        protected internal virtual void RestoreCustomization()
        {
            if (this.IsCustomization)
            {
                this.ViewAdapter.ProcessAction(ViewAction.ShowSelection);
                if (this.ShouldRestoreCustomizationForm)
                {
                    this.CustomizationController.ShowCustomizationForm();
                }
            }
        }

        private void RestoreFloatGroupOrder()
        {
            if (!base.IsDisposing)
            {
                Func<FloatGroup, int> keySelector = <>c.<>9__684_0;
                if (<>c.<>9__684_0 == null)
                {
                    Func<FloatGroup, int> local1 = <>c.<>9__684_0;
                    keySelector = <>c.<>9__684_0 = x => x.GetSerializableZOrder();
                }
                foreach (FloatGroup group in this.FloatGroups.OrderBy<FloatGroup, int>(keySelector))
                {
                    Action<FloatingWindowLock> action = <>c.<>9__684_1;
                    if (<>c.<>9__684_1 == null)
                    {
                        Action<FloatingWindowLock> local2 = <>c.<>9__684_1;
                        action = <>c.<>9__684_1 = x => x.Lock(FloatingWindowLock.LockerKey.Focus);
                    }
                    group.FloatingWindowLock.Do<FloatingWindowLock>(action);
                    this.BringToFront(group);
                    Action<FloatingWindowLock> action2 = <>c.<>9__684_2;
                    if (<>c.<>9__684_2 == null)
                    {
                        Action<FloatingWindowLock> local3 = <>c.<>9__684_2;
                        action2 = <>c.<>9__684_2 = x => x.Unlock(FloatingWindowLock.LockerKey.Focus);
                    }
                    group.FloatingWindowLock.Do<FloatingWindowLock>(action2);
                }
            }
        }

        protected internal virtual void RestoreFloatingWindows()
        {
            if (base.IsVisible)
            {
                this.CanShowFloatGroup = true;
                if (this.ShouldRestoreOnActivate || this.ShouldRestoreOnIsVisibleChanged)
                {
                    foreach (FloatGroup group in this.GetFloatGroups(true))
                    {
                        if (group.ShouldRestoreOnActivate)
                        {
                            group.ShouldRestoreOnActivate = false;
                            group.IsOpen ??= true;
                        }
                    }
                }
            }
        }

        private void RestoreLayoutCore(object path)
        {
            using (new NotificationBatch(this))
            {
                this.SerializationController.RestoreLayout(path);
                NotificationBatch.Action(this, this, null);
            }
        }

        public void RestoreLayoutFromStream(Stream stream)
        {
            this.RestoreLayoutCore(stream);
        }

        public void RestoreLayoutFromXml(string path)
        {
            this.RestoreLayoutCore(path);
        }

        public void SaveLayoutToStream(Stream stream)
        {
            this.SerializationController.SaveLayout(stream);
        }

        public void SaveLayoutToXml(string path)
        {
            this.SerializationController.SaveLayout(path);
        }

        private void SaveRestoreOffset()
        {
            if (this.CanGetRestoreOffset())
            {
                this.effectiveRestoreOffset = new Point?(this.GetRestoreOffset());
            }
        }

        public bool SelectItem(BaseLayoutItem item) => 
            this.SelectItem(item, DevExpress.Xpf.Layout.Core.SelectionMode.SingleItem);

        public bool SelectItem(BaseLayoutItem item, DevExpress.Xpf.Layout.Core.SelectionMode mode)
        {
            if (base.IsDisposing || (!this.IsCustomization || (item == null)))
            {
                return false;
            }
            LayoutGroup root = item.GetRoot();
            IView view = this.GetView(root);
            bool flag = false;
            if (view != null)
            {
                this.ViewAdapter.SelectionService.Select(view, view.GetElement(item), mode);
                this.ActiveLayoutItem = item;
                this.CustomizationController.CustomizationRoot = root;
                flag = true;
            }
            return flag;
        }

        private void SetAffinity(AutoHideTray tray, AutoHidePane panel, Dock dock)
        {
            tray.DockType = dock;
            panel.DockType = dock;
            tray.EnsureAutoHidePanel(panel);
        }

        private void SetAutoHideTrayAffinity(Dock dock, ref AutoHideTray partAutoHideTray, ref AutoHidePane partTrayPanel)
        {
            string str = dock.ToString();
            if ((partAutoHideTray != null) && !LayoutItemsHelper.IsTemplateChild<DockLayoutManager>(partAutoHideTray, this))
            {
                partAutoHideTray.Dispose();
            }
            if ((partTrayPanel != null) && !LayoutItemsHelper.IsTemplateChild<DockLayoutManager>(partTrayPanel, this))
            {
                partTrayPanel.Dispose();
            }
            partAutoHideTray = base.GetTemplateChild("PART_" + str + "AutoHideTray") as AutoHideTray;
            if (partAutoHideTray != null)
            {
                partTrayPanel = base.GetTemplateChild("PART_" + str + "AutoHideTrayPanel") as AutoHidePane;
                if (partTrayPanel != null)
                {
                    this.SetAffinity(partAutoHideTray, partTrayPanel, dock);
                }
                this.autoHideTrayCollection.Add(partAutoHideTray);
            }
            this.AutoHideLayer = base.GetTemplateChild("AutoHideLayer") as FrameworkElement;
        }

        private void SetClosedItemsPanelAffinity()
        {
            if ((this.ClosedItemsPanel != null) && !LayoutItemsHelper.IsTemplateChild<DockLayoutManager>(this.ClosedItemsPanel, this))
            {
                this.ClosedItemsPanel.Dispose();
            }
            this.ClosedItemsPanel = base.GetTemplateChild("PART_ClosedItemsPanel") as DevExpress.Xpf.Docking.ClosedItemsPanel;
        }

        public static void SetDockLayoutManager(DependencyObject obj, DockLayoutManager value)
        {
            obj.SetValue(DockLayoutManagerProperty, value);
        }

        private void SetFloatingLayerAffinity()
        {
            if ((this.FloatingLayer != null) && !LayoutItemsHelper.IsTemplateChild<DockLayoutManager>(this.FloatingLayer, this))
            {
                this.FloatingLayer.Dispose();
            }
            this.FloatingLayer = base.GetTemplateChild("FloatingLayer") as LayoutItemsControl;
        }

        public static void SetLayoutItem(DependencyObject obj, BaseLayoutItem value)
        {
            obj.SetValue(LayoutItemProperty, value);
        }

        private void SetLayoutLayerAffinity()
        {
            if ((this.LayoutLayer != null) && !LayoutItemsHelper.IsTemplateChild<DockLayoutManager>(this.LayoutLayer, this))
            {
                this.DisposeLayoutLayer();
            }
            this.LayoutLayer = base.GetTemplateChild("LayoutLayer") as DXContentPresenter;
            if (this.LayoutRoot != null)
            {
                this.LayoutRoot.SelectTemplate();
            }
        }

        protected internal void SetMDIChildrenTitle(string title)
        {
            this.lockOwnerWindowTitleChanged++;
            if ((this.OwnerWindow != null) && (this.OwnerWindow.GetBindingExpression(Window.TitleProperty) == null))
            {
                this.windowTitleLocker.LockOnce();
                this.OwnerWindow.Title = string.Format(this.WindowTitleFormat, this.OwnerWindowTitle, title);
            }
            this.lockOwnerWindowTitleChanged--;
        }

        public static void SetUIScope(DependencyObject obj, IUIElement value)
        {
            obj.SetValue(UIScopeProperty, value);
        }

        public void ShowContextMenu(BaseLayoutItem item)
        {
            this.CustomizationController.ShowContextMenu(item);
        }

        public void ShowCustomizationForm()
        {
            this.CustomizationController.ShowCustomizationForm();
        }

        public void ShowItemSelectorMenu(UIElement source, BaseLayoutItem[] items)
        {
            this.CustomizationController.ShowItemSelectorMenu(source, items);
        }

        internal void StartNativeDragging()
        {
            if (this.NativeDraggingStarted == null)
            {
                EventHandler nativeDraggingStarted = this.NativeDraggingStarted;
            }
            else
            {
                this.NativeDraggingStarted(this, EventArgs.Empty);
            }
        }

        protected virtual void SubscribeOwnerWindowEvents()
        {
            BindingHelper.SetBinding(this, OwnerWindowTitleProperty, this.OwnerWindow, "Title");
            OwnerWindowClosedEventManager.AddListener(this.OwnerWindow, this);
            OwnerWindowBoundsChangedEventManager.AddListener(this.OwnerWindow, this);
            if ((this.TopLevelWindow != null) && !ReferenceEquals(this.TopLevelWindow, this.OwnerWindow))
            {
                OwnerWindowBoundsChangedEventManager.AddListener(this.TopLevelWindow, this);
            }
        }

        private void SubscribeSystemEvents()
        {
            this.displaySettingsListener = new DisplaySettingsListener(this);
        }

        protected void SubscribeVisualRootEvents()
        {
            VisualRootPreviewKeyDownEventManager.AddListener(this.VisualRoot, this);
            VisualRootPreviewKeyUpEventManager.AddListener(this.VisualRoot, this);
        }

        private void SynchronizeWithCurrentItem(BaseLayoutItem item)
        {
            if ((item != null) && this.IsSynchronizedWithCurrentItem)
            {
                object itemForContainer = this.containerGenerator.GetItemForContainer(item);
                if (itemForContainer != null)
                {
                    this.itemsInternal.MoveCurrentTo(itemForContainer);
                }
            }
        }

        bool IWeakEventListener.ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            if (managerType == typeof(OwnerWindowBoundsChangedEventManager))
            {
                this.OwnerWindowBoundsChanged();
                return true;
            }
            if (managerType == typeof(OwnerWindowClosedEventManager))
            {
                this.OwnerWindowClosed(sender);
                return true;
            }
            if (managerType == typeof(VisualRootPreviewKeyDownEventManager))
            {
                this.OwnerWindowPreviewKeyDown(sender, e as KeyEventArgs);
                return true;
            }
            if (!(managerType == typeof(VisualRootPreviewKeyUpEventManager)))
            {
                return false;
            }
            this.OwnerWindowPreviewKeyUp(sender, e as KeyEventArgs);
            return true;
        }

        private bool TryActivate(IActiveItemOwner controller, BaseLayoutItem item, bool focus = true)
        {
            controller.Activate(item, focus);
            return ReferenceEquals(controller.ActiveItem, item);
        }

        internal void UnlockActivation()
        {
            if (this.DockController is ILockOwner)
            {
                ((ILockOwner) this.DockController).Unlock();
            }
            if (this.MDIController is ILockOwner)
            {
                ((ILockOwner) this.MDIController).Unlock();
            }
            this.activationLockCount--;
        }

        internal void UnlockClosedPanelsVisibility()
        {
            this.closedPanelsLocker.Unlock();
        }

        private void UnlockItemToLinkBinderService()
        {
            if (this.itemToLinkBinderServiceLocker)
            {
                this.itemToLinkBinderServiceLocker.Unlock();
                if (!this.itemToLinkBinderServiceLocker)
                {
                    BarNameScope.GetService<IItemToLinkBinderService>(this).Unlock();
                }
            }
        }

        internal void UnlockLogicalTreeChanging(LogicalTreeLocker logicalLocker)
        {
            this.logicalTreeLocks.Remove(logicalLocker);
            this.PurgeLogicalChildren();
            this.UnlockItemToLinkBinderService();
        }

        protected void UnlockUpdate()
        {
            int num = this.lockUpdateCounter - 1;
            this.lockUpdateCounter = num;
            if ((num == 0) && (this.updatesCount > 0))
            {
                this.Update(this.shouldUpdateLayout);
            }
        }

        protected virtual void UnRegisterInputBindings()
        {
            base.InputBindings.Remove(this.CloseMDIItemCommandBinding);
        }

        internal void UnRegisterView(IUIElement element)
        {
            IView view = this.ViewAdapter?.GetView(element);
            if (view != null)
            {
                this.ViewAdapter.Views.Remove(view);
                Ref.Dispose<IView>(ref view);
            }
        }

        protected virtual void UnSubscribeOwnerWindowEvents(bool keepWindowClosedListener = false)
        {
            this.ResetMDIChildrenTitle();
            BindingHelper.ClearBinding(this, OwnerWindowTitleProperty);
            if (!keepWindowClosedListener)
            {
                OwnerWindowClosedEventManager.RemoveListener(this.OwnerWindow, this);
            }
            OwnerWindowBoundsChangedEventManager.RemoveListener(this.OwnerWindow, this);
            this.OwnerWindow = null;
            if (this.TopLevelWindow != null)
            {
                OwnerWindowBoundsChangedEventManager.RemoveListener(this.TopLevelWindow, this);
                this.TopLevelWindow = null;
            }
        }

        private void UnsubscribeSystemEvents()
        {
            Ref.Dispose<IDisposable>(ref this.displaySettingsListener);
        }

        protected void UnsubscribeVisualRootEvents()
        {
            VisualRootPreviewKeyDownEventManager.RemoveListener(this.VisualRoot, this);
            VisualRootPreviewKeyUpEventManager.RemoveListener(this.VisualRoot, this);
        }

        protected internal void Update()
        {
            this.Update(true);
        }

        protected internal void Update(bool shouldUpdateLayout)
        {
            if (!base.IsDisposing)
            {
                this.updatesCount++;
                HitTestHelper.ResetCache();
                if (base.IsInitialized && !this.SerializationController.IsDeserializing)
                {
                    if (this.IsUpdateLocked)
                    {
                        this.shouldUpdateLayout ??= shouldUpdateLayout;
                    }
                    else
                    {
                        this.lockUpdateCounter++;
                        try
                        {
                            this.BeginUpdate();
                            this.UpdateMergingTarget();
                            if (shouldUpdateLayout && base.IsLoaded)
                            {
                                base.UpdateLayout();
                            }
                            this.InvalidateView(this.LayoutRoot);
                            if ((this.LayoutLayer != null) && (this.LayoutLayer.Parent != null))
                            {
                                ((UIElement) this.LayoutLayer.Parent).InvalidateMeasure();
                            }
                            this.CheckCustomizationRoot(this.LayoutRoot);
                            this.UpdateSelection();
                            this.UpdateActiveItems();
                            this.DecomposedItems.Purge();
                            this.PurgeLogicalChildren();
                        }
                        finally
                        {
                            this.shouldUpdateLayout = false;
                            this.lockUpdateCounter--;
                            this.updatesCount = 0;
                            this.EndUpdate();
                        }
                    }
                }
            }
        }

        private void UpdateActiveItems()
        {
            BaseLayoutItem[] items = this.GetItems();
            BaseLayoutItem activeDockItem = this.ActiveDockItem;
            if ((activeDockItem != null) && !items.Contains<BaseLayoutItem>(activeDockItem))
            {
                this.Deactivate(activeDockItem);
            }
            BaseLayoutItem activeMDIItem = this.ActiveMDIItem;
            if ((activeMDIItem != null) && !items.Contains<BaseLayoutItem>(activeMDIItem))
            {
                this.Deactivate(activeMDIItem);
            }
            BaseLayoutItem activeLayoutItem = this.ActiveLayoutItem;
            if ((activeLayoutItem != null) && !items.Contains<BaseLayoutItem>(activeLayoutItem))
            {
                this.Deactivate(activeLayoutItem);
            }
        }

        internal void UpdateClosedPanelsBar()
        {
            if (!base.IsDisposing && (this.ClosedPanelsBarVisibility == DevExpress.Xpf.Docking.Base.ClosedPanelsBarVisibility.Manual))
            {
                this.CustomizationController.ClosedPanelsVisibility = this.GetClosedPanelsVisibility();
            }
        }

        protected virtual void UpdateFloatingPaneResources()
        {
            this.DisableTransparencyBehavior.Detach();
            if (this.IsTransparencyDisabled)
            {
                this.DisableTransparencyBehavior.Attach(this);
            }
        }

        private void UpdateItemsVisibility()
        {
            foreach (BaseLayoutItem item in this.GetItems())
            {
                item.CoerceValue(UIElement.VisibilityProperty);
            }
        }

        private void UpdateMergingTarget()
        {
            if ((this.MergingTarget != null) && !this.GetItems().Contains<BaseLayoutItem>(this.MergingTarget))
            {
                this.MergingTarget = null;
            }
            if (this.MergingTarget == null)
            {
                Func<BaseLayoutItem, bool> predicate = <>c.<>9__697_0;
                if (<>c.<>9__697_0 == null)
                {
                    Func<BaseLayoutItem, bool> local1 = <>c.<>9__697_0;
                    predicate = <>c.<>9__697_0 = x => x is DocumentPanel;
                }
                Func<BaseLayoutItem, bool> func2 = <>c.<>9__697_1;
                if (<>c.<>9__697_1 == null)
                {
                    Func<BaseLayoutItem, bool> local2 = <>c.<>9__697_1;
                    func2 = <>c.<>9__697_1 = x => ((DocumentPanel) x).CanMerge;
                }
                DocumentPanel panel = (DocumentPanel) this.GetItems().Where<BaseLayoutItem>(predicate).FirstOrDefault<BaseLayoutItem>(func2);
                if (panel != null)
                {
                    this.MergingTarget = panel;
                }
            }
        }

        private void UpdateSelection()
        {
            if ((this.ViewAdapter != null) && this.IsCustomization)
            {
                this.ViewAdapter.ProcessAction(ViewAction.ShowSelection);
            }
        }

        internal void UpdateSelection(LayoutGroup layoutGroup)
        {
            if ((this.ViewAdapter != null) && this.IsCustomization)
            {
                IView view = this.GetView(layoutGroup);
                if (view != null)
                {
                    this.ViewAdapter.ProcessAction(view, ViewAction.ShowSelection);
                }
            }
        }

        [Description("Gets or sets the active dock item.This is a dependency property.")]
        public BaseLayoutItem ActiveDockItem
        {
            get => 
                (BaseLayoutItem) base.GetValue(ActiveDockItemProperty);
            set => 
                base.SetValue(ActiveDockItemProperty, value);
        }

        [Description("Gets or sets the active layout item. Setting this property doesn't move keyboard focus.This is a dependency property.")]
        public BaseLayoutItem ActiveLayoutItem
        {
            get => 
                (BaseLayoutItem) base.GetValue(ActiveLayoutItemProperty);
            set => 
                base.SetValue(ActiveLayoutItemProperty, value);
        }

        [Description("Gets or sets the active MDI child panel. This property is in effect when the assigned item represents an MDI child panel (DocumentPanel) within a DocumentGroup, and the group's DocumentGroup.MDIStyle property is set to MDI.This is a dependency property.")]
        public BaseLayoutItem ActiveMDIItem
        {
            get => 
                (BaseLayoutItem) base.GetValue(ActiveMDIItemProperty);
            set => 
                base.SetValue(ActiveMDIItemProperty, value);
        }

        [XtraSerializableProperty, Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
        public bool AllowAeroSnap
        {
            get => 
                (bool) base.GetValue(AllowAeroSnapProperty);
            set => 
                base.SetValue(AllowAeroSnapProperty, value);
        }

        [Description("Gets or sets whether Customization Mode can be invoked.This is a dependency property."), XtraSerializableProperty]
        public bool AllowCustomization
        {
            get => 
                (bool) base.GetValue(AllowCustomizationProperty);
            set => 
                base.SetValue(AllowCustomizationProperty, value);
        }

        [Description("Gets or sets whether dock items can be renamed.This is a dependency property.")]
        public bool? AllowDockItemRename
        {
            get => 
                (bool?) base.GetValue(AllowDockItemRenameProperty);
            set => 
                base.SetValue(AllowDockItemRenameProperty, value);
        }

        [Description("Gets or sets whether the Document Selector feature is enabled.This is a dependency property."), XtraSerializableProperty]
        public bool AllowDocumentSelector
        {
            get => 
                (bool) base.GetValue(AllowDocumentSelectorProperty);
            set => 
                base.SetValue(AllowDocumentSelectorProperty, value);
        }

        [EditorBrowsable(EditorBrowsableState.Never), XtraSerializableProperty]
        public bool AllowFloatGroupTransparency
        {
            get => 
                (bool) base.GetValue(AllowFloatGroupTransparencyProperty);
            set => 
                base.SetValue(AllowFloatGroupTransparencyProperty, value);
        }

        [Description("Gets or sets whether layout items can be renamed.This is a dependency property.")]
        public bool? AllowLayoutItemRename
        {
            get => 
                (bool?) base.GetValue(AllowLayoutItemRenameProperty);
            set => 
                base.SetValue(AllowLayoutItemRenameProperty, value);
        }

        public bool AllowMergingAutoHidePanels
        {
            get => 
                (bool) base.GetValue(AllowMergingAutoHidePanelsProperty);
            set => 
                base.SetValue(AllowMergingAutoHidePanelsProperty, value);
        }

        [Description("Gets or sets how an auto-hidden panel is expanded.This is a dependency property.")]
        public DevExpress.Xpf.Docking.Base.AutoHideExpandMode AutoHideExpandMode
        {
            get => 
                (DevExpress.Xpf.Docking.Base.AutoHideExpandMode) base.GetValue(AutoHideExpandModeProperty);
            set => 
                base.SetValue(AutoHideExpandModeProperty, value);
        }

        [Description("Provides access to the collection of AutoHideGroup objects, containing auto-hidden panels. Allows you to create auto-hidden panels in XAML."), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public AutoHideGroupCollection AutoHideGroups =>
            this.autoHideGroupsCore;

        [Description("Gets or sets time interval with which the DockLayoutManager checks whether it should close the opened auto-hide panel. This is a dependency property.")]
        public TimeSpan AutoHideGroupsCheckInterval
        {
            get => 
                (TimeSpan) base.GetValue(AutoHideGroupsCheckIntervalProperty);
            set => 
                base.SetValue(AutoHideGroupsCheckIntervalProperty, value);
        }

        public DevExpress.Xpf.Docking.Base.AutoHideMode AutoHideMode
        {
            get => 
                (DevExpress.Xpf.Docking.Base.AutoHideMode) base.GetValue(AutoHideModeProperty);
            set => 
                base.SetValue(AutoHideModeProperty, value);
        }

        [Description("Provides access to closed panels."), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ClosedPanelCollection ClosedPanels =>
            this.closedPanelsCore;

        public Dock ClosedPanelsBarPosition
        {
            get => 
                (Dock) base.GetValue(ClosedPanelsBarPositionProperty);
            set => 
                base.SetValue(ClosedPanelsBarPositionProperty, value);
        }

        [Description("Gets or sets the visibility state for the Closed Panels bar.This is a dependency property."), XtraSerializableProperty]
        public DevExpress.Xpf.Docking.Base.ClosedPanelsBarVisibility ClosedPanelsBarVisibility
        {
            get => 
                (DevExpress.Xpf.Docking.Base.ClosedPanelsBarVisibility) base.GetValue(ClosedPanelsBarVisibilityProperty);
            set => 
                base.SetValue(ClosedPanelsBarVisibilityProperty, value);
        }

        [Description("Gets or sets whether the float panels are closed when the DockLayoutManager is unloaded from visual tree. This is a dependency property.")]
        public bool CloseFloatWindowsOnManagerUnloading
        {
            get => 
                (bool) base.GetValue(CloseFloatWindowsOnManagerUnloadingProperty);
            set => 
                base.SetValue(CloseFloatWindowsOnManagerUnloadingProperty, value);
        }

        [Description("Gets or sets the way in which a panel is closed.")]
        public DevExpress.Xpf.Docking.ClosingBehavior ClosingBehavior { get; set; }

        [Description("A collection of modification actions to be performed on context menus for dock panels.")]
        public BarManagerActionCollection ContextMenuCustomizations =>
            this.CustomizationController.ItemContextMenuController.ActionContainer.Actions;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public LayoutGroupCollection DecomposedItems =>
            this.decomposedItems;

        [Description("Gets or sets the image displayed within a dock panel's header when the panel in the auto-hide state, and if no caption and image are explicitly assigned to the panel.This is a dependency property.")]
        public ImageSource DefaultAutoHidePanelCaptionImage
        {
            get => 
                (ImageSource) base.GetValue(DefaultAutoHidePanelCaptionImageProperty);
            set => 
                base.SetValue(DefaultAutoHidePanelCaptionImageProperty, value);
        }

        [Description("Gets or sets the image displayed within a dock panel's tab when the panel belongs to a tabbed group, and if no caption and image are explicitly assigned to the panel.This is a dependency property.")]
        public ImageSource DefaultTabPageCaptionImage
        {
            get => 
                (ImageSource) base.GetValue(DefaultTabPageCaptionImageProperty);
            set => 
                base.SetValue(DefaultTabPageCaptionImageProperty, value);
        }

        [Description(""), Browsable(false), EditorBrowsable(EditorBrowsableState.Never), Obsolete]
        public bool DestroyLastDocumentGroup
        {
            get => 
                (bool) base.GetValue(DestroyLastDocumentGroupProperty);
            set => 
                base.SetValue(DestroyLastDocumentGroupProperty, value);
        }

        [Description("")]
        public bool DisposeOnWindowClosing
        {
            get => 
                (bool) base.GetValue(DisposeOnWindowClosingProperty);
            set => 
                base.SetValue(DisposeOnWindowClosingProperty, value);
        }

        [Description("Gets the controller that provides methods to perform docking operations on panels.")]
        public IDockController DockController =>
            this.dockControllerCore;

        [XtraSerializableProperty]
        public DevExpress.Xpf.Docking.DockingStyle DockingStyle
        {
            get => 
                (DevExpress.Xpf.Docking.DockingStyle) base.GetValue(DockingStyleProperty);
            set => 
                base.SetValue(DockingStyleProperty, value);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced), XtraSerializableProperty]
        public bool EnableWin32Compatibility
        {
            get => 
                (bool) base.GetValue(EnableWin32CompatibilityProperty);
            set => 
                base.SetValue(EnableWin32CompatibilityProperty, value);
        }

        [Description("Provides access to floating groups of panels. Allows you to create floating panels in XAML."), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public FloatGroupCollection FloatGroups =>
            this.floatGroupsCore;

        [XtraSerializableProperty]
        public DevExpress.Xpf.Docking.Base.FloatingDocumentContainer FloatingDocumentContainer
        {
            get => 
                (DevExpress.Xpf.Docking.Base.FloatingDocumentContainer) base.GetValue(FloatingDocumentContainerProperty);
            set => 
                base.SetValue(FloatingDocumentContainerProperty, value);
        }

        [Description("Gets or sets how floating panels can be dragged, within or outside the boundaries of the current window.This is a dependency property."), XtraSerializableProperty]
        public DevExpress.Xpf.Docking.FloatingMode FloatingMode
        {
            get => 
                (DevExpress.Xpf.Docking.FloatingMode) base.GetValue(FloatingModeProperty);
            set => 
                base.SetValue(FloatingModeProperty, value);
        }

        [Description("Gets or sets whether the DockLayoutManager should handle the mouse events ocurring in layout items' nested HwndHosts.")]
        public bool HandleHwndHostMouseEvents
        {
            get => 
                (bool) base.GetValue(HandleHwndHostMouseEventsProperty);
            set => 
                base.SetValue(HandleHwndHostMouseEventsProperty, value);
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public HiddenItemsCollection HiddenItems =>
            this.LayoutController?.HiddenItems;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DXHelpExclude(true)]
        public bool IsDarkTheme
        {
            get => 
                (bool) base.GetValue(IsDarkThemeProperty);
            internal set => 
                base.SetValue(IsDarkThemePropertyKey, value);
        }

        [Description("Gets whether Customization Mode is enabled.This is a dependency property.")]
        public bool IsCustomization =>
            (bool) base.GetValue(IsCustomizationProperty);

        [Description("Gets whether the Customization Window is visible.")]
        public bool IsCustomizationFormVisible =>
            this.CustomizationController.IsCustomizationFormVisible;

        [Description("Gets whether an item is being renamed.This is a dependency property.")]
        public bool IsRenaming =>
            (bool) base.GetValue(IsRenamingProperty);

        [Description("Gets or sets whether the DockLayoutManager synchronizes its the currently selected child item with the current item in the System.ComponentModel.ICollectionView assigned to the DockLayoutManager.ItemsSource. This is a dependency property."), XtraSerializableProperty]
        public bool IsSynchronizedWithCurrentItem
        {
            get => 
                (bool) base.GetValue(IsSynchronizedWithCurrentItemProperty);
            set => 
                base.SetValue(IsSynchronizedWithCurrentItemProperty, value);
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), XtraSerializableProperty(XtraSerializationVisibility.Collection, true, true, false)]
        public SerializableItemCollection Items
        {
            get => 
                this.SerializationController.Items;
            set => 
                this.SerializationController.Items = value;
        }

        [Description("A collection of modification actions to be performed on selector menus for dock panels.")]
        public BarManagerActionCollection ItemSelectorMenuCustomizations =>
            this.CustomizationController.ItemsSelectorMenuController.ActionContainer.Actions;

        [Description("")]
        public IEnumerable ItemsSource
        {
            get => 
                (IEnumerable) base.GetValue(ItemsSourceProperty);
            set => 
                base.SetValue(ItemsSourceProperty, value);
        }

        public DataTemplate ItemTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ItemTemplateProperty);
            set => 
                base.SetValue(ItemTemplateProperty, value);
        }

        public DataTemplateSelector ItemTemplateSelector
        {
            get => 
                (DataTemplateSelector) base.GetValue(ItemTemplateSelectorProperty);
            set => 
                base.SetValue(ItemTemplateSelectorProperty, value);
        }

        [Description("This member supports the internal infrastructure, and is not intended to be used directly from your code.")]
        public BarManagerActionCollection LayoutControlItemContextMenuCustomizations =>
            this.CustomizationController.LayoutControlItemContextMenuController.ActionContainer.Actions;

        [Description("This member supports the internal infrastructure, and is not intended to be used directly from your code.")]
        public BarManagerActionCollection LayoutControlItemCustomizationMenuCustomizations =>
            this.CustomizationController.LayoutControlItemCustomizationMenuController.ActionContainer.Actions;

        [Description("Gets the controller that provides methods to perform layout operations on layout items.")]
        public ILayoutController LayoutController =>
            this.layoutControllerCore;

        [Description("Gets or sets a root group for items (panels and other groups).This is a dependency property.")]
        public LayoutGroup LayoutRoot
        {
            get => 
                (LayoutGroup) base.GetValue(LayoutRootProperty);
            set => 
                base.SetValue(LayoutRootProperty, value);
        }

        [XtraSerializableProperty, Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
        public bool ManualClosedPanelsBarVisibility
        {
            get => 
                this.CustomizationController.ClosedPanelsVisibility;
            set => 
                this._ManualClosedPanelsBarVisibility = value;
        }

        [Description("Gets the controller that provides methods to perform operations on MDI panels.")]
        public IMDIController MDIController =>
            this.mdiControllerCore;

        public DevExpress.Xpf.Bars.MDIMergeStyle MDIMergeStyle
        {
            get => 
                (DevExpress.Xpf.Bars.MDIMergeStyle) base.GetValue(MDIMergeStyleProperty);
            set => 
                base.SetValue(MDIMergeStyleProperty, value);
        }

        public ObservableCollection<object> MinimizedItems { get; private set; }

        [XtraSerializableProperty]
        public bool OwnsFloatWindows
        {
            get => 
                (bool) base.GetValue(OwnsFloatWindowsProperty);
            set => 
                base.SetValue(OwnsFloatWindowsProperty, value);
        }

        public DevExpress.Xpf.Docking.LogicalTreeStructure LogicalTreeStructure
        {
            get => 
                (DevExpress.Xpf.Docking.LogicalTreeStructure) base.GetValue(LogicalTreeStructureProperty);
            set => 
                base.SetValue(LogicalTreeStructureProperty, value);
        }

        public bool ShowContentWhenDragging
        {
            get => 
                (bool) base.GetValue(ShowContentWhenDraggingProperty);
            set => 
                base.SetValue(ShowContentWhenDraggingProperty, value);
        }

        [XtraSerializableProperty]
        public bool RedrawContentWhenResizing
        {
            get => 
                (bool) base.GetValue(RedrawContentWhenResizingProperty);
            set => 
                base.SetValue(RedrawContentWhenResizingProperty, value);
        }

        [XtraSerializableProperty]
        public bool ShowFloatWindowsInTaskbar
        {
            get => 
                (bool) base.GetValue(ShowFloatWindowsInTaskbarProperty);
            set => 
                base.SetValue(ShowFloatWindowsInTaskbarProperty, value);
        }

        [Description("Gets or sets whether invisible items are displayed within a layout.This is a dependency property.")]
        public bool? ShowInvisibleItems
        {
            get => 
                (bool?) base.GetValue(ShowInvisibleItemsProperty);
            set => 
                base.SetValue(ShowInvisibleItemsProperty, value);
        }

        [Description("Gets or sets if invisible BaseLayoutItems should be displayed in the Customization Mode.")]
        public bool ShowInvisibleItemsInCustomizationForm
        {
            get => 
                (bool) base.GetValue(ShowInvisibleItemsInCustomizationFormProperty);
            set => 
                base.SetValue(ShowInvisibleItemsInCustomizationFormProperty, value);
        }

        [Description("Gets or sets whether the caption of a maximized DocumentPanel is displayed within the window's title. This property is in effect in MDIStyle.MDI mode.")]
        public bool ShowMaximizedDocumentCaptionInWindowTitle
        {
            get => 
                (bool) base.GetValue(ShowMaximizedDocumentCaptionInWindowTitleProperty);
            set => 
                base.SetValue(ShowMaximizedDocumentCaptionInWindowTitleProperty, value);
        }

        [Description("Gets or sets a value that specifies how the layout items display their borders.")]
        public DockingViewStyle ViewStyle
        {
            get => 
                (DockingViewStyle) base.GetValue(ViewStyleProperty);
            set => 
                base.SetValue(ViewStyleProperty, value);
        }

        [Description("Gets or sets the format string used to format the window's title.This is a dependency property.")]
        public string WindowTitleFormat
        {
            get => 
                (string) base.GetValue(WindowTitleFormatProperty);
            set => 
                base.SetValue(WindowTitleFormatProperty, value);
        }

        internal bool AllowSelection =>
            this.IsCustomization && !this.IsInDesignTime;

        internal IEnumerable<AutoHidePane> AutoHidePanes
        {
            get
            {
                List<AutoHidePane> list1 = new List<AutoHidePane>();
                list1.Add(this.PartBottomAutoHidePane);
                list1.Add(this.PartLeftAutoHidePane);
                list1.Add(this.PartRightAutoHidePane);
                list1.Add(this.PartTopAutoHidePane);
                return list1;
            }
        }

        internal DevExpress.Xpf.Docking.DockControllerImpl DockControllerImpl
        {
            get
            {
                DevExpress.Xpf.Docking.DockControllerImpl dockControllerImpl = this.dockControllerImpl;
                if (this.dockControllerImpl == null)
                {
                    DevExpress.Xpf.Docking.DockControllerImpl local1 = this.dockControllerImpl;
                    dockControllerImpl = this.dockControllerImpl = new DevExpress.Xpf.Docking.DockControllerImpl(this);
                }
                return dockControllerImpl;
            }
        }

        internal bool EnableNativeDragging =>
            this.AllowAeroSnap && ((this.GetRealFloatingMode() == DevExpress.Xpf.Core.FloatingMode.Window) && (EnvironmentHelper.IsWinSevenOrLater && this.ShowContentWhenDragging));

        internal bool IsActivationLocked =>
            this.activationLockCount > 0;

        internal bool IsClosedPanelsVisibilityLocked =>
            (bool) this.closedPanelsLocker;

        internal bool IsDragging =>
            !base.IsDisposing && (this.ViewAdapter.DragService.DragItem != null);

        internal bool IsInDesignTime =>
            DesignerProperties.GetIsInDesignMode(this);

        internal bool IsLayoutLocked =>
            (bool) this._layoutLocker;

        internal bool IsThemeChangedLocked =>
            this.themeChangedLocker.IsLocked;

        internal bool IsTransparencyDisabled =>
            this.EnableWin32Compatibility || !this.AllowFloatGroupTransparency;

        internal WeakList<DockLayoutManager> Linked =>
            this._Linked;

        internal IDragService LinkedDragService { get; set; }

        internal IEnumerable<string> LinkedItemNames
        {
            get
            {
                Func<DockLayoutManager, IEnumerable<string>> selector = <>c.<>9__394_0;
                if (<>c.<>9__394_0 == null)
                {
                    Func<DockLayoutManager, IEnumerable<string>> local1 = <>c.<>9__394_0;
                    selector = <>c.<>9__394_0 = delegate (DockLayoutManager x) {
                        Func<BaseLayoutItem, string> func1 = <>c.<>9__394_1;
                        if (<>c.<>9__394_1 == null)
                        {
                            Func<BaseLayoutItem, string> local1 = <>c.<>9__394_1;
                            func1 = <>c.<>9__394_1 = y => y.Name;
                        }
                        return x.GetItems().Select<BaseLayoutItem, string>(func1);
                    };
                }
                return this.Linked.SelectMany<DockLayoutManager, string>(selector);
            }
        }

        internal bool OptimizedLogicalTree =>
            this.LogicalTreeStructure == DevExpress.Xpf.Docking.LogicalTreeStructure.Optimized;

        internal DevExpress.Xpf.Docking.Platform.Win32AdornerWindowProvider Win32AdornerWindowProvider
        {
            get
            {
                this.adornerWindowProvider ??= new DevExpress.Xpf.Docking.Platform.Win32AdornerWindowProvider();
                return this.adornerWindowProvider;
            }
        }

        internal DevExpress.Xpf.Docking.Platform.Win32DragService Win32DragService
        {
            get
            {
                this.win32DragService ??= new DevExpress.Xpf.Docking.Platform.Win32DragService();
                return this.win32DragService;
            }
        }

        protected internal FrameworkElement AutoHideLayer { get; private set; }

        protected internal bool CanAutoHideOnMouseDown =>
            (this.AutoHideExpandMode == DevExpress.Xpf.Docking.Base.AutoHideExpandMode.MouseDown) || (this.AutoHideMode == DevExpress.Xpf.Docking.Base.AutoHideMode.Inline);

        protected internal DevExpress.Xpf.Docking.ClosedItemsPanel ClosedItemsPanel { get; private set; }

        protected internal ICustomizationController CustomizationController =>
            this.customizationControllerCore;

        protected internal LogicalContainer<UIElement> DockHintsContainer { get; private set; }

        protected internal DevExpress.Xpf.Docking.Platform.DragAdorner DragAdorner { get; private set; }

        protected internal LayoutItemsControl FloatingLayer { get; private set; }

        protected internal bool IsDesktopFloatingMode =>
            this.GetRealFloatingMode() == DevExpress.Xpf.Core.FloatingMode.Window;

        protected internal bool IsFloating =>
            this.floatingCounter > 0;

        protected internal DXContentPresenter LayoutLayer { get; private set; }

        protected internal Window OwnerWindow { get; private set; }

        protected internal DevExpress.Xpf.Docking.RenameHelper RenameHelper =>
            this.renameHelperCore;

        protected internal ISerializationController SerializationController =>
            this.serializationControllerCore;

        protected internal bool ShouldRestoreOnActivate { get; set; }

        protected internal IViewAdapter ViewAdapter =>
            this.viewAdapterCore;

        protected bool IsUpdateLocked =>
            this.lockUpdateCounter > 0;

        protected override IEnumerator LogicalChildren
        {
            get
            {
                if (base.IsDisposing)
                {
                    return Enumerable.Empty<object>().GetEnumerator();
                }
                IEnumerator[] enumerators = new IEnumerator[] { new CustomizationEnumerator(this), this.logicalChildrenCore.ToList<object>().GetEnumerator(), this.internalElements.GetEnumerator() };
                return new DevExpress.Xpf.Docking.Base.MergedEnumerator(enumerators);
            }
        }

        protected bool ShouldRestoreCustomizationForm { get; set; }

        protected bool ShouldRestoreOnIsVisibleChanged { get; set; }

        protected override int VisualChildrenCount =>
            base.VisualChildrenCount + this.visualChildrenCore.Count;

        protected UIElement VisualRoot { get; private set; }

        private InputBinding CloseMDIItemCommandBinding
        {
            get
            {
                if (this.closeMDIItemCommandBinding == null)
                {
                    this.closeMDIItemCommandBinding = new InputBinding(DockControllerCommand.CloseActive, new KeyGesture(Key.F4, ModifierKeys.Control));
                    BindingHelper.SetBinding(this.closeMDIItemCommandBinding, InputBinding.CommandParameterProperty, this, ActiveMDIItemProperty, BindingMode.OneWay);
                    BindingHelper.SetBinding(this.closeMDIItemCommandBinding, InputBinding.CommandTargetProperty, this, DockLayoutManagerProperty, BindingMode.OneWay);
                }
                return this.closeMDIItemCommandBinding;
            }
        }

        private DevExpress.Xpf.Docking.DelayedActivationHelper DelayedActivationHelper
        {
            get
            {
                DevExpress.Xpf.Docking.DelayedActivationHelper delayedActivationHelper = this.delayedActivationHelper;
                if (this.delayedActivationHelper == null)
                {
                    DevExpress.Xpf.Docking.DelayedActivationHelper local1 = this.delayedActivationHelper;
                    delayedActivationHelper = this.delayedActivationHelper = new DevExpress.Xpf.Docking.DelayedActivationHelper(new DevExpress.Xpf.Docking.DelayedActivationHelper.DelayedActivationCallback(this.DoDelayedActivation));
                }
                return delayedActivationHelper;
            }
        }

        private DisableFloatingPanelTransparencyBehavior DisableTransparencyBehavior
        {
            get
            {
                this.disableTransparencyBehavior ??= new DisableFloatingPanelTransparencyBehavior();
                return this.disableTransparencyBehavior;
            }
        }

        private DockLayoutManager MergedParent { get; set; }

        private DockLayoutManagerMergingHelper MergingHelper =>
            this.mergingHelper;

        private DocumentPanel MergingTarget
        {
            get => 
                this.mergingTarget;
            set
            {
                if (!Equals(this.mergingTarget, value))
                {
                    if (this.mergingTarget != null)
                    {
                        this.mergingTarget.IsMergingTarget = false;
                    }
                    this.mergingTarget = value;
                    if (this.mergingTarget != null)
                    {
                        this.mergingTarget.IsMergingTarget = true;
                    }
                }
            }
        }

        private Point SavedRestoreOffset
        {
            get
            {
                if (this.effectiveRestoreOffset != null)
                {
                    return this.effectiveRestoreOffset.Value;
                }
                return new Point();
            }
        }

        private Window TopLevelWindow
        {
            get => 
                this.topLevelWindow;
            set
            {
                if (!ReferenceEquals(this.topLevelWindow, value))
                {
                    this.topLevelWindow = value;
                    if (this.topLevelWindow != null)
                    {
                        this.OnTopLevelWindowChanged();
                    }
                }
            }
        }

        private ThemeTreeWalkerHelper TreeWalkerHelper
        {
            get
            {
                this.treeWalkerHelper ??= new ThemeTreeWalkerHelper(this);
                return this.treeWalkerHelper;
            }
        }

        internal bool CanShowFloatGroup { get; set; }

        IUIElement IUIElement.Scope =>
            null;

        UIChildren IUIElement.Children
        {
            get
            {
                this.uiChildren ??= new UIChildren();
                return this.uiChildren;
            }
        }

        bool ISupportBatchUpdate.IsUpdatedLocked =>
            this.IsUpdateLocked;

        bool ILogicalOwner.IsLoaded =>
            base.IsLoaded;

        double ILogicalOwner.ActualWidth =>
            base.ActualWidth;

        double ILogicalOwner.ActualHeight =>
            base.ActualHeight;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DockLayoutManager.<>c <>9 = new DockLayoutManager.<>c();
            public static Func<bool> <>9__105_1;
            public static PropertyChangedEventHandler <>9__182_0;
            public static PropertyChangedEventHandler <>9__182_1;
            public static Func<BaseLayoutItem, string> <>9__394_1;
            public static Func<DockLayoutManager, IEnumerable<string>> <>9__394_0;
            public static Action<DispatcherOperation> <>9__517_0;
            public static Func<object, bool> <>9__529_0;
            public static Action<DispatcherOperation> <>9__531_0;
            public static VisitDelegate<FloatGroup> <>9__565_0;
            public static Action<FloatGroup> <>9__616_0;
            public static Action<DockLayoutManagerMergingHelper> <>9__617_1;
            public static Action<LayoutItemsControl> <>9__628_0;
            public static Predicate<DependencyObject> <>9__629_0;
            public static Predicate<DependencyObject> <>9__629_1;
            public static Func<FloatGroup, FloatingWindowLock> <>9__655_0;
            public static Action<FloatingWindowLock> <>9__655_1;
            public static Func<FloatGroup, FloatingWindowLock> <>9__655_2;
            public static Action<FloatingWindowLock> <>9__655_3;
            public static Action<FloatGroup> <>9__657_0;
            public static Action<DockLayoutManagerMergingHelper> <>9__673_0;
            public static Func<DockLayoutManager, bool> <>9__674_0;
            public static Func<bool> <>9__674_1;
            public static Func<DockLayoutManager, AutoHideGroupCollection> <>9__674_2;
            public static Action<FloatGroup> <>9__676_0;
            public static Action<FloatGroup> <>9__678_0;
            public static Func<FloatGroup, int> <>9__684_0;
            public static Action<FloatingWindowLock> <>9__684_1;
            public static Action<FloatingWindowLock> <>9__684_2;
            public static Func<BaseLayoutItem, bool> <>9__697_0;
            public static Func<BaseLayoutItem, bool> <>9__697_1;

            internal void <.cctor>b__89_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((DockLayoutManager) dObj).OnOwnerWindowTitleChanged();
            }

            internal object <.cctor>b__89_1(DependencyObject dObj, object value) => 
                ((DockLayoutManager) dObj).CoerceWindowTitleFormat((string) value);

            internal void <.cctor>b__89_10(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((DockLayoutManager) dObj).OnAllowCustomizationChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__89_11(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((DockLayoutManager) dObj).OnAllowDocumentSelectorChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__89_12(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((DockLayoutManager) dObj).OnIsCustomizationChanged((bool) e.NewValue);
            }

            internal object <.cctor>b__89_13(DependencyObject dObj, object value) => 
                ((DockLayoutManager) dObj).CoerceIsCustomization((bool) value);

            internal object <.cctor>b__89_14(DependencyObject dObj, object value) => 
                ((DockLayoutManager) dObj).CoerceIsRenaming((bool) value);

            internal void <.cctor>b__89_15(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((DockLayoutManager) dObj).OnShowInvisibleItemsChanged((bool?) e.NewValue);
            }

            internal void <.cctor>b__89_16(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((DockLayoutManager) dObj).OnShowInvisibleItemsInCustomizationFormChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__89_17(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((DockLayoutManager) dObj).OnMDIMergeStyleChanged((MDIMergeStyle) e.OldValue, (MDIMergeStyle) e.NewValue);
            }

            internal void <.cctor>b__89_18(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((DockLayoutManager) dObj).OnItemsSourceChanged((IEnumerable) e.OldValue, (IEnumerable) e.NewValue, e);
            }

            internal void <.cctor>b__89_19(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((DockLayoutManager) dObj).OnIsSynchronizedWithCurrentItemChanged((bool) e.OldValue, (bool) e.NewValue);
            }

            internal void <.cctor>b__89_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DockLayoutManager) d).InvokeUpdateFloatingPaneResources();
            }

            internal void <.cctor>b__89_20(DockLayoutManager d, LogicalTreeStructure oldValue, LogicalTreeStructure newValue)
            {
                d.OnLogicalTreeStructureChanged(oldValue, newValue);
            }

            internal void <.cctor>b__89_21(DockLayoutManager d, bool oldValue, bool newValue)
            {
                d.OnHandleHwndHostMouseEventsChanged(oldValue);
            }

            internal void <.cctor>b__89_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DockLayoutManager) d).OnEnableWin32CompatibilityChanged((bool) e.OldValue, (bool) e.NewValue);
            }

            internal void <.cctor>b__89_4(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DockLayoutManager) d).OnOwnsFloatWindowsChanged((bool) e.OldValue, (bool) e.NewValue);
            }

            internal void <.cctor>b__89_5(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((DockLayoutManager) dObj).OnClosedItemsBarVisibilityChanged((ClosedPanelsBarVisibility) e.NewValue);
            }

            internal void <.cctor>b__89_6(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DockLayoutManager) d).OnAutoHideModeChanged((AutoHideMode) e.OldValue, (AutoHideMode) e.NewValue);
            }

            internal void <.cctor>b__89_7(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((DockLayoutManager) dObj).OnActiveDockItemChanged((BaseLayoutItem) e.OldValue, (BaseLayoutItem) e.NewValue);
            }

            internal void <.cctor>b__89_8(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((DockLayoutManager) dObj).OnActiveLayoutItemChanged((BaseLayoutItem) e.OldValue, (BaseLayoutItem) e.NewValue);
            }

            internal void <.cctor>b__89_9(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((DockLayoutManager) dObj).OnActiveMDIItemChanged((BaseLayoutItem) e.OldValue, (BaseLayoutItem) e.NewValue);
            }

            internal void <.ctor>b__182_0(object <sender>, PropertyChangedEventArgs <e>)
            {
            }

            internal void <.ctor>b__182_1(object <sender>, PropertyChangedEventArgs <e>)
            {
            }

            internal FloatingWindowLock <CheckFloatGroupRestoreBounds>b__655_0(FloatGroup x) => 
                x.FloatingWindowLock;

            internal void <CheckFloatGroupRestoreBounds>b__655_1(FloatingWindowLock x)
            {
                x.Lock(FloatingWindowLock.LockerKey.CheckFloatBounds);
            }

            internal FloatingWindowLock <CheckFloatGroupRestoreBounds>b__655_2(FloatGroup x) => 
                x.FloatingWindowLock;

            internal void <CheckFloatGroupRestoreBounds>b__655_3(FloatingWindowLock x)
            {
                x.Unlock(FloatingWindowLock.LockerKey.CheckFloatBounds);
            }

            internal void <CheckFloatGroupsFloatState>b__657_0(FloatGroup x)
            {
                x.CheckFloatState();
            }

            internal IEnumerable<string> <get_LinkedItemNames>b__394_0(DockLayoutManager x)
            {
                Func<BaseLayoutItem, string> selector = <>9__394_1;
                if (<>9__394_1 == null)
                {
                    Func<BaseLayoutItem, string> local1 = <>9__394_1;
                    selector = <>9__394_1 = y => y.Name;
                }
                return x.GetItems().Select<BaseLayoutItem, string>(selector);
            }

            internal string <get_LinkedItemNames>b__394_1(BaseLayoutItem y) => 
                y.Name;

            internal void <InvokeUpdateFloatingPaneResources>b__517_0(DispatcherOperation x)
            {
                x.Abort();
            }

            internal void <OnAllowMergingAutoHidePanelsChanged>b__673_0(DockLayoutManagerMergingHelper x)
            {
                x.Changed();
            }

            internal bool <OnAutoHideGroupsCollectionChanged>b__674_0(DockLayoutManager x) => 
                x.AllowMergingAutoHidePanels;

            internal bool <OnAutoHideGroupsCollectionChanged>b__674_1() => 
                false;

            internal AutoHideGroupCollection <OnAutoHideGroupsCollectionChanged>b__674_2(DockLayoutManager x) => 
                x.AutoHideGroups;

            internal void <OnEnableWin32CompatibilityChanged>b__676_0(FloatGroup x)
            {
                x.CoerceValue(FloatGroup.IsOpenProperty);
            }

            internal void <OnIsEnabledChanged>b__616_0(FloatGroup x)
            {
                x.CoerceValue(UIElement.IsEnabledProperty);
            }

            internal void <OnIsVisibleChanged>b__617_1(DockLayoutManagerMergingHelper x)
            {
                x.Changed();
            }

            internal bool <OnLayoutItemChanged>b__105_1() => 
                false;

            internal void <OnSystemEventsDisplaySettingsChanged>b__678_0(FloatGroup x)
            {
                x.CoerceValue(BaseLayoutItem.FloatSizeProperty);
            }

            internal void <OwnerWindowClosed>b__628_0(LayoutItemsControl x)
            {
                x.PrepareForWindowClosing();
            }

            internal bool <OwnerWindowPreviewKeyDown>b__629_0(DependencyObject fe) => 
                (fe is DockLayoutManager) && ((DockLayoutManager) fe).AllowDocumentSelector;

            internal bool <OwnerWindowPreviewKeyDown>b__629_1(DependencyObject fe) => 
                fe is DockLayoutManager;

            internal bool <PurgeLogicalChildren>b__529_0(object x) => 
                x is BaseLayoutItem;

            internal void <RaiseDockItemActivatedEvent>b__531_0(DispatcherOperation x)
            {
                x.Abort();
            }

            internal void <RequestCheckFloatGroupRestoreBounds>b__565_0(FloatGroup x)
            {
                x.FloatStateLockHelper.Lock();
            }

            internal int <RestoreFloatGroupOrder>b__684_0(FloatGroup x) => 
                x.GetSerializableZOrder();

            internal void <RestoreFloatGroupOrder>b__684_1(FloatingWindowLock x)
            {
                x.Lock(FloatingWindowLock.LockerKey.Focus);
            }

            internal void <RestoreFloatGroupOrder>b__684_2(FloatingWindowLock x)
            {
                x.Unlock(FloatingWindowLock.LockerKey.Focus);
            }

            internal bool <UpdateMergingTarget>b__697_0(BaseLayoutItem x) => 
                x is DocumentPanel;

            internal bool <UpdateMergingTarget>b__697_1(BaseLayoutItem x) => 
                ((DocumentPanel) x).CanMerge;
        }

        private class ContainerGenerator
        {
            private readonly Dictionary<DependencyObject, IGeneratorHost> containerHash = new Dictionary<DependencyObject, IGeneratorHost>();
            private readonly Dictionary<object, DependencyObject> itemsHash = new Dictionary<object, DependencyObject>();

            public virtual void ClearContainerForItem(object item)
            {
                if (this.itemsHash.ContainsKey(item))
                {
                    DependencyObject key = this.itemsHash[item];
                    if (this.containerHash.ContainsKey(key))
                    {
                        IGeneratorHost host = this.containerHash[key];
                        if (host != null)
                        {
                            host.ClearContainer(key, item);
                        }
                        this.containerHash.Remove(key);
                    }
                    this.itemsHash.Remove(item);
                }
            }

            public DependencyObject GetContainerForItem(object item)
            {
                DependencyObject obj2;
                this.itemsHash.TryGetValue(item, out obj2);
                return obj2;
            }

            public DependencyObject GetContainerForItem(IGeneratorHost generatorHost, object item, DataTemplate itemTemplate, DataTemplateSelector itemTemplateSelector)
            {
                if (generatorHost == null)
                {
                    return null;
                }
                DependencyObject obj2 = generatorHost.GenerateContainerForItem(item, itemTemplate, itemTemplateSelector);
                this.itemsHash.Add(item, obj2);
                this.containerHash.Add(obj2, generatorHost);
                return obj2;
            }

            public object GetItemForContainer(DependencyObject container) => 
                this.itemsHash.ContainsValue(container) ? this.itemsHash.GetKeyByValue<object, DependencyObject>(container) : null;

            public IEnumerable GetItems() => 
                this.itemsHash.Keys.ToList<object>();

            public bool IsContainerHost(IGeneratorHost generatorHost) => 
                this.containerHash.ContainsValue(generatorHost);

            public DependencyObject LinkContainerToItem(object oldValue, object newValue, int newStartingIndex, DataTemplate itemTemplate, DataTemplateSelector itemTemplateSelector)
            {
                DependencyObject key = this.itemsHash[oldValue];
                if (this.containerHash.ContainsKey(key))
                {
                    IGeneratorHost host = this.containerHash[key];
                    if (host != null)
                    {
                        this.containerHash.Remove(key);
                        this.itemsHash.Remove(oldValue);
                        DependencyObject obj3 = host.LinkContainerToItem(key, newValue, itemTemplate, itemTemplateSelector);
                        this.itemsHash.Add(newValue, obj3);
                        this.containerHash.Add(obj3, host);
                        return obj3;
                    }
                }
                return null;
            }

            public void Reset()
            {
                foreach (object obj2 in this.itemsHash.Keys.ToArray<object>())
                {
                    this.ClearContainerForItem(obj2);
                }
                this.itemsHash.Clear();
                this.containerHash.Clear();
            }
        }

        private class DecomposedItemsCollection : LayoutGroupCollection
        {
            private readonly DockLayoutManager Owner;

            public DecomposedItemsCollection(DockLayoutManager owner)
            {
                this.Owner = owner;
            }

            protected override void InsertItem(int index, LayoutGroup item)
            {
                if (this.Owner != null)
                {
                    item.Manager ??= this.Owner;
                    DockLayoutManager.AddLogicalChild(this.Owner, item);
                    item.DockLayoutManagerCore = this.Owner;
                }
                base.InsertItem(index, item);
            }
        }

        private class DisableFloatingPanelTransparencyBehavior : Behavior<DockLayoutManager>
        {
            private ResourceDictionary floatPaneStyles;

            protected override void OnAttached()
            {
                base.OnAttached();
                Uri uri = base.AssociatedObject.ValuesProvider.Win32ResourcePath;
                if (uri != null)
                {
                    this.FloatPaneStyles.Source = uri;
                    if (!base.AssociatedObject.Resources.MergedDictionaries.Contains(this.FloatPaneStyles))
                    {
                        base.AssociatedObject.Resources.MergedDictionaries.Add(this.FloatPaneStyles);
                    }
                }
            }

            protected override void OnDetaching()
            {
                if (base.AssociatedObject != null)
                {
                    base.AssociatedObject.Resources.MergedDictionaries.Remove(this.FloatPaneStyles);
                }
                base.OnDetaching();
            }

            private ResourceDictionary FloatPaneStyles
            {
                get
                {
                    ResourceDictionary floatPaneStyles = this.floatPaneStyles;
                    if (this.floatPaneStyles == null)
                    {
                        ResourceDictionary local1 = this.floatPaneStyles;
                        floatPaneStyles = this.floatPaneStyles = new ResourceDictionary();
                    }
                    return floatPaneStyles;
                }
            }
        }

        private class DisplaySettingsListener : IDisposable
        {
            private readonly WeakReference containerRef;
            private bool isDisposed;

            public DisplaySettingsListener(DockLayoutManager container)
            {
                this.containerRef = new WeakReference(container);
                try
                {
                    SystemEvents.DisplaySettingsChanged += new EventHandler(this.SystemEventsOnDisplaySettingsChanged);
                }
                catch
                {
                }
            }

            public void Dispose()
            {
                if (!this.isDisposed)
                {
                    this.isDisposed = true;
                    try
                    {
                        SystemEvents.DisplaySettingsChanged -= new EventHandler(this.SystemEventsOnDisplaySettingsChanged);
                    }
                    catch
                    {
                    }
                }
                GC.SuppressFinalize(this);
            }

            private void SystemEventsOnDisplaySettingsChanged(object sender, EventArgs e)
            {
                DockLayoutManager target = this.containerRef.Target as DockLayoutManager;
                if (target != null)
                {
                    target.OnSystemEventsDisplaySettingsChanged(sender, e);
                }
                else
                {
                    this.Dispose();
                }
            }
        }

        private class FocusLocker
        {
            private readonly Locker _focusLocker = new Locker();
            private readonly List<BaseLayoutItem> lockedItems = new List<BaseLayoutItem>();

            private void AddItem(BaseLayoutItem item)
            {
                this.lockedItems.Remove(item);
                this.lockedItems.Add(item);
            }

            private void FocusItem(BaseLayoutItem item)
            {
                Action<LayoutPanel> action = <>c.<>9__6_0;
                if (<>c.<>9__6_0 == null)
                {
                    Action<LayoutPanel> local1 = <>c.<>9__6_0;
                    action = <>c.<>9__6_0 = x => x.ProcessFocus();
                }
                (item as LayoutPanel).Do<LayoutPanel>(action);
            }

            public void Lock()
            {
                this._focusLocker.Lock();
            }

            private void OnUnlock()
            {
                Func<BaseLayoutItem, bool> predicate = <>c.<>9__7_0;
                if (<>c.<>9__7_0 == null)
                {
                    Func<BaseLayoutItem, bool> local1 = <>c.<>9__7_0;
                    predicate = <>c.<>9__7_0 = x => (!x.IsTabPage || x.IsSelectedItem) ? x.IsInTree() : false;
                }
                List<BaseLayoutItem> source = this.lockedItems.Where<BaseLayoutItem>(predicate).ToList<BaseLayoutItem>();
                this.lockedItems.Clear();
                Func<BaseLayoutItem, bool> func2 = <>c.<>9__7_1;
                if (<>c.<>9__7_1 == null)
                {
                    Func<BaseLayoutItem, bool> local2 = <>c.<>9__7_1;
                    func2 = <>c.<>9__7_1 = x => x.IsKeyboardFocusWithin || x.IsKeyboardFocused;
                }
                BaseLayoutItem item = source.FirstOrDefault<BaseLayoutItem>(func2);
                if (item == null)
                {
                    item = source.LastOrDefault<BaseLayoutItem>();
                }
                this.FocusItem(item);
            }

            public void QueueFocus(BaseLayoutItem item)
            {
                if (this._focusLocker)
                {
                    this.AddItem(item);
                }
                else
                {
                    this.FocusItem(item);
                }
            }

            public void Unlock()
            {
                this._focusLocker.Unlock();
                if (!this._focusLocker)
                {
                    this.OnUnlock();
                }
            }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly DockLayoutManager.FocusLocker.<>c <>9 = new DockLayoutManager.FocusLocker.<>c();
                public static Action<LayoutPanel> <>9__6_0;
                public static Func<BaseLayoutItem, bool> <>9__7_0;
                public static Func<BaseLayoutItem, bool> <>9__7_1;

                internal void <FocusItem>b__6_0(LayoutPanel x)
                {
                    x.ProcessFocus();
                }

                internal bool <OnUnlock>b__7_0(BaseLayoutItem x) => 
                    (!x.IsTabPage || x.IsSelectedItem) ? x.IsInTree() : false;

                internal bool <OnUnlock>b__7_1(BaseLayoutItem x) => 
                    x.IsKeyboardFocusWithin || x.IsKeyboardFocused;
            }
        }

        private enum LayoutItemState
        {
            Close,
            AutoHide
        }

        private class LayoutItemStateHelper
        {
            private readonly DockLayoutManager Owner;

            public LayoutItemStateHelper(DockLayoutManager owner)
            {
                this.Owner = owner;
            }

            public void QueueCheckLayoutItemState(BaseLayoutItem item, DockLayoutManager.LayoutItemState state)
            {
                Action<BaseLayoutItem, DockLayoutManager.LayoutItemState> method = new Action<BaseLayoutItem, DockLayoutManager.LayoutItemState>(this.UpdateLayoutItemState);
                if (!this.Owner.GetIsLogicalChildrenIterationInProgress() && !item.IsLayoutTreeChangeInProgress)
                {
                    method(item, state);
                }
                else
                {
                    object[] args = new object[] { item, state };
                    this.Owner.Dispatcher.BeginInvoke(method, args);
                }
            }

            private void UpdateAutoHiddenState(BaseLayoutItem item)
            {
                LayoutPanel panel = item as LayoutPanel;
                if (panel != null)
                {
                    if (!panel.AutoHidden)
                    {
                        this.Owner.DockController.Dock(item);
                    }
                    else if (item.Parent != null)
                    {
                        this.Owner.DockController.Hide(item);
                    }
                    item.SetAutoHidden(item.IsAutoHidden);
                }
            }

            private void UpdateClosedState(BaseLayoutItem item)
            {
                if (!item.Closed)
                {
                    this.Owner.DockController.Restore(item);
                }
                else if (item.Parent != null)
                {
                    item.SetCurrentValue(BaseLayoutItem.ClosedProperty, this.Owner.DockController.Close(item));
                }
            }

            private void UpdateLayoutItemState(BaseLayoutItem item, DockLayoutManager.LayoutItemState state)
            {
                if (state == DockLayoutManager.LayoutItemState.Close)
                {
                    this.UpdateClosedState(item);
                }
                else if (state == DockLayoutManager.LayoutItemState.AutoHide)
                {
                    this.UpdateAutoHiddenState(item);
                }
            }
        }

        private class LogicalChildrenCollection : List<object>
        {
            public void AddOnce(object item)
            {
                if (!base.Contains(item))
                {
                    base.Add(item);
                }
            }
        }

        private class ThemeTreeWalkerHelper
        {
            private readonly DependencyObject Owner;
            private WeakReference walker;

            public ThemeTreeWalkerHelper(DependencyObject owner)
            {
                this.Owner = owner;
            }

            public bool IsThemeChanging()
            {
                bool flag = false;
                string themeName = null;
                if (this.walker != null)
                {
                    ThemeTreeWalker target = this.walker.Target as ThemeTreeWalker;
                    if (target != null)
                    {
                        themeName = target.ThemeName;
                    }
                }
                ThemeTreeWalker treeWalker = ThemeManager.GetTreeWalker(this.Owner);
                if (treeWalker == null)
                {
                    this.walker = null;
                }
                else if (treeWalker.ThemeName != themeName)
                {
                    flag = true;
                    this.walker = new WeakReference(treeWalker);
                }
                return flag;
            }
        }
    }
}

