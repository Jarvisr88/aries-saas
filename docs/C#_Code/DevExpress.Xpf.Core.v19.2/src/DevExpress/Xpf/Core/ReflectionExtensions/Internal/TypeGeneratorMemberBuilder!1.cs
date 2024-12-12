namespace DevExpress.Xpf.Core.ReflectionExtensions.Internal
{
    using DevExpress.Xpf.Core.Internal;
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

    public abstract class TypeGeneratorMemberBuilder<TMemberInfo> where TMemberInfo: MemberInfo
    {
        protected readonly BaseReflectionHelperInterfaceWrapperGenerator owner;
        protected readonly MethodInfo wrapperMethodInfo;
        protected readonly BaseReflectionHelperInterfaceWrapperSetting setting;
        protected readonly MemberInfoKind wrapperMemberKind;
        protected readonly TypeBuilder typeBuilder;
        protected readonly bool isStatic;
        protected readonly Type tWrapper;
        protected readonly Type elementType;
        protected readonly FieldBuilder sourceObjectField;
        protected MemberInfo baseMemberInfo;
        protected TMemberInfo sourceMemberInfo;
        protected FieldBuilder sourceMemberInfoStorageField;
        protected MethodBuilder methodBuilder;
        protected ILGenerator ilGenerator;
        protected string sourceMemberName;
        protected BindingFlags sourceMemberBindingFlags;
        protected Type[] wrapperParameterTypes;
        protected Type[] wrapperGenericParameters;
        protected Type wrapperReturnType;
        protected bool shouldWrapReturnType;
        protected GenericTypeParameterBuilder[] genericParameterBuilders;

        protected TypeGeneratorMemberBuilder(BaseReflectionHelperInterfaceWrapperGenerator owner, MethodInfo wrapperMethodInfo, BaseReflectionHelperInterfaceWrapperSetting setting, MemberInfoKind wrapperMemberKind, MemberInfo baseMemberInfo)
        {
            this.owner = owner;
            this.wrapperMethodInfo = wrapperMethodInfo;
            this.setting = setting;
            this.wrapperMemberKind = wrapperMemberKind;
            this.typeBuilder = owner.typeBuilder;
            this.isStatic = owner.isStatic;
            this.tWrapper = owner.tWrapper;
            this.sourceObjectField = owner.sourceObjectField;
            this.elementType = owner.ElementType;
            this.baseMemberInfo = baseMemberInfo;
        }

        protected FieldBuilder CreateStorageField<TFieldMemberInfo>(TFieldMemberInfo memberInfo, string name)
        {
            if (memberInfo == null)
            {
                return null;
            }
            FieldBuilder item = this.typeBuilder.DefineField("field" + name, typeof(FieldInfo), FieldAttributes.Private);
            this.owner.ctorInfos.Add(item);
            this.owner.ctorArgs.Add(memberInfo);
            return item;
        }

        protected void Define()
        {
            this.sourceMemberName = this.GetTargetName();
            this.sourceMemberBindingFlags = this.GetSourceMemberBindingFlags();
            this.sourceMemberInfo = this.GetSourceMemberInfo(this.sourceMemberName, this.sourceMemberBindingFlags);
            this.sourceMemberInfoStorageField = this.CreateStorageField<TMemberInfo>(this.sourceMemberInfo, this.wrapperMethodInfo.Name);
            Func<ParameterInfo, Type> selector = <>c<TMemberInfo>.<>9__28_0;
            if (<>c<TMemberInfo>.<>9__28_0 == null)
            {
                Func<ParameterInfo, Type> local1 = <>c<TMemberInfo>.<>9__28_0;
                selector = <>c<TMemberInfo>.<>9__28_0 = x => x.ParameterType;
            }
            this.wrapperParameterTypes = this.wrapperMethodInfo.GetParameters().Select<ParameterInfo, Type>(selector).ToArray<Type>();
            this.wrapperGenericParameters = this.wrapperMethodInfo.GetGenericArguments();
            this.wrapperReturnType = this.wrapperMethodInfo.ReturnType;
            this.shouldWrapReturnType = TypeGeneratorHelper.ShouldWrapType(this.wrapperReturnType);
            this.methodBuilder = this.typeBuilder.DefineMethod(this.wrapperMethodInfo.Name, MethodAttributes.Virtual | MethodAttributes.Public, this.wrapperMethodInfo.ReturnType, this.wrapperParameterTypes);
            if (this.wrapperGenericParameters.Length != 0)
            {
                Func<Type, string> func2 = <>c<TMemberInfo>.<>9__28_1;
                if (<>c<TMemberInfo>.<>9__28_1 == null)
                {
                    Func<Type, string> local2 = <>c<TMemberInfo>.<>9__28_1;
                    func2 = <>c<TMemberInfo>.<>9__28_1 = x => x.Name;
                }
                this.genericParameterBuilders = this.methodBuilder.DefineGenericParameters(this.wrapperGenericParameters.Select<Type, string>(func2).ToArray<string>());
            }
            this.ilGenerator = this.methodBuilder.GetILGenerator();
            if (this.DefineOverride())
            {
                if (this.shouldWrapReturnType)
                {
                    this.EmitTypeOf(this.wrapperReturnType);
                    this.EmitCall(ReflectionHelperInterfaceWrapper.WrapMethodInfo, null);
                }
                this.ilGenerator.Emit(OpCodes.Ret);
                this.typeBuilder.DefineMethodOverride(this.methodBuilder, this.wrapperMethodInfo);
            }
        }

        protected abstract bool DefineOverride();
        protected void EmitCall(MethodInfo info, Type[] types = null)
        {
            TypeGeneratorHelper.EmitCall(this.ilGenerator, info.IsVirtual ? OpCodes.Callvirt : OpCodes.Call, info, types);
        }

        protected void EmitLdfld(FieldInfo fieldBuilder)
        {
            TypeGeneratorHelper.Ldfld(this.ilGenerator, fieldBuilder);
        }

        protected void EmitLSTind(Type type, bool stind)
        {
            TypeGeneratorHelper.LSTind(this.ilGenerator, type, stind);
        }

        protected void EmitTypeOf(Type type)
        {
            TypeGeneratorHelper.TypeOf(this.ilGenerator, type);
        }

        protected abstract BindingFlags GetSourceMemberBindingFlags();
        protected abstract TMemberInfo GetSourceMemberInfo(string name, BindingFlags flags);
        protected string GetTargetName()
        {
            string str = null;
            MemberInfoKind wrapperMemberKind = this.wrapperMemberKind;
            if (wrapperMemberKind <= MemberInfoKind.PropertySetter)
            {
                if (wrapperMemberKind == MemberInfoKind.PropertyGetter)
                {
                    str = "get_";
                }
                else if (wrapperMemberKind == MemberInfoKind.PropertySetter)
                {
                    str = "set_";
                }
            }
            else if (wrapperMemberKind == MemberInfoKind.EventAdd)
            {
                str = "add_";
            }
            else if (wrapperMemberKind == MemberInfoKind.EventRemove)
            {
                str = "remove_";
            }
            MemberInfo baseMemberInfo = this.baseMemberInfo;
            if (this.baseMemberInfo == null)
            {
                MemberInfo local1 = this.baseMemberInfo;
                baseMemberInfo = this.wrapperMethodInfo;
            }
            string name = this.setting.GetName(baseMemberInfo.Name, this.baseMemberInfo ?? this.wrapperMethodInfo);
            if (!string.IsNullOrEmpty(str))
            {
                char[] separator = new char[] { '.' };
                if (!name.Split(separator).Last<string>().StartsWith(str))
                {
                    return (str + name);
                }
            }
            return name;
        }

        protected bool PrepareFallback(MethodInfo delegateInvoke, MemberInfo baseInfo)
        {
            ReflectionHelperFallbackModeInternal fallbackMode = this.setting.GetFallbackMode(this.wrapperMethodInfo, baseInfo, this.tWrapper, this.owner.defaultFallbackModeField);
            switch (fallbackMode)
            {
                case ReflectionHelperFallbackModeInternal.FallbackWithValidation:
                case ReflectionHelperFallbackModeInternal.FallbackWithoutValidation:
                    return this.PrepareFallback_FromDelegate(delegateInvoke, fallbackMode);

                case ReflectionHelperFallbackModeInternal.AbortWrapping:
                    throw new MissingMemberException($"
Cannot bind the {this.wrapperMethodInfo.DeclaringType}.{this.wrapperMethodInfo.Name} with the source member
");

                case ReflectionHelperFallbackModeInternal.UseFallbackType:
                    return this.PrepareFallback_UseFallbackType();
            }
            if (fallbackMode != ReflectionHelperFallbackModeInternal.CS_8_0_CallDefaultImpl)
            {
                throw new ArgumentOutOfRangeException();
            }
            return this.PrepareFallback_CS_8_0_DefaultImpl();
        }

        private bool PrepareFallback_CS_8_0_DefaultImpl()
        {
            this.ilGenerator.Emit(OpCodes.Ldarg_0);
            this.EmitCall(this.wrapperMethodInfo, null);
            return false;
        }

        private bool PrepareFallback_FromDelegate(MethodInfo delegateInvoke, ReflectionHelperFallbackModeInternal fallbackMode)
        {
            Delegate fallback = this.setting.GetFallback(this.wrapperMemberKind);
            if (fallback == null)
            {
                if (fallbackMode != ReflectionHelperFallbackModeInternal.FallbackWithValidation)
                {
                    throw new ArgumentException($"
Cannot bind the {this.wrapperMethodInfo.DeclaringType}.{this.wrapperMethodInfo.Name} with the source member.
Please check spelling or define the fallback method with the following signature: 
	{delegateInvoke.ToString()}.
");
                }
                if (!this.isStatic)
                {
                    this.ilGenerator.Emit(OpCodes.Pop);
                }
                this.ilGenerator.ThrowException(typeof(NotImplementedException));
                return false;
            }
            Type type = fallback.GetType();
            if (fallbackMode != ReflectionHelperFallbackModeInternal.FallbackWithoutValidation)
            {
                MethodInfo method = type.GetMethod("Invoke");
                ParameterInfo[] parameters = method.GetParameters();
                ParameterInfo[] infoArray2 = delegateInvoke.GetParameters();
                StringBuilder builder2 = new StringBuilder();
                object[] args = new object[] { this.wrapperMethodInfo.DeclaringType, this.wrapperMethodInfo.Name, delegateInvoke.ToString(), method.ToString() };
                builder2.AppendFormat("\r\nFallback method for the {0}.{1} has incorrect signature.\r\nExpected: {2};\r\nBut was: {3}.", args);
                if ((parameters.Length != infoArray2.Length) || !method.ReturnType.IsAssignableFrom(delegateInvoke.ReturnType))
                {
                    throw new ArgumentException(builder2.ToString() + "\r\n");
                }
                bool flag = false;
                int index = 0;
                while (true)
                {
                    if (index >= parameters.Length)
                    {
                        if (!flag)
                        {
                            break;
                        }
                        throw new ArgumentException(builder2.ToString() + "\r\n");
                    }
                    ParameterInfo info2 = parameters[index];
                    ParameterInfo info3 = infoArray2[index];
                    if (!info2.ParameterType.IsAssignableFrom(info3.ParameterType))
                    {
                        builder2.AppendFormat("\r\n\tParameter at {0}:\r\n\t\tShould be assignable with: {1}\r\n\t\tBut was: {2}", index, info3.ParameterType, info2.ParameterType);
                        flag = true;
                    }
                    index++;
                }
            }
            FieldBuilder fieldBuilder = this.CreateStorageField<Delegate>(fallback, this.wrapperMethodInfo.Name + "fallback");
            this.EmitLdfld(fieldBuilder);
            return true;
        }

        private bool PrepareFallback_UseFallbackType()
        {
            Type fallbackType = this.setting.GetFallbackType(this.tWrapper);
            if (!this.wrapperMethodInfo.DeclaringType.IsAssignableFrom(fallbackType))
            {
                throw new ArgumentException($"FallbackType should implement the {this.wrapperMethodInfo.DeclaringType} interface");
            }
            ConstructorInfo constructor = fallbackType.GetConstructor(new Type[0]);
            if (constructor == null)
            {
                throw new ArgumentException("FallbackType should have a default ctor");
            }
            this.ilGenerator.Emit(OpCodes.Newobj, constructor);
            ReflectionHelper.CastClass(this.ilGenerator, fallbackType, this.wrapperMethodInfo.DeclaringType, false);
            for (int i = 0; i < this.wrapperMethodInfo.GetParameters().Length; i++)
            {
                this.ilGenerator.Emit(OpCodes.Ldarg, (int) (i + 1));
            }
            this.EmitCall(this.wrapperMethodInfo, null);
            return false;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TypeGeneratorMemberBuilder<TMemberInfo>.<>c <>9;
            public static Func<ParameterInfo, Type> <>9__28_0;
            public static Func<Type, string> <>9__28_1;

            static <>c()
            {
                TypeGeneratorMemberBuilder<TMemberInfo>.<>c.<>9 = new TypeGeneratorMemberBuilder<TMemberInfo>.<>c();
            }

            internal Type <Define>b__28_0(ParameterInfo x) => 
                x.ParameterType;

            internal string <Define>b__28_1(Type x) => 
                x.Name;
        }
    }
}

