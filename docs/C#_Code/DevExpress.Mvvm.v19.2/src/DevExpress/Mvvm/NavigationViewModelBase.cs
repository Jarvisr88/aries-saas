namespace DevExpress.Mvvm
{
    using System;

    public abstract class NavigationViewModelBase : ViewModelBase, ISupportNavigation, ISupportParameter
    {
        protected NavigationViewModelBase()
        {
        }

        void ISupportNavigation.OnNavigatedFrom()
        {
            this.OnNavigatedFrom();
        }

        void ISupportNavigation.OnNavigatedTo()
        {
            this.OnNavigatedTo();
        }

        protected override void OnInitializeInDesignMode()
        {
            base.OnInitializeInDesignMode();
            this.OnNavigatedTo();
        }

        protected virtual void OnNavigatedFrom()
        {
        }

        protected virtual void OnNavigatedTo()
        {
        }
    }
}

