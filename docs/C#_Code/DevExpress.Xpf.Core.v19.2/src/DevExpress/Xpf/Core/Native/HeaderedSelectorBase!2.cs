namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public abstract class HeaderedSelectorBase<TContainer, TItem> : SelectorBase<TContainer, TItem> where TContainer: HeaderedSelectorBase<TContainer, TItem> where TItem: HeaderedSelectorItemBase<TContainer, TItem>
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty ItemHeaderTemplateProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty ItemHeaderTemplateSelectorProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty ItemHeaderStringFormatProperty;

        static HeaderedSelectorBase();
        protected HeaderedSelectorBase();
        protected override void ClearContainerForItemOverride(DependencyObject element, object item);
        protected virtual void OnItemHeaderStringFormatChanged(string oldValue, string newValue);
        protected virtual void OnItemHeaderTemplateChanged(DataTemplate oldValue, DataTemplate newValue);
        protected virtual void OnItemHeaderTemplateSelectorChanged(DataTemplateSelector oldValue, DataTemplateSelector newValue);
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item);
        private void UpdateContainers(DataTemplate oldItemHeaderTemplate, DataTemplateSelector oldItemHeaderTemplateSelector, string oldItemHeaderStringFormat);

        public DataTemplate ItemHeaderTemplate { get; set; }

        public DataTemplateSelector ItemHeaderTemplateSelector { get; set; }

        public string ItemHeaderStringFormat { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly HeaderedSelectorBase<TContainer, TItem>.<>c <>9;

            static <>c();
            internal void <.cctor>b__19_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__19_1(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__19_2(DependencyObject d, DependencyPropertyChangedEventArgs e);
        }
    }
}

