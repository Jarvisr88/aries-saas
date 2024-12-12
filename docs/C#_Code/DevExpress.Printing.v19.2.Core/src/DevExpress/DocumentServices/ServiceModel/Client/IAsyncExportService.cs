namespace DevExpress.DocumentServices.ServiceModel.Client
{
    using DevExpress.DocumentServices.ServiceModel.DataContracts;
    using DevExpress.Xpf.Printing;
    using System;
    using System.ServiceModel;

    [ServiceContract(Name="IExportService"), ServiceKnownType("GetKnownTypes", typeof(ServiceKnownTypeProvider))]
    public interface IAsyncExportService
    {
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginClearDocument(DocumentId documentId, AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginGetExportedDocument(ExportId exportId, AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginGetExportStatus(ExportId exportId, AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginStartExport(DocumentId documentId, DocumentExportArgs exportArgs, AsyncCallback callback, object asyncState);
        void EndClearDocument(IAsyncResult ar);
        byte[] EndGetExportedDocument(IAsyncResult ar);
        ExportStatus EndGetExportStatus(IAsyncResult ar);
        ExportId EndStartExport(IAsyncResult ar);
    }
}

