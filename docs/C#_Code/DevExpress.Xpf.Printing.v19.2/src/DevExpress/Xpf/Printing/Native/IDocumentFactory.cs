namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;

    internal interface IDocumentFactory
    {
        PrintingDocument Create(PrintingSystemBase ps);
    }
}

