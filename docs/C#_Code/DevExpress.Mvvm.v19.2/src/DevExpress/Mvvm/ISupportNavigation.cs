namespace DevExpress.Mvvm
{
    using System;

    public interface ISupportNavigation : ISupportParameter
    {
        void OnNavigatedFrom();
        void OnNavigatedTo();
    }
}

