namespace DevExpress.Xpf.Printing
{
    using DevExpress.XtraPrinting.DataNodes;
    using System;

    public interface IVisualDetailNode : IDataNode
    {
        RowViewInfo GetDetail(bool allowContentReuse);
    }
}

