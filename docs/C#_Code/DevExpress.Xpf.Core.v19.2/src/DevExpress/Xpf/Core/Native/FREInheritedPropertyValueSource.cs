namespace DevExpress.Xpf.Core.Native
{
    using System;

    [Flags]
    public enum FREInheritedPropertyValueSource
    {
        public const FREInheritedPropertyValueSource Default = FREInheritedPropertyValueSource.Default;,
        public const FREInheritedPropertyValueSource Inherited = FREInheritedPropertyValueSource.Inherited;,
        public const FREInheritedPropertyValueSource Local = FREInheritedPropertyValueSource.Local;,
        public const FREInheritedPropertyValueSource Coerced = FREInheritedPropertyValueSource.Coerced;
    }
}

