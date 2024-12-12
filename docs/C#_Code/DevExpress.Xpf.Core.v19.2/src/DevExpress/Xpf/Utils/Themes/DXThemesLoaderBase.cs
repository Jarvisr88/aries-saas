namespace DevExpress.Xpf.Utils.Themes
{
    using System;
    using System.Windows.Controls;

    public abstract class DXThemesLoaderBase : ContentControl
    {
        protected DXThemesLoaderBase()
        {
        }

        protected abstract Type TargetType { get; }
    }
}

