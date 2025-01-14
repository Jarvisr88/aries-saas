﻿namespace DevExpress.Internal.WinApi
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    [ComImport, Guid("000214F9-0000-0000-C000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IShellLinkW
    {
        uint GetPath([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile, int cchMaxPath, IntPtr pfd, uint fFlags);
        uint GetIDList(out IntPtr ppidl);
        uint SetIDList(IntPtr pidl);
        uint GetDescription([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile, int cchMaxName);
        uint SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);
        uint GetWorkingDirectory([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszDir, int cchMaxPath);
        uint SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string pszDir);
        uint GetArguments([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszArgs, int cchMaxPath);
        uint SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);
        uint GetHotKey(out short wHotKey);
        uint SetHotKey(short wHotKey);
        uint GetShowCmd(out uint iShowCmd);
        uint SetShowCmd(uint iShowCmd);
        uint GetIconLocation([MarshalAs(UnmanagedType.LPWStr)] out StringBuilder pszIconPath, int cchIconPath, out int iIcon);
        uint SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string pszIconPath, int iIcon);
        uint SetRelativePath([MarshalAs(UnmanagedType.LPWStr)] string pszPathRel, uint dwReserved);
        uint Resolve(IntPtr hwnd, uint fFlags);
        uint SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
    }
}

