namespace DevExpress.Entity.ProjectModel
{
    using System;
    using System.Runtime.CompilerServices;

    public sealed class MetaDataServices
    {
        public static IDXTypeInfo GetExistingOrCreateNew(Type type) => 
            ((type == null) || !IsInitialized) ? null : SolutionTypesProvider.ActiveProjectTypes.GetExistingOrCreateNew(type);

        public static void Initialize(ISolutionTypesProvider solutionTypesProvider)
        {
            SolutionTypesProvider = solutionTypesProvider;
        }

        public static void Reset()
        {
            SolutionTypesProvider = null;
        }

        public static ISolutionTypesProvider SolutionTypesProvider { get; private set; }

        public static bool IsInitialized =>
            SolutionTypesProvider != null;
    }
}

