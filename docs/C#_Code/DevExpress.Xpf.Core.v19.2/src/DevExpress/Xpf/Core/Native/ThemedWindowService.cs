namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Bars;
    using System;
    using System.Windows;

    public class ThemedWindowService : IThemedWindowService
    {
        private ThemedWindowDecorator decorator;

        public ThemedWindowService(ThemedWindowDecorator decorator);
        public FrameworkElement[] GetElements();
        public virtual void RegistratorChanged(ElementRegistrator sender, ElementRegistratorChangedArgs e);

        protected ThemedWindowDecorator Decorator { get; }
    }
}

