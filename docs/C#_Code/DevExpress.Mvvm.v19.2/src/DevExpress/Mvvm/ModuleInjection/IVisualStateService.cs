namespace DevExpress.Mvvm.ModuleInjection
{
    using System;

    public interface IVisualStateService
    {
        string GetCurrentState();
        string GetSavedState();
        void RestoreState(string state);
        void SaveState(string state);

        string DefaultState { get; }
    }
}

