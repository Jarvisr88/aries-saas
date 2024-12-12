namespace DevExpress.DirectX.Common
{
    using DevExpress.DirectX.NativeInterop;
    using DevExpress.DirectX.NativeInterop.CCW;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;

    public class DynamicAssemblyHelper
    {
        private const string comCallableWrapperInterfaceName = "IComCallableWrapper`1";
        private readonly List<Delegate> delegates = new List<Delegate>();
        private readonly IDictionary<Type, IList<ComCallableWrapperVtable>> comWrappersCache = new Dictionary<Type, IList<ComCallableWrapperVtable>>();
        private static DynamicAssemblyHelper instance = new DynamicAssemblyHelper();
        private readonly System.Reflection.Emit.ModuleBuilder moduleBuilder;
        private readonly AssemblyBuilder builder;
        private readonly AssemblyName assemblyName = new AssemblyName(Assembly.GetExecutingAssembly().GetName().Name + "_" + Guid.NewGuid().ToString());

        private DynamicAssemblyHelper()
        {
            this.builder = AssemblyBuilder.DefineDynamicAssembly(this.assemblyName, AssemblyBuilderAccess.Run);
            this.moduleBuilder = this.builder.DefineDynamicModule(this.assemblyName.Name);
        }

        public Type CreateDelegateType(MethodInfo method, bool isWinApi)
        {
            CustomAttributeBuilder builder4;
            string typeName = this.GetTypeName($"{method.DeclaringType.Name}{method.Name}Delegate");
            TypeBuilder builder = this.moduleBuilder.DefineType(typeName, TypeAttributes.Sealed | TypeAttributes.Public, typeof(MulticastDelegate));
            Type[] parameterTypes = new Type[] { typeof(object), typeof(IntPtr) };
            builder.DefineConstructor(MethodAttributes.RTSpecialName | MethodAttributes.HideBySig | MethodAttributes.Public, CallingConventions.Standard, parameterTypes).SetImplementationFlags(MethodImplAttributes.CodeTypeMask);
            Func<ParameterInfo, Type> selector = <>c.<>9__17_0;
            if (<>c.<>9__17_0 == null)
            {
                Func<ParameterInfo, Type> local1 = <>c.<>9__17_0;
                selector = <>c.<>9__17_0 = t => t.ParameterType;
            }
            Type[] typeArray = method.GetParameters().Select<ParameterInfo, Type>(selector).ToArray<Type>();
            builder.DefineMethod("Invoke", MethodAttributes.HideBySig | MethodAttributes.Virtual | MethodAttributes.Public, method.ReturnType, typeArray).SetImplementationFlags(MethodImplAttributes.CodeTypeMask);
            Type[] types = new Type[] { typeof(CallingConvention) };
            ConstructorInfo constructor = typeof(UnmanagedFunctionPointerAttribute).GetConstructor(types);
            if (isWinApi)
            {
                object[] constructorArgs = new object[] { CallingConvention.StdCall };
                builder4 = new CustomAttributeBuilder(constructor, constructorArgs);
            }
            else
            {
                FieldInfo field = typeof(UnmanagedFunctionPointerAttribute).GetField("CharSet");
                object[] constructorArgs = new object[] { CallingConvention.Cdecl };
                FieldInfo[] namedFields = new FieldInfo[] { field };
                object[] fieldValues = new object[] { CharSet.Unicode };
                builder4 = new CustomAttributeBuilder(constructor, constructorArgs, namedFields, fieldValues);
            }
            builder.SetCustomAttribute(builder4);
            return builder.CreateType();
        }

        internal static IInvokeHelper CreateInvokeHelper() => 
            new InvokeHelperBuilder(instance).CreateInvokeHelperImp();

        private IList<MethodInfo> CreateMethods(IEnumerable<MethodDescription> methods, string name)
        {
            TypeBuilder typeBuilder = this.moduleBuilder.DefineType(this.GetTypeName(name + "Helper"), TypeAttributes.BeforeFieldInit | TypeAttributes.Sealed | TypeAttributes.Abstract | TypeAttributes.Public);
            IList<string> list = new List<string>();
            foreach (MethodDescription description in methods)
            {
                list.Add(ComCallableWrapperMethodBuilder.CreateMethod(typeBuilder, description.InterfaceMethod, description.TargetMethod, description.InterfaceType));
            }
            MethodInfo[] infoArray = new MethodInfo[list.Count];
            Type type = typeBuilder.CreateType();
            for (int i = 0; i < list.Count; i++)
            {
                infoArray[i] = type.GetMethod(list[i]);
            }
            return infoArray;
        }

        internal static IList<ComCallableWrapperVtable> GetTypeComInterfaces(Type type) => 
            instance.GetTypeComInterfacesImp(type);

        [SecuritySafeCritical]
        private IList<ComCallableWrapperVtable> GetTypeComInterfacesImp(Type type)
        {
            IList<ComCallableWrapperVtable> list2;
            DynamicAssemblyHelper instance = DynamicAssemblyHelper.instance;
            lock (instance)
            {
                IList<ComCallableWrapperVtable> list;
                Type[] typeArray;
                int num;
                List<Guid> list3;
                List<MethodDescription> list4;
                Type type2;
                if (!this.comWrappersCache.TryGetValue(type, out list))
                {
                    Func<Type, bool> predicate = <>c.<>9__6_0;
                    if (<>c.<>9__6_0 == null)
                    {
                        Func<Type, bool> local1 = <>c.<>9__6_0;
                        predicate = <>c.<>9__6_0 = i => i.IsGenericType && (i.Name == "IComCallableWrapper`1");
                    }
                    typeArray = type.GetInterfaces().Where<Type>(predicate).ToArray<Type>();
                    list = new List<ComCallableWrapperVtable>(typeArray.Length);
                    num = 0;
                }
                else
                {
                    return list;
                }
                goto TR_0023;
            TR_0020:
                while (true)
                {
                    list3.Add(type2.GUID);
                    InterfaceMapping interfaceMap = type.GetInterfaceMap(type2);
                    List<InterfaceMethodDescription> source = new List<InterfaceMethodDescription>(interfaceMap.InterfaceMethods.Length);
                    int index = interfaceMap.InterfaceMethods.Length - 1;
                    while (true)
                    {
                        if (index >= 0)
                        {
                            MethodInfo element = interfaceMap.InterfaceMethods[index];
                            int offset = element.GetCustomAttribute<MethodOffsetAttribute>().Offset;
                            source.Add(new InterfaceMethodDescription(new MethodDescription(interfaceMap.TargetMethods[index], type2, element), offset));
                            index--;
                            continue;
                        }
                        Func<InterfaceMethodDescription, int> keySelector = <>c.<>9__6_1;
                        if (<>c.<>9__6_1 == null)
                        {
                            Func<InterfaceMethodDescription, int> local2 = <>c.<>9__6_1;
                            keySelector = <>c.<>9__6_1 = v => v.Offset;
                        }
                        foreach (InterfaceMethodDescription description in source.OrderByDescending<InterfaceMethodDescription, int>(keySelector))
                        {
                            list4.Add(description.MethodDescription);
                        }
                        type2 = type2.GetInterfaces().FirstOrDefault<Type>();
                        if (type2 != null)
                        {
                            continue;
                        }
                        else
                        {
                            List<IntPtr> methods = new List<IntPtr>();
                            list4.Reverse();
                            foreach (MethodInfo info2 in this.CreateMethods(list4, type.Name))
                            {
                                Delegate item = Delegate.CreateDelegate(this.CreateDelegateType(info2, true), info2);
                                this.delegates.Add(item);
                                methods.Add(Marshal.GetFunctionPointerForDelegate(item));
                            }
                            list.Add(new ComCallableWrapperVtable(list3, methods));
                            num++;
                        }
                        break;
                    }
                    break;
                }
            TR_0023:
                while (true)
                {
                    if (num < typeArray.Length)
                    {
                        list3 = new List<Guid>();
                        list4 = new List<MethodDescription>();
                        type2 = typeArray[num].GetGenericArguments()[0];
                    }
                    else
                    {
                        this.comWrappersCache.Add(type, list);
                        return list;
                    }
                    break;
                }
                goto TR_0020;
            }
            return list2;
        }

        private string GetTypeName(string nameBase)
        {
            int num = 0;
            string className = nameBase;
            while (this.moduleBuilder.GetType(className) != null)
            {
                className = nameBase + num++;
            }
            return className;
        }

        public static DynamicAssemblyHelper Instance =>
            instance;

        public System.Reflection.Emit.ModuleBuilder ModuleBuilder =>
            this.moduleBuilder;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DynamicAssemblyHelper.<>c <>9 = new DynamicAssemblyHelper.<>c();
            public static Func<Type, bool> <>9__6_0;
            public static Func<InterfaceMethodDescription, int> <>9__6_1;
            public static Func<ParameterInfo, Type> <>9__17_0;

            internal Type <CreateDelegateType>b__17_0(ParameterInfo t) => 
                t.ParameterType;

            internal bool <GetTypeComInterfacesImp>b__6_0(Type i) => 
                i.IsGenericType && (i.Name == "IComCallableWrapper`1");

            internal int <GetTypeComInterfacesImp>b__6_1(InterfaceMethodDescription v) => 
                v.Offset;
        }
    }
}

