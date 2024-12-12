namespace DevExpress.Mvvm.DataAnnotations
{
    using DevExpress.Mvvm.Native;

    public class EnumMetadataBuilder<T> : MetadataBuilderBase<T, EnumMetadataBuilder<T>> where T: struct
    {
        public EnumMemberMetadataBuilder<T> Member(T member) => 
            base.GetBuilder<EnumMemberMetadataBuilder<T>>(member.ToString(), x => new EnumMemberMetadataBuilder<T>(x, (EnumMetadataBuilder<T>) this));
    }
}

