namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public abstract class HeaderedSelectorItemBase<TContainer, TItem> : SelectorItemBase<TContainer, TItem> where TContainer: HeaderedSelectorBase<TContainer, TItem> where TItem: HeaderedSelectorItemBase<TContainer, TItem>
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty HeaderProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty HeaderTemplateProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty HeaderTemplateSelectorProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty HeaderStringFormatProperty;

        static HeaderedSelectorItemBase();
        protected HeaderedSelectorItemBase();
        protected virtual void OnHeaderChanged(object oldValue, object newValue);
        protected virtual void OnHeaderStringFormatChanged(string oldValue, string newValue);
        protected virtual void OnHeaderTemplateChanged(DataTemplate oldValue, DataTemplate newValue);
        protected virtual void OnHeaderTemplateSelectorChanged(DataTemplateSelector oldValue, DataTemplateSelector newValue);

        public object Header { get; set; }

        public DataTemplate HeaderTemplate { get; set; }

        public DataTemplateSelector HeaderTemplateSelector { get; set; }

        public string HeaderStringFormat { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly HeaderedSelectorItemBase<TContainer, TItem>.<>c <>9;

            static <>c();
            internal void <.cctor>b__21_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__21_1(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__21_2(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__21_3(DependencyObject d, DependencyPropertyChangedEventArgs e);
        }
    }
}

