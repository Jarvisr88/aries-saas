namespace DevExpress.Xpf.Bars
{
    using System;
    using System.ComponentModel;
    using System.Windows;

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public class RemoveBarItemLinkAction : UpdateBarItemLinkActionBase
    {
        static RemoveBarItemLinkAction();
        protected override void ExecuteCore(DependencyObject context);
    }
}

