namespace DevExpress.Xpf.Layout.Core
{
    using System;

    public interface IVisitor<T> where T: class
    {
        void Visit(T element);
    }
}

