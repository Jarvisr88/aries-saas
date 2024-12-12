namespace DevExpress.Utils
{
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public static class DXMethodInfoExtensions
    {
        public static Delegate CreateDelegate(this MethodInfo instance, Type delegateType) => 
            Delegate.CreateDelegate(delegateType, instance);

        public static Delegate CreateDelegate(this MethodInfo instance, Type delegateType, object target) => 
            Delegate.CreateDelegate(delegateType, target, instance);
    }
}

