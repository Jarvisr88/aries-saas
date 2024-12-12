namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    public class BarItemLinkControlStrategy : BaseBarItemLinkControlStrategy
    {
        private readonly FrameworkElement feInstance;
        private readonly IBarItemLinkControl instance;
        private Point lastMousePosition;
        private bool isInSimplifiedRibbon;
        protected bool wasMouseLeftButtonDown;
        private bool mouseMovePerformed;
        public static readonly Size LargeGlyphSize;
        public static readonly Size MediumGlyphSize;
        public static readonly Size SmallGlyphSize;
        public static ImageSource defaultMediumGlyphCore;
        public static ImageSource defaultLargeGlyphCore;
        private static ImageSource defaultGlyphCore;

        static BarItemLinkControlStrategy();
        public BarItemLinkControlStrategy(IBarItemLinkControl instance);
        protected internal virtual void AfterDestroyDragDropHelper();
        public virtual bool CalcIsEnabledCore();
        protected virtual RibbonItemStyles CalcRibbonStyleInApplicationMenu();
        protected virtual RibbonItemStyles CalcRibbonStyleInButtonGroup();
        protected virtual RibbonItemStyles CalcRibbonStyleInDropDownGallery();
        protected virtual RibbonItemStyles CalcRibbonStyleInPageGroup();
        protected virtual RibbonItemStyles CalcRibbonStyleInPageHeader();
        protected virtual RibbonItemStyles CalcRibbonStyleInQAT();
        protected virtual RibbonItemStyles CalcRibbonStyleInStatusBar();
        public virtual bool CanShowToolTip();
        public virtual bool CanStartDragCore(object sender, MouseButtonEventArgs e);
        public void ClearToolTip();
        public virtual void CreateDragDropHelper();
        public IDragElement CreateDragElement(Point offset);
        public IDropTarget CreateEmptyDropTarget();
        public virtual object CreateSuperTipControl();
        public virtual object CreateToolTip();
        public virtual void DestroyDragDropHelper();
        public virtual void ForceSetIsSelected(bool value);
        protected internal virtual object GetActualContent();
        protected virtual bool GetActualIsArrowEnabled();
        protected virtual bool GetActualIsContentEnabled();
        public virtual bool GetActualShowKeyGesture();
        protected virtual string GetAutomationHelpText(SuperTip superTip);
        public virtual BarItemDisplayMode GetBarItemDisplayMode();
        public virtual INavigationOwner GetBoundOwner();
        public virtual bool GetCloseSubMenuOnClick();
        public virtual object GetContent();
        protected virtual DataTemplate GetContentTemplate();
        protected virtual DataTemplateSelector GetContentTemplateSelector();
        public virtual string GetContentWithoutAccessKey(string actualContent);
        public virtual object GetDataContext();
        public virtual object GetDescription();
        protected virtual DataTemplate GetDescriptionTemplate();
        private DependencyObject GetFirstDisabledParent(DependencyObject item);
        public virtual ImageSource GetGlyph();
        public Dock GetGlyphAlignment();
        public Tuple<GlyphSize, Size> GetGlyphSize();
        internal virtual Size GetGlyphSizeByGlyphKind(GlyphSize glyphKind);
        private GlyphSize GetGlyphSizeInLinkContainerItem(ILinksHolder holder);
        public virtual DataTemplate GetGlyphTemplate();
        public virtual Tuple<GlyphSize, Size> GetHolderGlyphSize(BarItemLinkInfo info);
        public Tuple<GlyphSize, Size> GetHolderGlyphSizeInRadialMenu(ILinksHolder holder);
        protected virtual bool GetIsPressed();
        protected virtual bool GetIsPressedOverride();
        internal bool GetIsSelectable();
        public virtual string GetKeyGestureText();
        public virtual ImageSource GetLargeGlyph();
        public virtual ImageSource GetMediumGlyph();
        private bool GetObjectIsEnabled(DependencyObject element);
        public bool GetRecognizesAccessKey();
        public virtual bool GetShowHotBorder();
        public bool GetShowScreenTip();
        public object GetToolTip(FrameworkElement element);
        public IEnumerable<UIElement> GetTopLevelDropContainers();
        protected virtual VerticalAlignment GetVerticalAlignment();
        public bool IsCompatibleDropTargetFactory(IDropTargetFactory factory, UIElement dropTargetElement);
        public bool IsCustomToolTip();
        public bool IsDisabledByParentCommandCanExecuteOnly(bool includeSelfItem = false);
        public virtual bool IsEmptyToolTip();
        private bool IsEnabledPropertyAssigned(DependencyObject item);
        protected virtual bool IsMouseOverLinkControl(MouseButtonEventArgs args = null);
        protected virtual bool IsPointBelongToLinkControl(Point p);
        protected virtual bool IsPopupJustOpened();
        public virtual bool IsUninformativeToolTip();
        protected virtual bool NeedsMouseCapture();
        public void OnAccessKey(AccessKeyEventArgs e);
        public virtual void OnAccessKeyCore(AccessKeyEventArgs e);
        public virtual void OnAccessKeyPressed(AccessKeyPressedEventArgs e);
        protected virtual void OnAccessKeyPressedCore(AccessKeyPressedEventArgs e);
        public override void OnActualAllowGlyphThemingChanged(bool oldValue, bool newValue);
        public override void OnActualContentChanged(object oldValue, object newValue);
        public override void OnActualContentTemplateChanged(DataTemplate oldValue, DataTemplate newValue);
        public override void OnActualContentTemplateSelectorChanged(DataTemplateSelector oldValue, DataTemplateSelector newValue);
        public override void OnActualDescriptionChanged(object oldValue, object newValue);
        public override void OnActualDescriptionTemplateChanged(DataTemplate oldValue, DataTemplate newValue);
        public override void OnActualGlyphChanged(ImageSource oldValue, ImageSource newValue);
        public override void OnActualGlyphSizeChanged(Size oldValue, Size newValue);
        public override void OnActualGlyphTemplateChanged(DataTemplate oldValue, DataTemplate newValue);
        public override void OnActualKeyGestureTextChanged(string oldValue, string newValue);
        public void OnActualShowGlyphChanged(bool oldValue, bool newValue);
        public virtual void OnApplyTemplate();
        public virtual void OnBadgeChanged(Badge oldValue, Badge newValue);
        public override void OnCalculatedRibbonStyleChanged(RibbonItemStyles oldValue, RibbonItemStyles newValue);
        public virtual void OnClear();
        public virtual void OnClick();
        public override void OnContainerTypeChanged(LinkContainerType oldValue, LinkContainerType newValue);
        public override void OnCurrentRibbonStyleChanged(RibbonItemStyles oldValue, RibbonItemStyles newValue);
        public virtual void OnCustomizationModeChanged();
        public virtual void OnDataContextChanged(object oldValue, object newValue);
        public override void OnDesiredRibbonStyleChanged(RibbonItemStyles? oldValue, RibbonItemStyles? newValue);
        public override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e);
        public override void OnIsEnabledChanged(bool oldValue, bool newValue);
        public override void OnIsHighlightedChanged(bool oldValue, bool newValue);
        public virtual void OnIsInHistorySelectionChanged(bool oldValue, bool newValue);
        protected virtual void OnIsInSimplifiedRibbonChanged(bool oldValue);
        public override void OnIsPressedChanged(bool oldValue, bool newValue);
        public virtual bool OnIsPressedCoerce(bool value);
        public override void OnIsSelectedChanged(bool oldValue, bool newValue);
        public virtual bool OnKeyDown(KeyEventArgs e);
        public virtual bool OnKeyUp(KeyEventArgs e);
        public override void OnLinkBaseChanged(BarItemLinkBase oldValue, BarItemLinkBase newValue);
        public override void OnLinkInfoChanged(BarItemLinkInfo oldValue, BarItemLinkInfo newValue);
        public override void OnLoaded();
        public virtual void OnLostMouseCapture();
        public virtual bool OnMouseDoubleClick(MouseButtonEventArgs args);
        public virtual bool OnMouseDown(MouseButtonEventArgs args);
        public virtual bool OnMouseEnter(MouseEventArgs args);
        public virtual bool OnMouseLeave(MouseEventArgs args);
        public virtual bool OnMouseLeftButtonDown(MouseButtonEventArgs args);
        public virtual bool OnMouseLeftButtonUp(MouseButtonEventArgs args);
        public virtual bool OnMouseMove(MouseEventArgs args);
        public virtual bool OnMouseRightButtonDown(MouseButtonEventArgs args);
        public virtual bool OnMouseRightButtonUp(MouseButtonEventArgs args);
        public virtual bool OnMouseUp(MouseButtonEventArgs args);
        public virtual bool OnPreviewKeyDown(KeyEventArgs e);
        public virtual bool OnPreviewKeyUp(KeyEventArgs e);
        public virtual bool OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e);
        public virtual bool OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e);
        public virtual bool OnPreviewMouseRightButtonDown(MouseButtonEventArgs e);
        public virtual bool OnPreviewMouseRightButtonUp(MouseButtonEventArgs e);
        public override void OnRecycled();
        public override void OnShowGlyphInPopupMenuChanged(bool? oldValue, bool? newValue);
        public virtual bool OnShowHotBorderCoerce(bool obj);
        public override void OnSpacingModeChanged(SpacingMode oldValue, SpacingMode newValue);
        public override void OnUnloaded();
        public Visibility OnVisibilityCoerce(Visibility value);
        public virtual bool ProcessKeyDown(KeyEventArgs args);
        public virtual bool ProcessKeyUp(KeyEventArgs args);
        public void ProcessMouseLeftButtonDown(MouseButtonEventArgs args);
        protected internal virtual void RecreateToolTip();
        public void RegisterGlyphSize();
        protected virtual void SelectInMenuMode(bool force);
        public virtual bool SelectOnKeyTip();
        protected virtual void SelectRoot();
        protected virtual void SetActualGlyph(ImageSource actualGlyph, Size glyphSize);
        public virtual bool SetFocus();
        protected virtual void SetFocusOnNavigation();
        public void SetToolTip(FrameworkElement element, object toolTip);
        protected virtual bool ShouldHighlightItem();
        protected virtual bool ShouldPressItem();
        protected virtual bool ShouldUnselectOnMouseLeaveInMenu();
        protected virtual void StartClosePopupChildren();
        protected virtual bool StartNavigation();
        public Point TranslatePointWithoutTransform(Point point);
        protected virtual bool? TryShowPopupOnMouseEnterOrMove(bool mouseEnter);
        public virtual void UpdateActualAllowGlyphTheming();
        protected virtual void UpdateActualAllowGlyphTheming(BarItemLinkControl barItemLinkControl);
        protected virtual void UpdateActualAllowGlyphTheming(LightweightBarItemLinkControl lwLinkControl);
        internal virtual void UpdateActualBarItemDisplayMode();
        public void UpdateActualContent();
        public virtual void UpdateActualContentTemplate();
        public virtual void UpdateActualContentTemplateSelector();
        public virtual void UpdateActualDescription();
        public virtual void UpdateActualDescriptionTemplate();
        public virtual void UpdateActualGlyph();
        public virtual void UpdateActualGlyphAlignment();
        public virtual void UpdateActualGlyphAlignmentInBars();
        public virtual void UpdateActualGlyphAlignmentInRibbon();
        public virtual void UpdateActualGlyphInBars();
        public virtual void UpdateActualGlyphInRibbon();
        public virtual void UpdateActualGlyphTemplate();
        public virtual void UpdateActualIsArrowEnabled();
        public virtual void UpdateActualIsChecked();
        public virtual void UpdateActualIsContentEnabled();
        public virtual void UpdateActualKeyGestureText();
        public void UpdateActualProperties();
        protected virtual void UpdateActualPropertiesOverride();
        internal virtual void UpdateActualShowContent();
        protected internal virtual void UpdateActualShowCustomizationBorder();
        internal virtual void UpdateActualShowGlyph();
        public virtual void UpdateActualShowGlyphInBars();
        public virtual void UpdateActualShowGlyphInMenu();
        public virtual void UpdateActualShowGlyphInRibbon();
        public virtual void UpdateBadge();
        public virtual void UpdateCalculatedRibbonStyle();
        public virtual void UpdateCurrentRibbonStyle();
        public virtual void UpdateDataContext();
        public void UpdateHasGlyph();
        public virtual void UpdateIsEnabled();
        public virtual void UpdateIsHighlighted();
        public virtual void UpdateIsPressed(bool? forcedValue = new bool?());
        protected void UpdateMouseMovePerformed(MouseEventArgs args);
        public virtual void UpdateOrientation();
        public virtual void UpdateShowDescription();
        public virtual void UpdateShowGlyphInPopupMenu();
        public void UpdateShowHotBorder();
        public virtual void UpdateShowKeyGesture();
        private void UpdateShowToolTipOnDisabled();
        public void UpdateToolTip();
        public virtual void UpdateVerticalAlignment();
        public virtual void UpdateVisibility();

        protected BarItem Item { get; }

        protected BarItemLink Link { get; }

        protected BarItemLinkBase LinkBase { get; }

        protected IBarItemLinkControl Instance { get; }

        protected FrameworkElement FrameworkInstance { get; }

        protected LinkContainerType ContainerType { get; }

        protected internal virtual bool CanSelectOnHoverInMenuMode { get; }

        protected BarItemLinkInfo LinkInfo { get; }

        protected internal bool IsInSimplifiedRibbon { get; set; }

        public virtual bool ShouldDeactivateMenuOnAccessKey { get; }

        public bool CommandCanExecute { get; }

        public ISupportDragDrop dragDropInstance { get; }

        protected bool MouseMovePerformed { get; }

        public static ImageSource DefaultMediumGlyph { get; }

        public static ImageSource DefaultLargeGlyph { get; }

        public static ImageSource DefaultGlyph { get; }

        public BarManager Manager { get; }

        protected DevExpress.Xpf.Bars.LinksControl LinksControl { get; }

        public bool IsLinkInCustomizationMode { get; }

        public bool IsLinkInBar { get; }

        public bool IsLinkInRibbon { get; }

        public bool IsLinkInRibbonPageGroup { get; }

        public virtual bool IsInHorizontalHeader { get; }

        public bool IsLinkInApplicationMenu { get; }

        public bool IsLinkInMenu { get; }

        public bool IsLinkInMainMenu { get; }

        public bool IsLinkInRadialMenu { get; }

        public bool IsLinkInStatusBar { get; }

        public bool IsLinkInContextMenu { get; }

        public DragDropElementHelper DragDropHelper { get; set; }

        internal IDragElement DragElement { get; private set; }

        public FrameworkElement SourceElement { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BarItemLinkControlStrategy.<>c <>9;
            public static Func<BarControl, BarItemDisplayMode> <>9__38_0;
            public static Func<BarItemDisplayMode> <>9__38_1;
            public static Func<BarControl, BarContainerControl> <>9__38_2;
            public static Func<BarContainerControl, BarItemDisplayMode> <>9__38_3;
            public static Func<BarItemDisplayMode> <>9__38_4;
            public static Func<LinksControl, Size> <>9__39_0;
            public static Func<Size> <>9__39_1;
            public static HitTestResultCallback <>9__49_1;
            public static Func<BarPopupBase, IBarItemLinkControl> <>9__53_0;
            public static Func<IPopupOwner, IPopupControl> <>9__79_2;
            public static Func<IPopupControl, BarPopupBase> <>9__79_3;
            public static Func<BarPopupBase, bool> <>9__80_0;
            public static Action <>9__81_0;
            public static Func<BarItemLinkInfo, bool> <>9__85_0;
            public static Func<BarPopupBase, IBarItemLinkControl> <>9__85_1;
            public static Func<IBarItemLinkControl, BarItemLinkInfo> <>9__85_2;
            public static Func<IPopupOwner, bool> <>9__85_3;
            public static Func<bool?, bool> <>9__121_0;
            public static Func<bool> <>9__121_1;
            public static Predicate<DependencyObject> <>9__137_0;
            public static Func<bool?> <>9__161_0;
            public static Func<GlyphSize> <>9__172_1;
            public static Func<BarItem, bool> <>9__177_0;
            public static Func<bool> <>9__177_1;
            public static Func<BarItemLink, bool> <>9__177_2;
            public static Func<bool> <>9__177_3;
            public static Func<bool?> <>9__179_0;
            public static Func<BarItemLink, object> <>9__226_0;
            public static Func<BarItem, object> <>9__226_1;
            public static Func<BarManagerCustomizationHelper, bool> <>9__226_2;
            public static Func<bool> <>9__226_3;
            public static Func<BarManagerCustomizationHelper, bool> <>9__231_0;
            public static Func<bool> <>9__231_1;
            public static Func<BarManagerCustomizationHelper, bool> <>9__233_0;
            public static Func<bool> <>9__233_1;
            public static Func<LinksControl, PopupMenu> <>9__243_0;
            public static Action<DragDropElementHelper> <>9__255_0;
            public static Func<ILinksHolder, bool> <>9__258_1;
            public static Func<LinksControl, bool> <>9__258_0;
            public static Func<BarManager, bool> <>9__258_2;
            public static Func<bool> <>9__258_3;
            public static Func<BarItemLinkInfo, bool> <>9__258_4;
            public static Func<BarNameScope, FrameworkElement> <>9__263_0;
            public static Func<PresentationSource, FrameworkElement> <>9__267_0;

            static <>c();
            internal bool <CanShowToolTip>b__231_0(BarManagerCustomizationHelper x);
            internal bool <CanShowToolTip>b__231_1();
            internal bool <CanStartDragCore>b__258_0(LinksControl x);
            internal bool <CanStartDragCore>b__258_1(ILinksHolder h);
            internal bool <CanStartDragCore>b__258_2(BarManager x);
            internal bool <CanStartDragCore>b__258_3();
            internal bool <CanStartDragCore>b__258_4(BarItemLinkInfo x);
            internal FrameworkElement <CreateDragElement>b__263_0(BarNameScope x);
            internal object <CreateSuperTipControl>b__226_0(BarItemLink x);
            internal object <CreateSuperTipControl>b__226_1(BarItem x);
            internal bool <CreateSuperTipControl>b__226_2(BarManagerCustomizationHelper x);
            internal bool <CreateSuperTipControl>b__226_3();
            internal void <DestroyDragDropHelper>b__255_0(DragDropElementHelper x);
            internal FrameworkElement <get_SourceElement>b__267_0(PresentationSource x);
            internal PopupMenu <GetActualShowKeyGesture>b__243_0(LinksControl x);
            internal BarItemDisplayMode <GetBarItemDisplayMode>b__38_0(BarControl x);
            internal BarItemDisplayMode <GetBarItemDisplayMode>b__38_1();
            internal BarContainerControl <GetBarItemDisplayMode>b__38_2(BarControl x);
            internal BarItemDisplayMode <GetBarItemDisplayMode>b__38_3(BarContainerControl x);
            internal BarItemDisplayMode <GetBarItemDisplayMode>b__38_4();
            internal GlyphSize <GetHolderGlyphSize>b__172_1();
            internal bool <GetIsSelectable>b__177_0(BarItem x);
            internal bool <GetIsSelectable>b__177_1();
            internal bool <GetIsSelectable>b__177_2(BarItemLink x);
            internal bool <GetIsSelectable>b__177_3();
            internal HitTestResultBehavior <IsMouseOverLinkControl>b__49_1(HitTestResult e);
            internal bool <IsUninformativeToolTip>b__233_0(BarManagerCustomizationHelper x);
            internal bool <IsUninformativeToolTip>b__233_1();
            internal bool <OnGotKeyboardFocus>b__137_0(DependencyObject x);
            internal Size <OnLinkInfoChanged>b__39_0(LinksControl x);
            internal Size <OnLinkInfoChanged>b__39_1();
            internal IPopupControl <OnMouseEnter>b__79_2(IPopupOwner x);
            internal BarPopupBase <OnMouseEnter>b__79_3(IPopupControl x);
            internal bool <OnMouseLeave>b__80_0(BarPopupBase x);
            internal bool <SelectInMenuMode>b__85_0(BarItemLinkInfo x);
            internal IBarItemLinkControl <SelectInMenuMode>b__85_1(BarPopupBase x);
            internal BarItemLinkInfo <SelectInMenuMode>b__85_2(IBarItemLinkControl x);
            internal bool <SelectInMenuMode>b__85_3(IPopupOwner x);
            internal void <SelectRoot>b__81_0();
            internal IBarItemLinkControl <ShouldHighlightItem>b__53_0(BarPopupBase x);
            internal bool <UpdateActualAllowGlyphTheming>b__121_0(bool? x);
            internal bool <UpdateActualAllowGlyphTheming>b__121_1();
            internal bool? <UpdateActualShowGlyphInMenu>b__179_0();
            internal bool? <UpdateShowGlyphInPopupMenu>b__161_0();
        }
    }
}

