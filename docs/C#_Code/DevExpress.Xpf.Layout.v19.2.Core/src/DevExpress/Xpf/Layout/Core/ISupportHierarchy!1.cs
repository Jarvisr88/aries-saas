namespace DevExpress.Xpf.Layout.Core
{
    public interface ISupportHierarchy<T> : ISupportVisitor<T> where T: class
    {
        T Parent { get; }

        T[] Nodes { get; }
    }
}

