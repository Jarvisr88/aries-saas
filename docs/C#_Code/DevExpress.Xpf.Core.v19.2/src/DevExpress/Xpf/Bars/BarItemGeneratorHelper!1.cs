namespace DevExpress.Xpf.Bars
{
    using DevExpress.Utils.Design;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public class BarItemGeneratorHelper<T> where T: DependencyObject
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        protected readonly DependencyProperty AtatachedBehaviorProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        protected readonly DependencyProperty ItemStyleProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        protected readonly DependencyProperty ItemStyleSelectorProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        protected readonly DependencyProperty ItemTemplateProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        protected readonly DependencyProperty ItemTemplateSelectorProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        protected DependencyProperty ItemsSourceProperty;

        public BarItemGeneratorHelper(T owner, DependencyProperty atatachedBehaviorProperty, DependencyProperty itemStyleProperty, DependencyProperty itemStyleSelectorProperty, DependencyProperty itemTemplateProperty, CommonBarItemCollection targetCollection, DependencyProperty itemTemplateSelectorProperty, bool itemLinksSourceElementGeneratesUniqueBarItem = true);
        private void InsertItemAction(int index, object item);
        public void OnItemsSourceChanged(DependencyPropertyChangedEventArgs e);
        private void OnScopeChanged(object sender, RoutedEventArgs e);
        private void Recreate();

        public T Owner { get; private set; }

        protected bool ItemLinksSourceElementGeneratesUniqueBarItem { get; private set; }

        protected CommonBarItemCollection TargetCollection { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BarItemGeneratorHelper<T>.<>c <>9;
            public static Func<T, BarItem> <>9__21_1;
            public static Func<BarItem, bool> <>9__21_3;
            public static Func<BarManager, bool> <>9__21_5;
            public static Func<BarManager, ImageType> <>9__21_6;
            public static Func<ImageType> <>9__21_7;
            public static Func<BarItemLink, BarItem> <>9__22_0;
            public static Func<BarItem, bool> <>9__22_1;
            public static Func<BarItem, BarItemLink> <>9__22_3;

            static <>c();
            internal BarItem <InsertItemAction>b__22_0(BarItemLink x);
            internal bool <InsertItemAction>b__22_1(BarItem x);
            internal BarItemLink <InsertItemAction>b__22_3(BarItem x);
            internal BarItem <OnItemsSourceChanged>b__21_1(T owner);
            internal bool <OnItemsSourceChanged>b__21_3(BarItem i);
            internal bool <OnItemsSourceChanged>b__21_5(BarManager x);
            internal ImageType <OnItemsSourceChanged>b__21_6(BarManager x);
            internal ImageType <OnItemsSourceChanged>b__21_7();
        }
    }
}

