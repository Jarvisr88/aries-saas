namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using System;
    using System.Windows;

    public interface IStrategyManager
    {
        IStrategy CreateStrategy(DependencyObject target);
        IWindowStrategy CreateWindowStrategy(DependencyObject target);
        void RegisterStrategy<TTarget, TStrategy>() where TTarget: DependencyObject where TStrategy: IStrategy, new();
        void RegisterWindowStrategy<TTarget, TStrategy>() where TTarget: DependencyObject where TStrategy: IWindowStrategy, new();
    }
}

