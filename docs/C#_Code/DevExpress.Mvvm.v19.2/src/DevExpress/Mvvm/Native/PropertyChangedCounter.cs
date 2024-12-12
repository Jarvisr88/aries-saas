namespace DevExpress.Mvvm.Native
{
    using System;
    using System.ComponentModel;

    public class PropertyChangedCounter : EventFireCounter<INotifyPropertyChanged, PropertyChangedEventArgs>
    {
        public PropertyChangedCounter(INotifyPropertyChanged obj, string propertyName) : base(delegate (EventHandler h) {
            obj.PropertyChanged += delegate (object o, PropertyChangedEventArgs e) {
                if (e.PropertyName == propertyName)
                {
                    h(o, e);
                }
            };
        }, null)
        {
        }
    }
}

