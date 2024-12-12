namespace DevExpress.Xpf.Core.ReflectionExtensions.Internal
{
    using DevExpress.Xpf.Core.ReflectionExtensions.Attributes;
    using System;
    using System.Reflection;

    public class InterfaceWrapperMemberInfoInstance<TWrapper, TInterfaceWrapper> : BaseInterfaceWrapperMemberInfoInstance<TWrapper, TInterfaceWrapper> where TInterfaceWrapper: ReflectionHelperInterfaceWrapperGenerator<TWrapper>
    {
        public InterfaceWrapperMemberInfoInstance(MemberInfo info, TInterfaceWrapper root) : base(info, root)
        {
        }

        public InterfaceWrapperMemberInfoInstance<TWrapper, TInterfaceWrapper> BindingFlags(System.Reflection.BindingFlags flags)
        {
            base.BindingFlagsImpl(flags);
            return (InterfaceWrapperMemberInfoInstance<TWrapper, TInterfaceWrapper>) this;
        }

        public TInterfaceWrapper EndMember() => 
            base.root;

        public InterfaceWrapperMemberInfoInstance<TWrapper, TInterfaceWrapper> Fallback(Delegate fallbackAction)
        {
            base.FallbackImpl(fallbackAction);
            return (InterfaceWrapperMemberInfoInstance<TWrapper, TInterfaceWrapper>) this;
        }

        public InterfaceWrapperMemberInfoInstance<TWrapper, TInterfaceWrapper> FallbackMode(ReflectionHelperFallbackMode mode)
        {
            base.FallbackModeImpl(mode);
            return (InterfaceWrapperMemberInfoInstance<TWrapper, TInterfaceWrapper>) this;
        }

        public InterfaceWrapperMemberInfoInstance<TWrapper, TInterfaceWrapper> InterfaceMember<T>()
        {
            base.InterfaceImpl<T>();
            return (InterfaceWrapperMemberInfoInstance<TWrapper, TInterfaceWrapper>) this;
        }

        public InterfaceWrapperMemberInfoInstance<TWrapper, TInterfaceWrapper> InterfaceMember(string name)
        {
            base.InterfaceImpl(name);
            return (InterfaceWrapperMemberInfoInstance<TWrapper, TInterfaceWrapper>) this;
        }

        public InterfaceWrapperMemberInfoInstance<TWrapper, TInterfaceWrapper> InterfaceMember(Type interfaceType)
        {
            base.InterfaceImpl(interfaceType);
            return (InterfaceWrapperMemberInfoInstance<TWrapper, TInterfaceWrapper>) this;
        }

        public InterfaceWrapperMemberInfoInstance<TWrapper, TInterfaceWrapper> Name(string name)
        {
            base.NameImpl(name);
            return (InterfaceWrapperMemberInfoInstance<TWrapper, TInterfaceWrapper>) this;
        }
    }
}

