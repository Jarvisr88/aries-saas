namespace DevExpress.Mvvm.DataAnnotations
{
    using DevExpress.Mvvm.Native;
    using System;

    public class CommandMethodMetadataBuilder<T> : CommandMethodMetadataBuilderBase<T, CommandMethodMetadataBuilder<T>>
    {
        internal CommandMethodMetadataBuilder(MemberMetadataStorage storage, ClassMetadataBuilder<T> parent, string methodName) : base(storage, parent, methodName)
        {
        }
    }
}

