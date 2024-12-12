namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public interface ILogicalOwner : IInputElement
    {
        event RoutedEventHandler Loaded;

        void AddChild(object child);
        void RemoveChild(object child);

        bool IsLoaded { get; }

        double ActualWidth { get; }

        double ActualHeight { get; }
    }
}

