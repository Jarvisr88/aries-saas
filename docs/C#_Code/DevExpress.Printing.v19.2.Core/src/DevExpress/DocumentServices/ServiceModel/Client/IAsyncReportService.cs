namespace DevExpress.DocumentServices.ServiceModel.Client
{
    using DevExpress.DocumentServices.ServiceModel.DataContracts;
    using DevExpress.Xpf.Printing;
    using System;
    using System.ServiceModel;

    [ServiceContract(Name="IReportService"), ServiceKnownType("GetKnownTypes", typeof(ServiceKnownTypeProvider))]
    public interface IAsyncReportService : IAsyncExportService
    {
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginGetBuildStatus(DocumentId documentId, AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginGetDocumentData(DocumentId documentId, AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginGetLookUpValues(InstanceIdentity identity, ReportParameter[] parameterValues, string[] requiredParameterPaths, AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginGetPages(DocumentId documentId, int[] pageIndexes, PageCompatibility compatibility, AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginGetPrintDocument(PrintId printId, AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginGetPrintStatus(PrintId printId, AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginGetReportParameters(InstanceIdentity identity, AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginStartBuild(InstanceIdentity identity, ReportBuildArgs buildArgs, AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginStartPrint(DocumentId documentId, PageCompatibility compatibility, AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginStopBuild(DocumentId documentId, AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginStopPrint(PrintId printId, AsyncCallback callback, object asyncState);
        BuildStatus EndGetBuildStatus(IAsyncResult ar);
        DocumentData EndGetDocumentData(IAsyncResult ar);
        ParameterLookUpValues[] EndGetLookUpValues(IAsyncResult ar);
        byte[] EndGetPages(IAsyncResult ar);
        byte[] EndGetPrintDocument(IAsyncResult ar);
        PrintStatus EndGetPrintStatus(IAsyncResult ar);
        ReportParameterContainer EndGetReportParameters(IAsyncResult ar);
        DocumentId EndStartBuild(IAsyncResult ar);
        PrintId EndStartPrint(IAsyncResult ar);
        void EndStopBuild(IAsyncResult ar);
        void EndStopPrint(IAsyncResult ar);
    }
}

