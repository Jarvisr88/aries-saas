namespace DevExpress.Xpf.Printing.Native.Dialogs
{
    using System;
    using System.ComponentModel;

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), Flags]
    internal enum SFGAO : uint
    {
        NONE = 0,
        CANCOPY = 1,
        CANMOVE = 2,
        CANLINK = 4,
        STORAGE = 8,
        CANRENAME = 0x10,
        CANDELETE = 0x20,
        HASPROPSHEET = 0x40,
        DROPTARGET = 0x100,
        CAPABILITYMASK = 0x177,
        SYSTEM = 0x1000,
        ENCRYPTED = 0x2000,
        ISSLOW = 0x4000,
        GHOSTED = 0x8000,
        LINK = 0x10000,
        SHARE = 0x20000,
        READONLY = 0x40000,
        HIDDEN = 0x80000,
        DISPLAYATTRMASK = 0xfc000,
        FILESYSANCESTOR = 0x10000000,
        FOLDER = 0x20000000,
        FILESYSTEM = 0x40000000,
        HASSUBFOLDER = 0x80000000,
        CONTENTSMASK = 0x80000000,
        VALIDATE = 0x1000000,
        REMOVABLE = 0x2000000,
        COMPRESSED = 0x4000000,
        BROWSABLE = 0x8000000,
        NONENUMERATED = 0x100000,
        NEWCONTENT = 0x200000,
        CANMONIKER = 0x400000,
        HASSTORAGE = 0x400000,
        STREAM = 0x400000,
        STORAGEANCESTOR = 0x800000,
        STORAGECAPMASK = 0x70c50008,
        PKEYSFGAOMASK = 0x81044000,
        ALL = 0xfffff17f
    }
}

