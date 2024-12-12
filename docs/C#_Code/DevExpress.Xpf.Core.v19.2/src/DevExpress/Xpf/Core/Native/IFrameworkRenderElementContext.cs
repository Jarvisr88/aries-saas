namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows;

    public interface IFrameworkRenderElementContext
    {
        bool CaptureMouse();
        FrameworkRenderElementContext GetRenderChild(int index);
        object GetValue(string propertyName);
        void InvalidateArrangeInternal();
        void InvalidateMeasureInternal();
        void OnGotMouseCapture();
        void OnLostMouseCapture();
        void OnMouseDown(MouseRenderEventArgs args);
        void OnMouseEnter(MouseRenderEventArgs args);
        void OnMouseLeave(MouseRenderEventArgs args);
        void OnMouseMove(MouseRenderEventArgs args);
        void OnMouseUp(MouseRenderEventArgs args);
        void OnPreviewMouseDown(MouseRenderEventArgs args);
        void OnPreviewMouseUp(MouseRenderEventArgs args);
        void ReleaseMouseCapture();
        void SetValue(string propertyName, object value);

        double DpiScale { get; }

        Size DesiredSize { get; }

        Size RenderSize { get; }

        Rect RenderRect { get; }

        Vector VisualOffset { get; }

        object DataContext { get; set; }

        int RenderChildrenCount { get; }
    }
}

