namespace DevExpress.Internal.WinApi
{
    using DevExpress.Internal.WinApi.Windows.UI.Notifications;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Security;

    public static class ComFunctions
    {
        private static Dictionary<Type, Tuple<string, Guid>> knownTypes = new Dictionary<Type, Tuple<string, Guid>>();
        public const short UnmanagedType_HString = 0x2f;

        static ComFunctions()
        {
            knownTypes[typeof(IToastNotificationFactory)] = Tuple.Create<string, Guid>("Windows.UI.Notifications.ToastNotification", new Guid("04124B20-82C6-4229-B109-FD9ED4662B53"));
            knownTypes[typeof(IToastNotificationManager)] = Tuple.Create<string, Guid>("Windows.UI.Notifications.ToastNotificationManager", new Guid("50AC103F-D235-4598-BBEF-98FE4D1A3AD4"));
        }

        public static void CheckHRESULT(int hResult)
        {
            if (hResult < 0)
            {
                throw new Exception("Failed with HRESULT: " + hResult.ToString("X"));
            }
        }

        public static bool Initialize()
        {
            try
            {
                ComBaseImport.RoInitialize(RO_INIT_TYPE.RO_INIT_MULTITHREADED);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static T RoGetActivationFactory<T>()
        {
            object obj2;
            Tuple<string, Guid> tuple = knownTypes[typeof(T)];
            CheckHRESULT(ComBaseImport.RoGetActivationFactory(tuple.Item1, tuple.Item2, out obj2));
            return (T) obj2;
        }

        public static void Safe(Action action, Action<COMException> onError = null)
        {
            try
            {
                action();
            }
            catch (COMException exception)
            {
                if (onError != null)
                {
                    onError(exception);
                }
            }
        }

        [SecuritySafeCritical]
        private static class ComBaseImport
        {
            internal static int RoGetActivationFactory(string classId, Guid guid, out object iface) => 
                Unsafe.RoGetActivationFactory(classId, guid, out iface);

            internal static int RoInitialize(ComFunctions.RO_INIT_TYPE initType) => 
                Unsafe.RoInitialize(initType);

            private static class Unsafe
            {
                [DllImport("Combase.dll")]
                internal static extern int RoGetActivationFactory([MarshalAs(UnmanagedType.HString)] string classId, [In, MarshalAs(UnmanagedType.LPStruct)] Guid guid, [MarshalAs(UnmanagedType.IUnknown)] out object iface);
                [DllImport("Combase.dll")]
                internal static extern int RoInitialize(ComFunctions.RO_INIT_TYPE initType);
            }
        }

        public enum RO_INIT_TYPE
        {
            RO_INIT_MULTITHREADED = 1
        }
    }
}

