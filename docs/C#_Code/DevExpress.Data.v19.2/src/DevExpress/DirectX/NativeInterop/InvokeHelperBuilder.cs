namespace DevExpress.DirectX.NativeInterop
{
    using DevExpress.DirectX.Common;
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal class InvokeHelperBuilder
    {
        private readonly ModuleBuilder moduleBuilder;

        public InvokeHelperBuilder(DynamicAssemblyHelper helper)
        {
            this.moduleBuilder = helper.ModuleBuilder;
        }

        public IInvokeHelper CreateInvokeHelperImp()
        {
            TypeBuilder builder = this.moduleBuilder.DefineType("InteropHelperWrapper");
            builder.AddInterfaceImplementation(typeof(IInvokeHelper));
            Type returnType = typeof(IntPtr);
            Type[] parameterTypes = new Type[] { returnType, typeof(int) };
            MethodBuilder methodInfo = builder.DefineMethod("GetMethod", MethodAttributes.Static | MethodAttributes.Private, returnType, parameterTypes);
            ILGenerator iLGenerator = methodInfo.GetILGenerator();
            iLGenerator.Emit(OpCodes.Ldarg_0);
            Type type2 = typeof(Marshal);
            Type[] types = new Type[] { returnType };
            iLGenerator.EmitCall(OpCodes.Call, type2.GetMethod("ReadIntPtr", types), null);
            iLGenerator.Emit(OpCodes.Ldarg_1);
            iLGenerator.Emit(OpCodes.Ldc_I4, Marshal.SizeOf<IntPtr>());
            iLGenerator.Emit(OpCodes.Mul);
            Type[] typeArray3 = new Type[] { returnType, typeof(int) };
            iLGenerator.EmitCall(OpCodes.Call, type2.GetMethod("ReadIntPtr", typeArray3), null);
            iLGenerator.Emit(OpCodes.Ret);
            MethodInfo[] methods = typeof(IInvokeHelper).GetMethods();
            int index = 0;
            while (index < methods.Length)
            {
                MethodInfo methodInfoDeclaration = methods[index];
                Func<ParameterInfo, Type> selector = <>c.<>9__2_0;
                if (<>c.<>9__2_0 == null)
                {
                    Func<ParameterInfo, Type> local1 = <>c.<>9__2_0;
                    selector = <>c.<>9__2_0 = p => p.ParameterType;
                }
                Type[] typeArray = methodInfoDeclaration.GetParameters().Select<ParameterInfo, Type>(selector).ToArray<Type>();
                Type type3 = null;
                if (methodInfoDeclaration.ReturnType.Name != "Void")
                {
                    type3 = methodInfoDeclaration.ReturnType;
                }
                MethodBuilder methodInfoBody = builder.DefineMethod(methodInfoDeclaration.Name, MethodAttributes.Virtual | MethodAttributes.Public, methodInfoDeclaration.ReturnType, typeArray);
                ILGenerator generator = methodInfoBody.GetILGenerator();
                int num2 = 0;
                while (true)
                {
                    if (num2 >= (typeArray.Length - 1))
                    {
                        generator.EmitLoadArg(1);
                        generator.EmitLoadArg(typeArray.Length);
                        generator.EmitCall(OpCodes.Call, methodInfo, null);
                        generator.EmitCalli(OpCodes.Calli, CallingConvention.StdCall, type3, typeArray.Take<Type>((typeArray.Length - 1)).ToArray<Type>());
                        generator.Emit(OpCodes.Ret);
                        builder.DefineMethodOverride(methodInfoBody, methodInfoDeclaration);
                        index++;
                        break;
                    }
                    generator.EmitLoadArg(num2 + 1);
                    num2++;
                }
            }
            return (IInvokeHelper) Activator.CreateInstance(builder.CreateType());
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly InvokeHelperBuilder.<>c <>9 = new InvokeHelperBuilder.<>c();
            public static Func<ParameterInfo, Type> <>9__2_0;

            internal Type <CreateInvokeHelperImp>b__2_0(ParameterInfo p) => 
                p.ParameterType;
        }
    }
}

