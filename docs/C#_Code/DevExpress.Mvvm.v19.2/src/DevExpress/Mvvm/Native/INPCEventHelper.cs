namespace DevExpress.Mvvm.Native
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class INPCEventHelper
    {
        private event PropertyChangedEventHandler PropertyChanged;

        private event PropertyChangingEventHandler PropertyChanging;

        public void AddPropertyChangedHandler(PropertyChangedEventHandler handler)
        {
            this.PropertyChanged += handler;
        }

        public void AddPropertyChangingHandler(PropertyChangingEventHandler handler)
        {
            this.PropertyChanging += handler;
        }

        public void OnPropertyChanged(INotifyPropertyChanged obj, string propertyName)
        {
            this.PropertyChanged.Do<PropertyChangedEventHandler>(x => x(obj, new PropertyChangedEventArgs(propertyName)));
        }

        public void OnPropertyChanging(INotifyPropertyChanging obj, string propertyName)
        {
            this.PropertyChanging.Do<PropertyChangingEventHandler>(x => x(obj, new PropertyChangingEventArgs(propertyName)));
        }

        public void RemovePropertyChangedHandler(PropertyChangedEventHandler handler)
        {
            this.PropertyChanged -= handler;
        }

        public void RemovePropertyChangingHandler(PropertyChangingEventHandler handler)
        {
            this.PropertyChanging -= handler;
        }
    }
}

