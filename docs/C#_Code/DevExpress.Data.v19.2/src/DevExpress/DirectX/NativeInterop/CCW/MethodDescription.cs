namespace DevExpress.DirectX.NativeInterop.CCW
{
    using System;
    using System.Reflection;

    internal class MethodDescription
    {
        private readonly MethodInfo targetMethod;
        private readonly Type interfaceType;
        private readonly MethodInfo interfaceMethod;

        public MethodDescription(MethodInfo targetMethod, Type interfaceType, MethodInfo interfaceMethod)
        {
            this.targetMethod = targetMethod;
            this.interfaceType = interfaceType;
            this.interfaceMethod = interfaceMethod;
        }

        public MethodInfo TargetMethod =>
            this.targetMethod;

        public Type InterfaceType =>
            this.interfaceType;

        public MethodInfo InterfaceMethod =>
            this.interfaceMethod;
    }
}

