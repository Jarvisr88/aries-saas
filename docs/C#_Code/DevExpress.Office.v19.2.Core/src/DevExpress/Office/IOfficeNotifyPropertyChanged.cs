namespace DevExpress.Office
{
    using System;
    using System.Runtime.CompilerServices;

    public interface IOfficeNotifyPropertyChanged
    {
        event EventHandler<OfficePropertyChangedEventArgs> PropertyChanged;
    }
}

