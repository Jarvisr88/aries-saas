namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Bars;
    using System;
    using System.Windows;

    public interface IThemedWindowService
    {
        FrameworkElement[] GetElements();
        void RegistratorChanged(ElementRegistrator sender, ElementRegistratorChangedArgs e);
    }
}

