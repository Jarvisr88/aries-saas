namespace DevExpress.Utils
{
    using System;
    using System.Reflection.Emit;
    using System.Runtime.CompilerServices;

    public static class TypeBuilderExtensions
    {
        public static Type AsType(this TypeBuilder builder) => 
            builder;
    }
}

