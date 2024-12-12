namespace DevExpress.DirectX.NativeInterop.CCW
{
    using DevExpress.DirectX.Common;
    using DevExpress.DirectX.NativeInterop;
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.CompilerServices;

    internal class ComCallableWrapperMethodBuilder
    {
        private static Type nativeIntType = typeof(IntPtr);
        private readonly ILGenerator generator;
        private readonly Type[] targetMethodParameters;
        private int localsCount;

        private ComCallableWrapperMethodBuilder(TypeBuilder typeBuilder, MethodInfo targetMethod, string methodName)
        {
            Func<ParameterInfo, Type> selector = <>c.<>9__5_0;
            if (<>c.<>9__5_0 == null)
            {
                Func<ParameterInfo, Type> local1 = <>c.<>9__5_0;
                selector = <>c.<>9__5_0 = p => p.ParameterType;
            }
            this.targetMethodParameters = targetMethod.GetParameters().Select<ParameterInfo, Type>(selector).ToArray<Type>();
            Type[] first = new Type[] { nativeIntType };
            Func<Type, Type> func2 = <>c.<>9__5_1;
            if (<>c.<>9__5_1 == null)
            {
                Func<Type, Type> local2 = <>c.<>9__5_1;
                func2 = <>c.<>9__5_1 = t => !typeof(ComObject).IsAssignableFrom(t) ? t : nativeIntType;
            }
            Type[] parameterTypes = first.Concat<Type>(this.targetMethodParameters.Select<Type, Type>(func2)).ToArray<Type>();
            this.generator = typeBuilder.DefineMethod(methodName, MethodAttributes.Static | MethodAttributes.Public, targetMethod.ReturnType, parameterTypes).GetILGenerator();
        }

        private void AppendMethod(MethodInfo interfaceMethod, MethodInfo targetMethod, Type interfaceType)
        {
            bool[] flagArray = new bool[this.targetMethodParameters.Length];
            for (int i = 0; i < this.targetMethodParameters.Length; i++)
            {
                if (!typeof(ComObject).IsAssignableFrom(this.targetMethodParameters[i]))
                {
                    flagArray[i] = false;
                }
                else
                {
                    this.StartUsingBlock(i + 1, this.targetMethodParameters[i]);
                    flagArray[i] = true;
                }
            }
            MethodInfo method = typeof(ComCallableWrapperHelper).GetMethod("FromIntPtr", BindingFlags.Public | BindingFlags.Static);
            this.generator.Emit(OpCodes.Ldarg_0);
            Type[] typeArguments = new Type[] { interfaceType };
            this.generator.EmitCall(OpCodes.Call, method.MakeGenericMethod(typeArguments), null);
            int index = 0;
            int num4 = 0;
            while (index < this.targetMethodParameters.Length)
            {
                if (flagArray[index])
                {
                    this.generator.EmitLoadLocal(num4++);
                }
                else
                {
                    this.generator.EmitLoadArg(index + 1);
                }
                index++;
            }
            bool flag = targetMethod.ReturnType.Name != "Void";
            int arg = 0;
            this.generator.EmitCall(OpCodes.Callvirt, interfaceMethod, null);
            if (flag && (this.localsCount != 0))
            {
                arg = this.generator.DeclareLocal(targetMethod.ReturnType).LocalIndex;
                this.generator.Emit(OpCodes.Stloc, arg);
            }
            for (int j = 0; j < this.localsCount; j++)
            {
                this.EndUsingBlock((this.localsCount - j) - 1);
            }
            if (flag)
            {
                if (arg != 0)
                {
                    this.generator.EmitLoadLocal(arg);
                }
                this.generator.Emit(OpCodes.Ret);
            }
        }

        public static string CreateMethod(TypeBuilder typeBuilder, MethodInfo interfaceMethod, MethodInfo targetMethod, Type interfaceType)
        {
            string name = targetMethod.Name;
            new ComCallableWrapperMethodBuilder(typeBuilder, targetMethod, name).AppendMethod(interfaceMethod, targetMethod, interfaceType);
            return name;
        }

        private void EndUsingBlock(int localIndex)
        {
            this.generator.BeginFinallyBlock();
            Label label = this.generator.DefineLabel();
            this.generator.EmitLoadLocal(localIndex);
            this.generator.Emit(OpCodes.Brfalse_S, label);
            this.generator.EmitLoadLocal(localIndex);
            this.generator.EmitCall(OpCodes.Callvirt, typeof(IDisposable).GetMethod("Dispose"), null);
            this.generator.MarkLabel(label);
            this.generator.EndExceptionBlock();
        }

        private void StartUsingBlock(int argumentIndex, Type parametrType)
        {
            LocalBuilder builder = this.generator.DeclareLocal(parametrType);
            this.localsCount++;
            MethodInfo method = typeof(ComCallableWrapperHelper).GetMethod("CreateWrapperObject", BindingFlags.Public | BindingFlags.Static);
            this.generator.EmitLoadArg(argumentIndex);
            Type[] typeArguments = new Type[] { parametrType };
            this.generator.EmitCall(OpCodes.Call, method.MakeGenericMethod(typeArguments), null);
            this.generator.EmitStoreLocal(builder.LocalIndex);
            this.generator.BeginExceptionBlock();
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ComCallableWrapperMethodBuilder.<>c <>9 = new ComCallableWrapperMethodBuilder.<>c();
            public static Func<ParameterInfo, Type> <>9__5_0;
            public static Func<Type, Type> <>9__5_1;

            internal Type <.ctor>b__5_0(ParameterInfo p) => 
                p.ParameterType;

            internal Type <.ctor>b__5_1(Type t) => 
                !typeof(ComObject).IsAssignableFrom(t) ? t : ComCallableWrapperMethodBuilder.nativeIntType;
        }
    }
}

