namespace DevExpress.Mvvm
{
    using System;
    using System.Runtime.CompilerServices;

    public interface INavigationService
    {
        event EventHandler CanGoBackChanged;

        event EventHandler CanGoForwardChanged;

        event EventHandler CurrentChanged;

        void ClearCache();
        void ClearNavigationHistory();
        void GoBack();
        void GoBack(object param);
        void GoForward();
        void GoForward(object param);
        void Navigate(string viewType, object viewModel, object param, object parentViewModel, bool saveToJournal);

        bool CanNavigate { get; }

        bool CanGoBack { get; }

        bool CanGoForward { get; }

        object Current { get; }
    }
}

