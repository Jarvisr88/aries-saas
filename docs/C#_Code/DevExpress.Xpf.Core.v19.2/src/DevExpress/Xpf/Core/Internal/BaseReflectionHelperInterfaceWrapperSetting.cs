namespace DevExpress.Xpf.Core.Internal
{
    using DevExpress.Xpf.Core.ReflectionExtensions.Attributes;
    using DevExpress.Xpf.Core.ReflectionExtensions.Internal;
    using System;
    using System.Linq;
    using System.Reflection;

    public abstract class BaseReflectionHelperInterfaceWrapperSetting
    {
        private readonly BaseReflectionHelperInterfaceWrapperGenerator reflectionHelperInterfaceWrapperGenerator;

        public BaseReflectionHelperInterfaceWrapperSetting(BaseReflectionHelperInterfaceWrapperGenerator reflectionHelperInterfaceWrapperGenerator)
        {
            this.reflectionHelperInterfaceWrapperGenerator = reflectionHelperInterfaceWrapperGenerator;
        }

        public abstract int ComputeKey();
        internal virtual bool FieldAccessor(MemberInfo memberInfo) => 
            this.GetAttribute<FieldAccessorAttribute>(memberInfo) != null;

        private TAttribute GetAttribute<TAttribute>() => 
            this.reflectionHelperInterfaceWrapperGenerator.tWrapper.GetCustomAttributes(typeof(TAttribute), true).OfType<TAttribute>().FirstOrDefault<TAttribute>();

        private TAttribute GetAttribute<TAttribute>(MemberInfo memberInfo)
        {
            if (memberInfo != null)
            {
                return memberInfo.GetCustomAttributes(typeof(TAttribute), true).OfType<TAttribute>().FirstOrDefault<TAttribute>();
            }
            return default(TAttribute);
        }

        internal virtual BindingFlags GetBindingFlags(MemberInfo primaryMethodInfo, MemberInfo secondaryMemberInfo)
        {
            BindingFlagsAttribute local1 = this.GetAttribute<BindingFlagsAttribute>(primaryMethodInfo);
            BindingFlagsAttribute local5 = local1;
            if (local1 == null)
            {
                BindingFlagsAttribute local2 = local1;
                BindingFlagsAttribute local3 = this.GetAttribute<BindingFlagsAttribute>(secondaryMemberInfo);
                local5 = local3;
                if (local3 == null)
                {
                    BindingFlagsAttribute local4 = local3;
                    local5 = this.GetAttribute<BindingFlagsAttribute>();
                }
            }
            BindingFlagsAttribute attribute = local5;
            return ((attribute == null) ? ((this.GetAttribute<InterfaceMemberAttribute>(primaryMethodInfo) == null) ? this.reflectionHelperInterfaceWrapperGenerator.defaultFlags : (BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)) : attribute.Flags);
        }

        internal virtual Delegate GetFallback(MemberInfoKind kind) => 
            null;

        internal virtual ReflectionHelperFallbackModeInternal GetFallbackMode(MethodInfo wrapperMethodInfo, MemberInfo baseInfo, Type tWrapper, ReflectionHelperFallbackModeInternal defaultValue)
        {
            ReflectionHelperFallbackMode mode = this.GetFallbackModeOverride(wrapperMethodInfo, baseInfo, tWrapper);
            return ((mode != ReflectionHelperFallbackMode.Default) ? ((ReflectionHelperFallbackModeInternal) mode) : (wrapperMethodInfo.IsAbstract ? defaultValue : ReflectionHelperFallbackModeInternal.CS_8_0_CallDefaultImpl));
        }

        internal virtual ReflectionHelperFallbackMode GetFallbackModeOverride(MethodInfo wrapperMethodInfo, MemberInfo baseInfo, Type tWrapper)
        {
            FallbackModeAttribute local1 = this.GetAttribute<FallbackModeAttribute>(wrapperMethodInfo);
            FallbackModeAttribute local5 = local1;
            if (local1 == null)
            {
                FallbackModeAttribute local2 = local1;
                FallbackModeAttribute local3 = this.GetAttribute<FallbackModeAttribute>(baseInfo);
                local5 = local3;
                if (local3 == null)
                {
                    FallbackModeAttribute local4 = local3;
                    local5 = tWrapper.GetCustomAttributes(typeof(FallbackModeAttribute), true).OfType<FallbackModeAttribute>().FirstOrDefault<FallbackModeAttribute>();
                }
            }
            FallbackModeAttribute attribute = local5;
            ReflectionHelperFallbackMode useFallbackType = ReflectionHelperFallbackMode.Default;
            if (attribute != null)
            {
                useFallbackType = attribute.Mode;
            }
            if ((useFallbackType == ReflectionHelperFallbackMode.Default) && (this.GetFallbackType(tWrapper) != null))
            {
                useFallbackType = ReflectionHelperFallbackMode.UseFallbackType;
            }
            return useFallbackType;
        }

        internal virtual Type GetFallbackType(Type tWrapper)
        {
            FallbackTypeAttribute local1 = tWrapper.GetCustomAttributes(typeof(FallbackTypeAttribute), true).OfType<FallbackTypeAttribute>().FirstOrDefault<FallbackTypeAttribute>();
            if (local1 != null)
            {
                return local1.Type;
            }
            FallbackTypeAttribute local2 = local1;
            return null;
        }

        internal virtual bool GetIsInterface(string name, MemberInfo memberInfo) => 
            this.GetAttribute<InterfaceMemberAttribute>(memberInfo) != null;

        internal virtual string GetName(string defaultName, MemberInfo memberInfo)
        {
            NameAttribute attribute = this.GetAttribute<NameAttribute>(memberInfo);
            return ((attribute == null) ? defaultName : attribute.Name);
        }
    }
}

