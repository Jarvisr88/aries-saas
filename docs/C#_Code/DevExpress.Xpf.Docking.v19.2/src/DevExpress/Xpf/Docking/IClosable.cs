namespace DevExpress.Xpf.Docking
{
    using System;

    internal interface IClosable
    {
        bool CanClose();
        void OnClosed();
    }
}

