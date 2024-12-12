namespace DevExpress.Mvvm
{
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    public interface IViewInjectionManager
    {
        IViewInjectionService GetService(string regionName);
        [EditorBrowsable(EditorBrowsableState.Never)]
        void Inject(string regionName, object key, Func<object> viewModelFactory, string viewName, Type viewType);
        void Navigate(string regionName, object key);
        [EditorBrowsable(EditorBrowsableState.Never)]
        void RaiseNavigatedAwayEvent(object viewModel);
        [EditorBrowsable(EditorBrowsableState.Never)]
        void RaiseNavigatedEvent(object viewModel);
        [EditorBrowsable(EditorBrowsableState.Never)]
        void RaiseViewModelClosingEvent(ViewModelClosingEventArgs e);
        void RegisterNavigatedAwayEventHandler(object viewModel, Action eventHandler);
        void RegisterNavigatedEventHandler(object viewModel, Action eventHandler);
        [EditorBrowsable(EditorBrowsableState.Never)]
        void RegisterService(IViewInjectionService service);
        void RegisterViewModelClosingEventHandler(object viewModel, Action<ViewModelClosingEventArgs> eventHandler);
        void Remove(string regionName, object key);
        void UnregisterNavigatedAwayEventHandler(object viewModel, Action eventHandler = null);
        void UnregisterNavigatedEventHandler(object viewModel, Action eventHandler = null);
        [EditorBrowsable(EditorBrowsableState.Never)]
        void UnregisterService(IViewInjectionService service);
        void UnregisterViewModelClosingEventHandler(object viewModel, Action<ViewModelClosingEventArgs> eventHandler = null);
    }
}

