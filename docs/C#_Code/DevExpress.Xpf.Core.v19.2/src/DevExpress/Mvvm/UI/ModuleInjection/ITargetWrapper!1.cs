namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using System;

    public interface ITargetWrapper<T> where T: DependencyObject
    {
        T Target { get; set; }
    }
}

