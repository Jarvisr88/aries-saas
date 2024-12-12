namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public class BarStaticItemLinkControlStrategy : BarItemLinkControlStrategy
    {
        public BarStaticItemLinkControlStrategy(IBarItemLinkControl instance);
        protected override RibbonItemStyles CalcRibbonStyleInPageGroup();
        protected override RibbonItemStyles CalcRibbonStyleInQAT();
        protected override RibbonItemStyles CalcRibbonStyleInStatusBar();
        protected virtual HorizontalAlignment GetActualContentAlignment();
        [IteratorStateMachine(typeof(BarStaticItemLinkControlStrategy.<GetFocusTargets>d__12))]
        protected IEnumerable<FrameworkElement> GetFocusTargets(bool includeSelf);
        protected override bool GetIsPressed();
        protected virtual double GetMinWidth();
        public void OnActualContentAlignmentChanged(HorizontalAlignment oldValue, HorizontalAlignment newValue);
        public void OnActualMinWidthChanged(double oldValue, double newValue);
        public override bool OnMouseLeftButtonDown(MouseButtonEventArgs args);
        public override bool OnMouseLeftButtonUp(MouseButtonEventArgs args);
        public override bool SelectOnKeyTip();
        public override bool SetFocus();
        public void UpdateActualContentAlignment();
        public void UpdateActualMinWidth();
        protected override void UpdateActualPropertiesOverride();

        protected BarStaticItem StaticItem { get; }

        public BarStaticItemLink StaticItemLink { get; }

        private IBarStaticItemLinkControl StaticInstance { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BarStaticItemLinkControlStrategy.<>c <>9;
            public static Func<FrameworkElement, bool> <>9__10_0;
            public static Func<FrameworkElement, bool> <>9__12_0;

            static <>c();
            internal bool <GetFocusTargets>b__12_0(FrameworkElement x);
            internal bool <SelectOnKeyTip>b__10_0(FrameworkElement x);
        }

    }
}

