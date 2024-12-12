namespace DevExpress.XtraPrinting
{
    using System;
    using System.Runtime.InteropServices;

    [ComVisible(false)]
    public interface IBasePrintable
    {
        void CreateArea(string areaName, IBrickGraphics graph);
        void Finalize(IPrintingSystem ps, ILink link);
        void Initialize(IPrintingSystem ps, ILink link);
    }
}

