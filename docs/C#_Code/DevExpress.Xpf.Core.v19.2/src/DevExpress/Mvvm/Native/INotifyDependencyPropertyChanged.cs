namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public interface INotifyDependencyPropertyChanged
    {
        event DependencyPropertyChangedEventHandler DependencyPropertyChanged;
    }
}

