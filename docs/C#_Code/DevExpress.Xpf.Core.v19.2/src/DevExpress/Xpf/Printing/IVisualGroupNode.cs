namespace DevExpress.Xpf.Printing
{
    using DevExpress.XtraPrinting.DataNodes;
    using System;

    public interface IVisualGroupNode : IGroupNode, IDataNode
    {
        RowViewInfo GetFooter(bool allowContentReuse);
        RowViewInfo GetHeader(bool allowContentReuse);
    }
}

