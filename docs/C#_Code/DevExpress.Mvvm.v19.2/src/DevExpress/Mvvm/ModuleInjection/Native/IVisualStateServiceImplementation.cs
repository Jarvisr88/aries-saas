namespace DevExpress.Mvvm.ModuleInjection.Native
{
    using DevExpress.Mvvm.ModuleInjection;
    using System;

    public interface IVisualStateServiceImplementation : IVisualStateService
    {
        void EnforceSaveState();

        string Id { get; }
    }
}

