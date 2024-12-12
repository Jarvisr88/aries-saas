namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;

    public class DXMessageBoxCreator
    {
        public virtual DXMessageBox Create() => 
            new DXMessageBox();

        public virtual Window CreateWindow() => 
            new Window();
    }
}

