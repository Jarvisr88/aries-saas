namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Core.Internal;
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Markup;
    using System.Windows.Media.Animation;

    public interface IImplementBarItemLinkControl : IBarItemLinkControl, IFrameworkElementAPISupport, IUIElementAPI, IAnimatable, IFrameworkInputElement, IInputElement, ISupportInitialize, IQueryAmbient
    {
        bool Is<TBarItemLinkControl>() where TBarItemLinkControl: IBarItemLinkControl;
    }
}

