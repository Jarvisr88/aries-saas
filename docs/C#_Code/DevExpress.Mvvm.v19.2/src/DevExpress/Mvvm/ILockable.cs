namespace DevExpress.Mvvm
{
    using System;

    public interface ILockable
    {
        void BeginUpdate();
        void EndUpdate();

        bool IsLockUpdate { get; }
    }
}

