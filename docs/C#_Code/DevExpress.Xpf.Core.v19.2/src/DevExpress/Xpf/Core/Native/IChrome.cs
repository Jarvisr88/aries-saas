namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows;

    public interface IChrome
    {
        void AddChild(FrameworkElement element);
        bool CaptureMouse(FrameworkRenderElementContext context);
        void GoToState(string stateName);
        void InvalidateArrange();
        void InvalidateMeasure();
        void InvalidateVisual();
        void ReleaseMouseCapture(FrameworkRenderElementContext context);
        void RemoveChild(FrameworkElement element);

        bool IsLoaded { get; }

        FrameworkRenderElementContext Root { get; }

        double DpiScale { get; }
    }
}

