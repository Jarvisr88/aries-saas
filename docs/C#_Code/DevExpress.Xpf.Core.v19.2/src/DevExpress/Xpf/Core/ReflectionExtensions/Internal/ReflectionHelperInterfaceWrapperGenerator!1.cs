namespace DevExpress.Xpf.Core.ReflectionExtensions.Internal
{
    using DevExpress.Xpf.Core.Internal;
    using DevExpress.Xpf.Core.ReflectionExtensions.Attributes;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.InteropServices;

    public class ReflectionHelperInterfaceWrapperGenerator<TWrapper> : BaseReflectionHelperInterfaceWrapperGenerator
    {
        public ReflectionHelperInterfaceWrapperGenerator(ModuleBuilder builder, object element, bool isStatic) : base(builder, element, isStatic, typeof(TWrapper))
        {
        }

        public TWrapper Create() => 
            (TWrapper) base.CachedCreateImpl();

        public ReflectionHelperInterfaceWrapperGenerator<TWrapper> DefaultBindingFlags(BindingFlags flags = 20)
        {
            base.defaultFlags = flags;
            return (ReflectionHelperInterfaceWrapperGenerator<TWrapper>) this;
        }

        public ReflectionHelperInterfaceWrapperGenerator<TWrapper> DefaultFallbackMode(ReflectionHelperFallbackMode mode)
        {
            if (mode != ReflectionHelperFallbackMode.Default)
            {
                base.defaultFallbackModeField = (ReflectionHelperFallbackModeInternal) mode;
            }
            return (ReflectionHelperInterfaceWrapperGenerator<TWrapper>) this;
        }

        protected InterfaceWrapperMemberInfoInstance<TWrapper, TInterfaceWrapper> DefineMethod<TInterfaceWrapper>(Expression<Action<TWrapper>> expression) where TInterfaceWrapper: ReflectionHelperInterfaceWrapperGenerator<TWrapper> => 
            new InterfaceWrapperMemberInfoInstance<TWrapper, TInterfaceWrapper>((expression.Body as MethodCallExpression).Method, (TInterfaceWrapper) this);

        protected InterfaceWrapperPropertyMemberInfoInstance<TWrapper, TInterfaceWrapper> DefineProperty<TInterfaceWrapper>(Expression<Func<TWrapper, object>> expression) where TInterfaceWrapper: ReflectionHelperInterfaceWrapperGenerator<TWrapper> => 
            !(expression.Body is MemberExpression) ? (!(expression.Body is UnaryExpression) ? null : new InterfaceWrapperPropertyMemberInfoInstance<TWrapper, TInterfaceWrapper>(((expression.Body as UnaryExpression).Operand as MemberExpression).Member, (TInterfaceWrapper) this)) : new InterfaceWrapperPropertyMemberInfoInstance<TWrapper, TInterfaceWrapper>((expression.Body as MemberExpression).Member, (TInterfaceWrapper) this);
    }
}

