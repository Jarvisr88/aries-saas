namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Windows;

    internal interface ILayoutContent
    {
        object Content { get; set; }

        UIElement ContentPresenter { get; }

        UIElement Control { get; }
    }
}

