namespace DevExpress.Data.Filtering
{
    using System;
    using System.Runtime.CompilerServices;

    internal class SourceControlNotifier : IDisposable
    {
        private object sourceControl;

        public event SourceControlNotifier.PropertyChangedDelegate OnPropertiesChanged;

        public void Dispose();
        private void OnSourceControlPropertiesChanged(object sender, EventArgs e);
        private void RaiseOnPropertiesChanged();
        private void Subscribe();
        private void Unsubscribe();

        public object SourceControl { get; set; }

        public delegate void PropertyChangedDelegate();
    }
}

