namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using System;
    using System.Collections;
    using System.Windows;

    public interface IPanelWrapper<T> : ITargetWrapper<T> where T: DependencyObject
    {
        void AddChild(UIElement child);
        void RemoveChild(UIElement child);

        IEnumerable Children { get; }
    }
}

