namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class ValueChangedStorage
    {
        private bool notificationsPaused;
        private readonly Dictionary<ValueChangedStorage.ValueChangedStorageKey, ValueChangedStorage.ValueChangedStorageRecord> handlers;

        public ValueChangedStorage();
        public void ResumeNotifications();
        public void SubscribeValueChanged(object target, RenderPropertyChangedListenerContext context);
        public void SuspendNotifications();
        public void UnsubscribeValueChanged(object target, RenderPropertyChangedListenerContext context);

        [StructLayout(LayoutKind.Sequential)]
        private struct ValueChangedStorageKey
        {
            private readonly WeakReference targetReference;
            private readonly DependencyPropertyDescriptor descriptor;
            private readonly int hCode;
            public object Target { get; }
            public DependencyPropertyDescriptor Descriptor { get; }
            public ValueChangedStorageKey(object target, DependencyPropertyDescriptor descriptor);
            public override bool Equals(object obj);
            public override int GetHashCode();
            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly ValueChangedStorage.ValueChangedStorageKey.<>c <>9;
                public static Func<WeakReference, object> <>9__4_0;

                static <>c();
                internal object <get_Target>b__4_0(WeakReference x);
            }
        }

        private class ValueChangedStorageRecord : IDisposable
        {
            private readonly object target;
            private readonly DependencyPropertyDescriptor descriptor;
            private readonly EventHandler handler;
            private bool isDisposed;
            private bool isListening;
            private PropertyChangeTracker tracker;
            private bool useTracker;
            private readonly List<RenderPropertyChangedListenerContext> contexts;

            public ValueChangedStorageRecord(object target, DependencyPropertyDescriptor descriptor, bool notificationsPaused, bool useTracker);
            public void AddListener(RenderPropertyChangedListenerContext context);
            public void Dispose();
            private void OnValueChanged(object sender, EventArgs args);
            public void Refresh();
            public void RemoveListener(RenderPropertyChangedListenerContext context);
            public void StartListening();
            public void StopListening();

            public bool IsActive { get; }
        }
    }
}

