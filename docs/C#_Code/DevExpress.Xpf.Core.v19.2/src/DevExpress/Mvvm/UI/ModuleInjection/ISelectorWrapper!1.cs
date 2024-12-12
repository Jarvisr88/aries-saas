namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using System;
    using System.Runtime.CompilerServices;

    public interface ISelectorWrapper<T> : IItemsControlWrapper<T>, ITargetWrapper<T> where T: DependencyObject
    {
        event EventHandler SelectionChanged;

        object SelectedItem { get; set; }
    }
}

