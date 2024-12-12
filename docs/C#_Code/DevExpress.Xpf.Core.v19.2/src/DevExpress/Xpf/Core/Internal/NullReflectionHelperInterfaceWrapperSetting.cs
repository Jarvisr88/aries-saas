namespace DevExpress.Xpf.Core.Internal
{
    using DevExpress.Xpf.Core.ReflectionExtensions.Internal;
    using System;

    public class NullReflectionHelperInterfaceWrapperSetting : BaseReflectionHelperInterfaceWrapperSetting
    {
        public NullReflectionHelperInterfaceWrapperSetting(BaseReflectionHelperInterfaceWrapperGenerator reflectionHelperInterfaceWrapperGenerator) : base(reflectionHelperInterfaceWrapperGenerator)
        {
        }

        public override int ComputeKey() => 
            0;
    }
}

