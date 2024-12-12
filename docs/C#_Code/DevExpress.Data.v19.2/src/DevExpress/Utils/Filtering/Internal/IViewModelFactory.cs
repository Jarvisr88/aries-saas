namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    public interface IViewModelFactory
    {
        object Create(Type viewModelType, IViewModelBuilder builder);
    }
}

