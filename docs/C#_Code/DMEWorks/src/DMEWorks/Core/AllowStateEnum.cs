namespace DMEWorks.Core
{
    using System;

    [Flags]
    public enum AllowStateEnum
    {
        AllowNone = 0,
        AllowEdit00 = 1,
        AllowEdit01 = 2,
        AllowEdit02 = 4,
        AllowEdit03 = 8,
        AllowEdit04 = 0x10,
        AllowEdit05 = 0x20,
        AllowEdit06 = 0x40,
        AllowEdit07 = 0x80,
        AllowEdit = 0xff,
        AllowNew = 0x100,
        AllowDelete = 0x200,
        AllowAll = 0x3ff
    }
}

