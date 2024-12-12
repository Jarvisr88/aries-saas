namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;

    [ContentProperty("Children")]
    public class MultiContentControl : Control
    {
        private static readonly DependencyPropertyKey ChildrenPropertyKey;
        public static readonly DependencyProperty ChildrenProperty;
        public static readonly DependencyProperty VisibleChildIndexProperty;

        static MultiContentControl();
        public MultiContentControl();
        public override void OnApplyTemplate();
        protected virtual void OnChildrenCollectionChanged(object sender, NotifyCollectionChangedEventArgs e);
        protected virtual void PropertyChangedVisibleChildIndex(object oldValue);
        protected virtual void UpdateVisibleChild();

        public ObservableCollection<UIElement> Children { get; protected set; }

        public object VisibleChildIndex { get; set; }

        private Grid RootElement { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly MultiContentControl.<>c <>9;

            static <>c();
            internal void <.cctor>b__3_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
        }
    }
}

