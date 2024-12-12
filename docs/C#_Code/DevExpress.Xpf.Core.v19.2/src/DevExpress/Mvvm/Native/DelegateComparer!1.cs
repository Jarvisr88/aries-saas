namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Collections.Generic;

    internal class DelegateComparer<T> : IComparer<T>
    {
        private readonly Func<T, T, int> compare;

        public DelegateComparer(Func<T, T, int> compare)
        {
            this.compare = compare;
        }

        public int Compare(T x, T y) => 
            this.compare(x, y);
    }
}

