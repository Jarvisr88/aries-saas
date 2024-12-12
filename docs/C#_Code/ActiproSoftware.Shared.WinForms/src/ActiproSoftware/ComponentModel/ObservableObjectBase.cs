namespace ActiproSoftware.ComponentModel
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public abstract class ObservableObjectBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected ObservableObjectBase();
        protected void NotifyPropertyChanged(string propertyName);
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e);
    }
}

