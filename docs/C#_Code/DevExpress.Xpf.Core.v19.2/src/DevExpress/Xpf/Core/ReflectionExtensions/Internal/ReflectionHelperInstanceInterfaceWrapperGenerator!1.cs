namespace DevExpress.Xpf.Core.ReflectionExtensions.Internal
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection.Emit;

    public class ReflectionHelperInstanceInterfaceWrapperGenerator<TWrapper> : ReflectionHelperInterfaceWrapperGenerator<TWrapper>
    {
        public ReflectionHelperInstanceInterfaceWrapperGenerator(ModuleBuilder builder, object element) : base(builder, element, false)
        {
        }

        public InterfaceWrapperMemberInfoInstance<TWrapper, ReflectionHelperInstanceInterfaceWrapperGenerator<TWrapper>> DefineMethod(Expression<Action<TWrapper>> expression) => 
            base.DefineMethod<ReflectionHelperInstanceInterfaceWrapperGenerator<TWrapper>>(expression);

        public InterfaceWrapperPropertyMemberInfoInstance<TWrapper, ReflectionHelperInstanceInterfaceWrapperGenerator<TWrapper>> DefineProperty(Expression<Func<TWrapper, object>> expression) => 
            base.DefineProperty<ReflectionHelperInstanceInterfaceWrapperGenerator<TWrapper>>(expression);
    }
}

