namespace DevExpress.Mvvm.DataAnnotations
{
    using DevExpress.Mvvm.Native;
    using System;

    public class CommandMetadataBuilder<T> : CommandMetadataBuilderBase<T, CommandMetadataBuilder<T>>
    {
        internal CommandMetadataBuilder(MemberMetadataStorage storage, ClassMetadataBuilder<T> parent) : base(storage, parent)
        {
        }
    }
}

