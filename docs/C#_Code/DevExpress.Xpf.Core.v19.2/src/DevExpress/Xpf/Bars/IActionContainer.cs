namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Windows;

    public interface IActionContainer : IControllerAction
    {
        DependencyObject AssociatedObject { get; set; }
    }
}

