namespace DevExpress.XtraPrinting
{
    using System;
    using System.Runtime.InteropServices;

    [ComVisible(false)]
    public interface IPrintableEx : IPrintable, IBasePrintable
    {
        void OnEndActivity();
        void OnStartActivity();
    }
}

