namespace DevExpress.Mvvm.DataAnnotations
{
    using System;

    public interface IMetadataProvider<T>
    {
        void BuildMetadata(MetadataBuilder<T> builder);
    }
}

