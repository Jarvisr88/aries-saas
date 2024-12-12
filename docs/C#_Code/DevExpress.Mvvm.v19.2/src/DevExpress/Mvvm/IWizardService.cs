namespace DevExpress.Mvvm
{
    using System;

    public interface IWizardService
    {
        void GoBack(object param);
        void GoForward(object param);
        void Navigate(string viewType, object viewModel, object param, object parentViewModel);

        object Current { get; }
    }
}

