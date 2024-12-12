namespace DevExpress.XtraPrinting
{
    public interface IDocumentSource : ILink
    {
        DevExpress.XtraPrinting.PrintingSystemBase PrintingSystemBase { get; }
    }
}

