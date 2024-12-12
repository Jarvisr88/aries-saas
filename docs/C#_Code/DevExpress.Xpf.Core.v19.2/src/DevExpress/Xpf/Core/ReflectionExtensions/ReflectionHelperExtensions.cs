namespace DevExpress.Xpf.Core.ReflectionExtensions
{
    using DevExpress.Xpf.Core.ReflectionExtensions.Internal;
    using System;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.CompilerServices;

    public static class ReflectionHelperExtensions
    {
        private const string asmPrefix = "ReflectionHelper";
        private const string asmBuilderPostfix = "Types";
        private const string moduleBuilderPostfix = "Module";
        private const string publicWrappersPostfix = "PublicWrappers";
        public const string DynamicAssemblyName = "ReflectionHelperTypes, PublicKey=0024000004800000940000000602000000240000525341310004000001000100758f95f5e23e5e7f8191599ba1e7262093b5c8ca5d329e360a8d61f3b94f4ac23315703141ecf151723fbfcf6a1e7f09f2c3f3b824068293216482b4324596729e7d2973e73660aa11d59c2bd36d66b8799faa6802d29382d38cfbd9634c10424d1bfd43854769fe875bdd194a5527b45b61a2bbebb84d70180ca486748901f6";
        private static readonly Lazy<ModuleBuilderInfo> commonModuleBuilder = new Lazy<ModuleBuilderInfo>(delegate {
            Action<AssemblyName> assignProperties = <>c.<>9__6_1;
            if (<>c.<>9__6_1 == null)
            {
                Action<AssemblyName> local1 = <>c.<>9__6_1;
                assignProperties = <>c.<>9__6_1 = delegate (AssemblyName x) {
                };
            }
            return CreateModuleBuilderInfo("ReflectionHelperTypesPublicWrappers", "ReflectionHelperModulePublicWrappers", assignProperties);
        }, true);
        private static readonly Lazy<ModuleBuilderInfo> internalModuleBuilder;

        static ReflectionHelperExtensions()
        {
            internalModuleBuilder = new Lazy<ModuleBuilderInfo>(delegate {
                Action<AssemblyName> assignProperties = <>c.<>9__6_3;
                if (<>c.<>9__6_3 == null)
                {
                    Action<AssemblyName> local1 = <>c.<>9__6_3;
                    assignProperties = <>c.<>9__6_3 = x => x.KeyPair = new StrongNameKeyPair(ReflectionHelperAssemblyKey.strongKeyPair);
                }
                return CreateModuleBuilderInfo("ReflectionHelperTypes", "ReflectionHelperModule", assignProperties);
            }, true);
        }

        private static ModuleBuilderInfo CreateModuleBuilderInfo(string aName, string mName, Action<AssemblyName> assignProperties)
        {
            AssemblyName name = new AssemblyName(aName);
            assignProperties(name);
            AssemblyBuilder aBuilder = AssemblyBuilder.DefineDynamicAssembly(name, AssemblyBuilderAccess.Run);
            return new ModuleBuilderInfo(aBuilder.DefineDynamicModule(mName), aBuilder);
        }

        public static ReflectionHelperStaticInterfaceWrapperGenerator<TWrapper> DefineWrapper<TType, TWrapper>() => 
            typeof(TType).DefineWrapper<TWrapper>();

        public static ReflectionHelperInstanceInterfaceWrapperGenerator<TWrapper> DefineWrapper<TWrapper>(this object element) => 
            (element != null) ? new ReflectionHelperInstanceInterfaceWrapperGenerator<TWrapper>(GetModuleBuilder(typeof(TWrapper)), element) : null;

        public static ReflectionHelperStaticInterfaceWrapperGenerator<TWrapper> DefineWrapper<TWrapper>(this Type element) => 
            new ReflectionHelperStaticInterfaceWrapperGenerator<TWrapper>(GetModuleBuilder(typeof(TWrapper)), element);

        internal static ModuleBuilder GetModuleBuilder(Type type) => 
            !type.IsPublic ? internalModuleBuilder.Value.ModuleBuilder : commonModuleBuilder.Value.ModuleBuilder;

        public static TWrapper Wrap<TType, TWrapper>() => 
            typeof(TType).Wrap<TWrapper>();

        public static TWrapper Wrap<TWrapper>(this object element)
        {
            if (element != null)
            {
                return element.DefineWrapper<TWrapper>().Create();
            }
            return default(TWrapper);
        }

        public static TWrapper Wrap<TWrapper>(this Type targetType) => 
            targetType.DefineWrapper<TWrapper>().Create();

        internal static object Wrap(object element, Type wrapperType) => 
            (element != null) ? new BaseReflectionHelperInterfaceWrapperGenerator(GetModuleBuilder(wrapperType), element, false, wrapperType).CreateInternal() : null;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ReflectionHelperExtensions.<>c <>9 = new ReflectionHelperExtensions.<>c();
            public static Action<AssemblyName> <>9__6_1;
            public static Action<AssemblyName> <>9__6_3;

            internal ReflectionHelperExtensions.ModuleBuilderInfo <.cctor>b__6_0()
            {
                Action<AssemblyName> assignProperties = <>9__6_1;
                if (<>9__6_1 == null)
                {
                    Action<AssemblyName> local1 = <>9__6_1;
                    assignProperties = <>9__6_1 = delegate (AssemblyName x) {
                    };
                }
                return ReflectionHelperExtensions.CreateModuleBuilderInfo("ReflectionHelperTypesPublicWrappers", "ReflectionHelperModulePublicWrappers", assignProperties);
            }

            internal void <.cctor>b__6_1(AssemblyName x)
            {
            }

            internal ReflectionHelperExtensions.ModuleBuilderInfo <.cctor>b__6_2()
            {
                Action<AssemblyName> assignProperties = <>9__6_3;
                if (<>9__6_3 == null)
                {
                    Action<AssemblyName> local1 = <>9__6_3;
                    assignProperties = <>9__6_3 = delegate (AssemblyName x) {
                        x.KeyPair = new StrongNameKeyPair(ReflectionHelperAssemblyKey.strongKeyPair);
                    };
                }
                return ReflectionHelperExtensions.CreateModuleBuilderInfo("ReflectionHelperTypes", "ReflectionHelperModule", assignProperties);
            }

            internal void <.cctor>b__6_3(AssemblyName x)
            {
                x.KeyPair = new StrongNameKeyPair(ReflectionHelperAssemblyKey.strongKeyPair);
            }
        }

        private class ModuleBuilderInfo
        {
            private System.Reflection.Emit.ModuleBuilder mBuilder;
            private System.Reflection.Emit.AssemblyBuilder aBuilder;

            public ModuleBuilderInfo(System.Reflection.Emit.ModuleBuilder mBuilder, System.Reflection.Emit.AssemblyBuilder aBuilder)
            {
                this.mBuilder = mBuilder;
                this.aBuilder = aBuilder;
            }

            public System.Reflection.Emit.ModuleBuilder ModuleBuilder =>
                this.mBuilder;

            public System.Reflection.Emit.AssemblyBuilder AssemblyBuilder =>
                this.aBuilder;
        }
    }
}

