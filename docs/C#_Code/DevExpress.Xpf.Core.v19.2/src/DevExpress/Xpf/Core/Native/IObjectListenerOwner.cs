namespace DevExpress.Xpf.Core.Native
{
    using System;

    public interface IObjectListenerOwner
    {
        void OnErrorsChanged(ObjectListener listener, object obj, string propertyName);
        void OnPropertyChanged(ObjectListener listener, object obj, string propertyName);
        void OnPropertyChanging(ObjectListener listener, object obj, string propertyName);
    }
}

