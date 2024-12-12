namespace DevExpress.Xpf.Core.ReflectionExtensions.Internal
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection.Emit;

    public class ReflectionHelperStaticInterfaceWrapperGenerator<TWrapper> : ReflectionHelperInterfaceWrapperGenerator<TWrapper>
    {
        public ReflectionHelperStaticInterfaceWrapperGenerator(ModuleBuilder builder, object element) : base(builder, element, true)
        {
        }

        public InterfaceWrapperMemberInfoInstance<TWrapper, ReflectionHelperStaticInterfaceWrapperGenerator<TWrapper>> DefineMethod(Expression<Action<TWrapper>> expression) => 
            base.DefineMethod<ReflectionHelperStaticInterfaceWrapperGenerator<TWrapper>>(expression);

        public InterfaceWrapperPropertyMemberInfoInstance<TWrapper, ReflectionHelperStaticInterfaceWrapperGenerator<TWrapper>> DefineProperty(Expression<Func<TWrapper, object>> expression) => 
            base.DefineProperty<ReflectionHelperStaticInterfaceWrapperGenerator<TWrapper>>(expression);
    }
}

