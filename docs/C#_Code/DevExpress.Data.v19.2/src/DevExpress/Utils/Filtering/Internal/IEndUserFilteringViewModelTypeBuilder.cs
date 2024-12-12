namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    public interface IEndUserFilteringViewModelTypeBuilder
    {
        Type Create(Type baseType, IEndUserFilteringViewModelProperties properties, IEndUserFilteringViewModelPropertyValues values);
    }
}

