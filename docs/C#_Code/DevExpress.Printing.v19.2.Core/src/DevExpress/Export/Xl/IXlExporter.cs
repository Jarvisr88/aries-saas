namespace DevExpress.Export.Xl
{
    using DevExpress.Office.Crypto;
    using System.IO;

    public interface IXlExporter
    {
        IXlDocument CreateDocument(Stream stream);
        IXlDocument CreateDocument(Stream stream, EncryptionOptions encryptionOptions);
    }
}

