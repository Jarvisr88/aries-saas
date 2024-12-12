namespace DevExpress.Xpf.Core.ReflectionExtensions.Attributes
{
    using System;

    public enum ReflectionHelperFallbackMode
    {
        Default,
        ThrowNotImplementedException,
        FallbackWithoutValidation,
        AbortWrapping,
        UseFallbackType
    }
}

