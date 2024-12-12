namespace DevExpress.XtraPrinting
{
    using System;
    using System.Runtime.InteropServices;

    [ComVisible(false)]
    public interface IBasePrintableProvider
    {
        object GetIPrintableImplementer();
    }
}

