namespace DevExpress.Xpf.Printing
{
    using DevExpress.XtraPrinting.DataNodes;
    using System;

    public interface IVisualGroupNodeFixedFooter : IVisualGroupNode, IGroupNode, IDataNode
    {
        RowViewInfo GetFixedFooter(bool allowContentReuse);
    }
}

