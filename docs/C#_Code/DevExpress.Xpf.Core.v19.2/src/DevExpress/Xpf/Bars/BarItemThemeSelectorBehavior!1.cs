namespace DevExpress.Xpf.Bars
{
    using System;
    using System.ComponentModel;
    using System.Windows;

    [Browsable(false)]
    public abstract class BarItemThemeSelectorBehavior<T> : ThemeSelectorBehavior<T> where T: BarItem
    {
        protected BarItemThemeSelectorBehavior();
        protected override void Clear(T item);
        protected override void Initialize(T item);
        protected override void OnThemesCollectionChanged(ICollectionView oldValue);
        protected void UpdateAssociatedObjectResourceReference();

        protected override DependencyProperty StyleProperty { get; }
    }
}

