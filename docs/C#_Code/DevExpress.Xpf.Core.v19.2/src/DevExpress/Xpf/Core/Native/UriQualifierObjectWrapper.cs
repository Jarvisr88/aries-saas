namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.ComponentModel;

    internal class UriQualifierObjectWrapper : INotifyPropertyChanged
    {
        private QualifierListener instance;

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged;

        public UriQualifierObjectWrapper(QualifierListener instance);

        public QualifierListener Instance { get; }
    }
}

