namespace DevExpress.ReportServer.ServiceModel.Native.RemoteOperations
{
    using DevExpress.Data.Utils.ServiceModel;
    using DevExpress.DocumentServices.ServiceModel.Client;
    using DevExpress.DocumentServices.ServiceModel.DataContracts;
    using DevExpress.DocumentServices.ServiceModel.ServiceOperations;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public abstract class RemoteScalarOperation<T> : ReportServiceOperation
    {
        [CompilerGenerated]
        private EventHandler<ScalarOperationCompletedEventArgs<T>> OperationCompleted;

        public event EventHandler<ScalarOperationCompletedEventArgs<T>> OperationCompleted
        {
            [CompilerGenerated] add
            {
                EventHandler<ScalarOperationCompletedEventArgs<T>> operationCompleted = this.OperationCompleted;
                while (true)
                {
                    EventHandler<ScalarOperationCompletedEventArgs<T>> comparand = operationCompleted;
                    EventHandler<ScalarOperationCompletedEventArgs<T>> handler3 = comparand + value;
                    operationCompleted = Interlocked.CompareExchange<EventHandler<ScalarOperationCompletedEventArgs<T>>>(ref this.OperationCompleted, handler3, comparand);
                    if (ReferenceEquals(operationCompleted, comparand))
                    {
                        return;
                    }
                }
            }
            [CompilerGenerated] remove
            {
                EventHandler<ScalarOperationCompletedEventArgs<T>> operationCompleted = this.OperationCompleted;
                while (true)
                {
                    EventHandler<ScalarOperationCompletedEventArgs<T>> comparand = operationCompleted;
                    EventHandler<ScalarOperationCompletedEventArgs<T>> handler3 = comparand - value;
                    operationCompleted = Interlocked.CompareExchange<EventHandler<ScalarOperationCompletedEventArgs<T>>>(ref this.OperationCompleted, handler3, comparand);
                    if (ReferenceEquals(operationCompleted, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public RemoteScalarOperation(IReportServiceClient client, TimeSpan updateInterval, DocumentId documentId) : base(client, updateInterval, documentId)
        {
        }

        protected void RaiseOperationCompleted(ScalarOperationCompletedEventArgs<T> eventArgs)
        {
            if (this.OperationCompleted != null)
            {
                this.OperationCompleted(this, eventArgs);
            }
        }
    }
}

