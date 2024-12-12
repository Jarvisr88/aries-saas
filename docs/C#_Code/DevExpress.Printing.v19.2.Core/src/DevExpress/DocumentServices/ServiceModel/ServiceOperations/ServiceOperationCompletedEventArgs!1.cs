namespace DevExpress.DocumentServices.ServiceModel.ServiceOperations
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public abstract class ServiceOperationCompletedEventArgs<TOperationId> : AsyncCompletedEventArgs
    {
        public ServiceOperationCompletedEventArgs(TOperationId operationId, Exception error, bool cancelled, object userState) : base(error, cancelled, userState)
        {
            this.OperationId = operationId;
        }

        public TOperationId OperationId { get; private set; }
    }
}

