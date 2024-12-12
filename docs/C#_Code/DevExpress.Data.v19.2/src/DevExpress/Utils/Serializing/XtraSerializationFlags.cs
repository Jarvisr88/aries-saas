namespace DevExpress.Utils.Serializing
{
    using System;

    [Flags]
    public enum XtraSerializationFlags
    {
        None = 0,
        UseAssign = 1,
        DefaultValue = 2,
        Cached = 4,
        DeserializeCollectionItemBeforeCallSetIndex = 8,
        SuppressDefaultValue = 0x10,
        AutoScaleX = 0x20,
        AutoScaleY = 0x40,
        AutoScale = 0x60,
        LoadOnly = 0x100,
        CollectionContent = 0x200,
        AutoScaleIgnoreDefault = 0x400,
        AutoScaleXNoDefault = 0x420,
        AutoScaleYNoDefault = 0x440,
        AutoScaleNoDefault = 0x460
    }
}

