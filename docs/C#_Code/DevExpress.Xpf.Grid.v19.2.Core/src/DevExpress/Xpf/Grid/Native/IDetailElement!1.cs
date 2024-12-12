namespace DevExpress.Xpf.Grid.Native
{
    using System;

    public interface IDetailElement<T> where T: class
    {
        T CreateNewInstance(params object[] args);
    }
}

