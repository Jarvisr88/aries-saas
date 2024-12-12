namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.XtraPrinting;
    using System;

    public interface IOnPageUpdater
    {
        void Update(IVisualBrick brick);
    }
}

