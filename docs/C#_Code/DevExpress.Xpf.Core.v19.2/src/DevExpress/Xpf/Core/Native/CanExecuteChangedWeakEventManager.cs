namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    public class CanExecuteChangedWeakEventManager : WeakEventManager
    {
        private CanExecuteChangedWeakEventManager();
        public static void AddListener(ICommand source, IWeakEventListener listener);
        private void OnCanExecuteChanged(object sender, EventArgs e);
        public static void RemoveListener(ICommand source, IWeakEventListener listener);
        protected override void StartListening(object source);
        protected override void StopListening(object source);

        private static CanExecuteChangedWeakEventManager CurrentManager { get; }
    }
}

