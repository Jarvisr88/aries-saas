namespace DevExpress.Mvvm
{
    using System;
    using System.Runtime.CompilerServices;

    public static class MessengerExtensions
    {
        public static void Register<TMessage>(this IMessenger messenger, object recipient, Action<TMessage> action)
        {
            VerifyMessenger(messenger);
            messenger.Register<TMessage>(recipient, null, false, action);
        }

        public static void Register<TMessage>(this IMessenger messenger, object recipient, bool receiveInheritedMessagesToo, Action<TMessage> action)
        {
            VerifyMessenger(messenger);
            messenger.Register<TMessage>(recipient, null, receiveInheritedMessagesToo, action);
        }

        public static void Register<TMessage>(this IMessenger messenger, object recipient, object token, Action<TMessage> action)
        {
            VerifyMessenger(messenger);
            messenger.Register<TMessage>(recipient, token, false, action);
        }

        public static void Send<TMessage>(this IMessenger messenger, TMessage message)
        {
            VerifyMessenger(messenger);
            messenger.Send<TMessage>(message, null, null);
        }

        public static void Send<TMessage, TTarget>(this IMessenger messenger, TMessage message)
        {
            VerifyMessenger(messenger);
            messenger.Send<TMessage>(message, typeof(TTarget), null);
        }

        public static void Send<TMessage>(this IMessenger messenger, TMessage message, object token)
        {
            VerifyMessenger(messenger);
            messenger.Send<TMessage>(message, null, token);
        }

        public static void Unregister<TMessage>(this IMessenger messenger, object recipient)
        {
            VerifyMessenger(messenger);
            messenger.Unregister<TMessage>(recipient, null, null);
        }

        public static void Unregister<TMessage>(this IMessenger messenger, object recipient, Action<TMessage> action)
        {
            VerifyMessenger(messenger);
            messenger.Unregister<TMessage>(recipient, null, action);
        }

        public static void Unregister<TMessage>(this IMessenger messenger, object recipient, object token)
        {
            VerifyMessenger(messenger);
            messenger.Unregister<TMessage>(recipient, token, null);
        }

        private static void VerifyMessenger(IMessenger messenger)
        {
            if (messenger == null)
            {
                throw new ArgumentNullException("messenger");
            }
        }
    }
}

