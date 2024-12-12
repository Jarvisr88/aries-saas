namespace DevExpress.ReportServer.ServiceModel.DataContracts
{
    using System;
    using System.Runtime.Serialization;
    using System.ServiceModel;

    [DataContract]
    public class ReportNotCheckedOutFault
    {
        public static FaultException<ReportNotCheckedOutFault> CreateFaultException(string reason) => 
            new FaultException<ReportNotCheckedOutFault>(new ReportNotCheckedOutFault(), reason);
    }
}

