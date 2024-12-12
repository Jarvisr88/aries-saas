namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    public interface IVisualOwner : IInputElement
    {
        void AddChild(Visual child);
        void RemoveChild(Visual child);
    }
}

