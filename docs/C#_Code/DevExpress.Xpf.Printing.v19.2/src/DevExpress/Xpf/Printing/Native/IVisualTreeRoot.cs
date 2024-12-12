namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Windows;

    internal interface IVisualTreeRoot
    {
        bool Active { get; set; }

        UIElement Content { get; set; }
    }
}

