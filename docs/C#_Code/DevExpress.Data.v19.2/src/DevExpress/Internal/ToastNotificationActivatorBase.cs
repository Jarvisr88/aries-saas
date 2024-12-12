namespace DevExpress.Internal
{
    using DevExpress.Data.Utils;
    using DevExpress.Internal.WinApi.Windows.UI.Notifications;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Security;

    [ClassInterface(ClassInterfaceType.None), ComSourceInterfaces(typeof(INotificationActivationCallback))]
    public abstract class ToastNotificationActivatorBase : INotificationActivationCallback
    {
        protected ToastNotificationActivatorBase()
        {
        }

        internal static void CheckType(Type activatorType)
        {
            if (activatorType == null)
            {
                throw new ArgumentNullException("activatorType");
            }
            if (!activatorType.IsSubclassOf(typeof(ToastNotificationActivatorBase)))
            {
                throw new ArgumentException($"{activatorType.Name} must inherit from {typeof(ToastNotificationActivatorBase).Name}.");
            }
            if (activatorType.IsAbstract)
            {
                throw new ArgumentException($"{activatorType.Name} must provide its own implementation.");
            }
        }

        void INotificationActivationCallback.Activate(string appUserModelId, string invokedArgs, NOTIFICATION_USER_INPUT_DATA[] data, uint dataCount)
        {
            int capacity = (data != null) ? Math.Min((int) dataCount, data.Length) : 0;
            Dictionary<string, string> dictionary = new Dictionary<string, string>(capacity);
            for (int i = 0; i < capacity; i++)
            {
                dictionary.Add(data[i].Key, data[i].Value);
            }
            this.OnActivate(invokedArgs, dictionary);
        }

        public abstract void OnActivate(string arguments, Dictionary<string, string> data);
        internal static void Register(Type activatorType)
        {
            if (!FrameworkVersions.IsNetCore3AndAbove())
            {
                UnsafeRegistrator.Register(activatorType);
            }
        }

        internal static void Unregister()
        {
            if (!FrameworkVersions.IsNetCore3AndAbove())
            {
                UnsafeRegistrator.Unregister();
            }
        }

        [SecuritySafeCritical]
        private static class UnsafeRegistrator
        {
            private static int? cookie;

            internal static void Register(Type activatorType)
            {
                ToastNotificationActivatorBase.CheckType(activatorType);
                if (cookie == null)
                {
                    cookie = new int?(new RegistrationServices().RegisterTypeForComClients(activatorType, RegistrationClassContext.LocalServer, RegistrationConnectionType.MultipleUse));
                }
            }

            internal static void Unregister()
            {
                if (cookie != null)
                {
                    new RegistrationServices().UnregisterTypeForComClients(cookie.Value);
                    cookie = null;
                }
            }
        }
    }
}

