namespace DevExpress.Utils
{
    using System;
    using System.Reflection;

    public interface IVector<T>
    {
        int Count { get; }

        T this[int index] { get; set; }
    }
}

