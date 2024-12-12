namespace DevExpress.Mvvm
{
    using System;

    public interface ISupportLogicalLayout<T> : ISupportLogicalLayout
    {
        void RestoreState(T state);
        T SaveState();
    }
}

