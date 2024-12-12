namespace DevExpress.Mvvm.ModuleInjection
{
    using System;

    public interface IModule
    {
        string Key { get; }

        Func<object> ViewModelFactory { get; }

        string ViewModelName { get; }

        string ViewName { get; }

        Type ViewType { get; }
    }
}

