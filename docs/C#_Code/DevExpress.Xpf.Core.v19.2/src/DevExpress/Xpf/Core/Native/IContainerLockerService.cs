namespace DevExpress.Xpf.Core.Native
{
    using System;

    public interface IContainerLockerService
    {
        void Lock();
        void Unlock();

        bool IsContainerLocked { get; }
    }
}

