namespace DevExpress.Mvvm.POCO
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.DataAnnotations;
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.CompilerServices;

    internal static class BuilderType
    {
        [ThreadStatic]
        private static Dictionary<Assembly, ModuleBuilder> builders;

        private static ConstructorBuilder BuildConstructor(TypeBuilder type, ConstructorInfo baseConstructor)
        {
            ParameterInfo[] parameters = baseConstructor.GetParameters();
            Func<ParameterInfo, Type> selector = <>c.<>9__2_0;
            if (<>c.<>9__2_0 == null)
            {
                Func<ParameterInfo, Type> local1 = <>c.<>9__2_0;
                selector = <>c.<>9__2_0 = x => x.ParameterType;
            }
            ConstructorBuilder builder = type.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, parameters.Select<ParameterInfo, Type>(selector).ToArray<Type>());
            for (int i = 0; i < parameters.Length; i++)
            {
                builder.DefineParameter(i + 1, parameters[i].Attributes, parameters[i].Name);
            }
            ILGenerator iLGenerator = builder.GetILGenerator();
            iLGenerator.Emit(OpCodes.Ldarg_0);
            for (int j = 0; j < parameters.Length; j++)
            {
                iLGenerator.Emit(OpCodes.Ldarg_S, (int) (j + 1));
            }
            iLGenerator.Emit(OpCodes.Call, baseConstructor);
            iLGenerator.Emit(OpCodes.Ret);
            return builder;
        }

        public static void BuildConstructors(Type type, TypeBuilder typeBuilder)
        {
            Func<ConstructorInfo, bool> predicate = <>c.<>9__1_0;
            if (<>c.<>9__1_0 == null)
            {
                Func<ConstructorInfo, bool> local1 = <>c.<>9__1_0;
                predicate = <>c.<>9__1_0 = x => BuilderCommon.CanAccessFromDescendant(x);
            }
            ConstructorInfo[] source = type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).Where<ConstructorInfo>(predicate).ToArray<ConstructorInfo>();
            if (!source.Any<ConstructorInfo>())
            {
                throw new ViewModelSourceException($"Type has no accessible constructors: {type.Name}.");
            }
            foreach (ConstructorInfo info in source)
            {
                BuildConstructor(typeBuilder, info);
            }
        }

        private static ModuleBuilder CreateBuilder()
        {
            AssemblyName name = new AssemblyName {
                Name = "DevExpress.Mvvm.v19.2.DynamicTypes." + Guid.NewGuid().ToString()
            };
            return AssemblyBuilder.DefineDynamicAssembly(name, AssemblyBuilderAccess.Run).DefineDynamicModule(name.Name);
        }

        public static TypeBuilder CreateTypeBuilder(Type baseType)
        {
            ModuleBuilder moduleBuilder = GetModuleBuilder(baseType.Assembly);
            Type[] collection = new Type[] { typeof(INotifyPropertyChanged), typeof(IPOCOViewModel), typeof(ISupportServices), typeof(ISupportParentViewModel) };
            List<Type> list = new List<Type>(collection);
            if (BuilderCommon.ShouldImplementIDataErrorInfo(baseType))
            {
                list.Add(typeof(IDataErrorInfo));
            }
            if (ShouldImplementINotifyPropertyChanging(baseType))
            {
                list.Add(typeof(INotifyPropertyChanging));
            }
            return moduleBuilder.DefineType(baseType.Name + "_" + Guid.NewGuid().ToString().Replace('-', '_'), TypeAttributes.Public, baseType, list.ToArray());
        }

        private static ModuleBuilder GetModuleBuilder(Assembly assembly)
        {
            Func<ModuleBuilder> createValueDelegate = <>c.<>9__6_0;
            if (<>c.<>9__6_0 == null)
            {
                Func<ModuleBuilder> local1 = <>c.<>9__6_0;
                createValueDelegate = <>c.<>9__6_0 = () => CreateBuilder();
            }
            return Builders.GetOrAdd<Assembly, ModuleBuilder>(assembly, createValueDelegate);
        }

        private static bool ShouldImplementINotifyPropertyChanging(Type type)
        {
            if (type.GetInterfaces().Contains<Type>(typeof(INotifyPropertyChanging)))
            {
                return false;
            }
            POCOViewModelAttribute pOCOViewModelAttribute = BuilderCommon.GetPOCOViewModelAttribute(type);
            return ((pOCOViewModelAttribute != null) && pOCOViewModelAttribute.ImplementINotifyPropertyChanging);
        }

        private static Dictionary<Assembly, ModuleBuilder> Builders =>
            builders ??= new Dictionary<Assembly, ModuleBuilder>();

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BuilderType.<>c <>9 = new BuilderType.<>c();
            public static Func<ConstructorInfo, bool> <>9__1_0;
            public static Func<ParameterInfo, Type> <>9__2_0;
            public static Func<ModuleBuilder> <>9__6_0;

            internal Type <BuildConstructor>b__2_0(ParameterInfo x) => 
                x.ParameterType;

            internal bool <BuildConstructors>b__1_0(ConstructorInfo x) => 
                BuilderCommon.CanAccessFromDescendant(x);

            internal ModuleBuilder <GetModuleBuilder>b__6_0() => 
                BuilderType.CreateBuilder();
        }
    }
}

