namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    public class CheckEditChrome : ButtonChrome2
    {
        public static readonly DependencyProperty CheckSizeProperty;
        private double checkDisabledStateOpacity;

        static CheckEditChrome();
        protected override Size ArrangeOverride(Size finalSize);
        protected override HitTestResult HitTestCore(PointHitTestParameters hitTestParameters);
        protected override Size MeasureOverride(Size availableSize);
        protected override void OnTemplateProviderChanged(ChromeStateProviderBase oldValue);
        public virtual void UpdateContentPresenterProperties();

        public bool HasContent { get; }

        public Size CheckSize { get; set; }

        public double CheckDisabledStateOpacity { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CheckEditChrome.<>c <>9;
            public static Func<CheckEditChromeStateProvider, CheckEditBox> <>9__4_0;
            public static Func<CheckEditBox, object> <>9__4_1;

            static <>c();
            internal CheckEditBox <get_HasContent>b__4_0(CheckEditChromeStateProvider x);
            internal object <get_HasContent>b__4_1(CheckEditBox x);
        }
    }
}

