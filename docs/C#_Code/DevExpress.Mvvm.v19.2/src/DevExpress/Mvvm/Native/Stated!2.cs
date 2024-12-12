namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct Stated<TState, T>
    {
        public readonly TState State;
        public readonly T Value;
        public Stated(TState state, T value)
        {
            this.State = state;
            this.Value = value;
        }
    }
}

