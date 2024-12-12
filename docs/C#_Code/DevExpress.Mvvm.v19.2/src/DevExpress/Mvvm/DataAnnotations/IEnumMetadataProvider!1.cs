namespace DevExpress.Mvvm.DataAnnotations
{
    using System;

    public interface IEnumMetadataProvider<T> where T: struct
    {
        void BuildMetadata(EnumMetadataBuilder<T> builder);
    }
}

