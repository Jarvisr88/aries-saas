namespace DevExpress.Data.Entity
{
    using System;

    public static class TypesFilterHelper
    {
        private static readonly string[] ExcludedAssemblyPrefixes;
        private static readonly string[] MustIncludeAssemblyPrefixes;

        static TypesFilterHelper();
        public static bool ShouldIncludeTypesFromAssembly(string assemblyName);
    }
}

