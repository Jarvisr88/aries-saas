namespace DevExpress.Xpf.Layout.Core
{
    using System;

    public interface IUIServiceListener
    {
        object Key { get; }

        IUIServiceProvider ServiceProvider { get; set; }
    }
}

