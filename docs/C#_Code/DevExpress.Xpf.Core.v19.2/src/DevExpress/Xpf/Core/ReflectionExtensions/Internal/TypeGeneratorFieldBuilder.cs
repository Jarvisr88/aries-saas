namespace DevExpress.Xpf.Core.ReflectionExtensions.Internal
{
    using DevExpress.Xpf.Core.Internal;
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.CompilerServices;

    public class TypeGeneratorFieldBuilder : TypeGeneratorMemberBuilder<FieldInfo>
    {
        private TypeGeneratorFieldBuilder(BaseReflectionHelperInterfaceWrapperGenerator owner, MemberInfo baseMemberInfo, MethodInfo wrapperMethodInfo, BaseReflectionHelperInterfaceWrapperSetting setting, MemberInfoKind wrapperMemberKind) : base(owner, wrapperMethodInfo, setting, wrapperMemberKind, baseMemberInfo)
        {
        }

        public static void DefineFieldGetterOrSetter(BaseReflectionHelperInterfaceWrapperGenerator owner, MemberInfo propertyInfo, MethodInfo wrapperMethodInfo, BaseReflectionHelperInterfaceWrapperSetting setting, MemberInfoKind method)
        {
            new TypeGeneratorFieldBuilder(owner, propertyInfo, wrapperMethodInfo, setting, method).Define();
        }

        protected override bool DefineOverride()
        {
            bool flag = (((base.wrapperMemberKind & MemberInfoKind.PropertySetter) == MemberInfoKind.PropertySetter) && !base.shouldWrapReturnType) && TypeGeneratorHelper.ShouldWrapType(base.wrapperParameterTypes[0]);
            Type[] wrapperParameterTypes = base.wrapperParameterTypes;
            Type returnType = base.shouldWrapReturnType ? typeof(object) : base.wrapperReturnType;
            bool flag2 = base.sourceMemberInfo == null;
            if (flag && !flag2)
            {
                wrapperParameterTypes = new Type[] { base.sourceMemberInfo.FieldType };
            }
            bool useTuple = false;
            Type type = ReflectionHelper.MakeGenericDelegate(wrapperParameterTypes, ref returnType, base.isStatic ? null : typeof(object), out useTuple);
            MethodInfo method = type.GetMethod("Invoke");
            if (flag2)
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
                base.EmitTypeOf(type);
                base.EmitTypeOf(typeof(object));
                if (!flag && !base.shouldWrapReturnType)
                {
                    base.EmitTypeOf(base.sourceMemberInfo.FieldType);
                }
                else
                {
                    base.EmitTypeOf(typeof(object));
                }
                if (base.isStatic)
                {
                    base.ilGenerator.Emit(OpCodes.Ldc_I4_1);
                }
                else
                {
                    base.ilGenerator.Emit(OpCodes.Ldc_I4_0);
                }
                this.EmitCall(((base.wrapperMemberKind & MemberInfoKind.PropertyGetter) == MemberInfoKind.PropertyGetter) ? ReflectionHelperInterfaceWrapper.GetFieldGetterMethodInfo : ReflectionHelperInterfaceWrapper.GetFieldSetterMethodInfo, null);
            }
            if (!base.isStatic)
            {
                base.EmitLdfld(base.sourceObjectField);
            }
            for (byte i = 0; i < base.wrapperParameterTypes.Length; i = (byte) (i + 1))
            {
                base.ilGenerator.Emit(OpCodes.Ldarg, (int) (i + 1));
                if (flag)
                {
                    base.EmitCall(ReflectionHelperInterfaceWrapper.UnwrapMethodInfo, null);
                }
            }
            base.EmitCall(method, null);
            return true;
        }

        protected override BindingFlags GetSourceMemberBindingFlags() => 
            base.setting.GetBindingFlags(base.wrapperMethodInfo, base.baseMemberInfo) | (base.isStatic ? BindingFlags.Static : BindingFlags.Default);

        protected override FieldInfo GetSourceMemberInfo(string name, BindingFlags flags)
        {
            Func<FieldInfo, bool> predicate = <>c.<>9__2_1;
            if (<>c.<>9__2_1 == null)
            {
                Func<FieldInfo, bool> local1 = <>c.<>9__2_1;
                predicate = <>c.<>9__2_1 = x => x != null;
            }
            return (from x in TypeGeneratorHelper.FlatternType(base.elementType, false) select x.GetField(name, flags)).FirstOrDefault<FieldInfo>(predicate);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TypeGeneratorFieldBuilder.<>c <>9 = new TypeGeneratorFieldBuilder.<>c();
            public static Func<FieldInfo, bool> <>9__2_1;

            internal bool <GetSourceMemberInfo>b__2_1(FieldInfo x) => 
                x != null;
        }
    }
}

