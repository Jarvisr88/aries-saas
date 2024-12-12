namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct TreeWrapper<T, TValue>
    {
        public readonly T Item;
        public readonly TValue Value;
        public readonly TreeWrapper<T, TValue>[] Children;
        public TreeWrapper(T item, TValue value, TreeWrapper<T, TValue>[] children)
        {
            this.Item = item;
            this.Value = value;
            this.Children = children;
        }
    }
}

