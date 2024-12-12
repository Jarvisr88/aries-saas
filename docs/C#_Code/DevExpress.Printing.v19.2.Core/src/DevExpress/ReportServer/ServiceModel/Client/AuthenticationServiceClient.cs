namespace DevExpress.ReportServer.ServiceModel.Client
{
    using DevExpress.Data.Utils.ServiceModel;
    using System;
    using System.Threading.Tasks;

    public class AuthenticationServiceClient : ServiceClientBase, IAuthenticationServiceClient, IServiceClientBase
    {
        public AuthenticationServiceClient(IAuthenticationServiceAsync channel) : base((IChannel) channel)
        {
        }

        public void Login(string userName, string password, object asyncState, Action<ScalarOperationCompletedEventArgs<bool>> onCompleted)
        {
            AsyncCallback callback = delegate (IAsyncResult ar) {
                Func<EventHandler<ScalarOperationCompletedEventArgs<bool>>> <>9__1;
                IAuthenticationServiceAsync channel = this.Channel;
                Func<EventHandler<ScalarOperationCompletedEventArgs<bool>>> getCompletedEvent = <>9__1;
                if (<>9__1 == null)
                {
                    Func<EventHandler<ScalarOperationCompletedEventArgs<bool>>> local1 = <>9__1;
                    getCompletedEvent = <>9__1 = delegate {
                        EventHandler<ScalarOperationCompletedEventArgs<bool>> <>9__2;
                        EventHandler<ScalarOperationCompletedEventArgs<bool>> handler2 = <>9__2;
                        if (<>9__2 == null)
                        {
                            EventHandler<ScalarOperationCompletedEventArgs<bool>> local1 = <>9__2;
                            handler2 = <>9__2 = (s, args) => onCompleted(args);
                        }
                        return handler2;
                    };
                }
                this.EndScalarOperation<bool>(ar, new Func<IAsyncResult, bool>(channel.EndLogin), getCompletedEvent);
            };
            this.Channel.BeginLogin(userName, password, callback, asyncState);
        }

        public Task<bool> LoginAsync(string userName, string password, object asyncState)
        {
            IAuthenticationServiceAsync channel = this.Channel;
            IAuthenticationServiceAsync async2 = this.Channel;
            return Task.Factory.FromAsync<string, string, bool>(new Func<string, string, AsyncCallback, object, IAsyncResult>(channel.BeginLogin), new Func<IAsyncResult, bool>(async2.EndLogin), userName, password, asyncState);
        }

        protected IAuthenticationServiceAsync Channel =>
            (IAuthenticationServiceAsync) base.Channel;
    }
}

