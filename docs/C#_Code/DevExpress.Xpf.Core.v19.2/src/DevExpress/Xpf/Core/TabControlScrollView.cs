namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public class TabControlScrollView : TabControlViewBase
    {
        public static readonly DependencyProperty HeaderOrientationProperty;
        public static readonly DependencyProperty HeaderAutoFillProperty;
        public static readonly DependencyProperty AllowAnimationProperty;
        public static readonly DependencyProperty ScrollButtonShowModeProperty;
        private static readonly DependencyPropertyKey IsScrollPrevButtonVisiblePropertyKey;
        private static readonly DependencyPropertyKey IsScrollNextButtonVisiblePropertyKey;
        [IgnoreDependencyPropertiesConsistencyChecker, EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly DependencyProperty IsScrollPrevButtonVisibleProperty;
        [IgnoreDependencyPropertiesConsistencyChecker, EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly DependencyProperty IsScrollNextButtonVisibleProperty;
        public static readonly DependencyProperty AllowScrollOnMouseWheelProperty;

        static TabControlScrollView()
        {
            HeaderOrientationProperty = DependencyProperty.Register("HeaderOrientation", typeof(DevExpress.Xpf.Core.HeaderOrientation), typeof(TabControlScrollView), new PropertyMetadata(DevExpress.Xpf.Core.HeaderOrientation.Default, (d, e) => ((TabControlScrollView) d).UpdateViewProperties()));
            HeaderAutoFillProperty = DependencyProperty.Register("HeaderAutoFill", typeof(bool), typeof(TabControlScrollView), new PropertyMetadata(false));
            AllowAnimationProperty = DependencyProperty.Register("AllowAnimation", typeof(bool), typeof(TabControlScrollView), new PropertyMetadata(true));
            ScrollButtonShowModeProperty = DependencyProperty.Register("ScrollButtonShowMode", typeof(DevExpress.Xpf.Core.ScrollButtonShowMode), typeof(TabControlScrollView), new PropertyMetadata(DevExpress.Xpf.Core.ScrollButtonShowMode.AutoHideBothButtons, (d, e) => ((TabControlScrollView) d).UpdateScrollButtonsVisibility()));
            IsScrollPrevButtonVisiblePropertyKey = DependencyProperty.RegisterReadOnly("IsScrollPrevButtonVisible", typeof(bool), typeof(TabControlScrollView), new PropertyMetadata(false));
            IsScrollNextButtonVisiblePropertyKey = DependencyProperty.RegisterReadOnly("IsScrollNextButtonVisible", typeof(bool), typeof(TabControlScrollView), new PropertyMetadata(false));
            IsScrollPrevButtonVisibleProperty = IsScrollPrevButtonVisiblePropertyKey.DependencyProperty;
            IsScrollNextButtonVisibleProperty = IsScrollNextButtonVisiblePropertyKey.DependencyProperty;
            AllowScrollOnMouseWheelProperty = DependencyProperty.Register("AllowScrollOnMouseWheel", typeof(bool), typeof(TabControlScrollView), new PropertyMetadata(true));
        }

        public TabControlScrollView()
        {
            this.ScrollPrevCommand = new DelegateCommand(new Action(this.ScrollPrev), () => this.CanScrollPrev, false);
            this.ScrollNextCommand = new DelegateCommand(new Action(this.ScrollNext), () => this.CanScrollNext, false);
            this.ScrollToSelectedCommand = new DelegateCommand<bool>(new Action<bool>(this.ScrollToSelectedTabItem), false);
            this.UpdateScrollButtonsCommand = new DelegateCommand(new Action(this.UpdateScrollCommands), false);
            this.ScrollOnMouseWheelCommand = new DelegateCommand<MouseWheelEventArgs>(delegate (MouseWheelEventArgs x) {
                switch (new TabViewInfo(base.Owner).HeaderLocation)
                {
                    case HeaderLocation.Left:
                    case HeaderLocation.Right:
                        if (x.Delta < 0)
                        {
                            this.ScrollNext();
                            return;
                        }
                        this.ScrollPrev();
                        return;

                    case HeaderLocation.Top:
                    case HeaderLocation.Bottom:
                        if (x.Delta < 0)
                        {
                            this.ScrollNext();
                            return;
                        }
                        this.ScrollPrev();
                        return;
                }
            }, delegate (MouseWheelEventArgs x) {
                switch (new TabViewInfo(base.Owner).HeaderLocation)
                {
                    case HeaderLocation.Left:
                    case HeaderLocation.Right:
                        return (x.Delta < 0) ? this.CanScrollNext : this.CanScrollPrev;

                    case HeaderLocation.Top:
                    case HeaderLocation.Bottom:
                        return (x.Delta < 0) ? this.CanScrollNext : this.CanScrollPrev;
                }
                return false;
            }, false);
            this.UpdateScrollCommands();
        }

        public virtual void ScrollFirst()
        {
            Action<TabPanelScrollView> action = <>c.<>9__61_0;
            if (<>c.<>9__61_0 == null)
            {
                Action<TabPanelScrollView> local1 = <>c.<>9__61_0;
                action = <>c.<>9__61_0 = x => x.ScrollToBegin(true);
            }
            this.ScrollPanel.Do<TabPanelScrollView>(action);
            this.UpdateScrollCommands();
        }

        public virtual void ScrollLast()
        {
            Action<TabPanelScrollView> action = <>c.<>9__62_0;
            if (<>c.<>9__62_0 == null)
            {
                Action<TabPanelScrollView> local1 = <>c.<>9__62_0;
                action = <>c.<>9__62_0 = x => x.ScrollToEnd(true);
            }
            this.ScrollPanel.Do<TabPanelScrollView>(action);
            this.UpdateScrollCommands();
        }

        public void ScrollNext()
        {
            Action<TabPanelScrollView> action = <>c.<>9__64_0;
            if (<>c.<>9__64_0 == null)
            {
                Action<TabPanelScrollView> local1 = <>c.<>9__64_0;
                action = <>c.<>9__64_0 = x => x.ScrollNext(true);
            }
            this.ScrollPanel.Do<TabPanelScrollView>(action);
            this.UpdateScrollCommands();
        }

        public void ScrollPrev()
        {
            Action<TabPanelScrollView> action = <>c.<>9__63_0;
            if (<>c.<>9__63_0 == null)
            {
                Action<TabPanelScrollView> local1 = <>c.<>9__63_0;
                action = <>c.<>9__63_0 = x => x.ScrollPrev(true);
            }
            this.ScrollPanel.Do<TabPanelScrollView>(action);
            this.UpdateScrollCommands();
        }

        public void ScrollToSelectedTabItem(bool useAnimation = true)
        {
            this.ScrollPanel.Do<TabPanelScrollView>(x => x.ScrollTo(this.Owner.SelectedContainer, useAnimation));
            this.UpdateScrollCommands();
        }

        private void UpdateScrollButtonsVisibility()
        {
            switch (this.ScrollButtonShowMode)
            {
                case DevExpress.Xpf.Core.ScrollButtonShowMode.Always:
                    base.SetValue(IsScrollPrevButtonVisiblePropertyKey, true);
                    base.SetValue(IsScrollNextButtonVisiblePropertyKey, true);
                    return;

                case DevExpress.Xpf.Core.ScrollButtonShowMode.Never:
                    base.SetValue(IsScrollPrevButtonVisiblePropertyKey, false);
                    base.SetValue(IsScrollNextButtonVisiblePropertyKey, false);
                    return;

                case DevExpress.Xpf.Core.ScrollButtonShowMode.AutoHideBothButtons:
                    base.SetValue(IsScrollPrevButtonVisiblePropertyKey, this.CanScroll);
                    base.SetValue(IsScrollNextButtonVisiblePropertyKey, this.CanScroll);
                    return;

                case DevExpress.Xpf.Core.ScrollButtonShowMode.AutoHideEachButton:
                    base.SetValue(IsScrollPrevButtonVisiblePropertyKey, this.CanScrollPrev);
                    base.SetValue(IsScrollNextButtonVisiblePropertyKey, this.CanScrollNext);
                    return;
            }
        }

        protected virtual void UpdateScrollCommands()
        {
            this.ScrollPrevCommand.RaiseCanExecuteChanged();
            this.ScrollNextCommand.RaiseCanExecuteChanged();
            this.ScrollToSelectedCommand.RaiseCanExecuteChanged();
            this.UpdateScrollButtonsVisibility();
        }

        protected internal override void UpdateViewPropertiesCore()
        {
            base.UpdateViewPropertiesCore();
            this.ScrollToSelectedTabItem(true);
            this.UpdateScrollButtonsVisibility();
        }

        public DevExpress.Xpf.Core.ScrollButtonShowMode ScrollButtonShowMode
        {
            get => 
                (DevExpress.Xpf.Core.ScrollButtonShowMode) base.GetValue(ScrollButtonShowModeProperty);
            set => 
                base.SetValue(ScrollButtonShowModeProperty, value);
        }

        public bool AllowScrollOnMouseWheel
        {
            get => 
                (bool) base.GetValue(AllowScrollOnMouseWheelProperty);
            set => 
                base.SetValue(AllowScrollOnMouseWheelProperty, value);
        }

        [Obsolete("Use the ScrollButtonShowMode property.")]
        public ButtonShowMode ScrollButtonsShowMode
        {
            get
            {
                switch (this.ScrollButtonShowMode)
                {
                    case DevExpress.Xpf.Core.ScrollButtonShowMode.Always:
                        return ButtonShowMode.Always;

                    case DevExpress.Xpf.Core.ScrollButtonShowMode.AutoHideBothButtons:
                    case DevExpress.Xpf.Core.ScrollButtonShowMode.AutoHideEachButton:
                        return ButtonShowMode.WhenNeeded;
                }
                return ButtonShowMode.Never;
            }
            set
            {
                switch (value)
                {
                    case ButtonShowMode.Always:
                        this.ScrollButtonShowMode = DevExpress.Xpf.Core.ScrollButtonShowMode.Always;
                        return;

                    case ButtonShowMode.WhenNeeded:
                        this.ScrollButtonShowMode = this.AutoHideScrollButtons ? DevExpress.Xpf.Core.ScrollButtonShowMode.AutoHideEachButton : DevExpress.Xpf.Core.ScrollButtonShowMode.AutoHideBothButtons;
                        return;
                }
                this.ScrollButtonShowMode = DevExpress.Xpf.Core.ScrollButtonShowMode.Never;
            }
        }

        [Obsolete("Use the ScrollButtonShowMode property.")]
        public bool AutoHideScrollButtons
        {
            get => 
                this.ScrollButtonShowMode == DevExpress.Xpf.Core.ScrollButtonShowMode.AutoHideEachButton;
            set => 
                this.ScrollButtonsShowMode = this.ScrollButtonsShowMode;
        }

        [Description("Gets or sets the orientation of tab headers. This is a dependency property.")]
        public DevExpress.Xpf.Core.HeaderOrientation HeaderOrientation
        {
            get => 
                (DevExpress.Xpf.Core.HeaderOrientation) base.GetValue(HeaderOrientationProperty);
            set => 
                base.SetValue(HeaderOrientationProperty, value);
        }

        [Description("Gets or sets whether the Header Panel is stretched to the tab item's size. This is a dependency property.")]
        public bool HeaderAutoFill
        {
            get => 
                (bool) base.GetValue(HeaderAutoFillProperty);
            set => 
                base.SetValue(HeaderAutoFillProperty, value);
        }

        [Description("Gets or sets whether the scroll animation is played. This is a dependency property.")]
        public bool AllowAnimation
        {
            get => 
                (bool) base.GetValue(AllowAnimationProperty);
            set => 
                base.SetValue(AllowAnimationProperty, value);
        }

        public DelegateCommand ScrollPrevCommand { get; private set; }

        public DelegateCommand ScrollNextCommand { get; private set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public DelegateCommand<bool> ScrollToSelectedCommand { get; private set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public DelegateCommand UpdateScrollButtonsCommand { get; private set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public DelegateCommand<MouseWheelEventArgs> ScrollOnMouseWheelCommand { get; private set; }

        [Description("Gets whether the Header Panel can be scrolled.")]
        public bool CanScroll
        {
            get
            {
                Func<TabPanelScrollView, bool> evaluator = <>c.<>9__51_0;
                if (<>c.<>9__51_0 == null)
                {
                    Func<TabPanelScrollView, bool> local1 = <>c.<>9__51_0;
                    evaluator = <>c.<>9__51_0 = x => x.CanScroll;
                }
                return this.ScrollPanel.Return<TabPanelScrollView, bool>(evaluator, (<>c.<>9__51_1 ??= () => false));
            }
        }

        [Description("Gets whether the Header Panel can be scrolled backward.")]
        public bool CanScrollPrev
        {
            get
            {
                Func<TabPanelScrollView, bool> evaluator = <>c.<>9__53_0;
                if (<>c.<>9__53_0 == null)
                {
                    Func<TabPanelScrollView, bool> local1 = <>c.<>9__53_0;
                    evaluator = <>c.<>9__53_0 = x => x.CanScrollPrev;
                }
                return this.ScrollPanel.Return<TabPanelScrollView, bool>(evaluator, (<>c.<>9__53_1 ??= () => false));
            }
        }

        [Description("Gets whether the Header Panel can be scrolled forward.")]
        public bool CanScrollNext
        {
            get
            {
                Func<TabPanelScrollView, bool> evaluator = <>c.<>9__55_0;
                if (<>c.<>9__55_0 == null)
                {
                    Func<TabPanelScrollView, bool> local1 = <>c.<>9__55_0;
                    evaluator = <>c.<>9__55_0 = x => x.CanScrollNext;
                }
                return this.ScrollPanel.Return<TabPanelScrollView, bool>(evaluator, (<>c.<>9__55_1 ??= () => false));
            }
        }

        [Description("Gets whether the Header Panel can be scrolled to the selected tab item.")]
        public bool CanScrollToSelectedTabItem
        {
            get
            {
                Func<bool> fallback = <>c.<>9__57_1;
                if (<>c.<>9__57_1 == null)
                {
                    Func<bool> local1 = <>c.<>9__57_1;
                    fallback = <>c.<>9__57_1 = () => false;
                }
                return this.ScrollPanel.Return<TabPanelScrollView, bool>(x => x.CanScrollTo(base.Owner.SelectedContainer), fallback);
            }
        }

        protected internal TabPanelScrollView ScrollPanel
        {
            get
            {
                Func<DXTabControl, TabPanelContainer> evaluator = <>c.<>9__59_0;
                if (<>c.<>9__59_0 == null)
                {
                    Func<DXTabControl, TabPanelContainer> local1 = <>c.<>9__59_0;
                    evaluator = <>c.<>9__59_0 = x => x.TabPanel;
                }
                Func<TabPanelContainer, TabPanelScrollView> func2 = <>c.<>9__59_1;
                if (<>c.<>9__59_1 == null)
                {
                    Func<TabPanelContainer, TabPanelScrollView> local2 = <>c.<>9__59_1;
                    func2 = <>c.<>9__59_1 = x => x.Panel as TabPanelScrollView;
                }
                return base.Owner.With<DXTabControl, TabPanelContainer>(evaluator).With<TabPanelContainer, TabPanelScrollView>(func2);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TabControlScrollView.<>c <>9 = new TabControlScrollView.<>c();
            public static Func<TabPanelScrollView, bool> <>9__51_0;
            public static Func<bool> <>9__51_1;
            public static Func<TabPanelScrollView, bool> <>9__53_0;
            public static Func<bool> <>9__53_1;
            public static Func<TabPanelScrollView, bool> <>9__55_0;
            public static Func<bool> <>9__55_1;
            public static Func<bool> <>9__57_1;
            public static Func<DXTabControl, TabPanelContainer> <>9__59_0;
            public static Func<TabPanelContainer, TabPanelScrollView> <>9__59_1;
            public static Action<TabPanelScrollView> <>9__61_0;
            public static Action<TabPanelScrollView> <>9__62_0;
            public static Action<TabPanelScrollView> <>9__63_0;
            public static Action<TabPanelScrollView> <>9__64_0;

            internal void <.cctor>b__69_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TabControlScrollView) d).UpdateViewProperties();
            }

            internal void <.cctor>b__69_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TabControlScrollView) d).UpdateScrollButtonsVisibility();
            }

            internal bool <get_CanScroll>b__51_0(TabPanelScrollView x) => 
                x.CanScroll;

            internal bool <get_CanScroll>b__51_1() => 
                false;

            internal bool <get_CanScrollNext>b__55_0(TabPanelScrollView x) => 
                x.CanScrollNext;

            internal bool <get_CanScrollNext>b__55_1() => 
                false;

            internal bool <get_CanScrollPrev>b__53_0(TabPanelScrollView x) => 
                x.CanScrollPrev;

            internal bool <get_CanScrollPrev>b__53_1() => 
                false;

            internal bool <get_CanScrollToSelectedTabItem>b__57_1() => 
                false;

            internal TabPanelContainer <get_ScrollPanel>b__59_0(DXTabControl x) => 
                x.TabPanel;

            internal TabPanelScrollView <get_ScrollPanel>b__59_1(TabPanelContainer x) => 
                x.Panel as TabPanelScrollView;

            internal void <ScrollFirst>b__61_0(TabPanelScrollView x)
            {
                x.ScrollToBegin(true);
            }

            internal void <ScrollLast>b__62_0(TabPanelScrollView x)
            {
                x.ScrollToEnd(true);
            }

            internal void <ScrollNext>b__64_0(TabPanelScrollView x)
            {
                x.ScrollNext(true);
            }

            internal void <ScrollPrev>b__63_0(TabPanelScrollView x)
            {
                x.ScrollPrev(true);
            }
        }
    }
}

