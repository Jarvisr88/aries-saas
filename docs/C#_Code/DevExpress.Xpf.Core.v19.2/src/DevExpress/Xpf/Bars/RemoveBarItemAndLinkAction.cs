namespace DevExpress.Xpf.Bars
{
    using System;
    using System.ComponentModel;
    using System.Windows;

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public class RemoveBarItemAndLinkAction : RemoveBarItemAction
    {
        protected override void ExecuteCore(DependencyObject context);
        protected virtual void RemoveLinks();
    }
}

