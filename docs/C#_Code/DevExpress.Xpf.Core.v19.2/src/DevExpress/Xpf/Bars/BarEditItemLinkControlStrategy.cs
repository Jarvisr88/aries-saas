namespace DevExpress.Xpf.Bars
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Input;

    public class BarEditItemLinkControlStrategy : BarItemLinkControlStrategy
    {
        private readonly Locker editValueChangedLocker;

        public BarEditItemLinkControlStrategy(IBarItemLinkControl instance);
        protected override RibbonItemStyles CalcRibbonStyleInButtonGroup();
        protected override RibbonItemStyles CalcRibbonStyleInPageGroup();
        protected override RibbonItemStyles CalcRibbonStyleInQAT();
        protected override RibbonItemStyles CalcRibbonStyleInStatusBar();
        public virtual void CreateEdit();
        public BaseEdit FindTemplatedEdit();
        public object GetActualEditValue();
        protected virtual object GetContent2();
        protected virtual DataTemplate GetContent2Template();
        public virtual double? GetEditHeight();
        protected virtual Style GetEditStyle();
        public virtual double? GetEditWidth();
        protected virtual HorizontalAlignment GetHorizontalEditAlignment();
        public virtual void OnActualContent2Changed(object oldValue, object newValue);
        public virtual void OnActualContent2TemplateChanged(DataTemplate oldValue, DataTemplate newValue);
        public virtual void OnActualContent2VisibilityChanged(Visibility oldValue, Visibility newValue);
        public virtual void OnActualEditHeightChanged(double oldValue, double newValue);
        public virtual void OnActualEditStyleChanged(Style oldValue, Style newValue);
        public virtual void OnActualEditTemplateChanged(DataTemplate oldValue, DataTemplate newValue);
        public virtual void OnActualEditWidthChanged(double oldValue, double newValue);
        public virtual void OnActualHorizontalEditAlignmentChanged(HorizontalAlignment oldValue, HorizontalAlignment newValue);
        protected virtual void OnAfterIsSelectedChanged();
        public override void OnApplyTemplate();
        public override void OnCustomizationModeChanged();
        public override void OnDataContextChanged(object oldValue, object newValue);
        public virtual void OnDisabledEditStyleChanged(Style oldValue, Style newValue);
        public virtual void OnEditAndContentLayoutPanelStyleChanged(Style oldValue, Style newValue);
        public virtual void OnEditChanged(BaseEdit oldValue, BaseEdit newValue);
        public virtual void OnEditContentMarginChanged(Thickness oldValue, Thickness newValue);
        public virtual void OnEditContentMarginPropertyChanged(DependencyPropertyChangedEventArgs e);
        protected virtual void OnEditGotFocus(object sender, RoutedEventArgs e);
        protected virtual void OnEditLostFocus(object sender, RoutedEventArgs e);
        protected virtual void OnEditValueChanged(object sender, EditValueChangedEventArgs e);
        protected virtual void OnEditValueChangedCore(BaseEdit edit, object sender, EditValueChangedEventArgs e);
        public virtual void OnHotEditStyleChanged(Style oldValue, Style newValue);
        public virtual void OnInRibbonEditContentMarginChanged(Thickness oldValue);
        public virtual void OnInRibbonEditContentMarginChanged(Thickness oldValue, Thickness newValue);
        public virtual void OnIsEditFocusedChanged(bool oldValue, bool newValue);
        public void OnIsKeyboardFocusWithinChanged(DependencyPropertyChangedEventArgs e);
        public override void OnIsSelectedChanged(bool oldValue, bool newValue);
        public void OnItemChanged(BarEditItem oldValue, BarEditItem newValue);
        public override void OnLinkInfoChanged(BarItemLinkInfo oldValue, BarItemLinkInfo newValue);
        public override void OnLoaded();
        public override bool OnMouseEnter(MouseEventArgs args);
        public virtual void OnNormalEditStyleChanged(Style oldValue, Style newValue);
        public virtual void OnPressedEditStyleChanged(Style oldValue, Style newValue);
        public override bool OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e);
        public override bool OnShowHotBorderCoerce(bool obj);
        public virtual void OnShowInVerticalBarChanged(DefaultBoolean oldValue, DefaultBoolean newValue);
        public virtual void OnTemplatedEditChanged(BaseEdit oldValue, BaseEdit newValue);
        protected virtual void OnTemplatedEditGotFocus(object sender, RoutedEventArgs e);
        protected virtual void OnTemplatedEditLostFocus(object sender, RoutedEventArgs e);
        protected virtual void OnTemplatedEditValueChanged(object sender, EditValueChangedEventArgs e);
        protected virtual void ReassignEditSettings();
        public void RecreateEdit();
        public override bool SelectOnKeyTip();
        public override bool SetFocus();
        protected override bool ShouldUnselectOnMouseLeaveInMenu();
        protected override bool StartNavigation();
        protected virtual void SubscribeEditCoreEvents(FrameworkElement editCore);
        protected virtual void SubscribeEditEvents();
        public virtual void SubscribeTemplatedEditEvents();
        protected virtual void SubscribeTemplateEvents();
        protected internal virtual void SyncEditValue(bool force = false);
        public virtual void SyncEditValueFromEdit();
        protected virtual void UnsubscribeEditCoreEvents(FrameworkElement editCore);
        protected virtual void UnsubscribeEditEvents();
        protected virtual void UnsubscribeTemplatedEditEvents();
        protected virtual void UnsubscribeTemplateEvents();
        public virtual void UpdateActualContent2();
        public virtual void UpdateActualContent2Template();
        public virtual void UpdateActualContent2Visibility();
        public virtual void UpdateActualEditHeight();
        public virtual void UpdateActualEditStyle();
        public void UpdateActualEditTemplate();
        public virtual void UpdateActualEditWidth();
        public virtual void UpdateActualHorizontalEditAlignment();
        protected override void UpdateActualPropertiesOverride();
        public override void UpdateActualShowGlyphInRibbon();
        protected internal void UpdateEdit();
        public void UpdateEditContentMargin();
        public virtual void UpdateEditIsReadOnlyState();
        public virtual void UpdateLayoutPanelShowContent2();
        public virtual void UpdateShowInVerticalBar();

        public IBarEditItemLinkControl EditInstance { get; }

        public BarEditItemLink EditLink { get; }

        public BarEditItem EditItem { get; }

        public BaseEdit Edit { get; }

        public BaseEdit TemplatedEdit { get; }

        public BaseEdit ActualEdit { get; }

        protected Locker EditValueChangedLocker { get; }

        public bool IsLockEditValueChanged { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BarEditItemLinkControlStrategy.<>c <>9;
            public static Func<BarItemLinkInfo, IBarItemLinkControl> <>9__14_0;
            public static Func<IBarItemLinkControl, bool> <>9__14_1;
            public static Func<IBarItemLinkControl, BarPopupBase> <>9__14_2;
            public static Func<BaseEdit, FrameworkElement> <>9__33_0;
            public static Func<BaseEdit, FrameworkElement> <>9__33_1;
            public static Action<BarPopupBase> <>9__44_0;
            public static Predicate<FrameworkElement> <>9__76_0;

            static <>c();
            internal bool <FindTemplatedEdit>b__76_0(FrameworkElement e);
            internal FrameworkElement <OnIsKeyboardFocusWithinChanged>b__33_0(BaseEdit x);
            internal FrameworkElement <OnIsKeyboardFocusWithinChanged>b__33_1(BaseEdit x);
            internal IBarItemLinkControl <OnMouseEnter>b__14_0(BarItemLinkInfo x);
            internal bool <OnMouseEnter>b__14_1(IBarItemLinkControl x);
            internal BarPopupBase <OnMouseEnter>b__14_2(IBarItemLinkControl x);
            internal void <OnPreviewMouseLeftButtonDown>b__44_0(BarPopupBase x);
        }
    }
}

