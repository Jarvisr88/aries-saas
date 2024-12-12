namespace ActiproSoftware.WinUICore
{
    using System;

    [Flags]
    public enum InvalidationLevels
    {
        All = 1,
        TopLevelParent = 2,
        Parent = 4,
        Element = 8,
        Children = 0x10,
        ElementAndChildren = 0x18
    }
}

