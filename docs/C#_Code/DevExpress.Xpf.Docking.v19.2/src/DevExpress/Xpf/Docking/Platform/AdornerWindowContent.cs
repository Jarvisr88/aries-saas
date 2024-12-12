namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Controls;

    public class AdornerWindowContent : Decorator
    {
        internal AdornerWindowContent(IView view)
        {
            this.View = view;
        }

        public IView View { get; private set; }
    }
}

