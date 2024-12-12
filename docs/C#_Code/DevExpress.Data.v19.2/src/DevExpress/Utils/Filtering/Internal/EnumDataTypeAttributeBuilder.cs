namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;

    internal static class EnumDataTypeAttributeBuilder
    {
        private static readonly ConstructorInfo attributeCtor;

        static EnumDataTypeAttributeBuilder();
        internal static CustomAttributeBuilder Build(Type enumType);
    }
}

