namespace DevExpress.Mvvm.DataAnnotations
{
    using System;

    public interface IFilteringMetadataProvider<T>
    {
        void BuildMetadata(FilteringMetadataBuilder<T> builder);
    }
}

