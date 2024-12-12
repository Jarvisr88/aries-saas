namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Runtime.CompilerServices;

    public interface ITargetChangedHelper<T>
    {
        event TargetChangedEventHandler<T> TargetChanged;

        void RaiseTargetChanged(T value);
    }
}

