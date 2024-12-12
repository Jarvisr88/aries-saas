namespace DevExpress.Xpf.Core.Native
{
    using System;

    [Flags]
    internal enum GetWindowCommands
    {
        public const GetWindowCommands CHILD = GetWindowCommands.CHILD;,
        public const GetWindowCommands ENABLED_POPUP = GetWindowCommands.ENABLED_POPUP;,
        public const GetWindowCommands HWND_FIRST = GetWindowCommands.HWND_FIRST;,
        public const GetWindowCommands HWND_LAST = GetWindowCommands.HWND_LAST;,
        public const GetWindowCommands HWND_NEXT = GetWindowCommands.HWND_NEXT;,
        public const GetWindowCommands HWND_PREV = GetWindowCommands.HWND_PREV;,
        public const GetWindowCommands OWNER = GetWindowCommands.OWNER;
    }
}

