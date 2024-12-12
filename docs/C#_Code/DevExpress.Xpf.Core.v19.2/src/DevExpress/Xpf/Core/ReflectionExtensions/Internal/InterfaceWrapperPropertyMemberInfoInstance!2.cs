namespace DevExpress.Xpf.Core.ReflectionExtensions.Internal
{
    using DevExpress.Xpf.Core.Internal;
    using DevExpress.Xpf.Core.ReflectionExtensions.Attributes;
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class InterfaceWrapperPropertyMemberInfoInstance<TWrapper, TInterfaceWrapper> : BaseInterfaceWrapperMemberInfoInstance<TWrapper, TInterfaceWrapper> where TInterfaceWrapper: ReflectionHelperInterfaceWrapperGenerator<TWrapper>
    {
        public InterfaceWrapperPropertyMemberInfoInstance(MemberInfo info, TInterfaceWrapper root) : base(info, root)
        {
        }

        public InterfaceWrapperPropertyMemberInfoInstance<TWrapper, TInterfaceWrapper> BindingFlags(System.Reflection.BindingFlags flags)
        {
            base.BindingFlagsImpl(flags);
            return (InterfaceWrapperPropertyMemberInfoInstance<TWrapper, TInterfaceWrapper>) this;
        }

        public TInterfaceWrapper EndMember() => 
            base.root;

        public InterfaceWrapperPropertyMemberInfoInstance<TWrapper, TInterfaceWrapper> Fallback(Delegate fallbackAction)
        {
            base.FallbackImpl(fallbackAction);
            return (InterfaceWrapperPropertyMemberInfoInstance<TWrapper, TInterfaceWrapper>) this;
        }

        public InterfaceWrapperPropertyMemberInfoInstance<TWrapper, TInterfaceWrapper> FallbackMode(ReflectionHelperFallbackMode mode)
        {
            base.FallbackModeImpl(mode);
            return (InterfaceWrapperPropertyMemberInfoInstance<TWrapper, TInterfaceWrapper>) this;
        }

        public InterfaceWrapperPropertyMemberInfoInstance<TWrapper, TInterfaceWrapper> FieldAccessor()
        {
            Action<ReflectionHelperInterfaceWrapperSetting> func = <>c<TWrapper, TInterfaceWrapper>.<>9__9_0;
            if (<>c<TWrapper, TInterfaceWrapper>.<>9__9_0 == null)
            {
                Action<ReflectionHelperInterfaceWrapperSetting> local1 = <>c<TWrapper, TInterfaceWrapper>.<>9__9_0;
                func = <>c<TWrapper, TInterfaceWrapper>.<>9__9_0 = delegate (ReflectionHelperInterfaceWrapperSetting x) {
                    x.IsField = true;
                };
            }
            base.root.WriteSetting(base.info, func);
            return (InterfaceWrapperPropertyMemberInfoInstance<TWrapper, TInterfaceWrapper>) this;
        }

        public InterfaceWrapperPropertyMemberInfoInstance<TWrapper, TInterfaceWrapper> GetterFallback(Delegate fallbackAction)
        {
            base.root.WriteSetting(base.info, delegate (ReflectionHelperInterfaceWrapperSetting x) {
                x.GetterFallbackAction = fallbackAction;
            });
            return (InterfaceWrapperPropertyMemberInfoInstance<TWrapper, TInterfaceWrapper>) this;
        }

        public InterfaceWrapperPropertyMemberInfoInstance<TWrapper, TInterfaceWrapper> InterfaceMember<T>()
        {
            base.InterfaceImpl<T>();
            return (InterfaceWrapperPropertyMemberInfoInstance<TWrapper, TInterfaceWrapper>) this;
        }

        public InterfaceWrapperPropertyMemberInfoInstance<TWrapper, TInterfaceWrapper> InterfaceMember(string name)
        {
            base.InterfaceImpl(name);
            return (InterfaceWrapperPropertyMemberInfoInstance<TWrapper, TInterfaceWrapper>) this;
        }

        public InterfaceWrapperPropertyMemberInfoInstance<TWrapper, TInterfaceWrapper> InterfaceMember(Type interfaceType)
        {
            base.InterfaceImpl(interfaceType);
            return (InterfaceWrapperPropertyMemberInfoInstance<TWrapper, TInterfaceWrapper>) this;
        }

        public InterfaceWrapperPropertyMemberInfoInstance<TWrapper, TInterfaceWrapper> Name(string name)
        {
            base.NameImpl(name);
            return (InterfaceWrapperPropertyMemberInfoInstance<TWrapper, TInterfaceWrapper>) this;
        }

        public InterfaceWrapperPropertyMemberInfoInstance<TWrapper, TInterfaceWrapper> SetterFallback(Delegate fallbackAction)
        {
            base.root.WriteSetting(base.info, delegate (ReflectionHelperInterfaceWrapperSetting x) {
                x.SetterFallbackAction = fallbackAction;
            });
            return (InterfaceWrapperPropertyMemberInfoInstance<TWrapper, TInterfaceWrapper>) this;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly InterfaceWrapperPropertyMemberInfoInstance<TWrapper, TInterfaceWrapper>.<>c <>9;
            public static Action<ReflectionHelperInterfaceWrapperSetting> <>9__9_0;

            static <>c()
            {
                InterfaceWrapperPropertyMemberInfoInstance<TWrapper, TInterfaceWrapper>.<>c.<>9 = new InterfaceWrapperPropertyMemberInfoInstance<TWrapper, TInterfaceWrapper>.<>c();
            }

            internal void <FieldAccessor>b__9_0(ReflectionHelperInterfaceWrapperSetting x)
            {
                x.IsField = true;
            }
        }
    }
}

