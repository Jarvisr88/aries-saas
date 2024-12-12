namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;
    using System.Windows.Media;

    [ContentProperty("Child")]
    public class ChromeBase2 : Panel
    {
        public static readonly DependencyProperty RenderInfoProperty;
        public static readonly DependencyProperty TemplateProviderProperty;
        public static readonly DependencyProperty ContentMarginProperty;
        public static readonly DependencyProperty ForegroundInfoProperty;
        public static readonly DependencyProperty ChildProperty;
        private string state;
        private readonly RenderStrategy strategy;
        private double disabledStateOpacity;

        static ChromeBase2();
        public ChromeBase2();
        protected virtual void AddNewChild(UIElement newChild);
        protected override Size ArrangeOverride(Size finalSize);
        protected override Size MeasureOverride(Size availableSize);
        protected void OnChildChanged(UIElement oldChild, UIElement newChild);
        private void OnForegroundInfoChanged();
        protected virtual void OnIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e);
        protected override void OnRender(DrawingContext dc);
        private void OnRenderInfoChanged();
        private void OnStateChanged();
        protected virtual void OnTemplateProviderChanged(ChromeStateProviderBase oldValue);
        protected override void OnVisualParentChanged(DependencyObject oldParent);
        protected virtual void UpdateDisabledStateOpacity();
        protected virtual void UpdateTemplateProviderSource();

        protected RenderStrategy Strategy { get; }

        public DevExpress.Xpf.Core.Native.RenderInfo RenderInfo { get; set; }

        public string State { get; set; }

        public double DisabledStateOpacity { get; set; }

        public ChromeStateProviderBase TemplateProvider { get; set; }

        public Thickness ContentMargin { get; set; }

        public DevExpress.Xpf.Core.Native.ForegroundInfo ForegroundInfo { get; set; }

        public UIElement Child { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ChromeBase2.<>c <>9;

            static <>c();
            internal void <.cctor>b__5_0(DependencyObject d, DependencyPropertyChangedEventArgs _);
            internal void <.cctor>b__5_1(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__5_2(DependencyObject d, DependencyPropertyChangedEventArgs _);
            internal void <.cctor>b__5_3(DependencyObject d, DependencyPropertyChangedEventArgs e);
        }
    }
}

