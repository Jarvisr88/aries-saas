namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.ComponentModel;
    using System.Windows;

    public class PropertyChangedWeakEventManager : WeakEventManager
    {
        private PropertyChangedWeakEventManager();
        public static void AddListener(INotifyPropertyChanged source, IWeakEventListener listener);
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e);
        public static void RemoveListener(INotifyPropertyChanged source, IWeakEventListener listener);
        protected override void StartListening(object source);
        protected override void StopListening(object source);

        private static PropertyChangedWeakEventManager CurrentManager { get; }
    }
}

