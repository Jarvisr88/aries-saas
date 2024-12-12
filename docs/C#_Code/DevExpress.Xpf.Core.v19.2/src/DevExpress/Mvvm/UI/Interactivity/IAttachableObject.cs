namespace DevExpress.Mvvm.UI.Interactivity
{
    using System;
    using System.Windows;

    public interface IAttachableObject
    {
        void Attach(DependencyObject dependencyObject);
        void Detach();

        DependencyObject AssociatedObject { get; }
    }
}

