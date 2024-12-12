namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Reflection.Emit;

    public interface ICustomAttributeBuilderProvider
    {
        CustomAttributeBuilder CreateAttributeBuilder(Attribute attribute);

        Type AttributeType { get; }
    }
}

