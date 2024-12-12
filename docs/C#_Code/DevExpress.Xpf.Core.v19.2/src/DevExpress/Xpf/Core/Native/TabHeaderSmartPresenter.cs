namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class TabHeaderSmartPresenter : HeaderSmartPresenter
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty SmartPresentersProperty;
        public static readonly DependencyProperty IsSelectedProperty;
        private TabPanelBase ownerPanel;
        private double widthForHideControl;

        static TabHeaderSmartPresenter();
        public TabHeaderSmartPresenter();
        private void ClearOwnerPanel();
        private static List<TabHeaderSmartPresenter> GetSmartPresenters(FrameworkElement obj);
        protected override double GetWidthForHideControl(double fullWidth);
        private void InitOwnerPanel();
        protected override void OnHideControlChanged(bool? oldValue, bool? newValue);
        protected virtual void OnIsSelectedChanged(bool oldValue, bool newValue);
        private void OnOwnerPanelChanged(FrameworkElement oldValue, FrameworkElement newValue);
        private static void SetSmartPresenters(FrameworkElement obj, List<TabHeaderSmartPresenter> value);

        public bool IsSelected { get; set; }

        private TabPanelBase OwnerPanel { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TabHeaderSmartPresenter.<>c <>9;
            public static Func<List<TabHeaderSmartPresenter>, bool> <>9__13_1;

            static <>c();
            internal void <.cctor>b__18_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal bool <OnOwnerPanelChanged>b__13_1(List<TabHeaderSmartPresenter> x);
        }
    }
}

