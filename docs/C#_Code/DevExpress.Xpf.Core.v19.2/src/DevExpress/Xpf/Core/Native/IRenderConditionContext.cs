namespace DevExpress.Xpf.Core.Native
{
    using System;

    public interface IRenderConditionContext : IRenderPropertyContext
    {
        bool IsValid { get; }
    }
}

