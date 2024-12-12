namespace DevExpress.Internal.WinApi
{
    using System;

    internal enum STGM : long
    {
        STGM_READ = 0L,
        STGM_WRITE = 1L,
        STGM_READWRITE = 2L,
        STGM_SHARE_DENY_NONE = 0x40L,
        STGM_SHARE_DENY_READ = 0x30L,
        STGM_SHARE_DENY_WRITE = 0x20L,
        STGM_SHARE_EXCLUSIVE = 0x10L,
        STGM_PRIORITY = 0x40000L,
        STGM_CREATE = 0x1000L,
        STGM_CONVERT = 0x20000L,
        STGM_FAILIFTHERE = 0L,
        STGM_DIRECT = 0L,
        STGM_TRANSACTED = 0x10000L,
        STGM_NOSCRATCH = 0x100000L,
        STGM_NOSNAPSHOT = 0x200000L,
        STGM_SIMPLE = 0x8000000L,
        STGM_DIRECT_SWMR = 0x400000L,
        STGM_DELETEONRELEASE = 0x4000000L
    }
}

