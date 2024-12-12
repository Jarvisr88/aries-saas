namespace DevExpress.XtraPrinting.DataNodes
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Runtime.InteropServices;

    [ComVisible(false)]
    public interface IBrickGraphicsGroupNode : IGroupNode, IDataNode
    {
        void CreateFooterArea(IBrickGraphics graph);
        void CreateHeaderArea(IBrickGraphics graph);
    }
}

