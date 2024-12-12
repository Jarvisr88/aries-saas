namespace DevExpress.Xpf.Core.ReflectionExtensions.Internal
{
    using DevExpress.Xpf.Core.Internal;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.CompilerServices;

    public class TypeGeneratorMethodBuilder : TypeGeneratorMemberBuilder<MethodInfo>
    {
        private TypeGeneratorMethodBuilder(BaseReflectionHelperInterfaceWrapperGenerator owner, MethodInfo wrapperMethodInfo, MemberInfo baseMemberInfo, BaseReflectionHelperInterfaceWrapperSetting setting, MemberInfoKind wrapperMemberKind) : base(owner, wrapperMethodInfo, setting, wrapperMemberKind, baseMemberInfo)
        {
        }

        public static void DefineMethod(BaseReflectionHelperInterfaceWrapperGenerator owner, MethodInfo wrapperMethodInfo, MemberInfo baseMemberInfo, BaseReflectionHelperInterfaceWrapperSetting setting, MemberInfoKind memberKind)
        {
            new TypeGeneratorMethodBuilder(owner, wrapperMethodInfo, baseMemberInfo, setting, memberKind).Define();
        }

        protected override bool DefineOverride()
        {
            Type[] localArray1;
            if (base.sourceMemberInfo != null)
            {
                localArray1 = base.sourceMemberInfo.GetParameters().Select<ParameterInfo, Type>((<>c.<>9__3_0 ??= x => x.ParameterType)).ToArray<Type>();
            }
            else
            {
                MethodInfo sourceMemberInfo = base.sourceMemberInfo;
                localArray1 = null;
            }
            Type[] typeArray = localArray1;
            Type[] parameterTypes = new Type[base.wrapperParameterTypes.Length];
            for (int j = 0; j < base.wrapperParameterTypes.Length; j++)
            {
                Type type3 = base.wrapperParameterTypes[j];
                parameterTypes[j] = type3;
                if (TypeGeneratorHelper.ShouldWrapType(type3))
                {
                    parameterTypes[j] = type3.IsByRef ? TypeGeneratorHelper.TypeofPObject : typeof(object);
                }
            }
            base.ilGenerator = base.methodBuilder.GetILGenerator();
            Type returnType = base.shouldWrapReturnType ? typeof(object) : base.wrapperReturnType;
            bool useTuple = false;
            Type type = ReflectionHelper.MakeGenericDelegate(parameterTypes, ref returnType, base.isStatic ? null : typeof(object), out useTuple);
            MethodInfo method = type.GetMethod("Invoke");
            LocalBuilder tupleLocalBuilder = null;
            if (useTuple)
            {
                tupleLocalBuilder = base.ilGenerator.DeclareLocal(returnType);
            }
            if (base.sourceMemberInfo == null)
            {
                if (!base.PrepareFallback(method, base.baseMemberInfo))
                {
                    base.ilGenerator.Emit(OpCodes.Ret);
                    return false;
                }
            }
            else
            {
                base.ilGenerator.Emit(OpCodes.Ldarg_0);
                base.EmitLdfld(base.sourceMemberInfoStorageField);
                base.EmitTypeOf(base.elementType);
                base.EmitTypeOf(type);
                base.ilGenerator.Emit(useTuple ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0);
                MethodInfo getDelegateMethodInfo = ReflectionHelperInterfaceWrapper.GetDelegateMethodInfo;
                if (base.wrapperGenericParameters.Length != 0)
                {
                    base.ilGenerator.Emit(OpCodes.Ldc_I4, base.wrapperGenericParameters.Length);
                    base.ilGenerator.Emit(OpCodes.Newarr, typeof(Type));
                    int arg = 0;
                    while (true)
                    {
                        if (arg >= base.wrapperGenericParameters.Length)
                        {
                            getDelegateMethodInfo = ReflectionHelperInterfaceWrapper.GetGenericDelegateMethodInfo;
                            break;
                        }
                        base.ilGenerator.Emit(OpCodes.Dup);
                        base.ilGenerator.Emit(OpCodes.Ldc_I4, arg);
                        base.EmitTypeOf(base.genericParameterBuilders[arg]);
                        base.ilGenerator.Emit(OpCodes.Stelem_Ref);
                        arg++;
                    }
                }
                base.EmitCall(getDelegateMethodInfo, null);
            }
            if (!base.isStatic)
            {
                base.EmitLdfld(base.sourceObjectField);
            }
            for (byte k = 0; k < parameterTypes.Length; k = (byte) (k + 1))
            {
                Type type4 = parameterTypes[k];
                base.ilGenerator.Emit(OpCodes.Ldarg, (int) (k + 1));
                if (type4.IsByRef)
                {
                    base.EmitLSTind(type4.GetElementType(), false);
                }
                if (base.wrapperParameterTypes[k] != parameterTypes[k])
                {
                    base.EmitCall(ReflectionHelperInterfaceWrapper.UnwrapMethodInfo, null);
                }
                else if (TypeGeneratorHelper.ShouldWrapDelegate(base.wrapperParameterTypes[k], (typeArray != null) ? typeArray[k] : null))
                {
                    base.EmitTypeOf(typeArray[k]);
                    base.ilGenerator.Emit(OpCodes.Ldarg_0);
                    base.EmitCall(ReflectionHelperInterfaceWrapper.WrapDelegateMethodInfo, null);
                }
            }
            base.EmitCall(method, null);
            if (useTuple)
            {
                Func<Tuple<int, Type, Type>, bool> predicate = <>c.<>9__3_2;
                if (<>c.<>9__3_2 == null)
                {
                    Func<Tuple<int, Type, Type>, bool> local3 = <>c.<>9__3_2;
                    predicate = <>c.<>9__3_2 = x => x.Item2.IsByRef;
                }
                this.SyncTupleItems(parameterTypes.Select<Type, Tuple<int, Type, Type>>((x, i) => new Tuple<int, Type, Type>(i, x, base.wrapperParameterTypes[i])).Where<Tuple<int, Type, Type>>(predicate), returnType, base.wrapperMethodInfo.ReturnType != typeof(void), base.ilGenerator, tupleLocalBuilder, base.typeBuilder);
            }
            return true;
        }

        private bool GetIsInterface(BaseReflectionHelperInterfaceWrapperSetting setting, string name, MemberInfo memberInfo) => 
            setting.GetIsInterface(name, memberInfo);

        protected override BindingFlags GetSourceMemberBindingFlags() => 
            base.setting.GetBindingFlags(base.baseMemberInfo, base.wrapperMethodInfo) | (base.isStatic ? BindingFlags.Static : BindingFlags.Default);

        protected override MethodInfo GetSourceMemberInfo(string name, BindingFlags flags)
        {
            MemberInfo baseMemberInfo = base.baseMemberInfo;
            if (base.baseMemberInfo == null)
            {
                MemberInfo local1 = base.baseMemberInfo;
                baseMemberInfo = base.wrapperMethodInfo;
            }
            IEnumerable<Type> enumerable1 = this.GetIsInterface(base.setting, base.wrapperMethodInfo.Name, baseMemberInfo) ? ((IEnumerable<Type>) base.elementType.GetInterfaces()) : TypeGeneratorHelper.FlatternType(base.elementType, false);
            return (from x in enumerable1 select TypeGeneratorHelper.GetMethod(x, name, flags, this.wrapperMethodInfo)).FirstOrDefault<MethodInfo>((<>c.<>9__2_1 ??= x => (x != null)));
        }

        private static MethodInfo GetTupleItem(Type type, int i) => 
            type.GetMethod($"get_Item{i + 1}");

        protected void SyncTupleItems(IEnumerable<Tuple<int, Type, Type>> tuples, Type returnType, bool skipFirst, ILGenerator ilGenerator, LocalBuilder tupleLocalBuilder, TypeBuilder typeBuilder)
        {
            int num = skipFirst ? 1 : 0;
            ilGenerator.Emit(OpCodes.Stloc, tupleLocalBuilder);
            if (skipFirst)
            {
                ilGenerator.Emit(OpCodes.Ldloc, tupleLocalBuilder);
                base.EmitCall(GetTupleItem(returnType, 0), null);
            }
            Tuple<int, Type, Type>[] tupleArray = tuples.ToArray<Tuple<int, Type, Type>>();
            for (int i = 0; i < tupleArray.Length; i++)
            {
                Tuple<int, Type, Type> tuple = tupleArray[i];
                int arg = ((byte) tuple.Item1) + 1;
                ilGenerator.Emit(OpCodes.Ldarg, arg);
                ilGenerator.Emit(OpCodes.Ldloc, tupleLocalBuilder);
                base.EmitCall(GetTupleItem(returnType, i + num), null);
                if (tuple.Item2 != tuple.Item3)
                {
                    base.EmitTypeOf(tuple.Item3.GetElementType());
                    base.EmitCall(ReflectionHelperInterfaceWrapper.WrapMethodInfo, null);
                }
                base.EmitLSTind(tuple.Item3.GetElementType(), true);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TypeGeneratorMethodBuilder.<>c <>9 = new TypeGeneratorMethodBuilder.<>c();
            public static Func<MethodInfo, bool> <>9__2_1;
            public static Func<ParameterInfo, Type> <>9__3_0;
            public static Func<Tuple<int, Type, Type>, bool> <>9__3_2;

            internal Type <DefineOverride>b__3_0(ParameterInfo x) => 
                x.ParameterType;

            internal bool <DefineOverride>b__3_2(Tuple<int, Type, Type> x) => 
                x.Item2.IsByRef;

            internal bool <GetSourceMemberInfo>b__2_1(MethodInfo x) => 
                x != null;
        }
    }
}

