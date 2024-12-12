namespace DevExpress.Xpf.Bars
{
    using System;
    using System.ComponentModel;
    using System.Windows;

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public class RemoveBarAction : BarActionBase
    {
        static RemoveBarAction();
        protected override void ExecuteCore(DependencyObject context);
    }
}

