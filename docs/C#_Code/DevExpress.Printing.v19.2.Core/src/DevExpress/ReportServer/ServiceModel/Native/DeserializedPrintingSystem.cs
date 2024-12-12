namespace DevExpress.ReportServer.ServiceModel.Native
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;

    public class DeserializedPrintingSystem : PrintingSystemBase
    {
        protected override PrintingDocument CreateDocument() => 
            new DeserializedDocument(this);
    }
}

