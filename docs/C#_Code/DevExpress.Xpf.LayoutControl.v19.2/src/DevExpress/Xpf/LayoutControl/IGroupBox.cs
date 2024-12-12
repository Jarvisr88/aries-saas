namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Windows;

    public interface IGroupBox : IControl
    {
        bool DesignTimeClick(DXMouseButtonEventArgs args);
        void UpdateShadowVisibility();

        Rect MinimizeElementBounds { get; }
    }
}

