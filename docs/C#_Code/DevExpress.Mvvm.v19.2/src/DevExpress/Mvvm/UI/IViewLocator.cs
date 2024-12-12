namespace DevExpress.Mvvm.UI
{
    using System;

    public interface IViewLocator
    {
        string GetViewTypeName(Type type);
        object ResolveView(string name);
        Type ResolveViewType(string name);
    }
}

