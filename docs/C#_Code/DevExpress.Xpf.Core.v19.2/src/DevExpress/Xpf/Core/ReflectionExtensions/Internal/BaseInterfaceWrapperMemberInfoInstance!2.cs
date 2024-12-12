namespace DevExpress.Xpf.Core.ReflectionExtensions.Internal
{
    using DevExpress.Xpf.Core.Internal;
    using DevExpress.Xpf.Core.ReflectionExtensions.Attributes;
    using System;
    using System.Reflection;

    public class BaseInterfaceWrapperMemberInfoInstance<TWrapper, TInterfaceWrapper> where TInterfaceWrapper: ReflectionHelperInterfaceWrapperGenerator<TWrapper>
    {
        protected readonly MemberInfo info;
        protected BindingFlags? flags;
        protected TInterfaceWrapper root;

        public BaseInterfaceWrapperMemberInfoInstance(MemberInfo info, TInterfaceWrapper root)
        {
            this.info = info;
            this.root = root;
        }

        protected void BindingFlagsImpl(BindingFlags flags)
        {
            this.root.WriteSetting(this.info, x => x.BindingFlags = new BindingFlags?(flags));
        }

        protected void FallbackImpl(Delegate fallbackAction)
        {
            this.root.WriteSetting(this.info, x => x.FallbackAction = fallbackAction);
        }

        protected void FallbackModeImpl(ReflectionHelperFallbackMode value)
        {
            this.root.WriteSetting(this.info, x => x.FallbackMode = value);
        }

        protected void InterfaceImpl<T>()
        {
            this.InterfaceImpl(typeof(T));
        }

        protected void InterfaceImpl(string name)
        {
            this.root.WriteSetting(this.info, x => x.InterfaceName = name);
            this.BindingFlagsImpl(BindingFlags.NonPublic | BindingFlags.Instance);
        }

        protected void InterfaceImpl(Type type)
        {
            this.InterfaceImpl(type.FullName);
        }

        protected void NameImpl(string name)
        {
            this.root.WriteSetting(this.info, x => x.Name = name);
        }
    }
}

