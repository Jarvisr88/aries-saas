namespace DevExpress.XtraPrinting.DataNodes
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Runtime.InteropServices;

    [ComVisible(false)]
    public interface IBrickGraphicsDataNode : IDataNode
    {
        void CreateDetailArea(IBrickGraphics graph);
    }
}

