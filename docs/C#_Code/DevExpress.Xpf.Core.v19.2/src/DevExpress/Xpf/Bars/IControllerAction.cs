namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;

    public interface IControllerAction
    {
        void Execute(DependencyObject context = null);

        IActionContainer Container { get; set; }
    }
}

