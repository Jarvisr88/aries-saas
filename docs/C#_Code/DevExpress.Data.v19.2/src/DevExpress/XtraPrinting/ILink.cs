namespace DevExpress.XtraPrinting
{
    using System;

    public interface ILink
    {
        void CreateDocument();
        void CreateDocument(bool buildForInstantPreview);

        IPrintingSystem PrintingSystem { get; }
    }
}

