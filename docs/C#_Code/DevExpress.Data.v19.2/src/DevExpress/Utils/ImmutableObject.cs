namespace DevExpress.Utils
{
    using System;
    using System.ComponentModel;

    public abstract class ImmutableObject : INotifyPropertyChanged
    {
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add
            {
            }
            remove
            {
            }
        }

        protected ImmutableObject()
        {
        }
    }
}

