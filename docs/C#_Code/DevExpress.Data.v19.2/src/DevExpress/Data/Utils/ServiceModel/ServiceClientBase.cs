namespace DevExpress.Data.Utils.ServiceModel
{
    using System;
    using System.ServiceModel.Channels;
    using System.Threading;

    public abstract class ServiceClientBase : IServiceClientBase
    {
        private readonly IChannel channel;
        private SynchronizationContext syncContext;

        protected ServiceClientBase(IChannel channel);
        public void Abort();
        public void CloseAsync();
        protected void EndScalarOperation<T>(IAsyncResult ar, Func<IAsyncResult, T> endOperation, Func<EventHandler<ScalarOperationCompletedEventArgs<T>>> getCompletedEvent);
        protected void EndVoidOperation(IAsyncResult ar, Action<IAsyncResult> endOperation, Func<EventHandler<AsyncCompletedEventArgs>> getCompletedEvent);
        private void RaiseOperationCompleted<TEventArgs>(Func<EventHandler<TEventArgs>> getCompletedEvent, Func<TEventArgs> getEventArgs) where TEventArgs: AsyncCompletedEventArgs;
        protected void RaiseScalarOperationCompleted<T>(Func<EventHandler<ScalarOperationCompletedEventArgs<T>>> getCompletedEvent, object result, Exception exception, bool cancelled, object asyncState);
        protected void RaiseVoidOperationCompleted(Func<EventHandler<AsyncCompletedEventArgs>> getCompletedEvent, Exception exception, bool cancelled, object asyncState);
        public void SetSynchronizationContext(SynchronizationContext syncContext);

        protected IChannel Channel { get; }
    }
}

