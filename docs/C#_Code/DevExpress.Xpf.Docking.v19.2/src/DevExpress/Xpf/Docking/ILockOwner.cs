namespace DevExpress.Xpf.Docking
{
    using System;

    internal interface ILockOwner
    {
        void Lock();
        void Unlock();
    }
}

