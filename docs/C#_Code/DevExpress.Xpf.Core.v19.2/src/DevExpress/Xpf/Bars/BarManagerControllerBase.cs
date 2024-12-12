namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public abstract class BarManagerControllerBase : FrameworkElement, IActionContainer, IControllerAction
    {
        void IControllerAction.Execute(DependencyObject context);
        public abstract void Execute(DependencyObject context = null);

        public DependencyObject Owner { get; set; }

        DependencyObject IActionContainer.AssociatedObject { get; set; }

        IActionContainer IControllerAction.Container { get; set; }
    }
}

