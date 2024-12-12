namespace DevExpress.Mvvm
{
    using System;

    public interface IViewModelLocator
    {
        string GetViewModelTypeName(Type type);
        object ResolveViewModel(string name);
        Type ResolveViewModelType(string name);
    }
}

