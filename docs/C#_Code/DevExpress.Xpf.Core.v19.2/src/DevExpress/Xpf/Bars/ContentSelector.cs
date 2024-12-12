namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class ContentSelector : Panel
    {
        public static readonly DependencyProperty SelectedIndexProperty;
        public static readonly DependencyProperty SnapperTypeProperty;
        public static readonly DependencyProperty Content1Property;
        public static readonly DependencyProperty Content2Property;

        static ContentSelector();
        public ContentSelector();
        protected override Size ArrangeOverride(Size finalSize);
        protected override Size MeasureOverride(Size availableSize);
        private static void OnContent1Changed(DependencyObject o, DependencyPropertyChangedEventArgs e);
        protected virtual void OnContent1Changed(UIElement oldValue, UIElement newValue);
        private static void OnContent2Changed(DependencyObject o, DependencyPropertyChangedEventArgs e);
        protected virtual void OnContent2Changed(UIElement oldValue, UIElement newValue);
        protected virtual void OnSelectedIndexChanged(int oldValue, int newValue);
        private static void OnSelectedIndexChanged(DependencyObject o, DependencyPropertyChangedEventArgs e);

        public UIElement Content1 { get; set; }

        public UIElement Content2 { get; set; }

        public DevExpress.Xpf.Core.SnapperType SnapperType { get; set; }

        private ContentPresenter Content1Presenter { get; set; }

        private ContentPresenter Content2Presenter { get; set; }

        public int SelectedIndex { get; set; }
    }
}

