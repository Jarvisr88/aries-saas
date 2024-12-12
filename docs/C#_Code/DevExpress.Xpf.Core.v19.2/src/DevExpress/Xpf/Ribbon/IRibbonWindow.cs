namespace DevExpress.Xpf.Ribbon
{
    using System;
    using System.Windows;

    public interface IRibbonWindow
    {
        void ApplyWindowKind();
        UIElement GetContentContainer();
        FrameworkElement GetControlBoxContainer();
        Rect GetControlBoxRect();
        UIElement GetWindowHeaderElement();
        void HideWindowIcon();
        void ShowWindowIcon();

        IRibbonControl Ribbon { get; set; }

        bool ShowIcon { get; }

        bool IsCaptionVisible { get; set; }

        bool IsRibbonCaptionVisible { get; set; }
    }
}

