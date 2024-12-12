namespace DevExpress.Xpf.Core.ReflectionExtensions.Internal
{
    using System;

    [Flags]
    public enum MemberInfoKind
    {
        Method = 1,
        PropertyGetter = 2,
        PropertySetter = 4,
        EventAdd = 8,
        EventRemove = 0x10,
        Field = 0x20
    }
}

