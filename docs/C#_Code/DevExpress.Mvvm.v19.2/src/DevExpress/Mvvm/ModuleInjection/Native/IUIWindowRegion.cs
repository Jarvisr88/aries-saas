namespace DevExpress.Mvvm.ModuleInjection.Native
{
    using System;
    using System.Windows;

    public interface IUIWindowRegion : IUIRegion
    {
        void SetResult(MessageBoxResult result);

        MessageBoxResult? Result { get; }
    }
}

