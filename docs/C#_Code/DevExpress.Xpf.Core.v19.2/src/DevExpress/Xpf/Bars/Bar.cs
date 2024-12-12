namespace DevExpress.Xpf.Bars
{
    using DevExpress.Utils;
    using DevExpress.Utils.Design;
    using DevExpress.Xpf.Bars.Native;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class Bar : BarItemLinkHolderBase, ILinksHolder, IMultipleElementRegistratorSupport, IBarNameScopeSupport, IInputElement, ILogicalChildrenContainer, IBarManagerControllerAction, IControllerAction, IMergingSupport, IBar
    {
        public static readonly DependencyProperty UseWholeRowProperty;
        public static readonly DependencyProperty IsMultiLineProperty;
        public static readonly DependencyProperty BarItemHorzIndentProperty;
        public static readonly DependencyProperty BarItemVertIndentProperty;
        public static readonly DependencyProperty AllowQuickCustomizationProperty;
        public static readonly DependencyProperty ShowDragWidgetProperty;
        public static readonly DependencyProperty CaptionProperty;
        public static readonly DependencyProperty AllowCollapseProperty;
        public static readonly DependencyProperty IsMainMenuProperty;
        public static readonly DependencyProperty IsStatusBarProperty;
        public static readonly DependencyProperty ShowSizeGripProperty;
        public static readonly DependencyProperty GlyphSizeProperty;
        public static readonly DependencyProperty RotateWhenVerticalProperty;
        public static readonly DependencyProperty IsCollapsedProperty;
        public static readonly DependencyProperty AllowRenameProperty;
        public static readonly DependencyProperty AllowHideProperty;
        public static readonly DependencyProperty AllowRemoveProperty;
        public static readonly DependencyProperty BarItemsAlignmentProperty;
        public static readonly DependencyProperty AllowCustomizationMenuProperty;
        public static readonly DependencyProperty BarItemDisplayModeProperty;
        public static readonly DependencyProperty HideWhenEmptyProperty;
        protected internal bool ShouldOnNewRow;
        private BarDockInfo dockInfo;
        private bool hasVisibleItems;
        private Size defaultBarSize;
        private bool isRemoved;
        private BarCustomizationControl customizationControl;
        private bool isPrivate;
        private MergingSupportNameStorage msNameStorage;
        private bool showInOriginContainer;

        public event BarControlLoadedEventHandler BarControlLoaded;

        protected internal event EventHandler HasVisibleItemsChanged;

        public event BarVisibleChangedEventHandler VisibleChanged;

        static Bar();
        public Bar();
        [CompilerGenerated, DebuggerHidden]
        private IEnumerable<object> <>n__0();
        protected void AddCustomization(DependencyProperty property, object oldValue, object newValue);
        protected virtual void AddItems(BarCustomizationControl newValue);
        protected internal bool CanBeMerged();
        private bool CanMerge(IMergingSupport bar);
        protected virtual BarDockInfo CreateDockInfo();
        object IBarManagerControllerAction.GetObject();
        void IControllerAction.Execute(DependencyObject context);
        DevExpress.Xpf.Bars.GlyphSize ILinksHolder.GetDefaultItemsGlyphSize(LinkContainerType linkContainerType);
        bool IBar.CanBind(BarContainerControl container, object binderKey);
        void IBar.Merge(IBar bar);
        void IBar.Unmerge();
        void IBar.Unmerge(IBar bar);
        bool IMergingSupport.CanMerge(IMergingSupport second);
        bool IMergingSupport.IsMergedParent(IMergingSupport second);
        void IMergingSupport.Merge(IMergingSupport second);
        void IMergingSupport.Unmerge(IMergingSupport second);
        private static bool GetHasVisibleItems(ILinksHolder holder);
        protected internal bool GetIsMultiLine();
        [IteratorStateMachine(typeof(Bar.<GetRegistratorKeys>d__245))]
        protected override IEnumerable<object> GetRegistratorKeys();
        protected override object GetRegistratorName(object registratorKey);
        private string GetSpecialName();
        public void HideIfFloating();
        public void Merge(Bar bar);
        protected virtual void OnAlignmentChanged(DependencyPropertyChangedEventArgs e);
        protected static void OnAlignmentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        protected void OnAllowCollapseChanged(DependencyPropertyChangedEventArgs e);
        protected virtual object OnAllowCollapseCoerce(object baseValue);
        protected static void OnAllowCollapsePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e);
        protected static object OnAllowCollapsePropertyCoerce(DependencyObject obj, object baseValue);
        protected virtual void OnAllowQuickCuztomizationChanged(DependencyPropertyChangedEventArgs e);
        protected static void OnAllowQuickCuztomizationPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e);
        private static void OnBarCustomizationControlTemplateChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e);
        protected virtual void OnDisableCloseChanged(DependencyPropertyChangedEventArgs e);
        protected static void OnDisableClosePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e);
        protected virtual void OnDockInfoChanged(BarDockInfo prevValue);
        private void OnGlyphSizeChanged(DependencyPropertyChangedEventArgs e);
        protected static void OnGlyphSizePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e);
        protected virtual void OnHideWhenEmptyChanged(bool oldValue);
        protected void OnIsCollapsedChanged(DependencyPropertyChangedEventArgs e);
        protected virtual object OnIsCollapsedCoerce(object baseValue);
        protected static void OnIsCollapsedPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e);
        protected static object OnIsCollapsedPropertyCoerce(DependencyObject obj, object baseValue);
        protected override void OnIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e);
        protected virtual void OnIsMainMenuChanged(DependencyPropertyChangedEventArgs e);
        protected static void OnIsMainMenuChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e);
        private void OnIsPrivateChanged();
        protected virtual void OnIsRemovedChanged();
        protected virtual void OnIsStatusBarChanged(DependencyPropertyChangedEventArgs e);
        protected static void OnIsStatusBarChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e);
        private void OnLinkIsVisibleChanged(object sender, RoutedEventArgs e);
        protected override void OnLinksCollectionChanged(object sender, NotifyCollectionChangedEventArgs e);
        protected virtual void OnLoaded(object sender, RoutedEventArgs e);
        protected override void OnManagerChanged(BarManager newManager);
        protected override void OnManagerChanging(BarManager oldManager);
        protected override void OnMergedLinksCollectionChanged(object sender, NotifyCollectionChangedEventArgs e);
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e);
        protected static void OnRotateWhenVericalPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e);
        private void OnRotateWhenVerticalChanged(DependencyPropertyChangedEventArgs e);
        protected virtual void OnShowDragWidgetChanged(DependencyPropertyChangedEventArgs e);
        protected static void OnShowDragWidgetPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e);
        protected virtual void OnShowSizeGripChanged(DependencyPropertyChangedEventArgs e);
        protected static void OnShowSizeGripChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e);
        protected virtual void OnUnloaded(object sender, RoutedEventArgs e);
        protected virtual void OnUseWholeRowChanged(DependencyPropertyChangedEventArgs e);
        protected static void OnUseWholeRowPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e);
        protected override void OnVisibleChanged(DependencyPropertyChangedEventArgs e);
        protected internal virtual void RaiseBarControlLoadedEvent();
        protected virtual void RaiseVisibleChangedEvent();
        protected virtual void RecalcHasVisibleItems();
        private void RefreshPopup();
        protected internal void Remerge();
        protected virtual void RemoveItems(BarCustomizationControl oldValue);
        public void ShowIfFloating();
        public void UnMerge();
        public void UnMerge(Bar bar);
        private void UpdateHasVisibleItems();

        protected internal bool HasVisibleItems { get; set; }

        [Description("Contains information on the position where the bar is docked."), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public BarDockInfo DockInfo { get; set; }

        protected internal virtual bool ShowWhenBarManagerIsMerged { get; }

        protected override IEnumerator LogicalChildren { get; }

        [Description("Gets or sets whether a bar can be renamed via the Customization Window.This is a dependency property.")]
        public bool AllowRename { get; set; }

        [Description("Gets or sets whether a bar can be collapsed by double-clicking on the bar's drag widget. This is a dependency property.")]
        public bool AllowCollapse { get; set; }

        public bool HideWhenEmpty { get; set; }

        [Description("Gets or sets if the current Bar's customization context menu is enabled. This is a dependency property.")]
        public bool AllowCustomizationMenu { get; set; }

        [TypeConverter(typeof(DefaultBooleanConverter)), Description("Gets or sets whether the bar is stretched to match the container's width.")]
        public DefaultBoolean UseWholeRow { get; set; }

        [Description("Gets whether the bar is stretched to match the container's width.")]
        public bool IsUseWholeRow { get; }

        [Description("Gets or sets whether bar links are arranged into several lines when their total width exceeds the width of the bar. This is a dependency property.")]
        public bool IsMultiLine { get; set; }

        [TypeConverter(typeof(DefaultBooleanConverter)), Description("Gets or sets whether the bar provides the Quick Customization Button, opening the customization menu.")]
        public DefaultBoolean AllowQuickCustomization { get; set; }

        [Description("Gets whether the bar provides the Quick Customization Button.")]
        public bool IsAllowQuickCustomization { get; }

        [Description("Gets or sets whether a drag widget is displayed at the left of the bar, allowing the bar to be dragged using the mouse. This is a dependency property.")]
        public bool ShowDragWidget { get; set; }

        [Description("Gets or sets the horizontal interval between the bar's links. This is a dependency property.")]
        public double BarItemHorzIndent { get; set; }

        [Description("Gets or sets the vertical interval between the bar's links. This is a dependency property.")]
        public double BarItemVertIndent { get; set; }

        [Description("Gets or sets the bar's caption.This is a dependency property.")]
        public string Caption { get; set; }

        [Description("Gets or sets whether the bar represents the main menu.This is a dependency property.")]
        public bool IsMainMenu { get; set; }

        [Description("Gets or sets whether the bar represents the status bar.This is a dependency property.")]
        public bool IsStatusBar { get; set; }

        [Description("Gets or sets if a Bar object has a visible size grip.This is a dependency property.")]
        public bool ShowSizeGrip { get; set; }

        [Description("Gets or sets whether a small or large image is used by bar item links displayed in the current bar.This is a dependency property.")]
        public DevExpress.Xpf.Bars.GlyphSize GlyphSize { get; set; }

        public DevExpress.Xpf.Bars.BarItemDisplayMode BarItemDisplayMode { get; set; }

        [Description("Gets or sets whether the captions of the bar's links are rotated when the bar is vertically docked. This is a dependency property.")]
        public bool RotateWhenVertical { get; set; }

        [Description("Gets or sets whether the bar is collapsed.This is a dependency property.")]
        public bool IsCollapsed { get; set; }

        [Description("Gets or sets whether the bar's visibility can be changed by an end-user.This is a dependency property.")]
        public DefaultBoolean AllowHide { get; set; }

        [Description("Gets or sets whether end-users can remove a bar item from its container. This is a dependency property.")]
        public bool AllowRemove { get; set; }

        [Description("Gets whether the bar's visibility can be changed by an end-user.")]
        public bool IsAllowHide { get; }

        [Description("")]
        public BarItemAlignment BarItemsAlignment { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public Size DefaultBarSize { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool CreatedByCustomizationDialog { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public bool IsRemoved { get; set; }

        internal bool IsPrivate { get; set; }

        internal bool ActualAllowCustomizationMenu { get; }

        DevExpress.Xpf.Bars.GlyphSize ILinksHolder.ItemsGlyphSize { get; }

        LinksHolderType ILinksHolder.HolderType { get; }

        protected BarCustomizationControl CustomizationControl { get; set; }

        IActionContainer IControllerAction.Container { get; set; }

        bool IMergingSupport.IsMerged { get; }

        bool IMergingSupport.IsAutomaticallyMerged { get; set; }

        object IMergingSupport.MergingKey { get; }

        MergingSupportNameStorage IMergingSupport.NameStorage { get; }

        BarDockInfo IBar.DockInfo { get; }

        BarContainerControl IBar.OriginContainer { get; set; }

        protected internal ToolBarControlBase ToolBar { get; set; }

        bool IBar.ShowInOriginContainer { get; set; }

        int IBar.Index { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly Bar.<>c <>9;
            public static Action<BarControl> <>9__21_1;
            public static Action<BarControl> <>9__21_4;
            public static EventHandler <>9__39_0;
            public static Func<BarItemLink, ILinksHolder> <>9__53_0;
            public static Func<ILinksHolder, bool> <>9__53_1;
            public static Func<FloatingBarContainerControl, FloatingBarPopup> <>9__57_0;
            public static Action<FloatingBarPopup> <>9__57_1;
            public static Func<FloatingBarContainerControl, FloatingBarPopup> <>9__58_0;
            public static Action<FloatingBarPopup> <>9__58_1;
            public static Func<ToolBarControlBase, Bar> <>9__72_0;
            public static Func<ToolBarControlBase, Bar> <>9__73_0;
            public static Action<BarControl> <>9__175_0;
            public static Action<BarControl> <>9__183_0;
            public static Func<BarDockInfo, BarControl> <>9__186_0;
            public static Action<BarControl> <>9__186_1;
            public static Func<string, string> <>9__219_1;
            public static Func<DependencyObject, object> <>9__219_0;
            public static Func<Bar, BarManager> <>9__249_0;

            static <>c();
            internal void <.cctor>b__21_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__21_1(BarControl x);
            internal void <.cctor>b__21_2(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__21_3(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__21_4(BarControl x);
            internal void <.ctor>b__39_0(object o, EventArgs e);
            internal BarManager <AddCustomization>b__249_0(Bar x);
            internal Bar <DevExpress.Xpf.Bars.Native.IBar.Merge>b__72_0(ToolBarControlBase x);
            internal Bar <DevExpress.Xpf.Bars.Native.IBar.Unmerge>b__73_0(ToolBarControlBase x);
            internal object <DevExpress.Xpf.Bars.Native.IMergingSupport.get_NameStorage>b__219_0(DependencyObject x);
            internal string <DevExpress.Xpf.Bars.Native.IMergingSupport.get_NameStorage>b__219_1(string n);
            internal ILinksHolder <GetHasVisibleItems>b__53_0(BarItemLink x);
            internal bool <GetHasVisibleItems>b__53_1(ILinksHolder x);
            internal FloatingBarPopup <HideIfFloating>b__57_0(FloatingBarContainerControl x);
            internal void <HideIfFloating>b__57_1(FloatingBarPopup x);
            internal BarControl <OnHideWhenEmptyChanged>b__186_0(BarDockInfo x);
            internal void <OnHideWhenEmptyChanged>b__186_1(BarControl x);
            internal void <OnUseWholeRowChanged>b__183_0(BarControl x);
            internal void <OnVisibleChanged>b__175_0(BarControl x);
            internal FloatingBarPopup <ShowIfFloating>b__58_0(FloatingBarContainerControl x);
            internal void <ShowIfFloating>b__58_1(FloatingBarPopup x);
        }


        private class UpdateHasVisibleItemsAction : IAggregateAction, IAction
        {
            private readonly Bar bar;

            public UpdateHasVisibleItemsAction(Bar bar);
            void IAction.Execute();
            bool IAggregateAction.CanAggregate(IAction action);
        }
    }
}

