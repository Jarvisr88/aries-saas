namespace DevExpress.Entity.Model
{
    using System;
    using System.Collections.Generic;

    public sealed class Constants
    {
        private static Dictionary<string, string> typeNameToKeyword;
        public const string ServicesClientAssemblyName = "Microsoft.Data.Services.Client";
        public const string ServiceContextTypeName = "System.Data.Services.Client.DataServiceContext";
        public const string DbContextTypeName = "System.Data.Entity.DbContext";
        public const string DbModelBuilderTypeName = "System.Data.Entity.DbModelBuilder";
        public const string MetadataHelperTypeName = "System.Data.Common.Utils.MetadataHelper";
        public const string DbSetTypeName = "System.Data.Entity.DbSet`1";
        public const string EFCoreDbSetTypeName = "Microsoft.EntityFrameworkCore.DbSet`1";
        public const string DbConnectionTypeName = "System.Data.Common.DbConnection";
        public const string IObjectContextAdapterTypeName = "System.Data.Entity.Infrastructure.IObjectContextAdapter";
        public const string SqlCeConnectionTypeName = "System.Data.SqlServerCe.SqlCeConnection";
        public const string EntityStoreSchemaGeneratorTypeAttributeName = "http://schemas.microsoft.com/ado/2007/12/edm/EntityStoreSchemaGenerator:Type";
        public const string EntityStoreSchemaGeneratorTypeAttributeValueIsViews = "Views";
        public const string DatabaseFileName = "db.sdf";
        public const string EntityFrameworkAssemblyName = "EntityFramework";
        public const string SystemDataEntityAssemblyName = "System.Data.Entity";
        public const string Sql35ProviderName = "System.Data.SqlServerCe.3.5";
        public const string Sql40ProviderName = "System.Data.SqlServerCe.4.0";
        public const string CE35RegistryKey = @"SOFTWARE\Microsoft\Microsoft SQL Server Compact Edition\v3.5";
        public const string CE40RegistryKey = @"SOFTWARE\Microsoft\Microsoft SQL Server Compact Edition\v4.0";
        public const string SystemDataSqlCe350AssemblyFullName = "System.Data.SqlServerCe, Version=3.5.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91";
        public const string SystemDataSqlCe351AssemblyFullName = "System.Data.SqlServerCe, Version=3.5.1.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91";
        public const string SystemDataSqlCe40AssemblyFullName = "System.Data.SqlServerCe, Version=4.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91";
        public const string EntityFrameworkMySqlClientAssemblyName = "MySql.Data.Entity.EF6";
        public const string EntityFrameworkOracleManagedClientAssemblyName = "Oracle.ManagedDataAccess.EntityFramework";
        public const string EntityFrameworkSqliteAssemblyName = "System.Data.SQLite.EF6";
        public const string SqliteAssemblyName = "System.Data.SQLite";
        public const string EntityFrameworkSqlClientAssemblyName = "EntityFramework.SqlServer";
        public const string EntityFrameworkSqlCeAssemblyName = "EntityFramework.SqlServerCompact";
        public const string DbContextEFCoreTypeName = "Microsoft.EntityFrameworkCore.DbContext";
        public const string EntityFrameworkCoreMsSqlAssemblyName = "Microsoft.EntityFrameworkCore.SqlServer";
        public const string EntityFrameworkCoreMsSqlCeAssemblyName = "EntityFrameworkCore.SqlServerCompact";
        public const string EntityFrameworkCoreSqliteAssemblyName = "Microsoft.EntityFrameworkCore.Sqlite";
        public const string EntityFrameworkCoreNpgsqlAssemblyName = "Npgsql.EntityFrameworkCore.PostgreSQL";

        private Constants()
        {
        }

        private static void InitTypeNames()
        {
            if (typeNameToKeyword == null)
            {
                typeNameToKeyword = new Dictionary<string, string>();
                typeNameToKeyword["Boolean"] = "bool";
                typeNameToKeyword["Byte"] = "byte";
                typeNameToKeyword["SByte"] = "sbyte";
                typeNameToKeyword["Char"] = "char";
                typeNameToKeyword["Decimal"] = "decimal";
                typeNameToKeyword["Double"] = "double";
                typeNameToKeyword["Single"] = "float";
                typeNameToKeyword["Int32"] = "int";
                typeNameToKeyword["UInt32"] = "uint";
                typeNameToKeyword["Int64"] = "long";
                typeNameToKeyword["UInt64"] = "ulong";
                typeNameToKeyword["Object"] = "object";
                typeNameToKeyword["Int16"] = "short";
                typeNameToKeyword["UInt16"] = "ushort";
                typeNameToKeyword["String"] = "string";
                typeNameToKeyword["Void"] = "void";
            }
        }

        public static string TypeNameToKeyword(string typeName)
        {
            if (string.IsNullOrEmpty(typeName))
            {
                return typeName;
            }
            InitTypeNames();
            return (typeNameToKeyword.ContainsKey(typeName) ? typeNameToKeyword[typeName] : typeName);
        }
    }
}

