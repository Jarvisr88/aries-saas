namespace DevExpress.Mvvm.POCO
{
    using System;

    public interface IPOCOViewModel
    {
        void RaisePropertyChanged(string propertyName);
        void RaisePropertyChanging(string propertyName);
    }
}

