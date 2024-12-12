namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.ComponentModel;
    using System.Windows;

    public class ListChangedEventManager : WeakEventManager
    {
        private ListChangedEventManager();
        public static void AddListener(IBindingList source, IWeakEventListener listener);
        private void OnCollectionChanged(object sender, ListChangedEventArgs args);
        public static void RemoveListener(IBindingList source, IWeakEventListener listener);
        protected override void StartListening(object source);
        protected override void StopListening(object source);

        private static ListChangedEventManager CurrentManager { get; }
    }
}

