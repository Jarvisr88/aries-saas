namespace DevExpress.XtraPrinting.Native
{
    using System.Collections.Generic;

    public interface IMapNode<T> : IMapNode where T: IMapNode
    {
        IList<T> Nodes { get; }
    }
}

