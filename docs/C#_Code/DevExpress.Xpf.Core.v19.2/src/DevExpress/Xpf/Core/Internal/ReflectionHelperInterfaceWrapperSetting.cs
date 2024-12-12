namespace DevExpress.Xpf.Core.Internal
{
    using DevExpress.Xpf.Core.ReflectionExtensions.Attributes;
    using DevExpress.Xpf.Core.ReflectionExtensions.Internal;
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class ReflectionHelperInterfaceWrapperSetting : BaseReflectionHelperInterfaceWrapperSetting
    {
        public ReflectionHelperInterfaceWrapperSetting(BaseReflectionHelperInterfaceWrapperGenerator reflectionHelperInterfaceWrapperGenerator) : base(reflectionHelperInterfaceWrapperGenerator)
        {
        }

        public override int ComputeKey() => 
            (((((((((((((this.BindingFlags.GetHashCode() * 0x18d) ^ ((this.Name != null) ? this.Name.GetHashCode() : 0)) * 0x18d) ^ this.IsField.GetHashCode()) * 0x18d) ^ ((this.FallbackAction != null) ? this.FallbackAction.GetHashCode() : 0)) * 0x18d) ^ ((this.GetterFallbackAction != null) ? this.GetterFallbackAction.GetHashCode() : 0)) * 0x18d) ^ ((this.SetterFallbackAction != null) ? this.SetterFallbackAction.GetHashCode() : 0)) * 0x18d) ^ ((this.InterfaceName != null) ? this.InterfaceName.GetHashCode() : 0)) * 0x18d) ^ this.FallbackMode.GetHashCode();

        internal override bool FieldAccessor(MemberInfo memberInfo) => 
            this.IsField;

        internal override System.Reflection.BindingFlags GetBindingFlags(MemberInfo primaryMethodInfo, MemberInfo secondaryMemberInfo)
        {
            System.Reflection.BindingFlags? bindingFlags = this.BindingFlags;
            return ((bindingFlags != null) ? bindingFlags.GetValueOrDefault() : base.GetBindingFlags(primaryMethodInfo, secondaryMemberInfo));
        }

        internal override Delegate GetFallback(MemberInfoKind infoKind)
        {
            Delegate fallbackAction = null;
            switch ((infoKind & ~MemberInfoKind.Field))
            {
                case MemberInfoKind.Method:
                    fallbackAction = this.FallbackAction;
                    break;

                case MemberInfoKind.PropertyGetter:
                    fallbackAction = this.GetterFallbackAction;
                    break;

                case MemberInfoKind.PropertySetter:
                    fallbackAction = this.SetterFallbackAction;
                    break;

                default:
                    throw new ArgumentOutOfRangeException("infoKind", infoKind, null);
            }
            return (fallbackAction ?? base.GetFallback(infoKind));
        }

        internal override ReflectionHelperFallbackMode GetFallbackModeOverride(MethodInfo wrapperMethodInfo, MemberInfo baseInfo, Type tWrapper) => 
            (this.FallbackMode == ReflectionHelperFallbackMode.Default) ? base.GetFallbackModeOverride(wrapperMethodInfo, baseInfo, tWrapper) : this.FallbackMode;

        internal override string GetName(string defaultName, MemberInfo memberInfo)
        {
            string str = "";
            if (!string.IsNullOrEmpty(this.InterfaceName))
            {
                str = this.InterfaceName + ".";
            }
            string name = this.Name;
            string text2 = name;
            if (name == null)
            {
                string local1 = name;
                text2 = base.GetName(defaultName, memberInfo);
            }
            return (str + text2);
        }

        public System.Reflection.BindingFlags? BindingFlags { get; set; }

        public string Name { get; set; }

        public bool IsField { get; set; }

        public Delegate FallbackAction { get; set; }

        public Delegate GetterFallbackAction { get; set; }

        public Delegate SetterFallbackAction { get; set; }

        public string InterfaceName { get; set; }

        public ReflectionHelperFallbackMode FallbackMode { get; set; }
    }
}

