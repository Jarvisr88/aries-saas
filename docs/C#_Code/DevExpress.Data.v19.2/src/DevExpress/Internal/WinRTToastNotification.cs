namespace DevExpress.Internal
{
    using DevExpress.Internal.WinApi;
    using DevExpress.Internal.WinApi.Windows.UI.Notifications;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    public class WinRTToastNotification : IPredefinedToastNotification
    {
        private ToastNotificationHandlerInfo<HandlerDismissed> handlerDismissed;
        private ToastNotificationHandlerInfo<HandlerActivated> handlerActivated;
        private ToastNotificationHandlerInfo<HandlerFailed> handlerFailed;
        private IPredefinedToastNotificationContent contentCore;
        private Lazy<IToastNotificationAdapter> adapterCore;
        private IToastNotification Notification;
        private const uint WPN_E_TOAST_NOTIFICATION_DROPPED = 0x803e0207;

        internal event TypedEventHandler<IPredefinedToastNotification, string> Activated
        {
            add
            {
                this.handlerActivated.Handler.Subscribe(value);
            }
            remove
            {
                this.handlerActivated.Handler.Unsubscribe(value);
            }
        }

        internal event TypedEventHandler<IPredefinedToastNotification, ToastDismissalReason> Dismissed
        {
            add
            {
                this.handlerDismissed.Handler.Subscribe(value);
            }
            remove
            {
                this.handlerDismissed.Handler.Unsubscribe(value);
            }
        }

        internal event TypedEventHandler<IPredefinedToastNotification, ToastNotificationFailedException> Failed
        {
            add
            {
                this.handlerFailed.Handler.Subscribe(value);
            }
            remove
            {
                this.handlerFailed.Handler.Unsubscribe(value);
            }
        }

        internal WinRTToastNotification(IPredefinedToastNotificationContent content, Func<IToastNotificationAdapter> adapterRoutine)
        {
            if (content == null)
            {
                throw new ArgumentNullException("content");
            }
            this.adapterCore = new Lazy<IToastNotificationAdapter>(adapterRoutine);
            this.contentCore = content;
            this.handlerDismissed = new ToastNotificationHandlerInfo<HandlerDismissed>(() => new HandlerDismissed(this));
            this.handlerActivated = new ToastNotificationHandlerInfo<HandlerActivated>(() => new HandlerActivated(this));
            this.handlerFailed = new ToastNotificationHandlerInfo<HandlerFailed>(() => new HandlerFailed(this));
        }

        public void Hide()
        {
            if (this.Notification != null)
            {
                this.Adapter.Hide(this.Notification);
            }
        }

        public Task<ToastNotificationResultInternal> ShowAsync()
        {
            this.Notification ??= this.Adapter.Create(this.Content.Info);
            this.Subscribe();
            TaskCompletionSource<ToastNotificationResultInternal> source = new TaskCompletionSource<ToastNotificationResultInternal>(ToastActivationAsyncState.Create());
            this.Activated += delegate (IPredefinedToastNotification s, string args) {
                this.Unsubscribe();
                source.Task.SetActivationArgs(args);
                source.SetResult(ToastNotificationResultInternal.Activated);
            };
            this.Dismissed += delegate (IPredefinedToastNotification s, ToastDismissalReason reason) {
                this.Unsubscribe();
                if (reason > ToastDismissalReason.TimedOut)
                {
                    ToastDismissalReason local1 = reason;
                }
                else
                {
                    switch (((uint) reason))
                    {
                        case 0:
                            source.SetResult(ToastNotificationResultInternal.UserCanceled);
                            return;

                        case 1:
                            source.SetResult(ToastNotificationResultInternal.ApplicationHidden);
                            return;

                        case 2:
                            source.SetResult(ToastNotificationResultInternal.TimedOut);
                            return;
                    }
                }
            };
            this.Failed += delegate (IPredefinedToastNotification s, ToastNotificationFailedException exception) {
                this.Unsubscribe();
                if (exception.ErrorCode == -2143419897)
                {
                    source.SetResult(ToastNotificationResultInternal.Dropped);
                }
                else
                {
                    source.SetException(exception);
                }
            };
            ComFunctions.Safe(delegate {
                this.Adapter.Show(this.Notification);
            }, delegate (COMException ce) {
                this.Unsubscribe();
                source.SetException(new ToastNotificationFailedException(ce, ce.ErrorCode));
            });
            return source.Task;
        }

        private void Subscribe()
        {
            if (this.Notification != null)
            {
                this.Notification.AddDismissed(this.handlerDismissed.Handler, out this.handlerDismissed.Token);
                this.Notification.AddActivated(this.handlerActivated.Handler, out this.handlerActivated.Token);
                this.Notification.AddFailed(this.handlerFailed.Handler, out this.handlerFailed.Token);
            }
        }

        private void Unsubscribe()
        {
            if (this.Notification != null)
            {
                this.Notification.RemoveDismissed(this.handlerDismissed.Token);
                this.Notification.RemoveActivated(this.handlerActivated.Token);
                this.Notification.RemoveFailed(this.handlerFailed.Token);
            }
        }

        public IPredefinedToastNotificationContent Content =>
            this.contentCore;

        internal IToastNotificationAdapter Adapter =>
            this.adapterCore.Value;

        private sealed class HandlerActivated : WinRTToastNotification.ToastNotificationHandler<WinRTToastNotification.TypedEventHandler<IPredefinedToastNotification, string>>, ITypedEventHandler_IToastNotification_Activated
        {
            public HandlerActivated(WinRTToastNotification sender) : base(sender)
            {
            }

            private string GetActivationString(IToastActivatedEventArgs args)
            {
                string str = null;
                if (args != null)
                {
                    args.GetArguments(out str);
                }
                return str;
            }

            public int Invoke(IToastNotification sender, IInspectable args)
            {
                Action<WinRTToastNotification.TypedEventHandler<IPredefinedToastNotification, string>, WinRTToastNotification, string> action = <>c.<>9__1_0;
                if (<>c.<>9__1_0 == null)
                {
                    Action<WinRTToastNotification.TypedEventHandler<IPredefinedToastNotification, string>, WinRTToastNotification, string> local1 = <>c.<>9__1_0;
                    action = <>c.<>9__1_0 = delegate (WinRTToastNotification.TypedEventHandler<IPredefinedToastNotification, string> h, WinRTToastNotification s, string a) {
                        h(s, a);
                    };
                }
                return this.InvokeCore<string>(sender, this.GetActivationString(args as IToastActivatedEventArgs), action);
            }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly WinRTToastNotification.HandlerActivated.<>c <>9 = new WinRTToastNotification.HandlerActivated.<>c();
                public static Action<WinRTToastNotification.TypedEventHandler<IPredefinedToastNotification, string>, WinRTToastNotification, string> <>9__1_0;

                internal void <Invoke>b__1_0(WinRTToastNotification.TypedEventHandler<IPredefinedToastNotification, string> h, WinRTToastNotification s, string a)
                {
                    h(s, a);
                }
            }
        }

        private sealed class HandlerDismissed : WinRTToastNotification.ToastNotificationHandler<WinRTToastNotification.TypedEventHandler<IPredefinedToastNotification, ToastDismissalReason>>, ITypedEventHandler_IToastNotification_Dismissed
        {
            public HandlerDismissed(WinRTToastNotification sender) : base(sender)
            {
            }

            public int Invoke(IToastNotification sender, IToastDismissedEventArgs args)
            {
                Action<WinRTToastNotification.TypedEventHandler<IPredefinedToastNotification, ToastDismissalReason>, WinRTToastNotification, IToastDismissedEventArgs> action = <>c.<>9__1_0;
                if (<>c.<>9__1_0 == null)
                {
                    Action<WinRTToastNotification.TypedEventHandler<IPredefinedToastNotification, ToastDismissalReason>, WinRTToastNotification, IToastDismissedEventArgs> local1 = <>c.<>9__1_0;
                    action = <>c.<>9__1_0 = delegate (WinRTToastNotification.TypedEventHandler<IPredefinedToastNotification, ToastDismissalReason> h, WinRTToastNotification s, IToastDismissedEventArgs a) {
                        h(s, a.Reason);
                    };
                }
                return this.InvokeCore<IToastDismissedEventArgs>(sender, args, action);
            }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly WinRTToastNotification.HandlerDismissed.<>c <>9 = new WinRTToastNotification.HandlerDismissed.<>c();
                public static Action<WinRTToastNotification.TypedEventHandler<IPredefinedToastNotification, ToastDismissalReason>, WinRTToastNotification, IToastDismissedEventArgs> <>9__1_0;

                internal void <Invoke>b__1_0(WinRTToastNotification.TypedEventHandler<IPredefinedToastNotification, ToastDismissalReason> h, WinRTToastNotification s, IToastDismissedEventArgs a)
                {
                    h(s, a.Reason);
                }
            }
        }

        private sealed class HandlerFailed : WinRTToastNotification.ToastNotificationHandler<WinRTToastNotification.TypedEventHandler<IPredefinedToastNotification, ToastNotificationFailedException>>, ITypedEventHandler_IToastNotification_Failed
        {
            public HandlerFailed(WinRTToastNotification sender) : base(sender)
            {
            }

            public int Invoke(IToastNotification sender, IToastFailedEventArgs args)
            {
                Action<WinRTToastNotification.TypedEventHandler<IPredefinedToastNotification, ToastNotificationFailedException>, WinRTToastNotification, IToastFailedEventArgs> action = <>c.<>9__1_0;
                if (<>c.<>9__1_0 == null)
                {
                    Action<WinRTToastNotification.TypedEventHandler<IPredefinedToastNotification, ToastNotificationFailedException>, WinRTToastNotification, IToastFailedEventArgs> local1 = <>c.<>9__1_0;
                    action = <>c.<>9__1_0 = delegate (WinRTToastNotification.TypedEventHandler<IPredefinedToastNotification, ToastNotificationFailedException> h, WinRTToastNotification s, IToastFailedEventArgs a) {
                        h(s, ToastNotificationFailedException.ToException(a.Error));
                    };
                }
                return this.InvokeCore<IToastFailedEventArgs>(sender, args, action);
            }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly WinRTToastNotification.HandlerFailed.<>c <>9 = new WinRTToastNotification.HandlerFailed.<>c();
                public static Action<WinRTToastNotification.TypedEventHandler<IPredefinedToastNotification, ToastNotificationFailedException>, WinRTToastNotification, IToastFailedEventArgs> <>9__1_0;

                internal void <Invoke>b__1_0(WinRTToastNotification.TypedEventHandler<IPredefinedToastNotification, ToastNotificationFailedException> h, WinRTToastNotification s, IToastFailedEventArgs a)
                {
                    h(s, ToastNotificationFailedException.ToException(a.Error));
                }
            }
        }

        private class ToastNotificationHandler<THandler>
        {
            private List<THandler> handlers;
            private WinRTToastNotification toast;

            public ToastNotificationHandler(WinRTToastNotification toast)
            {
                this.handlers = new List<THandler>();
                this.toast = toast;
            }

            protected int InvokeCore<TArgs>(IToastNotification sender, TArgs args, Action<THandler, WinRTToastNotification, TArgs> action)
            {
                lock (handler)
                {
                    using (this.toast.Content)
                    {
                        this.handlers.ForEach(delegate (THandler h) {
                            action(h, ((WinRTToastNotification.ToastNotificationHandler<THandler>) this).toast, args);
                        });
                        this.handlers.Clear();
                    }
                }
                return 0;
            }

            public void Subscribe(THandler handler)
            {
                this.handlers.Add(handler);
            }

            public void Unsubscribe(THandler handler)
            {
                this.handlers.Remove(handler);
            }
        }

        private class ToastNotificationHandlerInfo<THandler>
        {
            public EventRegistrationToken Token;

            public ToastNotificationHandlerInfo(Func<THandler> initializer)
            {
                this.Handler = initializer();
            }

            public THandler Handler { get; private set; }
        }

        internal delegate void TypedEventHandler<TSender, TResult>(TSender sender, TResult args);
    }
}

