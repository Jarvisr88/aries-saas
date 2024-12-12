namespace DevExpress.Xpf.Docking
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Utils.Serializing;
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Core.Serialization;
    using DevExpress.Xpf.Docking.Base;
    using DevExpress.Xpf.Docking.Internal;
    using DevExpress.Xpf.Docking.UIAutomation;
    using DevExpress.Xpf.Docking.VisualElements;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
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
    using System.Windows.Media;
    using System.Windows.Threading;

    [TemplateVisualState(Name="Active", GroupName="ActiveStates"), TemplateVisualState(Name="Inactive", GroupName="ActiveStates")]
    public abstract class BaseLayoutItem : psvFrameworkElement, ISerializableItem, ISupportHierarchy<BaseLayoutItem>, ISupportVisitor<BaseLayoutItem>, IUIElement
    {
        protected static BaseLayoutItem[] EmptyNodes = new BaseLayoutItem[0];
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty ActualAppearanceObjectProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty ActualAppearanceProperty;
        public static readonly DependencyProperty ActualCaptionProperty;
        public static readonly DependencyProperty ActualCaptionWidthProperty;
        public static readonly DependencyProperty ActualMarginProperty;
        public static readonly DependencyProperty ActualMaxSizeProperty;
        public static readonly DependencyProperty ActualMinSizeProperty;
        public static readonly DependencyProperty ActualPaddingProperty;
        public static readonly DependencyProperty ActualTabCaptionProperty;
        public static readonly DependencyProperty AllowActivateProperty;
        public static readonly DependencyProperty AllowCloseProperty;
        public static readonly DependencyProperty AllowContextMenuProperty;
        public static readonly DependencyProperty AllowDockProperty;
        public static readonly DependencyProperty AllowDockToCurrentItemProperty;
        public static readonly DependencyProperty AllowDragProperty;
        public static readonly DependencyProperty AllowFloatProperty;
        public static readonly DependencyProperty AllowHideProperty;
        public static readonly DependencyProperty AllowMaximizeProperty;
        public static readonly DependencyProperty AllowMinimizeProperty;
        public static readonly DependencyProperty AllowMoveProperty;
        public static readonly DependencyProperty AllowRenameProperty;
        public static readonly DependencyProperty AllowRestoreProperty;
        public static readonly DependencyProperty AllowSelectionProperty;
        public static readonly DependencyProperty AllowSizingProperty;
        public static readonly DependencyProperty AppearanceProperty;
        public static readonly DependencyProperty BindableNameProperty;
        public static readonly DependencyProperty CaptionAlignModeProperty;
        public static readonly DependencyProperty CaptionFormatProperty;
        public static readonly DependencyProperty CaptionHorizontalAlignmentProperty;
        public static readonly DependencyProperty CaptionImageLocationProperty;
        public static readonly DependencyProperty CaptionImageProperty;
        public static readonly DependencyProperty CaptionLocationProperty;
        public static readonly DependencyProperty CaptionProperty;
        public static readonly DependencyProperty CaptionTemplateProperty;
        public static readonly DependencyProperty CaptionTemplateSelectorProperty;
        public static readonly DependencyProperty CaptionVerticalAlignmentProperty;
        public static readonly DependencyProperty CaptionWidthProperty;
        public static readonly DependencyProperty CloseCommandParameterProperty;
        public static readonly DependencyProperty CloseCommandProperty;
        public static readonly DependencyProperty ClosedProperty;
        public static readonly DependencyProperty ClosingBehaviorProperty;
        public static readonly DependencyProperty ContextMenuCustomizationsTemplateProperty;
        public static readonly DependencyProperty ControlBoxContentProperty;
        public static readonly DependencyProperty ControlBoxContentTemplateProperty;
        public static readonly DependencyProperty CustomizationCaptionProperty;
        public static readonly DependencyProperty DescriptionProperty;
        public static readonly DependencyProperty DesiredCaptionWidthProperty;
        public static readonly DependencyProperty FloatOnDoubleClickProperty;
        public static readonly DependencyProperty FloatSizeProperty;
        public static readonly DependencyProperty FooterDescriptionProperty;
        public static readonly DependencyProperty HasCaptionProperty;
        public static readonly DependencyProperty HasCaptionTemplateProperty;
        public static readonly DependencyProperty HasDesiredCaptionWidthProperty;
        public static readonly DependencyProperty HasImageProperty;
        public static readonly DependencyProperty HasTabCaptionProperty;
        public static readonly DependencyProperty HeaderBarContainerControlAllowDropProperty;
        public static readonly DependencyProperty HeaderBarContainerControlNameProperty;
        public static readonly DependencyProperty ImageToTextDistanceProperty;
        public static readonly DependencyProperty IsActiveProperty;
        public static readonly DependencyProperty IsCaptionImageVisibleProperty;
        public static readonly DependencyProperty IsCaptionVisibleProperty;
        public static readonly DependencyProperty IsCloseButtonVisibleProperty;
        public static readonly DependencyProperty IsClosedProperty;
        public static readonly DependencyProperty IsControlBoxVisibleProperty;
        public static readonly DependencyProperty IsControlItemsHostProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty IsDropDownButtonVisibleProperty;
        public static readonly DependencyProperty IsFloatingRootItemProperty;
        public static readonly DependencyProperty IsHiddenProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty IsMaximizeButtonVisibleProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty IsMinimizeButtonVisibleProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty IsPinButtonVisibleProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty IsRestoreButtonVisibleProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty IsScrollNextButtonVisibleProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty IsScrollPrevButtonVisibleProperty;
        public static readonly DependencyProperty IsSelectedItemProperty;
        public static readonly DependencyProperty IsSelectedProperty;
        public static readonly DependencyProperty IsTabPageProperty;
        public static readonly DependencyProperty ItemHeightProperty;
        public static readonly DependencyProperty ItemWidthProperty;
        public static readonly DependencyProperty LayoutSizeProperty;
        public static readonly DependencyProperty MarginProperty;
        public static readonly DependencyProperty PaddingProperty;
        public static readonly DependencyProperty ShowCaptionImageProperty;
        public static readonly DependencyProperty ShowCaptionProperty;
        public static readonly DependencyProperty ShowCloseButtonProperty;
        public static readonly DependencyProperty ShowControlBoxProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty ShowDropDownButtonProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty ShowMaximizeButtonProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty ShowMinimizeButtonProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty ShowPinButtonProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty ShowRestoreButtonProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty ShowScrollNextButtonProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty ShowScrollPrevButtonProperty;
        public static readonly DependencyProperty ShowTabCaptionImageProperty;
        public static readonly DependencyProperty TabCaptionFormatProperty;
        public static readonly DependencyProperty TabCaptionHorizontalAlignmentProperty;
        public static readonly DependencyProperty TabCaptionImageProperty;
        public static readonly DependencyProperty TabCaptionProperty;
        public static readonly DependencyProperty TabCaptionTemplateProperty;
        public static readonly DependencyProperty TabCaptionTemplateSelectorProperty;
        public static readonly DependencyProperty TabCaptionVerticalAlignmentProperty;
        public static readonly DependencyProperty TabCaptionWidthProperty;
        public static readonly DependencyProperty TextTrimmingProperty;
        public static readonly DependencyProperty TextWrappingProperty;
        public static readonly DependencyProperty ToolTipProperty;
        public static readonly DependencyProperty DockingViewStyleProperty;
        private static readonly DependencyPropertyKey DockingViewStylePropertyKey;
        [IgnoreDependencyPropertiesConsistencyChecker]
        internal static readonly DependencyProperty ViewStyleInternalProperty;
        internal static readonly DependencyPropertyKey ActualCaptionPropertyKey;
        internal static readonly DependencyPropertyKey ActualTabCaptionPropertyKey;
        internal static readonly DependencyPropertyKey DesiredCaptionWidthPropertyKey;
        internal static readonly DependencyPropertyKey HasDesiredCaptionWidthPropertyKey;
        internal static readonly DependencyPropertyKey IsControlItemsHostPropertyKey;
        [IgnoreDependencyPropertiesConsistencyChecker]
        internal static readonly DependencyProperty IsVisibleCoreProperty;
        private static readonly DependencyPropertyKey ActualAppearanceObjectPropertyKey;
        private static readonly DependencyPropertyKey ActualAppearancePropertyKey;
        private static readonly DependencyPropertyKey ActualCaptionWidthPropertyKey;
        private static readonly DependencyPropertyKey ActualMarginPropertyKey;
        private static readonly DependencyPropertyKey ActualMaxSizePropertyKey;
        private static readonly DependencyPropertyKey ActualMinSizePropertyKey;
        private static readonly DependencyPropertyKey ActualPaddingPropertyKey;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty DockLayoutManagerCoreProperty;
        private static readonly DependencyPropertyKey HasCaptionPropertyKey;
        private static readonly DependencyPropertyKey HasCaptionTemplatePropertyKey;
        private static readonly DependencyPropertyKey HasImagePropertyKey;
        private static readonly DependencyPropertyKey HasTabCaptionPropertyKey;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty HasTabCaptionTemplateProperty;
        private static readonly DependencyPropertyKey IsCaptionImageVisiblePropertyKey;
        private static readonly DependencyPropertyKey IsCaptionVisiblePropertyKey;
        private static readonly DependencyPropertyKey IsCloseButtonVisiblePropertyKey;
        private static readonly DependencyPropertyKey IsClosedPropertyKey;
        private static readonly DependencyPropertyKey IsControlBoxVisiblePropertyKey;
        private static readonly DependencyPropertyKey IsDropDownButtonVisiblePropertyKey;
        private static readonly DependencyPropertyKey IsFloatingRootItemPropertyKey;
        private static readonly DependencyPropertyKey IsHiddenPropertyKey;
        private static readonly DependencyPropertyKey IsMaximizeButtonVisiblePropertyKey;
        private static readonly DependencyPropertyKey IsMinimizeButtonVisiblePropertyKey;
        private static readonly DependencyPropertyKey IsPinButtonVisiblePropertyKey;
        private static readonly DependencyPropertyKey IsRestoreButtonVisiblePropertyKey;
        private static readonly DependencyPropertyKey IsScrollNextButtonVisiblePropertyKey;
        private static readonly DependencyPropertyKey IsScrollPrevButtonVisiblePropertyKey;
        private static readonly DependencyPropertyKey IsSelectedPropertyKey;
        private static readonly DependencyPropertyKey IsTabPagePropertyKey;
        private static readonly DependencyPropertyKey IsVisibleCorePropertyKey;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty LayoutItemDataProperty;
        private static readonly DependencyPropertyKey LayoutSizePropertyKey;
        [IgnoreDependencyPropertiesConsistencyChecker]
        internal static readonly DependencyProperty ActualCustomizationCaptionProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        internal static readonly DependencyProperty ActualCustomizationCaptionTemplateProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        internal static readonly DependencyProperty ActualCustomizationCaptionTemplateSelectorProperty;
        internal static readonly Size DefaultFloatSize = new Size(200.0, 200.0);
        internal WeakReference AttachedSerializationController;
        internal bool IsClosing;
        private readonly Locker activationCancelLocker = new Locker();
        private readonly Locker closeCommandLocker = new Locker();
        private readonly Locker dockingTargetLocker;
        private readonly Locker inheritablePropertiesLocker = new Locker();
        private readonly Locker isActivationCancelledLocker = new Locker();
        private readonly Locker layoutChangeLocker = new Locker();
        private readonly Locker themeChangedLocker = new Locker();
        private object _DataContext;
        private DevExpress.Xpf.Docking.Appearance _DefaultAppearance;
        private DockLayoutManager _DockLayoutManagerCached;
        private LayoutGroup _RootGroup;
        private Style _Style;
        private DispatcherOperation closedChangedDispatcherOperation;
        private ActionGroup contextMenuActionGroup;
        private PlaceHolderInfoHelper dockInfo;
        private int initCounter;
        private bool isInitializedCore;
        private bool isVisibleCore;
        private int lockActivation;
        private int lockDockItemState;
        private int lockDockItemStateCount;
        private int lockInheritablePropertiesCount;
        private LockHelper logicalTreeLockHelper;
        private DockLayoutManager managerCore;
        private LayoutGroup parentCore;
        private LockHelper parentLockHelper;
        private LockHelper resizeLockHelper;
        private UIChildren uiChildren;
        private int zIndexCore;
        internal Rect DragCursorBounds;
        private readonly List<object> logicalChildren = new List<object>();
        private UIChildren children;

        protected internal event EventHandler GeometryChanged;

        protected internal event EventHandler VisualChanged;

        static BaseLayoutItem()
        {
            DevExpress.Xpf.Docking.DependencyPropertyRegistrator<BaseLayoutItem> registrator = new DevExpress.Xpf.Docking.DependencyPropertyRegistrator<BaseLayoutItem>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.OverrideUIMetadata<SerializationProvider>(DXSerializer.SerializationProviderProperty, new BaseLayoutItemSerializationProvider(), null, null);
            registrator.OverrideFrameworkMetadata<bool>(BarNameScope.IsScopeOwnerProperty, true, null, null);
            FrameworkElement.DataContextProperty.OverrideMetadata(typeof(BaseLayoutItem), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits, (dObj, e) => ((BaseLayoutItem) dObj).OnDataContextChanged(e.NewValue), (dObj, value) => ((BaseLayoutItem) dObj).CoerceDataContext(value)));
            FrameworkElement.StyleProperty.OverrideMetadata(typeof(BaseLayoutItem), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure, (dObj, e) => ((BaseLayoutItem) dObj).OnStyleChangedOverride((Style) e.OldValue, (Style) e.NewValue), (dObj, value) => ((BaseLayoutItem) dObj).CoerceStyle((Style) value)));
            registrator.Register<DevExpress.Xpf.Docking.Appearance>("Appearance", ref AppearanceProperty, null, (dObj, e) => ((BaseLayoutItem) dObj).OnAppearanceChanged((DevExpress.Xpf.Docking.Appearance) e.OldValue, (DevExpress.Xpf.Docking.Appearance) e.NewValue), (dObj, value) => ((BaseLayoutItem) dObj).CoerceAppearance((DevExpress.Xpf.Docking.Appearance) value));
            registrator.RegisterReadonly<AppearanceObject>("ActualAppearanceObject", ref ActualAppearanceObjectPropertyKey, ref ActualAppearanceObjectProperty, null, (dObj, e) => ((BaseLayoutItem) dObj).OnActualAppearanceObjectChanged((AppearanceObject) e.NewValue), (dObj, value) => ((BaseLayoutItem) dObj).CoerceActualAppearanceObject((AppearanceObject) value));
            registrator.RegisterReadonly<DevExpress.Xpf.Docking.Appearance>("ActualAppearance", ref ActualAppearancePropertyKey, ref ActualAppearanceProperty, null, (dObj, e) => ((BaseLayoutItem) dObj).OnActualAppearanceChanged((DevExpress.Xpf.Docking.Appearance) e.OldValue, (DevExpress.Xpf.Docking.Appearance) e.NewValue), (dObj, value) => ((BaseLayoutItem) dObj).CoerceActualAppearance((DevExpress.Xpf.Docking.Appearance) value));
            registrator.Register<GridLength>("ItemWidth", ref ItemWidthProperty, new GridLength(1.0, GridUnitType.Star), (dObj, e) => ((BaseLayoutItem) dObj).OnWidthChanged((GridLength) e.NewValue, (GridLength) e.OldValue), (CoerceValueCallback) ((dObj, value) => ((BaseLayoutItem) dObj).CoerceWidth((GridLength) value)));
            registrator.Register<GridLength>("ItemHeight", ref ItemHeightProperty, new GridLength(1.0, GridUnitType.Star), (dObj, e) => ((BaseLayoutItem) dObj).OnHeightChanged((GridLength) e.NewValue, (GridLength) e.OldValue), (CoerceValueCallback) ((dObj, value) => ((BaseLayoutItem) dObj).CoerceHeight((GridLength) value)));
            registrator.RegisterReadonly<Size>("ActualMinSize", ref ActualMinSizePropertyKey, ref ActualMinSizeProperty, SizeHelper.Zero, (dObj, e) => ((BaseLayoutItem) dObj).OnActualMinSizeChanged(), (CoerceValueCallback) ((dObj, value) => ((BaseLayoutItem) dObj).CoerceActualMinSize()));
            registrator.RegisterReadonly<Size>("ActualMaxSize", ref ActualMaxSizePropertyKey, ref ActualMaxSizeProperty, new Size(double.NaN, double.NaN), (dObj, e) => ((BaseLayoutItem) dObj).OnActualMaxSizeChanged(), (CoerceValueCallback) ((dObj, value) => ((BaseLayoutItem) dObj).CoerceActualMaxSize()));
            registrator.Register<Size>("FloatSize", ref FloatSizeProperty, DefaultFloatSize, (dObj, e) => ((BaseLayoutItem) dObj).OnFloatSizeChanged((Size) e.OldValue, (Size) e.NewValue), (CoerceValueCallback) ((dObj, value) => ((BaseLayoutItem) dObj).CoerceFloatSize((Size) value)));
            Size defValue = new Size();
            registrator.RegisterReadonly<Size>("LayoutSize", ref LayoutSizePropertyKey, ref LayoutSizeProperty, defValue, null, null);
            FrameworkElement.NameProperty.OverrideMetadata(typeof(BaseLayoutItem), new FrameworkPropertyMetadata(string.Empty, (dObj, e) => ((BaseLayoutItem) dObj).OnNameChanged()));
            registrator.Register<object>("Caption", ref CaptionProperty, null, (dObj, e) => ((BaseLayoutItem) dObj).OnCaptionChanged(e.OldValue, e.NewValue), null);
            registrator.Register<string>("Description", ref DescriptionProperty, null, null, null);
            registrator.Register<string>("FooterDescription", ref FooterDescriptionProperty, null, null, null);
            registrator.Register<string>("CaptionFormat", ref CaptionFormatProperty, null, (dObj, e) => ((BaseLayoutItem) dObj).OnCaptionFormatChanged(), (CoerceValueCallback) ((dObj, value) => ((BaseLayoutItem) dObj).CoerceCaptionFormat((string) value)));
            registrator.Register<string>("CustomizationCaption", ref CustomizationCaptionProperty, null, (dObj, e) => ((BaseLayoutItem) dObj).OnCustomizationCaptionChanged((string) e.OldValue, (string) e.NewValue), (CoerceValueCallback) ((dObj, value) => ((BaseLayoutItem) dObj).CoerceCustomizationCaption((string) value)));
            registrator.Register<DevExpress.Xpf.Docking.CaptionLocation>("CaptionLocation", ref CaptionLocationProperty, DevExpress.Xpf.Docking.CaptionLocation.Default, (dObj, e) => ((BaseLayoutItem) dObj).OnCaptionLocationChanged((DevExpress.Xpf.Docking.CaptionLocation) e.NewValue), null);
            registrator.Register<DevExpress.Xpf.Docking.CaptionAlignMode>("CaptionAlignMode", ref CaptionAlignModeProperty, DevExpress.Xpf.Docking.CaptionAlignMode.Default, (dObj, e) => ((BaseLayoutItem) dObj).OnCaptionAlignModeChanged((DevExpress.Xpf.Docking.CaptionAlignMode) e.OldValue, (DevExpress.Xpf.Docking.CaptionAlignMode) e.NewValue), (CoerceValueCallback) ((dObj, value) => ((BaseLayoutItem) dObj).CoerceCaptionAlignMode((DevExpress.Xpf.Docking.CaptionAlignMode) value)));
            registrator.Register<double>("CaptionWidth", ref CaptionWidthProperty, double.NaN, (dObj, e) => ((BaseLayoutItem) dObj).OnCaptionWidthChanged((double) e.NewValue), (CoerceValueCallback) ((dObj, value) => ((BaseLayoutItem) dObj).CoerceCaptionWidth((double) value)));
            registrator.Register<ImageSource>("CaptionImage", ref CaptionImageProperty, null, (dObj, e) => ((BaseLayoutItem) dObj).OnCaptionImageChanged((ImageSource) e.NewValue), (CoerceValueCallback) ((dObj, value) => ((BaseLayoutItem) dObj).CoerceCaptionImage((ImageSource) value)));
            registrator.Register<ImageLocation>("CaptionImageLocation", ref CaptionImageLocationProperty, ImageLocation.Default, null, null);
            registrator.Register<double>("ImageToTextDistance", ref ImageToTextDistanceProperty, 3.0, null, null);
            registrator.Register<HorizontalAlignment>("CaptionHorizontalAlignment", ref CaptionHorizontalAlignmentProperty, HorizontalAlignment.Left, null, null);
            registrator.Register<VerticalAlignment>("CaptionVerticalAlignment", ref CaptionVerticalAlignmentProperty, VerticalAlignment.Center, null, null);
            registrator.RegisterReadonly<string>("ActualCaption", ref ActualCaptionPropertyKey, ref ActualCaptionProperty, null, (dObj, ea) => ((BaseLayoutItem) dObj).OnActualCaptionChanged((string) ea.NewValue), (CoerceValueCallback) ((dObj, value) => ((BaseLayoutItem) dObj).CoerceActualCaption((string) value)));
            registrator.RegisterReadonly<double>("DesiredCaptionWidth", ref DesiredCaptionWidthPropertyKey, ref DesiredCaptionWidthProperty, double.NaN, (dObj, e) => ((BaseLayoutItem) dObj).OnDesiredCaptionWidthChanged((double) e.NewValue), null);
            registrator.RegisterReadonly<bool>("HasDesiredCaptionWidth", ref HasDesiredCaptionWidthPropertyKey, ref HasDesiredCaptionWidthProperty, false, null, null);
            registrator.RegisterReadonly<double>("ActualCaptionWidth", ref ActualCaptionWidthPropertyKey, ref ActualCaptionWidthProperty, double.NaN, null, (CoerceValueCallback) ((dObj, value) => ((BaseLayoutItem) dObj).CoerceActualCaptionWidth((double) value)));
            registrator.Register<string>("HeaderBarContainerControlName", ref HeaderBarContainerControlNameProperty, null, null, null);
            bool? nullable = null;
            registrator.Register<bool?>("HeaderBarContainerControlAllowDrop", ref HeaderBarContainerControlAllowDropProperty, nullable, null, null);
            registrator.Register<Thickness>("Margin", ref MarginProperty, new Thickness(double.NaN), (dObj, e) => ((BaseLayoutItem) dObj).OnMarginChanged((Thickness) e.NewValue), null);
            registrator.Register<Thickness>("Padding", ref PaddingProperty, new Thickness(double.NaN), (dObj, e) => ((BaseLayoutItem) dObj).OnPaddingChanged((Thickness) e.NewValue), null);
            Thickness thickness = new Thickness();
            registrator.RegisterReadonly<Thickness>("ActualMargin", ref ActualMarginPropertyKey, ref ActualMarginProperty, thickness, (dObj, e) => ((BaseLayoutItem) dObj).OnActualMarginChanged((Thickness) e.NewValue), (CoerceValueCallback) ((dObj, value) => ((BaseLayoutItem) dObj).CoerceActualMargin((Thickness) value)));
            thickness = new Thickness();
            registrator.RegisterReadonly<Thickness>("ActualPadding", ref ActualPaddingPropertyKey, ref ActualPaddingProperty, thickness, null, (CoerceValueCallback) ((dObj, value) => ((BaseLayoutItem) dObj).CoerceActualPadding((Thickness) value)));
            registrator.Register<bool>("IsActive", ref IsActiveProperty, false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (dObj, e) => ((BaseLayoutItem) dObj).OnIsActiveChanged((bool) e.NewValue), null);
            registrator.Register<bool>("Closed", ref ClosedProperty, false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (dObj, e) => ((BaseLayoutItem) dObj).OnClosedChanged((bool) e.OldValue, (bool) e.NewValue), null);
            registrator.RegisterReadonly<bool>("IsClosed", ref IsClosedPropertyKey, ref IsClosedProperty, false, (d, e) => ((BaseLayoutItem) d).OnIsClosedChanged((bool) e.NewValue), null);
            registrator.RegisterReadonly<bool>("IsHidden", ref IsHiddenPropertyKey, ref IsHiddenProperty, false, null, null);
            registrator.RegisterReadonly<bool>("HasImage", ref HasImagePropertyKey, ref HasImageProperty, false, null, null);
            registrator.RegisterReadonly<bool>("HasCaption", ref HasCaptionPropertyKey, ref HasCaptionProperty, false, (dObj, ea) => ((BaseLayoutItem) dObj).OnHasCaptionChanged((bool) ea.NewValue), null);
            registrator.RegisterReadonly<bool>("HasCaptionTemplate", ref HasCaptionTemplatePropertyKey, ref HasCaptionTemplateProperty, false, (dObj, ea) => ((BaseLayoutItem) dObj).OnHasCaptionTemplateChanged((bool) ea.NewValue), null);
            registrator.Register<bool>("AllowActivate", ref AllowActivateProperty, true, null, null);
            registrator.Register<bool>("AllowSelection", ref AllowSelectionProperty, true, null, null);
            registrator.Register<bool>("AllowDrag", ref AllowDragProperty, true, null, null);
            registrator.Register<bool>("AllowFloat", ref AllowFloatProperty, true, null, null);
            registrator.Register<bool>("AllowDock", ref AllowDockProperty, true, (dObj, e) => ((BaseLayoutItem) dObj).OnAllowDockChanged((bool) e.NewValue), null);
            registrator.Register<bool>("AllowHide", ref AllowHideProperty, true, (dObj, e) => ((BaseLayoutItem) dObj).OnAllowHideChanged((bool) e.NewValue), null);
            registrator.Register<bool>("AllowClose", ref AllowCloseProperty, true, (dObj, e) => ((BaseLayoutItem) dObj).OnAllowCloseChanged((bool) e.NewValue), null);
            registrator.Register<bool>("AllowRestore", ref AllowRestoreProperty, true, (dObj, e) => ((BaseLayoutItem) dObj).OnAllowRestoreChanged((bool) e.NewValue), null);
            registrator.Register<bool>("AllowMove", ref AllowMoveProperty, true, null, null);
            registrator.Register<bool>("AllowRename", ref AllowRenameProperty, true, null, null);
            registrator.Register<bool>("AllowContextMenu", ref AllowContextMenuProperty, true, null, null);
            registrator.Register<bool>("AllowMaximize", ref AllowMaximizeProperty, true, (dObj, e) => ((BaseLayoutItem) dObj).OnAllowMaximizeChanged((bool) e.NewValue), null);
            registrator.Register<bool>("AllowMinimize", ref AllowMinimizeProperty, true, (dObj, e) => ((BaseLayoutItem) dObj).OnAllowMinimizeChanged((bool) e.NewValue), null);
            registrator.Register<bool>("AllowSizing", ref AllowSizingProperty, true, (dObj, e) => ((BaseLayoutItem) dObj).OnAllowSizingChanged((bool) e.NewValue), (dObj, value) => ((BaseLayoutItem) dObj).CoerceAllowSizing((bool) value));
            registrator.Register<bool>("AllowDockToCurrentItem", ref AllowDockToCurrentItemProperty, true, null, null);
            registrator.Register<bool>("FloatOnDoubleClick", ref FloatOnDoubleClickProperty, true, null, null);
            registrator.RegisterReadonly<bool>("IsFloatingRootItem", ref IsFloatingRootItemPropertyKey, ref IsFloatingRootItemProperty, false, (dObj, e) => ((BaseLayoutItem) dObj).OnIsFloatingRootItemChanged((bool) e.NewValue), null);
            registrator.RegisterReadonly<bool>("IsSelected", ref IsSelectedPropertyKey, ref IsSelectedProperty, false, (dObj, e) => ((BaseLayoutItem) dObj).OnIsSelectedChanged((bool) e.NewValue), null);
            registrator.RegisterReadonly<bool>("IsControlItemsHost", ref IsControlItemsHostPropertyKey, ref IsControlItemsHostProperty, false, (dObj, e) => ((BaseLayoutItem) dObj).OnIsControlItemsHostChanged((bool) e.NewValue), (CoerceValueCallback) ((dObj, value) => ((BaseLayoutItem) dObj).CoerceIsControlItemsHost((bool) value)));
            registrator.OverrideMetadata<Visibility>(UIElement.VisibilityProperty, Visibility.Visible, (dObj, e) => ((BaseLayoutItem) dObj).OnVisibilityChanged((Visibility) e.NewValue), (CoerceValueCallback) ((dObj, value) => ((BaseLayoutItem) dObj).CoerceVisibility((Visibility) value)));
            registrator.RegisterReadonly<bool>("IsVisibleCore", ref IsVisibleCorePropertyKey, ref IsVisibleCoreProperty, true, (dObj, e) => ((BaseLayoutItem) dObj).OnIsVisibleChanged((bool) e.NewValue), (CoerceValueCallback) ((dObj, value) => ((BaseLayoutItem) dObj).CoerceIsVisible()));
            registrator.RegisterReadonly<bool>("IsTabPage", ref IsTabPagePropertyKey, ref IsTabPageProperty, false, (dObj, e) => ((BaseLayoutItem) dObj).OnIsTabPageChanged(), (CoerceValueCallback) ((dObj, value) => ((BaseLayoutItem) dObj).CoerceIsTabPage((bool) value)));
            registrator.Register<bool>("IsSelectedItem", ref IsSelectedItemProperty, false, (dObj, e) => ((BaseLayoutItem) dObj).OnIsSelectedItemChanged(), (CoerceValueCallback) ((dObj, value) => ((BaseLayoutItem) dObj).CoerceIsSelectedItem((bool) value)));
            registrator.Register<object>("ControlBoxContent", ref ControlBoxContentProperty, null, (dObj, e) => ((BaseLayoutItem) dObj).OnControlBoxContentChanged(), null);
            registrator.Register<DataTemplate>("ControlBoxContentTemplate", ref ControlBoxContentTemplateProperty, null, (dObj, e) => ((BaseLayoutItem) dObj).OnControlBoxContentTemplateChanged(), null);
            registrator.Register<bool>("ShowCaption", ref ShowCaptionProperty, true, (dObj, e) => ((BaseLayoutItem) dObj).OnShowCaptionChanged((bool) e.NewValue), null);
            registrator.Register<bool>("ShowCaptionImage", ref ShowCaptionImageProperty, true, (dObj, e) => ((BaseLayoutItem) dObj).OnShowCaptionImageChanged((bool) e.NewValue), null);
            registrator.Register<bool>("ShowControlBox", ref ShowControlBoxProperty, true, (dObj, e) => ((BaseLayoutItem) dObj).OnShowControlBoxChanged((bool) e.NewValue), null);
            registrator.Register<bool>("ShowCloseButton", ref ShowCloseButtonProperty, true, (dObj, e) => ((BaseLayoutItem) dObj).OnShowCloseButtonChanged((bool) e.NewValue), null);
            registrator.Register<bool>("ShowPinButton", ref ShowPinButtonProperty, true, (dObj, e) => ((BaseLayoutItem) dObj).OnShowPinButtonChanged((bool) e.NewValue), null);
            registrator.Register<bool>("ShowMinimizeButton", ref ShowMinimizeButtonProperty, true, (dObj, e) => ((BaseLayoutItem) dObj).OnShowMinimizeButtonChanged((bool) e.NewValue), null);
            registrator.Register<bool>("ShowMaximizeButton", ref ShowMaximizeButtonProperty, true, (dObj, e) => ((BaseLayoutItem) dObj).OnShowMaximizeButtonChanged((bool) e.NewValue), null);
            registrator.Register<bool>("ShowRestoreButton", ref ShowRestoreButtonProperty, true, (dObj, e) => ((BaseLayoutItem) dObj).OnShowRestoreButtonChanged((bool) e.NewValue), null);
            registrator.Register<bool>("ShowDropDownButton", ref ShowDropDownButtonProperty, true, (dObj, e) => ((BaseLayoutItem) dObj).OnShowDropDownButtonChanged((bool) e.NewValue), null);
            registrator.Register<bool>("ShowScrollPrevButton", ref ShowScrollPrevButtonProperty, true, (dObj, e) => ((BaseLayoutItem) dObj).OnShowScrollPrevButtonChanged((bool) e.NewValue), null);
            registrator.Register<bool>("ShowScrollNextButton", ref ShowScrollNextButtonProperty, true, (dObj, e) => ((BaseLayoutItem) dObj).OnShowScrollNextButtonChanged((bool) e.NewValue), null);
            registrator.Register<ImageSource>("TabCaptionImage", ref TabCaptionImageProperty, null, null, (CoerceValueCallback) ((dObj, value) => ((BaseLayoutItem) dObj).CoerceTabCaptionImage((ImageSource) value)));
            registrator.Register<object>("TabCaption", ref TabCaptionProperty, null, (dObj, e) => ((BaseLayoutItem) dObj).OnTabCaptionChanged(e.NewValue), (dObj, value) => ((BaseLayoutItem) dObj).CoerceTabCaption(value));
            registrator.Register<string>("TabCaptionFormat", ref TabCaptionFormatProperty, null, (dObj, e) => ((BaseLayoutItem) dObj).OnTabCaptionFormatChanged((string) e.NewValue), (dObj, value) => ((BaseLayoutItem) dObj).CoerceTabCaptionFormat((string) value));
            registrator.RegisterReadonly<string>("ActualTabCaption", ref ActualTabCaptionPropertyKey, ref ActualTabCaptionProperty, null, (dObj, e) => ((BaseLayoutItem) dObj).OnActualTabCaptionChanged((string) e.NewValue), (dObj, value) => ((BaseLayoutItem) dObj).CoerceActualTabCaption((string) value));
            registrator.Register<double>("TabCaptionWidth", ref TabCaptionWidthProperty, double.NaN, (dObj, e) => ((BaseLayoutItem) dObj).OnTabCaptionWidthChanged((double) e.NewValue), (dObj, value) => ((BaseLayoutItem) dObj).CoerceTabCaptionWidth((double) value));
            registrator.RegisterReadonly<bool>("HasTabCaption", ref HasTabCaptionPropertyKey, ref HasTabCaptionProperty, false, null, null);
            registrator.Register<System.Windows.TextWrapping>("TextWrapping", ref TextWrappingProperty, System.Windows.TextWrapping.NoWrap, null, null);
            registrator.Register<System.Windows.TextTrimming>("TextTrimming", ref TextTrimmingProperty, System.Windows.TextTrimming.CharacterEllipsis, null, null);
            registrator.Register<object>("ToolTip", ref ToolTipProperty, null, null, null);
            registrator.Register<DataTemplate>("CaptionTemplate", ref CaptionTemplateProperty, null, (dObj, e) => ((BaseLayoutItem) dObj).OnCaptionTemplateChanged((DataTemplate) e.OldValue, (DataTemplate) e.NewValue), null);
            registrator.Register<DataTemplateSelector>("CaptionTemplateSelector", ref CaptionTemplateSelectorProperty, null, (dObj, e) => ((BaseLayoutItem) dObj).OnCaptionTemplateSelectorChanged((DataTemplateSelector) e.OldValue, (DataTemplateSelector) e.NewValue), null);
            registrator.Register<DevExpress.Xpf.Docking.ClosingBehavior>("ClosingBehavior", ref ClosingBehaviorProperty, DevExpress.Xpf.Docking.ClosingBehavior.Default, null, null);
            registrator.Register<DevExpress.Xpf.Docking.LayoutItemData>("LayoutItemData", ref LayoutItemDataProperty, null, null, null);
            registrator.RegisterReadonly<bool>("IsCaptionVisible", ref IsCaptionVisiblePropertyKey, ref IsCaptionVisibleProperty, true, (dObj, ea) => ((BaseLayoutItem) dObj).OnIsCaptionVisibleChanged((bool) ea.NewValue), (CoerceValueCallback) ((dObj, value) => ((BaseLayoutItem) dObj).CoerceIsCaptionVisible((bool) value)));
            registrator.RegisterReadonly<bool>("IsCaptionImageVisible", ref IsCaptionImageVisiblePropertyKey, ref IsCaptionImageVisibleProperty, false, null, (CoerceValueCallback) ((dObj, value) => ((BaseLayoutItem) dObj).CoerceIsCaptionImageVisible((bool) value)));
            registrator.RegisterReadonly<bool>("IsControlBoxVisible", ref IsControlBoxVisiblePropertyKey, ref IsControlBoxVisibleProperty, false, null, (CoerceValueCallback) ((dObj, value) => ((BaseLayoutItem) dObj).CoerceIsControlBoxVisible((bool) value)));
            registrator.RegisterReadonly<bool>("IsCloseButtonVisible", ref IsCloseButtonVisiblePropertyKey, ref IsCloseButtonVisibleProperty, false, (dObj, e) => ((BaseLayoutItem) dObj).OnIsCloseButtonVisibleChanged((bool) e.NewValue), (CoerceValueCallback) ((dObj, value) => ((BaseLayoutItem) dObj).CoerceIsCloseButtonVisible((bool) value)));
            registrator.RegisterReadonly<bool>("IsPinButtonVisible", ref IsPinButtonVisiblePropertyKey, ref IsPinButtonVisibleProperty, false, null, (CoerceValueCallback) ((dObj, value) => ((BaseLayoutItem) dObj).CoerceIsPinButtonVisible((bool) value)));
            registrator.RegisterReadonly<bool>("IsMinimizeButtonVisible", ref IsMinimizeButtonVisiblePropertyKey, ref IsMinimizeButtonVisibleProperty, false, null, (CoerceValueCallback) ((dObj, value) => ((BaseLayoutItem) dObj).CoerceIsMinimizeButtonVisible((bool) value)));
            registrator.RegisterReadonly<bool>("IsMaximizeButtonVisible", ref IsMaximizeButtonVisiblePropertyKey, ref IsMaximizeButtonVisibleProperty, false, (dObj, e) => ((BaseLayoutItem) dObj).OnIsMaximizeButtonVisibleChanged((bool) e.NewValue), (CoerceValueCallback) ((dObj, value) => ((BaseLayoutItem) dObj).CoerceIsMaximizeButtonVisible((bool) value)));
            registrator.RegisterReadonly<bool>("IsRestoreButtonVisible", ref IsRestoreButtonVisiblePropertyKey, ref IsRestoreButtonVisibleProperty, false, (dObj, e) => ((BaseLayoutItem) dObj).OnIsRestoreButtonVisibleChanged((bool) e.NewValue), (CoerceValueCallback) ((dObj, value) => ((BaseLayoutItem) dObj).CoerceIsRestoreButtonVisible((bool) value)));
            registrator.RegisterReadonly<bool>("IsDropDownButtonVisible", ref IsDropDownButtonVisiblePropertyKey, ref IsDropDownButtonVisibleProperty, false, null, (CoerceValueCallback) ((dObj, value) => ((BaseLayoutItem) dObj).CoerceIsDropDownButtonVisible((bool) value)));
            registrator.RegisterReadonly<bool>("IsScrollPrevButtonVisible", ref IsScrollPrevButtonVisiblePropertyKey, ref IsScrollPrevButtonVisibleProperty, false, null, (CoerceValueCallback) ((dObj, value) => ((BaseLayoutItem) dObj).CoerceIsScrollPrevButtonVisible((bool) value)));
            registrator.RegisterReadonly<bool>("IsScrollNextButtonVisible", ref IsScrollNextButtonVisiblePropertyKey, ref IsScrollNextButtonVisibleProperty, false, null, (CoerceValueCallback) ((dObj, value) => ((BaseLayoutItem) dObj).CoerceIsScrollNextButtonVisible((bool) value)));
            registrator.Register<ICommand>("CloseCommand", ref CloseCommandProperty, null, null, null);
            registrator.Register<object>("CloseCommandParameter", ref CloseCommandParameterProperty, null, null, null);
            registrator.Register<string>("BindableName", ref BindableNameProperty, string.Empty, (dObj, e) => ((BaseLayoutItem) dObj).OnBindableNameChanged((string) e.OldValue, (string) e.NewValue), null);
            registrator.OverrideFrameworkMetadata<double>(FrameworkElement.MinHeightProperty, 0.0, (dObj, e) => ((BaseLayoutItem) dObj).OnMinHeightChanged((double) e.OldValue, (double) e.NewValue), (CoerceValueCallback) ((dObj, value) => ((BaseLayoutItem) dObj).OnCoerceMinHeight((double) value)));
            registrator.OverrideFrameworkMetadata<double>(FrameworkElement.MinWidthProperty, 0.0, (dObj, e) => ((BaseLayoutItem) dObj).OnMinWidthChanged((double) e.OldValue, (double) e.NewValue), (CoerceValueCallback) ((dObj, value) => ((BaseLayoutItem) dObj).OnCoerceMinWidth((double) value)));
            registrator.OverrideFrameworkMetadata<double>(FrameworkElement.MaxWidthProperty, double.PositiveInfinity, (dObj, e) => ((BaseLayoutItem) dObj).OnMaxWidthChanged((double) e.OldValue, (double) e.NewValue), null);
            registrator.OverrideFrameworkMetadata<double>(FrameworkElement.MaxHeightProperty, double.PositiveInfinity, (dObj, e) => ((BaseLayoutItem) dObj).OnMaxHeightChanged((double) e.OldValue, (double) e.NewValue), null);
            registrator.Register<DevExpress.Xpf.Docking.DockingViewStyle>("ViewStyleInternal", ref ViewStyleInternalProperty, DevExpress.Xpf.Docking.DockingViewStyle.Default, (d, e) => ((BaseLayoutItem) d).DockingViewStyle = (DevExpress.Xpf.Docking.DockingViewStyle) e.NewValue, null);
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(BaseLayoutItem), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<BaseLayoutItem> registrator1 = DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<BaseLayoutItem>.New().Register<DockLayoutManager>(System.Linq.Expressions.Expression.Lambda<Func<BaseLayoutItem, DockLayoutManager>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(BaseLayoutItem.get_DockLayoutManagerCore)), parameters), out DockLayoutManagerCoreProperty, null, (d, oldValue, newValue) => d.OnDockLayoutManagerCoreChanged(oldValue, newValue), (d, value) => d.OnCoerceDockLayoutManagerCore(value), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(BaseLayoutItem), "d");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<BaseLayoutItem> registrator2 = registrator1.RegisterReadOnly<DevExpress.Xpf.Docking.DockingViewStyle>(System.Linq.Expressions.Expression.Lambda<Func<BaseLayoutItem, DevExpress.Xpf.Docking.DockingViewStyle>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(BaseLayoutItem.get_DockingViewStyle)), expressionArray2), out DockingViewStylePropertyKey, out DockingViewStyleProperty, DevExpress.Xpf.Docking.DockingViewStyle.Default, (d, oldValue, newValue) => d.OnDockingViewStyleChanged(oldValue, newValue), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(BaseLayoutItem), "d");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<BaseLayoutItem> registrator3 = registrator2.Register<HorizontalAlignment>(System.Linq.Expressions.Expression.Lambda<Func<BaseLayoutItem, HorizontalAlignment>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(BaseLayoutItem.get_TabCaptionHorizontalAlignment)), expressionArray3), out TabCaptionHorizontalAlignmentProperty, HorizontalAlignment.Left, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(BaseLayoutItem), "d");
            ParameterExpression[] expressionArray4 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<BaseLayoutItem> registrator4 = registrator3.Register<VerticalAlignment>(System.Linq.Expressions.Expression.Lambda<Func<BaseLayoutItem, VerticalAlignment>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(BaseLayoutItem.get_TabCaptionVerticalAlignment)), expressionArray4), out TabCaptionVerticalAlignmentProperty, VerticalAlignment.Center, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(BaseLayoutItem), "d");
            ParameterExpression[] expressionArray5 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<BaseLayoutItem> registrator5 = registrator4.Register<DataTemplate>(System.Linq.Expressions.Expression.Lambda<Func<BaseLayoutItem, DataTemplate>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(BaseLayoutItem.get_TabCaptionTemplate)), expressionArray5), out TabCaptionTemplateProperty, null, (d, oldValue, newValue) => d.OnTabCaptionTemplateChanged(oldValue, newValue), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(BaseLayoutItem), "d");
            ParameterExpression[] expressionArray6 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<BaseLayoutItem> registrator6 = registrator5.Register<DataTemplateSelector>(System.Linq.Expressions.Expression.Lambda<Func<BaseLayoutItem, DataTemplateSelector>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(BaseLayoutItem.get_TabCaptionTemplateSelector)), expressionArray6), out TabCaptionTemplateSelectorProperty, null, (d, oldValue, newValue) => d.OnTabCaptionTemplateSelectorChanged(oldValue, newValue), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(BaseLayoutItem), "d");
            ParameterExpression[] expressionArray7 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<BaseLayoutItem> registrator7 = registrator6.Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<BaseLayoutItem, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(BaseLayoutItem.get_HasTabCaptionTemplate)), expressionArray7), out HasTabCaptionTemplateProperty, false, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(BaseLayoutItem), "d");
            ParameterExpression[] expressionArray8 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<BaseLayoutItem> registrator8 = registrator7.Register<DataTemplate>(System.Linq.Expressions.Expression.Lambda<Func<BaseLayoutItem, DataTemplate>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(BaseLayoutItem.get_ContextMenuCustomizationsTemplate)), expressionArray8), out ContextMenuCustomizationsTemplateProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(BaseLayoutItem), "d");
            ParameterExpression[] expressionArray9 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<BaseLayoutItem> registrator9 = registrator8.Register<object>(System.Linq.Expressions.Expression.Lambda<Func<BaseLayoutItem, object>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(BaseLayoutItem.get_ActualCustomizationCaption)), expressionArray9), out ActualCustomizationCaptionProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(BaseLayoutItem), "d");
            ParameterExpression[] expressionArray10 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<BaseLayoutItem> registrator10 = registrator9.Register<DataTemplate>(System.Linq.Expressions.Expression.Lambda<Func<BaseLayoutItem, DataTemplate>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(BaseLayoutItem.get_ActualCustomizationCaptionTemplate)), expressionArray10), out ActualCustomizationCaptionTemplateProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(BaseLayoutItem), "d");
            ParameterExpression[] expressionArray11 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<BaseLayoutItem> registrator11 = registrator10.Register<DataTemplateSelector>(System.Linq.Expressions.Expression.Lambda<Func<BaseLayoutItem, DataTemplateSelector>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(BaseLayoutItem.get_ActualCustomizationCaptionTemplateSelector)), expressionArray11), out ActualCustomizationCaptionTemplateSelectorProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(BaseLayoutItem), "d");
            ParameterExpression[] expressionArray12 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            registrator11.Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<BaseLayoutItem, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(BaseLayoutItem.get_ShowTabCaptionImage)), expressionArray12), out ShowTabCaptionImageProperty, true, frameworkOptions);
        }

        protected BaseLayoutItem()
        {
            this.SerializationInfo = this.CreateSerializationInfo();
            this.LayoutItemData = new DevExpress.Xpf.Docking.LayoutItemData(this);
            this.dockingTargetLocker = new Locker();
            this.OnCreate();
            this.SetIsVisible(true);
            if (!this.IsInDesignTime)
            {
                base.CoerceValue(CaptionFormatProperty);
                base.CoerceValue(TabCaptionFormatProperty);
            }
            base.CoerceValue(CustomizationCaptionProperty);
            base.CoerceValue(ActualMarginProperty);
            base.CoerceValue(ActualPaddingProperty);
            base.CoerceValue(IsCloseButtonVisibleProperty);
            base.CoerceValue(IsPinButtonVisibleProperty);
            base.CoerceValue(IsMinimizeButtonVisibleProperty);
            base.CoerceValue(IsMaximizeButtonVisibleProperty);
            base.CoerceValue(IsRestoreButtonVisibleProperty);
            base.CoerceValue(IsDropDownButtonVisibleProperty);
            base.CoerceValue(IsScrollNextButtonVisibleProperty);
            base.CoerceValue(IsScrollPrevButtonVisibleProperty);
            base.CoerceValue(IsControlBoxVisibleProperty);
        }

        public virtual void Accept(IVisitor<BaseLayoutItem> visitor)
        {
            visitor.Visit(this);
        }

        public virtual void Accept(VisitDelegate<BaseLayoutItem> visit)
        {
            visit(this);
        }

        protected internal void AddLogicalChild(object child)
        {
            if (!this.logicalChildren.Contains(child))
            {
                this.logicalChildren.Add(child);
            }
            base.AddLogicalChild(child);
        }

        internal virtual bool AllowSerializeProperty(AllowPropertyEventArgs e) => 
            true;

        internal virtual void ApplySerializationInfo()
        {
        }

        protected internal virtual void BeginFloating()
        {
        }

        public override void BeginInit()
        {
            base.BeginInit();
            this.initCounter++;
        }

        protected virtual Size CalcMaxSizeValue(Size value) => 
            value;

        protected virtual Size CalcMinSizeValue(Size value) => 
            value;

        private void CheckClosedState()
        {
            this.ParentLockHelper.DoWhenUnlocked(new LockHelper.LockHelperDelegate(this.CheckClosedStateCore));
        }

        private void CheckClosedStateCore()
        {
            DockLayoutManager manager = this.FindDockLayoutManager();
            if ((manager != null) && ((this.Closed != this.IsClosed) && (!this.IsInDesignTime && (!this.IsInitializing && !this.IsDeserializing))))
            {
                using (this.LockCloseCommand())
                {
                    manager.CheckClosedState(this);
                }
            }
        }

        protected internal Size CheckSize(Size layoutSize)
        {
            Size size;
            if (System.Windows.DependencyPropertyHelper.GetValueSource(this, FloatSizeProperty).BaseValueSource != BaseValueSource.Default)
            {
                return this.FloatSize;
            }
            FloatGroup parent = this.Parent as FloatGroup;
            return (((parent == null) || !parent.TryGetActualAutoSize(out size)) ? layoutSize : size);
        }

        protected internal virtual void ClearTemplate()
        {
            this.ClearTemplateCore();
        }

        protected internal virtual void ClearTemplateCore()
        {
            if (this.PartMultiTemplateControl != null)
            {
                this.PartMultiTemplateControl.LayoutItem = null;
            }
            base.ClearValue(DockLayoutManager.UIScopeProperty);
            base.ClearValue(DockLayoutManager.DockLayoutManagerProperty);
        }

        protected virtual object CoerceActualAppearance(DevExpress.Xpf.Docking.Appearance value) => 
            AppearanceHelper.GetActualAppearance(this.Parent?.ActualItemsAppearance, this.Appearance);

        protected virtual object CoerceActualAppearanceObject(AppearanceObject value) => 
            (this.ActualAppearance == null) ? value : (this.IsActive ? this.ActualAppearance.Active : this.ActualAppearance.Normal);

        protected virtual string CoerceActualCaption(string value) => 
            (string.IsNullOrEmpty(this.CaptionFormat) || !IsStringOrPrimitive(this.Caption)) ? (this.Caption as string) : string.Format(this.CaptionFormat, this.Caption);

        protected virtual double CoerceActualCaptionWidth(double value) => 
            value;

        protected virtual Thickness CoerceActualMargin(Thickness value) => 
            MathHelper.AreEqual(this.Margin, new Thickness(double.NaN)) ? value : this.Margin;

        protected virtual Size CoerceActualMaxSize()
        {
            Size size = new Size(MathHelper.IsConstraintValid(base.MaxWidth) ? base.MaxWidth : double.NaN, MathHelper.IsConstraintValid(base.MaxHeight) ? base.MaxHeight : double.NaN);
            Size size2 = this.CalcMaxSizeValue(size);
            return new Size(Math.Max(this.ActualMinSize.Width, size2.Width), Math.Max(this.ActualMinSize.Height, size2.Height));
        }

        protected virtual Size CoerceActualMinSize()
        {
            Size size = new Size(MathHelper.IsConstraintValid(base.MinWidth) ? base.MinWidth : 0.0, MathHelper.IsConstraintValid(base.MinHeight) ? base.MinHeight : 0.0);
            return this.CalcMinSizeValue(size);
        }

        protected virtual Thickness CoerceActualPadding(Thickness value) => 
            MathHelper.AreEqual(this.Padding, new Thickness(double.NaN)) ? value : this.Padding;

        protected virtual object CoerceActualTabCaption(string captionFormat)
        {
            object caption = this.TabCaption ?? this.Caption;
            return ((string.IsNullOrEmpty(this.TabCaptionFormat) || !IsStringOrPrimitive(caption)) ? (caption as string) : string.Format(this.TabCaptionFormat, caption));
        }

        protected virtual object CoerceAllowSizing(bool value) => 
            value;

        protected virtual object CoerceAppearance(DevExpress.Xpf.Docking.Appearance value) => 
            value ?? this.DefaultAppearance;

        protected virtual DevExpress.Xpf.Docking.CaptionAlignMode CoerceCaptionAlignMode(object value)
        {
            DevExpress.Xpf.Docking.CaptionAlignMode mode = this.GetHierarchyPropertyValue<DevExpress.Xpf.Docking.CaptionAlignMode>(CaptionAlignModeProperty, value, DevExpress.Xpf.Docking.CaptionAlignMode.Default);
            return (((mode == DevExpress.Xpf.Docking.CaptionAlignMode.Default) || (mode == DevExpress.Xpf.Docking.CaptionAlignMode.AlignInGroup)) ? ((DevExpress.Xpf.Docking.CaptionAlignMode) value) : mode);
        }

        protected virtual string CoerceCaptionFormat(string captionFormat) => 
            captionFormat;

        protected virtual ImageSource CoerceCaptionImage(ImageSource value)
        {
            DockLayoutManager manager = this.FindDockLayoutManager();
            if ((manager != null) && (((value == null) && ((this.Caption == null) || ((this.Caption is string) && string.IsNullOrEmpty((string) this.Caption)))) && !this.HasCaptionTemplate))
            {
                if (this.IsTabPage)
                {
                    return manager.DefaultTabPageCaptionImage;
                }
                if (this.IsAutoHidden)
                {
                    return manager.DefaultAutoHidePanelCaptionImage;
                }
            }
            return value;
        }

        protected virtual double CoerceCaptionWidth(double value) => 
            CaptionAlignHelper.GetCaptionWidth(this, value);

        protected virtual string CoerceCustomizationCaption(string value)
        {
            string caption = this.Caption as string;
            return (string.IsNullOrEmpty(value) ? (string.IsNullOrEmpty(caption) ? (string.IsNullOrEmpty(base.Name) ? this.TypeName : base.Name) : caption) : value);
        }

        protected virtual object CoerceDataContext(object value) => 
            !this.AreInheritablePropertiesLocked ? value : this._DataContext;

        protected virtual Size CoerceFloatSize(Size value) => 
            MathHelper.MeasureSize(this.ActualMinSize, this.ActualMaxSize, value);

        protected virtual GridLength CoerceHeight(GridLength value)
        {
            if (value.IsAbsolute)
            {
                double num = MathHelper.MeasureDimension(this.ActualMinSize.Height, this.ActualMaxSize.Height, value.Value);
                value = new GridLength(num, GridUnitType.Pixel);
            }
            return value;
        }

        protected virtual bool CoerceIsCaptionImageVisible(bool visible) => 
            this.HasImage && this.ShowCaptionImage;

        protected virtual bool CoerceIsCaptionVisible(bool visible) => 
            this.ShowCaption;

        protected virtual bool CoerceIsCloseButtonVisible(bool visible) => 
            this.AllowClose && this.ShowCloseButton;

        protected virtual bool CoerceIsControlBoxVisible(bool visible) => 
            this.ShowControlBox;

        protected virtual bool CoerceIsControlItemsHost(bool value) => 
            false;

        protected virtual bool CoerceIsDropDownButtonVisible(bool visible) => 
            false;

        protected virtual bool CoerceIsMaximizeButtonVisible(bool visible) => 
            false;

        protected virtual bool CoerceIsMinimizeButtonVisible(bool visible) => 
            false;

        protected virtual bool CoerceIsPinButtonVisible(bool visible) => 
            false;

        protected virtual bool CoerceIsRestoreButtonVisible(bool visible) => 
            false;

        protected virtual bool CoerceIsScrollNextButtonVisible(bool visible) => 
            false;

        protected virtual bool CoerceIsScrollPrevButtonVisible(bool visible) => 
            false;

        protected virtual bool CoerceIsSelectedItem(bool value) => 
            value;

        protected virtual bool CoerceIsTabPage(bool value) => 
            (this.Parent != null) && (this.Parent.GroupBorderStyle == GroupBorderStyle.Tabbed);

        protected virtual bool CoerceIsVisible() => 
            VisibilityHelper.GetIsVisible(this, this.isVisibleCore);

        protected void CoerceParentProperty(DependencyProperty property)
        {
            if (this.Parent != null)
            {
                this.Parent.CoerceValue(property);
            }
        }

        protected virtual void CoerceSizes()
        {
            base.CoerceValue(FloatSizeProperty);
            base.CoerceValue(ItemWidthProperty);
            base.CoerceValue(ItemHeightProperty);
            base.CoerceValue(AutoHideGroup.AutoHideSizeProperty);
            if (this.Parent != null)
            {
                this.Parent.CoerceSizes();
            }
        }

        private object CoerceStyle(Style value) => 
            !this.AreInheritablePropertiesLocked ? value : this._Style;

        protected virtual object CoerceTabCaption(object value) => 
            value ?? this.Caption;

        protected virtual object CoerceTabCaptionFormat(string captionFormat) => 
            string.IsNullOrEmpty(captionFormat) ? DockLayoutManagerParameters.TabCaptionFormat : captionFormat;

        protected virtual ImageSource CoerceTabCaptionImage(ImageSource value) => 
            value ?? this.CaptionImage;

        protected virtual object CoerceTabCaptionWidth(double value) => 
            CaptionAlignHelper.GetTabCaptionWidth(this, value);

        protected virtual Visibility CoerceVisibility(Visibility visibility)
        {
            DockLayoutManager manager = this.FindDockLayoutManager();
            if (manager == null)
            {
                return visibility;
            }
            bool? showInvisibleItems = manager.ShowInvisibleItems;
            return VisibilityHelper.Convert((showInvisibleItems != null) ? showInvisibleItems.Value : (this.IsInDesignTime ? manager.IsCustomization : (manager.IsCustomization && manager.ShowInvisibleItemsInCustomizationForm)), visibility);
        }

        protected virtual GridLength CoerceWidth(GridLength value)
        {
            if (value.IsAbsolute)
            {
                double num = MathHelper.MeasureDimension(this.ActualMinSize.Width, this.ActualMaxSize.Width, value.Value);
                value = new GridLength(num, GridUnitType.Pixel);
            }
            return value;
        }

        internal virtual void CollectSerializationInfo()
        {
        }

        protected virtual BaseLayoutItemSerializationInfo CreateSerializationInfo() => 
            new BaseLayoutItemSerializationInfo(this);

        internal virtual UIChildren CreateUIChildren() => 
            new UIChildren();

        protected internal virtual void EndFloating()
        {
        }

        public override void EndInit()
        {
            int num = this.initCounter - 1;
            this.initCounter = num;
            if ((num == 0) && !this.isInitializedCore)
            {
                this.isInitializedCore = true;
                this.OnInitialized();
            }
            base.EndInit();
        }

        private void EnsureAppearance()
        {
            base.CoerceValue(AppearanceProperty);
        }

        private ActionGroup EnsureContextMenuActionGroup()
        {
            ActionGroup target = new ActionGroup();
            BindingHelper.SetBinding(target, ActionGroup.ActionsTemplateProperty, this, ContextMenuCustomizationsTemplateProperty, BindingMode.OneWay);
            this.AddLogicalChild(target);
            return target;
        }

        internal void EnsureTemplate()
        {
            this.SelectTemplateIfNeeded();
        }

        private IUIElement EnsureUIScope()
        {
            IUIElement element = this.FindUIScopeCore();
            base.SetValue(DockLayoutManager.UIScopeProperty, element);
            if (element != null)
            {
                base.SetValue(DockLayoutManager.LayoutItemProperty, this);
                this.UIElements.Add(this);
            }
            return element;
        }

        internal bool ExecuteCloseCommand()
        {
            if (this.closeCommandLocker)
            {
                return false;
            }
            using (this.closeCommandLocker.Lock())
            {
                ICommand closeCommand = this.CloseCommand;
                object parameter = this.IsPropertySet(CloseCommandParameterProperty) ? this.CloseCommandParameter : this;
                RoutedCommand command2 = closeCommand as RoutedCommand;
                if (closeCommand != null)
                {
                    if ((command2 == null) || !command2.CanExecute(parameter, this))
                    {
                        if (closeCommand.CanExecute(parameter))
                        {
                            closeCommand.Execute(parameter);
                            return true;
                        }
                    }
                    else
                    {
                        command2.Execute(parameter, this);
                        return true;
                    }
                }
                return false;
            }
        }

        protected internal virtual IUIElement FindUIScopeCore()
        {
            IUIElement element = ((this.Parent == null) || ((this.Parent is FloatGroup) || (this.Parent is AutoHideGroup))) ? this.FindUIScope() : this.Parent;
            return (!this.IsTabPage ? element : (this.IsSelectedItem ? element : null));
        }

        protected virtual DataTemplate GetActualCustmizationCaptionTemplate() => 
            this.CaptionTemplate;

        protected virtual object GetActualCustomizationCaption() => 
            this.Caption;

        protected virtual DataTemplateSelector GetActualCustomizationCaptionTemplateSelector() => 
            this.CaptionTemplateSelector;

        internal virtual bool GetAllowDockToCurrentItem() => 
            this.AllowDockToCurrentItem;

        private T GetHierarchyPropertyValue<T>(DependencyProperty property, object value, T defaultValue) => 
            new HierarchyPropertyValue<T>(property, defaultValue).Get(this, value);

        protected virtual bool GetIsAutoHidden() => 
            this.Parent is AutoHideGroup;

        private bool GetIsLayoutTreeChangeInProgress()
        {
            List<BaseLayoutItem> source = new List<BaseLayoutItem>();
            for (BaseLayoutItem item = this; item != null; item = item.Parent)
            {
                source.Add(item);
            }
            Func<BaseLayoutItem, bool> predicate = <>c.<>9__832_0;
            if (<>c.<>9__832_0 == null)
            {
                Func<BaseLayoutItem, bool> local1 = <>c.<>9__832_0;
                predicate = <>c.<>9__832_0 = x => x.IsLayoutChangeInProgress || x.GetIsLogicalChildrenIterationInProgress();
            }
            return source.Any<BaseLayoutItem>(predicate);
        }

        protected virtual bool GetIsPermanent() => 
            (bool) this.dockingTargetLocker;

        protected internal DevExpress.Xpf.Docking.DockSituation GetLastDockSituation() => 
            this.DockSituation;

        protected abstract LayoutItemType GetLayoutItemTypeCore();
        internal List<object> GetLogicalChildren() => 
            this.logicalChildren;

        protected virtual BaseLayoutItem[] GetNodesCore() => 
            EmptyNodes;

        protected internal virtual T GetUIElement<T>() where T: class => 
            this.GetUIElement<T>(false);

        internal T GetUIElement<T>(bool skipSelf) where T: class => 
            this.UIElements.GetElements().FirstOrDefault<IUIElement>(element => ((element is T) && (!skipSelf || !Equals(element, this)))) as T;

        protected void HasCaptionTemplateEval()
        {
            this.HasCaptionTemplate = (this.CaptionTemplate != null) || (this.CaptionTemplateSelector != null);
        }

        protected void HasTabCaptionTemplateEval()
        {
            this.HasTabCaptionTemplate = (this.TabCaptionTemplate != null) || (this.TabCaptionTemplateSelector != null);
        }

        protected internal void InvalidateTabHeader()
        {
            TabbedPaneItem element = this.UIElements.GetElement<TabbedPaneItem>();
            if (element != null)
            {
                BaseHeadersPanel.Invalidate(element);
            }
        }

        internal void InvokeCancelActivation(BaseLayoutItem activeItem)
        {
            this.isActivationCancelledLocker.Lock();
            object[] args = new object[] { activeItem };
            base.Dispatcher.BeginInvoke(new Action<BaseLayoutItem>(this.TryCancelActivation), args);
        }

        protected internal void InvokeCoerceDockItemState()
        {
            if (this.IsDockItemStateLocked)
            {
                this.lockDockItemStateCount++;
            }
            else
            {
                this.InvokeCoerceDockItemStateCore();
                this.lockDockItemStateCount = 0;
            }
        }

        protected virtual void InvokeCoerceDockItemStateCore()
        {
        }

        protected internal void InvokeCoerceInheritableProperties()
        {
            if (this.AreInheritablePropertiesLocked)
            {
                this.lockInheritablePropertiesCount++;
            }
            else
            {
                base.CoerceValue(DockLayoutManagerCoreProperty);
                base.CoerceValue(FrameworkElement.DataContextProperty);
                base.CoerceValue(FrameworkElement.StyleProperty);
                this.lockInheritablePropertiesCount = 0;
            }
        }

        private static bool IsStringOrPrimitive(object caption) => 
            (caption != null) && ((caption is string) || caption.GetType().IsPrimitive);

        internal virtual IDisposable LockCanMerge() => 
            null;

        internal IDisposable LockCloseCommand() => 
            this.closeCommandLocker.Lock();

        internal IDisposable LockDockingTarget() => 
            this.dockingTargetLocker.Lock();

        private void LockDockItemState()
        {
            this.lockDockItemState++;
        }

        protected internal virtual void LockInheritableProperties()
        {
            this.inheritablePropertiesLocker.Lock();
        }

        protected internal virtual void LockInLogicalTree()
        {
            this.LogicalTreeLockHelper.Lock();
            this.LockInheritableProperties();
            this.LockDockItemState();
        }

        protected virtual void OnActualAppearanceChanged(DevExpress.Xpf.Docking.Appearance newValue)
        {
            base.CoerceValue(ActualAppearanceObjectProperty);
            if (newValue != null)
            {
                newValue.Owner = this;
            }
        }

        protected virtual void OnActualAppearanceChanged(DevExpress.Xpf.Docking.Appearance oldValue, DevExpress.Xpf.Docking.Appearance newValue)
        {
            this.OnActualAppearanceChanged(newValue);
            if ((oldValue != null) && Equals(oldValue.Owner, this))
            {
                oldValue.Owner = null;
            }
        }

        protected virtual void OnActualAppearanceObjectChanged(AppearanceObject newValue)
        {
            base.ClearValue(DesiredCaptionWidthPropertyKey);
            this.RaiseVisualChanged();
        }

        protected virtual void OnActualCaptionChanged(string value)
        {
            base.SetValue(HasCaptionPropertyKey, !string.IsNullOrEmpty(value));
            this.UpdateWindowTaskbarTitle();
            this.UpdateActualCustomizationCaption();
        }

        protected virtual void OnActualMarginChanged(Thickness value)
        {
        }

        protected virtual void OnActualMaxSizeChanged()
        {
            this.CoerceSizes();
            this.CoerceParentProperty(ActualMaxSizeProperty);
            DefinitionsHelper.GetDefinition(this).Do<DefinitionBase>(x => x.SetMaxSize(this.ActualMaxSize));
        }

        protected virtual void OnActualMinSizeChanged()
        {
            base.CoerceValue(ActualMaxSizeProperty);
            this.CoerceSizes();
            this.CoerceParentProperty(ActualMinSizeProperty);
            if (base.Visibility != Visibility.Collapsed)
            {
                DefinitionsHelper.GetDefinition(this).Do<DefinitionBase>(x => x.SetMinSize(this.ActualMinSize));
            }
        }

        protected virtual void OnActualTabCaptionChanged(string value)
        {
            base.SetValue(HasTabCaptionPropertyKey, !string.IsNullOrEmpty(value));
        }

        protected virtual void OnAllowCloseChanged(bool value)
        {
            base.CoerceValue(IsCloseButtonVisibleProperty);
        }

        protected virtual void OnAllowDockChanged(bool value)
        {
            base.CoerceValue(IsPinButtonVisibleProperty);
        }

        protected virtual void OnAllowHideChanged(bool value)
        {
            base.CoerceValue(IsPinButtonVisibleProperty);
        }

        protected virtual void OnAllowMaximizeChanged(bool value)
        {
            base.CoerceValue(IsMaximizeButtonVisibleProperty);
        }

        protected virtual void OnAllowMinimizeChanged(bool value)
        {
            base.CoerceValue(IsMinimizeButtonVisibleProperty);
        }

        protected internal virtual void OnAllowRestoreChanged(bool value)
        {
            CommandManager.InvalidateRequerySuggested();
        }

        protected virtual void OnAllowSizingChanged(bool value)
        {
            if (this.IsFloatingRootItem)
            {
                this.Parent.CoerceValue(AllowSizingProperty);
            }
        }

        protected virtual void OnAppearanceChanged(DevExpress.Xpf.Docking.Appearance newValue)
        {
            base.CoerceValue(ActualAppearanceProperty);
            if (newValue != null)
            {
                newValue.Owner = this;
            }
        }

        protected virtual void OnAppearanceChanged(DevExpress.Xpf.Docking.Appearance oldValue, DevExpress.Xpf.Docking.Appearance newValue)
        {
            this.OnAppearanceChanged(newValue);
            if ((oldValue != null) && Equals(oldValue.Owner, this))
            {
                oldValue.Owner = null;
            }
        }

        protected internal virtual void OnAppearanceObjectPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.CoerceValue(ActualAppearanceProperty);
            base.CoerceValue(ActualAppearanceObjectProperty);
            this.RaiseVisualChanged();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.IsTemplateApplied = true;
            if ((this.PartMultiTemplateControl != null) && !LayoutItemsHelper.IsTemplateChild<BaseLayoutItem>(this.PartMultiTemplateControl, this))
            {
                this.PartMultiTemplateControl.Dispose();
            }
            this.PartMultiTemplateControl = LayoutItemsHelper.GetTemplateChild<MultiTemplateControl>(this);
            this.SelectTemplate();
            this.UpdateVisualState();
        }

        protected virtual void OnBindableNameChanged(string oldValue, string newValue)
        {
            if (!string.IsNullOrEmpty(newValue))
            {
                base.Name = newValue;
            }
        }

        protected virtual void OnCaptionAlignModeChanged(DevExpress.Xpf.Docking.CaptionAlignMode oldValue, DevExpress.Xpf.Docking.CaptionAlignMode value)
        {
        }

        protected virtual void OnCaptionChanged(object oldValue, object newValue)
        {
            base.CoerceValue(ActualCaptionProperty);
            base.CoerceValue(CaptionImageProperty);
            base.CoerceValue(CustomizationCaptionProperty);
            base.CoerceValue(IsCaptionVisibleProperty);
            base.ClearValue(DesiredCaptionWidthPropertyKey);
            base.CoerceValue(TabCaptionProperty);
            if (!IsStringOrPrimitive(newValue))
            {
                this.InvalidateTabHeader();
            }
            this.UpdateActualCustomizationCaption();
        }

        protected virtual void OnCaptionFormatChanged()
        {
            base.CoerceValue(ActualCaptionProperty);
        }

        protected virtual void OnCaptionImageChanged(ImageSource value)
        {
            base.SetValue(HasImagePropertyKey, value != null);
            base.CoerceValue(TabCaptionImageProperty);
            base.CoerceValue(IsCaptionImageVisibleProperty);
            this.UpdateWindowTaskbarIcon();
        }

        protected virtual void OnCaptionLocationChanged(DevExpress.Xpf.Docking.CaptionLocation value)
        {
        }

        protected virtual void OnCaptionTemplateChanged(DataTemplate oldValue, DataTemplate newValue)
        {
            this.OnCaptionTemplatePropertyChanged();
        }

        private void OnCaptionTemplatePropertyChanged()
        {
            this.HasCaptionTemplateEval();
            this.HasTabCaptionTemplateEval();
            this.InvalidateTabHeader();
            this.UpdateActualCustomizationCaption();
        }

        protected virtual void OnCaptionTemplateSelectorChanged(DataTemplateSelector oldValue, DataTemplateSelector newValue)
        {
            this.OnCaptionTemplatePropertyChanged();
        }

        protected virtual void OnCaptionWidthChanged(double value)
        {
        }

        protected virtual void OnClosedChanged(bool oldValue, bool newValue)
        {
            Action<DispatcherOperation> action = <>c.<>9__752_0;
            if (<>c.<>9__752_0 == null)
            {
                Action<DispatcherOperation> local1 = <>c.<>9__752_0;
                action = <>c.<>9__752_0 = x => x.Abort();
            }
            this.closedChangedDispatcherOperation.Do<DispatcherOperation>(action);
            this.closedChangedDispatcherOperation = DevExpress.Xpf.Docking.InvokeHelper.BeginInvoke(this, () => this.IsStyleUpdateInProgress, new Action(this.CheckClosedState), DispatcherPriority.Normal);
            this.InvokeCoerceInheritableProperties();
        }

        internal virtual DockLayoutManager OnCoerceDockLayoutManagerCore(DockLayoutManager value) => 
            !this.AreInheritablePropertiesLocked ? value : this._DockLayoutManagerCached;

        protected virtual double OnCoerceMinHeight(double value) => 
            value;

        protected virtual double OnCoerceMinWidth(double value) => 
            value;

        protected virtual void OnControlBoxContentChanged()
        {
            this.CoerceParentProperty(ControlBoxContentProperty);
        }

        protected virtual void OnControlBoxContentTemplateChanged()
        {
            this.CoerceParentProperty(ControlBoxContentTemplateProperty);
        }

        protected virtual void OnCreate()
        {
            this.EnsureAppearance();
        }

        protected override AutomationPeer OnCreateAutomationPeer() => 
            !this.IsAutoHidden ? (!this.IsClosed ? (!this.IsTabPage ? new BaseLayoutItemAutomationPeer(this) : new TabbedItemAutomationPeer(this)) : new ClosedPanelItemAutomationPeer(this)) : new AutoHideItemAutomationPeer(this);

        protected virtual void OnCustomizationCaptionChanged(string oldValue, string newValue)
        {
            this.UpdateActualCustomizationCaption();
        }

        protected virtual void OnDataContextChanged(object value)
        {
            this._DataContext = value;
        }

        protected internal virtual void OnDeserializationComplete()
        {
            this.IsDeserializing = false;
            this.ApplySerializationInfo();
            this.CheckClosedState();
        }

        protected internal virtual void OnDeserializationStarted()
        {
            this.IsDeserializing = true;
        }

        protected virtual void OnDesiredCaptionWidthChanged(double value)
        {
            base.SetValue(HasDesiredCaptionWidthPropertyKey, !double.IsNaN(value));
            if (double.IsNaN(value))
            {
                this.InvalidateTabHeader();
            }
            base.CoerceValue(ActualCaptionWidthProperty);
        }

        protected virtual void OnDockingViewStyleChanged(DevExpress.Xpf.Docking.DockingViewStyle oldValue, DevExpress.Xpf.Docking.DockingViewStyle newValue)
        {
            this.RaiseGeometryChanged();
        }

        protected virtual void OnDockLayoutManagerChanged()
        {
            this.InvokeCoerceInheritableProperties();
            base.CoerceValue(CaptionImageProperty);
            this.CheckClosedState();
        }

        protected virtual void OnDockLayoutManagerChanged(DockLayoutManager oldValue, DockLayoutManager newValue)
        {
            this.OnDockLayoutManagerChanged();
            if (newValue != null)
            {
                this.ParentLockHelper.DoWhenUnlocked(delegate {
                    if (this.IsActive)
                    {
                        this.TryToActivate();
                    }
                });
            }
        }

        internal virtual void OnDockLayoutManagerCoreChanged(DockLayoutManager oldValue, DockLayoutManager newValue)
        {
            this._DockLayoutManagerCached = newValue;
            if (newValue == null)
            {
                base.ClearValue(ViewStyleInternalProperty);
            }
            else
            {
                Binding binding = new Binding();
                binding.Source = newValue;
                binding.Path = new PropertyPath(DockLayoutManager.ViewStyleProperty);
                base.SetBinding(ViewStyleInternalProperty, binding);
            }
        }

        protected virtual void OnFloatSizeChanged(Size newValue)
        {
        }

        protected virtual void OnFloatSizeChanged(Size oldValue, Size newValue)
        {
            this.OnFloatSizeChanged(newValue);
        }

        protected virtual void OnHasCaptionChanged(bool hasCaption)
        {
        }

        protected virtual void OnHasCaptionTemplateChanged(bool hasCaptionTemplate)
        {
            base.CoerceValue(CaptionImageProperty);
        }

        protected virtual void OnHeightChanged(GridLength value, GridLength prevValue)
        {
            if (this.Manager != null)
            {
                this.Manager.RaiseItemSizeChangedEvent(this, false, value, prevValue);
            }
        }

        protected override void OnHeightInternalChanged(double oldValue, double newValue)
        {
            throw new NotSupportedException(DockLayoutManagerHelper.GetRule(DockLayoutManagerRule.HeightIsNotSupported));
        }

        protected virtual void OnInitialized()
        {
            this.InvokeCoerceInheritableProperties();
            this.RegisterName();
        }

        protected virtual void OnIsActiveChanged(bool value)
        {
            if (this.ParentLockHelper.IsLocked)
            {
                this.ParentLockHelper.AddUnlockAction(new LockHelper.LockHelperDelegate(this.OnIsActiveChangedCore));
            }
            else if (this.LogicalTreeLockHelper.IsLocked & value)
            {
                this.LogicalTreeLockHelper.AddUnlockAction(new LockHelper.LockHelperDelegate(this.OnIsActiveChangedCore));
            }
            else
            {
                this.OnIsActiveChangedCore();
            }
        }

        protected virtual void OnIsActiveChangedCore()
        {
            if (!this.activationCancelLocker.IsLocked)
            {
                this.TryToActivate();
            }
            base.CoerceValue(ActualAppearanceObjectProperty);
            this.RaiseVisualChanged();
            this.UpdateVisualState();
        }

        protected virtual void OnIsCaptionVisibleChanged(bool isCaptionVisible)
        {
        }

        protected virtual void OnIsCloseButtonVisibleChanged(bool visible)
        {
        }

        private void OnIsClosedChanged(bool newValue)
        {
            this.UpdateHideFromSearch();
        }

        protected virtual void OnIsControlItemsHostChanged(bool value)
        {
        }

        internal virtual void OnIsCustomizationChanged(bool isCustomization)
        {
            base.CoerceValue(UIElement.VisibilityProperty);
        }

        protected virtual void OnIsFloatingRootItemChanged(bool newValue)
        {
        }

        protected virtual void OnIsMaximizeButtonVisibleChanged(bool visible)
        {
        }

        protected virtual void OnIsRestoreButtonVisibleChanged(bool visible)
        {
        }

        protected virtual void OnIsSelectedChanged(bool selected)
        {
            if (this.Manager != null)
            {
                this.Manager.RaiseItemSelectionChangedEvent(this, selected);
            }
        }

        protected virtual void OnIsSelectedItemChanged()
        {
            if (this.Parent != null)
            {
                this.Parent.SetSelection(this, this.IsSelectedItem);
            }
            if (this.IsSelectedItem)
            {
                this.LastSelectionDateTime = DateTime.Now;
            }
            this.UpdateHideFromSearch();
        }

        protected virtual void OnIsTabPageChanged()
        {
            base.CoerceValue(CaptionImageProperty);
            this.UpdateHideFromSearch();
        }

        protected virtual void OnIsVisibleChanged(bool isVisible)
        {
            this.RaiseIsVisibleChanged(isVisible);
            if (this.Parent != null)
            {
                this.Parent.OnItemIsVisibleChanged(this);
            }
        }

        protected virtual void OnMarginChanged(Thickness value)
        {
            base.CoerceValue(ActualMarginProperty);
        }

        protected virtual void OnMaxHeightChanged(double oldValue, double newValue)
        {
            base.CoerceValue(ActualMaxSizeProperty);
        }

        protected virtual void OnMaxWidthChanged(double oldValue, double newValue)
        {
            base.CoerceValue(ActualMaxSizeProperty);
        }

        protected virtual void OnMinHeightChanged(double oldValue, double newValue)
        {
            base.CoerceValue(ActualMinSizeProperty);
        }

        protected virtual void OnMinWidthChanged(double oldValue, double newValue)
        {
            base.CoerceValue(ActualMinSizeProperty);
        }

        protected virtual void OnNameChanged()
        {
            base.CoerceValue(CustomizationCaptionProperty);
        }

        protected virtual void OnPaddingChanged(Thickness value)
        {
            base.CoerceValue(ActualPaddingProperty);
        }

        protected virtual void OnParentChanged()
        {
            this.InvokeCoerceInheritableProperties();
            base.CoerceValue(CaptionAlignModeProperty);
            base.CoerceValue(CaptionWidthProperty);
            base.CoerceValue(IsTabPageProperty);
            if ((this.Parent != null) && (!ReferenceEquals(this.Parent.SelectedItem, this) && this.IsSelectedItem))
            {
                this.Parent.SetSelection(this, this.IsSelectedItem);
            }
            base.CoerceValue(TabCaptionWidthProperty);
            this.UpdateIsFloatingRootItem();
            base.CoerceValue(IsVisibleCoreProperty);
            base.CoerceValue(ActualAppearanceProperty);
            this.CheckClosedState();
            base.CoerceValue(CaptionImageProperty);
            base.CoerceValue(UIElement.IsEnabledProperty);
        }

        protected virtual void OnParentChanged(LayoutGroup oldParent, LayoutGroup newParent)
        {
            this.OnParentChanged();
        }

        protected internal virtual void OnParentItemsChanged()
        {
            base.CoerceValue(CaptionAlignModeProperty);
            base.CoerceValue(CaptionWidthProperty);
            base.CoerceValue(TabCaptionWidthProperty);
            this.UpdateIsFloatingRootItem();
            this.ParentLockHelper.DoWhenUnlocked(() => this.RootGroup = this.GetRoot());
        }

        protected internal virtual void OnParentLoaded()
        {
        }

        protected internal virtual void OnParentUnloaded()
        {
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            this.OnRenderSizeChangedCore(sizeInfo);
            this.ResizeLockHelper.Unlock();
        }

        protected virtual void OnRenderSizeChangedCore(SizeChangedInfo sizeInfo)
        {
        }

        protected virtual void OnRootGroupChanged(LayoutGroup oldValue, LayoutGroup newValue)
        {
        }

        internal virtual void OnSerializationComplete()
        {
        }

        internal virtual void OnSerializationStarted()
        {
            this.CollectSerializationInfo();
        }

        protected virtual void OnShowCaptionChanged(bool value)
        {
            base.CoerceValue(IsCaptionVisibleProperty);
            base.CoerceValue(IsCaptionImageVisibleProperty);
        }

        protected virtual void OnShowCaptionImageChanged(bool value)
        {
            base.CoerceValue(IsCaptionImageVisibleProperty);
        }

        protected virtual void OnShowCloseButtonChanged(bool show)
        {
            base.CoerceValue(IsCloseButtonVisibleProperty);
        }

        protected virtual void OnShowControlBoxChanged(bool value)
        {
            base.CoerceValue(IsControlBoxVisibleProperty);
            base.CoerceValue(IsCloseButtonVisibleProperty);
        }

        protected virtual void OnShowDropDownButtonChanged(bool show)
        {
            base.CoerceValue(IsDropDownButtonVisibleProperty);
        }

        protected virtual void OnShowMaximizeButtonChanged(bool show)
        {
            base.CoerceValue(IsMaximizeButtonVisibleProperty);
        }

        protected virtual void OnShowMinimizeButtonChanged(bool show)
        {
            base.CoerceValue(IsMinimizeButtonVisibleProperty);
        }

        protected virtual void OnShowPinButtonChanged(bool show)
        {
            base.CoerceValue(IsPinButtonVisibleProperty);
        }

        protected virtual void OnShowRestoreButtonChanged(bool show)
        {
            base.CoerceValue(IsRestoreButtonVisibleProperty);
        }

        protected virtual void OnShowScrollNextButtonChanged(bool show)
        {
            base.CoerceValue(IsScrollNextButtonVisibleProperty);
        }

        protected virtual void OnShowScrollPrevButtonChanged(bool show)
        {
            base.CoerceValue(IsScrollPrevButtonVisibleProperty);
        }

        private void OnStyleChangedOverride(Style oldValue, Style newValue)
        {
            this._Style = newValue;
        }

        protected virtual void OnTabCaptionChanged(object tabCaption)
        {
            base.CoerceValue(ActualTabCaptionProperty);
        }

        protected virtual void OnTabCaptionFormatChanged(string captionFormat)
        {
            base.CoerceValue(ActualTabCaptionProperty);
        }

        protected virtual void OnTabCaptionTemplateChanged(DataTemplate oldValue, DataTemplate newValue)
        {
            this.OnCaptionTemplatePropertyChanged();
        }

        protected virtual void OnTabCaptionTemplateSelectorChanged(DataTemplateSelector oldValue, DataTemplateSelector newValue)
        {
            this.OnCaptionTemplatePropertyChanged();
        }

        protected virtual void OnTabCaptionWidthChanged(double width)
        {
            if (this.IsTabPage)
            {
                this.InvalidateTabHeader();
            }
        }

        protected internal virtual void OnThemeChanged(ThemeChangedRoutedEventArgs e)
        {
            this.themeChangedLocker.Unlock();
            if (!EnvironmentHelper.IsNet45OrNewer)
            {
                this.LockInLogicalTree();
                base.Dispatcher.BeginInvoke(new Action(this.UnlockItemInLogicalTree), DispatcherPriority.Loaded, new object[0]);
            }
        }

        protected internal virtual void OnThemeChanging(ThemeChangingRoutedEventArgs e)
        {
            this.themeChangedLocker.Lock();
        }

        internal virtual void OnUIChildrenCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
        }

        protected virtual void OnVisibilityChanged(Visibility visibility)
        {
            DockLayoutManager container = this.FindDockLayoutManager();
            DevExpress.Xpf.Docking.InvokeHelper.BeginInvoke(this, () => (container != null) && container.IsThemeChangedLocked, () => this.OnVisibilityChangedOverride(visibility), DispatcherPriority.Normal);
        }

        protected virtual void OnVisibilityChangedOverride(Visibility visibility)
        {
            this.SetIsVisible(visibility == Visibility.Visible);
            if (this.Parent != null)
            {
                this.Parent.OnItemVisibilityChanged(this, visibility);
            }
        }

        protected override void OnVisualParentChanged(DependencyObject oldParent)
        {
            base.OnVisualParentChanged(oldParent);
            if (base.VisualParent != null)
            {
                this.EnsureUIScope();
                if (this.IsFloating)
                {
                    this.Manager.Do<DockLayoutManager>(x => x.InvalidateView(this.GetRoot()));
                }
            }
        }

        protected virtual void OnWidthChanged(GridLength value, GridLength prevValue)
        {
            if (this.Manager != null)
            {
                this.Manager.RaiseItemSizeChangedEvent(this, true, value, prevValue);
            }
        }

        protected override void OnWidthInternalChanged(double oldValue, double newValue)
        {
            throw new NotSupportedException(DockLayoutManagerHelper.GetRule(DockLayoutManagerRule.WidthIsNotSupported));
        }

        protected virtual void OnZIndexChanged()
        {
        }

        internal virtual void PrepareForModification(bool isDeserializing)
        {
            if (isDeserializing)
            {
                PlaceHolderHelper.ClearPlaceHolder(this);
                this.ManagerReference = new WeakReference(this.Manager);
            }
        }

        protected void RaiseGeometryChanged()
        {
            if (this.GeometryChanged != null)
            {
                this.GeometryChanged(this, EventArgs.Empty);
            }
        }

        private void RaiseIsVisibleChanged(bool isVisible)
        {
            if (this.Manager != null)
            {
                this.Manager.RaiseEvent(new ItemIsVisibleChangedEventArgs(this, isVisible));
            }
        }

        protected void RaiseVisualChanged()
        {
            if (this.VisualChanged != null)
            {
                this.VisualChanged(this, EventArgs.Empty);
            }
        }

        protected void RegisterName()
        {
        }

        protected internal void RemoveLogicalChild(object child)
        {
            this.logicalChildren.Remove(child);
            base.RemoveLogicalChild(child);
        }

        protected internal virtual void SelectTemplate()
        {
            if (this.PartMultiTemplateControl != null)
            {
                this.PartMultiTemplateControl.LayoutItem = this;
            }
            this.EnsureUIScope();
        }

        protected internal virtual void SelectTemplateIfNeeded()
        {
            this.SelectTemplate();
        }

        protected internal void SetActive(bool value)
        {
            this.lockActivation++;
            if (this.IsActive != value)
            {
                this.IsActive = value;
            }
            this.lockActivation--;
        }

        protected internal virtual void SetAutoHidden(bool autoHidden)
        {
        }

        protected internal void SetClosed(bool value)
        {
            this.IsClosed = value;
            base.SetCurrentValue(ClosedProperty, value);
        }

        internal virtual void SetFloatState(FloatState state)
        {
        }

        protected internal virtual void SetHidden(bool value, LayoutGroup customizationRoot)
        {
            this.IsHidden = value;
            this.parentCore = customizationRoot;
        }

        private void SetIsVisible(bool value)
        {
            if (this.isVisibleCore != value)
            {
                this.isVisibleCore = value;
                base.CoerceValue(IsVisibleCoreProperty);
            }
        }

        protected internal void SetSelected(bool value)
        {
            this.IsSelected = value;
        }

        protected internal virtual void SetSelected(DockLayoutManager manager, bool value)
        {
            bool flag = ReferenceEquals(this.Manager, null);
            if (flag)
            {
                this.Manager = manager;
            }
            this.IsSelected = value;
            if (flag)
            {
                this.Manager = null;
            }
        }

        internal IDisposable SuspendLayoutChange() => 
            this.layoutChangeLocker.Lock();

        protected internal virtual bool ToggleTabPinStatus() => 
            false;

        private void TryCancelActivation(BaseLayoutItem activeItem)
        {
            try
            {
                if (this.IsActive)
                {
                    this.activationCancelLocker.Lock();
                    if (!ReferenceEquals(activeItem, this))
                    {
                        base.SetCurrentValue(IsActiveProperty, false);
                    }
                    this.activationCancelLocker.Unlock();
                }
            }
            finally
            {
                this.isActivationCancelledLocker.Unlock();
            }
        }

        protected void TryToActivate()
        {
            DockLayoutManager dockManager = this.Manager;
            if (dockManager == null)
            {
                LayoutGroup item = this.Parent ?? (LogicalTreeHelper.GetParent(this) as LayoutGroup);
                if (item != null)
                {
                    dockManager = item.GetRoot().GetDockLayoutManager();
                }
            }
            if ((!this.IsActivationLocked && (dockManager != null)) && !this.LogicalTreeLockHelper.IsLocked)
            {
                if (!this.IsActive)
                {
                    dockManager.Deactivate(this);
                }
                else
                {
                    dockManager.Activate(this);
                    if (this.IsFloating)
                    {
                        Action action = () => dockManager.BringToFront(this);
                        DevExpress.Xpf.Docking.InvokeHelper.BeginInvoke(this, () => !base.IsLoaded, action, DispatcherPriority.Normal);
                    }
                }
            }
        }

        private void UnlockDockItemState()
        {
            int num = this.lockDockItemState - 1;
            this.lockDockItemState = num;
            if ((num == 0) && (this.lockDockItemStateCount > 0))
            {
                this.InvokeCoerceDockItemState();
            }
        }

        protected internal virtual void UnlockInheritableProperties()
        {
            this.inheritablePropertiesLocker.Unlock();
            if (!this.inheritablePropertiesLocker && (this.lockInheritablePropertiesCount > 0))
            {
                this.InvokeCoerceInheritableProperties();
            }
        }

        protected internal virtual void UnlockItemInLogicalTree()
        {
            this.UnlockInheritableProperties();
            this.UnlockDockItemState();
            this.LogicalTreeLockHelper.Unlock();
        }

        protected internal virtual void UnlockLogicalTree()
        {
        }

        protected virtual void UnlockLogicalTreeCore()
        {
        }

        protected void UpdateActualCustomizationCaption()
        {
            bool flag = this.IsPropertySet(CustomizationCaptionProperty);
            if (!flag && this.ShouldUseCaptionTemplate)
            {
                this.ActualCustomizationCaption = this.GetActualCustomizationCaption();
                this.ActualCustomizationCaptionTemplate = this.GetActualCustmizationCaptionTemplate();
                this.ActualCustomizationCaptionTemplateSelector = this.GetActualCustomizationCaptionTemplateSelector();
            }
            else
            {
                this.ActualCustomizationCaption = (flag || string.IsNullOrEmpty(this.ActualCaption)) ? this.CustomizationCaption : this.ActualCaption;
                this.ActualCustomizationCaptionTemplate = null;
                this.ActualCustomizationCaptionTemplateSelector = null;
            }
        }

        protected internal void UpdateAutoHideSituation()
        {
            this.UpdateAutoHideSituation(AutoHideType.Default);
        }

        protected internal void UpdateAutoHideSituation(AutoHideType type)
        {
            this.DockSituation.AutoHideType = type;
        }

        protected internal virtual void UpdateButtons()
        {
        }

        protected internal void UpdateDockSituation(BaseLayoutItem dockTarget, DockType type)
        {
            if (this.DockSituation == null)
            {
                this.DockSituation = new DevExpress.Xpf.Docking.DockSituation(type, dockTarget);
            }
            else
            {
                this.DockSituation.Type = type;
                this.DockSituation.DockTarget = dockTarget;
            }
        }

        protected internal void UpdateDockSituation(DevExpress.Xpf.Docking.DockSituation situation, BaseLayoutItem dockTarget, BaseLayoutItem originalItem = null)
        {
            if ((this.DockSituation == null) || (situation == null))
            {
                this.DockSituation = situation;
            }
            else
            {
                this.DockSituation.Type = situation.Type;
                if (!this.DockSituation.Width.IsAbsolute || situation.Width.IsAbsolute)
                {
                    this.DockSituation.Width = situation.Width;
                }
                if (!this.DockSituation.Height.IsAbsolute || situation.Height.IsAbsolute)
                {
                    this.DockSituation.Height = situation.Height;
                }
            }
            if (this.DockSituation != null)
            {
                this.DockSituation.DockTarget = dockTarget;
            }
        }

        protected internal virtual void UpdateFloatState()
        {
            this.ParentLockHelper.DoWhenUnlocked(delegate {
                Action<FloatGroup> action = <>c.<>9__672_1;
                if (<>c.<>9__672_1 == null)
                {
                    Action<FloatGroup> local1 = <>c.<>9__672_1;
                    action = <>c.<>9__672_1 = x => x.UpdateFloatState();
                }
                (this.GetRoot() as FloatGroup).Do<FloatGroup>(action);
            });
        }

        protected virtual void UpdateHideFromSearch()
        {
            BarItemSearchSettings.SetHideFromSearch(this, this.IsClosed || (this.IsTabPage && !this.IsSelectedItem));
        }

        protected void UpdateIsFloatingRootItem()
        {
            this.IsFloatingRootItem = (this.Parent is FloatGroup) && (this.Parent.Items.Count == 1);
        }

        protected internal virtual void UpdateSizeToContent()
        {
            this.ParentLockHelper.DoWhenUnlocked(delegate {
                Action<FloatGroup> action = <>c.<>9__673_1;
                if (<>c.<>9__673_1 == null)
                {
                    Action<FloatGroup> local1 = <>c.<>9__673_1;
                    action = <>c.<>9__673_1 = x => x.UpdateSizeToContent();
                }
                (this.GetRoot() as FloatGroup).Do<FloatGroup>(action);
            });
        }

        protected virtual void UpdateVisualState()
        {
            VisualStateManager.GoToState(this, this.IsActive ? "Active" : "Inactive", false);
        }

        internal virtual void UpdateWindowTaskbarIcon()
        {
            if (this.IsFloating)
            {
                this.GetRoot().UpdateWindowTaskbarIcon();
            }
        }

        internal virtual void UpdateWindowTaskbarTitle()
        {
            if (this.IsFloating)
            {
                this.GetRoot().UpdateWindowTaskbarTitle();
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), Category("Caption")]
        public string ActualCaption =>
            (string) base.GetValue(ActualCaptionProperty);

        [Description("Gets the actual width of the item's caption.This is a dependency property."), Category("Caption")]
        public double ActualCaptionWidth
        {
            get => 
                (double) base.GetValue(ActualCaptionWidthProperty);
            internal set => 
                base.SetValue(ActualCaptionWidthPropertyKey, value);
        }

        [Description("Gets the actual margins (outer indents) for the current item.This is a dependency property.")]
        public Thickness ActualMargin
        {
            get => 
                (Thickness) base.GetValue(ActualMarginProperty);
            private set => 
                base.SetValue(ActualMarginPropertyKey, value);
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Size ActualMaxSize =>
            (Size) base.GetValue(ActualMaxSizeProperty);

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Size ActualMinSize =>
            (Size) base.GetValue(ActualMinSizeProperty);

        [Description("Gets the actual padding (inner indents) for the current item.This is a dependency property.")]
        public Thickness ActualPadding
        {
            get => 
                (Thickness) base.GetValue(ActualPaddingProperty);
            private set => 
                base.SetValue(ActualPaddingPropertyKey, value);
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), Category("TabHeader")]
        public string ActualTabCaption =>
            (string) base.GetValue(ActualTabCaptionProperty);

        [Description("Gets or sets whether the item can be activated.This is a dependency property."), XtraSerializableProperty, Category("Behavior")]
        public bool AllowActivate
        {
            get => 
                (bool) base.GetValue(AllowActivateProperty);
            set => 
                base.SetValue(AllowActivateProperty, value);
        }

        [Description("Gets or sets whether the current dock item can be closed.This is a dependency property."), XtraSerializableProperty, Category("Behavior")]
        public bool AllowClose
        {
            get => 
                (bool) base.GetValue(AllowCloseProperty);
            set => 
                base.SetValue(AllowCloseProperty, value);
        }

        [Description("Gets or sets whether a context menu is enabled for the current layout item.This is a dependency property."), XtraSerializableProperty, Category("Behavior")]
        public bool AllowContextMenu
        {
            get => 
                (bool) base.GetValue(AllowContextMenuProperty);
            set => 
                base.SetValue(AllowContextMenuProperty, value);
        }

        [Description("Gets or sets whether the dock item can be docked to another item (panel or group).This is a dependency property."), XtraSerializableProperty, Category("Behavior")]
        public bool AllowDock
        {
            get => 
                (bool) base.GetValue(AllowDockProperty);
            set => 
                base.SetValue(AllowDockProperty, value);
        }

        public bool AllowDockToCurrentItem
        {
            get => 
                (bool) base.GetValue(AllowDockToCurrentItemProperty);
            set => 
                base.SetValue(AllowDockToCurrentItemProperty, value);
        }

        [Description("Gets or sets whether the layout item may be dragged.This is a dependency property."), XtraSerializableProperty, Category("Behavior")]
        public bool AllowDrag
        {
            get => 
                (bool) base.GetValue(AllowDragProperty);
            set => 
                base.SetValue(AllowDragProperty, value);
        }

        [Description("Gets or sets whether the dock item can float.This is a dependency property."), XtraSerializableProperty, Category("Behavior")]
        public bool AllowFloat
        {
            get => 
                (bool) base.GetValue(AllowFloatProperty);
            set => 
                base.SetValue(AllowFloatProperty, value);
        }

        [Description("Gets or sets whether the item can be hidden (auto-hidden, for dock items).This is a dependency property."), XtraSerializableProperty, Category("Behavior")]
        public bool AllowHide
        {
            get => 
                (bool) base.GetValue(AllowHideProperty);
            set => 
                base.SetValue(AllowHideProperty, value);
        }

        [Description("Gets or sets whether the current item can be maximized. This property is supported for floating LayoutPanel and DocumentPanel objects."), XtraSerializableProperty, Category("Behavior")]
        public bool AllowMaximize
        {
            get => 
                (bool) base.GetValue(AllowMaximizeProperty);
            set => 
                base.SetValue(AllowMaximizeProperty, value);
        }

        [Description("Gets or sets whether the current item can be minimized. This property is supported for floating DocumentPanel objects."), XtraSerializableProperty, Category("Behavior")]
        public bool AllowMinimize
        {
            get => 
                (bool) base.GetValue(AllowMinimizeProperty);
            set => 
                base.SetValue(AllowMinimizeProperty, value);
        }

        [Description("Gets or sets whether the item is allowed to be moved.This is a dependency property."), XtraSerializableProperty, Category("Behavior")]
        public bool AllowMove
        {
            get => 
                (bool) base.GetValue(AllowMoveProperty);
            set => 
                base.SetValue(AllowMoveProperty, value);
        }

        [Description("Allows you to prevent an item's caption from being renamed when the DockLayoutManager.AllowLayoutItemRename option is enabled.This is a dependency property."), XtraSerializableProperty, Category("Behavior")]
        public bool AllowRename
        {
            get => 
                (bool) base.GetValue(AllowRenameProperty);
            set => 
                base.SetValue(AllowRenameProperty, value);
        }

        [Description("Gets or sets whether the item can be restored from the hidden state.This is a dependency property."), XtraSerializableProperty, Category("Behavior")]
        public bool AllowRestore
        {
            get => 
                (bool) base.GetValue(AllowRestoreProperty);
            set => 
                base.SetValue(AllowRestoreProperty, value);
        }

        [Description("Gets or sets whether the current layout item can be selected in Customization Mode.This is a dependency property."), XtraSerializableProperty, Category("Behavior")]
        public bool AllowSelection
        {
            get => 
                (bool) base.GetValue(AllowSelectionProperty);
            set => 
                base.SetValue(AllowSelectionProperty, value);
        }

        [Description("Gets or sets if the BaseLayoutItem resizing at runtime is enabled."), XtraSerializableProperty, Category("Behavior")]
        public bool AllowSizing
        {
            get => 
                (bool) base.GetValue(AllowSizingProperty);
            set => 
                base.SetValue(AllowSizingProperty, value);
        }

        [Description("Gets or sets the object that provides appearance settings for the item's captions.This is a dependency property."), Category("Caption")]
        public DevExpress.Xpf.Docking.Appearance Appearance
        {
            get => 
                (DevExpress.Xpf.Docking.Appearance) base.GetValue(AppearanceProperty);
            set => 
                base.SetValue(AppearanceProperty, value);
        }

        [Description("Gets or sets the layout item's background color."), XtraSerializableProperty, Category("Content")]
        public Brush Background
        {
            get => 
                (Brush) base.GetValue(Control.BackgroundProperty);
            set => 
                base.SetValue(Control.BackgroundProperty, value);
        }

        public string BindableName
        {
            get => 
                (string) base.GetValue(BindableNameProperty);
            set => 
                base.SetValue(BindableNameProperty, value);
        }

        [Description("Gets or sets the layout item's caption."), XtraSerializableProperty(2), Category("Caption"), TypeConverter(typeof(StringConverter))]
        public object Caption
        {
            get => 
                base.GetValue(CaptionProperty);
            set => 
                base.SetValue(CaptionProperty, value);
        }

        [Description("Gets or sets the alignment settings of a control(s) displayed by a LayoutControlItem object(s).This is a dependency property."), XtraSerializableProperty, Category("Caption")]
        public DevExpress.Xpf.Docking.CaptionAlignMode CaptionAlignMode
        {
            get => 
                (DevExpress.Xpf.Docking.CaptionAlignMode) base.GetValue(CaptionAlignModeProperty);
            set => 
                base.SetValue(CaptionAlignModeProperty, value);
        }

        [Description("Gets or sets the format string used to format the layout item's caption.This is a dependency property."), XtraSerializableProperty, Category("Caption")]
        public string CaptionFormat
        {
            get => 
                (string) base.GetValue(CaptionFormatProperty);
            set => 
                base.SetValue(CaptionFormatProperty, value);
        }

        [Description("Gets or sets the horizontal alignment of the item's caption.This is a dependency property."), XtraSerializableProperty, Category("Caption")]
        public HorizontalAlignment CaptionHorizontalAlignment
        {
            get => 
                (HorizontalAlignment) base.GetValue(CaptionHorizontalAlignmentProperty);
            set => 
                base.SetValue(CaptionHorizontalAlignmentProperty, value);
        }

        [Description("Gets or sets the image displayed within the item's caption.This is a dependency property."), Category("Caption")]
        public ImageSource CaptionImage
        {
            get => 
                (ImageSource) base.GetValue(CaptionImageProperty);
            set => 
                base.SetValue(CaptionImageProperty, value);
        }

        [Description("Gets or sets the relative position of an image within the item's caption. This is a dependency property."), XtraSerializableProperty, Category("Caption")]
        public ImageLocation CaptionImageLocation
        {
            get => 
                (ImageLocation) base.GetValue(CaptionImageLocationProperty);
            set => 
                base.SetValue(CaptionImageLocationProperty, value);
        }

        [Description("Gets or sets the position of the item's caption.This is a dependency property."), XtraSerializableProperty, Category("Caption")]
        public DevExpress.Xpf.Docking.CaptionLocation CaptionLocation
        {
            get => 
                (DevExpress.Xpf.Docking.CaptionLocation) base.GetValue(CaptionLocationProperty);
            set => 
                base.SetValue(CaptionLocationProperty, value);
        }

        [Description("Gets or sets the template used to visualize the current item's BaseLayoutItem.Caption.")]
        public DataTemplate CaptionTemplate
        {
            get => 
                (DataTemplate) base.GetValue(CaptionTemplateProperty);
            set => 
                base.SetValue(CaptionTemplateProperty, value);
        }

        [Description("")]
        public DataTemplateSelector CaptionTemplateSelector
        {
            get => 
                (DataTemplateSelector) base.GetValue(CaptionTemplateSelectorProperty);
            set => 
                base.SetValue(CaptionTemplateSelectorProperty, value);
        }

        [Description("Gets or sets the vertical alignment of the item's caption.This is a dependency property."), XtraSerializableProperty, Category("Caption")]
        public VerticalAlignment CaptionVerticalAlignment
        {
            get => 
                (VerticalAlignment) base.GetValue(CaptionVerticalAlignmentProperty);
            set => 
                base.SetValue(CaptionVerticalAlignmentProperty, value);
        }

        [Description("Gets or sets the width of the item's caption, which is in effect when the CaptionAlignMode property is set to Custom.This is a dependency property."), XtraSerializableProperty, Category("Caption")]
        public double CaptionWidth
        {
            get => 
                (double) base.GetValue(CaptionWidthProperty);
            set => 
                base.SetValue(CaptionWidthProperty, value);
        }

        [Description("Gets or sets a command executed when the current item's close button ('x') is clicked. This is a dependency property."), Category("Behavior")]
        public ICommand CloseCommand
        {
            get => 
                (ICommand) base.GetValue(CloseCommandProperty);
            set => 
                base.SetValue(CloseCommandProperty, value);
        }

        [Description("Gets or sets the parameter to pass to the BaseLayoutItem.CloseCommand. This is a dependency property."), Category("Behavior")]
        public object CloseCommandParameter
        {
            get => 
                base.GetValue(CloseCommandParameterProperty);
            set => 
                base.SetValue(CloseCommandParameterProperty, value);
        }

        [Description("Gets or sets whether a Dock Item is closed.This is a dependency property.")]
        public bool Closed
        {
            get => 
                (bool) base.GetValue(ClosedProperty);
            set => 
                base.SetValue(ClosedProperty, value);
        }

        [Description("Gets or sets the way the current item acts if closed."), Category("Behavior")]
        public DevExpress.Xpf.Docking.ClosingBehavior ClosingBehavior
        {
            get => 
                (DevExpress.Xpf.Docking.ClosingBehavior) base.GetValue(ClosingBehaviorProperty);
            set => 
                base.SetValue(ClosingBehaviorProperty, value);
        }

        public ObservableCollection<IControllerAction> ContextMenuCustomizations =>
            this.ContextMenuActionGroup.Actions;

        public DataTemplate ContextMenuCustomizationsTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ContextMenuCustomizationsTemplateProperty);
            set => 
                base.SetValue(ContextMenuCustomizationsTemplateProperty, value);
        }

        [Description("Gets or sets the content of the control box region.This is a dependency property."), Category("Content")]
        public object ControlBoxContent
        {
            get => 
                base.GetValue(ControlBoxContentProperty);
            set => 
                base.SetValue(ControlBoxContentProperty, value);
        }

        [Description("Gets or sets the template that defines how the object assigned to the BaseLayoutItem.ControlBoxContent property is represented onscreen. This is a dependency property."), Category("Content")]
        public DataTemplate ControlBoxContentTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ControlBoxContentTemplateProperty);
            set => 
                base.SetValue(ControlBoxContentTemplateProperty, value);
        }

        [Description("Gets or sets the caption that represents the current item within the Customization Window.This is a dependency property."), XtraSerializableProperty, Category("Caption")]
        public string CustomizationCaption
        {
            get => 
                (string) base.GetValue(CustomizationCaptionProperty);
            set => 
                base.SetValue(CustomizationCaptionProperty, value);
        }

        [Description("Gets or sets the item's description displayed within the header of the Document Selector window.This is a dependency property."), XtraSerializableProperty]
        public string Description
        {
            get => 
                (string) base.GetValue(DescriptionProperty);
            set => 
                base.SetValue(DescriptionProperty, value);
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public double DesiredCaptionWidth
        {
            get => 
                (double) base.GetValue(DesiredCaptionWidthProperty);
            internal set => 
                base.SetValue(DesiredCaptionWidthPropertyKey, value);
        }

        [Description("Gets or sets whether an end-user can double-click the item's caption to float it.This is a dependency property."), XtraSerializableProperty, Category("Behavior")]
        public bool FloatOnDoubleClick
        {
            get => 
                (bool) base.GetValue(FloatOnDoubleClickProperty);
            set => 
                base.SetValue(FloatOnDoubleClickProperty, value);
        }

        [Description("Gets or sets the size of the item when it is floating.This is a dependency property."), XtraSerializableProperty, Category("Layout")]
        public Size FloatSize
        {
            get => 
                (Size) base.GetValue(FloatSizeProperty);
            set => 
                base.SetValue(FloatSizeProperty, value);
        }

        [Description("Gets or sets the item's description displayed within the footer of the Document Selector window.This is a dependency property."), XtraSerializableProperty]
        public string FooterDescription
        {
            get => 
                (string) base.GetValue(FooterDescriptionProperty);
            set => 
                base.SetValue(FooterDescriptionProperty, value);
        }

        [Description("Gets whether a non-empty caption is assigned to the BaseLayoutItem.Caption property.This is a dependency property.")]
        public bool HasCaption
        {
            get => 
                (bool) base.GetValue(HasCaptionProperty);
            private set => 
                base.SetValue(HasCaptionPropertyKey, value);
        }

        [Description("Gets whether a DataTemplate is used to render the caption. This is a dependency property.")]
        public bool HasCaptionTemplate
        {
            get => 
                (bool) base.GetValue(HasCaptionTemplateProperty);
            private set => 
                base.SetValue(HasCaptionTemplatePropertyKey, value);
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool HasDesiredCaptionWidth =>
            (bool) base.GetValue(HasDesiredCaptionWidthProperty);

        [Description("Gets whether a caption image is assigned to the BaseLayoutItem.CaptionImage property.This is a dependency property.")]
        public bool HasImage
        {
            get => 
                (bool) base.GetValue(HasImageProperty);
            private set => 
                base.SetValue(HasImagePropertyKey, value);
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), Category("TabHeader")]
        public bool HasTabCaption =>
            (bool) base.GetValue(HasTabCaptionProperty);

        [Description("Gets or sets whether a bar can be dropped onto the bar container displayed in the current panel.This is a dependency property."), XtraSerializableProperty]
        public bool? HeaderBarContainerControlAllowDrop
        {
            get => 
                (bool?) base.GetValue(HeaderBarContainerControlAllowDropProperty);
            set => 
                base.SetValue(HeaderBarContainerControlAllowDropProperty, value);
        }

        [Description("Gets or sets the name of the bar container, used to embed bars from the DXBars library.This is a dependency property."), XtraSerializableProperty]
        public string HeaderBarContainerControlName
        {
            get => 
                (string) base.GetValue(HeaderBarContainerControlNameProperty);
            set => 
                base.SetValue(HeaderBarContainerControlNameProperty, value);
        }

        [Description("Gets or sets the distance between the item's caption and image. This is a dependency property."), XtraSerializableProperty, Category("Caption")]
        public double ImageToTextDistance
        {
            get => 
                (double) base.GetValue(ImageToTextDistanceProperty);
            set => 
                base.SetValue(ImageToTextDistanceProperty, value);
        }

        [Description("Gets or sets whether the item is active.This is a dependency property."), XtraSerializableProperty]
        public bool IsActive
        {
            get => 
                (bool) base.GetValue(IsActiveProperty);
            set => 
                base.SetValue(IsActiveProperty, value);
        }

        [Description("Gets whether the item is auto-hidden.")]
        public bool IsAutoHidden =>
            this.GetIsAutoHidden();

        [Description("Gets whether the caption image (BaseLayoutItem.CaptionImage) is visible.This is a dependency property.")]
        public bool IsCaptionImageVisible =>
            (bool) base.GetValue(IsCaptionImageVisibleProperty);

        [Description("Gets whether the item's caption is visible.This is a dependency property.")]
        public bool IsCaptionVisible =>
            (bool) base.GetValue(IsCaptionVisibleProperty);

        [Description("Gets whether the Close ('x') button is visible for the current item.This is a dependency property.")]
        public bool IsCloseButtonVisible =>
            (bool) base.GetValue(IsCloseButtonVisibleProperty);

        [Description("Gets whether the item is closed.This is a dependency property.")]
        public bool IsClosed
        {
            get => 
                (bool) base.GetValue(IsClosedProperty);
            private set => 
                base.SetValue(IsClosedPropertyKey, value);
        }

        [Description("Gets whether the control box region is visible.This is a dependency property.")]
        public bool IsControlBoxVisible =>
            (bool) base.GetValue(IsControlBoxVisibleProperty);

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsControlItemsHost
        {
            get => 
                (bool) base.GetValue(IsControlItemsHostProperty);
            private set => 
                base.SetValue(IsControlItemsHostPropertyKey, value);
        }

        [Description("Gets whether the item floats.")]
        public bool IsFloating =>
            this.GetRoot() is FloatGroup;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsFloatingRootItem
        {
            get => 
                (bool) base.GetValue(IsFloatingRootItemProperty);
            private set => 
                base.SetValue(IsFloatingRootItemPropertyKey, value);
        }

        [Description("Gets whether the current Layout Item is hidden.This is a dependency property.")]
        public bool IsHidden
        {
            get => 
                (bool) base.GetValue(IsHiddenProperty);
            private set => 
                base.SetValue(IsHiddenPropertyKey, value);
        }

        [Description("Gets whether the item is being initialized.")]
        public bool IsInitializing =>
            this.initCounter > 0;

        [Description("Gets whether the item is selected in Customization Mode.This is a dependency property.")]
        public bool IsSelected
        {
            get => 
                (bool) base.GetValue(IsSelectedProperty);
            private set => 
                base.SetValue(IsSelectedPropertyKey, value);
        }

        [Description("Gets or sets whether the current item is selected within any LayoutGroup. This is a dependency property.")]
        public bool IsSelectedItem
        {
            get => 
                (bool) base.GetValue(IsSelectedItemProperty);
            set => 
                base.SetValue(IsSelectedItemProperty, value);
        }

        [Description("Gets whether the current item is represented as a tab page.This is a dependency property.")]
        public bool IsTabPage
        {
            get => 
                (bool) base.GetValue(IsTabPageProperty);
            private set => 
                base.SetValue(IsTabPagePropertyKey, value);
        }

        [Description(""), XtraSerializableProperty, Category("Layout")]
        public GridLength ItemHeight
        {
            get => 
                (GridLength) base.GetValue(ItemHeightProperty);
            set => 
                base.SetValue(ItemHeightProperty, value);
        }

        [Description("Gets the current item's type.")]
        public LayoutItemType ItemType =>
            this.GetLayoutItemTypeCore();

        [Description(""), XtraSerializableProperty, Category("Layout")]
        public GridLength ItemWidth
        {
            get => 
                (GridLength) base.GetValue(ItemWidthProperty);
            set => 
                base.SetValue(ItemWidthProperty, value);
        }

        [Description("This member supports the internal infrastructure, and is not intended to be used directly from your code.")]
        public Size LayoutSize
        {
            get => 
                (Size) base.GetValue(LayoutSizeProperty);
            internal set => 
                base.SetValue(LayoutSizePropertyKey, value);
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public DependencyObject LogicalParent =>
            base.Parent;

        [Description("Gets or sets the outer indents of the item's borders. This is a dependency property."), XtraSerializableProperty, Category("Layout")]
        public Thickness Margin
        {
            get => 
                (Thickness) base.GetValue(MarginProperty);
            set => 
                base.SetValue(MarginProperty, value);
        }

        [Description("Gets or sets the amount of space between the item's borders and its contents.This is a dependency property."), XtraSerializableProperty, Category("Layout")]
        public Thickness Padding
        {
            get => 
                (Thickness) base.GetValue(PaddingProperty);
            set => 
                base.SetValue(PaddingProperty, value);
        }

        [Description("Gets or sets the item's parent group."), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public LayoutGroup Parent
        {
            get => 
                this.parentCore;
            internal set
            {
                if (!ReferenceEquals(this.parentCore, value))
                {
                    LayoutGroup parentCore = this.parentCore;
                    if ((this.parentCore != null) && this.parentCore.Items.Contains(this))
                    {
                        this.parentCore.Items.Remove(this);
                    }
                    this.parentCore = value;
                    if ((this.parentCore != null) && !this.parentCore.Items.Contains(this))
                    {
                        this.parentCore.Items.Add(this);
                    }
                    this.OnParentChanged(parentCore, this.parentCore);
                }
            }
        }

        [XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public PlaceHolderCollection SerializableDockSituation
        {
            get => 
                this.DockInfo.AsCollection();
            set
            {
            }
        }

        [XtraSerializableProperty(XtraSerializationVisibility.Content), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public BaseLayoutItemSerializationInfo SerializationInfo { get; private set; }

        [Description("Gets or sets whether the item's caption is visible.This is a dependency property."), XtraSerializableProperty, Category("Caption")]
        public bool ShowCaption
        {
            get => 
                (bool) base.GetValue(ShowCaptionProperty);
            set => 
                base.SetValue(ShowCaptionProperty, value);
        }

        [Description("Gets or sets whether the BaseLayoutItem.CaptionImage is visible.This is a dependency property."), XtraSerializableProperty, Category("Caption")]
        public bool ShowCaptionImage
        {
            get => 
                (bool) base.GetValue(ShowCaptionImageProperty);
            set => 
                base.SetValue(ShowCaptionImageProperty, value);
        }

        [Description("Allows you to hide the Close ('x') button for the current item. This property is only supported for LayoutPanel, DocumentPanel and DocumentGroup objects.This is a dependency property."), XtraSerializableProperty, Category("Layout")]
        public bool ShowCloseButton
        {
            get => 
                (bool) base.GetValue(ShowCloseButtonProperty);
            set => 
                base.SetValue(ShowCloseButtonProperty, value);
        }

        [Description("Gets or sets whether the object assigned to the BaseLayoutItem.ControlBoxContent property is visible.This is a dependency property."), XtraSerializableProperty, Category("Layout")]
        public bool ShowControlBox
        {
            get => 
                (bool) base.GetValue(ShowControlBoxProperty);
            set => 
                base.SetValue(ShowControlBoxProperty, value);
        }

        [Description("Get or sets whether to show an image within a tab caption of the current layout item. This is a dependency property."), Category("Caption")]
        public bool ShowTabCaptionImage
        {
            get => 
                (bool) base.GetValue(ShowTabCaptionImageProperty);
            set => 
                base.SetValue(ShowTabCaptionImageProperty, value);
        }

        [Description("Gets or sets an image displayed within the layout item caption in a tabbed layout. This is a dependency property."), Category("TabHeader")]
        public ImageSource TabCaptionImage
        {
            get => 
                (ImageSource) base.GetValue(TabCaptionImageProperty);
            set => 
                base.SetValue(TabCaptionImageProperty, value);
        }

        [Description("Gets or sets the layout item's tab caption."), Category("TabHeader"), XtraSerializableProperty(1)]
        public object TabCaption
        {
            get => 
                base.GetValue(TabCaptionProperty);
            set => 
                base.SetValue(TabCaptionProperty, value);
        }

        [Description("Gets or sets the format string used to format the layout item's tab caption.This is a dependency property."), Category("TabHeader")]
        public string TabCaptionFormat
        {
            get => 
                (string) base.GetValue(TabCaptionFormatProperty);
            set => 
                base.SetValue(TabCaptionFormatProperty, value);
        }

        public HorizontalAlignment TabCaptionHorizontalAlignment
        {
            get => 
                (HorizontalAlignment) base.GetValue(TabCaptionHorizontalAlignmentProperty);
            set => 
                base.SetValue(TabCaptionHorizontalAlignmentProperty, value);
        }

        public VerticalAlignment TabCaptionVerticalAlignment
        {
            get => 
                (VerticalAlignment) base.GetValue(TabCaptionVerticalAlignmentProperty);
            set => 
                base.SetValue(TabCaptionVerticalAlignmentProperty, value);
        }

        [Description("Gets or sets a template that defines the tab caption presentation. This is a dependency property."), Category("TabHeader")]
        public DataTemplate TabCaptionTemplate
        {
            get => 
                (DataTemplate) base.GetValue(TabCaptionTemplateProperty);
            set => 
                base.SetValue(TabCaptionTemplateProperty, value);
        }

        [Description("Gets or sets an object that chooses a tab caption template based on custom logic. This is a dependency property."), Category("TabHeader")]
        public DataTemplateSelector TabCaptionTemplateSelector
        {
            get => 
                (DataTemplateSelector) base.GetValue(TabCaptionTemplateSelectorProperty);
            set => 
                base.SetValue(TabCaptionTemplateSelectorProperty, value);
        }

        [Description("Gets or sets the width of the corresponding tab. This property is in effect when the current object represents items as tabs or when it represents one of the tabs.This is a dependency property."), Category("TabHeader")]
        public double TabCaptionWidth
        {
            get => 
                (double) base.GetValue(TabCaptionWidthProperty);
            set => 
                base.SetValue(TabCaptionWidthProperty, value);
        }

        [Description("Gets or sets text trimming options applied to the BaseLayoutItem.Caption.This is a dependency property."), XtraSerializableProperty, Category("Caption")]
        public System.Windows.TextTrimming TextTrimming
        {
            get => 
                (System.Windows.TextTrimming) base.GetValue(TextTrimmingProperty);
            set => 
                base.SetValue(TextTrimmingProperty, value);
        }

        [Description("Gets or sets text wrapping options applied to the BaseLayoutItem.Caption.This is a dependency property."), XtraSerializableProperty, Category("Caption")]
        public System.Windows.TextWrapping TextWrapping
        {
            get => 
                (System.Windows.TextWrapping) base.GetValue(TextWrappingProperty);
            set => 
                base.SetValue(TextWrappingProperty, value);
        }

        [Description("Gets or sets a tool tip, displayed at runtime when hovering a BaseLayoutItem's caption or tab caption. This is a dependency property."), XtraSerializableProperty]
        public object ToolTip
        {
            get => 
                base.GetValue(ToolTipProperty);
            set => 
                base.SetValue(ToolTipProperty, value);
        }

        public DevExpress.Xpf.Docking.DockingViewStyle DockingViewStyle
        {
            get => 
                (DevExpress.Xpf.Docking.DockingViewStyle) base.GetValue(DockingViewStyleProperty);
            private set => 
                base.SetValue(DockingViewStylePropertyKey, value);
        }

        internal DevExpress.Xpf.Docking.Appearance ActualAppearance
        {
            get => 
                (DevExpress.Xpf.Docking.Appearance) base.GetValue(ActualAppearanceProperty);
            private set => 
                base.SetValue(ActualAppearancePropertyKey, value);
        }

        internal AppearanceObject ActualAppearanceObject
        {
            get => 
                (AppearanceObject) base.GetValue(ActualAppearanceObjectProperty);
            private set => 
                base.SetValue(ActualAppearanceObjectPropertyKey, value);
        }

        internal object ActualCustomizationCaption
        {
            get => 
                base.GetValue(ActualCustomizationCaptionProperty);
            set => 
                base.SetValue(ActualCustomizationCaptionProperty, value);
        }

        internal DataTemplate ActualCustomizationCaptionTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ActualCustomizationCaptionTemplateProperty);
            set => 
                base.SetValue(ActualCustomizationCaptionTemplateProperty, value);
        }

        internal DataTemplateSelector ActualCustomizationCaptionTemplateSelector
        {
            get => 
                (DataTemplateSelector) base.GetValue(ActualCustomizationCaptionTemplateSelectorProperty);
            set => 
                base.SetValue(ActualCustomizationCaptionTemplateSelectorProperty, value);
        }

        internal string BarItemName { get; set; }

        internal ActionGroup ContextMenuActionGroup
        {
            get
            {
                ActionGroup contextMenuActionGroup = this.contextMenuActionGroup;
                if (this.contextMenuActionGroup == null)
                {
                    ActionGroup local1 = this.contextMenuActionGroup;
                    contextMenuActionGroup = this.contextMenuActionGroup = this.EnsureContextMenuActionGroup();
                }
                return contextMenuActionGroup;
            }
        }

        internal DevExpress.Xpf.Docking.Appearance DefaultAppearance
        {
            get
            {
                this._DefaultAppearance ??= new DevExpress.Xpf.Docking.Appearance();
                return this._DefaultAppearance;
            }
        }

        internal PlaceHolderInfoHelper DockInfo
        {
            get
            {
                this.dockInfo ??= new PlaceHolderInfoHelper();
                return this.dockInfo;
            }
        }

        internal DockLayoutManager DockLayoutManagerCore
        {
            get => 
                (DockLayoutManager) base.GetValue(DockLayoutManagerCoreProperty);
            set => 
                base.SetValue(DockLayoutManagerCoreProperty, value);
        }

        internal bool HasTabCaptionTemplate
        {
            get => 
                (bool) base.GetValue(HasTabCaptionTemplateProperty);
            private set => 
                base.SetValue(HasTabCaptionTemplateProperty, value);
        }

        internal Rect? InitialFloatBounds { get; set; }

        internal bool IsActivationCancelled =>
            (bool) this.isActivationCancelledLocker;

        internal bool IsDeserializing { get; private set; }

        internal bool IsLayoutTreeChangeInProgress =>
            this.GetIsLayoutTreeChangeInProgress();

        internal bool IsLogicalTreeLocked =>
            (bool) this.LogicalTreeLockHelper;

        internal bool IsPermanent =>
            this.GetIsPermanent();

        internal bool IsStyleUpdateInProgress =>
            this.GetIsStyleUpdateInProgress();

        internal bool IsThemeChangeInProgress =>
            (bool) this.themeChangedLocker;

        internal bool IsVisibleCore =>
            (bool) base.GetValue(IsVisibleCoreProperty);

        internal DevExpress.Xpf.Docking.LayoutItemData LayoutItemData
        {
            get => 
                (DevExpress.Xpf.Docking.LayoutItemData) base.GetValue(LayoutItemDataProperty);
            set => 
                base.SetValue(LayoutItemDataProperty, value);
        }

        internal LockHelper LogicalTreeLockHelper
        {
            get
            {
                this.logicalTreeLockHelper ??= new LockHelper(new LockHelper.LockHelperDelegate(this.UnlockLogicalTreeCore));
                return this.logicalTreeLockHelper;
            }
        }

        internal LockHelper ParentLockHelper
        {
            get
            {
                this.parentLockHelper ??= new LockHelper();
                return this.parentLockHelper;
            }
        }

        internal bool PreventCaptionSerialization { get; set; }

        internal LockHelper ResizeLockHelper
        {
            get
            {
                this.resizeLockHelper ??= new LockHelper();
                return this.resizeLockHelper;
            }
        }

        internal LayoutGroup RootGroup
        {
            get => 
                this._RootGroup;
            set
            {
                if (!ReferenceEquals(this._RootGroup, value))
                {
                    LayoutGroup oldValue = this._RootGroup;
                    this._RootGroup = value;
                    this.OnRootGroupChanged(oldValue, value);
                }
            }
        }

        internal virtual bool SupportsFloatOrMDIState =>
            false;

        internal virtual bool SupportsOptimizedLogicalTree =>
            false;

        internal int TabIndexBeforeRemove { get; set; }

        internal bool ShouldUseCaptionTemplate =>
            ((this.Caption == null) || ((this.Caption is string) || this.Caption.GetType().IsPrimitive)) ? this.HasCaptionTemplate : true;

        internal bool ShouldUseTabCaptionTemplate =>
            ((this.TabCaption == null) || ((this.TabCaption is string) || this.TabCaption.GetType().IsPrimitive)) ? (this.HasCaptionTemplate || this.HasTabCaptionTemplate) : true;

        internal int ZIndex
        {
            get => 
                this.zIndexCore;
            set
            {
                if (this.ZIndex != value)
                {
                    this.zIndexCore = value;
                    base.SetValue(Panel.ZIndexProperty, value);
                    this.OnZIndexChanged();
                }
            }
        }

        [Description("")]
        protected internal virtual bool IsMaximizable =>
            this.SupportsFloatOrMDIState;

        protected internal virtual bool IsMinimizable =>
            this.SupportsFloatOrMDIState;

        protected internal virtual bool IsPinnedTab =>
            false;

        protected internal bool IsTabDocument =>
            this.IsTabPage && (this.Parent is DocumentGroup);

        protected internal DateTime LastSelectionDateTime { get; set; }

        protected internal DockLayoutManager Manager
        {
            get => 
                this.managerCore;
            set
            {
                if (!ReferenceEquals(this.managerCore, value))
                {
                    DockLayoutManager managerCore = this.managerCore;
                    this.managerCore = value;
                    this.OnDockLayoutManagerChanged(managerCore, value);
                }
            }
        }

        protected internal UIChildren UIElements
        {
            get
            {
                this.uiChildren ??= new UIChildren();
                return this.uiChildren;
            }
        }

        protected DevExpress.Xpf.Docking.DockSituation DockSituation { get; set; }

        protected override bool IsEnabledCore
        {
            get
            {
                if (!base.IsEnabledCore)
                {
                    return false;
                }
                Func<LayoutGroup, bool> evaluator = <>c.<>9__583_0;
                if (<>c.<>9__583_0 == null)
                {
                    Func<LayoutGroup, bool> local1 = <>c.<>9__583_0;
                    evaluator = <>c.<>9__583_0 = x => x.IsEnabled;
                }
                return this.Parent.Return<LayoutGroup, bool>(evaluator, (<>c.<>9__583_1 ??= () => true));
            }
        }

        protected bool IsInDesignTime =>
            DesignerProperties.GetIsInDesignMode(this);

        protected bool IsTemplateApplied { get; private set; }

        protected WeakReference ManagerReference { get; private set; }

        protected MultiTemplateControl PartMultiTemplateControl { get; private set; }

        private bool AreInheritablePropertiesLocked =>
            (bool) this.inheritablePropertiesLocker;

        private bool IsActivationLocked =>
            this.lockActivation > 0;

        private bool IsDockItemStateLocked =>
            this.lockDockItemState > 0;

        BaseLayoutItem[] ISupportHierarchy<BaseLayoutItem>.Nodes =>
            this.GetNodesCore();

        BaseLayoutItem ISupportHierarchy<BaseLayoutItem>.Parent =>
            this.parentCore;

        private bool IsLayoutChangeInProgress =>
            (bool) this.layoutChangeLocker;

        protected override IEnumerator LogicalChildren
        {
            get
            {
                IEnumerator[] args = new IEnumerator[] { this.logicalChildren.ToList<object>().GetEnumerator(), base.LogicalChildren };
                return new DevExpress.Xpf.Core.Native.MergedEnumerator(args);
            }
        }

        [XtraSerializableProperty, XtraResetProperty(ResetPropertyMode.None), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string TypeName =>
            base.GetType().Name;

        [XtraSerializableProperty, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string ParentName { get; set; }

        [XtraSerializableProperty, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string ParentCollectionName { get; set; }

        IUIElement IUIElement.Scope =>
            DockLayoutManager.GetUIScope(this);

        UIChildren IUIElement.Children
        {
            get
            {
                if (this.children == null)
                {
                    this.children = this.CreateUIChildren();
                    this.children.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnUIChildrenCollectionChanged);
                }
                return this.children;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BaseLayoutItem.<>c <>9 = new BaseLayoutItem.<>c();
            public static Func<LayoutGroup, bool> <>9__583_0;
            public static Func<bool> <>9__583_1;
            public static Action<FloatGroup> <>9__672_1;
            public static Action<FloatGroup> <>9__673_1;
            public static Action<DispatcherOperation> <>9__752_0;
            public static Func<BaseLayoutItem, bool> <>9__832_0;

            internal void <.cctor>b__151_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnDataContextChanged(e.NewValue);
            }

            internal object <.cctor>b__151_1(DependencyObject dObj, object value) => 
                ((BaseLayoutItem) dObj).CoerceDataContext(value);

            internal void <.cctor>b__151_10(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnWidthChanged((GridLength) e.NewValue, (GridLength) e.OldValue);
            }

            internal object <.cctor>b__151_100(DependencyObject dObj, object value) => 
                ((BaseLayoutItem) dObj).CoerceIsMaximizeButtonVisible((bool) value);

            internal void <.cctor>b__151_101(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnIsRestoreButtonVisibleChanged((bool) e.NewValue);
            }

            internal object <.cctor>b__151_102(DependencyObject dObj, object value) => 
                ((BaseLayoutItem) dObj).CoerceIsRestoreButtonVisible((bool) value);

            internal object <.cctor>b__151_103(DependencyObject dObj, object value) => 
                ((BaseLayoutItem) dObj).CoerceIsDropDownButtonVisible((bool) value);

            internal object <.cctor>b__151_104(DependencyObject dObj, object value) => 
                ((BaseLayoutItem) dObj).CoerceIsScrollPrevButtonVisible((bool) value);

            internal object <.cctor>b__151_105(DependencyObject dObj, object value) => 
                ((BaseLayoutItem) dObj).CoerceIsScrollNextButtonVisible((bool) value);

            internal void <.cctor>b__151_106(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnBindableNameChanged((string) e.OldValue, (string) e.NewValue);
            }

            internal void <.cctor>b__151_107(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnMinHeightChanged((double) e.OldValue, (double) e.NewValue);
            }

            internal object <.cctor>b__151_108(DependencyObject dObj, object value) => 
                ((BaseLayoutItem) dObj).OnCoerceMinHeight((double) value);

            internal void <.cctor>b__151_109(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnMinWidthChanged((double) e.OldValue, (double) e.NewValue);
            }

            internal object <.cctor>b__151_11(DependencyObject dObj, object value) => 
                ((BaseLayoutItem) dObj).CoerceWidth((GridLength) value);

            internal object <.cctor>b__151_110(DependencyObject dObj, object value) => 
                ((BaseLayoutItem) dObj).OnCoerceMinWidth((double) value);

            internal void <.cctor>b__151_111(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnMaxWidthChanged((double) e.OldValue, (double) e.NewValue);
            }

            internal void <.cctor>b__151_112(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnMaxHeightChanged((double) e.OldValue, (double) e.NewValue);
            }

            internal void <.cctor>b__151_113(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) d).DockingViewStyle = (DockingViewStyle) e.NewValue;
            }

            internal void <.cctor>b__151_114(BaseLayoutItem d, DockLayoutManager oldValue, DockLayoutManager newValue)
            {
                d.OnDockLayoutManagerCoreChanged(oldValue, newValue);
            }

            internal DockLayoutManager <.cctor>b__151_115(BaseLayoutItem d, DockLayoutManager value) => 
                d.OnCoerceDockLayoutManagerCore(value);

            internal void <.cctor>b__151_116(BaseLayoutItem d, DockingViewStyle oldValue, DockingViewStyle newValue)
            {
                d.OnDockingViewStyleChanged(oldValue, newValue);
            }

            internal void <.cctor>b__151_117(BaseLayoutItem d, DataTemplate oldValue, DataTemplate newValue)
            {
                d.OnTabCaptionTemplateChanged(oldValue, newValue);
            }

            internal void <.cctor>b__151_118(BaseLayoutItem d, DataTemplateSelector oldValue, DataTemplateSelector newValue)
            {
                d.OnTabCaptionTemplateSelectorChanged(oldValue, newValue);
            }

            internal void <.cctor>b__151_12(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnHeightChanged((GridLength) e.NewValue, (GridLength) e.OldValue);
            }

            internal object <.cctor>b__151_13(DependencyObject dObj, object value) => 
                ((BaseLayoutItem) dObj).CoerceHeight((GridLength) value);

            internal void <.cctor>b__151_14(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnActualMinSizeChanged();
            }

            internal object <.cctor>b__151_15(DependencyObject dObj, object value) => 
                ((BaseLayoutItem) dObj).CoerceActualMinSize();

            internal void <.cctor>b__151_16(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnActualMaxSizeChanged();
            }

            internal object <.cctor>b__151_17(DependencyObject dObj, object value) => 
                ((BaseLayoutItem) dObj).CoerceActualMaxSize();

            internal void <.cctor>b__151_18(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnFloatSizeChanged((Size) e.OldValue, (Size) e.NewValue);
            }

            internal object <.cctor>b__151_19(DependencyObject dObj, object value) => 
                ((BaseLayoutItem) dObj).CoerceFloatSize((Size) value);

            internal void <.cctor>b__151_2(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnStyleChangedOverride((Style) e.OldValue, (Style) e.NewValue);
            }

            internal void <.cctor>b__151_20(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnNameChanged();
            }

            internal void <.cctor>b__151_21(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnCaptionChanged(e.OldValue, e.NewValue);
            }

            internal void <.cctor>b__151_22(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnCaptionFormatChanged();
            }

            internal object <.cctor>b__151_23(DependencyObject dObj, object value) => 
                ((BaseLayoutItem) dObj).CoerceCaptionFormat((string) value);

            internal void <.cctor>b__151_24(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnCustomizationCaptionChanged((string) e.OldValue, (string) e.NewValue);
            }

            internal object <.cctor>b__151_25(DependencyObject dObj, object value) => 
                ((BaseLayoutItem) dObj).CoerceCustomizationCaption((string) value);

            internal void <.cctor>b__151_26(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnCaptionLocationChanged((CaptionLocation) e.NewValue);
            }

            internal void <.cctor>b__151_27(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnCaptionAlignModeChanged((CaptionAlignMode) e.OldValue, (CaptionAlignMode) e.NewValue);
            }

            internal object <.cctor>b__151_28(DependencyObject dObj, object value) => 
                ((BaseLayoutItem) dObj).CoerceCaptionAlignMode((CaptionAlignMode) value);

            internal void <.cctor>b__151_29(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnCaptionWidthChanged((double) e.NewValue);
            }

            internal object <.cctor>b__151_3(DependencyObject dObj, object value) => 
                ((BaseLayoutItem) dObj).CoerceStyle((Style) value);

            internal object <.cctor>b__151_30(DependencyObject dObj, object value) => 
                ((BaseLayoutItem) dObj).CoerceCaptionWidth((double) value);

            internal void <.cctor>b__151_31(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnCaptionImageChanged((ImageSource) e.NewValue);
            }

            internal object <.cctor>b__151_32(DependencyObject dObj, object value) => 
                ((BaseLayoutItem) dObj).CoerceCaptionImage((ImageSource) value);

            internal void <.cctor>b__151_33(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
            {
                ((BaseLayoutItem) dObj).OnActualCaptionChanged((string) ea.NewValue);
            }

            internal object <.cctor>b__151_34(DependencyObject dObj, object value) => 
                ((BaseLayoutItem) dObj).CoerceActualCaption((string) value);

            internal void <.cctor>b__151_35(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnDesiredCaptionWidthChanged((double) e.NewValue);
            }

            internal object <.cctor>b__151_36(DependencyObject dObj, object value) => 
                ((BaseLayoutItem) dObj).CoerceActualCaptionWidth((double) value);

            internal void <.cctor>b__151_37(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnMarginChanged((Thickness) e.NewValue);
            }

            internal void <.cctor>b__151_38(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnPaddingChanged((Thickness) e.NewValue);
            }

            internal void <.cctor>b__151_39(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnActualMarginChanged((Thickness) e.NewValue);
            }

            internal void <.cctor>b__151_4(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnAppearanceChanged((Appearance) e.OldValue, (Appearance) e.NewValue);
            }

            internal object <.cctor>b__151_40(DependencyObject dObj, object value) => 
                ((BaseLayoutItem) dObj).CoerceActualMargin((Thickness) value);

            internal object <.cctor>b__151_41(DependencyObject dObj, object value) => 
                ((BaseLayoutItem) dObj).CoerceActualPadding((Thickness) value);

            internal void <.cctor>b__151_42(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnIsActiveChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__151_43(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnClosedChanged((bool) e.OldValue, (bool) e.NewValue);
            }

            internal void <.cctor>b__151_44(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) d).OnIsClosedChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__151_45(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
            {
                ((BaseLayoutItem) dObj).OnHasCaptionChanged((bool) ea.NewValue);
            }

            internal void <.cctor>b__151_46(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
            {
                ((BaseLayoutItem) dObj).OnHasCaptionTemplateChanged((bool) ea.NewValue);
            }

            internal void <.cctor>b__151_47(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnAllowDockChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__151_48(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnAllowHideChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__151_49(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnAllowCloseChanged((bool) e.NewValue);
            }

            internal object <.cctor>b__151_5(DependencyObject dObj, object value) => 
                ((BaseLayoutItem) dObj).CoerceAppearance((Appearance) value);

            internal void <.cctor>b__151_50(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnAllowRestoreChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__151_51(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnAllowMaximizeChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__151_52(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnAllowMinimizeChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__151_53(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnAllowSizingChanged((bool) e.NewValue);
            }

            internal object <.cctor>b__151_54(DependencyObject dObj, object value) => 
                ((BaseLayoutItem) dObj).CoerceAllowSizing((bool) value);

            internal void <.cctor>b__151_55(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnIsFloatingRootItemChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__151_56(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnIsSelectedChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__151_57(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnIsControlItemsHostChanged((bool) e.NewValue);
            }

            internal object <.cctor>b__151_58(DependencyObject dObj, object value) => 
                ((BaseLayoutItem) dObj).CoerceIsControlItemsHost((bool) value);

            internal void <.cctor>b__151_59(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnVisibilityChanged((Visibility) e.NewValue);
            }

            internal void <.cctor>b__151_6(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnActualAppearanceObjectChanged((AppearanceObject) e.NewValue);
            }

            internal object <.cctor>b__151_60(DependencyObject dObj, object value) => 
                ((BaseLayoutItem) dObj).CoerceVisibility((Visibility) value);

            internal void <.cctor>b__151_61(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnIsVisibleChanged((bool) e.NewValue);
            }

            internal object <.cctor>b__151_62(DependencyObject dObj, object value) => 
                ((BaseLayoutItem) dObj).CoerceIsVisible();

            internal void <.cctor>b__151_63(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnIsTabPageChanged();
            }

            internal object <.cctor>b__151_64(DependencyObject dObj, object value) => 
                ((BaseLayoutItem) dObj).CoerceIsTabPage((bool) value);

            internal void <.cctor>b__151_65(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnIsSelectedItemChanged();
            }

            internal object <.cctor>b__151_66(DependencyObject dObj, object value) => 
                ((BaseLayoutItem) dObj).CoerceIsSelectedItem((bool) value);

            internal void <.cctor>b__151_67(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnControlBoxContentChanged();
            }

            internal void <.cctor>b__151_68(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnControlBoxContentTemplateChanged();
            }

            internal void <.cctor>b__151_69(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnShowCaptionChanged((bool) e.NewValue);
            }

            internal object <.cctor>b__151_7(DependencyObject dObj, object value) => 
                ((BaseLayoutItem) dObj).CoerceActualAppearanceObject((AppearanceObject) value);

            internal void <.cctor>b__151_70(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnShowCaptionImageChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__151_71(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnShowControlBoxChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__151_72(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnShowCloseButtonChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__151_73(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnShowPinButtonChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__151_74(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnShowMinimizeButtonChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__151_75(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnShowMaximizeButtonChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__151_76(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnShowRestoreButtonChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__151_77(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnShowDropDownButtonChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__151_78(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnShowScrollPrevButtonChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__151_79(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnShowScrollNextButtonChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__151_8(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnActualAppearanceChanged((Appearance) e.OldValue, (Appearance) e.NewValue);
            }

            internal object <.cctor>b__151_80(DependencyObject dObj, object value) => 
                ((BaseLayoutItem) dObj).CoerceTabCaptionImage((ImageSource) value);

            internal void <.cctor>b__151_81(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnTabCaptionChanged(e.NewValue);
            }

            internal object <.cctor>b__151_82(DependencyObject dObj, object value) => 
                ((BaseLayoutItem) dObj).CoerceTabCaption(value);

            internal void <.cctor>b__151_83(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnTabCaptionFormatChanged((string) e.NewValue);
            }

            internal object <.cctor>b__151_84(DependencyObject dObj, object value) => 
                ((BaseLayoutItem) dObj).CoerceTabCaptionFormat((string) value);

            internal void <.cctor>b__151_85(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnActualTabCaptionChanged((string) e.NewValue);
            }

            internal object <.cctor>b__151_86(DependencyObject dObj, object value) => 
                ((BaseLayoutItem) dObj).CoerceActualTabCaption((string) value);

            internal void <.cctor>b__151_87(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnTabCaptionWidthChanged((double) e.NewValue);
            }

            internal object <.cctor>b__151_88(DependencyObject dObj, object value) => 
                ((BaseLayoutItem) dObj).CoerceTabCaptionWidth((double) value);

            internal void <.cctor>b__151_89(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnCaptionTemplateChanged((DataTemplate) e.OldValue, (DataTemplate) e.NewValue);
            }

            internal object <.cctor>b__151_9(DependencyObject dObj, object value) => 
                ((BaseLayoutItem) dObj).CoerceActualAppearance((Appearance) value);

            internal void <.cctor>b__151_90(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnCaptionTemplateSelectorChanged((DataTemplateSelector) e.OldValue, (DataTemplateSelector) e.NewValue);
            }

            internal void <.cctor>b__151_91(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
            {
                ((BaseLayoutItem) dObj).OnIsCaptionVisibleChanged((bool) ea.NewValue);
            }

            internal object <.cctor>b__151_92(DependencyObject dObj, object value) => 
                ((BaseLayoutItem) dObj).CoerceIsCaptionVisible((bool) value);

            internal object <.cctor>b__151_93(DependencyObject dObj, object value) => 
                ((BaseLayoutItem) dObj).CoerceIsCaptionImageVisible((bool) value);

            internal object <.cctor>b__151_94(DependencyObject dObj, object value) => 
                ((BaseLayoutItem) dObj).CoerceIsControlBoxVisible((bool) value);

            internal void <.cctor>b__151_95(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnIsCloseButtonVisibleChanged((bool) e.NewValue);
            }

            internal object <.cctor>b__151_96(DependencyObject dObj, object value) => 
                ((BaseLayoutItem) dObj).CoerceIsCloseButtonVisible((bool) value);

            internal object <.cctor>b__151_97(DependencyObject dObj, object value) => 
                ((BaseLayoutItem) dObj).CoerceIsPinButtonVisible((bool) value);

            internal object <.cctor>b__151_98(DependencyObject dObj, object value) => 
                ((BaseLayoutItem) dObj).CoerceIsMinimizeButtonVisible((bool) value);

            internal void <.cctor>b__151_99(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseLayoutItem) dObj).OnIsMaximizeButtonVisibleChanged((bool) e.NewValue);
            }

            internal bool <get_IsEnabledCore>b__583_0(LayoutGroup x) => 
                x.IsEnabled;

            internal bool <get_IsEnabledCore>b__583_1() => 
                true;

            internal bool <GetIsLayoutTreeChangeInProgress>b__832_0(BaseLayoutItem x) => 
                x.IsLayoutChangeInProgress || x.GetIsLogicalChildrenIterationInProgress();

            internal void <OnClosedChanged>b__752_0(DispatcherOperation x)
            {
                x.Abort();
            }

            internal void <UpdateFloatState>b__672_1(FloatGroup x)
            {
                x.UpdateFloatState();
            }

            internal void <UpdateSizeToContent>b__673_1(FloatGroup x)
            {
                x.UpdateSizeToContent();
            }
        }
    }
}

