namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows;

    public interface IDXTypeEditor
    {
        void Edit(object value, Window ownerWindow);
    }
}

