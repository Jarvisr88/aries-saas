namespace DevExpress.Xpo.Logger.Transport
{
    using DevExpress.Xpo.Logger;
    using System;
    using System.ServiceModel;

    [ServiceContract]
    public interface ILogSource
    {
        [OperationContract]
        LogMessage[] GetCompleteLog();
        [OperationContract]
        LogMessage GetMessage();
        [OperationContract]
        LogMessage[] GetMessages(int messageAmount);
    }
}

