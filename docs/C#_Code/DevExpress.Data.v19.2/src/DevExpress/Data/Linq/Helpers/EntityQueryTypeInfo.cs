namespace DevExpress.Data.Linq.Helpers
{
    using System;

    internal class EntityQueryTypeInfo
    {
        private const string EntityFrameworkVersionMistmatchMessage = "Entity Framework version mismatch. For details, see https://www.devexpress.com/Support/Center/Question/Details/KA18959/";
        private bool isEntityFunctionsChecked;
        private bool isSqlFunctionsChecked;
        private Type entityFunctionType;
        private Type sqlFunctionType;
        public readonly Type ProviderType;
        public readonly bool IsEntityFramework;
        public readonly bool IsEntityFrameworkCore;
        public readonly object EFCoreFunctionsInstance;
        public readonly EntityQueryTypeCaps ProviderCaps;
        public readonly Type ExpectedFunctionsType;
        public readonly Type ExpectedDBFunctionsType;
        public readonly Type ExpectedSqlFunctionsType;

        public EntityQueryTypeInfo();
        public EntityQueryTypeInfo(Type providerType, bool isEntityFramework, bool isEntityFrameworkCore, object efCoreFunctionsInstance, EntityQueryTypeCaps providerCaps, Type expectedFunctionsType, Type expectedDBFunctionsType, Type expectedSqlFynctionsType);
        public void CheckEntityFunctions();
        public void CheckSqlFunctions();
        public bool IsCapabilitySupported(EntityQueryTypeCaps caps);

        public Type EntityFunctionsType { get; }

        public Type SqlFunctionsType { get; }
    }
}

