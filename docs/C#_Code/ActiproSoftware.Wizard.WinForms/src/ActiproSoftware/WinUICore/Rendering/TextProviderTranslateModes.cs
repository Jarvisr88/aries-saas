namespace ActiproSoftware.WinUICore.Rendering
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue"), Flags]
    public enum TextProviderTranslateModes
    {
        FromSourceText,
        ToSourceText,
        PositiveTracking
    }
}

