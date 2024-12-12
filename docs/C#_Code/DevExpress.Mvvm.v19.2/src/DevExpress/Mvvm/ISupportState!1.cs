namespace DevExpress.Mvvm
{
    using System;

    public interface ISupportState<T>
    {
        void RestoreState(T state);
        T SaveState();
    }
}

