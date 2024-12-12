namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Internal;
    using DevExpress.Xpf.Core.ReflectionExtensions;
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Threading;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Interop;
    using System.Windows.Threading;

    public static class ClearAutomationEventsHelper
    {
        [ThreadStatic]
        private static ReflectionHelper helper;
        private static Type clmType;

        static ClearAutomationEventsHelper()
        {
            IsEnabled = !IsScreenReaderActive() && !IsNarratorActive();
        }

        public static void ClearAutomationEvents()
        {
            if (IsEnabled && !BrowserInteropHelper.IsBrowserHosted)
            {
                if (Helper.HasContent)
                {
                    ClearAutomationEventsImpl();
                }
                else
                {
                    ResetUpdatePeerCallback();
                    ClearAutomationEventsImpl();
                    RemoveAutomationPeersAtAll();
                }
            }
        }

        private static void ClearAutomationEventsImpl()
        {
            object entity = Helper.GetStaticMethodHandler<Func<object, object>>(ClmType, "From", BindingFlags.NonPublic | BindingFlags.Static)(Dispatcher.CurrentDispatcher);
            object obj3 = Helper.GetPropertyValue<object>(entity, "AutomationEvents", BindingFlags.NonPublic | BindingFlags.Instance);
            if (Helper.GetPropertyValue<int>(obj3, "Count", BindingFlags.NonPublic | BindingFlags.Instance) != 0)
            {
                int? parametersCount = null;
                object[] objArray = Helper.GetInstanceMethodHandler<Func<object, object[]>>(obj3, "CopyToArray", BindingFlags.NonPublic | BindingFlags.Instance, obj3.GetType(), parametersCount, null, true)(obj3);
                if (objArray != null)
                {
                    foreach (object obj4 in objArray)
                    {
                        parametersCount = null;
                        Helper.GetInstanceMethodHandler<Action<object, object>>(obj3, "Remove", BindingFlags.NonPublic | BindingFlags.Instance, obj3.GetType(), parametersCount, null, true)(obj3, obj4);
                    }
                }
            }
        }

        private static ReflectionHelper CreateReflectionHelper() => 
            new ReflectionHelper();

        private static object FakeUpdatePeerCallback(object args) => 
            null;

        [SecuritySafeCritical]
        private static bool IsNarratorActive()
        {
            using (Mutex mutex = new Mutex(false, "NarratorRunning"))
            {
                return !mutex.WaitOne(0, false);
            }
        }

        [SecuritySafeCritical]
        private static bool IsScreenReaderActive()
        {
            bool flag;
            return (SystemParametersInfo(70, 0, out flag, 0) & flag);
        }

        private static void RemoveAutomationPeersAtAll()
        {
            IEventMap map = typeof(AutomationPeer).Assembly.GetType("MS.Internal.Automation.EventMap").Wrap<IEventMap>();
            if (map != null)
            {
                object @lock = map.Lock;
                lock (@lock)
                {
                    map.EventsTable = new StirlitzHashTable();
                }
            }
        }

        private static void ResetUpdatePeerCallback()
        {
            typeof(AutomationPeer).GetField("_updatePeer", BindingFlags.NonPublic | BindingFlags.Static).SetValue(null, new DispatcherOperationCallback(ClearAutomationEventsHelper.FakeUpdatePeerCallback));
        }

        [DllImport("user32.dll")]
        private static extern bool SystemParametersInfo(int iAction, int iParam, out bool bActive, int iUpdate);

        private static ReflectionHelper Helper =>
            helper ??= CreateReflectionHelper();

        public static bool IsEnabled { get; set; }

        private static Type ClmType =>
            clmType ??= typeof(UIElement).Assembly.GetType("System.Windows.ContextLayoutManager");
    }
}

