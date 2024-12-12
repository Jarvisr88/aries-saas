namespace DevExpress.Xpf.Bars
{
    using System;
    using System.ComponentModel;
    using System.Windows;

    [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
    public class InsertStatusBarIfNotExistAction : InsertBarAction
    {
        protected override void ExecuteCore(DependencyObject context);
    }
}

