namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using System;

    public interface IBaseStrategy
    {
        object GetView(object viewModel);
        void Initialize(IStrategyOwner owner);
        void Uninitialize();

        IStrategyOwner Owner { get; }

        bool IsInitialized { get; }
    }
}

