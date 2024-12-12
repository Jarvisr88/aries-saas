namespace DevExpress.Mvvm.DataAnnotations
{
    using DevExpress.Mvvm.Native;
    using System;

    public class EnumMemberMetadataBuilder<T> : EnumMemberMetadataBuilderGeneric<T, EnumMemberMetadataBuilder<T>> where T: struct
    {
        internal EnumMemberMetadataBuilder(MemberMetadataStorage storage, EnumMetadataBuilder<T> parent) : base(storage, parent)
        {
        }
    }
}

