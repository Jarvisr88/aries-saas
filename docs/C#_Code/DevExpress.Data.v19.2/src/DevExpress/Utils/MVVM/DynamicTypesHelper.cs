namespace DevExpress.Utils.MVVM
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.InteropServices;
    using System.Text;

    public static class DynamicTypesHelper
    {
        private static readonly string dynamicSuffix = ("Dynamic_" + Guid.NewGuid().ToString());
        private static IDictionary<string, ModuleBuilder> mCache = new Dictionary<string, ModuleBuilder>(StringComparer.Ordinal);

        private static AssemblyName CreateDynamicAssemblyName(string assemblyName) => 
            new AssemblyName(MakeDynamicAssemblyName(assemblyName));

        private static AssemblyBuilder DefineDynamicAssemblyCore(AssemblyName assemblyName, AssemblyBuilderAccess access = 1) => 
            AssemblyBuilder.DefineDynamicAssembly(assemblyName, access);

        private static AssemblyBuilder DefineDynamicAssemblyRunAndSave(AssemblyName assemblyName) => 
            DefineDynamicAssemblyCore(assemblyName, AssemblyBuilderAccess.RunAndSave);

        public static string GetDynamicAssemblyName(string assemblyName) => 
            MakeDynamicAssemblyName(assemblyName) + ".dll";

        public static string GetDynamicTypeName(Type type) => 
            GetDynamicTypeName(type, null);

        public static string GetDynamicTypeName(string typeName, string typeNameModifier)
        {
            if (string.IsNullOrEmpty(typeNameModifier))
            {
                return (typeName + "_" + dynamicSuffix);
            }
            string[] textArray1 = new string[] { typeName, "_", typeNameModifier.Replace('.', '_'), "_", dynamicSuffix };
            return string.Concat(textArray1);
        }

        public static string GetDynamicTypeName(Type type, string typeNameModifier) => 
            !string.IsNullOrEmpty(type.Namespace) ? GetDynamicTypeName(type.Namespace + "." + GetTypeName(type), typeNameModifier) : GetDynamicTypeName(GetTypeName(type), typeNameModifier);

        public static ModuleBuilder GetModuleBuilder(Assembly assembly)
        {
            ModuleBuilder builder;
            AssemblyName assemblyName = assembly.GetName();
            string name = assemblyName.Name;
            if (!mCache.TryGetValue(name, out builder))
            {
                if (assembly.IsDynamic)
                {
                    builder = (from m in mCache.Values
                        where AssemblyName.ReferenceMatchesDefinition(m.Assembly.GetName(), assemblyName)
                        select m).FirstOrDefault<ModuleBuilder>();
                }
                if (builder == null)
                {
                    assemblyName = CreateDynamicAssemblyName(name);
                    builder = DefineDynamicAssemblyCore(assemblyName, AssemblyBuilderAccess.Run).DefineDynamicModule(GetDynamicAssemblyName(name));
                }
                mCache.Add(name, builder);
            }
            return builder;
        }

        public static TypeBuilder GetTypeBuilder(Type serviceType) => 
            GetModuleBuilder(serviceType.Assembly).DefineType(GetDynamicTypeName(serviceType));

        public static TypeBuilder GetTypeBuilder(Type serviceType, Type sourceType) => 
            GetModuleBuilder(serviceType.Assembly).DefineType(GetDynamicTypeName(sourceType), TypeAttributes.AnsiClass, sourceType);

        public static string GetTypeName(Type type)
        {
            string typeNameCore = GetTypeNameCore(type);
            if (!type.IsGenericType)
            {
                return typeNameCore;
            }
            StringBuilder builder = new StringBuilder(typeNameCore);
            int index = typeNameCore.IndexOf('`');
            builder.Remove(index, typeNameCore.Length - index);
            builder.Append("<");
            Type[] genericArguments = type.GetGenericArguments();
            for (int i = 0; i < genericArguments.Length; i++)
            {
                builder.Append(GetTypeName(genericArguments[i]));
                if (i > 0)
                {
                    builder.Append(",");
                }
            }
            builder.Append(">");
            return builder.ToString();
        }

        private static string GetTypeNameCore(Type type)
        {
            if (!type.IsEnum || string.IsNullOrEmpty(type.Namespace))
            {
                return type.Name;
            }
            StringBuilder builder = new StringBuilder(type.Namespace);
            if (type.IsNested)
            {
                for (Type type2 = type.DeclaringType; type2 != null; type2 = !type2.IsNested ? null : type2.DeclaringType)
                {
                    builder.Append(".");
                    builder.Append(type2.Name);
                }
            }
            builder.Append(".");
            builder.Append(type.Name);
            return builder.ToString();
        }

        private static string MakeDynamicAssemblyName(string assemblyName) => 
            assemblyName + "." + dynamicSuffix;
    }
}

