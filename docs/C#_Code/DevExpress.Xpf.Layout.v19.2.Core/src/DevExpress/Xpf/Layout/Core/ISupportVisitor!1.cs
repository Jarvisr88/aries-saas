namespace DevExpress.Xpf.Layout.Core
{
    using System;

    public interface ISupportVisitor<T> where T: class
    {
        void Accept(IVisitor<T> visitor);
        void Accept(VisitDelegate<T> visit);
    }
}

